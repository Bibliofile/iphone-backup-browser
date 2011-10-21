using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using iTunesBackupParser;

namespace iPhoneBackupBrowser
{
    public partial class frmMain : Form
    {
        private List<Dictionary<string, string>> _backups;
        private Dictionary<string, string> _activeBackup;

        public frmMain()
        {
            InitializeComponent();
        }

        private string _backupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                  @"Apple Computer\MobileSync\Backup");
        private void FrmMainLoad(object sender, EventArgs e)
        {
            txtTargetPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Extracted from Backup");
            RefreshList();
        }

        private void RefreshList()
        {
            txtBackupFolder.Text = _backupPath;
            treeBackupContent.Nodes.Clear();
            comboBackups.Items.Clear();
            comboBackups.Items.Add("Select backup...");
            lblBackupInfo.Text = "No backup selected.";
            comboBackups.SelectedIndex = 0;

            _backups = new List<Dictionary<string, string>>();
            _activeBackup = null;
            foreach (var d in Directory.GetDirectories(_backupPath))
            {
                var backupInfo = BackupInfoParser.ParseInfo(d);
                if (backupInfo == null) continue;
                if (!backupInfo.ContainsKey("Display Name")) continue;

                backupInfo.Add("BackupPath", d);

                _backups.Add(backupInfo);
                comboBackups.Items.Add(backupInfo["Display Name"]);
            }
        }

        private void ComboBackupSelectedIndexChanged(object sender, EventArgs e)
        {
            _consolidatedDbFile = "";
            _notesDbFile = "";
            _smsDbFile = "";
            _callHistoryDbFile = "";
            _contactsDbFile = "";
            _calendarDbFile = "";
            btnLocationCsv.Enabled = false;
            btnLocationKml.Enabled = false;
            btnNotes.Enabled = false;
            btnSms.Enabled = false;
            btnCalls.Enabled = false;
            btnCamera.Enabled = false;
            btnRecordings.Enabled = false;
            btnContacts.Enabled = false;
            btnContactsSingle.Enabled = false;
            btnCalendar.Enabled = false;

            ComboBox cmb = (ComboBox)sender;

            if (cmb.SelectedItem == null || cmb.SelectedIndex == 0)
            {
                _activeBackup = null;
                lblBackupInfo.Text = "No backup selected.";
                treeBackupContent.Nodes.Clear();
                return;
            }

            _activeBackup = _backups[cmb.SelectedIndex - 1];


            ReadBackup();
        }

