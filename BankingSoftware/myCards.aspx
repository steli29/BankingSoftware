<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="myCards.aspx.cs" Inherits="BankingSoftware.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./customCss/cards.css" rel="stylesheet" type="text/css" />
    <script>
        function showCodes(event) {
            const pin = event.querySelector('#PIN');
            const sec = event.querySelector('#SEC');
            if (pin.style.display == 'none') {
                pin.style.display = 'block';
                sec.style.display = 'block';
            } else {
                pin.style.display = 'none';
                sec.style.display = 'none';
            }

            let name = '<%= Session["user_id"]%>';
            let password = '<%= Session["pass"]%>';

            alert(name + '' + password);
        }
    </script>
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
    <asp:Button ID="RequestCard" runat="server" class="section2_btn btn22" Text="Request new card" OnClick="RequestCard_Click"/>
<div class="wrapper">
<asp:Repeater ID="CardRepeater" runat="server">
    <ItemTemplate>
        <div class="card">
            <div class="upper">
                <h6>Debit</h6>
                <h4>Visa</h4>
            </div>
            <div class="lower">
                <asp:Button ID="ShowCodes" runat="server" OnClick="ShowCodes_Click" Text="Show Pin and Security"/>
                <h5><%#Eval("name")%><span id="SEC" style="display: none;">Security:<%#Eval("securityCode") %></span></h5><span><%#Eval("card_id")%></span>
                <h6><%#Eval("expirationDate") %><span id="PIN" style="display: none;">Pin:<%#Eval("pinCode") %></span></h6>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
    </div>
</asp:Content>
