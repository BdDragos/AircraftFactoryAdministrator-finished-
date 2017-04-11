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
using System.Configuration;

namespace ConectareBaza
{
    public partial class Form1 : Form
    {
        SqlDataAdapter da1 = new SqlDataAdapter();
        DataSet ds1 = new DataSet();

        List<TextBox> textBoxList;

        SqlDataAdapter da3 = new SqlDataAdapter();
        DataSet ds3 = new DataSet();

        BindingSource bs1 = new BindingSource();

        SqlDataAdapter da2 = new SqlDataAdapter();
        DataSet ds2 = new DataSet();
        BindingSource bs2 = new BindingSource();

        SqlConnection cs;
        public Form1()
        {
            try
            {
                InitializeComponent();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Afiseaza avion;
        private void button1_Click(object sender, EventArgs e)
        {
            cs.Open();
            string selectcmd = ConfigurationManager.AppSettings["ParentSelectCmd"];
            da3.SelectCommand = new SqlCommand(selectcmd, cs);
            da3.Fill(ds3, "ParentTable");

            cs.Close();

            /*
            try
            {
                da1.SelectCommand = new SqlCommand("SELECT * FROM Avion", cs);
                ds1.Clear();
                da1.Fill(ds1);
                dataGridView1.DataSource = ds1.Tables[0];
                bs1.DataSource = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */

        }
        //Sterge din motor dupa avion
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                cs.Open();
                String value;
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                string toDeletedId = Convert.ToString(textBox2.Text);

                string deleteCmd = ConfigurationSettings.AppSettings["ChildDeleteCmd"];
                da3.DeleteCommand = new SqlCommand(deleteCmd, cs);

                value = "@value".ToString();
                da3.DeleteCommand.Parameters.Add(value, SqlDbType.VarChar).Value = toDeletedId;

                da3.DeleteCommand.ExecuteNonQuery();
                ds3.Tables["ChildTable"].Clear();
                string selectcmd = ConfigurationManager.AppSettings["ChildSelectCmd"];
                da3.SelectCommand = new SqlCommand(selectcmd, cs);
                da3.Fill(ds3, "ChildTable");

                cs.Close();
            }
            else
                MessageBox.Show("Selectati o linie");

            /*
            try
            {
                int Acod;
                int.TryParse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), out Acod);
                SqlCommand comanda = new SqlCommand();
                cs.Open();
                comanda.Connection = cs;
                comanda.CommandType = CommandType.Text;
                comanda.CommandText = "delete from Motor where Acod = @Acod";
                comanda.Parameters.AddWithValue("@Acod", Acod);
                comanda.ExecuteNonQuery();
                cs.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            button4_Click(null, null);
            */
        }
        //Insereaza in Motor;
        private void button3_Click(object sender, EventArgs e)
        {
            cs.Open();
            string value1;
            string value2;
            int i;
            int selectedRowindex = dataGridView1.SelectedCells[0].RowIndex;

            string foreingID = dataGridView1.Rows[selectedRowindex].Cells[0].Value.ToString();

            string insertCmd = ConfigurationSettings.AppSettings["ChildInsertCmd"];
            da3.InsertCommand = new SqlCommand(insertCmd, cs);

            int nrColumns = ds3.Tables["ChildTable"].Columns.Count;

   
            value1 = "@value1".ToString();
            da3.InsertCommand.Parameters.Add(value1, SqlDbType.VarChar).Value = textBox3.Text;

            value2 = "@value2".ToString();
            da3.InsertCommand.Parameters.Add(value2, SqlDbType.VarChar).Value = textBox4.Text;

            da3.InsertCommand.ExecuteNonQuery();

            ds3.Tables["ChildTable"].Clear();
            string selectcmd = ConfigurationManager.AppSettings["ChildSelectCmd"];
            da3.SelectCommand = new SqlCommand(selectcmd, cs);
            da3.Fill(ds3, "ChildTable");

            cs.Close();

            /*
            try
            {
                da2.InsertCommand = new SqlCommand("INSERT INTO Motor (NumeM,Acod) VALUES(@d,@c)", cs);
                da2.InsertCommand.Parameters.Add("@d", SqlDbType.VarChar).Value = textBox3.Text;
                da2.InsertCommand.Parameters.Add("@c", SqlDbType.VarChar).Value = textBox4.Text;
                cs.Open();
                da2.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Inserare cu succes");
                cs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cs.Close();
            }
            */
        }
        //Afiseaza motor;
        private void button4_Click(object sender, EventArgs e)
        {
            cs.Open();
            string selectcmd = ConfigurationManager.AppSettings["ChildSelectCmd"];
            da3.SelectCommand = new SqlCommand(selectcmd, cs);
            da3.Fill(ds3, "ChildTable");
            cs.Close();

            /*
            try
            {
                da2.SelectCommand = new SqlCommand("SELECT * FROM Motor", cs);
                ds2.Clear();
                da2.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                bs2.DataSource = ds2.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        //Afiseaza motor selectand un element din avion;
        private void update_gridview(String id)
        {
            /*
            try
            {
                int x = Int32.Parse(id);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cs;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Motor where Acod = @codA";
                cmd.Parameters.AddWithValue("@codA", x);

                DataSet ds3 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds3, "Motor");
                dataGridView2.DataSource = ds3.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            textBox2.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Clear();
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            update_gridview(dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }

        //Update motor;
        private void button5_Click(object sender, EventArgs e)
        {
            //actualizare
            if (dataGridView2.SelectedCells.Count > 0)
            {
                cs.Open();
                string updateCmd = ConfigurationSettings.AppSettings["ChildUpdateCmd"];
                da3.UpdateCommand = new SqlCommand(updateCmd, cs);

                
                String motorId = dataGridView2.CurrentRow.Cells[0].Value.ToString();

                int i = 0;
                string value1;
                string value2;
                string value3;

                value1 = "@value1".ToString();
                da3.UpdateCommand.Parameters.Add(value1, SqlDbType.VarChar).Value = textBox3.Text;

                value2 = "@value2" .ToString();
                da3.UpdateCommand.Parameters.Add(value2, SqlDbType.VarChar).Value = textBox4.Text;

                value3 = "@value3".ToString();
                da3.UpdateCommand.Parameters.Add(value3, SqlDbType.VarChar).Value = motorId;

                da3.UpdateCommand.ExecuteNonQuery();

                ds3.Tables["ChildTable"].Clear();
                string selectcmd = ConfigurationManager.AppSettings["ChildSelectCmd"];
                da3.SelectCommand = new SqlCommand(selectcmd, cs);
                da3.Fill(ds2, "ChildTable");

                cs.Close();
            }

            /*
            try
            {
                int codM;
                int.TryParse(textBox2.Text, out codM);
                String NumeM = textBox3.Text;
                int Acod;
                int.TryParse(textBox4.Text, out Acod);
                cs.Open();
                SqlCommand comanda = new SqlCommand();
                comanda.Connection = cs;
                comanda.CommandType = CommandType.Text;
                comanda.CommandText = "UPDATE Motor SET NumeM=@NumeM, Acod=@Acod WHERE codM=@codM";
                comanda.Parameters.AddWithValue("@NumeM", NumeM);
                comanda.Parameters.AddWithValue("@Acod", Acod);
                comanda.Parameters.AddWithValue("@codM", codM);
                comanda.ExecuteNonQuery();
                cs.Close();
                update_gridview(Acod.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        public void getData()
        {
            string parentId = ConfigurationManager.AppSettings["idParent"];

            DataRelation relation = new DataRelation("Parent_Child_FK",
            ds3.Tables["ParentTable"].Columns[parentId],
            ds3.Tables["ChildTable"].Columns[parentId]);
            ds3.Relations.Add(relation);

            bs1.DataSource = ds3;
            bs1.DataMember = "ParentTable";

            bs2.DataSource = bs1;
            bs2.DataMember = "Parent_Child_FK";
        }

        public void populateDataGridViews()
        {
            dataGridView1.DataSource = bs1;
            dataGridView2.DataSource = bs2;
            getData();
            dataGridView2.AutoResizeColumns();
            dataGridView1.AutoGenerateColumns = true;
        }

        public void fillDataSet()
        {
            cs.Open();
            string selectcmd = ConfigurationManager.AppSettings["ParentSelectCmd"];
            da3.SelectCommand = new SqlCommand(selectcmd, cs);
            da3.Fill(ds3, "ParentTable");

            selectcmd = ConfigurationManager.AppSettings["ChildSelectCmd"];
            da3.SelectCommand = new SqlCommand(selectcmd, cs);
            da3.Fill(ds3, "ChildTable");
            cs.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["conStr"];
            string connectionString = conSet.ConnectionString;
            cs = new SqlConnection(connectionString);

            fillDataSet();
            populateDataGridViews();

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