        private void ReadBackup()
        {
            Cursor = Cursors.WaitCursor;

            DateTime backupTime = DateTime.Parse(_activeBackup["Last Backup Date"]);

            lblBackupInfo.Text = string.Format("Backup name: {0}\r\nLast backup time: {1}\r\n" +
                                               "Device: {2}\r\nDevice name: {3}\r\niOS version: {4}", _activeBackup["Display Name"], backupTime.ToString(), _activeBackup["Product Type"], _activeBackup["Device Name"], _activeBackup["Product Version"]);

            if (_activeBackup.ContainsKey("Phone Number"))
            {
                lblBackupInfo.Text += "\r\nPhone number: " + _activeBackup["Phone Number"];
            }

            BackupFileRecord[] records;
            try
            {
                records = iTunesBackupReader.ReadBackup(_activeBackup["BackupPath"]);
            }
            catch (Exception)
            {
                MessageBox.Show("Error reading backup. Folder contains no valid backup.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                treeBackupContent.Nodes.Clear();
                return;
            }
            treeBackupContent.BeginUpdate();
            treeBackupContent.Nodes.Clear();
            foreach (var record in records)
            {
                var rpath = record.Path;
                if (record.Domain.StartsWith("AppDomain-"))
                {
                    string domain = record.Domain.Substring(10);
                    rpath = "Applications/" + domain + "/" + record.Path;
                }
                var filepath = rpath.Split('/');
                var nodes = treeBackupContent.Nodes;

                var thisDir = "";
                for (var i = 0; i < filepath.Length - 1; i++)
                {
                    var path = filepath[i];
                    thisDir += "/" + path;
                    if (nodes.ContainsKey(path))
                    {
                        nodes = nodes[path].Nodes;
                    }
                    else
                    {
                        var n = nodes.Add(path, path);
                        n.ToolTipText = thisDir + "\r\n" + "Directory";
                        nodes = n.Nodes;
                    }
                }

                if (filepath.Length > 1 && !nodes.ContainsKey(filepath[filepath.Length - 1]) && !filepath[filepath.Length - 1].StartsWith(".") && filepath[filepath.Length - 1].Split('.').Length > 1)
                {
                    var n = nodes.Add(record.Key, filepath[filepath.Length - 1]);
                    n.Tag = record.Flag;
                    n.ToolTipText = rpath + "\r\n" + record.Key + "\r\n" + record.FileLength + " bytes";
                }
                if (record.Path == "Library/Caches/locationd/consolidated.db")
                {
                    btnLocationCsv.Enabled = true;
                    btnLocationKml.Enabled = true;
                    _consolidatedDbFile = record.Key;
                }
                if (record.Path == "Library/Notes/notes.sqlite")
                {
                    btnNotes.Enabled = true;
                    _notesDbFile = record.Key;
                }
                if (record.Path == "Library/SMS/sms.db")
                {
                    btnSms.Enabled = true;
                    _smsDbFile = record.Key;
                }
                if (record.Path == "Library/CallHistory/call_history.db")
                {
                    btnCalls.Enabled = true;
                    _callHistoryDbFile = record.Key;
                }
                if (record.Path.StartsWith("Media/DCIM/100APPLE"))
                {
                    btnCamera.Enabled = true;
                }
                if (record.Path.StartsWith("Media/Recordings"))
                {
                    btnRecordings.Enabled = true;
                }
                if (record.Path.StartsWith("Library/AddressBook/AddressBook.sqlite"))
                {
                    btnContacts.Enabled = true;
                    btnContactsSingle.Enabled = true;
                    _contactsDbFile = record.Key;
                }
                if (record.Path == "Library/Calendar/Calendar.sqlitedb")
                {
                    btnCalendar.Enabled = true;
                    _calendarDbFile = record.Key;
                }
            }

            treeBackupContent.Sort();
            treeBackupContent.EndUpdate();
            Cursor = Cursors.Default;
        }

        private void TreeContentAfterCheck(object sender, TreeViewEventArgs e)
        {
            treeBackupContent.BeginUpdate();
            CheckRecursive(e.Node, e.Node.Checked);
            treeBackupContent.EndUpdate();
        }

        private void CheckRecursive(TreeNode node, bool check)
        {
            foreach (TreeNode n in node.Nodes)
            {
                n.Checked = check;
                CheckRecursive(n, check);
            }
        }

        private void CopyRecursive(TreeNode node, string targetPath)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Text == n.Name)
                {
                    CopyRecursive(n, targetPath);
                }
                else
                {
                    if (n.Checked)
                    {
                        var target = Path.Combine(targetPath, n.FullPath.Replace('/', Path.DirectorySeparatorChar));
                        if (string.IsNullOrEmpty(target))
                            continue;
                        if (!Directory.Exists(Path.GetDirectoryName(target)))
                            Directory.CreateDirectory(Path.GetDirectoryName(target));
                        if (!File.Exists(Path.Combine(_activeBackup["BackupPath"], n.Name)))
                        {
                            _skippedFiles++;
                            if (n.Tag.ToString() != "0") //no symbolic link or directory
                                MessageBox.Show("File " + n.Name + " alias " + n.FullPath + " was not found. Skip.", "Warning",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                        if (File.Exists(target))
                        {
                            if (chkSkipExisting.Checked)
                            {
                                _skippedFiles++;
                                continue;
                            }
                            File.Delete(target);
                        }
                        File.Copy(Path.Combine(_activeBackup["BackupPath"], n.Name), target);
                        _copiedFiles++;
                    }
                }
            }
        }

