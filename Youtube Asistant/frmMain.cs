using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Asistant
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using(frmAddUrl frm=new frmAddUrl(this))
            {
                frm.ShowDialog();
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        Downloader downloader;
        private void frmMain_Load(object sender, EventArgs e)
        {
            downloader = new Downloader();
            downloader.OnFinished += Downloader_OnFinished;
            string filename = string.Format("{0}/data.dat", Application.StartupPath);
            if (File.Exists(filename))
                App.DB.ReadXml(filename);
            foreach(Database.VideoRow row in App.DB.Video)
            {
                ListViewItem item = new ListViewItem(row.ID.ToString());
                item.SubItems.Add(row.URL);
                item.SubItems.Add(row.Title);
                item.SubItems.Add(row.Path);
                item.SubItems.Add(row.state);
                item.SubItems.Add(row.Date.ToLongDateString());
                lvUrls.Items.Add(item);
            }

        }

        private void Downloader_OnFinished(Database.VideoRow row)
        {
            MessageBox.Show("ddd");
        }

        private void tsStartDownlaod_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => { downloader.Download(App.DB.Video.First(i => i.state != "Finished")); }) { IsBackground = true };
            thread.Start();
            
        }

        private void tsStopDownload_Click(object sender, EventArgs e)
        {

        }

        private void tsRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = lvUrls.SelectedItems.Count; i > 0; i--)
                {
                    ListViewItem item = lvUrls.SelectedItems[i - 1];
                    App.DB.Video.Rows[item.Index].Delete();
                    lvUrls.Items[item.Index].Remove();
                }
                App.SaveChanges();

            }
        }
    }
}
