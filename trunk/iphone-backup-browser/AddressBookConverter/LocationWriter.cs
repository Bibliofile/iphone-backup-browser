using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace DatabaseConverter
{
    internal abstract class LocationWriter
    {
        public abstract void WriteHeader(string filePath);

        public abstract void WriteFooter();

        public abstract void WriteData(string time, string latitude, string longitude);
    }

    internal class LocationWriterKML : LocationWriter
    {
        private StreamWriter _writer;

        public override void WriteHeader(string filePath)
        {
            _writer = File.CreateText(filePath);
            _writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _writer.WriteLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\">");
            _writer.WriteLine("<Document>");
        }

        public override void WriteFooter()
        {
            _writer.WriteLine("</Document>");
            _writer.Write("</kml>");
            _writer.Close();
            _writer.Dispose();
        }

        public override void WriteData(string time, string latitude, string longitude)
        {
            _writer.WriteLine("<Placemark>");
            _writer.WriteLine("<description>" + time + "</description>");
            _writer.WriteLine("<Point>");
            _writer.WriteLine("<coordinates>" + latitude + "," + longitude + "</coordinates>");
            _writer.WriteLine("</Point>");
            _writer.WriteLine("</Placemark>");

        }
    }


    internal class LocationWriterCSV : LocationWriter
    {
        private StreamWriter _writer;

        public override void WriteHeader(string filePath)
        {
            _writer = File.CreateText(filePath);
            _writer.WriteLine("\"Timestamp\",\"Latitude\",\"Longitude\"");
        }

        public override void WriteFooter()
        {
            _writer.Close();
            _writer.Dispose();
        }

        public override void WriteData(string time, string latitude, string longitude)
        {
            _writer.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\"", time, latitude, longitude));
        }
    }

}
