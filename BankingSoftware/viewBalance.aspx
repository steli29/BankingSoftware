<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="viewBalance.aspx.cs" Inherits="BankingSoftware.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="customCss/StyleSheet1.css" rel="stylesheet" />
    <h2 class="font-monospace" style="text-align:center">Account</h2>

    <div class="card card-body col-md-4 mx-auto">
            <h2 style="color:cornflowerblue; text-align:center;float:left; text-decoration:underline" class="small-text">Your Balance</h2>
            <h1 style="color:coral; text-align:center; font-style:italic" class="font-monospace"></h1>
            <asp:Repeater ID="Balance" runat="server">
                <ItemTemplate>
                    <div>
                        <h5><%#Eval("balance")%></h5>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
    </div>
    
    <hr style="clear:both"/>    
    <br />    <br /><br />
    
    <h1 class="display-5 font-monospace" style="text-align:center">Your last transactions</h1>
 
    <div class="table-div">
        <div class="mb-1 buttons-pos">
    <asp:Button ID="Button1" class="btn btn-outline-success" runat="server" Text="All" />
    <asp:Button ID="Button2" class="btn btn-outline-primary" runat="server" Text="Income" />
    <asp:Button ID="Button3" class="btn btn-outline-danger" runat="server" Text="Outcome" />
    </div>

    <table class="table table-info">
  <thead>
    <tr>
      <th scope="col" style="width: 2%">#</th>
      <th scope="col" style="width: 5%">Date</th>
      <th scope="col" style="width: 40%">Info</th>
      <th scope="col" style="width: 5%">Price</th>
    </tr>
  </thead>
  <tbody>
      <asp:Repeater ID="Transaction" runat="server">
        <ItemTemplate>
            <tr>
                <th scope="row">1</th>
                <td>27.12.2020</td>
                <td><%#Eval("info")%></td>
                <td style="color:green"><%#Eval("transaction_amount")%></td>
            </tr>
         </ItemTemplate>
      </asp:Repeater>
  </tbody>
</table>
</div>
</asp:Content>
