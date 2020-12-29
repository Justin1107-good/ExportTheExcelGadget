
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class InputDialogForm : Form
    {
        // AutoSize As = new AutoSize();

        public delegate void TextEventHandler(string strText);
        public List<DouCustom> dous;
        public string strString = "";
        public string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 

        public InputDialogForm()
        {
            InitializeComponent();

        }
        public InputDialogForm(List<DouCustom> _dous, string _strString)
        {
            InitializeComponent();
            this.dous = _dous;
            this.strString = _strString;
        }
        public TextEventHandler TextHandler;


        private void btnOK_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show(grid_Prame.Rows.Count.ToString());
            if (null != TextHandler)
            {
                string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
                for (int i = 0; i < grid_Prame.Rows.Count; i++)
                {
                    String[] arr = new String[] { };

                    if (this.grid_Prame.Rows[i].Cells["Code"].Value != null && this.grid_Prame.Rows[i].Cells["ConString"].Value != null)
                    {
                        string sd = this.grid_Prame.Rows[i].Cells[0].Value + "&" + this.grid_Prame.Rows[i].Cells[1].Value + ",";
                        arr = sd.Trim().Split(',');

                        for (int v = 0; v < arr.Length; v++)
                        {
                            strBarcodeList += arr[v].Replace(",\n", "") + "\r\n";//将分隔开的字符串进行重新组装中间加\r\n回车
                        }
                        if (strBarcodeList.Length > 0)
                            strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);
                        // TextHandler.Invoke(this.grid_Prame.Rows[i].Cells[0].Value + "&&" + this.grid_Prame.Rows[i].Cells[1].Value);
                    }
                }

                TextHandler.Invoke(strBarcodeList);
                DialogResult = DialogResult.OK;
            }
            else
            {
                DouCustom dc = null;
                //    string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
                for (int i = 0; i < grid_Prame.Rows.Count; i++)
                {
                    String[] arr = new String[] { };

                    if (this.grid_Prame.Rows[i].Cells["Code"].Value != null && this.grid_Prame.Rows[i].Cells["ConString"].Value != null)
                    {
                        dc = new DouCustom { Code = this.grid_Prame.Rows[i].Cells[0].Value.ToString(), ConString = this.grid_Prame.Rows[i].Cells[1].Value.ToString() };
                        dous.Add(dc);
                        string sd = this.grid_Prame.Rows[i].Cells[0].Value + "&" + this.grid_Prame.Rows[i].Cells[1].Value + ",";
                        arr = sd.Trim().Split(',');

                        for (int v = 0; v < arr.Length; v++)
                        {
                            strBarcodeList += arr[v].Replace(",\n", "") + "\r\n";//将分隔开的字符串进行重新组装中间加\r\n回车
                        }
                        if (strBarcodeList.Length > 0)
                            strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);
                        // TextHandler.Invoke(this.grid_Prame.Rows[i].Cells[0].Value + "&&" + this.grid_Prame.Rows[i].Cells[1].Value);
                    }
                }

                UseTextBoxValue.BoxText = strBarcodeList;
                UseTextBoxValue.text_Name = strString;
                this.Close();

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void InputDialogForm_Load(object sender, EventArgs e)
        {

            label4.Text = "一、参数代号不可以填写重复值";
            label2.Text = "二、无代号，缺省用”0“表示；\n不带附件用“ - ”表示；无附件用“00”表示";
            //  As.controllInitializeSize(this);   
            if (dous == null)
            {
                dous = new List<DouCustom>();
                BindingSource bs = new BindingSource();
                bs.DataSource = dous;
                grid_Prame.DataSource = bs;
            }
            else
            {

                BindingSource bs = new BindingSource();
                bs.DataSource = dous;
                grid_Prame.DataSource = bs;
            }
        }

        private void InputDialogForm_SizeChanged(object sender, EventArgs e)
        {
            //调用类的自适应方法，完成自适应
            //   As.controlAutoSize(this);
        }
    }
}
