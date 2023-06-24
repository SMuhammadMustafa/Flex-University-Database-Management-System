using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DB_Project.Pages
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateSections();
            }
        }

        protected void PopulateSections()
        {
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string query = "SELECT SECTIONNAME, SECTIONID FROM SECTIONS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    section.DataSource = dataTable;
                    section.DataTextField = "SECTIONNAME";
                    section.DataValueField = "SECTIONID";
                    section.DataBind();

                    section.Items.Insert(0, new ListItem("-", "NULL"));
                }

            }
        }

        protected void OnRegister(object sender, EventArgs e)
        {
            string sName = fullname.Text;
            string sID = rollnumber.Text;
            string mail = email.Text;
            string pass = password.Text;
            string nic = cnic.Text;
            string birth = dob.Text;
            string gen = gender.Text;
            string ph = phone.Text;
            string add = address.Text;
            string camp = campus.Text;

            string sec = section.SelectedValue;
            string deg = degree.SelectedValue;
            string dept = department.SelectedValue;

            string year = DateTime.Now.Year.ToString();

            string query = "INSERT INTO USERS (USERID, NAME, CNIC, DOB, GENDER, EMAIL, PHONE, ADDRESS, CAMPUS) VALUES (@sID, @sName, @nic, @birth, @gen, @mail, @ph, @add, @camp)";

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.Parameters.AddWithValue("@sName", sName);
                insertCommand.Parameters.AddWithValue("@mail", mail);
                insertCommand.Parameters.AddWithValue("@nic", nic);
                insertCommand.Parameters.AddWithValue("@birth", birth);
                insertCommand.Parameters.AddWithValue("@gen", gen);
                insertCommand.Parameters.AddWithValue("@ph", ph);
                insertCommand.Parameters.AddWithValue("@add", add);
                insertCommand.Parameters.AddWithValue("@camp", camp);
                insertCommand.ExecuteNonQuery();

                connection.Close();

                query = "INSERT INTO STUDENTS (USERID, DEGREEPROGRAM, PARENTSECTIONID, PROGRAM) VALUES (@sID, @dept, @sec, @deg)";

                connection.Open();

                insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.Parameters.AddWithValue("@dept", dept);
                insertCommand.Parameters.AddWithValue("@sec", sec);
                insertCommand.Parameters.AddWithValue("@deg", deg);
                insertCommand.ExecuteNonQuery();

                connection.Close();

                query = "INSERT INTO STUDENTRECORD (STUDENTID, CURRENTSEMESTER, CREDITHRS, WARNINGCOUNT, DEGREESTARTYEAR) VALUES (@sID, 1, 0, 0, @year)";

                connection.Open();

                insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.Parameters.AddWithValue("@year", year);
                insertCommand.ExecuteNonQuery();

                connection.Close();

                query = "INSERT INTO LOGINDETAILS (USERID, PASSWORD) VALUES (@sID, @pass)";

                connection.Open();

                insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.Parameters.AddWithValue("@pass", pass);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Student Registered successfully!";
        }
        
        protected void OnRemove(object sender, EventArgs e)
        {
            string sID = rollnumber.Text;

            string query = "DELETE FROM STUDENTRECORD WHERE STUDENTID = @sID";

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.ExecuteNonQuery();

                connection.Close();

                query = "DELETE FROM STUDENTS WHERE USERID = @sID";

                connection.Open();

                insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.ExecuteNonQuery();

                connection.Close();

                query = "DELETE FROM USERS WHERE USERID = @sID";

                connection.Open();

                insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Student Removed successfully!";
        }
    }
}