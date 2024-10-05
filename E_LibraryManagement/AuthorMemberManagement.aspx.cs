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
    public partial class AuthorMemberManagement : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }


        // USER DEFINED FUNCTION
        void GetMemberById()
        {
            if (checkIfMemberExists())
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
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            TextBox2.Text = dr.GetValue(0).ToString();
                            TextBox7.Text = dr.GetValue(10).ToString();
                            TextBox3.Text = dr.GetValue(1).ToString();
                            TextBox4.Text = dr.GetValue(2).ToString();
                            TextBox8.Text = dr.GetValue(3).ToString();
                            TextBox5.Text = dr.GetValue(4).ToString();
                            TextBox6.Text = dr.GetValue(5).ToString();
                            TextBox9.Text = dr.GetValue(6).ToString();
                            TextBox10.Text = dr.GetValue(7).ToString();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid ID')</script>");
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");

                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID')</script>");
                ClearForm();
            }
        }


        // USER DEFINED FUNCTION
        void UpdateMemberStatusById(string Status)
        {
            if (checkIfMemberExists())
            {
                try
                {
                    SqlConnection connection = new SqlConnection(CS);
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlCommand cmd = new SqlCommand("spUpdateMemberStatusById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@account_status", Status);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Member Status Updated')</script>");
                    ClearForm();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");

                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID')</script>");
                ClearForm();
            }
        }


        // USER DEFINED FUNCTION
        void DeleteMember()
        {
            if (checkIfMemberExists())
            {
                try
                {
                    SqlConnection connection = new SqlConnection(CS);
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlCommand cmd = new SqlCommand("spDeleteMember", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Member Deleted Successfully')</script>");
                    ClearForm();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");

                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID')</script>");
                ClearForm();
            }
        }

        // USER DEFINED FUNCTION
        void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
        }

        //USER DEFINED FUNCTION 
        bool checkIfMemberExists()
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

                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

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



        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------


        //Member ID Go Button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GetMemberById();
        }

        //Active Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusById("Active");
        }

        //Pending Button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusById("Pending");
        }

        //Deactivate Button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusById("De-active");
        }

        //Delete Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteMember();
        }
    }
}