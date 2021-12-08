﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class applyforloan : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
                Response.Redirect("signin.aspx");
            Session["pass"] = null;
        }

        protected void SubmitLoan_Click(object sender, EventArgs e)
        {
            if (Loans.SelectedValue == "imlookingfor" || string.IsNullOrEmpty(NMI.Text.Trim()) ||
                string.IsNullOrEmpty(MoneyLoan.Text.Trim()) || string.IsNullOrEmpty(FullName.Text.Trim()) ||
                string.IsNullOrEmpty(ContactNumber.Text.Trim()) || string.IsNullOrEmpty(CityName.Text.Trim()))
                Response.Write("<script>alert('Please fill in all fields!');</script>");
            else if (!checkNameExists(FullName.Text.Trim()))
            {
                Response.Write("<script>alert('Invalid user!');</script>");
            }
            else if (!checkPhoneExists(ContactNumber.Text.Trim()))
            {
                Response.Write("<script>alert('Invalid contact number!');</script>");
            }
            else if (!checkMoneyInput(NMI.Text.Trim()))
            {
                Response.Write("<script>alert('Invalid money input!');</script>");
            }
            else if (!checkMoneyInput(MoneyLoan.Text.Trim()))
            {
                Response.Write("<script>alert('Invalid money input!');</script>");
            }
            else if (!checkCity(CityName.Text.Trim()))
            {
                Response.Write("<script>alert('Invalid city name!');</script>");
            }
            else
            {
                initLoanApplication();
            }

        }
        void Loan()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string Cash = MoneyLoan.Text.Replace('.', ',').Trim();
                decimal newBalance = decimal.Parse(Session["balance"].ToString()) + decimal.Parse(Cash);
                DateTime date = DateTime.Today;
                SqlCommand cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info, type)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info, @type)", con);
                cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                cmd.Parameters.AddWithValue("@new_balance", newBalance);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(Cash));
                cmd.Parameters.AddWithValue("@info", "Approved for credit '"+ Loans.Text + "'") ;
                cmd.Parameters.AddWithValue("@type","Loan");
                cmd.ExecuteNonQuery();
                Session["balance"] = newBalance;

                cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + newBalance.ToString().Replace(',', '.').Trim()
                    + "'WHERE user_id = '" + Session["user_id"] + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('Loan Approved!');window.location ='viewBalance.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void initLoanApplication()
        {
            string LoanType = Loans.SelectedValue;
            decimal MonthlyIncome = decimal.Parse(NMI.Text.ToString());
            decimal AmountNeeded = decimal.Parse(MoneyLoan.Text.ToString());

            decimal SixMonthsPayment = 6 * MonthlyIncome;
            decimal percent = 0;
            if (LoanType == "HomeLoan")
                percent = 0.1m;
            else if (LoanType == "CarLoan")
                percent = 0.2m;
            else if (LoanType == "PersonalLoan")
                percent = 0.3m;
            else if (LoanType == "EducationalLoan")
                percent = 0.09m;
            decimal MoneyToPayForSixMonths = percent * AmountNeeded;
            if (MoneyToPayForSixMonths < SixMonthsPayment) {
                Loan();
            } else {
                Response.Write("<script>alert('Loan Rejected!');</script>");
            }
        }

        bool checkNameExists(string nameField)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id ='"+ Session["user_id"] +"' AND name='" + nameField + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();

                return reader.Read();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        bool checkPhoneExists(string phoneNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id ='" + Session["user_id"] + "' AND number='" + phoneNumber + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();

                return reader.Read();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        bool checkCity(string city)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id ='" + Session["user_id"] + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();
                string address = default;
                if (reader.Read())
                {
                    address = reader.GetValue(6).ToString();
                }
               
                if (address.Split(' ')[2] == city)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        bool checkMoneyInput(string money)
        {
            try
            {
                return (decimal.TryParse(money.Replace('.', ',').Trim(), out decimal myMoney) &&
                    decimal.Parse(money.Replace('.', ',').Trim()) != 0);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
    }
}