using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IPOS
{
    partial class Shift_Manager
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayout;
        private Panel leftPanel;
        private Label lblShiftCode;
        private Label lblEmployee;
        private Label lblOpenTime;
        private Label lblStatus;
        private Button btnCloseShift;
        private TabControl tabControl;
        private TabPage tabOrders;
        private FlowLayoutPanel flowOrders;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.lblShiftCode = new System.Windows.Forms.Label();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblOpenTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCloseShift = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabOrders = new System.Windows.Forms.TabPage();
            this.flowOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayout.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabOrders.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.leftPanel, 0, 0);
            this.tableLayout.Controls.Add(this.tabControl, 1, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(1280, 960);
            this.tableLayout.TabIndex = 0;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.leftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPanel.Controls.Add(this.lblShiftCode);
            this.leftPanel.Controls.Add(this.lblEmployee);
            this.leftPanel.Controls.Add(this.lblOpenTime);
            this.leftPanel.Controls.Add(this.lblStatus);
            this.leftPanel.Controls.Add(this.btnCloseShift);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 3);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(30);
            this.leftPanel.Size = new System.Drawing.Size(314, 954);
            this.leftPanel.TabIndex = 0;
            // 
            // lblShiftCode
            // 
            this.lblShiftCode.AutoSize = true;
            this.lblShiftCode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblShiftCode.Location = new System.Drawing.Point(10, 20);
            this.lblShiftCode.Name = "lblShiftCode";
            this.lblShiftCode.Size = new System.Drawing.Size(69, 25);
            this.lblShiftCode.TabIndex = 0;
            this.lblShiftCode.Text = "Mã ca:";
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblEmployee.Location = new System.Drawing.Point(10, 60);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(107, 25);
            this.lblEmployee.TabIndex = 1;
            this.lblEmployee.Text = "Nhân viên:";
            // 
            // lblOpenTime
            // 
            this.lblOpenTime.AutoSize = true;
            this.lblOpenTime.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblOpenTime.Location = new System.Drawing.Point(10, 100);
            this.lblOpenTime.Name = "lblOpenTime";
            this.lblOpenTime.Size = new System.Drawing.Size(106, 25);
            this.lblOpenTime.TabIndex = 2;
            this.lblOpenTime.Text = "Giờ mở ca:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Teal;
            this.lblStatus.Location = new System.Drawing.Point(10, 140);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(107, 25);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // btnCloseShift
            // 
            this.btnCloseShift.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCloseShift.FlatAppearance.BorderSize = 0;
            this.btnCloseShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseShift.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCloseShift.ForeColor = System.Drawing.Color.White;
            this.btnCloseShift.Location = new System.Drawing.Point(10, 190);
            this.btnCloseShift.Name = "btnCloseShift";
            this.btnCloseShift.Size = new System.Drawing.Size(180, 50);
            this.btnCloseShift.TabIndex = 4;
            this.btnCloseShift.Text = "Đóng ca";
            this.btnCloseShift.UseVisualStyleBackColor = false;
            this.btnCloseShift.Click += new System.EventHandler(this.btnCloseShift_Click_1);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabOrders);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.tabControl.Location = new System.Drawing.Point(323, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(954, 954);
            this.tabControl.TabIndex = 1;
            // 
            // tabOrders
            // 
            this.tabOrders.BackColor = System.Drawing.Color.White;
            this.tabOrders.Controls.Add(this.flowOrders);
            this.tabOrders.Location = new System.Drawing.Point(4, 32);
            this.tabOrders.Name = "tabOrders";
            this.tabOrders.Size = new System.Drawing.Size(946, 918);
            this.tabOrders.TabIndex = 0;
            this.tabOrders.Text = "📋 Danh sách đơn hàng";
            // 
            // flowOrders
            // 
            this.flowOrders.AutoScroll = true;
            this.flowOrders.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowOrders.Location = new System.Drawing.Point(0, 0);
            this.flowOrders.Name = "flowOrders";
            this.flowOrders.Padding = new System.Windows.Forms.Padding(20);
            this.flowOrders.Size = new System.Drawing.Size(946, 918);
            this.flowOrders.TabIndex = 0;
            // 
            // Shift_Manager
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 960);
            this.Controls.Add(this.tableLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.Name = "Shift_Manager";
            this.Text = "Quản lý ca";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayout.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabOrders.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
