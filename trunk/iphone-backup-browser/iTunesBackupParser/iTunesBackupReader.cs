using System;
using System.Collections.Generic;
using System.IO;

namespace iTunesBackupParser
{
    public static class iTunesBackupReader
    {
        /// <summary>
        /// Reads all backup data
        /// </summary>
        /// <param name="backupPath">Path to backup folder</param>
        /// <returns>Array of <see cref="BackupFileRecord"/></returns>
        public static BackupFileRecord[] ReadBackup(string backupPath)
        {
            List<BackupFileRecord> records = new List<BackupFileRecord>();

            FileStream backupDatabase = new FileStream(Path.Combine(backupPath, "Manifest.mbdb"), FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[6];

            backupDatabase.Read(buffer, 0, 6);
            if (BitConverter.ToString(buffer, 0) != "6D-62-64-62-05-00") //mbdb\05\00
            {
                backupDatabase.Close();
                throw new IOException("Bad database file.");
            }

            while (backupDatabase.Position < backupDatabase.Length)
            {
                var record = new BackupFileRecord
                                 {
                                     Domain = DatabaseHelpers.GetString(backupDatabase),
                                     Path = DatabaseHelpers.GetString(backupDatabase),
                                     LinkTarget = DatabaseHelpers.GetString(backupDatabase),
                                     DataHash = DatabaseHelpers.GetString(backupDatabase)
                                 };

                DatabaseHelpers.GetString(backupDatabase); //this field is useless

                buffer = new byte[40];

                backupDatabase.Read(buffer, 0, 40);

                record.UserID = BigEndianHelper.ToInt32(buffer, 10);
                record.GroupID = BigEndianHelper.ToInt32(buffer, 14);

                record.FileLength = BigEndianHelper.ToInt64(buffer, 30);
                record.Flag = buffer[38];
                record.PropertyCount = buffer[39];

                record.Properties = new BackupFileProperty[record.PropertyCount];
                for (var i = 0; i < record.PropertyCount; i++)
                {
                    record.Properties[i] = new BackupFileProperty
                                               {
                                                   Name = DatabaseHelpers.GetString(backupDatabase),
                                                   Value = DatabaseHelpers.GetString(backupDatabase)
                                               };
                }

                record.Key = DatabaseHelpers.GetSHA1Hash(record.Domain + "-" + record.Path);

                records.Add(record);
            }

            return records.ToArray();
        }
    }
}
