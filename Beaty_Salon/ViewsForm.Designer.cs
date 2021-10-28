namespace Beaty_Salon
{
    partial class ViewsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewsForm));
            this.dGVView = new System.Windows.Forms.DataGridView();
            this.dGVViewFiles = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGVView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVViewFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVView
            // 
            this.dGVView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dGVView.Location = new System.Drawing.Point(0, 0);
            this.dGVView.Name = "dGVView";
            this.dGVView.Size = new System.Drawing.Size(713, 194);
            this.dGVView.TabIndex = 0;
            this.dGVView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVView_CellClick);
            // 
            // dGVViewFiles
            // 
            this.dGVViewFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVViewFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dGVViewFiles.Location = new System.Drawing.Point(0, 255);
            this.dGVViewFiles.Name = "dGVViewFiles";
            this.dGVViewFiles.Size = new System.Drawing.Size(713, 200);
            this.dGVViewFiles.TabIndex = 1;
            // 
            // ViewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 455);
            this.Controls.Add(this.dGVViewFiles);
            this.Controls.Add(this.dGVView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewsForm";
            this.Text = "Список посещений";
            this.Load += new System.EventHandler(this.ViewsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVViewFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVView;
        private System.Windows.Forms.DataGridView dGVViewFiles;
    }
}