using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizatorSkladniowy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                new SyntaxAnalyzer().IsValidExpression(this.textBox1.Text.Replace(" ", string.Empty)) ? "Expression valid" : "Expression invalid",
                "Result", MessageBoxButtons.OK);
        }
    }
}
