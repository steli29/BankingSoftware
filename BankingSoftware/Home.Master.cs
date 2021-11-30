using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankingSoftware
{
    public partial class Home : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //session[user_id] ?? -> ul[id=user]
            try
            {
                if(Session["user_id"] != null)
                {
                    signin.Visible = false;
                    signup.Visible = false;
                    Logout.Visible = true;
                }
                else
                {
                    /*ViewBalance.Visible = false;
                    AddFunds.Visible = false;
                    MoneyTrans.Visible = false;
                    MyCard.Visible = false;
                    Loan.Visible = false;  
                    Logout.Visible=false;*/   

                }
                //TODO function buttons should be hidden
            }
            catch(Exception ex)
            {

            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["user_id"] = null;
            Session["name"] = null;
            Response.Redirect("homepage.aspx");
        }
    }
}