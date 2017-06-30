using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Wallpaper_Calender_Caller
{
    public static class Globals
    {
       static string settingsFile;

      public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        static public void Beautify(this XDocument doc, string saveFile)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(saveFile, settings))
            {
                doc.Save(writer);
            }
        }

        static public string getSettingsFile() {return settingsFile;}
        static public void setSettingsFile(string file){settingsFile = file;}

    }

    public class Settings
    {
        public string file;
        public Wallpaper.Style style;
        public Settings()
        {
            file = "";
            style = Wallpaper.Style.Stretched;
        }
        public Settings(string file, Wallpaper.Style style)
        {
            this.file = file;
            this.style = style;
        }
    }

    public enum Months
    {
        Zero,
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public struct DateEntry
    {
        public DateTime date;
        public string fileName;
        public Wallpaper.Style style;
        public DateEntry(DateTime xdate, string xfilename, Wallpaper.Style xstyle)
        {
            date = xdate;
            fileName = xfilename;
            style = xstyle;
        }
    }
}
