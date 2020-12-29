using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public class GetEventArg : EventArgs
    {
        //传递对话框窗体的数据信息
        public string Text { get; set; }

        /// <summary>
        /// 提供外部访问自己元素的方法
        /// </summary>
        /// <param name="txt"></param>
        public void SetText(string txt)
        {
            TextBox textBox = new TextBox();
            // this.textBox.Text = txt;
            // button1_Click(this, new EventArgs());
            // MessageBox.Show(this.txt_KeJiaAModel.Text);
        }

        public void AfterInputFrmTextChange(object sender, EventArgs e)
        {
            //拿到对话框窗体的传来的文本
            GetEventArg arg = e as GetEventArg;
            this.SetText(arg.Text);
        }

        internal void InputFormTxtChaned(object sender, EventArgs e)
        {
            //取到对话框窗体的传来的文本
            GetEventArg arg = e as GetEventArg;
            this.SetText(arg.Text);

        }
    }
}
