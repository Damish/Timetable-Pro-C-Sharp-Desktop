namespace TimetablePro
{
    partial class UserControl2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartstudent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnselectfaculty = new System.Windows.Forms.Button();
            this.combogetf = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartstudent)).BeginInit();
            this.SuspendLayout();
            // 
            // chartstudent
            // 
            chartArea1.Name = "ChartArea1";
            this.chartstudent.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartstudent.Legends.Add(legend1);
            this.chartstudent.Location = new System.Drawing.Point(155, 98);
            this.chartstudent.Name = "chartstudent";
            this.chartstudent.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartstudent.Series.Add(series1);
            this.chartstudent.Size = new System.Drawing.Size(563, 381);
            this.chartstudent.TabIndex = 0;
            this.chartstudent.Text = "chart1";
            this.chartstudent.Click += new System.EventHandler(this.chart1_Click);
            // 
            // btnselectfaculty
            // 
            this.btnselectfaculty.Location = new System.Drawing.Point(167, 60);
            this.btnselectfaculty.Name = "btnselectfaculty";
            this.btnselectfaculty.Size = new System.Drawing.Size(132, 32);
            this.btnselectfaculty.TabIndex = 1;
            this.btnselectfaculty.Text = "genarate statistics";
            this.btnselectfaculty.UseVisualStyleBackColor = true;
            this.btnselectfaculty.Click += new System.EventHandler(this.btnselectfaculty_Click);
            // 
            // combogetf
            // 
            this.combogetf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combogetf.FormattingEnabled = true;
            this.combogetf.Items.AddRange(new object[] {
            "Computing",
            "Engineering",
            "Management"});
            this.combogetf.Location = new System.Drawing.Point(353, 64);
            this.combogetf.Name = "combogetf";
            this.combogetf.Size = new System.Drawing.Size(178, 28);
            this.combogetf.TabIndex = 2;
            this.combogetf.Text = "select faculty";
            this.combogetf.SelectedIndexChanged += new System.EventHandler(this.combogetf_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(167, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "No Of Student In deferent Faculties";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combogetf);
            this.Controls.Add(this.btnselectfaculty);
            this.Controls.Add(this.chartstudent);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(839, 631);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartstudent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartstudent;
        private System.Windows.Forms.Button btnselectfaculty;
        private System.Windows.Forms.ComboBox combogetf;
        private System.Windows.Forms.Label label1;
    }
}
