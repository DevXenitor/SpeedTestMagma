using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace yazma_test
{
    /* Data Source=DESKTOP-43PA3HA\SQLEXPRESS;Initial Catalog=Magma;Integrated Security=True */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Dictionary<int, string> kelimeler = new Dictionary<int, string>();
        static SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-43PA3HA\SQLEXPRESS;Initial Catalog=Magma;Integrated Security=True");
        public static int _true = 0;
        public static int _false = 0;
        public static int _keyLength = 0;

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {

                if (txtInput.TextLength > 0)
                {
                    Compare();
                    PassTheWord();
                }
                txtInput.Clear();
                e.SuppressKeyPress = true; //space tuşunu engeller
                lbl_first.Text = listBox1.Items[0].ToString();
                lbl_first.ForeColor = Color.Black;
            }
        }

        private void StartTime()
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void PassTheWord()
        {
            listBox1.Items[0] = listBox1.Items[1];
            listBox1.Items[1] = listBox1.Items[2];
            listBox1.Items[2] = listBox1.Items[3];
            listBox1.Items[3] = listBox1.Items[4];
            listBox1.Items[4] = listBox1.Items[5];
            listBox1.Items[5] = ReturnWord();
        }

        public string ReturnWord()
        {
            Random rnd = new Random();
            int sayı;
            string value = "salyangoz";
            sayı = rnd.Next(0, kelimeler.Keys.Count);
            foreach (var item in kelimeler)
            {
                if (item.Key == sayı)
                {
                    value = item.Value;
                }

            }
            return value;

        }

        private void Compare()
        {
            if (txtInput.Text == listBox1.Items[0].ToString())
            {
                _true += 1;
                _keyLength += txtInput.TextLength + 1;//+1 space tuşu için
            }
            else
            {
                _false += 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddingToList();

            AddDataToListBox();

            lbl_first.Text = listBox1.Items[0].ToString();

        }

        private void AddDataToListBox()
        {
            Random rnd = new Random();
            int sayac = 0;
            int sayı = 0;

            for (int i = 0; i < 6; i++)
            {
                sayı = rnd.Next(0, kelimeler.Count);
                foreach (var item in kelimeler)
                {
                    if (item.Key == sayı)
                    {
                        if (listBox1.Items.Count < 6)
                            listBox1.Items.Insert(sayac, item.Value);
                    }
                }
                sayac++;
            }
        }
        private void AddingToList()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select * from words", connection);
            SqlDataReader reader = command.ExecuteReader();
            int id = 0;
            while (reader.Read())
            {

                kelimeler.Add(id, reader[0].ToString());
                id += 1;
            }
            connection.Close();
        }
        static int timer = 60;
        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = 1000;
            timer--;
            lbl_time.Text = timer.ToString();

            if (timer == 0)
            {
                timer1.Stop();
                timer1.Enabled = false;

                var form2 = new Form2();

                form2.Closed += (s, args) => this.Close();
                form2.Show();
                this.Hide();
                timer = 60;
            }
        }
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string typedWordNow = txtInput.Text;
            int typedWorLength = typedWordNow.Length;
            string targetWord = listBox1.Items[0].ToString();
            if (typedWordNow.Length > targetWord.Length)
            {
                lbl_first.ForeColor = Color.Red;
            }
            else
            {
                string targetWordNow = targetWord.Substring(0, typedWorLength);
                if (typedWordNow == targetWordNow)
                {
                    lbl_first.ForeColor = Color.Green;
                }
                else
                {
                    lbl_first.ForeColor = Color.Red;
                }
            }
            StartTime();
        }
    }
}
