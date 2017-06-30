using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace Wallpaper_Calender_Caller
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Count() == 0 || args[0] != "-caller")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                if (args[1] == "")
                    return;
                try
                {
                    Globals.setSettingsFile(args[1]);
                    XDocument settingsFile = XDocument.Load(Globals.getSettingsFile());
                    XElement root = settingsFile.Root.Element("CurrentDate");
                    // If it is the same day, and its a slideshow, don't change it.
                    if (DateTime.Now.ToShortDateString() == root.Element("Day").Value && !Path.HasExtension(root.Element("File").Value))
                        return;

                    DateEntry wallpaperData;
                    wallpaperData = GetCurrentWallpaper();
                    string wallPaper = wallpaperData.fileName;
                    Tuple<string, int> newFile = new Tuple<string, int>("", -1);
                    if (!Path.HasExtension(wallpaperData.fileName))
                    {
                        int lastEntry = Convert.ToInt32(root.Element("LastEntry").Value);
                        newFile = GetRandomWallpaper(wallpaperData.fileName, lastEntry);
                        if (newFile.Item1 != "")
                            wallPaper = newFile.Item1;
                        else
                            return;
                    }
                    SetWallpaper(wallPaper, wallpaperData.style);
                    root.Element("Day").Value = DateTime.Now.ToShortDateString();
                    root.Element("File").Value = wallpaperData.fileName;
                    root.Element("Style").Value = wallpaperData.style.ToString();
                    root.Element("LastEntry").Value = newFile.Item2.ToString();
                    Globals.Beautify(settingsFile, Globals.getSettingsFile());
                }
                catch { }
            }
        }

        public static Tuple<string, int> GetRandomWallpaper(string folder, int lastEntry)
        {
            // *.bmp; *.jpeg; *.jpg; *.png"
            DirectoryInfo di = new DirectoryInfo(folder);
            string[] extentionArray = new string[] { ".bmp", ".jpeg", ".jpg", ".png" };
            HashSet<string> allowedExtensions = new HashSet<string>(extentionArray, StringComparer.OrdinalIgnoreCase);
            int ran = -1;
            Random rd = new Random();
            try
            {
                FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));
                for (int i = 0; i < 10; i++)
                {
                    ran = rd.Next(files.Count());
                    if (ran != lastEntry) break;
                }
                return new Tuple<string, int>(Path.Combine(folder, files[ran].Name), ran);
            }
            catch { return new Tuple<string, int>("", -1); }
        }

        public static DateEntry GetCurrentWallpaper()
        {
            List<DateEntry> allData = ReadXMLData(Globals.getSettingsFile());
            DateTime currentTime = DateTime.Now;
            if (allData.Count == 0) return new DateEntry(DateTime.Now, "", Wallpaper.Style.Stretched);
            else if (allData.Count == 1) return new DateEntry(DateTime.Now, allData[0].fileName, allData[0].style);

            DateEntry previousVal = new DateEntry(DateTime.Now, "", Wallpaper.Style.Stretched);
            foreach (DateEntry entry in allData)
            {
                if (entry.date.Month <= DateTime.Now.Month && entry.date.Day <= DateTime.Now.Day)
                {
                    previousVal = entry;
                }
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

        public static List<DateEntry> ReadXMLData(string fileName)
        {
            XDocument settingsFile = XDocument.Load(fileName);
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

        public static void SetWallpaper(string file, Wallpaper.Style style)
        {
            Wallpaper.Set(new Uri(file), style);
        }
    }
}
