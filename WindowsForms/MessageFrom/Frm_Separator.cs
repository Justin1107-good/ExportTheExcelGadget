using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.MessageFrom
{
    public partial class Frm_Separator : Form
    {
        public Frm_Separator()
        {
            InitializeComponent();
        }

        private void Frm_Separator_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "一、 \\[ \\] \\^ \\ \n二、 -_*×――(^) \n三、 $%~!＠@＃#$ \n四、 …&%￥—+=<>《》\n五、 !！??？:：•`·、 \n六、 。，；,.;/\'\"{} \n七、（）‘’“”-";
        }
    }
}
