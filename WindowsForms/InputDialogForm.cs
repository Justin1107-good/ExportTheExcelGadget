
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        //时间计时器
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
            groupBox_Search.Text = UseTextBoxValue.Text_String;

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
            ConvertTxtToDataSet();
            //dgv_List.Rows.Clear();
            //dgv_List.DataSource = douCustoms;
            // dgv_List.ReadOnly = true;
            if (UseTextBoxValue.Text_String != "")
            {
                TimerEvent();
            }
            label4.Text = "一、参数代号不可以填写重复值";
            label2.Text = "二、无代号，缺省用”0“表示；\n\n不带附件用“ - ”表示；无附件用“00”表示\n";
            label1.Text = "三、删除行请使用-delete-操作";
            // As.controllInitializeSize(this);
            if (dous == null)
            {
                //ConvertTxtToDataSet();
                for (int i = 0; i < 50; i++)
                {
                    grid_Prame.Rows.Add();
                }
                grid_Prame.Rows[0].Visible = false;
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
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
            {
                DataTable tb1 = GetDgvToTable(dgv_List);
                DataTable tb2 = new DataTable();
                tb2 = tb1.Copy();
            }
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
            //if (e.KeyChar == 3 && grid_Prame.Rows.Count > 0)
            //{
            //    MessageBox.Show("数据已经被绑定，此操作不可执行");
            //    return;
            //}
            if (e.KeyChar == 22)
            {
                PasteData();
            }


        }
        /// <summary>
        /// 粘贴板处理
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

            if (!string.IsNullOrEmpty((UseTextBoxValue.Text_String)))
            {
                string nameValue = UseTextBoxValue.Text_String;
                //List<SaveDatGirdCustom> list = new List<SaveDatGirdCustom>();
                //DataTable dataTable = GetDgvToTable(grid_Prame);    

                string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 

                for (int i = 0; i < grid_Prame.Rows.Count; i++)
                {
                    String[] arr = new String[] { };
                    String[] arr1 = new String[] { };
                    if (this.grid_Prame.Rows[i].Cells["Code"].Value != null && this.grid_Prame.Rows[i].Cells["ConString"].Value != null)
                    {
                        //  custom  
                        string sd = this.grid_Prame.Rows[i].Cells[0].Value + "&" + this.grid_Prame.Rows[i].Cells[1].Value.ToString() + ",";
                        //  string sd = this.grid_Prame.Rows[i].Cells[0].Value + ",";
                        // string sd1 = this.grid_Prame.Rows[i].Cells[1].Value.ToString() + ",";
                        arr = sd.Trim().Split(',');
                        //  arr1 = sd1.Trim().Split(',');
                        //custom = new SaveDatGirdCustom { LCode = arr, CConString = arr1 };
                        //list.Add(custom);

                        for (int v = 0; v < arr.Length; v++)
                        {
                            strBarcodeList += arr[v].Replace(",\n", "") + "\r\n";//将分隔开的字符串进行重新组装中间加\r\n回车
                        }
                        if (strBarcodeList.Length > 0)
                            strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);

                    }

                }
                List<SaveDatGirdCustom> list = GetStrToList(strBarcodeList);
                if (list == null)
                {
                    MessageBox.Show("要保存的数据为空!");
                    return;
                }
                else
                {
                    SaveDataGridViewToXml(list);
                    WriteXmlData(nameValue);
                    comboBox_list.Items.Clear();
                    comboBox_list.SelectedIndex = comboBox_list.Items.IndexOf("--请选择--");
                    ConvertTxtToDataSet();


                }
            }
            else
            {
                MessageBox.Show("填写您要保存的文件名");
                return;
            }
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

                FileStream fs = new FileStream(path + "ConBox_list.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write("" + strName + "" + "\r\n");
                sw.Flush();
                sw.Close();
                //ConvertTxtToDataSet();
            }
            catch
            {
                return;
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
                                      System.Text.Encoding.GetEncoding("GB2312"));
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
        /// 保存datagridview数据到x'm'l，保存预加载数据信息
        /// </summary>
        /// <param name="columns"></param>
        public void SaveDataGridViewToXml(List<SaveDatGirdCustom> columns)
        {
            #region 数据记忆保存  

            // string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "//DataGridViewToXml//";
            string path = "../Debug" + "//DataGridViewToXml//";
            string pathString = "DataGridViewToXml/" + UseTextBoxValue.Text_String + ".xml";

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


            }
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK);

            columns.Clear();
            comboBox_list.Items.Clear();
            ConvertTxtToDataSet();
            #endregion




        }
        /// <summary>
        /// combox数据值被选中加载数据到右侧datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgv_List.Rows.Clear();
            // MessageBox.Show(comboBox_list.SelectedItem.ToString());
            if (comboBox_list.SelectedIndex == 0)
            {
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

                    //// 指定DataGridView控件显示的列数   
                    //dgv_List.ColumnCount = 2;
                    ////显示列标题   
                    //dgv_List.ColumnHeadersVisible = true;
                    ////设置DataGridView控件标题列的样式   
                    //DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                    ////设置列标题的背景颜色    
                    //columnHeaderStyle.BackColor = Color.Beige;
                    ////设置列标题的字体大小、样式      
                    //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                    //dgv_List.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                    ////设置DataGridView控件的标题列名    
                    //dgv_List.Columns[0].Name = "Code";
                    //dgv_List.Columns[1].Name = "ConString";




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
    }

}
