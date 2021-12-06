using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankingSoftware
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static decimal fee = 0.30M;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("signin.aspx");
            }
            Session["pass"] = null;
        }
        protected void Transfer_Click(object sender, EventArgs e)
        {
            bool[] check = { checkTextBox(), checkReceiverName(), checkIsYourId(), checkMoneyInput(), checkAmountOfMoney(), checkYourPassword() };
            if (check.All(x => x))
                SendMoney();
            else
            {
                if (!check[0])
                    Response.Write("<script>alert('Please fill in all fields!');</script>");
                else if (!check[1])
                    Response.Write("<script>alert('This User ID does not exist!');</script>");
                else if (!check[2])
                    Response.Write("<script>alert('Invalid user!');</script>");
                else if (!check[3])
                    Response.Write("<script>alert('Invalid money input!');</script>");
                else if (!check[4])
                    Response.Write("<script>alert('No enought money to send!');</script>");
                else if (!check[5])
                    Response.Write("<script>alert('Incorrect password!');</script>");
            }
        }

        void SendMoney()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string cash = AmountOfMoney.Text.Replace('.', ',').Trim();
                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id='" + ReceiverID.Text + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();
                decimal balance = default;

                if (reader.Read())
                    balance = (decimal)reader.GetValue(7);


                decimal new_balance = balance + decimal.Parse(cash);
                reader.Close();

                DateTime date = DateTime.Today;

                cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info)", con);
                cmd.Parameters.AddWithValue("@user_id", ReceiverID.Text);
                cmd.Parameters.AddWithValue("@new_balance", new_balance);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(cash));
                cmd.Parameters.AddWithValue("@info", Reason.Text);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + new_balance.ToString().Replace(',', '.').Trim()
                + "' WHERE user_id = '" + ReceiverID.Text + "'", con);
                cmd.ExecuteNonQuery();

                new_balance = decimal.Parse(Session["balance"].ToString()) - (decimal.Parse(cash) + fee);
                Session["balance"] = new_balance;

                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + new_balance.ToString().Replace(',', '.').Trim()
                    + "' WHERE user_id = '" + Session["user_id"] + "'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info)", con);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"].ToString());
                cmd.Parameters.AddWithValue("@new_balance", new_balance.ToString().Replace(',', '.').Trim());
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", (decimal.Parse(cash) + fee) * -1);
                cmd.Parameters.AddWithValue("@info", Reason.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('Money Transfer is Successful!');window.location ='viewBalance.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkTextBox()
        {
            try
            {
                if (ReceiverID.Text != string.Empty && YourPassword.Text != string.Empty
                    && AmountOfMoney.Text != string.Empty && Reason.Text != string.Empty)
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

        bool checkReceiverName()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id='" + ReceiverID.Text + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();

                return reader.Read();

                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        
        bool checkIsYourId()
        {
            return Session["user_id"].ToString() != ReceiverID.Text.Trim();
        }

        bool checkMoneyInput()
        {
            try
            {
                if (decimal.TryParse(AmountOfMoney.Text.ToString().Replace('.', ',').Trim(), out decimal money) && 
                    decimal.Parse(AmountOfMoney.Text.ToString().Replace('.', ',').Trim()) != 0)
                    return true ;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        
        bool checkAmountOfMoney()
        {
            try
            {
                if (checkMoneyInput())
                {
                    if (decimal.Parse(Session["balance"].ToString()) > (decimal.Parse(AmountOfMoney.Text.ToString().Replace('.', ',').Trim()) + fee)) return true;
                    else return false;
                }

                else return false;
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        
        bool checkYourPassword()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id='" + Session["user_id"].ToString() + "' AND password='" + YourPassword.Text + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { return true; }
                else return false;
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
    }
}