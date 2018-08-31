using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Telephone_Diary
{
    public partial class Phone : Form
    {
       
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Phone;Integrated Security=True");
        public Phone()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void button2_Click(object sender, EventArgs e) //Insert button
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles 
            (First, Last, Mobile, Email, Category) 
            VALUES     ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", con);            

            //con.Close();
            MessageBox.Show("Successfully Saved");
            Display();
    }
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Mobiles", con); //New 
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
           
            }       
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)       //Selektuje ceo red
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[1].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[2].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[3].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[4].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)              //Delete button
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Mobiles 
            WHERE     (Mobile ='" + textBox3.Text + "')", con);            

            con.Close();
            MessageBox.Show("Delete Successfully");
            Display();
        }

        private void button4_Click(object sender, EventArgs e)              //Update button
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE    Mobiles
            SET     First ='" + textBox1.Text + "', Last ='" + textBox2.Text + "', Mobile ='" + textBox3.Text + "', Email ='" + textBox4.Text + "', Category ='" + comboBox1.Text + "' " +
            "WHERE   (Mobile = '" + textBox3.Text + "')", con);

            con.Close();
            MessageBox.Show("Update Successfully");
            Display();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)           //Search po broju telefona, imenu i prezimenu
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Mobiles Where (Mobile like '%'" + textBox5.Text + "%') or (First like '%" + textBox5.Text + "%') or (Last like '%" + textBox5.Text + "%') ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();


            }
        }
    }
}
       
