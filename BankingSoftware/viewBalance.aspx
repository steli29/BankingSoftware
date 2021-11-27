<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="viewBalance.aspx.cs" Inherits="BankingSoftware.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="customCss/StyleSheet1.css" rel="stylesheet" />

    <h2 class="display-1 font-monospace" style="text-align:center">Account</h2>
    <h2 style="color:cornflowerblue; text-align:left;float:left;" class="small-text">Your Balance is: </h2>
    <h1 style="color:coral" class="display-3 font-monospace big-text">15200</h1>
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
    <tr>
      <th scope="row">1</th>
      <td>27.12.2020</td>
      <td>Zaplata ot firma</td>
      <td style="color:green">1000</td>
    </tr>
    <tr>
      <th scope="row">2</th>
      <td>20.12.2020</td>
      <td>Smetki post terminal</td>
      <td style="color:red">320</td>
    </tr>
    <tr>
      <th scope="row">3</th>
      <td>12.12.2020</td>
      <td>Zaplata ot firma</td>
      <td style="color:green">1000</td>
    </tr>
    <tr>
      <th scope="row">4</th>
      <td>24.11.2020</td>
      <td>Smetka Fantastico</td>
      <td style="color:red">128</td>
    </tr>
  </tbody>
</table>
</div>
</asp:Content>
