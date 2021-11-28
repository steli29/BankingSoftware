<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="applyforloan.aspx.cs" Inherits="BankingSoftware.applyforloan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mt-3 col-md-6 mx-auto">
        <div class="card card-body">
            <div class="row">
                <div class="col">
                    <center>
                         <h1>Loan Application</h1>    
                    </center>
                </div>
            </div>
        
        <asp:TextBox ID="FullName" CssClass="form-control" runat="server">Full Name</asp:TextBox>
         <br /> <?/br>
        <asp:TextBox ID="ContactNumber" CssClass="form-control" runat="server">Contact Number</asp:TextBox>
         <br /> <?/br>
        <asp:TextBox ID="CityName" CssClass="form-control" runat="server">City Name</asp:TextBox>
         <br /> <?/br>
        <asp:TextBox ID="NMI" CssClass="form-control" runat="server">Net Monthly Income</asp:TextBox>
         <br /> <?/br>


         <div class="form-group" >
          <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
              <asp:ListItem Text="I'm looking For" Value="imlookingfor" />
                            <asp:ListItem Text="PersonalLoan" Value="PersonalLoan" />
                            <asp:ListItem Text="CarLoan" Value="CarLoan" />
                            <asp:ListItem Text="HomeLoan" Value="HomeLoan" />
                            <asp:ListItem Text="EducationLoan" Value="EducationLoan" />
          
                        </asp:DropDownList>
             </div>

     
             <div class="form-group" >
        <asp:Button  ID="Button1" CssClass="btn btn-info col-12 mt-5 btn-lg" runat="server" Text="Submit"/>
                 </div>

    </div>

    <br>

        </div>
   
</asp:Content>
