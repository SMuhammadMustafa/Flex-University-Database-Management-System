using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Project_DB.Pages
{
    public partial class AllocateStudents : System.Web.UI.Page
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
            string query = "SELECT CONCAT(STUDENTS.USERID, ' - ' , NAME) AS SNAME, STUDENTS.USERID FROM STUDENTS INNER JOIN USERS ON STUDENTS.USERID = USERS.USERID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    rollNumber.DataSource = dataTable;
                    rollNumber.DataTextField = "SNAME";
                    rollNumber.DataValueField = "USERID";
                    rollNumber.DataBind();

                    rollNumber.Items.Insert(0, new ListItem("-", "NULL"));
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
                    sectionName.DataSource = dataTable;
                    sectionName.DataTextField = "SECTIONNAME";
                    sectionName.DataValueField = "SECTIONID";
                    sectionName.DataBind();

                    sectionName.Items.Insert(0, new ListItem("-", "NULL"));
                }

            }
        }

        protected void OnAllocate(object sender, EventArgs e)
        {
            string cCode = courseCode.SelectedValue;
            string sID = rollNumber.SelectedValue;
            string secID = sectionName.SelectedValue;

            string query = "INSERT INTO COURSETAKING (STUDENTID, COURSECODE, SECTIONID, ACTIVE, SEMESTER) VALUES (@sID, @cCode, @secID, 'Y', (SELECT CURRENTSEMESTER FROM STUDENTRECORD WHERE STUDENTID = @sID))";

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@cCode", cCode);
                insertCommand.Parameters.AddWithValue("@sID", sID);
                insertCommand.Parameters.AddWithValue("@secID", secID);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Course Registered successfully!";
        }

    }
}