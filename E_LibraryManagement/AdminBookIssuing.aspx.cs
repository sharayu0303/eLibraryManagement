using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_LibraryManagement
{
    public partial class AdminBookIssuing : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //Go Button Click
        protected void Button4_Click(object sender, EventArgs e)
        {
            GetNames();
        }

        //Issue Book Click Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckIfBookExists() && CheckIfMemberExists())
            {
                if (CheckIfIssuedBookExists())
                {
                    Response.Write("<script>alert('This Member Already Had This Book')</script>");
                }
                else
                {
                    IssueBook();
                }
                ClearForm();
            }
            else
            {
                Response.Write("<script>alert('Wrong Book Id Or Member Id')</script>");
            }
        }

        //Return Book Click Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckIfBookExists() && CheckIfMemberExists())
            {
                if (CheckIfIssuedBookExists())
                {
                    ReturnBook();
                }
                else
                {
                    Response.Write("<script>alert('This Entry Does Not Exist')</script>");
                }
                ClearForm();
            }
            else
            {
                Response.Write("<script>alert('Wrong Book Id Or Member Id')</script>");
            }
        }

        //USER DEFINED FUNCTION
        void GetNames()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetBookNameByBookId", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox4.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Wrong Book Id')</script>");
                }


                cmd = new SqlCommand("spGetMemberNameByMemberId", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox3.Text = dt.Rows[0]["full_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Wrong Member Id')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //USER DEFINED FUNCTION
        bool CheckIfBookExists()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetBookByBookIdAndCurrentStock", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }


        //USER DEFINED FUNCTION
        bool CheckIfIssuedBookExists()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetIssuedBookDetails", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        //USER DEFINED FUNCTION
        void ReturnBook()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand("spDeleteReturnedBookEntry", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.ExecuteNonQuery();
                //int result = cmd.ExecuteNonQuery();
                //if(result > 0)
                //{
                cmd = new SqlCommand("spUpdateCurrentStockAfterBookReturning", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();

                connection.Close();
                Response.Write("<script>alert('Book Returned Successfully')</script>");
                GridView1.DataBind();
                //}
                //else
                //{
                //    Response.Write("<script>alert('Error... Invalid Details')</script>");
                //}

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //USER DEFINED FUNCTION
        void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
        }


        //USER DEFINED FUNCTION
        bool CheckIfMemberExists()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetMemberNameByMemberId", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        //USER DEFINED FUNCTION
        void IssueBook()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand("spInsertBookIssuingDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@member_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@issue_date", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@due_date", TextBox6.Text.Trim());

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("spUpdateCurrentStockByBookId", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();

                connection.Close();
                Response.Write("<script>alert('Book Issued Successfully')</script>");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check Condition Here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[1].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[2].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[3].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[4].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

    }
}