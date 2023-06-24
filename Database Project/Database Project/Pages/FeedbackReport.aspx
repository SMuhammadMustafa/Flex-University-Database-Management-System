﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedbackReport.aspx.cs" Inherits="Project_DB.Pages.FeedbackReport" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="icon" href="favicon.ico" type="image/x-icon">
    <title>Flex Academic Portal - Feedback Report</title>
    <style>
        body {
            background-color: rgba(200, 210, 200, 1.0);
            font-family: "Montserrat", sans-serif;
            color: #333;
            margin: 0;
            padding: 0px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }

        .overlay {
            background-color: rgba(255, 255, 255, 0.6);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: -1;
        }

        .flex-text {
            font-size: 42px;
            font-weight: bold;
            color: #44ac48;
            margin-left: 0px;
            text-align: center;
            text-shadow: 1.5px 1.5px #999;
            padding: 0px;
            padding-top: 20px;
            width: 400px;
        }

        h1 {
            color: #44ac48;
            text-align: center;
            font-size: 36px;
            margin: 0;
            text-shadow: 1px 1px #999;
        }

        select {
            background-color: rgba(255, 255, 255, 1.0);
            border: none;
            border-radius: 7px;
            color: #333;
            font-family: "Montserrat", sans-serif;
            font-size: 16px;
            text-align: center;
            margin-bottom: 20px;
            margin-top: 20px;
            padding: 12px;
            width: 96%;
            box-shadow: 1px 1px #999;
        }

        input {
            background-color: rgba(255, 255, 255, 1.0);
            border: none;
            border-radius: 7px;
            color: #333;
            font-family: "Montserrat", sans-serif;
            font-size: 16px;
            margin-bottom: 20px;
            margin-top: 20px;
            padding: 12px;
            width: 92.5%;
            box-shadow: 1px 1px #999;
        }

        .linkbar {
            background-color: rgba(250, 250, 250, 1.0);
            padding: 30px;
            width: 250px;
            border-radius: 10px;
            box-shadow: 5px 5px 20px rgba(0, 0, 0, 0.2);
            text-align: center;
            position: fixed;
            left: 0;
            top: 0;
            bottom: 0;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }

        .linkbar {
            left: -250px;
            transition: left 0.5s ease-in-out;
        }

        .linkbar:hover,
        .linkbar:hover ~ .overlay,
        .link-btn:hover ~ .linkbar {
            left: 0;
        }

        .link-btn {
            cursor: pointer;
        }

        .linkbar a {
            display: block;
            margin: 10px;
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            color: #44ac48;
            text-decoration: none;
            padding: 10px;
            width: 200px;
            border-radius: 5px;
            box-shadow: 3px 3px 10px rgba(0, 0, 0, 0.2);
            transition: all 0.3s ease;
        }

        .linkbar a:hover {
            background-color: #44ac48;
            color: #fff;
            box-shadow: 2px 2px 3px rgba(0, 0, 0, 0.2), 0px 0px 10px rgba(0, 0, 0, 0.2);
            transform: translateY(-5px);
            -webkit-transform: translateY(-2px);
            transform: translateY(-2px);
            -webkit-transform: translateY(-2px);
        }

        .profile {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 50px;
        }

        .section {
            background-color: rgba(250, 250, 250, 1.0);
            padding: 30px;
            width: 700px;
            border-radius: 10px;
            box-shadow: 5px 5px 20px rgba(0, 0, 0, 0.2);
            text-align: center;
            position: relative;
            z-index: 1;
            margin: 20px;
            margin-left: 0px;
        }

        .section h2 {
            color: #44ac48;
            text-align: center;
            font-size: 24px;
            margin: 0;
            text-shadow: 1px 1px #999;
        }

        .section ul {
            list-style: none;
            margin: 0;
            padding: 0;
            text-align: left;
        }

        li {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }

            li span {
                font-weight: bold;
            }

        .gpa {
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
            color: #44ac48;
            text-shadow: 1px 1px #999;
        }

        .form-container {
            margin-top: 50px;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .cName {
            color: #44AC48;
            text-align: center;
            font-size: 24px;
            margin: 0;
            text-shadow: 1px 1px #999;
            font-weight: bold;
        }

        .table-container {
            margin-top: 20px;
        }

        table {
            width: 75%;
            border-collapse: collapse;
            margin-top: 20px;
            margin-left: 80px;
        }

        th,
        td {
            padding: 10px;
            text-align: center;
            border-bottom: 1px solid #aaa;
        }

        th {
            background-color: #44ac48;
            color: #fff;
        }

        button {
            background-color: #44AC48;
            border: none;
            border-radius: 7px;
            box-shadow: 3px 3px 10px rgba(0, 0, 0, 0.2);
            color: #FFF;
            cursor: pointer;
            font-family: "Montserrat", sans-serif;
            font-size: 18px;
            font-weight: bold;
            letter-spacing: 2px;
            margin-top: 5px;
            padding: 15px 30px;
            text-transform: uppercase;
            transition: all 0.3s ease;
            display: block;
            margin: 10px auto;
        }

        button:hover {
            background-color: #E5E5E5;
            color: #44AC48;
            box-shadow: 2px 2px 3px rgba(0, 0, 0, 0.2), 0px 0px 10px rgba(0, 0, 0, 0.2);
            transform: translateY(-5px);
            -webkit-transform: translateY(-2px);
            transform: translateY(-2px);
            -webkit-transform: translateY(-2px);
        }
    </style>
</head>
<body>
    <div class="overlay"></div>
    <h1 class="flex-text">Feedback Report</h1>
    <div class="linkbar">
      <a href="FacultyHome.aspx" class="link-btn">Home</a>
      <a href="SetMarksDistribution.aspx" class="link-btn">Set Marks Distribution</a>
      <a href="SetAttendance.aspx" class="link-btn">Set Attendance</a>        
      <a href="Evaluations.aspx" class="link-btn">Evaluations</a>        
      <a href="GradeReport.aspx" class="link-btn">Grade Report</a> 
      <a href="GradeCount.aspx" class="link-btn">Grade Count</a>        
      <a href="FeedbackReport.aspx" class="link-btn">Feedback Report</a>
      <a href="EvaluationReport1.aspx">Evaluation Report</a>
      <a href="LogIn.aspx" class="link-btn">Logout</a>  
    </div>
    <div class="profile">
        <div class="section">
            <div class="form-group">
                <form runat="server">
                    <asp:DropDownList ID="category" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="UpdateFeedback" AutoPostBack="true">
                        <asp:ListItem Text="Neatness" Value="Neatness" />
                        <asp:ListItem Text="Professional Attire" Value="Professional Attire" />
                        <asp:ListItem Text="Punctuality" Value="Punctuality" />
                        <asp:ListItem Text="Communication Skills" Value="Communication Skills" />
                        <asp:ListItem Text="Overall Impression" Value="Overall Impression" />
                        <asp:ListItem Text="Preparation and Organization" Value="Preparation and Organization" />
                        <asp:ListItem Text="Use of Technology" Value="Use of Technology" />
                        <asp:ListItem Text="Availability" Value="Availability" />
                        <asp:ListItem Text="Classroom Management" Value="Classroom Management" />
                        <asp:ListItem Text="Responsiveness" Value="Responsiveness" />
                        <asp:ListItem Text="Clarity and Explanation" Value="Clarity and Explanation" />
                        <asp:ListItem Text="Interaction and Engagement" Value="Interaction and Engagement" />
                        <asp:ListItem Text="Relevance to Course Objectives" Value="Relevance to Course Objectives" />
                        <asp:ListItem Text="Assessment Methods" Value="Assessment Methods" />
                        <asp:ListItem Text="Feedback and Evaluation" Value="Feedback and Evaluation" />
                        <asp:ListItem Text="Respect Towards Students" Value="Respect Towards Students" />
                        <asp:ListItem Text="Approachability and Friendliness" Value="Approachability and Friendliness" />
                        <asp:ListItem Text="Openness to Discussion" Value="Openness to Discussion" />
                        <asp:ListItem Text="Handling of Questions and Concerns" Value="Handling of Questions and Concerns" />
                        <asp:ListItem Text="Punctuality and Availability" Value="Punctuality and Availability" />
                        <asp:ListItem Text="Comments" Value="Comments" />
                    </asp:DropDownList>
                    <asp:Label class="cName" runat="server" ID="label1"></asp:Label>
                    <asp:Table ID="results" runat="server"></asp:Table>
                </form>
            </div>
            <div class="table-container">
            </div>
        </div>
    </div>
</body>
</html>