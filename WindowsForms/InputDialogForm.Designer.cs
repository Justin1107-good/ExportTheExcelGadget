
namespace WindowsForms
{
    partial class InputDialogForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grid_Prame = new System.Windows.Forms.DataGridView();
            this.groupBox_Search = new System.Windows.Forms.GroupBox();
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.LCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CConString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Prame)).BeginInit();
            this.groupBox_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(860, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 40);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(1027, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 42);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(85, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 19);
            this.label2.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 183);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "提示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 19);
            this.label4.TabIndex = 10;
            // 
            // grid_Prame
            // 
            this.grid_Prame.AllowUserToDeleteRows = false;
            this.grid_Prame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_Prame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Prame.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.ConString});
            this.grid_Prame.Location = new System.Drawing.Point(6, 43);
            this.grid_Prame.Name = "grid_Prame";
            this.grid_Prame.RowHeadersWidth = 51;
            this.grid_Prame.RowTemplate.Height = 27;
            this.grid_Prame.Size = new System.Drawing.Size(616, 385);
            this.grid_Prame.TabIndex = 10;
            this.grid_Prame.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grid_Prame_KeyPress);
            this.grid_Prame.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grid_Prame_KeyUp);
            // 
            // groupBox_Search
            // 
            this.groupBox_Search.Controls.Add(this.dgv_List);
            this.groupBox_Search.Location = new System.Drawing.Point(654, 23);
            this.groupBox_Search.Name = "groupBox_Search";
            this.groupBox_Search.Size = new System.Drawing.Size(481, 622);
            this.groupBox_Search.TabIndex = 11;
            this.groupBox_Search.TabStop = false;
            this.groupBox_Search.Text = "groupBox2";
            // 
            // dgv_List
            // 
            this.dgv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_List.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LCode,
            this.CConString});
            this.dgv_List.Location = new System.Drawing.Point(7, 35);
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.RowHeadersWidth = 51;
            this.dgv_List.RowTemplate.Height = 27;
            this.dgv_List.Size = new System.Drawing.Size(468, 581);
            this.dgv_List.TabIndex = 0;
            this.dgv_List.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgv_List_KeyUp);
            // 
            // LCode
            // 
            this.LCode.DataPropertyName = "Code";
            this.LCode.HeaderText = "值";
            this.LCode.MinimumWidth = 6;
            this.LCode.Name = "LCode";
            this.LCode.Width = 125;
            // 
            // CConString
            // 
            this.CConString.DataPropertyName = "ConString";
            this.CConString.HeaderText = "含义";
            this.CConString.MinimumWidth = 6;
            this.CConString.Name = "CConString";
            this.CConString.Width = 125;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 657);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1147, 52);
            this.panel1.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grid_Prame);
            this.groupBox2.Location = new System.Drawing.Point(13, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(622, 434);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输入";
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "值";
            this.Code.MinimumWidth = 6;
            this.Code.Name = "Code";
            this.Code.Width = 125;
            // 
            // ConString
            // 
            this.ConString.DataPropertyName = "ConString";
            this.ConString.HeaderText = "含义";
            this.ConString.MinimumWidth = 6;
            this.ConString.Name = "ConString";
            this.ConString.Width = 125;
            // 
            // InputDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 709);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox_Search);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "InputDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数输入";
            this.Load += new System.EventHandler(this.InputDialogForm_Load);
            this.SizeChanged += new System.EventHandler(this.InputDialogForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Prame)).EndInit();
            this.groupBox_Search.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grid_Prame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox_Search;
        private System.Windows.Forms.DataGridView dgv_List;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConString;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConString;
    }
}