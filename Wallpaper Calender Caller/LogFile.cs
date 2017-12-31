using System;
using System.IO;

namespace Wallpaper_Calender_Caller
{
    class LogFile
    {
        private string path;
        private bool active;
        public LogFile(string ppath, bool aactive) { path = ppath; active = aactive; }

        public void writeLine(DateTime date, string error)
        {
            if (active)
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("'{0}' :: {1}", date.ToString("G"), error);
                }
            }
        }
        public void writeLine(Tuple<DateTime, string>[] errors)
        {
            if (active)
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    foreach (Tuple<DateTime, string> error in errors)
                        w.WriteLine("'{0}' :: {1}", error.Item1.ToString("G"), error.Item2);
                }
            }
        }
    }
}
