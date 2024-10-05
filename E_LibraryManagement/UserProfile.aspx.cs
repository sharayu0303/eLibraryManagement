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
    public partial class UserProfile : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired.. Login Again');</script>");
                    Response.Redirect("UserLogin.aspx");
                }
                else
                {
                    GetUserBookData();

                    if (!Page.IsPostBack)
                    {
                        GetUserPersonalDetails();
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Session Expired.. Login Again');</script>");
                Response.Redirect("UserLogin.aspx");
            }
        }

        //Update Button Click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired.. Login Again');</script>");
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                UpdateUserPersonalDetails();
            }
        }


        //USER DEFINED FUNCTIONS
        void GetUserBookData()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetMemberDetails", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@member_id", Session["username"].ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        //USER DEFINED FUNCTIONS
        void GetUserPersonalDetails()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetMemberById", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@member_id", Session["username"].ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                TextBox2.Text = dt.Rows[0]["dob"].ToString();
                TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
                TextBox4.Text = dt.Rows[0]["email"].ToString();
                DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                TextBox5.Text = dt.Rows[0]["city"].ToString();
                TextBox6.Text = dt.Rows[0]["pincode"].ToString();
                TextBox7.Text = dt.Rows[0]["full_address"].ToString();
                TextBox8.Text = dt.Rows[0]["member_id"].ToString();
                TextBox9.Text = dt.Rows[0]["password"].ToString();

                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                if(dt.Rows[0]["account_status"].ToString().Trim() == "Active")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-success");
                    //Label1.CssClass = "badge badge-pill badge-success";
                }
                else if(dt.Rows[0]["account_status"].ToString().Trim() == "Pending")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                    //Label1.CssClass = "badge badge-pill badge-warning";
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "De-active")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                    //Label1.CssClass = "badge badge-pill badge-danger";
                }
                else
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                    //Label1.CssClass = "badge badge-pill badge-info";
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        //USER DEFINED FUNCTIONS
        void UpdateUserPersonalDetails()
        {
            string password = "";
            if(TextBox10.Text.Trim() == "")
            {
                password = TextBox9.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }

            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spUpdateMemberDetailsById", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@account_status", "Pending");
                cmd.Parameters.AddWithValue("@member_id", Session["username"].ToString());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Member Details Updated Successfully.');</script>");
                GetUserPersonalDetails();
                GetUserBookData();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

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