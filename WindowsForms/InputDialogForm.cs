
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsForms
{
    public partial class InputDialogForm : Form
    {

        // AutoSize As = new AutoSize();  
        //定义委托
        public delegate void TextEventHandler(string strText);
        public List<DouCustom> dous;
        public string strString = "";
        public string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
        private SaveDatGirdCustom custom = null;
        //定义变量用于选中行数功能
        int r = -1, Icol = -1;
        //时间计时器
        System.Timers.Timer t = new System.Timers.Timer(1 * 0.5 * 0.5 * 1000);

        public InputDialogForm()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_dous">DouCustom 集合数据</param>
        /// <param name="_strString">字符串数据</param>
        public InputDialogForm(List<DouCustom> _dous, string _strString)
        {     //事件调用线程错误捕获
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.dous = _dous;
            this.strString = _strString;
            //groupBox_Search.Text = UseTextBoxValue.Text_String;

        }
        //申明委托
        public TextEventHandler TextHandler;
        /// <summary>
        /// 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
            comboBox_list.SelectedIndex = comboBox_list.Items.IndexOf("--请选择--");
            if (txt_ComboxSaveName.Text == "" | comboBox_list.SelectedIndex == comboBox_list.Items.IndexOf("--请选择--"))
            {
                ConvertTxtToDataSet();
            }
            if (UseTextBoxValue.Text_String != "")
            {
                TimerEvent();
            }
            label4.Text = "一、参数代号不可以填写重复值\n";
            label2.Text = "二、无代号，缺省用”0“表示；\n不带附件用“ - ”表示；无附件用“00”表示\n";
            label1.Text = "三、删除行请使用-delete-操作\n";
            label3.Text = "四、数据表绑定数据后,不可多行操作";
            // As.controllInitializeSize(this);
            if (dous == null)
            {
                // ConvertTxtToDataSet();
                //for (int i = 0; i < 50; i++)
                //{
                //    grid_Prame.Rows.Add();
                //}
                //grid_Prame.Rows[0].Visible = false;
            }
            else
            {

                grid_Prame.Rows.Clear();
                //BindingSource bs = new BindingSource();
                //bs.DataSource = dous;
                //grid_Prame.DataSource = bs;   

                // 指定DataGridView控件显示的列数   
                grid_Prame.ColumnCount = 2;
                //显示列标题   
                grid_Prame.ColumnHeadersVisible = true;
                //设置DataGridView控件标题列的样式   
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                //设置列标题的背景颜色    
                columnHeaderStyle.BackColor = Color.Beige;
                //设置列标题的字体大小、样式      
                columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                grid_Prame.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                //设置DataGridView控件的标题列名    
                grid_Prame.Columns[0].Name = "Code";
                grid_Prame.Columns[1].Name = "ConString";
                string[] row1 = null;
                for (int i = 0; i < dous.Count; i++)
                {
                    row1 = new string[] { dous[i].Code, dous[i].ConString };
                    grid_Prame.Rows.Add(row1);
                }
            }
        }

        private void InputDialogForm_SizeChanged(object sender, EventArgs e)
        {
            //调用类的自适应方法，完成自适应
            //  As.controlAutoSize(this);
        }
        /// <summary>
        /// 数据复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_List_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
            //{
            //    DataTable tb1 = GetDgvToTable(dgv_List);
            //    DataTable tb2 = new DataTable();
            //    tb2 = tb1.Copy();
            //}
        }
        /// <summary>
        /// 数据转换datatable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
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

        /// <summary>
        /// dgv_list的数据处理粘贴到grid_Oram数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_Prame_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys keyData = new Keys();
            if (keyData == (Keys.V | Keys.Control | Keys.Shift))
            {
                PasteData();
            }
            //if (e.KeyChar == 22)
            //{
            //    PasteData();
            //}

        }
        /// <summary>
        /// 粘贴板处理Excel数据
        /// </summary>
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
                            //增加行
                            grid_Prame.Rows.Add();

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
        /// <summary>
        /// 粘贴板处理Excel数据
        /// </summary>
        /// <param name="data1"></param>
        private void copydata(string data1)
        {
            string clipboardText = Clipboard.GetText(); //获取剪贴板中的内容

            if (data1.Trim().Length < 1) { return; }
            try
            {
                int colnum = 0;
                int rownum = 0;
                for (int i = 0; i < clipboardText.Length; i++)
                {
                    if (clipboardText.Substring(i, 1).Equals("\t"))
                    {
                        colnum++;
                    }
                    if (clipboardText.Substring(i, 1).Equals("\n"))
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
                int start, end = -1, index, rowStart = 0, columnStart = 0;

                rowStart = r;//选中单元格的行号
                columnStart = Icol;//选中单元格的列号
                DataSet dsMainFilter1 = new DataSet();
                for (int i = 0; i <= rownum; i++)
                {
                    #region 如果datagridview中行数不够，就自动增加行
                    if ((i + rowStart) > grid_Prame.Rows.Count - 1)
                    {
                        //添加新行　　　　　　　　　　　　
                        DataRow row = dsMainFilter1.Tables[0].NewRow();
                        //str = SYSVARS.vars.userId + System.DateTime.Now.ToString("yyyyMMddHHmmss").ToString().Trim() + dsMainFilter1.Tables[0].Rows.Count.ToString();//以时间标识代码不同的单据号

                        dsMainFilter1.Tables[0].Rows.Add(row);
                    }


                    #endregion

                    for (int j = 0; j <= colnum; j++)//将值赋值过去---如果datagridview中没有自动增加列
                    {
                        #region 需要判断单元格是不是只读的，是只读的就不用不赋值
                        bool iszd = this.grid_Prame.Rows[i + rowStart].Cells[j + columnStart].ReadOnly;
                        if (iszd == true)
                        {
                            continue;
                        }
                        #endregion

                        string sjz = "";
                        try
                        {
                            sjz = data[i, j].ToString();
                        }
                        catch { sjz = ""; }
                        if (sjz.Trim().Length < 1) { continue; }//直接复制this.dataGridView3.Rows[i + rowStart].Cells[j + columnStart].Value = sjz;
                    }
                }


            }
            catch { }
        }
        /// <summary>
        /// 重写this.dataGridView1的ProcessCmdKey方法，获取键盘点击事件，识别Ctrl+V
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //在检测到按Ctrl+V键后，系统无法复制多单元格数据，重写ProcessCmdKey方法，屏蔽系统粘贴事件，使用自定义粘贴事件，在事件中对剪贴板的HTML格式进行处理，获取表数据，更新DataGrid控件内容
            if (keyData == (Keys.V | Keys.Control) && this.dgv_List.SelectedCells.Count > 0 && !this.dgv_List.SelectedCells[0].IsInEditMode)  // &&
            {
                IDataObject idataObject = Clipboard.GetDataObject();
                string[] s = idataObject.GetFormats();
                string data;

                if (!s.Any(f => f == "OEMText"))
                {
                    if (!s.Any(f => f == "HTML Format"))
                    {

                    }
                    else
                    {
                        data = idataObject.GetData("HTML Format").ToString();//多个单元格     
                        copyClipboardHtmltoGrid(data, this.dgv_List);
                        //msg = Message.;
                        msg = new Message();
                        return base.ProcessCmdKey(ref msg, Keys.Control);

                    }
                }
                else
                    data = idataObject.GetData("OEMText").ToString();//单个单元格,使用系统功能，无需处理

            }

            //在检测到按Shift+V键后，系统无法复制多单元格数据，重写ProcessCmdKey方法，屏蔽系统粘贴事件，使用自定义粘贴事件，在事件中对剪贴板的HTML格式进行处理，获取表数据，更新DataGrid控件内容
            if (keyData == (Keys.V | Keys.Shift) && this.grid_Prame.SelectedCells.Count > 0 && !this.grid_Prame.SelectedCells[0].IsInEditMode)  // &&
            {
                IDataObject idataObject = Clipboard.GetDataObject();
                string[] s = idataObject.GetFormats();
                string data;

                if (!s.Any(f => f == "OEMText"))
                {
                    if (!s.Any(f => f == "HTML Format"))
                    {

                    }
                    else
                    {
                        data = idataObject.GetData("HTML Format").ToString();//多个单元格


                        copyClipboardHtmltoGrid(data, this.grid_Prame);

                        //msg = Message.;
                        msg = new Message();
                        return base.ProcessCmdKey(ref msg, Keys.Control);

                    }
                }
                else
                    data = idataObject.GetData("OEMText").ToString();//单个单元格,使用系统功能，无需处理
            }

            #region excel复制粘贴功能


            if (keyData == (Keys.V | Keys.Control | Keys.Shift))  // ctrl+V
            {
                bool bd = grid_Prame.Focus();//避免影响到界面上其他功能使用粘贴
                if (bd == true)
                {
                    IDataObject idataObject = Clipboard.GetDataObject();
                    string da = Clipboard.GetText();
                    string[] s = idataObject.GetFormats();
                    copydata(da);
                    return true;//很重要，不写将会把所有值填充在最后一个单元格里面
                }

            }
            #endregion
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 获取剪贴板HTML数据并解析，将多行多列数据分别填至右边datagridview光标指定或选中的单元格中
        /// </summary>
        /// <param name="data"></param>
        private void copyClipboardHtmltoGrid(string data, DataGridView dataGrid)
        {
            //截取出HTML内容
            int start, end = -1, index, rowStart = 0, columnStart = 0;
            Regex regex = new Regex(@"StartFragment:\d+");
            Match match = regex.Match(data);
            if (match.Success)
            {
                start = Convert.ToInt16(match.Value.Substring(14));
            }
            else
            {
                return;
            }
            regex = new Regex(@"EndFragment:\d+");
            match = regex.Match(data, match.Index + match.Length);
            if (match.Success)
            {
                end = Convert.ToInt16(match.Value.Substring(12));
            }
            else
            {
                return;
            }

            if (dataGrid.SelectedCells.Count > 0)
            {
                rowStart = dataGrid.SelectedCells[0].RowIndex;
                columnStart = dataGrid.SelectedCells[0].ColumnIndex;
            }
            data = data.Substring(start, end - start);

            MatchCollection matchcollection = new Regex(@"<TR>[\S\s]*?</TR>").Matches(data), sub_matchcollection;
            int count = rowStart + matchcollection.Count - dataGrid.RowCount;
            if (count >= 0)
            {
                dataGrid.Rows.Add(count + 1);
            }
            for (int i = 0; i < matchcollection.Count && i + rowStart < dataGrid.RowCount; i++)
            {
                sub_matchcollection = new Regex(@"<TD>[\S\s]*?</TD>").Matches(matchcollection[i].Value);
                for (int j = 0; j < sub_matchcollection.Count && j + columnStart < dataGrid.ColumnCount; j++)
                {
                    dataGrid.Rows[i + rowStart].Cells[j + columnStart].Value = sub_matchcollection[j].Value.Substring(4, sub_matchcollection[j].Length - 9).Trim();
                }
            }
        }
        /// <summary>
        /// 获取值事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ////groupBox_Search.Text = UseTextBoxValue.Text_String;
            //if (UseTextBoxValue.Text_String == "")
            //{
            //    groupBox_Search.Text = "条件不存在";
            //}
            //else
            //{
            //    //   string strLocalPath = @"..//DataGridViewToXml";      
            //    string strLocalPath = System.Windows.Forms.Application.StartupPath + "//DataGridViewToXml//";
            //    string serchName = groupBox_Search.Text + ".xml";

            //    string fullName = strLocalPath + "\\" + serchName;
            //    if (!File.Exists(fullName))
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        dgv_List.Rows.Clear();
            //        List<GetStringToBindDataGrid> list = ReadDataXml(fullName);
            //        BindingSource bs = new BindingSource();
            //        bs.DataSource = list;
            //        dgv_List.DataSource = bs;
            //    }
            //    #region DataGridViewToXml  
            //    //  pathString = "DataGridViewToXml/" + textBox_Name.Text + ".xml";

            //    //if (UseTextBoxValue.Text_String != "")
            //    //{
            //    //    OpenFileDialog dialog = new OpenFileDialog();
            //    //    dialog.Multiselect = false;
            //    //    dialog.Title = "请选择文件夹";
            //    //    dialog.Filter = "(.xml) | *.xml";

            //    //    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    //        groupBox_Search.Text = System.IO.Path.GetFileName(System.IO.Path.GetFileName(UseTextBoxValue.Text_String).Substring(0, System.IO.Path.GetFileName(UseTextBoxValue.Text_String).IndexOf('.')));

            //    //    List<SaveDatGirdCustom> list = ReadDataXml(dialog.FileName);
            //    //    BindingSource bs = new BindingSource();
            //    //    bs.DataSource = list;
            //    //    dgv_List.DataSource = bs;

            //    //}
            //    #endregion
            //}
        }
        /// <summary>
        /// 读取保存到x'm'l的combox数据
        /// </summary>
        /// <param name="dataXmlPath"></param>
        /// <returns></returns>
        private List<GetStringToBindDataGrid> ReadDataXml(string dataXmlPath)
        {
            #region MyRegion   
            List<GetStringToBindDataGrid> list = new List<GetStringToBindDataGrid>();
            //   string dataXmlPath = subpath + "" + txt_productName.Text.ToUpper() + ".xml";
            if (dataXmlPath != null)
            {
                StreamReader sr = new StreamReader(dataXmlPath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //在此处添加需要对文件中每一行数据进行处理的代码   
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveDatGirdCustom));
                    SaveDatGirdCustom cm = (SaveDatGirdCustom)serializer.Deserialize(sr);
                    string pxml = GetXmlText(cm.LCode);
                    list = GetStringToList(pxml);
                }

                sr.Close();
                sr.Dispose();

            }
            return list;
            #endregion



            #region MyRegion   
            //List<SaveDatGirdCustom> list = new List<SaveDatGirdCustom>();
            //// string dataXmlPath = subpath + "" + txt_productName.Text.ToUpper() + ".xml";
            //if (dataXmlPath != null)
            //{
            //    using (FileStream fs = new FileStream(dataXmlPath, FileMode.Open, FileAccess.Read))
            //    {
            //        XmlSerializer serializer = new XmlSerializer(typeof(SaveDatGirdCustom));
            //        SaveDatGirdCustom cm = (SaveDatGirdCustom)serializer.Deserialize(fs);
            //        list.Add(cm);
            //    }
            //}
            //return list;
            #endregion
        }
        /// <summary>
        /// 得到xml数据转换后的内容为字符串格式
        /// </summary>
        /// <param name="strArr"></param>
        /// <returns></returns>
        private String GetXmlText(string[] strArr)
        {
            string strAllString = "";
            string name = "";
            if (strArr == null)
            {
                strArr = new string[] { };
                name = strAllString;
            }
            if (strArr.Length > 0 && strArr != null)
            {
                for (int i = 0; i < strArr.Length; i++)
                {
                    strAllString += '\n' + strArr[i] + '\r';
                }
                name = strAllString;
            }
            else
            {
                name = strAllString;
            }
            return name;

        }
        private void groupBox_Search_TextChanged(object sender, EventArgs e)
        {


        }
        /// <summary>
        /// 触发定时器
        /// </summary>
        private void TimerEvent()
        {
            t.Elapsed += new System.Timers.ElapsedEventHandler(timeup);
            t.Enabled = true;
        }
        /// <summary>
        /// 触发定时器事件调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeup(object sender, System.Timers.ElapsedEventArgs e)
        {
            //调用点击事件
            this.button1.PerformClick();
            //点击事件调用结束后停止计时
            t.Stop();
        }
        /// <summary>
        /// 保存预加载数据到xml事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_savedatagridview_Click(object sender, EventArgs e)
        {
            string saveName = txt_ComboxSaveName.Text;

            // string getComBox_Text = txt_ComboxSaveName.Text;
            if (saveName == "" | saveName == null | string.IsNullOrEmpty(saveName))
            {
                MessageBox.Show("请您输入要保存的文件名称");
                txt_ComboxSaveName.BackColor = Color.Beige;
                return;
            }
            if (dgv_List.Rows.Count <= 1)
            {
                MessageBox.Show("您要保存的数据为空");
                return;
            }
            for (int j = 0; j < this.comboBox_list.Items.Count; j++)
            {
                if (this.comboBox_list.Items[j].Equals(saveName))
                {
                    MessageBox.Show("该文件名已存在,如需要修改请选择后修改");
                    return;
                }

            }
            comboBox_list.Items.Clear();

            string strBarcodeList = GetStringData(dgv_List);
            List<SaveDatGirdCustom> list = GetStrToList(strBarcodeList);

            WriteXmlData(saveName);
            SaveDataGridViewToXml(list, saveName);

            comboBox_list.Items.Clear();
            ConvertTxtToDataSet();


        }

        public string GetStringData(DataGridView dataGrid)
        {
            string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符   
            for (int i = 0; i < dgv_List.Rows.Count; i++)
            {
                String[] arr = new String[] { };
                if (this.dgv_List.Rows[i].Cells["LCode"].Value != null && this.dgv_List.Rows[i].Cells["CConString"].Value != null)
                {
                    string sd = this.dgv_List.Rows[i].Cells[0].Value + "&" + this.dgv_List.Rows[i].Cells[1].Value.ToString() + ",";
                    arr = sd.Trim().Split(',');
                    for (int v = 0; v < arr.Length; v++)
                    {
                        strBarcodeList += arr[v].Replace(",\n", "") + "\r\n";//将分隔开的字符串进行重新组装中间加\r\n回车
                    }
                    if (strBarcodeList.Length > 0)
                        strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);
                }
            }
            return strBarcodeList;
        }
        /// <summary>
        /// 写入预加载文件名到txt文件
        /// </summary>
        /// <param name="strName"></param>
        public void WriteXmlData(string strName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
                path = path + "XmlComListData/";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fs = new FileStream(path + "ConBox_list.txt", FileMode.Append))
                {
                    //FileStream fs = new FileStream(path + "ConBox_list.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("utf-8"));
                    sw.Write("" + strName + "" + "\r\n");
                    sw.Flush();
                    sw.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // <summary>
        /// 从txt文件中得到文件名，再显示到comboBox上
        /// </summary>
        private void ConvertTxtToDataSet()
        {

            string ReadLine;
            string[] array;
            string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//XmlComListData//";

            string serchName = "ConBox_list.txt";

            string fullName = strLocalPath + "\\" + serchName;

            if (!File.Exists(fullName))
            {
                MessageBox.Show("您要加载的数据文件不存在，请重试！");
                return;
            }
            else
            {
                //  string Path = @"..//"+UseTextBoxValue.Text_String+"";

                StreamReader reader = new StreamReader(fullName,
                                      System.Text.Encoding.GetEncoding("utf-8"));

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        array = ReadLine.Split('\n');
                        if (array.Length == 0)
                        {
                            MessageBox.Show("您选择的导入数据类型有误，请重试！");
                            return;


                        }
                        this.comboBox_list.Items.Add(array[0]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                reader.Close();
                reader.Dispose();

            }

        }
        /// <summary>
        /// 查询数据判断是否已存在此名称，存在则替换
        /// </summary>
        private void UpdateTxtToDataSet(string updateName)
        {
            string ReadLine;
            string[] array;
            string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//XmlComListData//";

            string serchName = "ConBox_list.txt";

            string fullName = strLocalPath + "\\" + serchName;
            if (!File.Exists(fullName))
            {
                MessageBox.Show("您要加载的数据文件不存在，请重试！");
                return;
            }
            else
            {
                //  string Path = @"..//"+UseTextBoxValue.Text_String+"";

                StreamReader reader = new StreamReader(fullName,
                                      System.Text.Encoding.GetEncoding("utf-8"));
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        array = ReadLine.Split('\n');
                        if (array.Length == 0)
                        {
                            MessageBox.Show("您选择的导入数据类型有误，请重试！");
                            return;


                        }
                        if (updateName.Equals(array[0]))
                        {
                            array[0].Replace(array[0], updateName);

                        }
                        this.comboBox_list.Items.Add(array[0]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                reader.Close();
                reader.Dispose();
            }

        }
        // <summary>
        /// 模糊查询从txt文件中得到文件名，再显示到comboBox上
        /// </summary>
        private void ConvertTxtToDataSet(string searchTextName)
        {
            string ReadLine;
            string[] array;
            string[] array1;
            string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//XmlComListData//";

            string serchName = "ConBox_list.txt";

            string fullName = strLocalPath + "\\" + serchName;
            if (!File.Exists(fullName))
            {
                MessageBox.Show("您要加载的数据文件不存在，请重试！");
                return;
            }
            else
            {

                //  string Path = @"..//"+UseTextBoxValue.Text_String+"";

                StreamReader reader = new StreamReader(fullName,
                                      System.Text.Encoding.GetEncoding("utf-8"));
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();

                        array = ReadLine.Split('\n');
                        if (array.Length == 0)
                        {
                            MessageBox.Show("您选择的导入数据类型有误，请重试！");
                            return;

                        }

                        if (array[0].Trim().ToString().Contains("" + searchTextName + "%") == true)
                        {
                            MessageBox.Show("True");
                        }

                        if (array[0].Trim().ToString().Contains("%" + searchTextName + "%") == true | array[0].Trim().ToString().Contains("%" + searchTextName + "") == true | array[0].Trim().ToString().Contains("" + searchTextName + "%") == true | array[0].Trim().ToString().Contains("" + searchTextName + "") == true)
                        {
                            array1 = array;
                            comboBox_list.Items.Add(array1[0]);
                        }



                        //MessageBox.Show(array[0]);
                        // comboBox_list.Items.Clear();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                reader.Close();
                reader.Dispose();
            }
        }
        /// <summary>
        /// 保存datagridview数据到x'm'l，保存预加载数据信息
        /// </summary>
        /// <param name="columns"></param>
        public void SaveDataGridViewToXml(List<SaveDatGirdCustom> columns, string strName)
        {
            #region 数据记忆保存  

            string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//DataGridViewToXml//";
            // string path = "../Debug" + "//DataGridViewToXml//";
            string pathString = "DataGridViewToXml/" + strName + ".xml";



            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (System.IO.Directory.Exists(pathString) == false)
            {

                FileStream fs = new FileStream(pathString, FileMode.Create, FileAccess.Write);

                XmlSerializer xs = new XmlSerializer(typeof(SaveDatGirdCustom));
                xs.Serialize(fs, custom);
                fs.Close();
                fs.Dispose();

            }
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK);
            comboBox_list.Items.Clear();
            dgv_List.Rows.Clear();
            ConvertTxtToDataSet();
            //txt_ComboxSaveName.Text = string.Empty;
            columns.Clear();

            #endregion




        }
        /// <summary>
        /// combox数据值被选中加载数据到右侧datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_List.Rows.Clear();
            // comboBox_list.Items.Clear();
            //comboBox_list.SelectedIndex = comboBox_list.Items.IndexOf("--请选择--");
            // MessageBox.Show(comboBox_list.SelectedItem.ToString());
            if (comboBox_list.SelectedItem.ToString() == "--请选择--")
            {
                dgv_List.Rows.Clear();
                return;
            }
            else
            {
                //   string strLocalPath = @"..//DataGridViewToXml";      
                string strLocalPath = System.Windows.Forms.Application.StartupPath + "//DataGridViewToXml//";
                string serchName = comboBox_list.SelectedItem.ToString() + ".xml";

                string fullName = strLocalPath + "\\" + serchName;
                if (!File.Exists(fullName))
                {

                }
                else
                {

                    List<GetStringToBindDataGrid> list = ReadDataXml(fullName);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = list;
                    dgv_List.Rows.Clear();
                    dgv_List.DataSource = bs;

                }
            }
        }

        private List<SaveDatGirdCustom> GetStrToList(string arrString)
        {
            List<SaveDatGirdCustom> list = new List<SaveDatGirdCustom>();
            //SaveDatGirdCustom custom = null;
            string[] OmeArr = new string[] { };

            string[] arrAll = arrString.Trim().Split('\r');//将文本框的内容按回车进行分组 

            string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
            for (int v = 0; v < arrAll.Length; v++)
            {
                strBarcodeList += arrAll[v].Replace("\n", "") + ",";//将分隔开的字符串进行重新组装中间加,逗号
            }
            if (strBarcodeList.Length > 0)
                strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);//去除字符串最后的逗号 
            /*string[] */
            OmeArr = strBarcodeList.Split(',');
            custom = new SaveDatGirdCustom
            {
                LCode = OmeArr,
            };

            list.Add(custom);
            return list;
        }
        /// <summary>
        /// 将字符串数据转换成  List集合
        /// </summary>
        /// <param name="arrString"></param>
        /// <returns></returns>
        private List<GetStringToBindDataGrid> GetStringToList(string arrString)
        {
            List<GetStringToBindDataGrid> list = new List<GetStringToBindDataGrid>();
            GetStringToBindDataGrid dou = null;
            Dictionary<string, string> newKeyValuesList = new Dictionary<string, string>();
            string[] OmeArr = new string[] { };

            string[] arrAll = arrString.Trim().Split('\r');//将文本框的内容按回车进行分组 

            string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
            for (int v = 0; v < arrAll.Length; v++)
            {
                strBarcodeList += arrAll[v].Replace("\n", "") + ",";//将分隔开的字符串进行重新组装中间加,逗号
            }
            if (strBarcodeList.Length > 0)
                strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);//去除字符串最后的逗号 
            /*string[] */
            OmeArr = strBarcodeList.Split(',');

            //  newKeyValuesList = new Dictionary<string, string>();
            for (int i = 0; i < OmeArr.Length; i++)
            {
                if (OmeArr[i].IndexOf('&') > 0)
                {
                    //System.Windows.Forms.MessageBox.Show(OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')));//截取&前面的值
                    // System.Windows.Forms.MessageBox.Show(OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString());//截取&后面的值
                    if (OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')) != null && OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString() != null)
                    {
                        dou = new GetStringToBindDataGrid
                        {
                            LCode = OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')),
                            CConString = OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString()
                        };
                        list.Add(dou);
                        // douCustom = new DouCustom { Code = OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')), ConString = OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString() };
                        //newKeyValuesList.Add(OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')), OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString());

                    }
                }

            }

            return list;
        }
        /// <summary>
        /// 查询数据并重新绑定下拉列表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_search_Click(object sender, EventArgs e)
        {
            // comboBox_list.Text = string.Empty;
            comboBox_list.Items.Clear();
            dgv_List.Rows.Clear();
            string getSerch = txt_ComboxSaveName.Text;
            if (txt_ComboxSaveName.Text != "")
            {        //调用绑定函数
                ConvertTxtToDataSet(txt_ComboxSaveName.Text);
            }
            else
            {     //刷新
                ConvertTxtToDataSet();
            }

        }
        /// <summary>
        /// 修改combox选中下的数据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_update_Click(object sender, EventArgs e)
        {
            string getUpdateName = comboBox_list.SelectedItem.ToString();
            if (getUpdateName != "--请选择--")
            {
                UpdateTxtToDataSet(getUpdateName);
                string nameValue = comboBox_list.SelectedItem.ToString();
                string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
                strBarcodeList = GetStringData(dgv_List);

                List<SaveDatGirdCustom> list = GetStrToList(strBarcodeList);
                if (list == null)
                {
                    MessageBox.Show("要保存的数据为空!");
                    return;
                }
                else
                {
                    //保存数据到xml文件
                    SaveDataGridViewToXml(list, nameValue);
                    //写入数据到txt便于combox读取
                    //WriteXmlData(nameValue);
                    comboBox_list.Items.Clear();
                    comboBox_list.SelectedIndex = comboBox_list.Items.IndexOf("--请选择--");
                    //刷新
                    ConvertTxtToDataSet();

                }
            }
            else
            {
                MessageBox.Show("请选择您要修改的一项");
                //return;
            }

        }

        private void grid_Prame_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                r = e.RowIndex; Icol = e.ColumnIndex;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除combox选定的数据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            if (comboBox_list.Items.Count == 0)
            {
                MessageBox.Show("请您选择要删除的项");
                return;
            }
            if (comboBox_list.Text == "")
            {
                MessageBox.Show("请您选择要删除的项为空");
                return;
            }
            if (comboBox_list.SelectedItem.ToString() == "--请选择--")
            {
                MessageBox.Show("请您选择要删除的项");
                return;
            }
            string getSelectedItem_Name = comboBox_list.SelectedItem.ToString();

            string strLocalPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//XmlComListData//";

            string serchName = "ConBox_list.txt";

            string fullName = strLocalPath + "\\" + serchName;

            if (!File.Exists(fullName))
            {
                MessageBox.Show("您要加载的数据文件不存在，请重试！");
                return;
            }
            List<string> lines = new List<string>(File.ReadAllLines(fullName));
            lines.Remove(getSelectedItem_Name);
            File.WriteAllLines(fullName, lines.ToArray());

            string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//DataGridViewToXml//";
            string XmlName = getSelectedItem_Name + ".xml";

            string fullXmlName = path + "\\" + XmlName;
            //  string pathString = "DataGridViewToXml/" + getSelectedItem_Name + ".xml";
            // 1、首先判断文件或者文件路径是否存在
            if (File.Exists(fullXmlName))
            {
                // 2、根据路径字符串判断是文件还是文件夹
                FileAttributes attr = File.GetAttributes(fullXmlName);
                // 3、根据具体类型进行删除
                if (attr == FileAttributes.Directory)
                {
                    // 3.1、删除文件夹
                    Directory.Delete(fullXmlName, false);
                }
                else
                {
                    // 3.2、删除文件
                    File.Delete(fullXmlName);
                }
                File.Delete(fullXmlName);
            }

            dgv_List.Rows.Clear();
            comboBox_list.Text = string.Empty;
            comboBox_list.Items.Clear();
            ConvertTxtToDataSet();
        }
    }

}
