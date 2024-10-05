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
    public partial class AdminPublisherManagement : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }


        //User Defined Function
        bool CheckPublisherExists()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetPublisherById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        // User defined Add New Publisher
        void AddNewpublisher()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spInsertPublisherDetails", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Publisher Added Successfully.');</script>");
                ClearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        // User Defined Update Publisher
        void UpdatePublisher()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spUpdatePublisher", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Publisher Updated Successfully.');</script>");
                ClearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        // User Defined Delete Publisher
        void DeletePublisher()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spDeletePublisher", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Publisher Deleted Successfully.');</script>");
                ClearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        // User Defined ClearForm
        void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        //User Defined Get Publisher By ID
        void GetPublisherById()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetPublisherById", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Publisher Id');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }




        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------



        //Add Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPublisherExists())
                {
                    Response.Write("<script>alert('Publisher With This ID Already Exists..You Cannot Add Publisher With Same Publisher ID')</script>");

                }
                else
                {
                    AddNewpublisher();
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }


        //Update Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPublisherExists())
                {
                    UpdatePublisher();
                }
                else
                {
                    Response.Write("<script>alert('Publisher Does Not Exists')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }


        //Delete Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPublisherExists())
                {
                    DeletePublisher();
                }
                else
                {
                    Response.Write("<script>alert('Publisher Does Not Exists')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }


        //Go Button
        protected void Button4_Click(object sender, EventArgs e)
        {
            GetPublisherById();
        }
    }
}