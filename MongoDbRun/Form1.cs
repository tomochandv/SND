using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongoDbRun
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("MongoDB");
            sc.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*mongodb 계정 추가
             *use admin
                db.createUser( { user: "<username>",
                          pwd: "<password>",
                          roles: [ "userAdminAnyDatabase",
                                   "dbAdminAnyDatabase",
                                   "readWriteAnyDatabase"
 
                ] } )
             * 
             * */
            //mongod --logpath D:\DB\log\log --auth --dbpath D:DB --install    윈도우 서비스 등록
            ServiceController sc = new ServiceController("MongoDB");
            sc.Stop();
        }

        private string ProcessCheck(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                return sc.Status.ToString();
            }
            catch (Exception ex)
            {
                return "Not Install.";
            }
        }
        System.Threading.Timer tt;
        private void Form1_Load(object sender, EventArgs e)
        {
            TimerCallback tc = new TimerCallback(TimerEventMethod);
            tt = new System.Threading.Timer(tc, null, 0, 1000);
            
        }
        public void TimerEventMethod(Object stateInfo)
        {
            label1.Text = ProcessCheck("MongoDB");
        }

       
    }
}
