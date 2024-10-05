using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_LibraryManagement
{
    public partial class AdminBookInventory : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAuthorPublisherValues();
            }
            GridView1.DataBind();
        }

        //USER DEFINED FUNCTION
        void FillAuthorPublisherValues()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                //FOR AUTHOR NAME DROPDOWN
                SqlCommand cmd = new SqlCommand("spGetAuthorNameDropDown", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList3.DataSource = dt;
                DropDownList3.DataValueField = "author_name";
                DropDownList3.DataBind();


                //FOR PUBLISHER NAME DROPDOWN
                cmd = new SqlCommand("spGetPublisherNameDropDown", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DropDownList2.DataSource = dt;
                DropDownList2.DataValueField = "publisher_name";
                DropDownList2.DataBind();

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

                SqlCommand cmd = new SqlCommand("spGetBookByBookId", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());

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
        void AddNewBook()
        {
            try
            {
                //For Genre
                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }
                genres = genres.Remove(genres.Length - 1);

                //For IMAGE Upload
                string filePath = "~/book_inventory/books.png";
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + fileName));
                filePath = "~/book_inventory/" + fileName;

                SqlConnection connection = new SqlConnection(CS);
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand("spInsertBookDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_date", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link", filePath);

                cmd.ExecuteNonQuery();
                connection.Close();
                Response.Write("<script>alert('Book Added Successfully')</script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }

        }

        //USER DEFINED FUNCTION
        void GetBookById()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand("spGetBooksById", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["book_name"].ToString();
                    DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();
                    TextBox3.Text = dt.Rows[0]["publisher_date"].ToString();
                    //For Genre
                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for (int i = 0; i < genre.Length; i++)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if(ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;
                            }
                        }
                    }
                    TextBox9.Text = dt.Rows[0]["edition"].ToString();
                    TextBox4.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    TextBox11.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    TextBox5.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    TextBox6.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    TextBox7.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));
                    TextBox10.Text = dt.Rows[0]["book_description"].ToString();

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID')</script>");
                }


            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }


        //USER DEFINED FUNCTION
        void UpdateBookById()
        {
            if (CheckIfBookExists())
            {
                try
                {
                    //For Genre
                    string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);


                    //For Book Img Link
                    string filePath = "~/book_inventory/books";
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if(fileName == "" ||  fileName == null)
                    {
                        filePath = global_filepath;
                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("book_inventory/" + fileName));
                        filePath = "~/book_inventory/" + fileName;
                    }

                    //For Actual Stock And Current Stock
                    int actual_stock = Convert.ToInt32(TextBox5.Text.Trim());
                    int current_stock = Convert.ToInt32(TextBox6.Text.Trim());

                    if(global_actual_stock == actual_stock)
                    {

                    }
                    else
                    {
                        if(actual_stock < global_issued_books)
                        {
                            Response.Write("<script>alert('Actual Stock Value Cannot Be Less Than The Issued Books')</script>");
                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_books;
                            TextBox6.Text = "" + current_stock;
                        }
                    }


                    SqlConnection connection = new SqlConnection(CS);
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlCommand cmd = new SqlCommand("spUpdateBookById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisher_date", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_cost", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                    cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                    cmd.Parameters.AddWithValue("@book_img_link", filePath);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Book Updated Successfully')</script>");
                    //ClearForm();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");

                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID')</script>");
                //ClearForm();
            }
        }

        //USER DEFINED FUNCTION
        void DeleteBookById()
        {
            if (CheckIfBookExists())
            {
                try
                {
                    SqlConnection connection = new SqlConnection(CS);
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlCommand cmd = new SqlCommand("spDeleteBookById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Book Deleted Successfully')</script>");
                    ClearForm();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");

                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID')</script>");
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
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------


        //GO BUTTON CLICK
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GetBookById();
        }

        //ADD BUTTON CLICK
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckIfBookExists())
            {
                Response.Write("<script>alert('Book Already Exists...Try Some Other Book ID')</script>");
            }
            else
            {
                AddNewBook();
            }
        }

        //UPDATE BUTTON CLICK
        protected void Button2_Click(object sender, EventArgs e)
        {
            UpdateBookById();
        }

        //DELETE BUTTON CLICK
        protected void Button3_Click(object sender, EventArgs e)
        {
            DeleteBookById();
        }
    }
}