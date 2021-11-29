<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BankingSoftware.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="customCss/StyleSheet1.css" rel="stylesheet" />

<div class="mt-3 col-md-10 mx-auto">
    <div class="card card-body" >
        <div class="row">
            <div class="col frame">
                <div class="opacity-50">
                    <img height="400" src="imgs/logo2.jpg" />
                </div>
                <div class="centered font-monospace"><h1 style="color:black">WELCOME TO OUR WEBSITE</h1><br /><h4 style="color:black">Please log-in to access your balance information.<br /> You are a new user? Hurry up and create account!</h4></div>
            </div>
            <div class="col">
                <div></div>
                <h1 class="font-monospace">Sign in</h1>
                <asp:TextBox CssClass="form-control col-2" Font-Size="X-Large" ID="TextBox1" Placeholder="Enter Username"  runat="server"></asp:TextBox><br />
                <asp:TextBox CssClass="form-control col-2" Font-Size="X-Large" TextMode="Password" Placeholder="Enter Password" ID="TextBox2" runat="server"></asp:TextBox><br />
                <asp:Button ID="Button1" class="btn btn-primary" Font-Size="Large" runat="server" Text="Sign In" /><br /><br>
                <asp:Button ID="Button2" class="btn btn-outline-warning" runat="server" Text="Forgot Password?" />
                <asp:Button ID="Button3" class="btn btn-outline-danger" runat="server" Text="Create Account" />
            </div>
        </div>
    </div>
</div>
</asp:Content>
