using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Asistant
{
    static class App
    {
        static Database db;
        public static Database DB
        {
            get
            {
                if (db==null)
                {
                    db = new Database();
                }
                return db;
            }
        }
        public static void SaveChanges()
        {
            DB.AcceptChanges();
            DB.WriteXml(string.Format("{0}/data.dat", Application.StartupPath));
        }
    }
}
