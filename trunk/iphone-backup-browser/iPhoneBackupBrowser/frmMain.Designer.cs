namespace iPhoneBackupBrowser
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.comboBackups = new System.Windows.Forms.ComboBox();
            this.treeBackupContent = new System.Windows.Forms.TreeView();
            this.btnExtract = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.btnRefreshBackupList = new System.Windows.Forms.Button();
            this.btnBrowseBackupPath = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowseTargetPath = new System.Windows.Forms.Button();
            this.btnLocationKml = new System.Windows.Forms.Button();
            this.btnLocationCsv = new System.Windows.Forms.Button();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnSms = new System.Windows.Forms.Button();
            this.btnCalls = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnRecordings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnContactsSingle = new System.Windows.Forms.Button();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.btnContacts = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBackupFolder = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBackupInfo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.chkSkipExisting = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBackups
            // 
            this.comboBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBackups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBackups.FormattingEnabled = true;
            this.comboBackups.Location = new System.Drawing.Point(94, 28);
            this.comboBackups.Name = "comboBackups";
            this.comboBackups.Size = new System.Drawing.Size(432, 21);
            this.comboBackups.TabIndex = 0;
            this.comboBackups.SelectedIndexChanged += new System.EventHandler(this.ComboBackupSelectedIndexChanged);
            // 
            // treeBackupContent
            // 
            this.treeBackupContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeBackupContent.CheckBoxes = true;
            this.treeBackupContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeBackupContent.ItemHeight = 20;
            this.treeBackupContent.Location = new System.Drawing.Point(0, 0);
            this.treeBackupContent.Name = "treeBackupContent";
            this.treeBackupContent.PathSeparator = "/";
            this.treeBackupContent.ShowNodeToolTips = true;
            this.treeBackupContent.Size = new System.Drawing.Size(622, 333);
            this.treeBackupContent.TabIndex = 1;
            this.treeBackupContent.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeContentAfterCheck);
            // 
            // btnExtract
            // 
            this.btnExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtract.Location = new System.Drawing.Point(492, 449);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(150, 30);
            this.btnExtract.TabIndex = 2;
            this.btnExtract.Text = "extract selected files";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.BtnCopyClick);
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetPath.Location = new System.Drawing.Point(109, 422);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(452, 20);
            this.txtTargetPath.TabIndex = 3;
            // 
            // btnRefreshBackupList
            // 
            this.btnRefreshBackupList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshBackupList.Location = new System.Drawing.Point(591, 27);
            this.btnRefreshBackupList.Name = "btnRefreshBackupList";
            this.btnRefreshBackupList.Size = new System.Drawing.Size(51, 23);
            this.btnRefreshBackupList.TabIndex = 5;
            this.btnRefreshBackupList.Text = "refresh";
            this.btnRefreshBackupList.UseVisualStyleBackColor = true;
            this.btnRefreshBackupList.Click += new System.EventHandler(this.BtnRefreshClick);
            // 
            // btnBrowseBackupPath
            // 
            this.btnBrowseBackupPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseBackupPath.Location = new System.Drawing.Point(532, 27);
            this.btnBrowseBackupPath.Name = "btnBrowseBackupPath";
            this.btnBrowseBackupPath.Size = new System.Drawing.Size(53, 23);
            this.btnBrowseBackupPath.TabIndex = 6;
            this.btnBrowseBackupPath.Text = "browse";
            this.btnBrowseBackupPath.UseVisualStyleBackColor = true;
            this.btnBrowseBackupPath.Click += new System.EventHandler(this.BtnSelectBackupPathClick);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // btnBrowseTargetPath
            // 
            this.btnBrowseTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTargetPath.Location = new System.Drawing.Point(567, 420);
            this.btnBrowseTargetPath.Name = "btnBrowseTargetPath";
            this.btnBrowseTargetPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTargetPath.TabIndex = 7;
            this.btnBrowseTargetPath.Text = "browse";
            this.btnBrowseTargetPath.UseVisualStyleBackColor = true;
            this.btnBrowseTargetPath.Click += new System.EventHandler(this.BtnSelectTargetDirClick);
            // 
            // btnLocationKml
            // 
            this.btnLocationKml.Enabled = false;
            this.btnLocationKml.Location = new System.Drawing.Point(6, 19);
            this.btnLocationKml.Name = "btnLocationKml";
            this.btnLocationKml.Size = new System.Drawing.Size(147, 23);
            this.btnLocationKml.TabIndex = 8;
            this.btnLocationKml.Text = "save location data as kml";
            this.btnLocationKml.UseVisualStyleBackColor = true;
            this.btnLocationKml.Click += new System.EventHandler(this.BtnLocationKmlClick);
            // 
            // btnLocationCsv
            // 
            this.btnLocationCsv.Enabled = false;
            this.btnLocationCsv.Location = new System.Drawing.Point(6, 48);
            this.btnLocationCsv.Name = "btnLocationCsv";
            this.btnLocationCsv.Size = new System.Drawing.Size(147, 23);
            this.btnLocationCsv.TabIndex = 9;
            this.btnLocationCsv.Text = "save location data as csv";
            this.btnLocationCsv.UseVisualStyleBackColor = true;
            this.btnLocationCsv.Click += new System.EventHandler(this.BtnLocationCsvClick);
            // 
            // btnNotes
            // 
            this.btnNotes.Enabled = false;
            this.btnNotes.Location = new System.Drawing.Point(312, 19);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(144, 23);
            this.btnNotes.TabIndex = 10;
            this.btnNotes.Text = "save notes as html";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler(this.BtnNotesClick);
            // 
            // btnSms
            // 
            this.btnSms.Enabled = false;
            this.btnSms.Location = new System.Drawing.Point(462, 19);
            this.btnSms.Name = "btnSms";
            this.btnSms.Size = new System.Drawing.Size(144, 23);
            this.btnSms.TabIndex = 11;
            this.btnSms.Text = "save sms as csv";
            this.btnSms.UseVisualStyleBackColor = true;
            this.btnSms.Click += new System.EventHandler(this.BtnSmsClick);
            // 
            // btnCalls
            // 
            this.btnCalls.Enabled = false;
            this.btnCalls.Location = new System.Drawing.Point(312, 48);
            this.btnCalls.Name = "btnCalls";
            this.btnCalls.Size = new System.Drawing.Size(144, 23);
            this.btnCalls.TabIndex = 12;
            this.btnCalls.Text = "save call history as csv";
            this.btnCalls.UseVisualStyleBackColor = true;
            this.btnCalls.Click += new System.EventHandler(this.BtnCallsClick);
            // 
            // btnCamera
            // 
            this.btnCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCamera.Enabled = false;
            this.btnCamera.Location = new System.Drawing.Point(95, 219);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(181, 23);
            this.btnCamera.TabIndex = 13;
            this.btnCamera.Text = "select all pictures and videos";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.BtnCameraClick);
            // 
            // btnRecordings
            // 
            this.btnRecordings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecordings.Enabled = false;
            this.btnRecordings.Location = new System.Drawing.Point(282, 219);
            this.btnRecordings.Name = "btnRecordings";
            this.btnRecordings.Size = new System.Drawing.Size(181, 23);
            this.btnRecordings.TabIndex = 14;
            this.btnRecordings.Text = "select all recordings";
            this.btnRecordings.UseVisualStyleBackColor = true;
            this.btnRecordings.Click += new System.EventHandler(this.BtnRecordingsClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnContactsSingle);
            this.groupBox2.Controls.Add(this.btnCalendar);
            this.groupBox2.Controls.Add(this.btnContacts);
            this.groupBox2.Controls.Add(this.btnSms);
            this.groupBox2.Controls.Add(this.btnLocationKml);
            this.groupBox2.Controls.Add(this.btnCalls);
            this.groupBox2.Controls.Add(this.btnLocationCsv);
            this.groupBox2.Controls.Add(this.btnNotes);
            this.groupBox2.Location = new System.Drawing.Point(7, 248);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 79);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "convert data (files will be saved in location below; existing files will be overw" +
                "ritten)";
            // 
            // btnContactsSingle
            // 
            this.btnContactsSingle.Enabled = false;
            this.btnContactsSingle.Location = new System.Drawing.Point(159, 19);
            this.btnContactsSingle.Name = "btnContactsSingle";
            this.btnContactsSingle.Size = new System.Drawing.Size(147, 23);
            this.btnContactsSingle.TabIndex = 15;
            this.btnContactsSingle.Text = "save contacts as vcard";
            this.btnContactsSingle.UseVisualStyleBackColor = true;
            this.btnContactsSingle.Click += new System.EventHandler(this.BtnContactsClick);
            // 
            // btnCalendar
            // 
            this.btnCalendar.Enabled = false;
            this.btnCalendar.Location = new System.Drawing.Point(462, 50);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(144, 23);
            this.btnCalendar.TabIndex = 14;
            this.btnCalendar.Text = "save calendar as ical";
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.Click += new System.EventHandler(this.BtnCalendarClick);
            // 
            // btnContacts
            // 
            this.btnContacts.Enabled = false;
            this.btnContacts.Location = new System.Drawing.Point(159, 48);
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(147, 23);
            this.btnContacts.TabIndex = 13;
            this.btnContacts.Text = "save contacts as vcards";
            this.btnContacts.UseVisualStyleBackColor = true;
            this.btnContacts.Click += new System.EventHandler(this.BtnContactsClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 425);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "File save location:";
            // 
            // lblBackupFolder
            // 
            this.lblBackupFolder.AutoSize = true;
            this.lblBackupFolder.Location = new System.Drawing.Point(9, 9);
            this.lblBackupFolder.Name = "lblBackupFolder";
            this.lblBackupFolder.Size = new System.Drawing.Size(112, 13);
            this.lblBackupFolder.TabIndex = 18;
            this.lblBackupFolder.Text = "Current backup folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Select backup:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 465);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "iPhone Backup Browser";
            // 
            // lblBackupInfo
            // 
            this.lblBackupInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackupInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupInfo.Location = new System.Drawing.Point(10, 97);
            this.lblBackupInfo.Name = "lblBackupInfo";
            this.lblBackupInfo.Size = new System.Drawing.Size(609, 119);
            this.lblBackupInfo.TabIndex = 21;
            this.lblBackupInfo.Text = "No backup selected.\r\nReserved space.";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(630, 359);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.btnRecordings);
            this.tabPage3.Controls.Add(this.lblBackupInfo);
            this.tabPage3.Controls.Add(this.btnCamera);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(622, 333);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Easy mode";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(615, 93);
            this.label4.TabIndex = 22;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeBackupContent);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(622, 333);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Advanced";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBackupFolder.Location = new System.Drawing.Point(127, 9);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.ReadOnly = true;
            this.txtBackupFolder.Size = new System.Drawing.Size(515, 13);
            this.txtBackupFolder.TabIndex = 24;
            // 
            // chkSkipExisting
            // 
            this.chkSkipExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSkipExisting.AutoSize = true;
            this.chkSkipExisting.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSkipExisting.Checked = true;
            this.chkSkipExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkipExisting.Location = new System.Drawing.Point(380, 457);
            this.chkSkipExisting.Name = "chkSkipExisting";
            this.chkSkipExisting.Size = new System.Drawing.Size(106, 17);
            this.chkSkipExisting.TabIndex = 25;
            this.chkSkipExisting.Text = "Skip existing files";
            this.chkSkipExisting.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Easy selection:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 487);
            this.Controls.Add(this.chkSkipExisting);
            this.Controls.Add(this.txtBackupFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblBackupFolder);
            this.Controls.Add(this.comboBackups);
            this.Controls.Add(this.btnBrowseBackupPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRefreshBackupList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowseTargetPath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.btnExtract);
            this.MinimumSize = new System.Drawing.Size(670, 525);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iPhone Backup Browser";
            this.Load += new System.EventHandler(this.FrmMainLoad);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBackups;
        private System.Windows.Forms.TreeView treeBackupContent;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Button btnRefreshBackupList;
        private System.Windows.Forms.Button btnBrowseBackupPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Button btnBrowseTargetPath;
        private System.Windows.Forms.Button btnLocationKml;
        private System.Windows.Forms.Button btnLocationCsv;
        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.Button btnSms;
        private System.Windows.Forms.Button btnCalls;
        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.Button btnRecordings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBackupFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBackupInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnContacts;
        private System.Windows.Forms.Button btnCalendar;
        private System.Windows.Forms.Button btnContactsSingle;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.CheckBox chkSkipExisting;
        private System.Windows.Forms.Label label5;
    }
}

