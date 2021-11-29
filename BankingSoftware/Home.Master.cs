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
                    logout.Visible = true;
                }
                //TODO function buttons should be hidden
            }
            catch(Exception ex)
            {

            }
        }
    }
}