namespace ScheduleUI
{
    partial class Main
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.tableScheduleLists = new System.Windows.Forms.TableLayoutPanel();
            this.tableSchedule = new System.Windows.Forms.TableLayoutPanel();
            this.flowScheduleButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.panelContainer.SuspendLayout();
            this.tableSchedule.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.tableSchedule);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(674, 289);
            this.panelContainer.TabIndex = 0;
            // 
            // tableScheduleLists
            // 
            this.tableScheduleLists.ColumnCount = 5;
            this.tableScheduleLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableScheduleLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableScheduleLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableScheduleLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableScheduleLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableScheduleLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableScheduleLists.Location = new System.Drawing.Point(3, 3);
            this.tableScheduleLists.Name = "tableScheduleLists";
            this.tableScheduleLists.RowCount = 1;
            this.tableScheduleLists.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableScheduleLists.Size = new System.Drawing.Size(668, 243);
            this.tableScheduleLists.TabIndex = 0;
            // 
            // tableSchedule
            // 
            this.tableSchedule.ColumnCount = 1;
            this.tableSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSchedule.Controls.Add(this.tableScheduleLists, 0, 0);
            this.tableSchedule.Controls.Add(this.flowScheduleButtons, 0, 1);
            this.tableSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSchedule.Location = new System.Drawing.Point(0, 0);
            this.tableSchedule.Name = "tableSchedule";
            this.tableSchedule.RowCount = 2;
            this.tableSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableSchedule.Size = new System.Drawing.Size(674, 289);
            this.tableSchedule.TabIndex = 1;
            // 
            // flowScheduleButtons
            // 
            this.flowScheduleButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowScheduleButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowScheduleButtons.Location = new System.Drawing.Point(3, 252);
            this.flowScheduleButtons.Name = "flowScheduleButtons";
            this.flowScheduleButtons.Size = new System.Drawing.Size(668, 34);
            this.flowScheduleButtons.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 289);
            this.Controls.Add(this.panelContainer);
            this.Name = "Main";
            this.Text = "Main";
            this.panelContainer.ResumeLayout(false);
            this.tableSchedule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TableLayoutPanel tableSchedule;
        private System.Windows.Forms.TableLayoutPanel tableScheduleLists;
        private System.Windows.Forms.FlowLayoutPanel flowScheduleButtons;
    }
}

