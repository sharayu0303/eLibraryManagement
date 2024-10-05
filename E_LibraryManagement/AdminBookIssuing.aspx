<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminBookIssuing.aspx.cs" Inherits="E_LibraryManagement.AdminBookIssuing" %>

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
                                    <h5>Book Issuing</h5>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="80" src="Images/books.png" />
                                </center>
                            </div>
                        </div>
                        <%--ID--%>
                        <div class="row mt-3">
                           
                            <div class="col-md-6">
                                <label>Member ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Member ID"></asp:TextBox>
                                </div>
                            </div>

                             <div class="col-md-6">
                                <label>Book ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Book ID"></asp:TextBox>
                                        <asp:Button class="btn btn-primary" ID="Button4" runat="server" Text="Go" OnClick="Button4_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--NAME--%>
                         <div class="row mt-3">
                           
                            <div class="col-md-6">
                                <label>Member Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Member Name" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                             <div class="col-md-6">
                                <label>Book Name</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Book Name" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--DATE--%>
                        <div class="row mt-3">
                           
                            <div class="col-md-6">
                                <label>&nbsp;Issue Date</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>

                             <div class="col-md-6">
                                <label>Due Date</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Buttons--%>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Button type="button" class="btn btn-primary btn-lg btn-block  col-12 mx-auto" ID="Button1" runat="server" Text="Issue" OnClick="Button1_Click" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Button type="button" class="btn btn-success btn-lg btn-block  col-12 mx-auto" ID="Button2" runat="server" Text="Return" OnClick="Button2_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h5>Issued Book List</h5>
                                </center>
                            </div>
                        </div>  

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:ELibraryDBConnectionString %>' SelectCommand="SELECT * FROM [book_issue_tbl]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="member_id" HeaderText="Member Id" SortExpression="member_id"></asp:BoundField>
                                        <asp:BoundField DataField="member_name" HeaderText="Member Name" SortExpression="member_name"></asp:BoundField>
                                        <asp:BoundField DataField="book_id" HeaderText="Book Id" SortExpression="book_id"></asp:BoundField>
                                        <asp:BoundField DataField="book_name" HeaderText="Book Name" SortExpression="book_name"></asp:BoundField>
                                        <asp:BoundField DataField="issue_date" HeaderText="Issue Date" SortExpression="issue_date"></asp:BoundField>
                                        <asp:BoundField DataField="due_data" HeaderText="Due Date" SortExpression="due_data"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
                <a class="text-decoration-none" href="Homepage.aspx"><< Back to Homepage</a>
        </div>
    </div>

</asp:Content>
