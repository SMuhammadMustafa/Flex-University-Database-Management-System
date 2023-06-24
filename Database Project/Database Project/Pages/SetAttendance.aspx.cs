using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Database_Project.Pages
{
    public partial class SetAttendance : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCourses();
                PopulateRollNumber("-");
                PopulateSections("-");
            }
        }

        protected void GetTable()
        {
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string courseCode = course.SelectedValue;

            string query = "SELECT USERS.userID, USERS.name FROM USERS JOIN STUDENTS ON USERS.userID = STUDENTS.userID JOIN COURSETAKING ON STUDENTS.userID = COURSETAKING.studentID WHERE COURSETAKING.courseCode = @CourseCode;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CourseCode", courseCode);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string rollNumber = reader["userID"].ToString();
                    string name = reader["name"].ToString();

                    TableRow tableRow = new TableRow();
                    TableCell rollNumberCell = new TableCell();
                    TableCell nameCell = new TableCell();

                    rollNumberCell.Text = rollNumber;
                    nameCell.Text = name;

                    tableRow.Cells.Add(rollNumberCell);
                    tableRow.Cells.Add(nameCell);

                    table1.Controls.Add(tableRow);

                }

                reader.Close();
                connection.Close();
            }
        }

        protected void PopulateRollNumber(string section1)
        {
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string userId = Request.Cookies["UserInfo"]["ID"];
            string courseCode = course.SelectedValue;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT USERS.userID, USERS.name FROM USERS JOIN COURSETAKING ON COURSETAKING.STUDENTID = USERS.userID JOIN COURSETEACHING ON COURSETAKING.courseCode = COURSETEACHING.courseCode WHERE COURSETAKING.courseCode = @courseCode AND teacherID = @username AND COURSETAKING.sectionID = @sec";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", userId);
                    command.Parameters.AddWithValue("@courseCode", courseCode);
                    command.Parameters.AddWithValue("@sec", section1);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RollNumber1.Items.Clear();

                        while (reader.Read())
                        {
                            ListItem item = new ListItem
                            {
                                Text = reader["USERID"].ToString(),
                                Value = reader["USERID"].ToString()
                            };

                            RollNumber1.Items.Add(item);
                        }
                    }
                }
            }
        }

        protected void PopulateCourses()
        {

            string userId = Request.Cookies["UserInfo"]["ID"];

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string query = "SELECT DISTINCT CONCAT(COURSES.[COURSECODE], ' - ', COURSES.[COURSENAME]) AS CNAME, COURSES.[COURSECODE] FROM [COURSETEACHING] INNER JOIN COURSES ON COURSETEACHING.COURSECODE = COURSES.COURSECODE WHERE [TEACHERID] = @username";

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
                    course.DataSource = dataTable;
                    course.DataTextField = "CNAME";
                    course.DataValueField = "COURSECODE";
                    course.DataBind();

                    course.Items.Insert(0, new ListItem("Select an item", ""));
                }

            }
        }

        protected void PopulateSections(string courseSelected)
        {            
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string userId = Request.Cookies["UserInfo"]["ID"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SECTIONNAME, SECTIONS.SECTIONID FROM COURSETEACHING INNER JOIN SECTIONS ON COURSETEACHING.SECTIONID = SECTIONS.SECTIONID WHERE TEACHERID = @username AND COURSECODE = @course";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", userId);
                    command.Parameters.AddWithValue("@course", courseSelected);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        section.Items.Clear();

                        section.Items.Add(new ListItem("-", ""));

                        while (reader.Read())
                        {
                            ListItem item = new ListItem
                            {
                                Text = reader["SECTIONNAME"].ToString(),
                                Value = reader["SECTIONID"].ToString()
                            };

                            section.Items.Add(item);
                        }
                    }
                }
            }
        }

        protected void OnChangeCourse(object sender, EventArgs e)
        {
            string selectedSemester = course.SelectedValue;

            if (!string.IsNullOrEmpty(selectedSemester))
            {
                PopulateSections(selectedSemester);
            }
            else
            {
                section.Items.Clear();
            }
        }

        protected void OnChangeSection(object sender, EventArgs e)
        {
            string selectedSemester = section.SelectedValue;

            if (!string.IsNullOrEmpty(selectedSemester))
            {
                PopulateRollNumber(selectedSemester);
            }
            else
            {
                RollNumber1.Items.Clear();
            }

            string userId = Request.Cookies["UserInfo"]["ID"];
            string courseCode = course.SelectedValue;
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string query = "SELECT USERS.userID, USERS.name FROM USERS JOIN COURSETAKING ON COURSETAKING.STUDENTID = USERS.userID JOIN COURSETEACHING ON COURSETAKING.courseCode = COURSETEACHING.courseCode WHERE COURSETAKING.courseCode = @courseCode AND teacherID = @username AND COURSETAKING.sectionID = @sec";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CourseCode", courseCode);
                command.Parameters.AddWithValue("@username", userId);
                command.Parameters.AddWithValue("@sec", selectedSemester);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string rollNumber = reader["userID"].ToString();
                    string name = reader["name"].ToString();

                    TableRow tableRow = new TableRow();
                    TableCell rollNumberCell = new TableCell();
                    TableCell nameCell = new TableCell();

                    rollNumberCell.Text = rollNumber;
                    nameCell.Text = name;

                    tableRow.Cells.Add(rollNumberCell);
                    tableRow.Cells.Add(nameCell);

                    table1.Controls.Add(tableRow);

                }

                reader.Close();
                connection.Close();
            }
        }


        protected void OnSearchButton_Click(object sender, EventArgs e)
        {
            string lecture = lectureNum.Text;
            string studentID = RollNumber1.SelectedValue;
            string subjectCode = course.SelectedValue;
            string attendance = status.SelectedValue;
            string dur = duration.Text;
            string dateAndTime = DateTime.Now.ToString();

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO ATTENDANCE (LECTURENUM, STUDENTID, SUBJECTCODE, STATUSATTENDANCE, DURATION, DATEANDTIME) VALUES (@lecture, @studentID, @subjectCode, @attendance, @dur, @dateAndTime)";

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@lecture", lecture);
                insertCommand.Parameters.AddWithValue("@studentID", studentID);
                insertCommand.Parameters.AddWithValue("@subjectCode", subjectCode);
                insertCommand.Parameters.AddWithValue("@attendance", attendance);
                insertCommand.Parameters.AddWithValue("@dur", dur);
                insertCommand.Parameters.AddWithValue("@dateAndTime", dateAndTime);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Attendance set successfully!";
        }
    }
}