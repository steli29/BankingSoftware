<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="myCards.aspx.cs" Inherits="BankingSoftware.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./customCss/cards.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="col-lg-2 col-md-2 col-xs-12 col-sm-6 mx-auto"> 
<div class="row">
    <div class="col">
        <center>
            <h4>My cards</h4>
            <hr>
        </center>
    </div>
</div>
    </div>
    <asp:Button ID="Button1" runat="server" class="section2_btn btn22" Text="Request new card" OnClick="Button1_Click"/>
<div class="wrapper">
    <div class="card">
        <div class="upper">
            <h6>Debit</h6>
            <h4>Visa</h4>
        </div>
        <div class="lower">
            <h5>Samuel Richard</h5> <span>4564 - 3432 - 3434 - 1236</span>
            <h6>09/22</h6>
        </div>
    </div>

    <div class="card">
        <div class="upper">
            <h6>Credit</h6>
            <h4>Visa</h4>
        </div>
        <div class="lower">
            <h5>Samuel Richard</h5> <span>4564 - 3432 - 3434 - 1236</span>
            <h6>09/22</h6>
        </div>
    </div>
</div>

</asp:Content>
