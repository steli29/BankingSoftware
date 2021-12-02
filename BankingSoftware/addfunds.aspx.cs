using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;


namespace BankingSoftware
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddFunds_Click(object sender, EventArgs e)
        {
            Funds();
        }

        void Funds()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                
                string _Cash = Cash.Text.Replace('.', ',').Trim();
                decimal new_balance = decimal.Parse(Session["balance"].ToString()) + decimal.Parse(_Cash);
                DateTime date = DateTime.Today;
                SqlCommand cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info)", con);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"].ToString());
                cmd.Parameters.AddWithValue("@new_balance", new_balance);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(_Cash));
                cmd.Parameters.AddWithValue("@info", "Added from another card");
                cmd.ExecuteNonQuery();
                Session["balance"] = new_balance;

                _Cash = new_balance.ToString();
                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + _Cash.Replace(',', '.').Trim() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('Added Funds Successful!');window.location ='viewBalance.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}