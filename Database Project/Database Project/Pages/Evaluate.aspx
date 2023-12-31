﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Evaluate.aspx.cs" Inherits="Project_DB.Pages.Evaluate" %>

<!DOCTYPE html>
    <html>
    <head>
        <link rel="icon" href="favicon.ico" type="image/x-icon">
        <title>Flex Academic Portal - Evaluate</title>
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

            input {
                background-color: rgba(255, 255, 255, 1.0);
                border: none;
                border-radius: 7px;
                color: #333;
                font-family: "Montserrat", sans-serif;
                font-size: 16px;
                margin-bottom: 10px;
                margin-top: 20px;
                padding: 12px;
                width: 60%;
                box-shadow: 1px 1px #999;
            }

            .flex-text {
                font-size: 42px;
                font-weight: bold;
                color: #44AC48;
                margin-left: 0px;
                text-align: center;
                text-shadow: 1.5px 1.5px #999;
                padding: 0px;
                padding-top: 20px;
                width: 400px;
            }

            h1 {
                color: #44AC48;
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
            width: 20%;
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
                color: #44AC48;
                text-decoration: none;
                padding: 10px;
                width: 200px;
                border-radius: 5px;
                box-shadow: 3px 3px 10px rgba(0, 0, 0, 0.2);
                transition: all 0.3s ease;
            }

                .linkbar a:hover {
                    background-color: #44AC48;
                    color: #FFF;
                    box-shadow: 2px 2px 3px rgba(0, 0, 0, 0.2), 0px 0px 10px rgba(0, 0, 0, 0.2);
                    transform: translateY(-5px);
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

                .section label {
                    font-weight: bold;
                }

                .section h2 {
                    color: #44AC48;
                    text-align: center;
                    font-size: 24px;
                    margin: 0;
                    margin-top: 10px;
                    margin-bottom: 20px;
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

            .fee-table {
                width: 700px;
                margin: 30px auto;
                border-collapse: collapse;
            }

                .fee-table th,
                .fee-table td {
                    padding: 10px;
                    text-align: center;
                    border: 1px solid #333;
                }

                .fee-table th {
                    background-color: #44AC48;
                    color: #FFF;
                }

                .fee-table td.submitted {
                    background-color: #44AC48;
                    color: #FFF;
                }

                .fee-table td.not-submitted {
                    background-color: #F00;
                    color: #FFF;
                }

                .cName {
            color: #44AC48;
            text-align: center;
            font-size: 24px;
            margin: 10px;
            text-shadow: 1px 1px #999;
            font-weight: bold;
        }

                input[type="submit"] {
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

            input[type="submit"]:hover {
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
        <h1 class="flex-text">Evaluate</h1>
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
                <form runat="server">
                    <asp:Label class="cName" ID="courseCodeLabel" runat="server"></asp:Label><br />
                    <asp:Label class="cName" ID="courseNameLabel" runat="server"></asp:Label><br />
                    <asp:Label class="cName" ID="sectionLabel" runat="server"></asp:Label><br />
                    <label>Evaluation Name: </label>
                    <br />
                    <asp:Textbox type="text" runat="server" id="inputEvalName1"></asp:Textbox>
                    <br />
                    <label>Total Marks: </label>
                    <br />
                    <asp:Textbox type="text" runat="server" id="inputTotalMarks1" />
                    <br />
                    <label>Roll Number: </label>
                    <br />
                    <asp:DropDownList runat="server" id="RollNumber1" />
                    <br />
                    <label>Obtained Marks: </label>
                    <br />
                    <asp:Textbox type="text" runat="server" id="obtainedMarks1" />
                    <br />                   
                        <asp:table class="fee-table" ID="table1" runat="server">
                            <asp:TableHeaderRow runat="server">
                                <asp:TableHeaderCell>
                                    Roll Number
                                </asp:TableHeaderCell>  
                                <asp:TableHeaderCell>
                                    Name
                                </asp:TableHeaderCell>  
                            </asp:TableHeaderRow>
                        </asp:table>
                    <asp:Button runat="server" ID="button" Text="Add" OnClick="OnSearchButton_Click" />
                    <br />
                    <asp:Label ID="successLabel" runat="server"></asp:Label>
                </form>
            </div>
        </div>

    </body>
    </html>