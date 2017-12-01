using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Wallpaper_Calender_Caller
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool log = false;
            string logFile = "";
            if (args.Count() == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                if (args.Contains("-log")) {
                    log = true;
                }
                if (args.Contains("-caller"))
                {
                    string target = args[Array.IndexOf(args, "-caller") + 1];
                    try
                    {
                        SaveFile saveFile = new SaveFile(target);
                        if (log)
                            logFile = Path.Combine(Path.GetDirectoryName(target), Path.GetFileNameWithoutExtension(target) + ".log");
                        try
                        {

                            XElement root = saveFile.getXML().Root.Element("CurrentDate");
                            // If it is the same day, and its a slideshow, don't change it.
                            if (DateTime.Now.ToShortDateString() == root.Element("Day").Value && !Path.HasExtension(root.Element("File").Value))
                            {
                                if (log)
                                {
                                    using (StreamWriter w = File.AppendText(logFile))
                                        w.WriteLine("'{0}' :: {1}", DateTime.Now.ToString("G"), "Day already checked.");
                                }
                                return;
                            }
                            DateEntry wallpaperData;
                            wallpaperData = saveFile.GetCurrentWallpaper(DateTime.Now);
                            string wallPaper = wallpaperData.fileName;
                            DateEntry newFile = wallpaperData;
                            newFile.errorList = new List<Tuple<DateTime, string>>();
                            if (!Path.HasExtension(wallpaperData.fileName))
                            {
                                string lastEntry = root.Element("LastEntry").Value;
                                newFile = saveFile.GetRandomWallpaper(wallpaperData.fileName, lastEntry);
                                if (newFile.fileName != "")
                                    wallPaper = newFile.fileName;
                                else
                                    return;
                            }
                            saveFile.SetWallpaper(wallPaper, wallpaperData.style);
                            root.Element("Day").Value = DateTime.Now.ToShortDateString();
                            root.Element("File").Value = wallpaperData.fileName;
                            root.Element("Style").Value = wallpaperData.style.ToString();
                            root.Element("LastEntry").Value = newFile.fileName;
                            Globals.Beautify(saveFile.getXML(), saveFile.getSettingsFileName());
                            if (log)
                            {
                                using (StreamWriter w = File.AppendText(logFile))
                                {
                                    foreach (Tuple<DateTime, string> error in wallpaperData.errorList)
                                        w.WriteLine("'{0}' :: {1}", error.Item1.ToString("G"), error.Item2);
                                    foreach (Tuple<DateTime, string> error in newFile.errorList)
                                        w.WriteLine("'{0}' :: {1}", error.Item1.ToString("G"), error.Item2);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (log)
                            {
                                using (StreamWriter w = File.AppendText(logFile))
                                {
                                    w.WriteLine("'{0}' :: {1}", DateTime.Now, e.ToString());
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
