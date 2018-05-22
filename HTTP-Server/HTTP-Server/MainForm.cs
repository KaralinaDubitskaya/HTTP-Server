using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTP_Server
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            HTTPServer server = new HTTPServer(8081, LogMessage);
        }

        private void LogMessage(string msg)
        {
            textBox.Text += msg + "\r\n\r\n";
        }
    }
}
