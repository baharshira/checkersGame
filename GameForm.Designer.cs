namespace GameUI
{
    partial class GameForm
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.player1Name = new System.Windows.Forms.Label();
            this.player2Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // player1Name
            // 
            this.player1Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.player1Name.AutoSize = true;
            this.player1Name.Font = new System.Drawing.Font("Candara", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.player1Name.Location = new System.Drawing.Point(56, 5);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(0, 35);
            this.player1Name.TabIndex = 0;
            this.player1Name.Text = String.Format("{0}: {1}", m_Player1.Name, m_Player1.Score);
            // 
            // player2Name
            // 
            this.player2Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.player2Name.AutoSize = true;
            this.player2Name.Location = new System.Drawing.Point(189, 5);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(65, 29);
            this.player2Name.TabIndex = 1;
            this.player2Name.Text = String.Format("{0}: {1}", m_Player2.Name, m_Player2.Score);
            this.player2Name.Font = new System.Drawing.Font("Candara", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(368, 299);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player1Name);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 30, 30);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        #endregion
        private HelpProvider helpProvider1;
        private Label player1Name;
        private Label player2Name;
    }
}