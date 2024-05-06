using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Adodotnetdemo
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {


            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);

        }
        private void ClearFileds()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }


        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(textBox1.Text);
                if (row != null)
                {
                    textBox2.Text = row["name"].ToString();
                    textBox3.Text = row["city"].ToString();
                    textBox4.Text = row["salary"].ToString();

                }
                else
                {
                    MessageBox.Show("Record not Found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            public DataSet GetAllEmployees()
            {
                string qry = "select * from Employee";
                da = new SqlDataAdapter(qry, con);
                //add pk to the coloumn which is in the dataset
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                //scb will track the dataset and generate the qry -assign to dataadapter
                scb = new SqlCommandBuilder(da);
                ds = new DataSet();
                //emp is the anme given to the tabe which is in the datset
                //if not given then it will take default name is[0]
                //da.fill(ds);
                da.Fill(ds, "emp");
                return ds;
            }

            private void btnsave_Click(object sender, EventArgs e)
            {
                try
                {
                    ds = GetAllEmployees();
                    // create new row to add record
                    DataRow row = ds.Tables["emp"].NewRow();
                    row["name"] = textBox2.Text;
                    row["city"] = textBox3.Text;
                    row["salary"] = textBox4.Text;
                    //attach row to the emp table
                    ds.Tables["emp"].Rows.Add(row);
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record inserted");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(textBox1.Text);
                if (row != null)
                {
                    // override the value from textbox to row
                    row["name"] = textBox2.Text;
                    row["city"] = textBox3.Text;
                    row["salary"] = textBox4.Text;
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record updated");
                        ClearFileds();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(textBox1.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                       ClearFileds();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnshow_Click(object sender, EventArgs e)
        {

            ds = GetAllEmployees();
            dataGridView1.DataSource = ds.Tables["emp"];


        }
    }
    }
