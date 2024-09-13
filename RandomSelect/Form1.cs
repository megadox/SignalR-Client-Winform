using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomSelect
{
    public partial class Form1 : Form
    {
        List<string> list;
        public Form1()
        {
            InitializeComponent();

            list = new List<string>();

            this.btnCal.Click += BtnCal_Click;
            btnclear.Click += Btnclear_Click;
            txtRandomCount.Text = "0";
        }

        private void Btnclear_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            txtRandomNumber.Clear();
            results = new List<int>();
        }

        List<int> results = new List<int>();

        private void BtnCal_Click(object sender, EventArgs e)
        {
            string humanPredict = txtRandomNumber.Text;
            int randomCount = Convert.ToInt32(txtRandomCount.Text);

            if(string.IsNullOrEmpty(humanPredict) )
            {
                int i = 0;

                while(i < 6)
                {

                    Random ran = new Random();
                    int rInt = ran.Next(1, 45);
                    if(!results.Contains(rInt))
                    {
                        results.Add(rInt);
                        i++;
                    }
                    
                }
                
            }
            else
            {
                string[] hp = txtRandomNumber.Text.Split(' ');
                int i = 0;
                List<string> list = new List<string>();
                list.AddRange(hp);
                if(randomCount > 0)
                {
                    while (i <= randomCount)
                    {

                        Random ran = new Random();
                        int rInt = ran.Next(1, 45);
                        if(!list.Contains(rInt.ToString()))
                        {
                            list.Add(rInt.ToString());
                            i++;
                        }
                    }
                }

                i = 0;
                while (i < 6)
                {
                    Random ran = new Random();
                    int rInt = ran.Next(0, list.Count-1);
                    string sel = list[rInt];
                    if(int.TryParse(sel, out int rr))
                    {
                        if (!results.Contains(rr))
                    {
                            results.Add(rr);
                            i++;
                        }
                    }
                }
            }

            results.Sort();

            StringBuilder sb = new StringBuilder();
            foreach(int i in results)
            {
                sb.Append($"{i}, ");
            }

            txtResult.Text = sb.ToString();
        }
    }
}
