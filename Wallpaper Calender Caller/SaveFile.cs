using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Wallpaper_Calender_Caller
{
    public class SaveFile
    {
        private string settingFileName;
        private XDocument settingsFile = new XDocument();

        public SaveFile(string settingFileName) {
            this.settingFileName = settingFileName;
            this.settingsFile = XDocument.Load(settingFileName);
        }
        public SaveFile(XDocument settingsFile){
            this.settingsFile = settingsFile;
            this.settingFileName = "";
        }
        public XDocument getXML(){return settingsFile;}
        public string getSettingsFileName() { return settingFileName; }
        public void setSettingsFileName(string file) {
            settingFileName = file;
            settingsFile = XDocument.Load(file);
        }
        public List<DateEntry> LoadDates()
        {
            List<DateEntry> ret = new List<DateEntry>();
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
        public DateEntry GetRandomWallpaper(string folder, string lastEntry)
        {
            // *.bmp; *.jpeg; *.jpg; *.png"
            DateEntry ret = new DateEntry();
            ret.errorList = new List<Tuple<DateTime, string>>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                string[] extensionArray = new string[] { ".bmp", ".jpeg", ".jpg", ".png" };
                HashSet<string> allowedExtensions = new HashSet<string>(extensionArray, StringComparer.OrdinalIgnoreCase);
                Random rd = new Random();
                int ran = -1;
                FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));
                for (int i = 0; i < 10; i++)
                {
                    ran = rd.Next(files.Count());
                    if (files[ran].Name == lastEntry)
                    {
                        if (i != 0)
                            ret.errorList.Add(Tuple.Create(DateTime.Now, "Randomed same wallpaper " + i.ToString() + " times in a row."));
                    }
                    else
                        break;
                    if (i == 9)
                        ret.errorList.Add(Tuple.Create(DateTime.Now, "Randomed same wallpaper 10 times in a row."));
                }
                ret.fileName = Path.Combine(folder, files[ran].Name);
                ret.errorList.Add(Tuple.Create(DateTime.Now, "Found wallpaper :: " + ret.fileName));
                return ret;
            }
            catch (Exception e)
            {
                ret.errorList.Add(Tuple.Create(DateTime.Now, e.ToString()));
                ret.fileName = "";
                return ret;
            }
        }
        public List<DateEntry> ReadXMLData()
        {
            XElement root = settingsFile.Root.Element("Calender");
            List<DateEntry> ret = new List<DateEntry>();
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
        public DateEntry GetCurrentWallpaper(DateTime date)
        {
            List<DateEntry> allData = ReadXMLData();
            if (allData.Count == 0) return new DateEntry(date, "", Wallpaper.Style.Stretched);
            else if (allData.Count == 1) return new DateEntry(date, allData[0].fileName, allData[0].style);

            DateEntry previousVal = new DateEntry(date, "", Wallpaper.Style.Stretched);
            foreach (DateEntry entry in allData)
            {
                if ((entry.date.Month < date.Month) || (entry.date.Month == date.Month && entry.date.Day <= date.Day))
                    previousVal = entry;
                else
                {
                    if (previousVal.fileName == "")
                        // Then we actually want the final entry
                        return allData.Last();
                    else
                        return previousVal;
                }
            }
            return previousVal;
        }
        public void SetWallpaper(string file, Wallpaper.Style style)
        {
            Wallpaper.Set(new Uri(file), style);
        }
    }
}
