using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazma_test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

       
      

        
        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = $"Doğru Sayısı: {Form1._true}";
            label2.Text = $"Yanlış Sayısı: {Form1._false}";
            label3.Text = $"Tus Vurusu: {Form1._keyLength}";
            int wpm = (Form1._keyLength / 5);
            label5.Text = wpm.ToString();

            /* for calculate wpm : https://penandthepad.com/calculate-words-per-minute-reading-7563359.html */

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = new Form1();

            form1.Closed += (s, args) => this.Close();
            form1.Show();
            this.Hide();
        }
    }
}
