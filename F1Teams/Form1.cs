using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace F1Teams
{
    public partial class Form1 : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["F1String"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlText = "select * from Teams";
            SqlCommand command = new SqlCommand(sqlText, connection);
            SqlDataReader reader = command.ExecuteReader();

            LstTeams.Items.Clear();

            int i = 0;
            while (reader.Read())
            {
                LstTeams.Items.Add(reader["TeamID"].ToString());
                LstTeams.Items[i].SubItems.Add(reader["TeamName"].ToString());
                i++;
            }
            connection.Close();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            try
            {
                int TeamID = Int32.Parse(txtID.Text);
                string TeamName = txtName.Text;

                string sqlText = "insert into Teams (TeamID, TeamName)" +
                    " values (@TeamID, @TeamName)";
                SqlCommand cmd = new SqlCommand(sqlText, connect);
                cmd.Parameters.AddWithValue("@TeamID", TeamID);
                cmd.Parameters.AddWithValue("@Teamname", TeamName);

                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            connect.Close();
        }
    }
}