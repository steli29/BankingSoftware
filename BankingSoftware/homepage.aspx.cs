using System;
using System.Web.UI;


namespace BankingSoftware
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] != null)
                Session["pass"] = null;
        }
    }
}