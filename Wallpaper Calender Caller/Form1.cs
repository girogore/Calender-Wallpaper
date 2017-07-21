using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Pabo.Calendar;

namespace Wallpaper_Calender_Caller
{
    public partial class Form1 : Form
    {
        string currentLoadedFile = "";
        string defaultTitle = "";
        DateTime timeSinceSave = DateTime.Now;
        Settings copySettings;
        string startingText = "";
        List<DateEntry> boldedDates = new List<DateEntry>();
        SaveFile saveFile;

        public Form1()
        {
            InitializeComponent();
            this.monthCalendar1.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.monthCalendar1.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.monthCalendar1.Header.Text = ((Months)DateTime.Now.Month).ToString();
            this.monthCalendar1.SelectDate(DateTime.Now);
            defaultTitle = this.Text;
            TextReader tr = new StringReader(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<header>\n" +
                "   <CurrentDate>\n" +
                "       <Day>01/01/1999</Day>\n" +
                "       <File> </File>\n" +
                "       <Style>Stretched</Style>\n" +
                "       <LastEntry></LastEntry>" +      
                "   </CurrentDate>\n" +
                "   <Calender>\n" +
                "       <January></January>\n" +
                "       <February></February>\n" +
                "       <March></March>\n" +
                "       <April></April>\n" +
                "       <May></May >\n" +
                "       <June></June>\n" +
                "       <July></July>\n" +
                "       <August></August>\n" +
                "       <September></September>\n" +
                "       <October></October>\n" +
                "       <November></November>\n" +
                "       <December></December>\n" +
                "   </Calender>\n" +
                "</header>");
            saveFile = new SaveFile(XDocument.Load(tr));
            Settings currentSettings = LoadDate(DateTime.Now);
            UpdateSettings(currentSettings, DateTime.Now);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((DateTime.Now - timeSinceSave).TotalSeconds > 30)
            {
                DialogResult result = MessageBox.Show("It has been " + Math.Floor((DateTime.Now - timeSinceSave).TotalSeconds) + " seconds since your last save.\nSave Now?", "Closing...",
                   MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    {
                        XElement root = saveFile.getXML().Root.Element("CurrentDate");
                        // If it is the same day, and its a slideshow, don't change it.
                        root.Element("Day").Value = new DateTime(1999, 1, 1).ToShortDateString();
                        root.Element("File").Value = "";
                        root.Element("Style").Value = "";

                        //foreach (Months month in Enum.GetValues(typeof(Months))){SortMonth(month);}
                        if (currentLoadedFile == "")
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = "Save File| *.xml";
                            saveFileDialog1.Title = "Saving...";
                            saveFileDialog1.RestoreDirectory = true;
                            if (currentLoadedFile != "") saveFileDialog1.InitialDirectory = Path.GetDirectoryName(currentLoadedFile);
                            else saveFileDialog1.RestoreDirectory = true;
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                currentLoadedFile = saveFileDialog1.FileName;
                                saveFile.getXML().Beautify(currentLoadedFile);
                                SetTitleBar(currentLoadedFile);
                                timeSinceSave = DateTime.Now;
                            }
                        }
                        else
                        {
                            saveFile.getXML().Beautify(currentLoadedFile);
                            SetTitleBar(currentLoadedFile);
                        }
                    }
                }
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
        private void SetTitleBar(string newTitle)
        {
            this.Text = defaultTitle + " [" + Globals.ShortenPath(newTitle, 60) + "]";
        }

        private void SetLabel(DateTime date)
        {
            this.currentWPLabel.Text = Globals.ShortenPath(saveFile.GetCurrentWallpaper(date).fileName, 50);
        }
        private void setStartingText(string file)
        {
            startingText = file;
            setBT.Text = "SET";
        }

        private void UpdateSettings(Settings settings, DateTime date)
        {
            tiledRB.Checked = false;
            stretchedRB.Checked = false;
            centeredRB.Checked = false;
            switch (settings.style)
            {
                case Wallpaper.Style.Centered: centeredRB.Checked = true; break;
                case Wallpaper.Style.Tiled: tiledRB.Checked = true; break;
                case Wallpaper.Style.Stretched: stretchedRB.Checked = true; break;
            }
            textBox1.Text = settings.file;
            setStartingText(settings.file);
            SetLabel(date);
        }

        private Settings GetCurrentSettings()
        {
            Wallpaper.Style style = Wallpaper.Style.Stretched;
            if (centeredRB.Checked) style = Wallpaper.Style.Centered;
            else if (tiledRB.Checked) style = Wallpaper.Style.Tiled;
            else if (stretchedRB.Checked) style = Wallpaper.Style.Stretched;
            return new Settings(textBox1.Text, style);
        }

