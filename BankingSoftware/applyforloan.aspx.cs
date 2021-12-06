using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class applyforloan : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitLoan_Click(object sender, EventArgs e)
        {
            string LoanType = DropDownList1.SelectedValue;
            string Name = FullName.Text.ToString();
            decimal MonthlyIncome = decimal.Parse(NMI.Text.ToString());
            decimal AmountNeeded = decimal.Parse(MoneyLoan.Text.ToString());

            decimal SixMonthsPayment = 6 * MonthlyIncome;
            decimal percent = 0;
            if (LoanType == "HomeLoan")
            {
                percent = 0.1m;
            }
            else if (LoanType == "CarLoan")
            {
                percent = 0.2m;
            }
            else if (LoanType == "PersonalLoan")
            {
                percent = 0.3m;
            }
            else if (LoanType == "EducationalLoan")
            {
                percent = 0.09m;
            }
            decimal MoneyToPayForSixMonths = percent * AmountNeeded;
            if (MoneyToPayForSixMonths > SixMonthsPayment)
            {
                Response.Write("<script>alert('No');</script>");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string _Cash = MoneyLoan.Text.Replace('.', ',').Trim();
                    decimal newBalance = decimal.Parse(Session["balance"].ToString()) + decimal.Parse(_Cash);
                    DateTime date = DateTime.Today;
                    SqlCommand cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info)" +
                        " values(@user_id, @new_balance, @date, @transaction_amount, @info)", con);
                    cmd.Parameters.AddWithValue("@user_id", Session["user_id"].ToString());
                    cmd.Parameters.AddWithValue("@new_balance", newBalance);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@transaction_amount", decimal.Parse(_Cash));
                    cmd.Parameters.AddWithValue("@info", "Cerdit transaction amount - "+AmountNeeded.ToString());
                    cmd.ExecuteNonQuery();
                    Session["balance"] = newBalance;

                    _Cash = newBalance.ToString();
                    cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + _Cash.Replace(',', '.').Trim()
                        + "'WHERE user_id = '" + Session["user_id"].ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch(Exception ex)
                {

                }
                Response.Redirect("viewBalance.aspx");

            }

        }
    }
}