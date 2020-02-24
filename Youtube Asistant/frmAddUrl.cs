using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Asistant
{
    public partial class frmAddUrl : Form
    {
        public frmAddUrl(frmMain main)
        {
            InitializeComponent();
            frmMain = main;
        }
        frmMain frmMain;
        private void btnPath_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog fbd = new FolderBrowserDialog() { Description="Select your path" ,SelectedPath=txtPath.Text})
            {
                if (fbd.ShowDialog()==DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void frmAddUrl_Load(object sender, EventArgs e)
        {
            txtPath.Text = Properties.Settings.Default.Path;
            //TODO : read clipbord and put into url
        }

        private void btnAddUrl_Click(object sender, EventArgs e)
        {
            Database.VideoRow row = App.DB.Video.NewVideoRow();
            row.Date = DateTime.Now;
            row.URL = txtUrl.Text;
            row.Path = txtPath.Text;
            row.state = "";
            row.Title = "";//TODO : Get Video Title
            App.DB.Video.AddVideoRow(row);
            App.SaveChanges();
            ListViewItem item = new ListViewItem(row.ID.ToString());
            item.SubItems.Add(row.URL);
            item.SubItems.Add(row.Title);
            item.SubItems.Add(row.Path);
            item.SubItems.Add(row.state);
            item.SubItems.Add(row.Date.ToLongDateString());
            frmMain.lvUrls.Items.Add(item);
            this.Close();
        }
    }
}
