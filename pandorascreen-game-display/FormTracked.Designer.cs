namespace pandorascreen_game_display
{
    partial class FormTracked
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
            this.lstTracked = new System.Windows.Forms.ListBox();
            this.btnRemoveTracked = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblSelectedTracked = new System.Windows.Forms.Label();
            this.btnTopPriority = new System.Windows.Forms.Button();
            this.lblPriority = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstTracked
            // 
            this.lstTracked.FormattingEnabled = true;
            this.lstTracked.Location = new System.Drawing.Point(12, 25);
            this.lstTracked.Name = "lstTracked";
            this.lstTracked.Size = new System.Drawing.Size(313, 225);
            this.lstTracked.TabIndex = 0;
            this.lstTracked.SelectedIndexChanged += new System.EventHandler(this.lstTracked_SelectedIndexChanged);
            // 
            // btnRemoveTracked
            // 
            this.btnRemoveTracked.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveTracked.Enabled = false;
            this.btnRemoveTracked.Location = new System.Drawing.Point(331, 211);
            this.btnRemoveTracked.Name = "btnRemoveTracked";
            this.btnRemoveTracked.Size = new System.Drawing.Size(162, 39);
            this.btnRemoveTracked.TabIndex = 1;
            this.btnRemoveTracked.Text = "Remove";
            this.btnRemoveTracked.UseVisualStyleBackColor = false;
            this.btnRemoveTracked.Click += new System.EventHandler(this.btnRemoveTracked_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelectedTracked);
            this.groupBox1.Controls.Add(this.lblSelected);
            this.groupBox1.Location = new System.Drawing.Point(331, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 43);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Process";
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(6, 18);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(0, 13);
            this.lblSelected.TabIndex = 4;
            // 
            // lblSelectedTracked
            // 
            this.lblSelectedTracked.AutoSize = true;
            this.lblSelectedTracked.Location = new System.Drawing.Point(12, 18);
            this.lblSelectedTracked.Name = "lblSelectedTracked";
            this.lblSelectedTracked.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedTracked.TabIndex = 5;
            // 
            // btnTopPriority
            // 
            this.btnTopPriority.Enabled = false;
            this.btnTopPriority.Location = new System.Drawing.Point(331, 61);
            this.btnTopPriority.Name = "btnTopPriority";
            this.btnTopPriority.Size = new System.Drawing.Size(162, 38);
            this.btnTopPriority.TabIndex = 5;
            this.btnTopPriority.Text = "To Top";
            this.btnTopPriority.UseVisualStyleBackColor = true;
            this.btnTopPriority.Click += new System.EventHandler(this.btnTopPriority_Click);
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(12, 9);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(172, 13);
            this.lblPriority.TabIndex = 6;
            this.lblPriority.Text = "Higher on the list, higher the priority";
            // 
            // FormTracked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 265);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.btnTopPriority);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRemoveTracked);
            this.Controls.Add(this.lstTracked);
            this.Name = "FormTracked";
            this.Text = "Currently Tracked Items";
            this.Load += new System.EventHandler(this.FormTracked_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstTracked;
        private System.Windows.Forms.Button btnRemoveTracked;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSelectedTracked;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Button btnTopPriority;
        private System.Windows.Forms.Label lblPriority;
    }
}