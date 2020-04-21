using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pen.Common.Browser
{
    public partial class PenBrowserTab : UserControl
    {
        public PenBrowserTab()
        {
            InitializeComponent();
        }

        public void AddTab(string url, int insertIndex = -1, BroserSetting setting = null, string title = "")
        {
            browserTabControl.SuspendLayout();

            var browser = new PenBrowser(AddTab, url, setting)
            {
                Dock = DockStyle.Fill
            };
            if (string.IsNullOrEmpty(title))
            {
                title = url;
            }
            if (title.Length > 15)
            {
                title = title.Substring(0, 15) + "...";
            }

            var tabPage = new TabPage(title + "   ");

            tabPage.Controls.Add(browser);
            if (insertIndex == -1)
            {
                browserTabControl.TabPages.Add(tabPage);
            }
            else
            {
                browserTabControl.TabPages.Insert(insertIndex, tabPage);
            }

            browserTabControl.SelectedTab = tabPage;
            browserTabControl.ResumeLayout(true);

            tabPage.Dock = DockStyle.Fill;
        }

        public void CloseTab(int index = -1)
        {
            if (this.browserTabControl.TabPages.Count == 0)
            {
                return;
            }

            if (index == -1)
            {
                index = browserTabControl.SelectedIndex;
            }

            var tabPage = browserTabControl.TabPages[index];

            //var control = tabPage.Controls[0] as PenBrowser;
            //if (control != null)
            //{
            //    control.Close();
            //}

            this.browserTabControl.TabPages.Remove(tabPage);
            tabPage.Dispose();
            this.browserTabControl.SelectedIndex = index == 0 ? 0 : index - 1;
        }

        public PenBrowser GetCurrentTabControl()
        {
            if (this.browserTabControl.SelectedIndex == -1)
            {
                return null;
            }

            var tabPage = this.browserTabControl.Controls[this.browserTabControl.SelectedIndex];
            var control = tabPage.Controls[0] as PenBrowser;

            return control;
        }


        private void browserTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.YellowGreen, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.browserTabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void browserTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.browserTabControl.TabPages.Count; i++)
            {
                Rectangle r = browserTabControl.GetTabRect(i);
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    this.CloseTab(i);
                    break;
                }
            }
        }

        private void browserTabControl_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.browserTabControl.TabPages.Count; i++)
            {
                Rectangle r = browserTabControl.GetTabRect(i);
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    browserTabControl.Cursor = Cursors.Hand;
                    break;
                }

                browserTabControl.Cursor = Cursors.Default;
            }
        }
    }
}
