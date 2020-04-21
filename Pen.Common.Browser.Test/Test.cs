using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pen.Common.Browser.Test
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();

            ProxyPool.Init(Path.Combine(Environment.CurrentDirectory, "proxys.txt"));
        }

        private void x1XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.penBrowserTab.AddTab("https://www.ip.cn", title: "Nothing");
        }

        private void x2XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.penBrowserTab.AddTab("https://www.ip.cn",
                setting: new BroserSetting
                {
                    CookiesSetting = new PenCookie
                    {
                        Cookies = new List<PenCookieItem>
                        {
                            new PenCookieItem { Domain="baidu.com" ,Name="KEY",Value="Hello Pen："+DateTime.Now},
                        }
                    },
                    Proxy = ProxyPool.GetRandomProxy(),
                    InjectJS = "var v = document.createElement('script');v.innerText=\"function test(){pen.send('hello pen')}\";document.body.appendChild(v);"
                },
                title: "Proxy");
        }

        private void x3XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = this.penBrowserTab.GetCurrentTabControl();

            //执行注入的js
            control.ExecuteJs("test();");
        }

        private void 调试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = this.penBrowserTab.GetCurrentTabControl();
            control.Browser.ShowDevTools();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = this.penBrowserTab.GetCurrentTabControl();
            control.Reload(true);
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Logs");
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = this.penBrowserTab.GetCurrentTabControl();
            control.SettingZoomLevel(-1);
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = this.penBrowserTab.GetCurrentTabControl();
            control.SettingZoomLevel(1);
        }
    }
}
