
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
        public string strText = "";
        public string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
        System.Timers.Timer t = new System.Timers.Timer(1 * 0.5 * 0.5 * 1000);

        List<DouCustom> douCustoms = new List<DouCustom>() {
          new DouCustom{  Code="11", ConString="11A"},
          new DouCustom{  Code="12", ConString="12A"},
          new DouCustom{  Code="13", ConString="13A"},
            new DouCustom{  Code="14", ConString="14A"},
        };
        public InputDialogForm()
        {
            InitializeComponent();


        }
        public InputDialogForm(List<DouCustom> _dous, string _strString)
        {     //事件调用线程错误捕获
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.dous = _dous;
            this.strString = _strString;
        }
        public InputDialogForm(List<DouCustom> _dous, string _strString, string _strText)
        {     //事件调用线程错误捕获
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.dous = _dous;
            this.strString = _strString;
            this.strText = _strText;
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
            // As.controllInitializeSize(this);
            if (dous == null)
            {
                for (int i = 0; i < 9; i++)
                {
                    grid_Prame.Rows.Add();
                }
                grid_Prame.Rows[0].Visible = false;
                BindingSource bs = new BindingSource();
                bs.DataSource = dous;
                grid_Prame.DataSource = dous;
            }
            else
            {

                BindingSource bs = new BindingSource();
                bs.DataSource = dous;
                grid_Prame.DataSource = dous;
                grid_Prame.Rows[0].Visible = true;
            }
        }

        private void InputDialogForm_SizeChanged(object sender, EventArgs e)
        {
            //调用类的自适应方法，完成自适应
            //  As.controlAutoSize(this);
        }

        private void dgv_List_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
            {
                DataTable tb1 = GetDgvToTable(dgv_List);
                DataTable tb2 = new DataTable();
                tb2 = tb1.Copy();
            }
        }
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void grid_Prame_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.V)
            //{
            //    string[] arrText = CreatAllArry(Clipboard.GetText());//检索与指定格式相关联的数据
            //    for (int i = 0; i < arrText.Length; i++)
            //    {
            //        //  this.grid_Prame.Rows[i].Cells[0].Value = arrText[i].Substring(0, arrText[i].IndexOf('&'));
            //        this.grid_Prame.Rows[i].Cells[1].Value = arrText[i].Substring(arrText[i].IndexOf('&') + 1);
            //    }
            //    //IDataObject iData = Clipboard.GetDataObject();
            //    //if (iData.GetDataPresent(DataFormats.Text))
            //    //{
            //    //    CreateDictionary(DataFormats.Text);
            //    //    // copydata(DataFormats.Text);
            //    //    //如果剪贴板中的数据是文本格式  


            //    //    // this.grid_Prame.Rows[0].Cells[0].Value = (string)iData.GetData(DataFormats.Text.Substring(0, DataFormats.Text.IndexOf('&')));//检索与指定格式相关联的数据 
            //    //    this.grid_Prame.Rows[0].Cells[1].Value = (string)iData.GetData(DataFormats.Text.Substring(DataFormats.Text.IndexOf('&') + 1).ToString());//检索与指定格式相关联的数据 

            //    //    MessageBox.Show("11");
            //    //}
            //    //else
            //    //{
            //    //    MessageBox.Show("目前剪贴板中数据不可转换为文本", "错误");
            //    //}
            //}

        }
        private void grid_Prame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
            {
                PasteData();
            }

        }
        private void PasteData()
        {
            try
            {
                string clipboardText = Clipboard.GetText(); //获取剪贴板中的内容
                if (string.IsNullOrEmpty(clipboardText))
                {
                    return;
                }
                int colnum = 0;
                int rownum = 0;
                for (int i = 0; i < clipboardText.Length; i++)
                {
                    if (clipboardText.Substring(i, 1) == "\t")
                    {
                        colnum++;
                    }
                    if (clipboardText.Substring(i, 1) == "\n")
                    {
                        rownum++;
                    }
                }
                //粘贴板上的数据来源于EXCEL时，每行末尾都有\n，来源于DataGridView是，最后一行末尾没有\n
                if (clipboardText.Substring(clipboardText.Length - 1, 1) == "\n")
                {
                    rownum--;
                }
                colnum = colnum / (rownum + 1);
                object[,] data; //定义object类型的二维数组
                data = new object[rownum + 1, colnum + 1];  //根据剪贴板的行列数实例化数组
                string rowStr = "";
                //对数组各元素赋值
                for (int i = 0; i <= rownum; i++)
                {
                    for (int j = 0; j <= colnum; j++)
                    {
                        //一行中的其它列
                        if (j != colnum)
                        {
                            rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\t"));
                            clipboardText = clipboardText.Substring(clipboardText.IndexOf("\t") + 1);
                        }
                        //一行中的最后一列
                        if (j == colnum && clipboardText.IndexOf("\r") != -1)
                        {
                            rowStr = clipboardText.Substring(0, clipboardText.IndexOf("\r"));
                        }
                        //最后一行的最后一列
                        if (j == colnum && clipboardText.IndexOf("\r") == -1)
                        {
                            rowStr = clipboardText.Substring(0);
                        }
                        data[i, j] = rowStr;
                    }
                    //截取下一行及以后的数据
                    clipboardText = clipboardText.Substring(clipboardText.IndexOf("\n") + 1);
                }
                clipboardText = Clipboard.GetText();
                int cellsCount = grid_Prame.SelectedCells.Count;
                int r1 = (grid_Prame.SelectedCells[cellsCount - 1].RowIndex);
                int r2 = (grid_Prame.SelectedCells[0].RowIndex);
                int c1 = (grid_Prame.SelectedCells[cellsCount - 1].ColumnIndex);
                int c2 = (grid_Prame.SelectedCells[0].ColumnIndex);

                int rowIndex = Math.Abs(r2 - r1) + 1;
                int colIndex = Math.Abs(c2 - c1) + 1;

                if (colIndex != colnum + 1 || rowIndex != rownum + 1)
                {
                    MessageBox.Show("粘贴区域大小不一致");
                    return;
                }
                else
                {
                    for (int i = 0; i <= rownum; i++)
                    {
                        for (int j = 0; j <= colnum; j++)
                        {

                            grid_Prame.Rows[i + r1].Cells[j + c1].Value = data[i, j];
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("粘贴区域大小不一致");
                return;
            }
        }

    }
}
