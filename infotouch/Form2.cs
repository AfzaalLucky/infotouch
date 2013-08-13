using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace infotouch
{
    public partial class Form2 : Form
    {
        Properties.Settings settings = new Properties.Settings();
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                comboBox1.Enabled = true;
                groupBox2.Enabled = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                groupBox2.Enabled = true;
                comboBox1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (comboBox1.Text != "")
                {
                    string cb = comboBox1.Text;
                    settings.Every = int.Parse(cb.Substring(0, 2));
                    settings.Save();

                    comboBox1.SelectedIndex = -1;
                    radioButton1.Checked = false;
                    comboBox1.Enabled = false;

                    MessageBox.Show("Settings Save.","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Choose preferred schedule.","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();

                }
            }
            else if (radioButton2.Checked == true)
            {
                settings.Day = dateTimePicker1.Value.Date;
                settings.Time = dateTimePicker2.Value;
                settings.Save();

                groupBox2.Enabled = false;

                MessageBox.Show("Settings Save.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

 
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Choose preferred schedule.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
    }
}
