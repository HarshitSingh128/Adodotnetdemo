using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Adodotnetdemo
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {

            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
           
        }
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

            try
            {
                // step1 - write query
                string qry = "update Employee set name=@name,city=@city,salary=@salary where id=@id";
                // create object of command and assign the query
                cmd = new SqlCommand(qry, con);
                // assign values to parameters
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@city", textBox3.Text);
                cmd.Parameters.AddWithValue("@salary", textBox4.Text);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                // fire the query
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1:write query
                string qry = "insert into Employee values(@name,@city,@salary)";
                //create object of command and assign the query
                cmd = new SqlCommand(qry, con);
                //assign value to parameter
               
                cmd.Parameters.AddWithValue("@name",textBox2.Text);
                cmd.Parameters.AddWithValue("@city", textBox3.Text);
                cmd.Parameters.AddWithValue("@salary",textBox4.Text);
                //fire the query
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "select * from Employee where id=@id";
                //step 2
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                //step3 exceute query
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textBox2.Text = dr["name"].ToString();
                        textBox3.Text = dr["city"].ToString();
                        textBox4.Text = dr["salary"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Record not found");

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

                // step1 - write query
                string qry = "delete from Employee where id=@id";
                // create object of command and assign the query
                cmd = new SqlCommand(qry, con);
                // assign values to parameters
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                // fire the query
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                //dr hold date in the from of recods(object)
                //grid needs data in table format
                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
