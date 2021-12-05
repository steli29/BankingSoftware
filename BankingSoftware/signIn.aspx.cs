using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed) {
                    con.Open(); 
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM  users_tbl WHERE (user_id='" +Username.Text.Trim() + "'OR email='"+ Username.Text.Trim()+"') AND password='" + Password.Text.Trim() +"';", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["user_id"] = reader.GetValue(0).ToString();
                    Session["name"] = reader.GetValue(1).ToString();
                    Session["balance"] = reader.GetValue(7).ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('Welcome, " + reader.GetValue(1).ToString() + "!');window.location ='viewBalance.aspx';", true);
                    con.Close();
                }
                else
                {
                    Response.Write("<script>alert('Username/email or password dont match!');</script>");
                }

            }
            catch  (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message +"');</script>");
            }
        }

        protected void Redirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }

        protected void Change_Click(object sender, EventArgs e)
        {
            Response.Redirect("passwordchange.aspx");
        }
    }
}