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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Transfer_Click(object sender, EventArgs e)
        {
            Checks();
        }

        void Checks()
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
                {
                    balance = (decimal)reader.GetValue(7);
                }

                decimal new_balance = balance + decimal.Parse(cash);
                con.Close();
                con.Open();
                cmd.ExecuteNonQuery();

                string reason = Reason.Text;
                string receiverID = ReceiverID.Text;
                DateTime date = DateTime.Today;

                cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info)", con);
                cmd.Parameters.AddWithValue("@user_id", receiverID);
                cmd.Parameters.AddWithValue("@new_balance", new_balance);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(cash));
                cmd.Parameters.AddWithValue("@info", reason);
                cmd.ExecuteNonQuery();

                cash = new_balance.ToString();

                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + cash.Replace(',', '.').Trim()
                    + "' WHERE user_id = '" + ReceiverID.Text + "'", con);
                cmd.ExecuteNonQuery();

                new_balance = decimal.Parse(Session["balance"].ToString()) - decimal.Parse(cash);
                
                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + new_balance
                    + "' WHERE user_id = '" + Session["user_id"] + "'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info)", con);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"].ToString());
                cmd.Parameters.AddWithValue("@new_balance", new_balance);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(cash)*-1);
                cmd.Parameters.AddWithValue("@info", "Sent money to " + ReceiverID.Text + ".");
                cmd.ExecuteNonQuery();

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
                if (ReceiversCardID.Text != string.Empty && ReceiverID.Text != string.Empty && YourPassword.Text != string.Empty
                    && YourPhoneNumber.Text != string.Empty && AmountOfMoney.Text != string.Empty && Reason.Text != string.Empty)
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

        bool checkReceiverCardId()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM cards_tbl WHERE user_id='" + ReceiverID.Text + "' AND card_id='" + ReceiversCardID.Text + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                    return false;
                else
                    return true;


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
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                    return false;
                else
                    return true;


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

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id='" + ReceiverID.Text + "' AND password='" + YourPassword.Text + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        bool checkPhoneNumber()
        {
            try
            {
                if (YourPhoneNumber.Text.Length == 10 && !Regex.IsMatch(YourPhoneNumber.Text.Trim(), "[^0-9]"))
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

        bool checkAmountOfMoney()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id='" + ReceiverID.Text + "' AND balance>='" + decimal.Parse(AmountOfMoney.Text.ToString()) + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
    }
}