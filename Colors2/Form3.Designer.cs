﻿namespace Colors2
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.speed = new System.Windows.Forms.ComboBox();
            this.movement = new System.Windows.Forms.ComboBox();
            this.kindOfPicture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.select = new System.Windows.Forms.Button();
            this.quit = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.selectedListView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // speed
            // 
            this.speed.FormattingEnabled = true;
            this.speed.Location = new System.Drawing.Point(43, 79);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(95, 20);
            this.speed.TabIndex = 0;
            this.speed.SelectedIndexChanged += new System.EventHandler(this.speed_SelectedIndexChanged);
            // 
            // movement
            // 
            this.movement.FormattingEnabled = true;
            this.movement.Location = new System.Drawing.Point(144, 79);
            this.movement.Name = "movement";
            this.movement.Size = new System.Drawing.Size(95, 20);
            this.movement.TabIndex = 0;
            this.movement.SelectedIndexChanged += new System.EventHandler(this.movement_SelectedIndexChanged);
            // 
            // kindOfPicture
            // 
            this.kindOfPicture.FormattingEnabled = true;
            this.kindOfPicture.Location = new System.Drawing.Point(245, 79);
            this.kindOfPicture.Name = "kindOfPicture";
            this.kindOfPicture.Size = new System.Drawing.Size(95, 20);
            this.kindOfPicture.TabIndex = 0;
            this.kindOfPicture.SelectedIndexChanged += new System.EventHandler(this.kindOfPicture_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(70, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "速度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(152, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "動きの種類";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(250, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "画像の種類";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // select
            // 
            this.select.Location = new System.Drawing.Point(346, 79);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(93, 19);
            this.select.TabIndex = 3;
            this.select.Text = "使う画像を選択";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // quit
            // 
            this.quit.Location = new System.Drawing.Point(22, 258);
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(75, 23);
            this.quit.TabIndex = 5;
            this.quit.Text = "もどる\r\n";
            this.quit.UseVisualStyleBackColor = true;
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(103, 258);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(75, 23);
            this.apply.TabIndex = 6;
            this.apply.Text = "けってい\r\n";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // selectedListView
            // 
            this.selectedListView.Location = new System.Drawing.Point(445, 78);
            this.selectedListView.Name = "selectedListView";
            this.selectedListView.Size = new System.Drawing.Size(263, 198);
            this.selectedListView.TabIndex = 7;
            this.selectedListView.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 283);
            this.Controls.Add(this.selectedListView);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.quit);
            this.Controls.Add(this.select);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kindOfPicture);
            this.Controls.Add(this.movement);
            this.Controls.Add(this.speed);
            this.Name = "Form3";
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox speed;
        private System.Windows.Forms.ComboBox movement;
        private System.Windows.Forms.ComboBox kindOfPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button quit;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.ListView selectedListView;
        private System.Windows.Forms.ImageList imageList1;
    }
}