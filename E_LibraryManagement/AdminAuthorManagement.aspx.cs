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
    public partial class AdminAuthorManagement : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //user defined function
        bool checkIfAuthorExists()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetAuthorById", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

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

        // User defined Add New Author
        void AddNewAuthor()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spInsertAuthorDetails", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Author Added Successfully.');</script>");
                ClearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        // User Defined Update Author
        void UpdateAuthor()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spUpdateAuthor", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Author Updated Successfully.');</script>");
                ClearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        // User Defined Delete Author
        void DeleteAuthor()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spDeleteAuthor", connection);

                //To Connect Database
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Author Deleted Successfully.');</script>");
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

        //User Defined Get Author By ID
        void GetAuthorById()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand cmd = new SqlCommand("spGetAuthorById", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Author Id');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------


        //Add Button Click
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkIfAuthorExists())
                {
                    Response.Write("<script>alert('Author With This ID Already Exists..You Cannot Add Another Author With Same Author ID')</script>");
                }
                else
                {
                    AddNewAuthor();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //Update Button Click
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkIfAuthorExists())
                {
                    UpdateAuthor();
                }
                else
                {
                    Response.Write("<script>alert('Author Does Not Exists')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //Delete Button Click

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkIfAuthorExists())
                {
                    DeleteAuthor();
                }
                else
                {
                    Response.Write("<script>alert('Author Does Not Exists')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //Go Button Click
        protected void Button4_Click(object sender, EventArgs e)
        {
            GetAuthorById();
        }
    }
}