        private int _skippedFiles;
        private int _copiedFiles;
        private void BtnCopyClick(object sender, EventArgs e)
        {
            if (!CheckTargetDir())
                return;

            _skippedFiles = 0;
            _copiedFiles = 0;
            foreach (TreeNode node in treeBackupContent.Nodes)
            {
                CopyRecursive(node, txtTargetPath.Text);
            }

            if (_skippedFiles > 0)
                MessageBox.Show(_copiedFiles + " files were copied.\r\n" + _skippedFiles + " files were skipped because they already existed in target folder or were symbolic links.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(_copiedFiles + " files were copied.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnRefreshClick(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void BtnSelectBackupPathClick(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = _backupPath;
            folderBrowserDialog1.ShowDialog();
            _backupPath = folderBrowserDialog1.SelectedPath;
            RefreshList();
        }

        private void BtnSelectTargetDirClick(object sender, EventArgs e)
        {
            folderBrowserDialog2.SelectedPath = txtTargetPath.Text;
            folderBrowserDialog2.ShowDialog();
            txtTargetPath.Text = folderBrowserDialog2.SelectedPath;
        }

        private string _consolidatedDbFile = "";
        private void BtnLocationKmlClick(object sender, EventArgs e)
        {
            ExtractLocationData(false);
        }

        private void BtnLocationCsvClick(object sender, EventArgs e)
        {
            ExtractLocationData(true);
        }

        private void ExtractLocationData(bool csv)
        {
            if (_consolidatedDbFile == "")
            {
                MessageBox.Show("No location data found.", "Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (!CheckTargetDir())
                return;

            try
            {
                DatabaseConverter.DatabaseConverter.ConvertLocation(Path.Combine(_activeBackup["BackupPath"], _consolidatedDbFile), txtTargetPath.Text,
                                                  csv, false);
                DatabaseConverter.DatabaseConverter.ConvertLocation(Path.Combine(_activeBackup["BackupPath"], _consolidatedDbFile), txtTargetPath.Text,
                                                  csv, true);

                MessageBox.Show("Location data successfully exported.", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during location data extraction: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _notesDbFile = "";
        private void BtnNotesClick(object sender, EventArgs e)
        {
            if (_notesDbFile == "")
            {
                MessageBox.Show("No notes data found.", "Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (!CheckTargetDir())
                return;

            try
            {
                var notes = DatabaseConverter.DatabaseConverter.ConvertNotes(Path.Combine(_activeBackup["BackupPath"], _notesDbFile), txtTargetPath.Text);

                MessageBox.Show(notes + " notes successfully exported.", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during notes extraction: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _smsDbFile = "";
        private void BtnSmsClick(object sender, EventArgs e)
        {
            if (_smsDbFile == "")
            {
                MessageBox.Show("No sms data found.", "Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (!CheckTargetDir())
                return;

            try
            {
                var sms = DatabaseConverter.DatabaseConverter.ConvertSms(Path.Combine(_activeBackup["BackupPath"], _smsDbFile), txtTargetPath.Text);

                MessageBox.Show(sms + " sms successfully exported.", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during sms extraction: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _callHistoryDbFile = "";
        private void BtnCallsClick(object sender, EventArgs e)
        {
            if (_callHistoryDbFile == "")
            {
                MessageBox.Show("No call history data found.", "Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (!CheckTargetDir())
                return;

            try
            {
                var entries = DatabaseConverter.DatabaseConverter.ConvertCallHistory(Path.Combine(_activeBackup["BackupPath"], _callHistoryDbFile), txtTargetPath.Text);

                MessageBox.Show(entries + " call history entries successfully exported.", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during call history extraction: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _calendarDbFile = "";
        private void BtnCalendarClick(object sender, EventArgs e)
        {
            if (_calendarDbFile == "")
            {
                MessageBox.Show("No call history data found.", "Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (!CheckTargetDir())
                return;

            try
            {
                var count = DatabaseConverter.DatabaseConverter.ConvertCalendar(Path.Combine(_activeBackup["BackupPath"], _calendarDbFile), txtTargetPath.Text);

                MessageBox.Show(count + " calendars successfully exported.\r\nNote: Only the most important information is extracted. Recurring events will not be extracted as recurring.", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during calendar extraction: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string _contactsDbFile = "";
        private void BtnContactsClick(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            bool singleFile = s.Name.Contains("Single");
            if (_contactsDbFile == "")
            {
                MessageBox.Show("No call history data found.", "Information", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (!CheckTargetDir())
                return;

            try
            {
                var count = DatabaseConverter.DatabaseConverter.ConvertAddressBook(Path.Combine(_activeBackup["BackupPath"], _contactsDbFile), txtTargetPath.Text, singleFile);

                MessageBox.Show(count + " contact entries successfully exported.", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during contacts extraction: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCameraClick(object sender, EventArgs e)
        {
            if (treeBackupContent.Nodes.ContainsKey("Media"))
            {
                TreeNode node = treeBackupContent.Nodes["Media"];
                if (node.Nodes.ContainsKey("DCIM"))
                {
                    node = node.Nodes["DCIM"];
                    if (node.Nodes.ContainsKey("100APPLE"))
                    {
                        node = node.Nodes["100APPLE"];
                        node.Checked = true;
                        treeBackupContent.BeginUpdate();
                        foreach (TreeNode n in node.Nodes)
                        {
                            n.Checked = true;
                        }
                        treeBackupContent.EndUpdate();
                    }
                }
            }
        }

        private void BtnRecordingsClick(object sender, EventArgs e)
        {
            if (treeBackupContent.Nodes.ContainsKey("Media"))
            {
                TreeNode node = treeBackupContent.Nodes["Media"];
                if (node.Nodes.ContainsKey("Recordings"))
                {
                    node = node.Nodes["Recordings"];
                    node.Checked = true;
                    treeBackupContent.BeginUpdate();
                    foreach (TreeNode n in node.Nodes)
                    {
                        n.Checked = true;
                    }
                    treeBackupContent.EndUpdate();
                }
            }
        }

        private bool CheckTargetDir()
        {
            if (string.IsNullOrEmpty(txtTargetPath.Text))
            {
                MessageBox.Show("Please choose destination first.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            if (!Directory.Exists(txtTargetPath.Text))
            {
                Directory.CreateDirectory(txtTargetPath.Text);
            }
            if (!Directory.Exists(txtTargetPath.Text))
            {
                MessageBox.Show("Error creating directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

    }
}
