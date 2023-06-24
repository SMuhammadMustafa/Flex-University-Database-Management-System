using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Database_Project.Pages
{
    public partial class SetCourseDistribution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string courseCode = Request.QueryString["courseCode"];
            string query = "SELECT COURSECODE, COURSENAME FROM COURSES WHERE COURSECODE = @courseCode";
            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand courseCommand = new SqlCommand(query, connection);
                courseCommand.Parameters.AddWithValue("@courseCode", courseCode);
                SqlDataReader courseReader = courseCommand.ExecuteReader();

                if (courseReader.Read())
                {
                    courseCodeLabel.Text = courseReader["courseCode"].ToString();
                    courseNameLabel.Text = courseReader["courseName"].ToString();
                }

                courseReader.Close();
                connection.Close();
            }

        }

        protected void OnButtonClick(object sender, EventArgs e)
        {
            string assign = assignment.Text;
            string quizzes = quiz.Text;
            string finals = final.Text;
            string sess1 = mid1.Text;
            string sess2 = mid2.Text;
            string proj = project.Text;
            string assignCount = assignmentCount.Text;
            string quizzesCount = quizCount.Text;
            string courseCode = Request.QueryString["courseCode"];
            string teacherID = Request.Cookies["UserInfo"]["ID"];

            string query = "INSERT INTO COURSEMARKSDISTRIBUTION (COURSECODE, TEACHERID, PROJECTWEIGHTAGE, QUIZCOUNT, QUIZWEIGHTAGE, ASSIGNMENTCOUNT, ASSIGNMENTWEIGHTAGE, MID1WEIGHTAGE, MID2WEIGHTAGE, FINALWEIGHTAGE) VALUES (@courseCode, @teacherID, @proj, @quizzesCount, @quizzes, @assignCount, @assign, @sess1, @sess2, @finals)";

            string connectionString = "Data Source=DESKTOP-CD3BJVN\\SQLEXPRESS;Initial Catalog=PROJECT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand courseCommand = new SqlCommand(query, connection);
                courseCommand.Parameters.AddWithValue("@courseCode", courseCode);
                courseCommand.Parameters.AddWithValue("@teacherID", teacherID);
                courseCommand.Parameters.AddWithValue("@assign", assign);
                courseCommand.Parameters.AddWithValue("@quizzes", quizzes);
                courseCommand.Parameters.AddWithValue("@finals", finals);
                courseCommand.Parameters.AddWithValue("@sess1", sess1);
                courseCommand.Parameters.AddWithValue("@sess2", sess2);
                courseCommand.Parameters.AddWithValue("@proj", proj);
                courseCommand.Parameters.AddWithValue("@assignCount", assignCount);
                courseCommand.Parameters.AddWithValue("@quizzesCount", quizzesCount);
                SqlDataReader courseReader = courseCommand.ExecuteReader();
                courseReader.Close();
                connection.Close();

                successLabel.Text = "Marks inserted successfully!";
            }
        }
    }
}