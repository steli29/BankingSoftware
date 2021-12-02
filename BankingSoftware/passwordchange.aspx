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
            <div class="form-group">
                <asp:TextBox CssClass="form-control" ID="Email" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
            </div>
        </div>
        </div>
        
        <div class="row">
        <div class="col-md-8 mx-auto">
            <div class="form-group">
                <asp:TextBox Visible="false" CssClass="form-control" ID="Code" runat="server" placeholder="Security Code" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        </div>

        <div class="row">
                <div class="col-md-8 mx-auto">
                    <div class="form-group">
                        <asp:TextBox Visible="false" CssClass="form-control" ID="Npass" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
            </div>

        <div class="row">
            <div class="col-md-8 mx-auto mt-2">
                <div class="form-group">
                    <asp:TextBox Visible="false" CssClass="form-control" ID="CNpass" runat="server" placeholder="Confirm New Password" TextMode="Password"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="form-group">
                <center>
                    <asp:Button ID="Send" runat="server" CssClass="btn btn-danger col-10 mt-3 " Visible="true" OnClick="Send_Click" Text="Send"/>
                    <asp:Button ID="Check" runat="server" CssClass="btn btn-danger col-10 mt-3 " Visible="false" OnClick="Check_Click" Text="Check"/>
                    <asp:Button ID="Submit" runat="server" CssClass="btn btn-danger col-10 mt-3 " Visible="false" OnClick="Submit_Click" Text="Submit"/>
                </center>
        </div>
    </div>

</asp:Content>
