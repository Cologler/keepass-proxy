using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeepassProxy
{
    public partial class ProxySettingsForm : Form
    {
        public ProxySettingsForm()
        {
            this.InitializeComponent();
        }

        public ProxySettingsForm(string host)
            : this()
        {
            this.HostText.Text = host ?? string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var host = this.HostText.Text.Trim();
            if (string.IsNullOrWhiteSpace(host))
            {
                this.HostResult = string.Empty;
            }
            else
            {
                var match = Regex.Match(host, "^(.+)\\:(\\d{1,5})$");
                if (!match.Success)
                {
                    MessageBox.Show("Invaild Proxy", "Error");
                    return;
                }
                var port = int.Parse(match.Groups[2].Value);
                if (port < 1 || port >= 65535)
                {
                    MessageBox.Show("Invaild Port", "Error");
                    return;
                }

                this.HostResult = host;
            }

            this.DialogResult = DialogResult.OK;
        }

        public string HostResult { get; private set; }
    }
}
