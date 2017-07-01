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
                    SaveFile saveFile = new SaveFile(args[1]);
                    XElement root = saveFile.getXML().Root.Element("CurrentDate");
                    // If it is the same day, and its a slideshow, don't change it.
                    if (DateTime.Now.ToShortDateString() == root.Element("Day").Value && !Path.HasExtension(root.Element("File").Value))
                        return;

                    DateEntry wallpaperData;
                    wallpaperData = saveFile.GetCurrentWallpaper(DateTime.Now);
                    string wallPaper = wallpaperData.fileName;
                    string newFile = "";
                    if (!Path.HasExtension(wallpaperData.fileName))
                    {
                        string lastEntry = root.Element("LastEntry").Value;
                        newFile = saveFile.GetRandomWallpaper(wallpaperData.fileName, lastEntry);
                        if (newFile != "")
                            wallPaper = newFile;
                        else
                            return;
                    }
                    saveFile.SetWallpaper(wallPaper, wallpaperData.style);
                    root.Element("Day").Value = DateTime.Now.ToShortDateString();
                    root.Element("File").Value = wallpaperData.fileName;
                    root.Element("Style").Value = wallpaperData.style.ToString();
                    root.Element("LastEntry").Value = newFile;
                    Globals.Beautify(saveFile.getXML(), saveFile.getSettingsFileName());
                }
                catch { }
            }
        }
    }
}
