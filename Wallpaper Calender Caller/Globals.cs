using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Wallpaper_Calender_Caller
{
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

    public static class Globals
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static DateTime DateFromString(string date)
        {
            string[] split = date.Split('/');
            int month = Convert.ToInt32(split[0]);
            int day = Convert.ToInt32(split[1]);
            int year = Convert.ToInt32(split[2]);
            return new DateTime(year, month, day);
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
        static public string ShortenPath(string newTitle, int length)
        {
            if (newTitle.Length > length)
            {
                string[] split = newTitle.Split('\\');
                List<string> finalName = new List<string>();
                int count = 0;
                foreach (string folder in split.Reverse())
                {
                    count += folder.Length;
                    if (count > length)
                    {
                        if (finalName.Count != 0) {
                            finalName.Reverse();
                            newTitle = "";
                            foreach (string finalFolder in finalName)
                            {
                                newTitle += "\\" + finalFolder;
                            }
                            newTitle = "..." + newTitle;
                        }
                        else newTitle = "..." + split.Last().Substring(split.Last().Count() - length);
                    }
                    else
                        finalName.Add(folder);
                }
            }

            return newTitle;
        }
    }
}
