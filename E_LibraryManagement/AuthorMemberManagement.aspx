<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AuthorMemberManagement.aspx.cs" Inherits="E_LibraryManagement.AuthorMemberManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <%--Below jquery code is to get "search tool" in table where ".table" is a class provided to the GridView--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.table').prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            //$('.table1').DataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5">
                <%--Using Card For Better Look--%>
                <div class="card" style="border-color: forestgreen">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h5>Member Details</h5>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="80" src="Images/generaluser.png" />
                                </center>
                            </div>
                        </div>
                        <%--ID, Full Name, Account Status--%>
                        <div class="row mt-3">

                            <div class="col-md-3">
                                <label>Member ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder=" ID"></asp:TextBox>
                                        <asp:LinkButton class="btn btn-primary" ID="LinkButton4" runat="server" Text="Go" OnClick="LinkButton4_Click"><i class="fa-solid fa-magnifying-glass"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Full Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Member Name" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-5">
                                <label>Account Status</label>
                                <div class="input-group">
                                        <asp:TextBox CssClass="form-control me-1 px-2" ID="TextBox7" runat="server" placeholder="Status" ReadOnly="true"></asp:TextBox>
                                        <asp:LinkButton class="btn btn-success me-1 px-2" ID="LinkButton1" runat="server" Text="A" OnClick="LinkButton1_Click" ><i class="fa-solid fa-circle-check"></i></asp:LinkButton>
                                        <asp:LinkButton class="btn btn-warning me-1 px-2" ID="LinkButton2" runat="server" Text="P" OnClick="LinkButton2_Click"><i class="fa-regular fa-circle-pause"></i></asp:LinkButton>
                                        <asp:LinkButton class="btn btn-danger me-1 px-2" ID="LinkButton3" runat="server" Text="D" OnClick="LinkButton3_Click"><i class="fa-solid fa-circle-xmark"></i></asp:LinkButton>
                                    </div>
                            </div>

                        </div>

                        <%--DOB, Contact No, Email Id--%>
                        <div class="row mt-3">

                            <div class="col-md-3 ">
                                <label>DOB</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control px-1" ID="TextBox3" runat="server" TextMode="Date" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Contact No</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Contact No" TextMode="Phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-5">
                                <label>Email ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="Email ID" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--State, City, PinCode--%>
                        <div class="row mt-3">

                            <div class="col-md-4">
                                <label>State</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="State" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>City</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="City" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Pin Code</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Pin Code" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <%--Full Postal Address--%>
                        <div class="row mt-3">

                            <div class="col-12">
                                <label>Full Postal Address</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Product Description" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>

                        </div>


                        <div class="row mt-3">
                            <div class="col-12 mx-auto">
                                <div class="form-group">
                                    <asp:Button type="button" class="btn btn-danger btn-lg btn-block  col-12 mx-auto" ID="Button1" runat="server" Text="Delete User Permanently" OnClick="Button1_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <a class="text-decoration-none" href="Homepage.aspx"><< Back to Homepage</a>
                </div>
            </div>

            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h5>Member List</h5>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ELibraryDBConnectionString2 %>" SelectCommand="SELECT * FROM [member_master_table]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="member_id" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="member_id" HeaderText="Member ID" ReadOnly="True" SortExpression="member_id" />
                                        <asp:BoundField DataField="full_name" HeaderText="Name" SortExpression="full_name" />
                                        <asp:BoundField DataField="account_status" HeaderText="Account Status" SortExpression="account_status" />
                                        <asp:BoundField DataField="contact_no" HeaderText="Contact" SortExpression="contact_no" />
                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                        <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />
                                        <%--<asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
