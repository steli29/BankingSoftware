using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                SqlCommand cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, transaction_amount, info)" +
                    " values(@user_id, @new_balance, @transaction_amount, @info)", con);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"].ToString());
                cmd.Parameters.AddWithValue("@new_balance", new_balance);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(_Cash));
                cmd.Parameters.AddWithValue("@info", "Added from another card");
                cmd.ExecuteNonQuery();
                Session["balance"] = new_balance;
                Response.Write("<script>alert('Added Funds Successful!');</script>");

                _Cash = new_balance.ToString();

                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + _Cash.Replace(',', '.').Trim() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}