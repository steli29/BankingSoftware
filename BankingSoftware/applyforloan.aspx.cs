using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankingSoftware
{
    public partial class applyforloan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitLoan_Click(object sender, EventArgs e)
        {
            string LoanType = DropDownList1.SelectedValue;
            string Name = FullName.Text.ToString();
            double MonthlyIncome = double.Parse(NMI.Text.ToString());
            double AmountNeeded = double.Parse(MoneyLoan.Text.ToString());

            double SixMonthsPayment = 6 * MonthlyIncome;
            double percent = 0;
            if (LoanType == "HomeLoan")
            {
                percent = 0.1;
            }
            else if (LoanType == "CarLoan")
            {
                percent = 0.2;
            }
            else if (LoanType == "PersonalLoan")
            {
                percent = 0.3;
            }
            else if (LoanType == "EducationalLoan")
            {
                percent = 0.09;
            }
            double MoneyToPayForSixMonths = percent * AmountNeeded;
            if (MoneyToPayForSixMonths > SixMonthsPayment)
            {
                Response.Write("<script>alert('No');</script>");
            }
            else
            {
                Response.Write("<script>alert('Yes');</script>");
            }

        }
    }
}