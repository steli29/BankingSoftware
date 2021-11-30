using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BankingSoftware
{
    public partial class WebForm3 : System.Web.UI.Page
    {   class Card
        {

        }
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {   
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM  cards_tbl FULL OUTER JOIN users_tbl ON cards_tbl.user_id = users_tbl.user_id WHERE cards_tbl.user_id='" + Session["user_id"] +"';" ,con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if(ds.Tables.Count > 0)
                {
                    CardRepeater.DataSource = ds;
                    CardRepeater.DataBind();
                }
                /*foreach (DataRow row in dt.Rows)
                {
                    TextBox1.Text += row["expirationDate"].ToString();
                    TextBox1.Text += row["pinCode"].ToString();
                    TextBox1.Text += row["securityCode"].ToString();
                    TextBox1.Text += row["user_id"].ToString();
                    TextBox1.Text += row["card_id"].ToString();
                    TextBox1.Text += "\n";

                }*/
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void RequestCard_Click(object sender, EventArgs e)
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
            Response.Write("<script>alert('card is made');</script>");
            Response.Redirect("myCards.aspx");
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
            if(month.Length == 1)
            {
                month = "0" + month;
            }
            return month + "/" + year;
        }
    }
}

   