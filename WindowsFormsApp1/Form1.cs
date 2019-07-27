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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string stringConnection = "Server=INSTRUCTORIT;Database=TournamentManager;User Id=ProfileUser;Password=ProfileUser2019";

        public SqlConnection myConnection { get; private set; }

        public Form1()
        {
            InitializeComponent();
            Creatcontacts();
            Update();
            Delete();
        }

       

        private void BtnCreate_Click(object sender, EventArgs e)
        {

           
            txtId.Text = txtId.Text.Trim().Replace(";", "");
            txtTeamName.Text = txtTeamName.Text.Trim().Replace(";", "");
            txtCoachName.Text = txtCoachName.Text.Trim().Replace(";", "");
            txtDirectorName.Text = txtDirectorName.Text.Trim().Replace(";", "");
            txtAddressLine1.Text = txtId.Text.Trim().Replace(";", "");
            txtAddressLine2.Text = txtTeamName.Text.Trim().Replace(";", "");
            txtPostCode.Text = txtDirectorName.Text.Trim().Replace(";", "");
            txtCity.Text = txtCoachName.Text.Trim().Replace(";", "");
            txtContactNumber.Text = txtDirectorName.Text.Trim().Replace(";", "");
            txtEmail.Text = txtCoachName.Text.Trim().Replace(";", "");
            txtCreatedBy.Text = txtCoachName.Text.Trim().Replace(";", "");
            //Coution to empty text
            if (txtTeamName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a  team name");
                return;
            }
            if (txtDirectorName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a coach  name");
                return;
            }
            if (txtCoachName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a Director name");
                return;
            }
            if (txtContactNumber.Text == string.Empty)
            {
                MessageBox.Show("Please enter a Phone number");
                return;
            }


            //Insert
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = stringConnection;
            try
            {  
                myConnection.Open();
                string query = "INSERT INTO Teams VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
                query = String.Format(query,
                   
                    txtTeamName.Text,
                    txtCoachName.Text,
                    txtDirectorName.Text,
                    txtAddressLine1.Text,
                    txtAddressLine2.Text,
                    txtPostCode.Text,
                    txtCity.Text,
                    txtContactNumber,
                    txtEmail,
                    txtCreatedBy
                    );
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = query;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
            txtId.Text = string.Empty;
            txtTeamName.Text = string.Empty;
            txtCoachName.Text = string.Empty;
            txtDirectorName.Text = string.Empty;
            txtAddressLine1.Text = string.Empty;
            txtAddressLine2.Text = string.Empty;
            txtPostCode.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCreatedBy.Text = string.Empty;


            Creatcontacts();

        }
        public void Creatcontacts()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = stringConnection;
            try
            {
                myConnection.Open();
                string query = "SELECT * FROM Teams ORDER BY TeamName";
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = query;
                DataTable myContacts = new DataTable();
                myContacts.Columns.Add(new DataColumn("TeamId"));
                myContacts.Columns.Add(new DataColumn("TeamName"));
                myContacts.Columns.Add(new DataColumn("CoachName"));
                myContacts.Columns.Add(new DataColumn("DirectorName"));
                myContacts.Columns.Add(new DataColumn("AddressLine1"));
                myContacts.Columns.Add(new DataColumn("AddressLine2"));
                myContacts.Columns.Add(new DataColumn("PostCode"));
                myContacts.Columns.Add(new DataColumn("City"));
                myContacts.Columns.Add(new DataColumn("ContactNumber"));
                myContacts.Columns.Add(new DataColumn("EmailAddress"));
                myContacts.Columns.Add(new DataColumn("CreatedBy"));
                SqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    DataRow contact = myContacts.NewRow();
                    contact["TeamId"] = myReader["TeamId"];
                    contact["TeamName"] = myReader["TeamName"];
                    contact["CoachName"] = myReader["CoachName"];
                    contact["DirectorName"] = myReader["DirectorName"];
                    contact["AddressLine1"] = myReader["AddressLine1"];
                    contact["AddressLine2"] = myReader["AddressLine2"];
                    contact["PostCode"] = myReader["PostCode"];
                    contact["City"] = myReader["City"];
                    contact["ContactNumber"] = myReader["ContactNumber"];
                    contact["EmailAddress"] = myReader["EmailAddress"];
                    contact["CreatedBy"] = myReader["CreatedBy"];
                    myContacts.Rows.Add(contact);
                }
                dataGridView1.DataSource = myContacts;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
        }

    
        private void BttnUpdate_Click(object sender, EventArgs e)
        {   //UPDATE
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        static void Update()
        {
           
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //DELETE
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);

                String st = "DELETE FROM Teams WHERE TeamId =" + txtId.Text;

                SqlCommand sqlcom = new SqlCommand(st, myConnection);
                try
                {
                    sqlcom.ExecuteNonQuery();
                    MessageBox.Show("delete successful");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        static void Delete()
        {
            try
            {
                string connectionString = "Server=INSTRUCTORIT;Database=TournamentManager;User Id=ProfileUser;Password=ProfileUser2019";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("DELETE FROM Teams " +
                            "WHERE TeamId=@TeamId", conn))
                    {
                        cmd.Parameters.AddWithValue("@TeamId", 1);

                        int rows = cmd.ExecuteNonQuery();

                        //rows number of record got deleted
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
    }
}
