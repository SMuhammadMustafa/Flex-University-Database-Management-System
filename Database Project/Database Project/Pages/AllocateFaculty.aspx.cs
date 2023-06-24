using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Project_DB.Pages
{
    public partial class AllocateFaculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCourses();
                PopulateSections();
                PopulateRollNumber();
            }
        }

        protected void PopulateCourses()
        {
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string query = "SELECT CONCAT(COURSES.[COURSECODE], ' - ', COURSES.[COURSENAME]) AS CNAME, COURSES.[COURSECODE] FROM COURSES";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    courseCode.DataSource = dataTable;
                    courseCode.DataTextField = "CNAME";
                    courseCode.DataValueField = "COURSECODE"; 
                    courseCode.DataBind();

                    courseCode.Items.Insert(0, new ListItem("-", "NULL"));
                }

            }
        }

        protected void PopulateRollNumber()
        {
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string query = "SELECT CONCAT(FACULTY.USERID, ' - ' , NAME) AS SNAME, FACULTY.USERID FROM FACULTY INNER JOIN USERS ON FACULTY.USERID = USERS.USERID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    facID.DataSource = dataTable;
                    facID.DataTextField = "SNAME";
                    facID.DataValueField = "USERID";
                    facID.DataBind();

                    facID.Items.Insert(0, new ListItem("-", "NULL"));
                }

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
                    secID.DataSource = dataTable;
                    secID.DataTextField = "SECTIONNAME";
                    secID.DataValueField = "SECTIONID";
                    secID.DataBind();

                    secID.Items.Insert(0, new ListItem("-", "NULL"));
                }

            }
        }

        protected void OnAllocate(object sender, EventArgs e)
        {
            string cCode = courseCode.SelectedValue;
            string sID = facID.SelectedValue;
            string sectID = secID.SelectedValue;

            string query = "INSERT INTO COURSETEACHING (COURSECODE, TEACHERID, SECTIONID) VALUES (@cCode, @sID, @secID)";

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@cCode", cCode);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.Parameters.AddWithValue("@secID", sectID);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Course Registered successfully!";
        }
    }
}