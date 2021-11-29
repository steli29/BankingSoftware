<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="MoneyTrans.aspx.cs" Inherits="BankingSoftware.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="mt-3 col-md-6 mx-auto">
        <div class="card card-body">
            <div class="row">
                <div class="col">
                    <center>
                         <h1>MoneyTransfer</h1>    
                    </center>
                </div>
            </div>
            

         <asp:TextBox ID="ReciverID" CssClass="form-control" runat="server" placeholder="Reciver ID"></asp:TextBox>
              <br /> <?/br>
         <asp:TextBox ID="ReciverName" CssClass="form-control" runat="server" placeholder="Reciver Name"></asp:TextBox>
              <br /> <?/br>
         <asp:TextBox ID="YourPassword" CssClass="form-control" runat="server" placeholder="Your Password"></asp:TextBox>
              <br /> <?/br>
         <asp:TextBox ID="YourPhoneNumber" CssClass="form-control" runat="server" placeholder="Your Phone Number"></asp:TextBox>
              <br /> <?/br>
         <asp:TextBox ID="AmountOfMoney" CssClass="form-control" runat="server" placeholder="Amount Of Money"></asp:TextBox>
              <br /> <?/br>
         <asp:TextBox ID="Reason" CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Reason"></asp:TextBox>

                    <div class="form-group" >
        <asp:Button  ID="Button2" CssClass="btn btn-info col-12 mt-5 btn-lg" runat="server" Text="Submit"/>
                 </div>

            </div>

           <br>

         </div>



</asp:Content>
