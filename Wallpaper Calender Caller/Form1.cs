using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Wallpaper_Calender_Caller
{
    public partial class Form1 : Form
    {
        XDocument settingsFile = new XDocument();
        string currentLoadedFile = "";
        string defaultTitle = "";
        DateTime timeSinceSave = DateTime.Now;
        Settings copySettings;
        string startingText = "";
        List<DateTime> boldedDates = new List<DateTime>();

        public Form1()
        {
            InitializeComponent();
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
            settingsFile = XDocument.Load(tr);
            Settings currentSettings = LoadDate(this.monthCalendar1.SelectionStart);
            UpdateSettings(currentSettings);
            Globals.setSettingsFile("");
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
                        XElement root = settingsFile.Root.Element("CurrentDate");
                        // If it is the same day, and its a slideshow, don't change it.
                        root.Element("Day").Value = new DateTime(1999, 1, 1).ToShortDateString();
                        root.Element("File").Value = "";
                        root.Element("Style").Value = "";

                        foreach (Months month in Enum.GetValues(typeof(Months)))
                        {
                            SortMonth(month);
                        }
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
                                settingsFile.Beautify(currentLoadedFile);
                                SetTitleBar(currentLoadedFile);
                                timeSinceSave = DateTime.Now;
                            }
                        }
                        else
                        {
                            settingsFile.Beautify(currentLoadedFile);
                            SetTitleBar(currentLoadedFile);
                        }
                    }
                }
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        public void SetTitleBar(string newTitle)
        {
            if (newTitle.Length > 60)
            {
                string[] split = newTitle.Split('\\');
                List<string> finalName = new List<string>();
                int count = 0;
                foreach (string folder in split.Reverse()) {
                    count += folder.Length;
                    if (count > 60)
                    {
                        finalName.Reverse();
                        newTitle = "";
                        foreach (string finalFolder in finalName)
                        {
                            newTitle += "\\" + finalFolder;
                        }
                        newTitle = "..." + newTitle;
                    }
                    else
                        finalName.Add(folder);
                }
            }
            this.Text = defaultTitle + " [" + newTitle + "]";
        }

        private void UpdateSettings(Settings settings)
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
        }

        private Settings LoadSettings()
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
                Globals.setSettingsFile(path);
                return XDocument.Load(path);
            }
            return new XDocument();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            Settings settings = LoadDate(e.Start);
            UpdateSettings(settings);
            setStartingText(settings.file);
        }

        private Settings LoadDate(DateTime loadDate)
        {
            //Go through settings, find date, load settings
            XElement root = settingsFile.Root.Element("Calender").Element(((Months)loadDate.Month).ToString());
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
            XElement root = settingsFile.Root.Element("Calender").Element(((Months)removeDate.Month).ToString());
            Match match;
            foreach (XElement node in root.Elements())
            {
                match = Regex.Match(node.Name.ToString(), @"\d+");
                if (Convert.ToInt32(match.Value) == removeDate.Day)
                {
                    node.Remove();
                    return true;
                }
            }
            return false;
        }

        private void SetDate(DateTime setDate, Settings settings)
        {
            if (textBox1.Text == "") { return; }
            XElement root = settingsFile.Root.Element("Calender").Element(((Months)setDate.Month).ToString());
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
        }

        private void SortMonth(Months month)
        {
            if (month == Months.Zero) return;
            var root = settingsFile.Root.Element("Calender").Element(month.ToString());
            List<XElement> ordered = root.Elements().OrderBy(e => e.Name.ToString()).ToList();
            root.RemoveAll();
            root.Add(ordered);
        }

        private void browseBT_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Common Image Files|*.bmp; *.jpeg; *.jpg; *.png";
            openFileDialog1.Title = "Select a Wallpaper OR folder";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
        private void browseFldBT_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            try
            {
                openFileDialog1.SelectedPath = Path.GetDirectoryName(textBox1.Text);
            }
            catch { }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.SelectedPath;
            }
        }
        private void setBT_Click(object sender, EventArgs e)
        {
            DateTime currentDate = monthCalendar1.SelectionStart;
            Settings currentSettings = LoadSettings();
            SetDate(currentDate, currentSettings);
            setStartingText(currentSettings.file);
            if (textBox1.Text != "")
            {
                boldedDates.Add(currentDate);
                monthCalendar1.MonthlyBoldedDates = boldedDates.ToArray();
            }
            else
            {
                bool success = RemoveDate(currentDate);
                textBox1.Text = "";
                boldedDates.Remove(currentDate);
                monthCalendar1.MonthlyBoldedDates = boldedDates.ToArray();
            }
        }

        private void setStartingText(string file)
        {
            startingText = file;
            setBT.Text = "SET";
        }

        private void clearBT_Click(object sender, EventArgs e)
        {
            DateTime currentDate = monthCalendar1.SelectionStart;
            bool success = RemoveDate(currentDate);
            textBox1.Text = "";
            tiledRB.Checked = false;
            stretchedRB.Checked = true;
            centeredRB.Checked = false;
            boldedDates.Remove(currentDate);
            monthCalendar1.MonthlyBoldedDates = boldedDates.ToArray();
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
                settingsFile = LoadSettingsFile(openFileDialog1.FileName);
                currentLoadedFile = openFileDialog1.FileName;
                SetTitleBar(currentLoadedFile);
                Settings currentSettings = LoadDate(this.monthCalendar1.SelectionStart);
                UpdateSettings(currentSettings);
                setStartingText(currentSettings.file);
                timeSinceSave = DateTime.Now;
                boldedDates.AddRange(LoadDates());
                monthCalendar1.MonthlyBoldedDates = boldedDates.ToArray();
            }
        }

        private List<DateTime> LoadDates()
        {
            List<DateTime> ret = new List<DateTime>();
            XDocument settingsFile = XDocument.Load(Globals.getSettingsFile());
            XElement root = settingsFile.Root.Element("Calender");
            Match match;
            foreach (XElement month in root.Elements())
            {
                foreach (XElement day in month.Elements())
                {
                    match = Regex.Match(day.Name.ToString(), @"\d+");
                    ret.Add(new DateTime(DateTime.Now.Year, (int)Globals.ToEnum<Months>(month.Name.ToString()), Convert.ToInt32(match.Value)));
                }
            }
            return ret;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XElement root = settingsFile.Root.Element("CurrentDate");
            // If it is the same day, and its a slideshow, don't change it.
            root.Element("Day").Value = new DateTime(1999, 1, 1).ToShortDateString();
            root.Element("File").Value = "";
            root.Element("Style").Value = "";
            root.Element("LastEntry").Value = "-1";

            foreach (Months month in Enum.GetValues(typeof(Months))) SortMonth(month);
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
                    settingsFile.Beautify(currentLoadedFile);
                    SetTitleBar(currentLoadedFile);
                    timeSinceSave = DateTime.Now;
                }
            }
            else
            {
                settingsFile.Beautify(currentLoadedFile);
                SetTitleBar(currentLoadedFile);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copySettings = LoadSettings();
            this.pasteToolStripMenuItem.Enabled = true;
            pasteBT.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateSettings(copySettings);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != startingText) setBT.Text = "*SET*";
            else setBT.Text = "SET";
        }
    }
}