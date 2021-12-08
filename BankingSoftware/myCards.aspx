<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="myCards.aspx.cs" Inherits="BankingSoftware.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./customCss/cards.css" rel="stylesheet" type="text/css" />
    <script>
    async function showCodes(event) {
        event.preventDefault();
        const pin = event.target.querySelector('#Pin');
        const sec = event.target.querySelector('#Sec');
        pin.style.display = 'block';
        sec.style.display = 'block';
        setTimeout(() => {
            pin.style.display = 'none';
            sec.style.display = 'none';
        }, 10000);
        
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ScriptMode="Release" LoadScriptsBeforeUI="false" EnablePageMethods="true"
    runat="server" ID="ScriptManager1" />
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
    <%--<asp:HiddenField ID="HiddenPass" runat="server" />--%>
    <asp:Button ID="RequestCard" runat="server" class="section2_btn btn22" Text="Request new card" OnClick="RequestCard_Click"/>
    <div class="wrapper">
        <asp:Repeater ID="CardRepeater" runat="server">
            <ItemTemplate>
                <div class="card" onclick="showCodes(event)">
                    <div class="upper">
                        <h6>Debit</h6>
                        <h4>Visa</h4>
                    </div>
                    <div class="lower">
                        <h5><%#Eval("name")%><span id="Sec" style="display: none;">Security:<%#Eval("securityCode") %></span></h5><span><%#Eval("card_id")%></span>
                        <h6><%#Eval("expirationDate") %><span id="Pin" style="display: none;">Pin:<%#Eval("pinCode") %></span></h6>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
