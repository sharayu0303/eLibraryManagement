using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_LibraryManagement
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].Equals(""))
                    {
                        LinkButton1.Visible = true;  //User Login Link Button
                        LinkButton2.Visible = true;  //Sign Up Link Button

                        LinkButton3.Visible = false;  //LogOut Link Button
                        LinkButton7.Visible = false;  //Hello User Link Button

                        LinkButton6.Visible = true;  //Admin Login Link Button

                        LinkButton11.Visible = false;  //Author Management Link Button
                        LinkButton12.Visible = false;  //Publisher Management Link Button
                        LinkButton8.Visible = false;  //Book Inventory Link Button
                        LinkButton9.Visible = false;  //Book Issuing Link Button
                        LinkButton10.Visible = false;  //Member Management Link Button
                    }

                    else if (Session["role"].Equals("user"))
                    {
                        LinkButton1.Visible = false;  //User Login Link Button
                        LinkButton2.Visible = false;  //Sign Up Link Button

                        LinkButton3.Visible = true;  //LogOut Link Button
                        LinkButton7.Visible = true;  //Hello User Link Button
                        LinkButton7.Text = "Hello " + Session["username"].ToString();

                        LinkButton6.Visible = true;  //Admin Login Link Button
                        LinkButton11.Visible = false;  //Author Management Link Button
                        LinkButton12.Visible = false;  //Publisher Management Link Button
                        LinkButton8.Visible = false;  //Book Inventory Link Button
                        LinkButton9.Visible = false;  //Book Issuing Link Button
                        LinkButton10.Visible = false;  //Member Management Link Button
                    }

                    else if (Session["role"].Equals("admin"))
                    {
                        LinkButton1.Visible = false;  //User Login Link Button
                        LinkButton2.Visible = false;  //Sign Up Link Button

                        LinkButton3.Visible = true;  //LogOut Link Button
                        LinkButton7.Visible = true;  //Hello User Link Button
                        LinkButton7.Text = "Hello Admin";

                        LinkButton6.Visible = false;  //Admin Login Link Button
                        LinkButton11.Visible = true;  //Author Management Link Button
                        LinkButton12.Visible = true;  //Publisher Management Link Button
                        LinkButton8.Visible = true;  //Book Inventory Link Button
                        LinkButton9.Visible = true;  //Book Issuing Link Button
                        LinkButton10.Visible = true;  //Member Management Link Button
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }

        }

        protected void linkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
        }

        protected void linkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAuthorManagement.aspx");
        }

        protected void linkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPublisherManagement.aspx");
        }

        protected void linkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookInventory.aspx");
        }

        protected void linkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookIssuing.aspx");
        }

        protected void linkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("AuthorMemberManagement.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBooks.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            Response.Redirect("UserLogin.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSignUp.aspx");
        }

        //Logout Button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            if (Session["role"].Equals(""))
            {
                LinkButton1.Visible = true;  //User Login Link Button
                LinkButton2.Visible = true;  //Sign Up Link Button

                LinkButton3.Visible = false;  //LogOut Link Button
                LinkButton7.Visible = false;  //Hello User Link Button

                LinkButton6.Visible = true;  //Admin Login Link Button
                LinkButton11.Visible = false;  //Author Management Link Button
                LinkButton12.Visible = false;  //Publisher Management Link Button
                LinkButton8.Visible = false;  //Book Inventory Link Button
                LinkButton9.Visible = false;  //Book Issuing Link Button
                LinkButton10.Visible = false;  //Member Management Link Button

                Response.Redirect("Homepage.aspx");

            }
        }

        //Hello User
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }
}