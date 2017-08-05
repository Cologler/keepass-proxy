using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeePass.Plugins;

namespace KeepassProxy
{
    public class KeepassProxyExt : Plugin
    {
        private const string KeyProxy = "DF3BB3C8-F3A4-4AFF-A43A-68381DFED0CF";

        private string _proxy;
        private IPluginHost _host;

        public override bool Initialize(IPluginHost host)
        {
            this._host = host;
            this._proxy = host.CustomConfig.GetString(KeyProxy, null);
            this.SetProxy(this._proxy);

            var items = host.MainWindow.ToolsMenu.DropDownItems;
            var menu = new ToolStripMenuItem("Keepass Proxy Options");
            menu.Click += this.MenuOnClick;
            items.Add(menu);

            return true;
        }

        private void SetProxy(string proxy)
        {
            WebRequest.DefaultWebProxy = !string.IsNullOrWhiteSpace(proxy) ? new WebProxy(proxy) : null;
        }

        private void MenuOnClick(object sender, EventArgs eventArgs)
        {
            var form = new ProxySettingsForm(this._proxy);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this._proxy = form.HostResult;
                this.SetProxy(this._proxy);
                this._host.CustomConfig.SetString(KeyProxy, this._proxy);
            }
        }
    }
}
