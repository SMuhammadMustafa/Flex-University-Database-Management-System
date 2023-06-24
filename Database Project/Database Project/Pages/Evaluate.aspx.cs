using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Project_DB.Pages
{
    public partial class Evaluate : System.Web.UI.Page
    {
        string courseCode;
        string sectionName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["courseCode"] != null)
                {

                    courseCode = Request.QueryString["courseCode"];
                    string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
                    string userId = Request.Cookies["UserInfo"]["ID"];

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string courseQuery = "SELECT CT.courseCode, C.courseName, S.sectionName, S.sectionID FROM COURSETEACHING CT JOIN COURSES C ON CT.courseCode = C.courseCode JOIN SECTIONS S ON CT.sectionID = S.sectionID WHERE CT.courseCode = @courseCode;";
                        SqlCommand courseCommand = new SqlCommand(courseQuery, connection);
                        courseCommand.Parameters.AddWithValue("@courseCode", courseCode);
                        SqlDataReader courseReader = courseCommand.ExecuteReader();

                        if (courseReader.Read())
                        {
                            courseCodeLabel.Text = courseReader["courseCode"].ToString();
                            courseNameLabel.Text = courseReader["courseName"].ToString();
                            sectionLabel.Text = courseReader["sectionName"].ToString();
                            sectionName = courseReader["sectionID"].ToString();
                        }

                        courseReader.Close();
                        connection.Close();
                    }

                    string query = "SELECT USERS.userID, USERS.name FROM USERS JOIN COURSETAKING ON COURSETAKING.STUDENTID = USERS.userID JOIN COURSETEACHING ON COURSETAKING.courseCode = COURSETEACHING.courseCode WHERE COURSETAKING.courseCode = @courseCode AND teacherID = @username AND COURSETAKING.sectionID = @sec";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@courseCode", courseCode);
                        command.Parameters.AddWithValue("@username", userId);
                        command.Parameters.AddWithValue("@sec", sectionName);

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
                PopulateRollNumber();
            }
        }

        protected void PopulateRollNumber()
        {
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            string userId = Request.Cookies["UserInfo"]["ID"];
            string courseCode = Request.QueryString["courseCode"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT USERS.userID, USERS.name FROM USERS JOIN COURSETAKING ON COURSETAKING.STUDENTID = USERS.userID JOIN COURSETEACHING ON COURSETAKING.courseCode = COURSETEACHING.courseCode WHERE COURSETAKING.courseCode = @courseCode AND teacherID = @username AND COURSETAKING.sectionID = @sec";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", userId);
                    command.Parameters.AddWithValue("@courseCode", courseCode);
                    command.Parameters.AddWithValue("@sec", sectionName);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RollNumber1.Items.Clear();
                        RollNumber1.Items.Add(new ListItem("-", ""));

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

        protected void OnSearchButton_Click(object sender, EventArgs e)
        {
            string evalName = inputEvalName1.Text;
            int totalMarks = int.Parse(inputTotalMarks1.Text);
            string rollNumber = RollNumber1.SelectedItem.Text;
            int obtainedMarks = int.Parse(obtainedMarks1.Text);
            string courseCode = Request.QueryString["courseCode"];

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO MARKS (studentID, courseCode, obtainedMarks, totalMarks, assesmentType, semester) VALUES (@studentId, @courseCode, @obtainedMarks, @totalMarks, @assessmentType, (SELECT semester FROM COURSETAKING WHERE studentID = @studentId AND COURSECODE =@courseCode))";

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@studentId", rollNumber);
                insertCommand.Parameters.AddWithValue("@courseCode", courseCode);
                insertCommand.Parameters.AddWithValue("@obtainedMarks", obtainedMarks);
                insertCommand.Parameters.AddWithValue("@totalMarks", totalMarks);
                insertCommand.Parameters.AddWithValue("@assessmentType", evalName);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

            successLabel.Text = "Marks inserted successfully!";
        }
    }
}