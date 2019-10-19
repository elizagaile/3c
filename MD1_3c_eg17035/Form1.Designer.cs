namespace MD1_3c_eg17035
{
    partial class OffsetForm
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
            this.Canva = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbLineStart = new System.Windows.Forms.Label();
            this.lbLineEnd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbQuad1 = new System.Windows.Forms.Label();
            this.lbQuad2 = new System.Windows.Forms.Label();
            this.lbQuad3 = new System.Windows.Forms.Label();
            this.lbQuad4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Canva)).BeginInit();
            this.SuspendLayout();
            // 
            // Canva
            // 
            this.Canva.BackColor = System.Drawing.Color.White;
            this.Canva.Location = new System.Drawing.Point(13, 13);
            this.Canva.Name = "Canva";
            this.Canva.Size = new System.Drawing.Size(1000, 1000);
            this.Canva.TabIndex = 0;
            this.Canva.TabStop = false;
            this.Canva.Paint += new System.Windows.Forms.PaintEventHandler(this.Canva_Paint);
            this.Canva.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canva_MouseDown);
            this.Canva.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canva_MouseMove);
            this.Canva.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canva_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1019, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nogriežņa galapunktu koordinātas:";
            // 
            // lbLineStart
            // 
            this.lbLineStart.AutoSize = true;
            this.lbLineStart.Location = new System.Drawing.Point(1020, 37);
            this.lbLineStart.Name = "lbLineStart";
            this.lbLineStart.Size = new System.Drawing.Size(23, 20);
            this.lbLineStart.TabIndex = 5;
            this.lbLineStart.Text = "(;)";
            // 
            // lbLineEnd
            // 
            this.lbLineEnd.AutoSize = true;
            this.lbLineEnd.Location = new System.Drawing.Point(1020, 57);
            this.lbLineEnd.Name = "lbLineEnd";
            this.lbLineEnd.Size = new System.Drawing.Size(23, 20);
            this.lbLineEnd.TabIndex = 6;
            this.lbLineEnd.Text = "(;)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1020, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Četrstūra virsotņu koordinātas:";
            // 
            // lbQuad1
            // 
            this.lbQuad1.AutoSize = true;
            this.lbQuad1.Location = new System.Drawing.Point(1020, 141);
            this.lbQuad1.Name = "lbQuad1";
            this.lbQuad1.Size = new System.Drawing.Size(23, 20);
            this.lbQuad1.TabIndex = 8;
            this.lbQuad1.Text = "(;)";
            // 
            // lbQuad2
            // 
            this.lbQuad2.AutoSize = true;
            this.lbQuad2.Location = new System.Drawing.Point(1020, 161);
            this.lbQuad2.Name = "lbQuad2";
            this.lbQuad2.Size = new System.Drawing.Size(23, 20);
            this.lbQuad2.TabIndex = 9;
            this.lbQuad2.Text = "(;)";
            // 
            // lbQuad3
            // 
            this.lbQuad3.AutoSize = true;
            this.lbQuad3.Location = new System.Drawing.Point(1020, 181);
            this.lbQuad3.Name = "lbQuad3";
            this.lbQuad3.Size = new System.Drawing.Size(23, 20);
            this.lbQuad3.TabIndex = 10;
            this.lbQuad3.Text = "(;)";
            // 
            // lbQuad4
            // 
            this.lbQuad4.AutoSize = true;
            this.lbQuad4.Location = new System.Drawing.Point(1020, 201);
            this.lbQuad4.Name = "lbQuad4";
            this.lbQuad4.Size = new System.Drawing.Size(23, 20);
            this.lbQuad4.TabIndex = 11;
            this.lbQuad4.Text = "(;)";
            // 
            // OffsetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 1024);
            this.Controls.Add(this.lbQuad4);
            this.Controls.Add(this.lbQuad3);
            this.Controls.Add(this.lbQuad2);
            this.Controls.Add(this.lbQuad1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbLineEnd);
            this.Controls.Add(this.lbLineStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Canva);
            this.Name = "OffsetForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Canva)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Canva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbLineStart;
        private System.Windows.Forms.Label lbLineEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbQuad1;
        private System.Windows.Forms.Label lbQuad2;
        private System.Windows.Forms.Label lbQuad3;
        private System.Windows.Forms.Label lbQuad4;
    }
}

