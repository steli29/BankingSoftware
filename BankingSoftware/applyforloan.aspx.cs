using System;
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
            if (MoneyToPayForSixMonths < SixMonthsPayment)
                Loan();
            else
                Response.Write("<script>alert('Loan Rejected!');</script>");
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
    }
}