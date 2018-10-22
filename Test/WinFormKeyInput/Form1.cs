using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WinFormKeyInput
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        CancellationTokenSource cts;
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            TaskFactory factory = new TaskFactory();
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "setting.xml");
            XmlNodeList nodes = doc.SelectNodes("/root/set");

            factory.StartNew(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    List<Task> tasks = new List<Task>();
                    foreach (XmlNode node in nodes)
                    {

                        var task= factory.StartNew(() =>
                        {
                            Random random = new Random();
                            int min = int.Parse(node.Attributes["MinSecond"].Value);
                            int max = int.Parse(node.Attributes["MaxSecond"].Value);
                            int rdm = random.Next(min, max);

                            Thread.Sleep(rdm);
                            MyKeyDown(node.Attributes["KeyValue"].Value);
                        });
                        tasks.Add(task);
                    }

                    Task.WaitAll(tasks.ToArray());
                }
            }, cts.Token);

        }

        void MyKeyDown(string KeyValue)
        {
            Io_Api ia = new Io_Api();
            ia.keybd(ia.getKeys(KeyValue));
            ia.mouse(MouseEventFlag.Move, 5, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cts != null)
                cts.Cancel();
        }

        //[DllImport("user32.dll", EntryPoint = "FindWindow")]
        //public static extern int FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);
        //[DllImport("user32.dll")]
        //static extern IntPtr SetActiveWindow(IntPtr hWnd);
    }
}
