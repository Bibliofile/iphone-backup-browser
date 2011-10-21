using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace DatabaseConverter
{
    public static class DatabaseConverter
    {
        public static void ConvertLocation(string locationDatabase, string targetDir, bool createCsvInsteadOfKml, bool dumpWifiInsteadOfCellular)
        {
            var outputFileName = Path.Combine(targetDir,
                                              "Location_" + (dumpWifiInsteadOfCellular ? "Wifi" : "Cell") + "." +
                                              (createCsvInsteadOfKml ? "csv" : "kml"));

            LocationWriter writer;
            if (createCsvInsteadOfKml)
            {
                writer = new LocationWriterCSV();
            }
            else
            {
                writer = new LocationWriterKML();
            }

            writer.WriteHeader(outputFileName);

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + locationDatabase);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT Timestamp, Latitude, Longitude FROM " + (dumpWifiInsteadOfCellular ? "WifiLocation" : "CellLocation") + ";",
                                                      connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var data = float.Parse(reader[0].ToString().Split('.')[0]);
                DateTime time = new DateTime(2001, 1, 1);
                time = time.AddSeconds(data);
                    
                writer.WriteData(time.ToString(), reader[2].ToString().Replace(',', '.'), reader[1].ToString().Replace(',', '.'));
            }

            writer.WriteFooter();

            reader.Close();
            reader.Dispose();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public static int ConvertNotes(string notesDatabase, string targetDir)
        {
            var notes = 0;
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + notesDatabase);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT Z_PK, ZCREATIONDATE, ZTITLE FROM ZNOTE;", connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                notes++;
                var data = int.Parse(reader[1].ToString().Replace(',', '.').Split('.')[0]);
                DateTime time = new DateTime(2001, 1, 1);
                time = time.AddSeconds(data);

                var key = reader[0].ToString();
                var title = reader[2].ToString();

                SQLiteCommand notecommand = new SQLiteCommand("SELECT ZCONTENT FROM ZNOTEBODY WHERE Z_PK=" + key + ";", connection);

                var notereader = notecommand.ExecuteReader();

                notereader.Read();
                var content = notereader[0].ToString();

                var file = File.CreateText(Path.Combine(targetDir, "Note " + notes + ".html"));
                file.WriteLine("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\"><title>" + title + "</title></head>");
                file.WriteLine("<body><h1>" + title + "</h1><h3>" + time + "</h3>");
                file.WriteLine(content);
                file.Write("</body></html>");

                file.Close();

                notereader.Close();
                notereader.Dispose();
                notecommand.Dispose();
            }

            reader.Close();
            reader.Dispose();
            command.Dispose();
            connection.Close();
            connection.Dispose();
            return notes;
        }

        public static int ConvertSms(string smsDatabase, string targetDir)
        {
            var sms = 0;
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + smsDatabase);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT address, date, text, flags FROM message;", connection);

            var reader = command.ExecuteReader();

            var file = File.CreateText(Path.Combine(targetDir, "sms.csv"));
            file.WriteLine("\"Time\",\"Direction\",\"Address\",\"Text\"");

            while (reader.Read())
            {
                sms++;
                var data = int.Parse(reader[1].ToString().Replace(',', '.').Split('.')[0]);
                DateTime time = new DateTime(2001, 1, 1);
                time = time.AddSeconds(data);

                var address = reader[0].ToString();
                var text = reader[2].ToString();
                var direction = reader[3].ToString();
                if (direction == "3") direction = "OUT";
                if (direction == "2") direction = "IN";


                file.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", time, direction, address, text));
            }

            file.Close();

            reader.Close();
            reader.Dispose();
            command.Dispose();
            connection.Close();
            connection.Dispose();
            return sms;

        }

        public static int ConvertCallHistory(string callsDatabase, string targetDir)
        {
            var calls = 0;
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + callsDatabase);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT address, date, duration, flags FROM call;", connection);

            var reader = command.ExecuteReader();

            var file = File.CreateText(Path.Combine(targetDir, "call history.csv"));
            file.WriteLine("\"Date\",\"Direction\",\"Address\",\"Duration\"");

            while (reader.Read())
            {
                calls++;
                var data = int.Parse(reader[1].ToString().Split('.')[0]);
                DateTime time = new DateTime(2001, 1, 1);
                time = time.AddSeconds(data);

                var address = reader[0].ToString();
                var duration = reader[2].ToString();
                int d;
                if (int.TryParse(duration, out d))
                {
                    TimeSpan ts = new TimeSpan(0, 0, d);
                    duration = ts.ToString();
                }
                var direction = reader[3].ToString();
                direction = direction == "5" ? "OUT" : "IN";


                file.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", time, direction, address, duration));
            }

            file.Close();

            reader.Close();
            reader.Dispose();
            command.Dispose();
            connection.Close();
            connection.Dispose();
            return calls;
        }

        private static bool DoesTableExist(SQLiteConnection database, String tableName)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE name='" + tableName + "';", database);
            var rdr = cmd.ExecuteReader();
            return rdr.HasRows;
        }

        public static int ConvertCalendar(string database, string targetFolder)
        {
            var count = 0;
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + database);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT ROWID, title FROM Calendar;", connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var rowId = reader[0].ToString();
                var title = RemoveIllegalCharsInFileName(reader[1].ToString());

                var newCalendarScheme = DoesTableExist(connection, "CalendarItem");
                var table = newCalendarScheme ? "CalendarItem" : "Event";
                var locationColumn = newCalendarScheme ? "location_id" : "location";
                SQLiteCommand itemCommand =
                    new SQLiteCommand(
                        "SELECT summary, start_date, end_date, description, " + locationColumn + " FROM " + table + " WHERE calendar_id=" +
                        rowId + ";", connection);



                var itemReader = itemCommand.ExecuteReader();

                if (!itemReader.HasRows)
                    continue;

                count++;
                var file = File.CreateText(Path.Combine(targetFolder, "Calendar " + title + ".ics"));
                file.WriteLine("BEGIN:VCALENDAR");
                file.WriteLine("VERSION:2.0");

                while (itemReader.Read())
                {
                    var eventtitle = itemReader[0].ToString();
                    int startSeconds;
                    int endSeconds;

                    DateTime startTime = new DateTime(2001, 1, 1);
                    DateTime endTime = new DateTime(2001, 1, 1);
                    if (int.TryParse(itemReader[1].ToString(), out startSeconds))
                        startTime = startTime.AddSeconds(startSeconds);
                    if (int.TryParse(itemReader[2].ToString(), out endSeconds))
                        endTime = endTime.AddSeconds(endSeconds);

                    var description = itemReader[3].ToString();
                    var locationId = itemReader[4].ToString();
                    var location = "";

                    if (newCalendarScheme)
                    {
                        SQLiteCommand locationCommand =
                            new SQLiteCommand("SELECT title FROM Location WHERE ROWID=" + locationId + ";", connection);
                        var locationReader = locationCommand.ExecuteReader();

                        while (locationReader.Read())
                        {
                            location = locationReader[0].ToString();
                        }

                        locationReader.Close();
                        locationReader.Dispose();
                        locationCommand.Dispose();
                    }
                    else
                        location = locationId;

                    file.WriteLine("BEGIN:VEVENT");
                    file.WriteLine("DTSTART:" + startTime.ToUniversalTime().ToString("yyyyMMdd") + "T" +
                                   startTime.ToUniversalTime().ToString("HHmmss") + "Z");
                    file.WriteLine("DTEND:" + endTime.ToUniversalTime().ToString("yyyyMMdd") + "T" +
                                   endTime.ToUniversalTime().ToString("HHmmss") + "Z");
                    file.WriteLine("SUMMARY:" + eventtitle);
                    file.WriteLine("LOCATION:" + location);
                    file.WriteLine("DESCRIPTION:" + description);
                    file.WriteLine("END:VEVENT");
                }

                itemReader.Close();
                itemReader.Dispose();
                itemCommand.Dispose();

                file.WriteLine("END:VCALENDAR");
                file.Close();
            }


            reader.Close();
            reader.Dispose();
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return count;
        }

        public static int ConvertAddressBook(string database, string targetFolder, bool singleFile)
        {
            var vCards = 0;

            StreamWriter file = null;

            if (singleFile)
                file = File.CreateText(Path.Combine(targetFolder, "Contacts.vcf"));

            SQLiteConnection con = new SQLiteConnection("Data Source=" + database);
            con.Open();

            var multiValueLabels = AddressBookValueLabels(con);
            var addressLabels = AddressBookAddressValueLabels(con);

            SQLiteCommand command = new SQLiteCommand("SELECT ROWID, First, Last, Middle, Organization, Department, Note, Birthday, JobTitle, Prefix, Suffix FROM ABPerson;", con);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                vCards++;
                var row = reader[0].ToString();
                var firstName = reader[1].ToString();
                var middleName = reader[3].ToString();
                var lastName = reader[2].ToString();
                var organization = reader[4].ToString();
                var department = reader[5].ToString();
                var note = reader[6].ToString();
                var birthday = DateTime.MinValue;
                var bday = reader[7].ToString().Split('.')[0];
                int bdayseconds;
                if (int.TryParse(bday, out bdayseconds))
                {
                    birthday = new DateTime(2001, 1, 1).AddSeconds(bdayseconds);
                }
                var jobtitle = reader[8].ToString();
                var prefix = reader[9].ToString();
                var suffix = reader[10].ToString();

                List<StringStringPair> additionalEntries = new List<StringStringPair>();

                List<StringStringPair> phoneNumber = new List<StringStringPair>(); //phone number
                List<StringStringPair> email = new List<StringStringPair>();
                List<Address> address = new List<Address>();

                SQLiteCommand multiValueCommand = new SQLiteCommand("SELECT property, label, value, UID FROM ABMultiValue WHERE record_id=" + row + ";", con);

                var multiValueReader = multiValueCommand.ExecuteReader();
                while (multiValueReader.Read())
                {
                    var prop = multiValueReader[0].ToString();
                    var lab = multiValueReader[1].ToString();

                    var property = int.Parse(string.IsNullOrEmpty(prop) ? "0" : prop);
                    var label = int.Parse(string.IsNullOrEmpty(lab) ? "0" : lab);
                    var value = multiValueReader[2].ToString();
                    var uid = multiValueReader[3].ToString();
                    switch (property)
                    {
                        case 3: //seems to be phone number
                            StringStringPair p = new StringStringPair {Key = value, Value = multiValueLabels[label]};
                            phoneNumber.Add(p);
                            break;
                        case 4: //seems to be email
                            StringStringPair e = new StringStringPair { Key = value, Value = multiValueLabels[label] };
                            email.Add(e);
                            break;
                        case 5: //address...
                            SQLiteCommand addressCommand =
                                new SQLiteCommand(
                                    "SELECT key, value FROM ABMultiValueEntry WHERE parent_id=" + uid + ";", con);

                            Address a = new Address {Key = multiValueLabels[label]};
                            var addressReader = addressCommand.ExecuteReader();
                            while (addressReader.Read())
                            {
                                var k = addressReader[0].ToString();
                                var key = int.Parse(string.IsNullOrEmpty(k) ? "0" : k);
                                var avalue = addressReader[1].ToString();
                                StringStringPair s = new StringStringPair {Key = addressLabels[key], Value = avalue};
                                a.AddressData.Add(s);
                            }
                            address.Add(a);
                            break;
                        case 22: //seems to be additional entry
                            StringStringPair b = new StringStringPair {Key = multiValueLabels[label], Value = value};
                            additionalEntries.Add(b);
                            break;
                    }
                }
                multiValueReader.Close();
                multiValueReader.Dispose();
                multiValueCommand.Dispose();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("BEGIN:VCARD");
                sb.AppendLine("VERSION:3.0");
                sb.AppendLine("FN;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE:");
                sb.AppendLine("N;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE:" + lastName + ";" + firstName + ";" + middleName + ";" + prefix + ";" + suffix);
                    
                if (birthday != DateTime.MinValue)
                {
                    sb.AppendLine("BDAY:" + birthday.ToString("yyyyMMdd"));
                }

                foreach (var p in phoneNumber)
                {
                    sb.AppendLine("TEL;TYPE=" + p.Value + ":" + p.Key);
                }

                foreach (var a in address)
                {
                    var country = "";
                    var locality = "";
                    var postalCode = "";
                    var street = "";

                    foreach (var s in a.AddressData)
                    {
                        var key = s.Key.ToLower();

                        if (key.Contains("zip"))
                        { postalCode = s.Value; continue; }
                        if (key.Contains("city"))
                        { locality = s.Value; continue; }
                        if (key.Contains("countrycode"))
                        { continue; }
                        if (key.Contains("country"))
                        { country = s.Value; continue; }
                        if (key.Contains("street"))
                        { street = s.Value; continue; }
                    }

                    sb.AppendLine("ADR;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE;" + a.Key + ":;;"
                                  + street + ";" + locality + ";;" +
                                  postalCode + ";" + country);
                }

                foreach (var e in email)
                {
                    sb.AppendLine("EMAIL; INTERNET:" + e.Key);
                }

                    
                sb.AppendLine("NOTE;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE:" + note);
                sb.AppendLine("ORG;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE:" + organization);
                sb.AppendLine("ROLE;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE:" + department);
                sb.AppendLine("TITLE;CHARSET=utf-8;ENCODING=QUOTED-PRINTABLE:" + jobtitle);
                sb.AppendLine("END:VCARD");

                if (!singleFile)
                    file = File.CreateText(Path.Combine(targetFolder, RemoveIllegalCharsInFileName(firstName + " " + lastName + ".vcf")));
                file.Write(sb.ToString());
                if (!singleFile)
                    file.Close();
            }

            if (singleFile)
                file.Close();

            reader.Close();
            reader.Dispose();
            command.Dispose();
            con.Close();
            return vCards;
        }

        private class Address
        {
            public string Key { get; set; }
            public List<StringStringPair> AddressData { get; private set; }
            public Address()
            {
                AddressData = new List<StringStringPair>();
            }
        }
        private class StringStringPair
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        private static List<string> AddressBookValueLabels(SQLiteConnection con)
        {
            var ret = new List<string> {"HOME"}; //failback.
            SQLiteCommand com = new SQLiteCommand("SELECT value FROM ABMultiValueLabel;", con);

            var reader = com.ExecuteReader();

            while (reader.Read())
            {
                var label = reader[0].ToString().ToLower();
                var addlabel = "HOME";
                if (label.Contains("mobile"))
                    addlabel = "CELL";
                if (label.Contains("home"))
                    addlabel = "HOME";
                if (label.Contains("work"))
                    addlabel = "WORK";
                if (label.Contains("main"))
                    addlabel = "PREF";
                if (label.Contains("iphone"))
                    addlabel = "CELL";
                ret.Add(addlabel);
            }

            reader.Close();
            reader.Dispose();
            com.Dispose();

            return ret;
        }

        private static string RemoveIllegalCharsInFileName(string fileName)
        {
            var illegalChars = Path.GetInvalidFileNameChars();

            foreach (var c in illegalChars)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName;
        }

        private static List<string> AddressBookAddressValueLabels(SQLiteConnection con)
        {
            var ret = new List<string> { "HOME" }; //failback.
            SQLiteCommand com = new SQLiteCommand("SELECT value FROM ABMultiValueEntryKey;", con);

            var reader = com.ExecuteReader();

            while (reader.Read())
            {
                var label = reader[0].ToString();
                ret.Add(label);
            }

            reader.Close();
            reader.Dispose();
            com.Dispose();

            return ret;
        }
    }
}
