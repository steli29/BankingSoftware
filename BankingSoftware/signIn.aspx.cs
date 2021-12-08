using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class WebForm4 : Page
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

                string password = Password.Text.Trim();

                SqlCommand cmd = new SqlCommand("SELECT * FROM  users_tbl WHERE (user_id='" +Username.Text.Trim() + "'OR email='"+ Username.Text.Trim()+"');", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    bool result = ValidatePassword(password, reader.GetValue(5).ToString());
                    if (result)
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
                        Response.Write("<script>alert('Incorrect password!');</script>");
                    }
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

        public static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public static bool ValidatePassword(string password, string hashedPasswordFromDatabase)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPasswordFromDatabase);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }
}