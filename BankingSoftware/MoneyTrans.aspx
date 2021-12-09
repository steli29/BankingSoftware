<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="MoneyTrans.aspx.cs" Inherits="BankingSoftware.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mt-3 col-md-6 mx-auto">
        <div class="card card-body">
            <div class="row">
                <div class="col">
                    <center>
                        <h2>MoneyTransfer</h2>
                        <hr>
                    </center>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 mx-auto">
                    <label>Receiver ID</label>
                    <div class="form-group">
                        <asp:TextBox ID="ReceiverID" CssClass="form-control" runat="server" placeholder="Receiver ID"></asp:TextBox>
                   </div>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 mx-auto">
                    <label>Your Password</label>
                    <div class="form-group">
                        <asp:TextBox ID="YourPassword" CssClass="form-control" runat="server" placeholder="Your Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 mx-auto">
                    <label>Amount Of Money</label>
                    <div class="form-group">
                        <asp:TextBox ID="AmountOfMoney" CssClass="form-control" runat="server" placeholder="Amount Of Money"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 mx-auto">
                    <label>Reason</label>
                    <div class="form-group">
                        <asp:TextBox ID="Reason" CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Reason"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group" >
                <asp:Button  ID="Submit" CssClass="btn btn-info col-12 mt-5 btn-lg" runat="server" Text="Submit" OnClick="Transfer_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
