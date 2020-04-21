namespace Pen.Common.Browser
{
    partial class PenBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PenBrowser));
            this.browserPanel = new System.Windows.Forms.Panel();
            this.pic_loading = new System.Windows.Forms.PictureBox();
            this.browserPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_loading)).BeginInit();
            this.SuspendLayout();
            // 
            // browserPanel
            // 
            this.browserPanel.BackColor = System.Drawing.Color.Transparent;
            this.browserPanel.Controls.Add(this.pic_loading);
            this.browserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserPanel.Location = new System.Drawing.Point(0, 0);
            this.browserPanel.Name = "browserPanel";
            this.browserPanel.Size = new System.Drawing.Size(211, 138);
            this.browserPanel.TabIndex = 0;
            // 
            // pic_loading
            // 
            this.pic_loading.BackColor = System.Drawing.Color.Transparent;
            this.pic_loading.Image = ((System.Drawing.Image)(resources.GetObject("pic_loading.Image")));
            this.pic_loading.Location = new System.Drawing.Point(32, 29);
            this.pic_loading.Name = "pic_loading";
            this.pic_loading.Size = new System.Drawing.Size(37, 38);
            this.pic_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_loading.TabIndex = 0;
            this.pic_loading.TabStop = false;
            // 
            // PenBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.browserPanel);
            this.Name = "PenBrowser";
            this.Size = new System.Drawing.Size(211, 138);
            this.browserPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel browserPanel;
        private System.Windows.Forms.PictureBox pic_loading;
    }
}
