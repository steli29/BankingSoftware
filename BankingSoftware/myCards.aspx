<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="myCards.aspx.cs" Inherits="BankingSoftware.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./customCss/cards.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container d-flex justify-content-center text-white mt-5">
    <div class="wrapper">
        <div class="card">
            <div class="upper">
                <h6>Credit</h6>
                <img class="mb-1 img-fluid img-responsive card-image" src="https://i.imgur.com/XN4Josy.png">
            </div>
            <div class="lower">
                <h5>Samuel Richard</h5> <span>4564 - 3432 - 3434 - 1236</span>
                <h6>09/12</h6>  
            </div>
        </div>
    </div>
</div>


</asp:Content>
