using System;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BankingSoftware
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        Random random = new Random();
        static int SCode;
        static string emailcheck;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Send_Click(object sender, EventArgs e)
        {
            if (checkEmailExists())
            {
                SendEmail();
                Email.Visible = false;
                Send.Visible = false;
                Code.Visible = true;
                Check.Visible = true;
            }      
            else
                Response.Write("<script>alert('The email is incorrect.');</script>");
        }
        protected void Check_Click(object sender, EventArgs e)
        {
            if (SCode == int.Parse(Code.Text.Trim()))
            {
                Code.Visible = false;
                Check.Visible = false;
                Submit.Visible = true;
                Npass.Visible = true;
                CNpass.Visible = true;
            }
            else
                Response.Write("<script>alert('Incorrect Code!');</script>");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Npass.Text == CNpass.Text)
                ChangePassword();
            else
                Response.Write("<script>alert('Passwords do not match!');</script>");
        }

        void SendEmail()
        {
            try
            {
                SCode = random.Next(1000, 9999);
                emailcheck = Email.Text.Trim();
                string text = "Your security code is " + SCode + " ";
                MailMessage message = new MailMessage("bankingsoftware05@gmail.com", emailcheck, "Changing Password", text);
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("bankingsoftware05@gmail.com", "1234bank");
                client.Send(message);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkEmailExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE email = '" + Email.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void ChangePassword()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE email = '" + emailcheck + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (Npass.Text != reader.GetValue(5).ToString())
                    {
                        con.Close();
                        con.Open();
                        cmd = new SqlCommand("UPDATE users_tbl SET password = '"
                            + Npass.Text.Trim() + "' WHERE email = '" + emailcheck + "'", con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('Password Changed!');window.location ='signin.aspx';", true);
                        break;
                    }
                    else
                        Response.Write("<script>alert('New password cannot be the same as the old password');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Username/email or password dont match!');</script>");
            }
            con.Close();
            
        }

    }
}