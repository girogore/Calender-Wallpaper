using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static List<DateEntry> LoadDates()
        {
            List<DateEntry> ret = new List<DateEntry>();
            XDocument settingsFile = XDocument.Load(Globals.getSettingsFile());
            XElement root = settingsFile.Root.Element("Calender");
            Match match;
            foreach (XElement month in root.Elements())
            {
                foreach (XElement day in month.Elements())
                {
                    match = Regex.Match(day.Name.ToString(), @"\d+");
                    ret.Add(
                        new DateEntry(
                        new DateTime(DateTime.Now.Year, (int)Globals.ToEnum<Months>(month.Name.ToString()), Convert.ToInt32(match.Value)),
                        day.Element("File").Value.ToString(),
                        Globals.ToEnum<Wallpaper.Style>(day.Element("Style").Value.ToString())));
                }
            }
            return ret;
        }

        public static string GetRandomWallpaper(string folder, string lastEntry)
        {
            // *.bmp; *.jpeg; *.jpg; *.png"
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                string[] extentionArray = new string[] { ".bmp", ".jpeg", ".jpg", ".png" };
                HashSet<string> allowedExtensions = new HashSet<string>(extentionArray, StringComparer.OrdinalIgnoreCase);
                Random rd = new Random();
                int ran = -1;
                FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));
                for (int i = 0; i < 10; i++)
                {
                    ran = rd.Next(files.Count());
                    if (files[ran].Name != lastEntry) break;
                }
                return Path.Combine(folder, files[ran].Name);
            }
            catch { return ""; }
        }

        public static void SetWallpaper(string file, Wallpaper.Style style)
        {
            Wallpaper.Set(new Uri(file), style);
        }

        public static DateTime DateFromString(string date)
        {
            string[] split = date.Split('/');
            int month = Convert.ToInt32(split[0]);
            int day = Convert.ToInt32(split[1]);
            int year = Convert.ToInt32(split[2]);
            return new DateTime(year, month, day);
        }
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
