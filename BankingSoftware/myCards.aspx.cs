using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class WebForm3 : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        int rowsLength = default;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["user_id"] == null)
                Response.Redirect("signin.aspx");
            else if (!Page.IsPostBack)
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT * FROM  cards_tbl FULL OUTER JOIN users_tbl ON cards_tbl.user_id = users_tbl.user_id WHERE cards_tbl.user_id='" + Session["user_id"] + "';", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {   
                        rowsLength = ds.Tables[0].Rows.Count;
                        if (ds.Tables[0].Rows.Count >= 2)
                            RequestCard.Visible = false;
                        CardRepeater.DataSource = ds;
                        CardRepeater.DataBind();
                    }
                    ShowCodes();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            
        }

        protected void RequestCard_Click(object sender, EventArgs e)
        {
            if (rowsLength < 2)
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO cards_tbl (card_id, user_id, expirationDate, pinCode, securityCode) " +
                            "values(@card_id, @user_id, @expirationDate, @pinCode, @securityCode)", con);
                cmd.Parameters.AddWithValue("@card_id", GenerateCardNumber());
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                cmd.Parameters.AddWithValue("@expirationDate", GenerateDate());
                cmd.Parameters.AddWithValue("@pinCode", GeneratePin());
                cmd.Parameters.AddWithValue("@securityCode", GenerateSecurity());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('New card is made');</script>");
                Response.Redirect("myCards.aspx");
            }
            else
                Response.Write("<script>alert('You cant make more than two cards!');</script>");
        }

        int GeneratePin()
        {
            Random pin = new Random();
            return pin.Next(1000, 9999);
        }

        int GenerateSecurity()
        {
            Random security = new Random();
            return security.Next(100, 999);
        }

        string GenerateCardNumber()
        {
            Random rnd = new Random();
            int cardNumber1 = rnd.Next(4572, 4999);
            int cardNumber2 = rnd.Next(1000, 9999);
            int cardNumber3 = rnd.Next(1000, 9999);
            int cardNumber4 = rnd.Next(1000, 9999);
            return cardNumber1 + " " + cardNumber2 + " " + cardNumber3 + " " + cardNumber4;
        }

        string GenerateDate()
        {
            DateTime now = DateTime.Now;
            DateTime expirationDate = now.AddYears(4);
            var month = expirationDate.Month.ToString();
            var year = expirationDate.Year.ToString();
            if (month.Length == 1)
                month = "0" + month;
            return month + "/" + year;
        }

        void ShowCodes()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM  users_tbl WHERE user_id='" + Session["user_id"] + "';", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                Session["pass"] = dr.GetValue(5).ToString();
        }

        public bool ValidatePassword()
        {
            string password = Passw.Text.Trim();
            byte[] hashBytes = Convert.FromBase64String(Session["pass"].ToString());
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }

            }

            return true;
        }
    }
}

   