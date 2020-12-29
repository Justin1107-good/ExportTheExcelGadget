using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using WindowsForms.MessageFrom;

namespace WindowsForms
{

    public partial class Frm_Excel : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }

        Microsoft.Office.Interop.Excel.Application excel = null;
        Microsoft.Office.Interop.Excel.Workbook wBook = null;
        Microsoft.Office.Interop.Excel._Worksheet workSheet;
        AutoSize auto = new AutoSize();
        private int n = 0;
        //定义全局变量
        public int currentCount = 0;
        private DateTime startTime;
        private Dictionary<string, string> keyValues;
        private Dictionary<string, string> keyValues1;
        private Dictionary<string, string> keyValues2;
        private Dictionary<string, string> keyValues3;
        private Dictionary<string, string> keyValues4;
        private Dictionary<string, string> keyValues5;
        private Dictionary<string, string> keyValues6;
        private Dictionary<string, string> keyValues7;
        private Dictionary<string, string> keyValues8;
        private Dictionary<string, string> keyValues9;
        private Dictionary<string, string> keyValues10;
        private Dictionary<string, string> keyValues11;
        private Dictionary<string, string> keyValues12;
        private Dictionary<string, string> keyValues13;
        private Dictionary<string, string> keyValues14;
        private Dictionary<string, string> keyValues15;
        private Dictionary<string, string> keyValues16;
        private Dictionary<string, string> keyValues17;

        private List<string> groups = null;
        List<ColumnModel> columns = null;
        public List<DouCustom> douCustoms = null;
        // DouCustom douCustom = null;
        List<PrameterModel> arrModels = null;
        //PrameterModel arrModel = null;
        ColumnModel column = null;
        string filePath = "";
        public string GetUpdate = "";
        private string IsDeleteData = "";
        StringBuilder sb = new StringBuilder();
        System.Timers.Timer t = new System.Timers.Timer(1 * 0.5 * 0.5 * 1000);
        //static string currpath = System.Windows.Forms.Application.StartupPath;     
        public Frm_Excel()
        {
            //事件调用线程错误捕获
            Control.CheckForIllegalCrossThreadCalls = false;

            WindowState = FormWindowState.Normal;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            InitializeComponent();
            this.progressBar1.Value = 0;
            if (keyValues == null)
            {
                keyValues = new Dictionary<string, string>();
            }
            if (columns == null)
            {
                columns = new List<ColumnModel>();

            }
            if (column == null)
            {
                column = new ColumnModel();
            }
            if (arrModels == null)
            {
                arrModels = new List<PrameterModel>();
            }

        }
        public Frm_Excel(List<DouCustom> _douCustoms)
        {
            InitializeComponent();
            if (douCustoms == null)
            {
                douCustoms = new List<DouCustom>();
            }
            this.douCustoms = _douCustoms;

            for (int i = 0; i < douCustoms.Count; i++)
            {
                sb.Append((douCustoms[i].Code + "&" + douCustoms[i].ConString) + "\r");
                // MessageBox.Show(douCustoms[i].Code+"&"+ douCustoms[i].ConString);
            }
            // MessageBox.Show(sb.ToString());
            // txt_KeJiaAModel.Text = sb.ToString();
            // InputDialogForm inf = new InputDialogForm(sb.ToString());
            // inf.ShowDialog();
            txt_KeJiaAModel.Text = sb.ToString();
            MessageBox.Show(txt_KeJiaAModel.Text);
        }
        /// <summary>
        /// 正则判断格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>                     
        public bool IsSpecialChar(string str)
        {
            Regex regExp = new Regex("[ \\[ \\] \\^ \\-_*×――(^)$%~!＠@＃#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;/\'\"{}（）‘’“”-]");
            if (regExp.IsMatch(str))
            {
                return true;
            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //开始计时
            this.timer1.Start();
            //CreateXML(XMLfile);
            label33.Visible = true;
            label35.Visible = true;
            string product_Name = txt_productName.Text.ToUpper();
            //塑壳断路器及漏电塑壳断路器

            if (string.IsNullOrEmpty(product_Name))
            {
                System.Windows.Forms.MessageBox.Show("产品名称不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            column.Column_Name = txt_productName.Text;
            columns.Add(column);
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.FileName = txt_productName.Text;
            SaveFile.Filter = "Microsoft Excel 工作表(*.xls)|*.xlsx|所有文件(*.*)|*.*";//EXCEL|*.xlsx|*.xls|EPLAN|*.elk|所有文件类型|*.*
            SaveFile.RestoreDirectory = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                filePath = SaveFile.FileName;
                //txt_productName.Text = System.IO.Path.GetFileName(filePath);
            }
            else
            {
                return;
            }

            CommonButton2ClickData(filePath);
            //ThreadNewButtonClick(filePath);
        }
        private void CommonButton2ClickData(string filePath)
        {
            try
            {
                label36.Visible = false;
                progressBar1.Visible = false;
                startTime = DateTime.Now;
                ///Data is not null
                ///null or is nou null
                /// 
                #region if Data is not null 
                #region 分隔符 

                if (groups == null)
                {
                    groups = new List<string> { };
                }
                if (!string.IsNullOrEmpty(txt_groupFH_0.Text))
                {
                    groups.Add(txt_groupFH_0.Text);
                }
                else
                {
                    groups.Add(txt_groupFH_0.Text);
                    // txt_groupFH1.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH_1.Text))
                {
                    groups.Add(txt_groupFH_1.Text);
                }
                else
                {
                    groups.Add(txt_groupFH_1.Text);
                    //txt_groupFH1.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH1.Text))
                {
                    groups.Add(txt_groupFH1.Text);
                }
                else
                {
                    groups.Add(txt_groupFH1.Text);
                    //txt_groupFH1.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH2.Text))
                {
                    groups.Add(txt_groupFH2.Text);
                }
                else
                {
                    groups.Add(txt_groupFH2.Text);
                    //txt_groupFH2.Visible = false;
                }

                if (!string.IsNullOrEmpty(txt_groupFH3.Text))
                {
                    groups.Add(txt_groupFH3.Text);
                }
                else
                {
                    groups.Add(txt_groupFH3.Text);
                    //txt_groupFH3.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH4.Text))
                {
                    groups.Add(txt_groupFH4.Text);
                }
                else
                {
                    groups.Add(txt_groupFH4.Text);
                    //txt_groupFH4.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH5.Text))
                {
                    groups.Add(txt_groupFH5.Text);
                }
                else
                {
                    groups.Add(txt_groupFH5.Text);
                    //txt_groupFH5.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH6.Text))
                {
                    groups.Add(txt_groupFH6.Text);
                }
                else
                {
                    groups.Add(txt_groupFH6.Text);
                    //txt_groupFH6.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH7.Text))
                {
                    groups.Add(txt_groupFH7.Text);
                }
                else
                {
                    groups.Add(txt_groupFH7.Text);
                    //txt_groupFH7.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH8.Text))
                {
                    groups.Add(txt_groupFH8.Text);
                }
                else
                {
                    groups.Add(txt_groupFH8.Text);
                    //txt_groupFH8.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH9.Text))
                {
                    groups.Add(txt_groupFH9.Text);
                }
                else
                {
                    groups.Add(txt_groupFH9.Text);
                    // txt_groupFH9.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH10.Text))
                {
                    groups.Add(txt_groupFH10.Text);
                }
                else
                {
                    groups.Add(txt_groupFH10.Text);
                    //  txt_groupFH10.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH11.Text))
                {
                    groups.Add(txt_groupFH11.Text);
                }
                else
                {
                    groups.Add(txt_groupFH11.Text);
                    //txt_groupFH11.Visible = false;
                }
                if (string.IsNullOrEmpty(txt_groupFH12.Text))
                {
                    groups.Add(txt_groupFH12.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH12.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH13.Text))
                {
                    groups.Add(txt_groupFH13.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH13.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH14.Text))
                {
                    groups.Add(txt_groupFH14.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH14.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH15.Text))
                {
                    groups.Add(txt_groupFH15.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH15.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH16.Text))
                {
                    groups.Add(txt_groupFH16.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH16.Text);
                }
                #endregion

                #endregion 
                #region 壳架等级
                string[] KJArr = null;
                if (!string.IsNullOrEmpty(parameter1.Text))
                {
                    string strKj = txt_KeJiaAModel.Text.ToUpper();
                    keyValues = CreateDictionary(strKj);
                    //column.Column_Name = parameter1.Text;
                    column.parameter1 = parameter1.Text;
                    column.strArrString1 = txt_KeJiaAModel.Text.ToUpper().Split('\r');//CreatAllArry(strKj); //strKj;
                    columns.Add(column);
                    /*string[] */
                    KJArr = CreatAllArry(strKj);

                }
                else
                {
                    IsDeleteData = txt_KeJiaAModel.Text = "";
                    string strKj = IsDeleteData;//txt_KeJiaAModel.Text.ToUpper();
                    /* string[] */
                    KJArr = CreatAllArry(strKj);
                }

                #endregion

                #region 级数
                string[] JSArr = null;
                if (!string.IsNullOrEmpty(parameter2.Text))
                {
                    string strJS = txt_JSModel.Text.Trim().ToUpper();
                    keyValues1 = CreateDictionary(strJS);

                    //column.Column_Name = parameter2.Text;
                    column.parameter2 = parameter2.Text;
                    column.strArrString2 = strJS.Split('\r');
                    columns.Add(column);
                    /*string[] */
                    JSArr = CreatAllArry(strJS);
                }
                else
                {
                    IsDeleteData = txt_JSModel.Text = "";
                    string strJS = IsDeleteData;//txt_JSModel.Text.ToUpper();
                    /*string[] */
                    JSArr = CreatAllArry(strJS);
                }

                #endregion

                #region 产品附件
                string[] PMArr = null;
                if (!string.IsNullOrEmpty(parameter3.Text))
                {
                    string strpm = txt_ProductModel.Text.ToUpper();
                    /*  string[] */
                    PMArr = CreatAllArry(strpm);
                    keyValues2 = CreateDictionary(strpm);

                    //  column.Column_Name = parameter3.Text;
                    column.parameter3 = parameter3.Text;
                    column.strArrString3 = strpm.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    string strpm = txt_ProductModel.Text.ToUpper();
                    /*  string[] */
                    PMArr = CreatAllArry(strpm);
                }


                #endregion

                #region 四级类型
                string[] FTArr = null;
                if (!string.IsNullOrEmpty(parameter4.Text))
                {
                    string strFT = txt_FourJTypeModel.Text.ToUpper();
                    /* string[] */
                    FTArr = CreatAllArry(strFT);
                    keyValues3 = CreateDictionary(strFT);

                    //  column.Column_Name = parameter4.Text;
                    column.parameter4 = parameter4.Text;
                    column.strArrString4 = strFT.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_FourJTypeModel.Text = "";
                    string strFT = IsDeleteData;// txt_FourJTypeModel.Text.ToUpper();
                    FTArr = CreatAllArry(strFT);
                }

                #endregion

                #region 扩展方式
                string[] KZArr = null;
                if (!string.IsNullOrEmpty(parameter5.Text))
                {
                    string strkz = txt_KuoZhanFangshiModel.Text.ToUpper();
                    /*string[] */
                    KZArr = CreatAllArry(strkz);
                    keyValues4 = CreateDictionary(strkz);

                    // column.Column_Name = parameter5.Text;
                    column.parameter5 = parameter5.Text;
                    column.strArrString5 = strkz.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_KuoZhanFangshiModel.Text = "";
                    string strkz = IsDeleteData;// txt_KuoZhanFangshiModel.Text.ToUpper();
                    KZArr = CreatAllArry(strkz);
                }


                #endregion

                #region 安装方式
                string[] AZArr = null;
                if (!string.IsNullOrEmpty(parameter6.Text))
                {
                    string strAZ = txt_AnZhuangFangshiModel.Text.ToUpper();
                    /*string[] */
                    AZArr = CreatAllArry(strAZ);
                    keyValues5 = CreateDictionary(strAZ);

                    //column.Column_Name = parameter6.Text;
                    column.parameter6 = parameter6.Text;
                    column.strArrString6 = strAZ.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_AnZhuangFangshiModel.Text = "";
                    string strAZ = IsDeleteData;// txt_AnZhuangFangshiModel.Text.ToUpper();
                    AZArr = CreatAllArry(strAZ);
                }

                #endregion

                #region 供货方式
                string[] GHFSArr = null;
                if (!string.IsNullOrEmpty(parameter7.Text))
                {
                    string strGHFS = txt_GongHuoFangshiModel.Text.ToUpper();
                    /* string[]*/
                    GHFSArr = CreatAllArry(strGHFS);
                    keyValues6 = CreateDictionary(strGHFS);

                    // column.Column_Name = parameter7.Text;
                    column.parameter7 = parameter7.Text;
                    column.strArrString7 = strGHFS.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_GongHuoFangshiModel.Text = "";
                    string strGHFS = IsDeleteData;// txt_GongHuoFangshiModel.Text.ToUpper();
                    /* string[]*/
                    GHFSArr = CreatAllArry(strGHFS);
                }

                #endregion

                #region 分断能力
                string[] FDNLArr = null;
                if (!string.IsNullOrEmpty(parameter8.Text))
                {
                    string strFdnl = txt_FenDuanNengLiModel.Text.ToUpper();
                    /* string[] */
                    FDNLArr = CreatAllArry(strFdnl);
                    keyValues7 = CreateDictionary(strFdnl);

                    //column.Column_Name = parameter8.Text;
                    column.parameter8 = parameter8.Text;
                    column.strArrString8 = strFdnl.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_FenDuanNengLiModel.Text = "";
                    string strFdnl = IsDeleteData;// txt_FenDuanNengLiModel.Text.ToUpper();
                    FDNLArr = CreatAllArry(strFdnl);
                }

                #endregion

                #region 脱扣方式
                string[] TgfsArr = null;
                if (!string.IsNullOrEmpty(parameter9.Text))
                {
                    string strTgfs = txt_TuoGouFangshiModel.Text.ToUpper();
                    /* string[]*/
                    TgfsArr = CreatAllArry(strTgfs);
                    keyValues8 = CreateDictionary(strTgfs);

                    //column.Column_Name = parameter9.Text;
                    column.parameter9 = parameter9.Text;
                    column.strArrString9 = strTgfs.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_TuoGouFangshiModel.Text = "";
                    string strTgfs = IsDeleteData; txt_TuoGouFangshiModel.Text.ToUpper();
                    TgfsArr = CreatAllArry(strTgfs);
                }

                #endregion

                #region 保护类型
                string[] BHFSArr = null;
                if (!string.IsNullOrEmpty(parameter10.Text))
                {
                    string strBHFS = txt_BaoHuTypeModel.Text.ToUpper();
                    /* string[] */
                    BHFSArr = CreatAllArry(strBHFS);
                    keyValues9 = CreateDictionary(strBHFS);

                    //   column.Column_Name = parameter10.Text;
                    column.parameter10 = parameter10.Text;
                    column.strArrString10 = strBHFS.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_BaoHuTypeModel.Text = "";
                    string strBHFS = IsDeleteData;// txt_BaoHuTypeModel.Text.ToUpper();
                    BHFSArr = CreatAllArry(strBHFS);
                }

                #endregion

                #region 操作方式
                string[] CZArr = null;
                if (!string.IsNullOrEmpty(parameter11.Text))
                {
                    string strCZ = txt_CaoZuoFangshiModel.Text.ToUpper();
                    /*string[] */
                    CZArr = CreatAllArry(strCZ);
                    keyValues10 = CreateDictionary(strCZ);

                    //  column.Column_Name = parameter11.Text;
                    column.parameter11 = parameter11.Text;
                    column.strArrString11 = strCZ.Split('\r');
                    columns.Add(column);
                }
                else
                {
                    IsDeleteData = txt_CaoZuoFangshiModel.Text = "";
                    string strCZ = IsDeleteData;// txt_CaoZuoFangshiModel.Text.ToUpper();
                    CZArr = CreatAllArry(strCZ);
                }

                #endregion

                #region 剩余电流
                string[] SAArr = null;
                if (!string.IsNullOrEmpty(parameter12.Text))
                {
                    string strSA = txt_ShengyuAModel.Text.ToUpper();
                    /* string[] */
                    SAArr = CreatAllArry(strSA);
                    keyValues11 = CreateDictionary(strSA);

                    //  column.Column_Name = parameter12.Text;
                    column.parameter12 = parameter12.Text;
                    column.strArrString12 = strSA.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_ShengyuAModel.Text = "";
                    string strSA = IsDeleteData;// txt_ShengyuAModel.Text.ToUpper();
                    SAArr = CreatAllArry(strSA);
                }

                #endregion

                #region 额定电流
                string[] EDAArr = null;
                if (!string.IsNullOrEmpty(parameter13.Text))
                {
                    string strEDA = txt_EDingAModel.Text.ToUpper();
                    /*  string[]*/
                    EDAArr = CreatAllArry(strEDA);
                    keyValues12 = CreateDictionary(strEDA);

                    //  column.Column_Name = parameter13.Text;
                    column.parameter13 = parameter13.Text;
                    column.strArrString13 = strEDA.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_EDingAModel.Text = "";
                    string strEDA = IsDeleteData;// txt_EDingAModel.Text.ToUpper();
                    EDAArr = CreatAllArry(strEDA);
                }

                #endregion

                #region 延时时间
                string[] YSTArr = null;
                if (!string.IsNullOrEmpty(parameter14.Text))
                {
                    string strYST = txt_YanShiTimeModel.Text.ToUpper();
                    /* string[] */
                    YSTArr = CreatAllArry(strYST);
                    keyValues13 = CreateDictionary(strYST);

                    //  column.Column_Name = parameter14.Text;
                    column.parameter14 = parameter14.Text;
                    column.strArrString14 = strYST.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_YanShiTimeModel.Text = "";
                    string strYST = IsDeleteData;// txt_YanShiTimeModel.Text.ToUpper();
                    YSTArr = CreatAllArry(strYST);
                }

                #endregion

                #region +1
                string[] Arr_1 = null;
                if (!string.IsNullOrEmpty(parameter15.Text))
                {
                    string strArr_1 = txt_Arr_1.Text.ToUpper();
                    /* string[]*/
                    Arr_1 = CreatAllArry(strArr_1);
                    keyValues14 = CreateDictionary(strArr_1);

                    // column.Column_Name = parameter15.Text;
                    column.parameter15 = parameter15.Text;
                    column.strArrString15 = strArr_1.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_Arr_1.Text = "";
                    string strArr_1 = IsDeleteData;// txt_Arr_1.Text.ToUpper();
                    /* string[]*/
                    Arr_1 = CreatAllArry(strArr_1);
                }
                #endregion
                #region +2
                string[] Arr_2 = null;
                if (!string.IsNullOrEmpty(parameter16.Text))
                {
                    string strArr_2 = txt_Arr_2.Text.ToUpper();
                    /* string[]*/
                    Arr_2 = CreatAllArry(strArr_2);
                    keyValues15 = CreateDictionary(strArr_2);

                    //  column.Column_Name = parameter16.Text;
                    column.parameter16 = parameter16.Text;
                    column.strArrString16 = strArr_2.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_Arr_2.Text = "";
                    string strArr_2 = IsDeleteData;// txt_Arr_2.Text.ToUpper();
                    /* string[]*/
                    Arr_2 = CreatAllArry(strArr_2);
                }
                #endregion
                #region +3
                string[] Arr_3 = null;
                if (!string.IsNullOrEmpty(parameter17.Text))
                {
                    string strArr_3 = txt_Arr_3.Text.ToUpper();
                    /* string[]*/
                    Arr_3 = CreatAllArry(strArr_3);
                    keyValues16 = CreateDictionary(strArr_3);


                    column.parameter17 = parameter17.Text;
                    column.strArrString17 = strArr_3.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_Arr_3.Text = "";
                    string strArr_3 = IsDeleteData;// txt_Arr_3.Text.ToUpper();
                    /* string[]*/
                    Arr_3 = CreatAllArry(strArr_3);
                }
                #endregion
                #region +4

                string[] Arr_4 = null;
                if (!string.IsNullOrEmpty(parameter18.Text))
                {
                    string strArr_4 = txt_Arr_4.Text.ToUpper();
                    /* string[]*/
                    Arr_4 = CreatAllArry(strArr_4);
                    keyValues17 = CreateDictionary(strArr_4);
                    column.parameter18 = parameter18.Text;
                    column.strArrString18 = strArr_4.Split('\r');
                    columns.Add(column);
                }
                else
                {
                    IsDeleteData = txt_Arr_3.Text = "";
                    string strArr_4 = IsDeleteData;// txt_Arr_4.Text.ToUpper();
                    /* string[]*/
                    Arr_4 = CreatAllArry(strArr_4);
                }
                #endregion
                arrModels.Add(new PrameterModel { Arrparam_Name = txt_productName.Text, ArrString_List = "" });
                arrModels.Add(new PrameterModel { Arrparam_Name = "描述", ArrString_List = "" });
                #region 技术参数名称    

                if (parameter1.Text != "" && txt_KeJiaAModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter1.Text, ArrString_List = txt_KeJiaAModel.Text });
                }

                if (parameter2.Text != "" && txt_JSModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter2.Text, ArrString_List = txt_JSModel.Text });
                }

                if (parameter3.Text != "" && txt_ProductModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter3.Text, ArrString_List = txt_ProductModel.Text });
                }

                if (parameter4.Text != "" && txt_FourJTypeModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter4.Text, ArrString_List = txt_FourJTypeModel.Text });
                }

                if (parameter5.Text != "" && txt_KuoZhanFangshiModel.Text != "")
                {

                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter5.Text, ArrString_List = txt_KuoZhanFangshiModel.Text });
                }
                if (parameter6.Text != "" && txt_AnZhuangFangshiModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter6.Text, ArrString_List = txt_AnZhuangFangshiModel.Text });
                }

                if (parameter7.Text != "" && txt_GongHuoFangshiModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter7.Text, ArrString_List = txt_GongHuoFangshiModel.Text });
                }

                if (parameter8.Text != "" && txt_FenDuanNengLiModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter8.Text, ArrString_List = txt_FenDuanNengLiModel.Text });
                }

                if (parameter9.Text != "" && txt_TuoGouFangshiModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter9.Text, ArrString_List = txt_TuoGouFangshiModel.Text });
                }

                if (parameter10.Text != "" && txt_BaoHuTypeModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter10.Text, ArrString_List = txt_BaoHuTypeModel.Text });
                }

                if (parameter11.Text != "" && txt_CaoZuoFangshiModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter11.Text, ArrString_List = txt_CaoZuoFangshiModel.Text });
                }

                if (parameter12.Text != "" && txt_ShengyuAModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter12.Text, ArrString_List = txt_ShengyuAModel.Text });
                }

                if (parameter13.Text != "" && txt_EDingAModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter13.Text, ArrString_List = txt_EDingAModel.Text });
                }

                if (parameter14.Text != "" && txt_YanShiTimeModel.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter14.Text, ArrString_List = txt_YanShiTimeModel.Text });
                }

                if (parameter15.Text != "" && txt_Arr_1.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter15.Text, ArrString_List = txt_Arr_1.Text });
                }

                if (parameter16.Text != "" && txt_Arr_2.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter16.Text, ArrString_List = txt_Arr_2.Text });
                }

                if (parameter17.Text != "" && txt_Arr_3.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter17.Text, ArrString_List = txt_Arr_3.Text });
                }

                if (parameter18.Text != "" && txt_Arr_4.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter18.Text, ArrString_List = txt_Arr_4.Text });
                }
                #endregion

                ThreadNewButtonClick(filePath, KJArr, JSArr, PMArr, FTArr, KZArr, AZArr, GHFSArr, FDNLArr, TgfsArr, BHFSArr, CZArr, SAArr, EDAArr, YSTArr, Arr_1, Arr_2, Arr_3, Arr_4);


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
            }
        }

        private void ThreadNewButtonClick(string filePath, String[] KJArr, String[] JSArr, String[] PMArr, String[] FTArr, String[] KZArr, String[] AZArr, String[] GHFSArr, String[] FDNLArr, String[] TgfsArr, String[] BHFSArr, String[] CZArr, String[] SAArr, String[] EDAArr, String[] YSTArr, String[] Arr_1, String[] Arr_2, String[] Arr_3, String[] Arr_4)
        {
            try
            {
                // string[] arrParam = new string[] { txt_productName.Text, parameter1.Text, parameter2.Text, parameter3.Text, parameter4.Text, parameter5.Text, parameter6.Text, parameter7.Text, parameter8.Text, parameter9.Text, parameter10.Text, parameter11.Text, parameter12.Text, parameter13.Text, parameter14.Text };
                n = KJArr.Length * JSArr.Length * PMArr.Length * FTArr.Length * KZArr.Length * AZArr.Length * GHFSArr.Length * FDNLArr.Length * TgfsArr.Length * BHFSArr.Length * CZArr.Length * SAArr.Length * EDAArr.Length * YSTArr.Length * Arr_1.Length * Arr_2.Length * Arr_3.Length * Arr_4.Length;
                int m = 0;
                String[] str4 = new String[n];
                for (int i = 0; i < KJArr.Length; i++)
                {
                    for (int k = 0; k < JSArr.Length; k++)
                    {
                        for (int l = 0; l < PMArr.Length; l++)
                        {

                            for (int ft = 0; ft < FTArr.Length; ft++)
                            {
                                for (int kz = 0; kz < KZArr.Length; kz++)
                                {
                                    for (int az = 0; az < AZArr.Length; az++)
                                    {
                                        for (int gh = 0; gh < GHFSArr.Length; gh++)
                                        {
                                            for (int fdn = 0; fdn < FDNLArr.Length; fdn++)
                                            {
                                                for (int tg = 0; tg < TgfsArr.Length; tg++)
                                                {
                                                    for (int bh = 0; bh < BHFSArr.Length; bh++)
                                                    {
                                                        for (int cz = 0; cz < CZArr.Length; cz++)
                                                        {
                                                            for (int sa = 0; sa < SAArr.Length; sa++)
                                                            {
                                                                for (int ed = 0; ed < EDAArr.Length; ed++)
                                                                {
                                                                    for (int ts = 0; ts < YSTArr.Length; ts++)
                                                                    {
                                                                        for (int a_1 = 0; a_1 < Arr_1.Length; a_1++)
                                                                        {
                                                                            for (int a_2 = 0; a_2 < Arr_1.Length; a_2++)
                                                                            {
                                                                                for (int a_3 = 0; a_3 < Arr_1.Length; a_3++)
                                                                                {
                                                                                    for (int a_4 = 0; a_4 < Arr_1.Length; a_4++)
                                                                                    {
                                                                                        //FTArr[]+ KZArr[]+ AZArr[]+GHFSArr[]+FDNLArr[]+TgfsArr[]+ BHFSArr[]+ CZArr[]+SAArr[]+EDAArr[]+YSTArr[]
                                                                                        str4[m] = KJArr[i] + JSArr[k] + PMArr[l] + FTArr[ft] + KZArr[kz] + AZArr[az] + GHFSArr[gh] + FDNLArr[fdn] + TgfsArr[tg] + BHFSArr[bh] + CZArr[cz] + SAArr[sa] + EDAArr[ed] + YSTArr[ts] + Arr_1[a_1] + Arr_2[a_2] + Arr_3[a_3] + Arr_4[a_4];
                                                                                        m++;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //,FTArr,KZArr,AZArr, GHFSArr,FDNLArr,TgfsArr,BHFSArr,CZArr,SAArr,EDAAr,YSTArr.Length;  
                OutPutResult(filePath, n, KJArr, JSArr, PMArr, FTArr, KZArr, AZArr, GHFSArr, FDNLArr, TgfsArr, BHFSArr, CZArr, SAArr, EDAArr, YSTArr, Arr_1, Arr_2, Arr_3, Arr_4, str4);

            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show("错误原因：" + err.Message, "提示信息",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 读xml数据
        /// </summary>
        private void ReadDataXml(string dataXmlPath)
        {
            string strArr_1 = "";
            string strArr_2 = "";
            string strArr_3 = "";
            string strArr_4 = "";
            string strArr_5 = "";
            string strArr_6 = "";
            string strArr_7 = "";
            string strArr_8 = "";
            string strArr_9 = "";
            string strArr_10 = "";
            string strArr_11 = "";
            string strArr_12 = "";
            string strArr_13 = "";
            string strArr_14 = "";
            string strArr_15 = "";
            string strArr_16 = "";
            string strArr_17 = "";
            string strArr_18 = "";
            //   string dataXmlPath = subpath + "" + txt_productName.Text.ToUpper() + ".xml";
            if (dataXmlPath != null)
            {

                using (FileStream fs = new FileStream(dataXmlPath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ColumnModel));
                    ColumnModel cm = (ColumnModel)serializer.Deserialize(fs);
                    //txt_productName.Text = cm.Column_Name;
                    parameter1.Text = cm.parameter1;
                    parameter2.Text = cm.parameter2;
                    parameter3.Text = cm.parameter3;
                    parameter4.Text = cm.parameter4;
                    parameter5.Text = cm.parameter5;
                    parameter6.Text = cm.parameter6;
                    parameter7.Text = cm.parameter7;
                    parameter8.Text = cm.parameter8;
                    parameter9.Text = cm.parameter9;
                    parameter10.Text = cm.parameter10;
                    parameter11.Text = cm.parameter11;
                    parameter12.Text = cm.parameter12;
                    parameter13.Text = cm.parameter13;
                    parameter14.Text = cm.parameter14;
                    parameter15.Text = cm.parameter15;
                    parameter16.Text = cm.parameter16;
                    parameter17.Text = cm.parameter17;
                    parameter18.Text = cm.parameter18;
                    txt_KeJiaAModel.Text = GetXmlText(cm.strArrString1, strArr_1, txt_KeJiaAModel);
                    #region TEST


                    //if (cm.strArrString1.Length > 0)
                    //{
                    //    for (int i = 0; i < cm.strArrString1.Length; i++)
                    //    {
                    //        strArr_1 += '\n' + cm.strArrString1[i] + '\r';
                    //    }
                    //    txt_KeJiaAModel.Text = strArr_1;
                    //}
                    //else
                    //{
                    //    txt_KeJiaAModel.Text = strArr_1;
                    //}
                    //txt_JSModel.Text = cm.strArrString2;
                    //if (cm.strArrString2.Length > 0)
                    //{
                    //    for (int i = 0; i < cm.strArrString2.Length; i++)
                    //    {
                    //        strArr_2 += '\n' + cm.strArrString2[i] + '\r';
                    //    }
                    //    txt_JSModel.Text = strArr_2;
                    //}
                    //else
                    //{
                    //    txt_JSModel.Text = strArr_2;
                    //}
                    #endregion
                    txt_JSModel.Text = GetXmlText(cm.strArrString2, strArr_2, txt_JSModel);
                    txt_ProductModel.Text = GetXmlText(cm.strArrString3, strArr_3, txt_ProductModel); //cm.strArrString3;
                    txt_FourJTypeModel.Text = GetXmlText(cm.strArrString4, strArr_4, txt_FourJTypeModel); // cm.strArrString4;
                    txt_KuoZhanFangshiModel.Text = GetXmlText(cm.strArrString5, strArr_5, txt_KuoZhanFangshiModel); // cm.strArrString5;
                    txt_AnZhuangFangshiModel.Text = GetXmlText(cm.strArrString6, strArr_6, txt_AnZhuangFangshiModel); // cm.strArrString6;
                    txt_GongHuoFangshiModel.Text = GetXmlText(cm.strArrString7, strArr_7, txt_GongHuoFangshiModel); // cm.strArrString7;
                    txt_FenDuanNengLiModel.Text = GetXmlText(cm.strArrString8, strArr_8, txt_FenDuanNengLiModel); // cm.strArrString8;
                    txt_TuoGouFangshiModel.Text = GetXmlText(cm.strArrString9, strArr_9, txt_TuoGouFangshiModel); // cm.strArrString9;
                    txt_BaoHuTypeModel.Text = GetXmlText(cm.strArrString10, strArr_10, txt_BaoHuTypeModel); //cm.strArrString10;
                    txt_CaoZuoFangshiModel.Text = GetXmlText(cm.strArrString11, strArr_11, txt_CaoZuoFangshiModel); // cm.strArrString11;
                    txt_ShengyuAModel.Text = GetXmlText(cm.strArrString12, strArr_12, txt_ShengyuAModel); // cm.strArrString12;
                    txt_EDingAModel.Text = GetXmlText(cm.strArrString13, strArr_13, txt_EDingAModel); // cm.strArrString13;
                    txt_YanShiTimeModel.Text = GetXmlText(cm.strArrString14, strArr_14, txt_YanShiTimeModel); // cm.strArrString14;
                    txt_Arr_1.Text = GetXmlText(cm.strArrString15, strArr_15, txt_Arr_1); // cm.strArrString15;
                    txt_Arr_2.Text = GetXmlText(cm.strArrString16, strArr_16, txt_Arr_2); // cm.strArrString16;
                    txt_Arr_3.Text = GetXmlText(cm.strArrString17, strArr_17, txt_Arr_3); // cm.strArrString17;
                    txt_Arr_4.Text = GetXmlText(cm.strArrString18, strArr_18, txt_Arr_4); // cm.strArrString18;
                }

            }
            try
            {
            }
            catch
            {
                //add some code here
            }
        }
        private String GetXmlText(string[] strArr, string strString, System.Windows.Forms.TextBox textBox)
        {
            if (strArr == null)
            {
                strArr = new string[] { };
                textBox.Text = strString;
            }
            if (strArr.Length > 0 && strArr != null)
            {
                for (int i = 0; i < strArr.Length; i++)
                {
                    strString += '\n' + strArr[i] + '\r';
                }
                textBox.Text = strString;
            }
            else
            {
                textBox.Text = strString;
            }
            return textBox.Text;
        }
        private String[] CreatAllArry(string strArr)
        {
            string[] OmeArr = new string[] { };
            string[] NewArr = new string[] { };
            string[] arrAll = strArr.Trim().Split('\r');//将文本框的内容按回车进行分组  
            string strBarcodeList = "";//设置一个字符串接受分割开的每一个字符 
            for (int v = 0; v < arrAll.Length; v++)
            {
                strBarcodeList += arrAll[v].Replace("\n", "") + ",";//将分隔开的字符串进行重新组装中间加,逗号
            }
            if (strBarcodeList.Length > 0)
                strBarcodeList = strBarcodeList.Remove(strBarcodeList.Length - 1);//去除字符串最后的逗号 
            /*string[] */
            OmeArr = strBarcodeList.Split(',');
            string strOmeBeforeList = "";
            for (int i = 0; i < OmeArr.Length; i++)
            {
                if (OmeArr[i].IndexOf('&') > 0)
                {
                    //System.Windows.Forms.MessageBox.Show(OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')));//截取&前面的值

                    if (OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')) != null && OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString() != null)
                    {
                        strOmeBeforeList += OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')) + ',';
                    }
                }
            }
            if (strOmeBeforeList.Length > 0)
            {
                strOmeBeforeList = strOmeBeforeList.Remove(strOmeBeforeList.Length - 1);//去除字符串最后的逗号 
            }
            NewArr = strOmeBeforeList.Split(',');
            return NewArr;

        }
        private Dictionary<string, string> CreateDictionary(string arrString)
        {
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
                        newKeyValuesList.Add(OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')), OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString());
                    }
                }
            }
            return newKeyValuesList;
        }
        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="n">所有数组共有的可能的情况</param>
        /// <param name="arr1">数组1</param>
        /// <param name="arr2">数组2</param>
        /// <param name="str4">数组1和数组2组成的n个数量的新数组</param>
        public void OutPutResult(string filePath, int n, string[] arr1, string[] arr2, string[] arr3, string[] arr6, string[] arr7, string[] arr8, string[] arr9, string[] arr10, string[] arr11, string[] arr12, string[] arr13, string[] arr14, string[] arr15, string[] arr16, string[] Arr_1, string[] Arr_2, string[] Arr_3, string[] Arr_4, String[] str4)
        {
            //arr6,arr7,arr8,arr9,arr10,arr11, arr12,arr13,arr14,arr15, arr16,

            String[] str5 = fun(filePath, n, arr1, arr2, arr3, arr6, arr7, arr8, arr9, arr10, arr11, arr12, arr13, arr14, arr15, arr16, Arr_1, Arr_2, Arr_3, Arr_4, str4);

            // long totalCount = str5.Count();

            // System.Windows.Forms.MessageBox.Show($"共有组合:" + totalCount.ToString()+"*****{n}="+n);
        }
        /// <summary>
        /// 递归实现
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        String[] fun(string filePath, int n, String[] str1, String[] str2, String[] arr3, string[] arr6, string[] arr7, string[] arr8, string[] arr9, string[] arr10, string[] arr11, string[] arr12, string[] arr13, string[] arr14, string[] arr15, string[] arr16, string[] Arr_1, string[] Arr_2, string[] Arr_3, string[] Arr_4, String[] str4)
        {

            try
            {
                /*  Microsoft.Office.Interop.Excel.Application */
                excel = new Microsoft.Office.Interop.Excel.Application(); //创建Excel对象
                /*Microsoft.Office.Interop.Excel.Workbook */
                wBook = excel.Application.Workbooks.Add(Missing.Value); //创建新的Excel工作簿 

                excel.Visible = false; //使Excel不可视 

                //设置禁止弹出保存和覆盖的询问提示框
                excel.DisplayAlerts = false;
                excel.AlertBeforeOverwriting = true;
                workSheet = (Microsoft.Office.Interop.Excel._Worksheet)wBook.ActiveSheet;

                //保存
                workSheet.Name = txt_productName.Text.ToUpper();

                long rowRead = 0;
                decimal percent = 0;
                int m = 0;
                /*String[] */
                str4 = new String[n];
                int rowIndex = 1;
                int colIndex = 2;
                StringBuilder sb = new StringBuilder();
                MEMORY_INFO MemInfo;
                MemInfo = new MEMORY_INFO();
                excel.Cells[1, 1] = txt_productName.Text.ToUpper();
                excel.Cells[1, 2] = "描述";
                for (int i = 1; i <= arrModels.Count - 2; i++)
                {
                    colIndex++;
                    excel.Cells[1, colIndex] = "技术参数" + i;
                }

                //壳架等级
                for (int i = 0; i < str1.Length; i++)
                {
                    //级数
                    for (int k = 0; k < str2.Length; k++)
                    {
                        //产品附件
                        for (int l = 0; l < arr3.Length; l++)
                        {
                            for (int ft = 0; ft < arr6.Length; ft++)
                            {
                                for (int kz = 0; kz < arr7.Length; kz++)
                                {
                                    for (int az = 0; az < arr8.Length; az++)
                                    {
                                        for (int gh = 0; gh < arr9.Length; gh++)
                                        {
                                            for (int fdn = 0; fdn < arr10.Length; fdn++)
                                            {
                                                for (int tg = 0; tg < arr11.Length; tg++)
                                                {
                                                    for (int bh = 0; bh < arr12.Length; bh++)
                                                    {
                                                        for (int cz = 0; cz < arr13.Length; cz++)
                                                        {
                                                            for (int sa = 0; sa < arr14.Length; sa++)
                                                            {
                                                                for (int ed = 0; ed < arr15.Length; ed++)
                                                                {
                                                                    for (int ts = 0; ts < arr16.Length; ts++)
                                                                    {
                                                                        for (int a_1 = 0; a_1 < Arr_1.Length; a_1++)
                                                                        {
                                                                            for (int a_2 = 0; a_2 < Arr_2.Length; a_2++)
                                                                            {
                                                                                for (int a_3 = 0; a_3 < Arr_3.Length; a_3++)
                                                                                {
                                                                                    for (int a_4 = 0; a_4 < Arr_4.Length; a_4++)
                                                                                    {
                                                                                        rowIndex++;
                                                                                        #region ceshhi10000


                                                                                        // colIndex=1;
                                                                                        //if (rowIndex == 1000)
                                                                                        //{
                                                                                        //    wBook.SaveAs(filePath/* + workSheet.Name + ".xlsx"*/,
                                                                                        //    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                                                        //    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
                                                                                        //    Missing.Value, Missing.Value);

                                                                                        //    wBook = null;
                                                                                        //    excel.Quit();   //必须关闭，才能有效结束
                                                                                        //    excel = null;
                                                                                        //    label34.Visible = true;
                                                                                        //    label34.Text = (DateTime.Now - startTime).ToString();
                                                                                        //    System.Windows.Forms.MessageBox.Show("导出数据成功!", "系统信息");
                                                                                        //    this.Hide(); //先隐藏主窗体 

                                                                                        //    return str4;

                                                                                        //}
                                                                                        //else
                                                                                        //{
                                                                                        #endregion       
                                                                                        str4[m] = txt_productName.Text + groups[0] + (str1[i].Equals(0) ? "" : str1[i]) + groups[1] + (str2[k].Equals(0) ? "" : str2[k]) + groups[2] + (arr3[l].Equals(0) ? "" : arr3[l]) + groups[3] + (arr6[ft].Equals(0) ? "" : arr6[ft]) + groups[4] + (arr7[kz].Equals(0) ? "" : arr7[kz]) + groups[5] + (arr8[az].Equals(0) ? "" : arr8[az]) + groups[6] + (arr9[gh].Equals(0) ? "" : arr9[gh]) + groups[7] + (arr10[fdn].Equals(0) ? "" : arr10[fdn]) + groups[8] + (arr11[tg].Equals(0) ? "" : arr11[tg]) + groups[9] + (arr12[bh].Equals(0) ? "" : arr12[bh]) + groups[10] + (arr13[cz].Equals(0) ? "" : arr13[cz]) + groups[11] + (arr14[sa].Equals(0) ? "" : arr14[sa]) + groups[12] + (arr15[ed].Equals(0) ? "" : arr15[ed]) + groups[13] + (arr16[ts].Equals(0) ? "" : arr16[ts]) + groups[14] + (Arr_1[a_1].Equals(0) ? "" : Arr_1[a_1]) + groups[15] + (Arr_2[a_2].Equals(0) ? "" : Arr_2[a_2]) + groups[16] + (Arr_3[a_3].Equals(0) ? "" : Arr_3[a_3]) + groups[17] + (Arr_4[a_4].Equals(0) ? "" : Arr_4[a_4]);

                                                                                        excel.Cells[rowIndex, 1] = str4[m];

                                                                                        if (chk_1.Checked == true)
                                                                                        {
                                                                                            if (keyValues != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvp in keyValues)
                                                                                                {
                                                                                                    if (kvp.Key.Equals(str1[i]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 3] = parameter1.Text + ":" + kvp.Value;

                                                                                                        sb.Append(parameter1.Text + ":" + kvp.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                        //excel.Cells[rowIndex, 2] = "";
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 3] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvp in keyValues)
                                                                                                {
                                                                                                    if (kvp.Key.Equals(str1[i]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 3] = parameter1.Text + ":" + kvp.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 3] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_2.Checked == true)
                                                                                        {
                                                                                            if (keyValues1 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvpk in keyValues1)
                                                                                                {
                                                                                                    if (kvpk.Key.Equals(str2[k]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 4] = parameter2.Text + ":" + kvpk.Value/*str1[i]*/;
                                                                                                        sb.Append(parameter2.Text + ":" + kvpk.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 4] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues1 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvpk in keyValues1)
                                                                                                {
                                                                                                    if (kvpk.Key.Equals(str2[k]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 4] = parameter2.Text + ":" + kvpk.Value/*str1[i]*/;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 4] = "";
                                                                                            }
                                                                                        }


                                                                                        #region MyRegion 
                                                                                        if (chk_3.Checked == true)
                                                                                        {
                                                                                            if (keyValues2 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvpk in keyValues2)
                                                                                                {
                                                                                                    if (kvpk.Key.Equals(arr3[l]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 5] = parameter3.Text + ":" + kvpk.Value/*str1[i]*/;
                                                                                                        sb.Append(parameter3.Text + ":" + kvpk.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 5] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues2 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvpk in keyValues2)
                                                                                                {
                                                                                                    if (kvpk.Key.Equals(arr3[l]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 5] = parameter3.Text + ":" + kvpk.Value/*str1[i]*/;

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 5] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_4.Checked == true)
                                                                                        {


                                                                                            if (keyValues3 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues3)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr6[ft]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 6] = parameter4.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter4.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 6] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues3 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues3)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr6[ft]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 6] = parameter4.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 6] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_5.Checked == true)
                                                                                        {


                                                                                            if (keyValues4 != null)
                                                                                            {
                                                                                                //keyValues4

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues4)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr7[kz]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 7] = parameter5.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter5.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 7] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues4 != null)
                                                                                            {
                                                                                                //keyValues4

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues4)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr7[kz]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 7] = parameter5.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 7] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_6.Checked == true)
                                                                                        {


                                                                                            if (keyValues5 != null)
                                                                                            {
                                                                                                //keyValues5

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues5)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr8[az]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 8] = parameter6.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter6.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 8] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues5 != null)
                                                                                            {
                                                                                                //keyValues5

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues5)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr8[az]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 8] = parameter6.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 8] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_7.Checked == true)
                                                                                        {

                                                                                            if (keyValues6 != null)
                                                                                            {
                                                                                                //keyValues6

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues6)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr9[gh]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 9] = parameter7.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter7.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 9] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues6 != null)
                                                                                            {
                                                                                                //keyValues6

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues6)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr9[gh]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 9] = parameter7.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 9] = "";
                                                                                            }
                                                                                        }


                                                                                        if (chk_8.Checked == true)
                                                                                        {


                                                                                            if (keyValues7 != null)
                                                                                            {
                                                                                                //keyValues7
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues7)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr10[fdn]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 10] = parameter8.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter8.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 10] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            if (keyValues7 != null)
                                                                                            {
                                                                                                //keyValues7
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues7)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr10[fdn]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 10] = parameter8.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 10] = "";
                                                                                            }
                                                                                        }
                                                                                        if (chk_9.Checked == true)
                                                                                        {
                                                                                            if (keyValues8 != null)
                                                                                            {
                                                                                                // excel.Cells[rowIndex, 8] = arr10[fdn];

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues8)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr11[tg]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 11] = parameter9.Text + ":" + kvps.Value; ;
                                                                                                        sb.Append(parameter9.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 11] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            if (keyValues8 != null)
                                                                                            {
                                                                                                // excel.Cells[rowIndex, 8] = arr10[fdn];

                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues8)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr11[tg]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 11] = parameter9.Text + ":" + kvps.Value; ;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 11] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_10.Checked == true)
                                                                                        {


                                                                                            if (keyValues9 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues9)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr12[bh]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 12] = parameter10.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter10.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 12] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            if (keyValues9 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues9)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr12[bh]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 12] = parameter10.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 12] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_11.Checked == true)
                                                                                        {


                                                                                            if (keyValues10 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues10)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr13[cz]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 13] = parameter11.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter11.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 13] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            if (keyValues10 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues10)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr13[cz]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 13] = parameter11.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 13] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_12.Checked == true)
                                                                                        {


                                                                                            if (keyValues11 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues11)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr14[sa]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 14] = parameter12.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter12.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 14] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues11 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues11)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr14[sa]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 14] = parameter12.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 14] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_13.Checked == true)
                                                                                        {


                                                                                            if (keyValues12 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues12)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr15[ed]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 15] = parameter13.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter13.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 15] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues12 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues12)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr15[ed]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 15] = parameter13.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 15] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_14.Checked == true)
                                                                                        {


                                                                                            if (keyValues13 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues13)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr16[ts]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 16] = parameter14.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter14.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 16] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            if (keyValues13 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues13)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(arr16[ts]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 16] = parameter14.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 16] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_15.Checked == true)
                                                                                        {


                                                                                            if (keyValues14 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues14)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_1[a_1]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 17] = parameter15.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                        sb.Append(parameter15.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 17] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues14 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues14)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_1[a_1]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 17] = parameter15.Text + ":" + kvps.Value;//arr6[ft];
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 17] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_16.Checked == true)
                                                                                        {


                                                                                            if (keyValues15 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues15)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_2[a_2]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 18] = parameter16.Text + ":" + kvps.Value;
                                                                                                        sb.Append(parameter16.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 18] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues15 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues15)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_2[a_2]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 18] = parameter16.Text + ":" + kvps.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 18] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_17.Checked == true)
                                                                                        {


                                                                                            if (keyValues16 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues16)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_3[a_3]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 19] = parameter17.Text + ":" + kvps.Value;
                                                                                                        sb.Append(parameter17.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 19] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            if (keyValues16 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues16)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_3[a_3]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 19] = parameter17.Text + ":" + kvps.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 19] = "";
                                                                                            }
                                                                                        }

                                                                                        if (chk_18.Checked == true)
                                                                                        {


                                                                                            if (keyValues17 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues17)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_4[a_4]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 20] = parameter18.Text + ":" + kvps.Value;
                                                                                                        sb.Append(parameter18.Text + ":" + kvps.Value + ",");
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 20] = "";
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (keyValues17 != null)
                                                                                            {
                                                                                                foreach (KeyValuePair<string, string> kvps in keyValues17)
                                                                                                {
                                                                                                    if (kvps.Key.Equals(Arr_4[a_4]))
                                                                                                    {
                                                                                                        excel.Cells[rowIndex, 20] = parameter18.Text + ":" + kvps.Value;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        continue;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                excel.Cells[rowIndex, 20] = "";
                                                                                            }
                                                                                        }
                                                                                        if (chk_1.Checked == false && chk_2.Checked == false && chk_3.Checked == false && chk_4.Checked == false && chk_5.Checked == false && chk_6.Checked == false && chk_7.Checked == false && chk_8.Checked == false && chk_9.Checked == false && chk_10.Checked == false && chk_11.Checked == false && chk_12.Checked == false && chk_13.Checked == false && chk_14.Checked == false && chk_15.Checked == false && chk_16.Checked == false && chk_17.Checked == false && chk_18.Checked == false)
                                                                                        {
                                                                                            excel.Cells[rowIndex, 2] = "";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            excel.Cells[rowIndex, 2] = sb.ToString().Trim().Remove(sb.ToString().Trim().Length - 1);
                                                                                            sb.Clear();
                                                                                        }

                                                                                        #endregion

                                                                                        // }

                                                                                        GlobalMemoryStatus(ref MemInfo);
                                                                                        label33.Text = string.Format($"当前是第{rowIndex}行");
                                                                                        label35.Text = MemInfo.dwMemoryLoad.ToString() + "%的内存正在使用,当前时间:" + DateTime.Now.ToLocalTime();
                                                                                        if (label35.Text != "")
                                                                                        {
                                                                                            label35.Text = string.Empty;
                                                                                            label35.Text += MemInfo.dwMemoryLoad.ToString() + "%的内存正在使用,当前时间:" + DateTime.Now.ToLocalTime();
                                                                                        }
                                                                                        percent = (decimal)(((decimal)100 * rowRead) / rowIndex);
                                                                                        this.label36.Text = "正在导出数据[" + percent.ToString("0.00") + "%]...";
                                                                                        progressBar1.Value = Convert.ToInt32(percent);
                                                                                        System.Windows.Forms.Application.DoEvents();
                                                                                        m++;
                                                                                        rowRead++;

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                workSheet.Protect("skycore!");
                //workSheet.Protect("skycore!", SheetProtectionType.None);
                wBook.SaveAs(filePath/* + workSheet.Name + ".xlsx"*/,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);

                wBook = null;
                excel.Quit();   //必须关闭，才能有效结束
                excel = null;
                label34.Visible = true;

                //System.Windows.Forms.MessageBox.Show("导出数据成功!", "系统信息");
                this.timer1.Stop();
                label34.Text = "耗时：" + (DateTime.Now - startTime).ToString();
                //语音提示
                System.Threading.Thread t = new System.Threading.Thread(PlayWarnSoundSuccess);//创建了线程
                t.Start();//开启线程
                MessageBox.Show("EXCEL文件导出成功", "提示", MessageBoxButtons.OK);
                t.Abort();
                groups.Clear();
                arrModels.Clear();
                columns.Clear();
                //主界面刷新
                //this.Hide(); //先隐藏主窗体   
                //Frm_Excel form1 = new Frm_Excel(); //重新实例化此窗体 
                //form1.ShowDialog();//已模式窗体的方法重新打开   
                //this.Close();//原窗体关闭

                return str4;

            }
            catch (Exception err)
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                wBook.SaveAs(filePath/* + workSheet.Name + ".xlsx"*/,
               Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
               Missing.Value, Missing.Value);
                wBook = null;
                excel.Quit();   //必须关闭，才能有效结束
                excel = null;
                groups.Clear();
                arrModels.Clear();
                columns.Clear();
                System.Windows.Forms.MessageBox.Show("错误原因：" + err.Message, "提示信息",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //调用主界面刷新函数
                // Thread thread = new Thread(FrmLoad);
                // Thread.Sleep(1000); 
                // this.Close();//原窗体关闭
                //Form1_Load(null, null);
                groups.Clear();
                arrModels.Clear();
                columns.Clear();
                if (wBook != null)
                    wBook = null;  // WorkBook 的实例欢畅
                if (excel != null)
                    excel.Quit(); // Microsoft.Office.Interop.Excel  的实例对象 
                GC.Collect();  // 回收资源
                System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (var item in excelProcess)
                {
                    item.Kill();
                }
            }

            return str4;
        }
        private static void PlayWarnSoundSuccess(object obj)
        {
            using (SpeechSynthesizer speech = new SpeechSynthesizer())
            {
                speech.Rate = 1;  //语速   
                speech.Volume = 100;  //音量  
                while (true)
                {
                    speech.Speak("EXCEL文件导出成功");

                }
            }

        }
        /// <summary>
        /// 刷新主页面
        /// </summary>
        //private void FrmLoad()
        //{
        //    this.Hide(); //先隐藏主窗体  
        //    Frm_Excel form1 = new Frm_Excel(); //重新实例化此窗体    
        //    form1.ShowDialog();//已模式窗体的方法重新打开
        //}

        private void Form1_Load(object sender, EventArgs e)
        {      
            //设置Timer控件可用
            this.timer1.Enabled = true;
            //设置时间间隔（毫秒为单位）
            this.timer1.Interval = 1000;
            //调用类的初始化方法，记录窗体和其控件的初始位置和大小
            auto.controllInitializeSize(this);
            // TODO: 这行代码将数据加载到表“_ESS_part001_mdbDataSet.tblAccessoryList”中。您可以根据需要移动或删除它
            System.Windows.Forms.MessageBox.Show("“参数”及“参数名称”请依次根据“参数编号”输入\n【例:参数1代表参数编号】");

            label36.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            label35.Visible = false;
            progressBar1.Visible = false;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label58.Visible = false;
            label60.Visible = false;
            label62.Visible = false;
            label64.Visible = false;

            //MyTimer.Interval = 100;  




        }

        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref SYSTEMTIME_INFO stinfo);
        /// <summary>
        /// 界面卡动
        /// </summary>


        //定义内存的信息结构    
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }

        //定义系统时间的信息结构    
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME_INFO
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }


        /// <summary>
        /// 触发，点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region 触发，点击事件

        private void parameter1_MouseLeave(object sender, EventArgs e)
        {     
            UpdateClickMouseLeaveMessage(textBox17, parameter1, label1);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            
        }


        private void parameter2_MouseLeave(object sender, EventArgs e)
        {
            
            UpdateClickMouseLeaveMessage(parameter2, label2);
        }

        private void label2_Click(object sender, EventArgs e)
        {
             
        }
        /// <summary>
        /// two
        /// </summary>
        /// <param name="text"></param>
        /// <param name="label"></param>
        private void UpdateClickMouseLeaveMessage(System.Windows.Forms.TextBox text, System.Windows.Forms.Label label)
        {
            if (label.Text != "" && text.Text != "")
            {
                label.Visible = true;
                label.Text = text.Text;
                text.Visible = false;
            }
            else
            {
                label.Visible = false;
                text.Visible = true;
            }
        }
        /// <summary>
        /// 3 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="label"></param>
        private void UpdateClickMouseLeaveMessage(System.Windows.Forms.TextBox gText, System.Windows.Forms.TextBox text, System.Windows.Forms.Label label)
        {
            if (label.Text != "" && text.Text != "")
            {
                label.Visible = true;
                label.Text = text.Text;
                gText.Text = text.Text;
                text.Visible = false;
            }
            else
            {
                label.Visible = false;
                text.Visible = true;
            }
        }

        private void UpdateClickMessage(System.Windows.Forms.TextBox gText, System.Windows.Forms.TextBox text, System.Windows.Forms.Label label)
        {
            // UpdateClickMessage(textBox17, parameter1, label1);
            text.Text = label.Text;
            gText.Text = label.Text;
            label.Visible = false;
            text.Visible = true;
        }
        private void UpdateClickMessage(System.Windows.Forms.TextBox text, System.Windows.Forms.Label label)
        {
            // UpdateClickMessage(textBox17, parameter1, label1);
            text.Text = label.Text;

            label.Visible = false;
            text.Visible = true;
        }
        private void label3_Click(object sender, EventArgs e)
        {
            // UpdateClickMessage(parameter3, label3);
        }

        private void parameter3_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter3, label3);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //  UpdateClickMessage(parameter4, label4);
        }

        private void parameter4_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter4, label4);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // UpdateClickMessage(parameter6, label6);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter5, label5);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter7, label7);
        }

        private void parameter5_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter5, label5);
        }

        private void parameter6_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter6, label6);
        }

        private void parameter7_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter7, label7);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter11, label11);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter9, label9);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter10, label10);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter8, label8);
        }

        private void parameter8_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter8, label8);
        }

        private void parameter9_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter9, label9);
        }

        private void parameter10_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter10, label10);
        }

        private void parameter11_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter11, label11);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter13, label13);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter12, label12);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            //UpdateClickMessage(parameter14, label14);
        }

        private void parameter12_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter12, label12);
        }

        private void parameter13_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter13, label13);
        }

        private void parameter14_MouseLeave(object sender, EventArgs e)
        {
            UpdateClickMouseLeaveMessage(parameter14, label14);
        }

        private void label32_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("前三参数规则不可改变", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        /// <summary>
        /// click event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region click event 
        private void txt_KeJiaAModel_Click(object sender, EventArgs e)
        {
            //  txt_KeJiaAModel.Text = InputTextValue();

        }
        private Dictionary<string, string> DictionaryToDataGridView(string arrString)
        {
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
                        newKeyValuesList.Add(OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')), OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString());
                    }
                }
            }
            return newKeyValuesList;
        }
        /// <summary>
        /// 弹框封装函数
        /// </summary>
        /// <returns></returns>
        public string InputTextValue()
        {

            string strText = string.Empty;
            InputDialog.Show(out strText);
            return strText;
        }


        private void txt_JSModel_Click(object sender, EventArgs e)
        {

            //  this.txt_JSModel.Text = InputTextValue();
        }

        private void txt_ProductModel_Click(object sender, EventArgs e)
        {
            //this.txt_ProductModel.Text = InputTextValue();

        }

        private void txt_FourJTypeModel_Click(object sender, EventArgs e)
        {
            //this.txt_FourJTypeModel.Text = InputTextValue();

        }

        private void txt_KuoZhanFangshiModel_Click(object sender, EventArgs e)
        {
            //this.txt_KuoZhanFangshiModel.Text = InputTextValue();
        }

        private void txt_AnZhuangFangshiModel_Click(object sender, EventArgs e)
        {
            //this.txt_AnZhuangFangshiModel.Text = InputTextValue();
        }

        private void txt_GongHuoFangshiModel_Click(object sender, EventArgs e)
        {
            //this.txt_GongHuoFangshiModel.Text = InputTextValue();
        }

        private void txt_FenDuanNengLiModel_Click(object sender, EventArgs e)
        {
            //this.txt_FenDuanNengLiModel.Text = InputTextValue();
        }

        private void txt_TuoGouFangshiModel_Click(object sender, EventArgs e)
        {
            //this.txt_TuoGouFangshiModel.Text = InputTextValue();
        }

        private void txt_BaoHuTypeModel_Click(object sender, EventArgs e)
        {
            //this.txt_BaoHuTypeModel.Text = InputTextValue();
        }

        private void txt_CaoZuoFangshiModel_Click(object sender, EventArgs e)
        {
            //this.txt_CaoZuoFangshiModel.Text = InputTextValue();
        }

        private void txt_ShengyuAModel_Click(object sender, EventArgs e)
        {
            //this.txt_ShengyuAModel.Text = InputTextValue();
        }

        private void txt_EDingAModel_Click(object sender, EventArgs e)
        {
            //this.txt_EDingAModel.Text = InputTextValue();
        }

        private void txt_YanShiTimeModel_Click(object sender, EventArgs e)
        {
            //this.txt_YanShiTimeModel.Text = InputTextValue();
        }
        #endregion

        private void S_TextChanged(System.Windows.Forms.TextBox pText, System.Windows.Forms.TextBox gText)
        {
            pText.Text = gText.Text.ToUpper();
        }
        //, System.Windows.Forms.Label label
        private void S_TextChanged(System.Windows.Forms.TextBox pText, System.Windows.Forms.TextBox gText, System.Windows.Forms.Label label)
        {
            pText.Text = gText.Text.ToUpper();
            label.Text = gText.Text.ToUpper();
        }
        //private void TextClick(System.Windows.Forms.TextBox gText)
        //{
        //    gText.Text = String.Empty;
        //}
        private void TextClick(System.Windows.Forms.TextBox pText, System.Windows.Forms.TextBox gText, System.Windows.Forms.Label label)
        {
            pText.Text = gText.Text.ToUpper();
            label.Text = gText.Text.ToUpper();
        }
        #region 更改text值时触发事件

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            //parameter1.Text = textBox17.Text;
            // S_TextChanged(parameter1, textBox17);
            S_TextChanged(parameter1, textBox17, label1);

        }

        private void txt_productName_TextChanged(object sender, EventArgs e)
        {
            this.txt_proList.Text = txt_productName.Text;
        }

        private void textBox17_Click(object sender, EventArgs e)
        {
            //textBox17.Text = String.Empty;
            //  TextClick(textBox17);
            TextClick(parameter1, textBox17, label1);
            //parameter1.Visible = true;
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter2, textBox16, label2);
            //S_TextChanged(parameter2, textBox16);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter3, textBox15, label3);
            //S_TextChanged(parameter3, textBox15);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter4, textBox6, label4);
            // S_TextChanged(parameter4, textBox6);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter5, textBox5, label5);
            //  S_TextChanged(parameter5, textBox5);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter6, textBox11, label6);
            // S_TextChanged(parameter6, textBox11);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter7, textBox13, label7);
            //S_TextChanged(parameter7, textBox13);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter8, textBox4, label8);
            // S_TextChanged(parameter8, textBox4);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter9, textBox7, label9);
            // S_TextChanged(parameter9, textBox7);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter10, textBox8, label10);
            // S_TextChanged(parameter10, textBox8);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //S_TextChanged(parameter11, textBox9);
            S_TextChanged(parameter11, textBox9, label11);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //  S_TextChanged(parameter12, textBox10);
            S_TextChanged(parameter12, textBox10, label12);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //   S_TextChanged(parameter13, textBox12);
            S_TextChanged(parameter13, textBox12, label13);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            // S_TextChanged(parameter14, textBox14);
            S_TextChanged(parameter14, textBox14, label14);
        }

        #endregion

        #region 文本点击事件


        private void textBox16_Click(object sender, EventArgs e)
        {
            TextClick(parameter2, textBox16, label2);
            // TextClick(textBox16);
        }

        private void textBox15_Click(object sender, EventArgs e)
        {
            TextClick(parameter3, textBox15, label3);
            // TextClick(textBox15);
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            TextClick(parameter4, textBox6, label4);
            //TextClick(textBox6);
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            TextClick(parameter5, textBox5, label5);
            // TextClick(textBox5);
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            TextClick(parameter6, textBox11, label6);
            // TextClick(textBox11);
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            TextClick(parameter7, textBox13, label7);
            //TextClick(textBox13);
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            TextClick(parameter8, textBox4, label8);
            // TextClick(textBox4);
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            TextClick(parameter9, textBox7, label9);
            //TextClick(textBox7);
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            TextClick(parameter10, textBox8, label10);
            //TextClick(textBox8);
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            TextClick(parameter11, textBox9, label11);
            //TextClick(textBox9);
        }

        private void textBox10_Click(object sender, EventArgs e)
        {
            TextClick(parameter12, textBox10, label12);
            //TextClick(textBox10);
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            TextClick(parameter13, textBox12, label13);
            // TextClick(textBox12);
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            TextClick(parameter14, textBox14, label14);
            //TextClick(textBox14);
        }

        #endregion
        #region 技术参数文本子cheange
        private void Frm_Excel_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosing(e);
            System.Windows.Forms.Application.Exit(e);
        }

        private void Frm_Excel_SizeChanged(object sender, EventArgs e)
        {
            auto.controlAutoSize(this);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        bool isDown = false;

        System.Drawing.Point startPoint;
        private void Frm_Excel_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            startPoint = e.Location;
        }

        private void Frm_Excel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true && e.Button == MouseButtons.Left)
                this.Location += new Size(e.X - startPoint.X, e.Y - startPoint.Y);

        }

        private void Frm_Excel_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }
        public static void ShowInfo(System.Windows.Forms.TextBox txtInfo, string Info)
        {
            //this.listView1.EnsureVisible(2);
            txtInfo.AppendText(Info);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.ScrollToCaret();

        }
        /// <summary>
        /// double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(textBox17, parameter1, label1);
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter2, label2);
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter3, label3);
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter4, label4);
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter5, label5);
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter6, label6);
        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter7, label7);
        }

        private void label8_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter8, label8);
        }

        private void label9_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter9, label9);
        }

        private void label10_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter10, label10);
        }

        private void label11_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter11, label11);
        }

        private void label12_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter12, label12);
        }

        private void label13_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter13, label13);
        }

        private void label14_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter14, label14);
        }
        /// <summary>
        /// Control /Enter KeyUp 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parameter1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                UpdateClickMouseLeaveMessage(textBox17, parameter1, label1);
                // UpdateClickMouseLeaveMessage(parameter1, label1);
                //MessageBox.Show("看到此提示说明在textbox1内按了回车键", "textbox控件绑定回车控件");
            }
        }

        private void parameter3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter3, label3);
                UpdateClickMouseLeaveMessage(textBox15, parameter3, label3);
            }
        }

        private void parameter2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter2, label2);
                UpdateClickMouseLeaveMessage(textBox16, parameter2, label2);
            }

        }

        private void parameter4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                //UpdateClickMouseLeaveMessage(parameter4, label4);
                UpdateClickMouseLeaveMessage(textBox6, parameter4, label4);
            }

        }

        private void parameter5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter5, label5);
                UpdateClickMouseLeaveMessage(textBox5, parameter5, label5);
            }

        }

        private void parameter6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter6, label6);
                UpdateClickMouseLeaveMessage(textBox11, parameter6, label6);
            }

        }

        private void parameter7_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                //UpdateClickMouseLeaveMessage(parameter7, label7);
                UpdateClickMouseLeaveMessage(textBox13, parameter7, label7);
            }

        }

        private void parameter8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter8, label8);
                UpdateClickMouseLeaveMessage(textBox4, parameter8, label8);
            }

        }

        private void parameter9_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                //UpdateClickMouseLeaveMessage(parameter9, label9);
                UpdateClickMouseLeaveMessage(textBox7, parameter9, label9);
            }

        }

        private void parameter10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                //UpdateClickMouseLeaveMessage(parameter10, label10);
                UpdateClickMouseLeaveMessage(textBox8, parameter10, label10);
            }

        }

        private void parameter11_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter11, label11);
                UpdateClickMouseLeaveMessage(textBox9, parameter11, label11);
            }

        }

        private void parameter12_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter12, label12);
                UpdateClickMouseLeaveMessage(textBox10, parameter12, label12);
            }

        }

        private void parameter13_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                //UpdateClickMouseLeaveMessage(parameter13, label13);
                UpdateClickMouseLeaveMessage(textBox12, parameter13, label13);
            }

        }

        private void parameter14_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter14, label14);
                UpdateClickMouseLeaveMessage(textBox14, parameter14, label14);
            }

        }
        private void parameterTextChanged(System.Windows.Forms.TextBox text, System.Windows.Forms.TextBox para)
        {
            text.Text = para.Text;
        }
        private void parameter1_TextChanged(object sender, EventArgs e)
        {
            //textBox17.Text = parameter1.Text;
            //sparameterTextChanged(textBox17, parameter1);
        }

        private void parameter2_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox16, parameter2);
        }

        private void parameter3_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox15, parameter3);
        }

        private void parameter4_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox6, parameter4);
        }

        private void parameter5_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox5, parameter5);
        }

        private void parameter6_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox11, parameter6);
        }

        private void parameter7_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox13, parameter7);
        }

        private void parameter8_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox4, parameter8);
        }

        private void parameter9_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox7, parameter9);
        }

        private void parameter10_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox8, parameter10);
        }

        private void parameter11_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox9, parameter11);
        }

        private void parameter12_TextChanged(object sender, EventArgs e)
        {
            // parameterTextChanged(textBox10, parameter12);
        }

        private void parameter13_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox12, parameter13);
        }

        private void parameter14_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox14, parameter14);
        }


        #endregion

        #region TextChange or click      

        private void txt_KeJiaAModel_TextChanged(object sender, EventArgs e)
        {


            // System.Windows.Forms.MessageBox.Show(txt_KeJiaAModel.Text.LastIndexOf("&").ToString());
            // System.Windows.Forms.MessageBox.Show(txt_KeJiaAModel.Text.Substring(txt_KeJiaAModel.Text.IndexOf('&') + 1));
            //  CreateDictionary("");
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            TextClick(parameter15, textBox2, label58);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter15, textBox2, label58);
        }

        private void parameter15_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter8, label8);
                UpdateClickMouseLeaveMessage(textBox2, parameter15, label58);
            }
        }

        private void parameter15_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox2, parameter15);
        }

        private void label58_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter15, label58);
        }

        private void textBox18_Click(object sender, EventArgs e)
        {
            TextClick(parameter16, textBox18, label60);
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter16, textBox18, label60);
        }

        private void parameter16_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter8, label8);
                UpdateClickMouseLeaveMessage(textBox18, parameter16, label60);
            }
        }

        private void parameter16_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox18, parameter16);
        }

        private void label60_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter16, label60);
        }

        private void textBox20_Click(object sender, EventArgs e)
        {
            TextClick(parameter17, textBox20, label62);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter17, textBox20, label62);
        }

        private void parameter17_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter8, label8);
                UpdateClickMouseLeaveMessage(textBox20, parameter17, label62);
            }
        }

        private void parameter17_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox20, parameter17);
        }

        private void label62_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter17, label62);
        }

        private void parameter18_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                // UpdateClickMouseLeaveMessage(parameter8, label8);
                UpdateClickMouseLeaveMessage(textBox22, parameter18, label64);
            }
        }

        private void parameter18_TextChanged(object sender, EventArgs e)
        {
            //parameterTextChanged(textBox22, parameter18);
        }

        private void label64_DoubleClick(object sender, EventArgs e)
        {
            UpdateClickMessage(parameter18, label64);
        }

        private void textBox22_Click(object sender, EventArgs e)
        {
            TextClick(parameter18, textBox22, label64);
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            S_TextChanged(parameter18, textBox22, label64);
        }

        private void txt_Arr_1_DoubleClick(object sender, EventArgs e)
        {
            //txt_Arr_1.Text = InputTextValue();
            IntoDataGriv(txt_Arr_1, txt_Arr_1);
        }

        private void txt_Arr_2_DoubleClick(object sender, EventArgs e)
        {
            // txt_Arr_2.Text = InputTextValue();
            IntoDataGriv(txt_Arr_2, txt_Arr_2);
        }

        private void txt_Arr_3_DoubleClick(object sender, EventArgs e)
        {
            //txt_Arr_3.Text = InputTextValue();
            IntoDataGriv(txt_Arr_3, txt_Arr_3);
        }

        private void txt_Arr_4_DoubleClick(object sender, EventArgs e)
        {
            // txt_Arr_4.Text = InputTextValue();
            IntoDataGriv(txt_Arr_4, txt_Arr_4);
        }
        private void Frm_Excel_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Width <= 250)
            {
                this.Width = 250;
            }

            if (this.Height <= 250)
            {
                this.Height = 250;
            }
        }

        private void txt_proList_TextChanged(object sender, EventArgs e)
        {
            txt_productName.Text = txt_proList.Text;
        }

        private void txt_productName_DoubleClick(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = false;//该值确定是否可以选择多个文件
            //dialog.Title = "请选择文件夹";
            //dialog.Filter = "所有文件(*.*) | *.*";
            //;
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string file = dialog.FileName;
            //    System.Windows.Forms.MessageBox.Show(file);
            //    ReadDataXml(file);
            //}

        }


        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        public void PlayBeep()
        {
            //调用  
            Beep(800, 50000);

        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_productName.Text))
            {
                System.Windows.Forms.MessageBox.Show("产品名称不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            column.Column_Name = txt_productName.Text.ToUpper();
            columns.Add(column);
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.FileName = txt_productName.Text.ToUpper();
            SaveFile.Filter = "(.xml)|.xml";//EXCEL|*.xlsx|*.xls|EPLAN|*.elk|所有文件类型|*.*
            SaveFile.RestoreDirectory = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                filePath = SaveFile.FileName;
                //txt_productName.Text = System.IO.Path.GetFileName(filePath);
            }
            else
            {
                return;
            }
            try
            {
                label36.Visible = false;
                progressBar1.Visible = false;
                startTime = DateTime.Now;
                ///Data is not null
                ///null or is nou null
                /// 
                #region if Data is not null 
                #region 分隔符 

                if (groups == null)
                {
                    groups = new List<string> { };
                }
                if (!string.IsNullOrEmpty(txt_groupFH_0.Text))
                {
                    if (IsSpecialChar(txt_groupFH_0.Text) == true)
                    {
                        groups.Add(txt_groupFH_0.Text);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("分隔符格式错误！！！\n请点击红色字体" + "分隔符" + "查看");
                        return;
                    }
                    return;
                }
                else
                {
                    groups.Add(txt_groupFH_0.Text);
                    //txt_groupFH1.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH_1.Text))
                {
                    groups.Add(txt_groupFH_1.Text);
                }
                else
                {
                    groups.Add(txt_groupFH_1.Text);
                    //txt_groupFH1.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH1.Text))
                {
                    groups.Add(txt_groupFH1.Text);
                }
                else
                {
                    groups.Add(txt_groupFH1.Text);
                    //txt_groupFH1.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH2.Text))
                {
                    groups.Add(txt_groupFH2.Text);
                }
                else
                {
                    groups.Add(txt_groupFH2.Text);
                    //txt_groupFH2.Visible = false;
                }

                if (!string.IsNullOrEmpty(txt_groupFH3.Text))
                {
                    groups.Add(txt_groupFH3.Text);
                }
                else
                {
                    groups.Add(txt_groupFH3.Text);
                    //txt_groupFH3.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH4.Text))
                {
                    groups.Add(txt_groupFH4.Text);
                }
                else
                {
                    groups.Add(txt_groupFH4.Text);
                    //txt_groupFH4.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH5.Text))
                {
                    groups.Add(txt_groupFH5.Text);
                }
                else
                {
                    groups.Add(txt_groupFH5.Text);
                    //txt_groupFH5.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH6.Text))
                {
                    groups.Add(txt_groupFH6.Text);
                }
                else
                {
                    groups.Add(txt_groupFH6.Text);
                    //txt_groupFH6.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH7.Text))
                {
                    groups.Add(txt_groupFH7.Text);
                }
                else
                {
                    groups.Add(txt_groupFH7.Text);
                    //txt_groupFH7.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH8.Text))
                {
                    groups.Add(txt_groupFH8.Text);
                }
                else
                {
                    groups.Add(txt_groupFH8.Text);
                    //txt_groupFH8.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH9.Text))
                {
                    groups.Add(txt_groupFH9.Text);
                }
                else
                {
                    groups.Add(txt_groupFH9.Text);
                    // txt_groupFH9.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH10.Text))
                {
                    groups.Add(txt_groupFH10.Text);
                }
                else
                {
                    groups.Add(txt_groupFH10.Text);
                    //  txt_groupFH10.Visible = false;
                }
                if (!string.IsNullOrEmpty(txt_groupFH11.Text))
                {
                    groups.Add(txt_groupFH11.Text);
                }
                else
                {
                    groups.Add(txt_groupFH11.Text);
                    //txt_groupFH11.Visible = false;
                }
                if (string.IsNullOrEmpty(txt_groupFH12.Text))
                {
                    groups.Add(txt_groupFH12.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH12.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH13.Text))
                {
                    groups.Add(txt_groupFH13.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH13.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH14.Text))
                {
                    groups.Add(txt_groupFH14.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH14.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH15.Text))
                {
                    groups.Add(txt_groupFH15.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH15.Text);
                }
                if (string.IsNullOrEmpty(txt_groupFH16.Text))
                {
                    groups.Add(txt_groupFH16.Text);
                    // txt_groupFH12.Visible = false;
                }
                else
                {
                    groups.Add(txt_groupFH16.Text);
                }
                #endregion

                #endregion
                #region 壳架等级
                string[] KJArr = null;
                if (!string.IsNullOrEmpty(parameter1.Text))
                {
                    string strKj = txt_KeJiaAModel.Text.ToUpper();
                    keyValues = CreateDictionary(strKj);
                    //column.Column_Name = parameter1.Text;
                    column.parameter1 = parameter1.Text;
                    column.strArrString1 = txt_KeJiaAModel.Text.ToUpper().Split('\r');//CreatAllArry(strKj); //strKj;
                    columns.Add(column);
                    /*string[] */
                    KJArr = CreatAllArry(strKj);

                }
                else
                {
                    IsDeleteData = txt_KeJiaAModel.Text = "";
                    string strKj = IsDeleteData;//txt_KeJiaAModel.Text.ToUpper();
                    /* string[] */
                    KJArr = CreatAllArry(strKj);
                }

                #endregion

                #region 级数
                string[] JSArr = null;
                if (!string.IsNullOrEmpty(parameter2.Text))
                {
                    string strJS = txt_JSModel.Text.Trim().ToUpper();
                    keyValues1 = CreateDictionary(strJS);

                    //column.Column_Name = parameter2.Text;
                    column.parameter2 = parameter2.Text;
                    column.strArrString2 = strJS.Split('\r');
                    columns.Add(column);
                    /*string[] */
                    JSArr = CreatAllArry(strJS);
                }
                else
                {
                    IsDeleteData = txt_JSModel.Text = "";
                    string strJS = IsDeleteData;//txt_JSModel.Text.ToUpper();
                    /*string[] */
                    JSArr = CreatAllArry(strJS);
                }

                #endregion

                #region 产品附件
                string[] PMArr = null;
                if (!string.IsNullOrEmpty(parameter3.Text))
                {
                    string strpm = txt_ProductModel.Text.ToUpper();
                    /*  string[] */
                    PMArr = CreatAllArry(strpm);
                    keyValues2 = CreateDictionary(strpm);

                    //  column.Column_Name = parameter3.Text;
                    column.parameter3 = parameter3.Text;
                    column.strArrString3 = strpm.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    string strpm = txt_ProductModel.Text.ToUpper();
                    /*  string[] */
                    PMArr = CreatAllArry(strpm);
                }


                #endregion

                #region 四级类型
                string[] FTArr = null;
                if (!string.IsNullOrEmpty(parameter4.Text))
                {
                    string strFT = txt_FourJTypeModel.Text.ToUpper();
                    /* string[] */
                    FTArr = CreatAllArry(strFT);
                    keyValues3 = CreateDictionary(strFT);

                    //  column.Column_Name = parameter4.Text;
                    column.parameter4 = parameter4.Text;
                    column.strArrString4 = strFT.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_FourJTypeModel.Text = "";
                    string strFT = IsDeleteData;// txt_FourJTypeModel.Text.ToUpper();
                    FTArr = CreatAllArry(strFT);
                }

                #endregion

                #region 扩展方式
                string[] KZArr = null;
                if (!string.IsNullOrEmpty(parameter5.Text))
                {
                    string strkz = txt_KuoZhanFangshiModel.Text.ToUpper();
                    /*string[] */
                    KZArr = CreatAllArry(strkz);
                    keyValues4 = CreateDictionary(strkz);

                    // column.Column_Name = parameter5.Text;
                    column.parameter5 = parameter5.Text;
                    column.strArrString5 = strkz.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_KuoZhanFangshiModel.Text = "";
                    string strkz = IsDeleteData;// txt_KuoZhanFangshiModel.Text.ToUpper();
                    KZArr = CreatAllArry(strkz);
                }


                #endregion

                #region 安装方式
                string[] AZArr = null;
                if (!string.IsNullOrEmpty(parameter6.Text))
                {
                    string strAZ = txt_AnZhuangFangshiModel.Text.ToUpper();
                    /*string[] */
                    AZArr = CreatAllArry(strAZ);
                    keyValues5 = CreateDictionary(strAZ);

                    //column.Column_Name = parameter6.Text;
                    column.parameter6 = parameter6.Text;
                    column.strArrString6 = strAZ.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_AnZhuangFangshiModel.Text = "";
                    string strAZ = IsDeleteData;// txt_AnZhuangFangshiModel.Text.ToUpper();
                    AZArr = CreatAllArry(strAZ);
                }

                #endregion

                #region 供货方式
                string[] GHFSArr = null;
                if (!string.IsNullOrEmpty(parameter7.Text))
                {
                    string strGHFS = txt_GongHuoFangshiModel.Text.ToUpper();
                    /* string[]*/
                    GHFSArr = CreatAllArry(strGHFS);
                    keyValues6 = CreateDictionary(strGHFS);

                    // column.Column_Name = parameter7.Text;
                    column.parameter7 = parameter7.Text;
                    column.strArrString7 = strGHFS.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_GongHuoFangshiModel.Text = "";
                    string strGHFS = IsDeleteData;// txt_GongHuoFangshiModel.Text.ToUpper();
                    /* string[]*/
                    GHFSArr = CreatAllArry(strGHFS);
                }

                #endregion

                #region 分断能力
                string[] FDNLArr = null;
                if (!string.IsNullOrEmpty(parameter8.Text))
                {
                    string strFdnl = txt_FenDuanNengLiModel.Text.ToUpper();
                    /* string[] */
                    FDNLArr = CreatAllArry(strFdnl);
                    keyValues7 = CreateDictionary(strFdnl);

                    //column.Column_Name = parameter8.Text;
                    column.parameter8 = parameter8.Text;
                    column.strArrString8 = strFdnl.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_FenDuanNengLiModel.Text = "";
                    string strFdnl = IsDeleteData;// txt_FenDuanNengLiModel.Text.ToUpper();
                    FDNLArr = CreatAllArry(strFdnl);
                }

                #endregion

                #region 脱扣方式
                string[] TgfsArr = null;
                if (!string.IsNullOrEmpty(parameter9.Text))
                {
                    string strTgfs = txt_TuoGouFangshiModel.Text.ToUpper();
                    /* string[]*/
                    TgfsArr = CreatAllArry(strTgfs);
                    keyValues8 = CreateDictionary(strTgfs);

                    //column.Column_Name = parameter9.Text;
                    column.parameter9 = parameter9.Text;
                    column.strArrString9 = strTgfs.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_TuoGouFangshiModel.Text = "";
                    string strTgfs = IsDeleteData; txt_TuoGouFangshiModel.Text.ToUpper();
                    TgfsArr = CreatAllArry(strTgfs);
                }

                #endregion

                #region 保护类型
                string[] BHFSArr = null;
                if (!string.IsNullOrEmpty(parameter10.Text))
                {
                    string strBHFS = txt_BaoHuTypeModel.Text.ToUpper();
                    /* string[] */
                    BHFSArr = CreatAllArry(strBHFS);
                    keyValues9 = CreateDictionary(strBHFS);

                    //   column.Column_Name = parameter10.Text;
                    column.parameter10 = parameter10.Text;
                    column.strArrString10 = strBHFS.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_BaoHuTypeModel.Text = "";
                    string strBHFS = IsDeleteData;// txt_BaoHuTypeModel.Text.ToUpper();
                    BHFSArr = CreatAllArry(strBHFS);
                }

                #endregion

                #region 操作方式
                string[] CZArr = null;
                if (!string.IsNullOrEmpty(parameter11.Text))
                {
                    string strCZ = txt_CaoZuoFangshiModel.Text.ToUpper();
                    /*string[] */
                    CZArr = CreatAllArry(strCZ);
                    keyValues10 = CreateDictionary(strCZ);

                    //  column.Column_Name = parameter11.Text;
                    column.parameter11 = parameter11.Text;
                    column.strArrString11 = strCZ.Split('\r');
                    columns.Add(column);
                }
                else
                {
                    IsDeleteData = txt_CaoZuoFangshiModel.Text = "";
                    string strCZ = IsDeleteData;// txt_CaoZuoFangshiModel.Text.ToUpper();
                    CZArr = CreatAllArry(strCZ);
                }

                #endregion

                #region 剩余电流
                string[] SAArr = null;
                if (!string.IsNullOrEmpty(parameter12.Text))
                {
                    string strSA = txt_ShengyuAModel.Text.ToUpper();
                    /* string[] */
                    SAArr = CreatAllArry(strSA);
                    keyValues11 = CreateDictionary(strSA);

                    //  column.Column_Name = parameter12.Text;
                    column.parameter12 = parameter12.Text;
                    column.strArrString12 = strSA.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_ShengyuAModel.Text = "";
                    string strSA = IsDeleteData;// txt_ShengyuAModel.Text.ToUpper();
                    SAArr = CreatAllArry(strSA);
                }

                #endregion

                #region 额定电流
                string[] EDAArr = null;
                if (!string.IsNullOrEmpty(parameter13.Text))
                {
                    string strEDA = txt_EDingAModel.Text.ToUpper();
                    /*  string[]*/
                    EDAArr = CreatAllArry(strEDA);
                    keyValues12 = CreateDictionary(strEDA);

                    //  column.Column_Name = parameter13.Text;
                    column.parameter13 = parameter13.Text;
                    column.strArrString13 = strEDA.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_EDingAModel.Text = "";
                    string strEDA = IsDeleteData;// txt_EDingAModel.Text.ToUpper();
                    EDAArr = CreatAllArry(strEDA);
                }

                #endregion

                #region 延时时间
                string[] YSTArr = null;
                if (!string.IsNullOrEmpty(parameter14.Text))
                {
                    string strYST = txt_YanShiTimeModel.Text.ToUpper();
                    /* string[] */
                    YSTArr = CreatAllArry(strYST);
                    keyValues13 = CreateDictionary(strYST);

                    //  column.Column_Name = parameter14.Text;
                    column.parameter14 = parameter14.Text;
                    column.strArrString14 = strYST.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_YanShiTimeModel.Text = "";
                    string strYST = IsDeleteData;// txt_YanShiTimeModel.Text.ToUpper();
                    YSTArr = CreatAllArry(strYST);
                }

                #endregion

                #region +1
                string[] Arr_1 = null;
                if (!string.IsNullOrEmpty(parameter15.Text))
                {
                    string strArr_1 = txt_Arr_1.Text.ToUpper();
                    /* string[]*/
                    Arr_1 = CreatAllArry(strArr_1);
                    keyValues14 = CreateDictionary(strArr_1);

                    // column.Column_Name = parameter15.Text;
                    column.parameter15 = parameter15.Text;
                    column.strArrString15 = strArr_1.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_Arr_1.Text = "";
                    string strArr_1 = IsDeleteData;// txt_Arr_1.Text.ToUpper();
                    /* string[]*/
                    Arr_1 = CreatAllArry(strArr_1);
                }
                #endregion
                #region +2
                string[] Arr_2 = null;
                if (!string.IsNullOrEmpty(parameter16.Text))
                {
                    string strArr_2 = txt_Arr_2.Text.ToUpper();
                    /* string[]*/
                    Arr_2 = CreatAllArry(strArr_2);
                    keyValues15 = CreateDictionary(strArr_2);

                    //  column.Column_Name = parameter16.Text;
                    column.parameter16 = parameter16.Text;
                    column.strArrString16 = strArr_2.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_Arr_2.Text = "";
                    string strArr_2 = IsDeleteData;// txt_Arr_2.Text.ToUpper();
                    /* string[]*/
                    Arr_2 = CreatAllArry(strArr_2);
                }
                #endregion
                #region +3
                string[] Arr_3 = null;
                if (!string.IsNullOrEmpty(parameter17.Text))
                {
                    string strArr_3 = txt_Arr_3.Text.ToUpper();
                    /* string[]*/
                    Arr_3 = CreatAllArry(strArr_3);
                    keyValues16 = CreateDictionary(strArr_3);


                    column.parameter17 = parameter17.Text;
                    column.strArrString17 = strArr_3.Split('\r');
                    columns.Add(column);

                }
                else
                {
                    IsDeleteData = txt_Arr_3.Text = "";
                    string strArr_3 = IsDeleteData;// txt_Arr_3.Text.ToUpper();
                    /* string[]*/
                    Arr_3 = CreatAllArry(strArr_3);
                }
                #endregion
                #region +4

                string[] Arr_4 = null;
                if (!string.IsNullOrEmpty(parameter18.Text))
                {
                    string strArr_4 = txt_Arr_4.Text.ToUpper();
                    /* string[]*/
                    Arr_4 = CreatAllArry(strArr_4);
                    keyValues17 = CreateDictionary(strArr_4);
                    column.parameter18 = parameter18.Text;
                    column.strArrString18 = strArr_4.Split('\r');
                    columns.Add(column);
                }
                else
                {
                    IsDeleteData = txt_Arr_3.Text = "";
                    string strArr_4 = IsDeleteData;// txt_Arr_4.Text.ToUpper();
                    /* string[]*/
                    Arr_4 = CreatAllArry(strArr_4);
                }
                #endregion
                arrModels.Add(new PrameterModel { Arrparam_Name = txt_productName.Text });
                arrModels.Add(new PrameterModel { Arrparam_Name = "描述" });
                #region 技术参数名称    

                if (parameter1.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter1.Text });
                }
                else
                {

                }

                if (parameter2.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter2.Text });
                }

                if (parameter3.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter3.Text });
                }

                if (parameter4.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter4.Text });
                }

                if (parameter5.Text != "")
                {

                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter5.Text });
                }
                if (parameter6.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter6.Text });
                }

                if (parameter7.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter7.Text });
                }

                if (parameter8.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter8.Text });
                }

                if (parameter9.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter9.Text });
                }

                if (parameter10.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter10.Text });
                }

                if (parameter11.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter11.Text });
                }

                if (parameter12.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter12.Text });
                }

                if (parameter13.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter13.Text });
                }

                if (parameter14.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter14.Text });
                }

                if (parameter15.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter15.Text });
                }

                if (parameter16.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter16.Text });
                }

                if (parameter17.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter17.Text });
                }

                if (parameter18.Text != "")
                {
                    arrModels.Add(new PrameterModel { Arrparam_Name = parameter18.Text });
                }


                #endregion
                n = KJArr.Length * JSArr.Length * PMArr.Length * FTArr.Length * KZArr.Length * AZArr.Length * GHFSArr.Length * FDNLArr.Length * TgfsArr.Length * BHFSArr.Length * CZArr.Length * SAArr.Length * EDAArr.Length * YSTArr.Length * Arr_1.Length * Arr_2.Length * Arr_3.Length * Arr_4.Length;
                if (n > 1048576)
                {
                    MessageBox.Show("保存的配方数据条数已达EXCEL表行数上限", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #region 数据记忆保存  
                List<ColumnModel> list = columns.Distinct().ToList();  //过滤重复数据
                if (System.IO.Directory.Exists(filePath) == false)
                {
                    //  System.IO.Directory.CreateDirectory(filePath);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    XmlSerializer xs = new XmlSerializer(typeof(ColumnModel));
                    xs.Serialize(fs, column);
                    fs.Close();
                    // System.Windows.Forms.MessageBox.Show($"保存路径:{filePath}", "参数配方保存成功");
                    //语音提示
                    System.Threading.Thread t = new System.Threading.Thread(PlayWarnSound);//创建了线程
                    t.Start();//开启线程
                    MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK);
                    //清空所有参数值
                    //txt_KeJiaAModel.Text = string.Empty;
                    //txt_KeJiaAModel.Text = string.Empty;
                    //txt_JSModel.Text = string.Empty;
                    //txt_ProductModel.Text = string.Empty;
                    //txt_FourJTypeModel.Text = string.Empty;
                    //txt_KuoZhanFangshiModel.Text = string.Empty;
                    //txt_AnZhuangFangshiModel.Text = string.Empty;
                    //txt_GongHuoFangshiModel.Text = string.Empty;
                    //txt_FenDuanNengLiModel.Text = string.Empty;
                    //txt_TuoGouFangshiModel.Text = string.Empty;
                    //txt_BaoHuTypeModel.Text = string.Empty;
                    //txt_CaoZuoFangshiModel.Text = string.Empty;
                    //txt_ShengyuAModel.Text = string.Empty;
                    //txt_EDingAModel.Text = string.Empty;
                    //txt_YanShiTimeModel.Text = string.Empty;
                    //txt_Arr_1.Text = string.Empty;
                    //txt_Arr_2.Text = string.Empty;
                    //txt_Arr_3.Text = string.Empty;
                    //txt_Arr_4.Text = string.Empty;
                    t.Abort();

                }
                groups.Clear();
                arrModels.Clear();
                columns.Clear();

                #endregion    

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ClearMemory();
                groups.Clear();
                arrModels.Clear();
                columns.Clear();
            }


        }
        private static void PlayWarnSound(object obj)
        {
            using (SpeechSynthesizer speech = new SpeechSynthesizer())
            {
                speech.Rate = 1;  //语速   
                speech.Volume = 100;  //音量  
                while (true)
                {
                    speech.Speak("配方生成成功");

                }
            }

        }

        private void label40_Click(object sender, EventArgs e)
        {
            Frm_Separator frm_Separator = new Frm_Separator();
            frm_Separator.ShowDialog();
        }
        #endregion
        private void BTN_GETxml_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "(.xml) | *.xml";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                if (System.IO.Path.GetFileName(file).Substring(System.IO.Path.GetFileName(file).IndexOf('.') + 1).ToString() == "xml")
                {
                    txt_productName.Text = System.IO.Path.GetFileName(System.IO.Path.GetFileName(file).Substring(0, System.IO.Path.GetFileName(file).IndexOf('.')));
                    //System.Windows.Forms.MessageBox.Show(file);
                    ReadDataXml(file);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("您所选择的文件名必须为(.xml)格式");
                    return;
                }

            }
        }
        /// <summary>
        /// doing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KeJiaAModel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {

                //   InputDialogForm inp = new InputDialogForm(GetDictionaryToList(txt_KeJiaAModel.Text));

                //  inp.ShowDialog();
                //for (int i = 0; i < douCustoms.Count(); i++)
                //{
                //    txt_KeJiaAModel.Text += douCustoms[i].Code + "&" + douCustoms[i].ConString;
                //}
                // txt_KeJiaAModel.Text = GetInputTextValue();
                //if (txt_KeJiaAModel.Text == "")
                //{

                //    txt_KeJiaAModel.Text = InputTextValue();
                //}
                //else
                //{
                //    InputDialogForm input = new InputDialogForm(GetDictionaryToList(txt_KeJiaAModel.Text),txt_KeJiaAModel);
                //    input.Show();
                //}

            }
        }
        private List<DouCustom> GetDictionaryToList(string arrString)
        {
            List<DouCustom> list = new List<DouCustom>();
            DouCustom dou = null;
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
                        dou = new DouCustom
                        {
                            Code = OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')),
                            ConString = OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString()
                        };
                        list.Add(dou);
                        // douCustom = new DouCustom { Code = OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')), ConString = OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString() };
                        //newKeyValuesList.Add(OmeArr[i].Substring(0, OmeArr[i].IndexOf('&')), OmeArr[i].Substring(OmeArr[i].IndexOf('&') + 1).ToString());

                    }
                }
                // douCustoms.Add(douCustom);
            }

            return list;
        }
        private void txt_KeJiaAModel_DoubleClick(object sender, EventArgs e)
        {

            IntoDataGriv(txt_KeJiaAModel, txt_KeJiaAModel);



        }
        private void TimerEvent()
        {
            t.Elapsed += new System.Timers.ElapsedEventHandler(timeup);
            t.Enabled = true;
        }
        private void timeup(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.btn_getInputString.PerformClick();
            t.Stop();
        }
        private void txt_Arr_1_Click(object sender, EventArgs e)
        {
            //txt_Arr_1.Text = InputTextValue();
        }

        private void txt_Arr_2_Click(object sender, EventArgs e)
        {
            //txt_Arr_2.Text = InputTextValue();
        }

        private void txt_Arr_3_Click(object sender, EventArgs e)
        {
            //txt_Arr_3.Text = InputTextValue();
        }

        private void txt_Arr_4_Click(object sender, EventArgs e)
        {
            //txt_Arr_4.Text = InputTextValue();
        }


        #region 内存回收

        [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        /// <summary>
        /// 清理内存
        /// </summary>
        private void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentCount += 1;
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
        public void IntoDataGriv(TextBox box, TextBox textBox)
        {
            if (box.Text == "")
            {
                box.Text = InputTextValue();
            }
            else
            {
                InputDialogForm inp = new InputDialogForm(GetDictionaryToList(box.Text), textBox.Name);

                inp.ShowDialog();
                TimerEvent();
            }

        }
        private void txt_JSModel_DoubleClick(object sender, EventArgs e)
        {
            IntoDataGriv(txt_JSModel, txt_JSModel);
            // IntoDataGriv(txt_JSModel);
            //if (txt_JSModel.Text == "")
            //{
            //    this.txt_JSModel.Text = InputTextValue();
            //}
            //else
            //{
            //    IntoDataGriv(txt_JSModel);
            //}

        }

        private void txt_ProductModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_ProductModel.Text = InputTextValue();
            IntoDataGriv(txt_ProductModel, txt_ProductModel);
        }

        private void txt_FourJTypeModel_DoubleClick(object sender, EventArgs e)
        {
            // this.txt_FourJTypeModel.Text = InputTextValue();
            IntoDataGriv(txt_FourJTypeModel, txt_FourJTypeModel);
        }

        private void txt_KuoZhanFangshiModel_DoubleClick(object sender, EventArgs e)
        {
            // this.txt_KuoZhanFangshiModel.Text = InputTextValue();
            IntoDataGriv(txt_KuoZhanFangshiModel, txt_KuoZhanFangshiModel);
        }

        private void txt_AnZhuangFangshiModel_DoubleClick(object sender, EventArgs e)
        {
            // this.txt_AnZhuangFangshiModel.Text = InputTextValue();
            IntoDataGriv(txt_AnZhuangFangshiModel, txt_AnZhuangFangshiModel);
        }
        private void txt_GongHuoFangshiModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_GongHuoFangshiModel.Text = InputTextValue();
            IntoDataGriv(txt_GongHuoFangshiModel, txt_GongHuoFangshiModel);
        }

        private void txt_FenDuanNengLiModel_DoubleClick(object sender, EventArgs e)
        {
            //  this.txt_FenDuanNengLiModel.Text = InputTextValue();
            IntoDataGriv(txt_FenDuanNengLiModel, txt_FenDuanNengLiModel);
        }

        private void txt_TuoGouFangshiModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_TuoGouFangshiModel.Text = InputTextValue();
            IntoDataGriv(txt_TuoGouFangshiModel, txt_TuoGouFangshiModel);
        }

        private void txt_BaoHuTypeModel_DoubleClick(object sender, EventArgs e)
        {
            // this.txt_BaoHuTypeModel.Text = InputTextValue();
            IntoDataGriv(txt_BaoHuTypeModel, txt_BaoHuTypeModel);
        }

        private void txt_CaoZuoFangshiModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_CaoZuoFangshiModel.Text = InputTextValue();
            IntoDataGriv(txt_CaoZuoFangshiModel, txt_CaoZuoFangshiModel);
        }

        private void txt_ShengyuAModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_ShengyuAModel.Text = InputTextValue();
            IntoDataGriv(txt_ShengyuAModel, txt_ShengyuAModel);
        }

        private void txt_EDingAModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_EDingAModel.Text = InputTextValue();
            IntoDataGriv(txt_EDingAModel, txt_EDingAModel);
        }

        private void txt_YanShiTimeModel_DoubleClick(object sender, EventArgs e)
        {
            //this.txt_YanShiTimeModel.Text = InputTextValue();
            IntoDataGriv(txt_YanShiTimeModel, txt_YanShiTimeModel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (UseTextBoxValue.text_Name)
            {
                case "txt_KeJiaAModel":
                    txt_KeJiaAModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_JSModel":
                    txt_JSModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_ProductModel":
                    txt_ProductModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_FourJTypeModel":
                    txt_FourJTypeModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_KuoZhanFangshiModel":
                    txt_KuoZhanFangshiModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_AnZhuangFangshiModel":
                    txt_AnZhuangFangshiModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_GongHuoFangshiModel":
                    txt_GongHuoFangshiModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_FenDuanNengLiModel":
                    txt_FenDuanNengLiModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_TuoGouFangshiModel":
                    txt_TuoGouFangshiModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_BaoHuTypeModel":
                    txt_BaoHuTypeModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_CaoZuoFangshiModel":
                    txt_CaoZuoFangshiModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_ShengyuAModel":
                    txt_ShengyuAModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_EDingAModel":
                    txt_EDingAModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_YanShiTimeModel":
                    txt_YanShiTimeModel.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_Arr_1":
                    txt_Arr_1.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_Arr_2":
                    txt_Arr_2.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_Arr_3":
                    txt_Arr_3.Text = UseTextBoxValue.BoxText;
                    break;
                case "txt_Arr_4":
                    txt_Arr_4.Text = UseTextBoxValue.BoxText;
                    break;
                default:
                    break;
            }


        }




    }



}
