using System;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class Home : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["user_id"] != null)
                {
                    ViewBalance.Visible = true;
                    AddFunds.Visible = true;
                    MoneyTrans.Visible = true;
                    MyCard.Visible = true;
                    Loan.Visible = true;
                    signin.Visible = false;
                    signup.Visible = false;
                    Logout.Visible= true;
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["user_id"] = null;
            Session["name"] = null;
            Session["balance"] = null;
            Session["pass"] = null;
            ViewBalance.Visible = false;
            AddFunds.Visible = false;
            MoneyTrans.Visible = false;
            MyCard.Visible = false;
            Loan.Visible = false;
            signin.Visible = true;
            signup.Visible = true;
            Logout.Visible = false;
            Response.Redirect("homepage.aspx");
        }
    }
}