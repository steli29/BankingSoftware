<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="passwordchange.aspx.cs" Inherits="BankingSoftware.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mt-3 mb-3 col-md-6 mx-auto card card-body">
        <div class="row">
            <div class="col">
                <center>
                    <h4>Change Password</h4>
                    <hr>
                </center>
            </div>
        </div>

        <div class="row">
            <div class="col-md-8 mx-auto">
                <label>New Password</label>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="Npass" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-8 mx-auto mt-2">
                <label>Confirm New Password</label>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="CNpass" runat="server" placeholder="Confirm New Password" TextMode="Password"></asp:TextBox>
                </div>
            </div>
        </div>

            <div class="form-group">
                <center>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger col-10 mt-3 " Text="Change"/>
                </center>
        </div>
    </div>

</asp:Content>
