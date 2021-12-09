<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="applyforloan.aspx.cs" Inherits="BankingSoftware.applyforloan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mt-3 col-md-6 mx-auto">
        <div class="card card-body">
            <div class="row">
                <div class="col">
                    <center>
                        <h2>Loan Application</h2>    
                        <hr>
                    </center>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Full Name</label>
                    <div class="form-group">
                        <asp:TextBox ID="FullName" CssClass="form-control" runat="server" placeholder="Full Name"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Contact Number</label>                
                    <div class="form-group">
                        <asp:TextBox ID="ContactNumber" CssClass="form-control" runat="server" placeholder="Contact Number"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Password</label>                
                    <div class="form-group">
                        <asp:TextBox ID="Pswrd" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>   
            </div>
            <div class="row mt-2">
                <div class="col-md-6 mx-auto">
                    <label>Net Monthly Income</label>
                    <div class="form-group">
                        <asp:TextBox ID="NMI" CssClass="form-control" runat="server" placeholder="Net Monthly Income"></asp:TextBox>
                    </div>
                </div>
                 <div class="col-md-6 mx-auto">
                    <label>Needed Amount</label>
                    <div class="form-group">
                        <asp:TextBox ID="MoneyLoan" CssClass="form-control" runat="server" placeholder="Needed Amount"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 mx-auto">
                    <label>Net Monthly Income</label>
                    <div class="form-group" >
                        <asp:DropDownList ID="Loans" CssClass="form-control" runat="server">
                            <asp:ListItem Text="I'm looking for" Value="imlookingfor" />
                            <asp:ListItem Text="PersonalLoan" Value="PersonalLoan" />
                            <asp:ListItem Text="CarLoan" Value="CarLoan" />
                            <asp:ListItem Text="HomeLoan" Value="HomeLoan" />
                            <asp:ListItem Text="EducationLoan" Value="EducationLoan" />          
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group" >
                <asp:Button  ID="SubmitLoan" OnClick="SubmitLoan_Click" CssClass="btn btn-info col-12 mt-5 btn-lg" runat="server" Text="Submit"/>
            </div>
        </div>
        <a href="viewBalance.aspx"><< Back to Main</a><br><br>
    </div> 
</asp:Content>
