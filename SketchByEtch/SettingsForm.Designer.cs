namespace SketchByEtch
{
    partial class SettingsForm
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
            this.cboxSwapKnobs = new System.Windows.Forms.CheckBox();
            this.cboxInvertX = new System.Windows.Forms.CheckBox();
            this.cboxInvertY = new System.Windows.Forms.CheckBox();
            this.txtKnobsMaxValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtScreenWidth = new System.Windows.Forms.TextBox();
            this.txtScreenHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAutoSettings = new System.Windows.Forms.Button();
            this.cboxFullScreen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboxSwapKnobs
            // 
            this.cboxSwapKnobs.AutoSize = true;
            this.cboxSwapKnobs.Font = new System.Drawing.Font("Verdana", 10F);
            this.cboxSwapKnobs.Location = new System.Drawing.Point(12, 12);
            this.cboxSwapKnobs.Name = "cboxSwapKnobs";
            this.cboxSwapKnobs.Size = new System.Drawing.Size(173, 21);
            this.cboxSwapKnobs.TabIndex = 0;
            this.cboxSwapKnobs.Text = "Swap X and Y knobs";
            this.cboxSwapKnobs.UseVisualStyleBackColor = true;
            this.cboxSwapKnobs.CheckedChanged += new System.EventHandler(this.cboxSwapKnobs_CheckedChanged);
            // 
            // cboxInvertX
            // 
            this.cboxInvertX.AutoSize = true;
            this.cboxInvertX.Font = new System.Drawing.Font("Verdana", 10F);
            this.cboxInvertX.Location = new System.Drawing.Point(12, 39);
            this.cboxInvertX.Name = "cboxInvertX";
            this.cboxInvertX.Size = new System.Drawing.Size(117, 21);
            this.cboxInvertX.TabIndex = 1;
            this.cboxInvertX.Text = "Invert X axis";
            this.cboxInvertX.UseVisualStyleBackColor = true;
            this.cboxInvertX.CheckedChanged += new System.EventHandler(this.cboxInvertX_CheckedChanged);
            // 
            // cboxInvertY
            // 
            this.cboxInvertY.AutoSize = true;
            this.cboxInvertY.Font = new System.Drawing.Font("Verdana", 10F);
            this.cboxInvertY.Location = new System.Drawing.Point(12, 66);
            this.cboxInvertY.Name = "cboxInvertY";
            this.cboxInvertY.Size = new System.Drawing.Size(116, 21);
            this.cboxInvertY.TabIndex = 2;
            this.cboxInvertY.Text = "Invert Y axis";
            this.cboxInvertY.UseVisualStyleBackColor = true;
            this.cboxInvertY.CheckedChanged += new System.EventHandler(this.cboxInvertY_CheckedChanged);
            // 
            // txtKnobsMaxValue
            // 
            this.txtKnobsMaxValue.Location = new System.Drawing.Point(252, 13);
            this.txtKnobsMaxValue.Name = "txtKnobsMaxValue";
            this.txtKnobsMaxValue.Size = new System.Drawing.Size(102, 20);
            this.txtKnobsMaxValue.TabIndex = 3;
            this.txtKnobsMaxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F);
            this.label1.Location = new System.Drawing.Point(360, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Knobs max value";
            // 
            // txtScreenWidth
            // 
            this.txtScreenWidth.Location = new System.Drawing.Point(252, 39);
            this.txtScreenWidth.Name = "txtScreenWidth";
            this.txtScreenWidth.Size = new System.Drawing.Size(102, 20);
            this.txtScreenWidth.TabIndex = 5;
            this.txtScreenWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_KeyPress);
            // 
            // txtScreenHeight
            // 
            this.txtScreenHeight.Location = new System.Drawing.Point(252, 67);
            this.txtScreenHeight.Name = "txtScreenHeight";
            this.txtScreenHeight.Size = new System.Drawing.Size(102, 20);
            this.txtScreenHeight.TabIndex = 6;
            this.txtScreenHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F);
            this.label2.Location = new System.Drawing.Point(360, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Screen width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10F);
            this.label3.Location = new System.Drawing.Point(360, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Screen height";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnSave.Location = new System.Drawing.Point(12, 130);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 37);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnCancel.Location = new System.Drawing.Point(110, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 37);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAutoSettings
            // 
            this.btnAutoSettings.Font = new System.Drawing.Font("Verdana", 10F);
            this.btnAutoSettings.Location = new System.Drawing.Point(252, 130);
            this.btnAutoSettings.Name = "btnAutoSettings";
            this.btnAutoSettings.Size = new System.Drawing.Size(185, 37);
            this.btnAutoSettings.TabIndex = 11;
            this.btnAutoSettings.Text = "Auto width and height";
            this.btnAutoSettings.UseVisualStyleBackColor = true;
            this.btnAutoSettings.Click += new System.EventHandler(this.btnAutoSettings_Click);
            // 
            // cboxFullScreen
            // 
            this.cboxFullScreen.AutoSize = true;
            this.cboxFullScreen.Font = new System.Drawing.Font("Verdana", 10F);
            this.cboxFullScreen.Location = new System.Drawing.Point(12, 93);
            this.cboxFullScreen.Name = "cboxFullScreen";
            this.cboxFullScreen.Size = new System.Drawing.Size(222, 21);
            this.cboxFullScreen.TabIndex = 12;
            this.cboxFullScreen.Text = "Use the absolute full screen";
            this.cboxFullScreen.UseVisualStyleBackColor = true;
            this.cboxFullScreen.CheckedChanged += new System.EventHandler(this.cboxFullScreen_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 205);
            this.Controls.Add(this.cboxFullScreen);
            this.Controls.Add(this.btnAutoSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtScreenHeight);
            this.Controls.Add(this.txtScreenWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKnobsMaxValue);
            this.Controls.Add(this.cboxInvertY);
            this.Controls.Add(this.cboxInvertX);
            this.Controls.Add(this.cboxSwapKnobs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cboxSwapKnobs;
        private System.Windows.Forms.CheckBox cboxInvertX;
        private System.Windows.Forms.CheckBox cboxInvertY;
        private System.Windows.Forms.TextBox txtKnobsMaxValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScreenWidth;
        private System.Windows.Forms.TextBox txtScreenHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAutoSettings;
        private System.Windows.Forms.CheckBox cboxFullScreen;
    }
}