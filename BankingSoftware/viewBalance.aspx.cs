using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankingSoftware
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM  users_tbl  WHERE user_id='" + Session["user_id"] + "';", con);
                //cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                Balance.DataSource = ds;
                Balance.DataBind();

                cmd = new SqlCommand("SELECT * FROM balance_tbl FULL OUTER JOIN users_tbl ON users_tbl.user_id = balance_tbl.user_id WHERE users_tbl.user_id='" + Session["user_id"] + "';", con);
                SqlDataAdapter db = new SqlDataAdapter(cmd);
                DataSet de = new DataSet();
                db = new SqlDataAdapter(cmd);
                db.Fill(de);
                SqlCommand cmdRows = new SqlCommand("SELECT COUNT(1) FROM balance_tbl WHERE user_id='" + Session["user_id"] + "';", con);
                int UserExist = (int)cmdRows.ExecuteScalar();
                if (UserExist != 0) 
                {
                    All.Visible = true;
                    Income.Visible = true;
                    Costs.Visible = true;
                    Transaction.DataSource = de;
                    Transaction.DataBind();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        public void All_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SELECT * FROM balance_tbl FULL OUTER JOIN users_tbl ON users_tbl.user_id = balance_tbl.user_id WHERE users_tbl.user_id='" + Session["user_id"] + "';", con);
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            DataSet de = new DataSet();
            db = new SqlDataAdapter(cmd);
            db.Fill(de);
            Transaction.DataSource = de;
            Transaction.DataBind();
        }

        public void Income_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SELECT * FROM balance_tbl FULL OUTER JOIN users_tbl ON users_tbl.user_id = balance_tbl.user_id WHERE users_tbl.user_id='" + Session["user_id"] + "' AND transaction_amount > '0';", con);
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            DataSet de = new DataSet();
            db = new SqlDataAdapter(cmd);
            db.Fill(de);
            Transaction.DataSource = de;
            Transaction.DataBind();
        }

        public void Costs_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SELECT * FROM balance_tbl FULL OUTER JOIN users_tbl ON users_tbl.user_id = balance_tbl.user_id WHERE users_tbl.user_id='" + Session["user_id"] + "' AND transaction_amount < '0';", con);
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            DataSet de = new DataSet();
            db = new SqlDataAdapter(cmd);
            db.Fill(de);
            Transaction.DataSource = de;
            Transaction.DataBind();
        }


    }
}