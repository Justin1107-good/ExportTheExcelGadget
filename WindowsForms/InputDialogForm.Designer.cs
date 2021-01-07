
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
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grid_Prame = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_Search = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_ComboxSaveName = new System.Windows.Forms.TextBox();
            this.comboBox_list = new System.Windows.Forms.ComboBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_savedatagridview = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.LCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CConString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Delete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Prame)).BeginInit();
            this.groupBox_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Delete.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(18, 6);
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
            this.label2.Location = new System.Drawing.Point(85, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 19);
            this.label2.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 250);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "提示";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 19);
            this.label1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 19);
            this.label4.TabIndex = 10;
            // 
            // grid_Prame
            // 
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
            this.grid_Prame.Size = new System.Drawing.Size(616, 428);
            this.grid_Prame.TabIndex = 10;
            this.grid_Prame.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_Prame_CellMouseDown);
            this.grid_Prame.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grid_Prame_KeyPress);
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
            // groupBox_Search
            // 
            this.groupBox_Search.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox_Search.Controls.Add(this.button2);
            this.groupBox_Search.Controls.Add(this.btn_search);
            this.groupBox_Search.Controls.Add(this.txt_ComboxSaveName);
            this.groupBox_Search.Controls.Add(this.comboBox_list);
            this.groupBox_Search.Controls.Add(this.btn_update);
            this.groupBox_Search.Controls.Add(this.btn_savedatagridview);
            this.groupBox_Search.Controls.Add(this.button1);
            this.groupBox_Search.Controls.Add(this.dgv_List);
            this.groupBox_Search.Location = new System.Drawing.Point(654, 23);
            this.groupBox_Search.Name = "groupBox_Search";
            this.groupBox_Search.Size = new System.Drawing.Size(481, 716);
            this.groupBox_Search.TabIndex = 11;
            this.groupBox_Search.TabStop = false;
            this.groupBox_Search.Text = "提取";
            this.groupBox_Search.TextChanged += new System.EventHandler(this.groupBox_Search_TextChanged);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(393, 28);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 28);
            this.btn_search.TabIndex = 17;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_ComboxSaveName
            // 
            this.txt_ComboxSaveName.Location = new System.Drawing.Point(234, 28);
            this.txt_ComboxSaveName.Name = "txt_ComboxSaveName";
            this.txt_ComboxSaveName.Size = new System.Drawing.Size(144, 25);
            this.txt_ComboxSaveName.TabIndex = 16;
            // 
            // comboBox_list
            // 
            this.comboBox_list.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_list.FormattingEnabled = true;
            this.comboBox_list.Items.AddRange(new object[] {
            "--请选择--"});
            this.comboBox_list.Location = new System.Drawing.Point(7, 25);
            this.comboBox_list.Name = "comboBox_list";
            this.comboBox_list.Size = new System.Drawing.Size(127, 28);
            this.comboBox_list.TabIndex = 15;
            this.comboBox_list.SelectedIndexChanged += new System.EventHandler(this.comboBox_list_SelectedIndexChanged);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(7, 670);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(147, 40);
            this.btn_update.TabIndex = 15;
            this.btn_update.Text = "修改";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_savedatagridview
            // 
            this.btn_savedatagridview.Location = new System.Drawing.Point(328, 670);
            this.btn_savedatagridview.Name = "btn_savedatagridview";
            this.btn_savedatagridview.Size = new System.Drawing.Size(147, 40);
            this.btn_savedatagridview.TabIndex = 15;
            this.btn_savedatagridview.Text = "新建";
            this.btn_savedatagridview.UseVisualStyleBackColor = true;
            this.btn_savedatagridview.Click += new System.EventHandler(this.btn_savedatagridview_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(5, 5);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv_List
            // 
            this.dgv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_List.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LCode,
            this.CConString});
            this.dgv_List.Location = new System.Drawing.Point(6, 63);
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.RowHeadersWidth = 51;
            this.dgv_List.RowTemplate.Height = 27;
            this.dgv_List.Size = new System.Drawing.Size(469, 601);
            this.dgv_List.TabIndex = 0;
            this.dgv_List.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgv_List_KeyUp);
            // 
            // LCode
            // 
            this.LCode.DataPropertyName = "LCode";
            this.LCode.HeaderText = "值";
            this.LCode.MinimumWidth = 6;
            this.LCode.Name = "LCode";
            this.LCode.Width = 125;
            // 
            // CConString
            // 
            this.CConString.DataPropertyName = "CConString";
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
            this.panel1.Location = new System.Drawing.Point(0, 745);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1153, 52);
            this.panel1.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Controls.Add(this.grid_Prame);
            this.groupBox2.Location = new System.Drawing.Point(12, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(622, 477);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输入";
            // 
            // Delete
            // 
            this.Delete.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Delete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(127, 28);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(142, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 18;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 19);
            this.label3.TabIndex = 12;
            // 
            // InputDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 797);
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
            this.groupBox_Search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.Delete.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConString;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_savedatagridview;
        private System.Windows.Forms.DataGridViewTextBoxColumn LCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConString;
        private System.Windows.Forms.ComboBox comboBox_list;
        private System.Windows.Forms.TextBox txt_ComboxSaveName;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.ContextMenuStrip Delete;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
    }
}