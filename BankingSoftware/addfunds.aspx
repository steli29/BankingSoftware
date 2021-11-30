<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="addfunds.aspx.cs" Inherits="BankingSoftware.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="mt-3 col-md-6 mx-auto">
        <div class="card card-body">
            <div class="row">
                <div class="col">
                    <center>
                        <h4>Add funds</h4>
                        <hr>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 mx-auto">
                    <label>Card Number</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="Card" runat="server" placeholder="Card Number" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt-2">
                <div class="col-md-4">
                    <label>Expire Date</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="DueDate" runat="server" placeholder="Expire Date" TextMode="Month"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>CVC</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="CVV" runat="server" placeholder="CVC" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Cash Amount</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="Cash" runat="server"  placeholder="Cash Amount" ></asp:TextBox>
                    </div>
                </div>
            </div>

                <div class="form-group">
                    <center>
                        <asp:Button ID="AddFunds" runat="server" CssClass="btn btn-secondary col-10 mt-5 " Text="Submit" OnClick="AddFunds_Click"/>
                    </center>
            </div>
        </div>
        <a href="homepage.aspx"><< Back to Home</a><br><br>
    </div>

</asp:Content>
