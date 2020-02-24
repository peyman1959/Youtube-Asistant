using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace Youtube_Asistant
{
    public delegate void callback(Database.VideoRow row); 
    class Downloader
    {
        public event callback OnFinished;
        public void Download(Database.VideoRow row)
        {
            using (var service = Client.For(YouTube.Default))
            {
                var video = service.GetVideo(row.URL);//TODO : Quality
                string path = Path.Combine(row.Path, video.FullName);
                row.Title = video.Title;
                File.WriteAllBytes(path, video.GetBytes());
                row.state = "Finished";
                App.SaveChanges();
                OnFinished(row);
                

            }
        }
    }
}
