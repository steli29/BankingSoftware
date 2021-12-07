using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class WebForm5 : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
                Response.Redirect("signin.aspx");
            Session["pass"] = null;
        }

        protected void AddFunds_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Card.Text.Trim()) || string.IsNullOrEmpty(CVC.Text.Trim()) || 
                string.IsNullOrEmpty(Cash.Text.Trim()) || string.IsNullOrEmpty(DueDate.Text.Trim()))
                Response.Write("<script>alert('Please fill in all fields!');</script>");
            else if (Card.Text.Length != 16)
                Response.Write("<script>alert('Card numbers needs to be 16!');</script>");
            else if(!checkduedate())
                Response.Write("<script>alert('Card is expired');</script>");
            else if (CVC.Text.Length != 3)
                Response.Write("<script>alert('CVV needs to be numbers 3!');</script>");
            else if (!decimal.TryParse(Cash.Text.Replace('.', ',').Trim(), out decimal result))
                Response.Write("<script>alert('Invalid input');</script>");
            else
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
                
                string Cash = this.Cash.Text.Replace('.', ',').Trim();
                decimal new_balance = decimal.Parse(Session["balance"].ToString()) + decimal.Parse(Cash);
                DateTime date = DateTime.Today;
                SqlCommand cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info, type)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info, @type)", con);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                cmd.Parameters.AddWithValue("@new_balance", new_balance);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(Cash));
                cmd.Parameters.AddWithValue("@info", "Added from another card");
                cmd.Parameters.AddWithValue("@type", "Income");
                cmd.ExecuteNonQuery();
                Session["balance"] = new_balance;

                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + new_balance.ToString().Replace(',', '.').Trim() 
                    + "'WHERE user_id = '" + Session["user_id"] +"'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('Added Funds Successful!');window.location ='viewBalance.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        bool checkduedate()
        {
            try
            {
                DateTime date = DateTime.Now;
                DateTime duedate = DateTime.Parse(DueDate.Text + "-01");
                return duedate.Year > date.Year || duedate.Month >= date.Month;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

    }
}