        private XDocument LoadSettingsFile(string path)
        {
            if (File.Exists(path))
            {
                saveFile.setSettingsFileName(path);
                return XDocument.Load(path);
            }
            return new XDocument();
        }

        private Settings LoadDate(DateTime loadDate)
        {
            //Go through settings, find date, load settings
            XElement root = saveFile.getXML().Root.Element("Calender").Element(((Months)loadDate.Month).ToString());
            Match match;
            foreach (XElement node in root.Elements())
            {
                match = Regex.Match(node.Name.ToString(), @"\d+");
                if (Convert.ToInt32(match.Value) == loadDate.Day)
                {
                    try { return new Settings(node.Element("File").Value, Globals.ToEnum<Wallpaper.Style>(node.Element("Style").Value)); }
                    catch { node.Remove(); }
                }
            }
            return new Settings();
        }

        private bool RemoveDate(DateTime removeDate)
        {
            XElement root = saveFile.getXML().Root.Element("Calender").Element(((Months)removeDate.Month).ToString());
            Match match;
            foreach (XElement node in root.Elements())
            {
                match = Regex.Match(node.Name.ToString(), @"\d+");
                if (Convert.ToInt32(match.Value) == removeDate.Day)
                {
                    node.Remove();
                    SortMonth((Months)removeDate.Month);
                    return true;
                }
            }
            return false;
        }

