using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private const int listenPort = 80;

        private UdpClient listener = new UdpClient(listenPort);

        private bool done = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            


            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("192.168.240.170"), listenPort);
            try
            {
                while (!done)
                {
                    lstvMessage.Items.Add("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);
                    lstvMessage.Items.Add("Received broadcast from " + groupEP.ToString() + " : " + Encoding.ASCII.GetString(bytes, 0, bytes.Length) + "");

                    Application.DoEvents();

                    Thread.Sleep(100);
                }
            }
            catch (Exception epx)
            {
                lblMessage.Text += epx.ToString();
            }
            finally
            {
                listener.Close();
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            done = true;

            listener.Close();
        }
    }
}
