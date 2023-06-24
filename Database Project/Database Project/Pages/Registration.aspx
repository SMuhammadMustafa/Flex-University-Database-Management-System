<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="DB_Project.Pages.Registration" %>

<!DOCTYPE html>
<html>
  <head>
    <link rel="icon" href="favicon.ico" type="image/x-icon">
    <title>Flex Academic Portal - Registration/Removal</title>
    <style>
      
      body {
        background-color: rgba(200, 210, 200, 1.0);
        font-family: "Montserrat", sans-serif;
        color: #333;
        margin: 0;
        padding: 0px;
        height: 270vh;
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

      h1 {
        color: #44ac48;
        text-align: center;
        font-size: 36px;
        margin: 0;
        text-shadow: 1px 1px #999;
      }

      form {
        background-color: rgba(250, 250, 250, 1.0);
        padding: 50px;
        width: 500px;
        border-radius: 10px;
        box-shadow: 5px 5px 20px rgba(0, 0, 0, 0.2);
        text-align: center;
        position: relative;
        z-index: 1;
      }

      label {
        color: #454545;
        display: block;
        font-size: 18px;
        font-weight: bold;
        letter-spacing: 2px;
        margin-top: 20px;
        text-transform: uppercase;
        text-align: center;
      }

      #username {
        margin-bottom: 0;
      }

      #signup {
        margin-top: 50px;
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

      select {
        background-color: rgba(255, 255, 255, 1.0);
        border: none;
        border-radius: 7px;
        color: #333;
        font-family: "Montserrat", sans-serif;
        font-size: 16px;
        margin-bottom: 5px;
        margin-top: 20px;
        padding: 12px;
        width: 98%;
        box-shadow: 1px 1px #999;
      }

      input[type="submit"] {
        background-color: #44ac48;
        border: none;
        border-radius: 7px;
        box-shadow: 3px 3px 10px rgba(0, 0, 0, 0.2);
        color: #fff;
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
    background-color: #e5e5e5;
    color: #44ac48;
    box-shadow: 2px 2px 3px rgba(0, 0, 0, 0.2),
      0px 0px 10px rgba(0, 0, 0, 0.2);
    transform: translateY(-5px);
    -webkit-transform: translateY(-2px); 
    transform: translateY(-2px);
    -webkit-transform: translateY(-2px); 
  }

  
  .flex-text {
    font-size: 42px;
    font-weight: bold;
    color: #44ac48;
    margin: 30px;
    text-align: center;
    text-shadow: 1.5px 1.5px #999;
    padding: 20px;
    width: 400px;
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
    box-shadow: 2px 2px 3px rgba(0, 0, 0, 0.2),
      0px 0px 10px rgba(0, 0, 0, 0.2);
    transform: translateY(-5px);
    -webkit-transform: translateY(-2px); 
    transform: translateY(-2px);
    -webkit-transform: translateY(-2px); 
  }
</style>
</head>
<body>
    <div class="overlay"></div>
    <h1 class="flex-text">Registration/Removal</h1>
    <div class="linkbar">
      <a href="AdminHome.aspx" class="link-btn">Home</a>
      <a href="OfferCourses.aspx">Offer Courses</a>
      <a href="AllocateStudents.aspx">Allocate Students</a>
      <a href="AllocateFaculty.aspx">Allocate Faculty</a>
      <a href="Registration.aspx">Registration/Removal</a>
      <a href="Courses.aspx" class="link-btn">Courses</a>
      <a href="StudentsSections.aspx">Students Sections</a>
      <a href="CourseAllocation.aspx">Course Allocation</a>
      <a href="LogIn.aspx">Logout</a>
    </div>
    <form runat="server">
      <label for:"fullName">Full Name:</label>
      <asp:TextBox type="text" id="fullname" name="fullname" runat="server"/>
      <label for:"rollNumber">Roll Number:</label>
      <asp:TextBox type="text" id="rollnumber" name="rollnumber" runat="server"/>
      <label for:"email">E-mail:</label>
      <asp:TextBox type="email" id="email" name="email" runat="server"/>
      <label for:"password">Password:</label>
      <asp:TextBox type="password" id="password" name="password" runat="server"/>
      <label for:"cnic">CNIC:</label>
      <asp:TextBox type="text" id="cnic" runat="server"/>
      <label for:"dob">Date Of Birth:</label>
      <asp:TextBox type="text" id="dob" runat="server" placeholder="YYYY-MM-DD"/>
      <label for:"gender">Gender:</label>
      <asp:TextBox type="text" id="gender" runat="server"/>
      <label for:"phone">Phone:</label>
      <asp:TextBox type="text" id="phone" runat="server"/>
      <label for:"address">Address:</label>
      <asp:TextBox type="text" id="address" runat="server"/>
      <label for:"campus">Campus:</label>
      <asp:TextBox type="text" id="campus" runat="server"/>

      <label for:"section">Parent Section:</label>
      <asp:DropDownList id="section" runat="server">
      </asp:DropDownList>

      <label for:"degree">Degree:</label>
      <asp:DropDownList id="degree" runat="server">
        <asp:ListItem Text="Bachelor's" value="Bachelor"></asp:ListItem>
        <asp:ListItem Text="Master's" value="Master"></asp:ListItem>
        <asp:ListItem Text="phD" value="phD"></asp:ListItem>
      </asp:DropDownList>

      <label for:"department">Department:</label>
      <asp:DropDownList id="department" runat="server">
        <asp:ListItem Text="Computer Science" value="CS"></asp:ListItem>
        <asp:ListItem Text="Data Science" value="DS"></asp:ListItem>
        <asp:ListItem Text="Artificial Intelligence" value="AI"></asp:ListItem>
        <asp:ListItem Text="Cyber Security" value="CYS"></asp:ListItem>
        <asp:ListItem Text="Electrical Engineering" value="EE"></asp:ListItem>
      </asp:DropDownList>
      <asp:button id="signup" runat="server" Text="Register" OnClick="OnRegister"></asp:button>
      <asp:button id="delete" runat="server" Text="Remove" OnClick="OnRemove"></asp:button>
      <asp:Label ID="successLabel" runat="server"></asp:Label>
    </form>
    <hr>
  </body>
</html>