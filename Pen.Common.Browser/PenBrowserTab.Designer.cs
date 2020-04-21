namespace Pen.Common.Browser
{
    partial class PenBrowserTab
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.browserTabControl = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // browserTabControl
            // 
            this.browserTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.browserTabControl.Location = new System.Drawing.Point(0, 0);
            this.browserTabControl.Name = "browserTabControl";
            this.browserTabControl.SelectedIndex = 0;
            this.browserTabControl.Size = new System.Drawing.Size(150, 150);
            this.browserTabControl.TabIndex = 4;
            this.browserTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.browserTabControl_DrawItem);
            this.browserTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.browserTabControl_MouseDown);
            this.browserTabControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.browserTabControl_MouseMove);
            // 
            // PenBrowserTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.browserTabControl);
            this.Name = "PenBrowserTab";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl browserTabControl;
    }
}