        private void SetDate(DateTime setDate, Settings settings)
        {
            if (textBox1.Text == "") { return; }
            XElement root = saveFile.getXML().Root.Element("Calender").Element(((Months)setDate.Month).ToString());
            Match match;
            bool exists = false;
            foreach (XElement node in root.Elements())
            {
                match = Regex.Match(node.Name.ToString(), @"\d+");
                if (Convert.ToInt32(match.Value) == setDate.Day)
                {
                    node.Element("File").Value = settings.file;
                    node.Element("Style").Value = settings.style.ToString();
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                XElement fileName = new XElement("File", settings.file);
                XElement styleType = new XElement("Style", settings.style.ToString());
                XElement dayName = new XElement("Day" + setDate.Day.ToString("00"));

                root.Add(dayName);
                dayName.Add(fileName);
                dayName.Add(styleType);
            }
            SortMonth((Months)setDate.Month);
        }

        private void SortMonth(Months month)
        {
            if (month == Months.Zero) return;
            var root = saveFile.getXML().Root.Element("Calender").Element(month.ToString());
            List<XElement> ordered = root.Elements().OrderBy(e => e.Name.ToString()).ToList();
            root.RemoveAll();
            root.Add(ordered);
        }

        private void browseBT_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Common Image Files|*.bmp; *.jpeg; *.jpg; *.png";
            openFileDialog1.Title = "Select an image file.";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) textBox1.Text = openFileDialog1.FileName;
        }
        private void browseFldBT_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            try {openFileDialog1.SelectedPath = Path.GetDirectoryName(textBox1.Text);}
            catch { }
            if (openFileDialog1.ShowDialog() == DialogResult.OK) textBox1.Text = openFileDialog1.SelectedPath;
        }
        private void setBT_Click(object sender, EventArgs e)
        {
            DateTime currentDate = monthCalendar1.SelectedDates[0];
            Settings currentSettings = GetCurrentSettings();
            SetDate(currentDate, currentSettings);
            setStartingText(currentSettings.file);
            if (textBox1.Text != "")
            {
                DateItem di = BoldDate(currentDate, currentSettings.file);
                boldedDates.Add(new DateEntry(currentDate, currentSettings.file, Wallpaper.Style.Centered));
                monthCalendar1.AddDateInfo(di);
                SetLabel(currentDate);
            }
            else
            {
                bool success = RemoveDate(currentDate);
                textBox1.Text = "";
                RemoveBoldDate(currentDate);
            }
        }
        private void clearBT_Click(object sender, EventArgs e)
        {
            DateTime currentDate = monthCalendar1.SelectedDates[0];
            bool success = RemoveDate(currentDate);
            textBox1.Text = "";
            tiledRB.Checked = false;
            stretchedRB.Checked = true;
            centeredRB.Checked = false;
            setBT.Text = "SET";
            RemoveBoldDate(currentDate);
            SetLabel(currentDate);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Save File| *.xml";
            openFileDialog1.Title = "Select a file to Load";
            openFileDialog1.RestoreDirectory = true;
            if (currentLoadedFile != "") openFileDialog1.InitialDirectory = Path.GetDirectoryName(currentLoadedFile);
            else openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFile = new SaveFile(LoadSettingsFile(openFileDialog1.FileName));
                currentLoadedFile = openFileDialog1.FileName;
                SetTitleBar(currentLoadedFile);
                Settings currentSettings = LoadDate(DateTime.Now);
                UpdateSettings(currentSettings, DateTime.Now);
                timeSinceSave = DateTime.Now;
                monthCalendar1.ResetDateInfo();
                boldedDates.Clear();
                boldedDates.AddRange(saveFile.LoadDates().ToArray());
                monthCalendar1.AddDateInfo(BoldTheseDates(boldedDates).ToArray());
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XElement root = saveFile.getXML().Root.Element("CurrentDate");
            // If it is the same day, and its a slideshow, don't change it.
            root.Element("Day").Value = new DateTime(1999, 1, 1).ToShortDateString();
            root.Element("File").Value = "";
            root.Element("Style").Value = "";
            root.Element("LastEntry").Value = "-1";

            
            // foreach (Months month in Enum.GetValues(typeof(Months))) SortMonth(month);
            if (currentLoadedFile == "" || sender.Equals(saveAsToolStripMenuItem))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Save File| *.xml";
                saveFileDialog1.Title = "Saving...";
                saveFileDialog1.RestoreDirectory = true;
                if (currentLoadedFile != "")saveFileDialog1.InitialDirectory = Path.GetDirectoryName(currentLoadedFile);
                else saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    currentLoadedFile = saveFileDialog1.FileName;
                    saveFile.getXML().Beautify(currentLoadedFile);
                    SetTitleBar(currentLoadedFile);
                    timeSinceSave = DateTime.Now;
                }
            }
            else
            {
                saveFile.getXML().Beautify(currentLoadedFile);
                SetTitleBar(currentLoadedFile);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copySettings = GetCurrentSettings();
            this.pasteToolStripMenuItem.Enabled = true;
            pasteBT.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string beforeText = textBox1.Text;
            UpdateSettings(copySettings, monthCalendar1.SelectedDates[0]);
            if (textBox1.Text != beforeText) setBT.Text = "*SET*";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != startingText) setBT.Text = "*SET*";
            else setBT.Text = "SET";
        }

        private DateItem BoldDate(DateTime date, string file)
        {
            DateItem ret = new DateItem();
            ret.Date = date;
            ret.BackColor1 = Color.Green;
            //ret.Text = file;
            return ret;
        }
        private List<DateItem> BoldTheseDates(List<DateEntry> dates)
        {
            List<DateItem> ret = new List<DateItem>();
            foreach (DateEntry date in dates) ret.Add(BoldDate(date.date, date.fileName));
            return ret;
        }
        private void RemoveBoldDate(DateTime date)
        {
            foreach (DateEntry di in boldedDates)
            {
                if (di.date == date)
                {
                    boldedDates.Remove(di);
                    monthCalendar1.RemoveDateInfo(di.date);
                    break;
                }
            }
        }
        private void RemoveBoldDate(DateTime[] dates)
        {
            foreach (DateEntry de in boldedDates)
            {
                if (dates.Contains(de.date))
                {
                    boldedDates.Remove(de);
                    monthCalendar1.RemoveDateInfo(de.date);
                }
            }
        }
        private void monthCalendar1_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            this.monthCalendar1.SelectDate(new DateTime(e.Year, e.Month, 1));
            monthCalendar1.Header.Text = ((Months)e.Month).ToString();
        }

        private void setWallpaperBT_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            DateEntry wallpaperData;
            Settings settings = GetCurrentSettings();
            wallpaperData = new DateEntry(DateTime.Now, settings.file, settings.style);
            string wallPaper = wallpaperData.fileName;
            string newFile = "";
            if (!Path.HasExtension(wallpaperData.fileName))
            {
                newFile = saveFile.GetRandomWallpaper(wallpaperData.fileName, "");
                if (newFile != "")
                    wallPaper = newFile;
                else
                    return;
            }
            saveFile.SetWallpaper(wallPaper, wallpaperData.style);
        }
        private void monthCalendar1_DateChanged(object sender, DayClickEventArgs e)
        {
            DateTime date = Globals.DateFromString(e.Date);
            this.monthCalendar1.SelectDate(date);
            Settings settings = LoadDate(date);
            UpdateSettings(settings, date);
        }
    }
}