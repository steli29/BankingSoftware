<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="BankingSoftware.signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mt-3 col-md-6 mx-auto">
        <div class="card card-body">
            <div class="row">
                <div class="col">
                    <center>
                        <h4>Sign Up</h4>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label>First Name</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="Name" runat="server" placeholder="First Name"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>Surname</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="Surname" runat="server" placeholder="Surname"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label>User ID</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="Uname" runat="server" placeholder="Username"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>Email</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="Email" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label>Phone Number</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="PNumber" runat="server"  placeholder="Phone Number" TextMode="Phone"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>Date of Birth</label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="DoB" runat="server" placeholder="Date of birth" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label>Password</label>
                    <div class="form-group">
                        <asp:TextBox ID="Pswrd" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>Confirm Password</label>
                    <div class="form-group">
                        <asp:TextBox ID="CPswrd" CssClass="form-control" runat="server" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <center>
                        <asp:Label CssClass="badge mt-3 mb-2 badge-pill bg-info" ID="Label1" 
                                runat="server" Text="Billing information"></asp:Label>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label>Country</label>
                    <div class="form-group">
                        <asp:DropDownList ID="Country" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Bulgaria" Value="Bulgaria" />
                            <asp:ListItem Text="England" Value="England" />
                            <asp:ListItem Text="Spain" Value="Spain" />
                            <asp:ListItem Text="France" Value="France" />
                            <asp:ListItem Text="USA" Value="USA" />
                            <asp:ListItem Text="Germany" Value="Germany" />
                            <asp:ListItem Text="Sweden" Value="Sweden" />
                            <asp:ListItem Text="Turkey" Value="Turkey" />
                            <asp:ListItem Text="Hungary" Value="Hungary" />
                            <asp:ListItem Text="Netherlands" Value="Netherlands" />
                            <asp:ListItem Text="Russia" Value="Russia" />
                        </asp:DropDownList>
                                    
                    </div>
                </div>
                 <div class="col-md-6">
                        <label>State</label>
                        <div class="form-group">
                            <asp:TextBox class="form-control" ID="State" runat="server" placeholder="State"></asp:TextBox>                            
                        </div>
                     </div>
            </div>

            <div class="row">  
                     <div class="col-md-6">
                        <label>City</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="City" runat="server" placeholder="City"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Pincode</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="Pin" runat="server" placeholder="Pincode" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                  </div>

            <div class="row">
                     <div class="col">
                        <label>Full Address</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="FAdress" runat="server" placeholder="Full Address" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                     </div>
                  </div>

                <div class="form-group">
                    <asp:Button ID="Signup" runat="server" CssClass="btn btn-info col-12 mt-5 btn-lg" OnClick="Signup_Click" Text="Sign up"/>
            </div>
        </div>
        <a href="homepage.aspx"><< Back to Home</a><br><br>
    </div>

</asp:Content>
