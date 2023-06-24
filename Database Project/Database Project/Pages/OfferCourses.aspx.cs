using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Project_DB.Pages
{
    public partial class OfferCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCourses();
            }
        }

        protected void PopulateCourses()
        {

            string userId = Request.Cookies["UserInfo"]["ID"];

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string query = "SELECT CONCAT(COURSES.[COURSECODE], ' - ', COURSES.[COURSENAME]) AS CNAME, COURSES.[COURSECODE] FROM COURSES";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", userId);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    prereqDropdown.DataSource = dataTable;
                    prereqDropdown.DataTextField = "CNAME";
                    prereqDropdown.DataValueField = "COURSECODE";
                    prereqDropdown.DataBind();

                    prereqDropdown.Items.Insert(0, new ListItem("-", "NULL"));
                }

            }
        }

        protected void OnRegister(object sender, EventArgs e)
        {
            string cCode = courseCode.Text;
            string cName = courseName.Text;
            string hrs = crdHrs.Text;
            string preReq = prereqDropdown.SelectedValue;
            string type = electiveAssigned.SelectedValue;
            string sem = semester.SelectedValue;

            string query = "INSERT INTO COURSES (COURSECODE, COURSENAME, CREDITHRS, PREREQUISITE, ELECTIVEASSIGNED, SEMESTER) VALUES (@cCode, @cName, @hrs, @preReq, @type, @sem)";

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(query, connection);
                insertCommand.Parameters.AddWithValue("@cCode", cCode);
                insertCommand.Parameters.AddWithValue("@cName", cName);
                insertCommand.Parameters.AddWithValue("@hrs", hrs);
                insertCommand.Parameters.AddWithValue("@preReq", preReq);
                insertCommand.Parameters.AddWithValue("@type", type);
                insertCommand.Parameters.AddWithValue("@sem", sem);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Course Registered successfully!";
        }
    }
}