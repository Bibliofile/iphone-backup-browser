namespace iTunesBackupParser
{
    public class BackupFileRecord
    {
        /// <summary>
        /// Filename in backup directory
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Domain backup file belongs to
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Absolute path
        /// </summary>
        public string LinkTarget { get; set; }

        /// <summary>
        /// Hash for some files
        /// </summary>
        public string DataHash { get; set; }

        /// <summary>
        /// Data structure
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// User file belongs to
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Group file belongs to
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// File length
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// Flag, 0 for directories or symlinks
        /// </summary>
        public byte Flag { get; set; }

        /// <summary>
        /// Number of properties attached
        /// </summary>
        public byte PropertyCount { get; set; }

        /// <summary>
        /// Properties attached
        /// </summary>
        public BackupFileProperty[] Properties { get; set; }
    }

    public class BackupFileProperty
    {
        public BackupFileProperty()
        {}

        public BackupFileProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
