using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace iTunesBackupParser
{
    public static class BackupInfoParser
    {
        public static Dictionary<string, string> ParseInfo(string backupFolder)
        {
            try
            {
                string infoFile = Path.Combine(backupFolder, "Info.plist");
                if (!File.Exists(infoFile))
                    throw new Exception("No valid backup folder.");

                XmlSerializer serializer = new XmlSerializer(typeof (plist));

                plist info = (plist) serializer.Deserialize(File.OpenRead(infoFile));

                if (info.Item.GetType() != typeof (dict))
                    return null;

                dict _dict = (dict) info.Item;

                Dictionary<string, string> ret = new Dictionary<string, string>();

                for (var i = 0; i < _dict.key.Length; i++)
                {
                    ret.Add(_dict.key[i], _dict.Items[i].ToString());
                }
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
