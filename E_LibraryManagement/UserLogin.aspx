<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="E_LibraryManagement.UserLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-5 mx-auto">
                <%--Using Card For Better Look--%>
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="150" src="Images/generaluser.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Member Login</h3>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                    <hr />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <%--<label>Member Id</label>--%>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Member Id"></asp:TextBox>
                                </div>
                                <br />

                                <%--<label>Password</label>--%>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                </div>
                                <br>

                                <div class="form-group">
                                    <asp:Button type="button" class="btn btn-success btn-lg d-grid gap-2 col-12 mx-auto" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <a class="text-decoration-none" href="UserSignUp.aspx">
                                    <input class="btn btn-info btn-lg d-grid gap-2 col-12 mx-auto" id="Button2" type="button" value="Sign Up" />
                                    </a>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>



                <a class="text-decoration-none" href="Homepage.aspx"><< Back to Homepage</a><br /> <br />
            </div>
        </div>
    </div>
</asp:Content>
