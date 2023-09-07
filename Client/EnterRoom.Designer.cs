namespace lab4
{
    partial class EnterRoom
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
            this.btn_connect = new System.Windows.Forms.Button();
            this.ServerList = new System.Windows.Forms.ListBox();
            this.btnUpdateServerList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_connect.Location = new System.Drawing.Point(0, 268);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(268, 23);
            this.btn_connect.TabIndex = 1;
            this.btn_connect.Text = "Подключиться";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // ServerList
            // 
            this.ServerList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ServerList.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ServerList.FormattingEnabled = true;
            this.ServerList.ItemHeight = 25;
            this.ServerList.Location = new System.Drawing.Point(0, 0);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(268, 254);
            this.ServerList.TabIndex = 2;
            this.ServerList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ServerList_DrawItem);
            // 
            // btnUpdateServerList
            // 
            this.btnUpdateServerList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUpdateServerList.Location = new System.Drawing.Point(0, 245);
            this.btnUpdateServerList.Name = "btnUpdateServerList";
            this.btnUpdateServerList.Size = new System.Drawing.Size(268, 23);
            this.btnUpdateServerList.TabIndex = 3;
            this.btnUpdateServerList.Text = "Обновить список комнат";
            this.btnUpdateServerList.UseVisualStyleBackColor = true;
            this.btnUpdateServerList.Click += new System.EventHandler(this.btnUpdateServerList_Click);
            // 
            // EnterRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 291);
            this.Controls.Add(this.btnUpdateServerList);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.btn_connect);
            this.Name = "EnterRoom";
            this.Text = "EnterRoom";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_connect;
        private ListBox ServerList;
        private Button btnUpdateServerList;
    }
}