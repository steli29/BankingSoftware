using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BankingSoftware
{
    public partial class WebForm6 : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoanWithdraw();

            if (Session["user_id"] == null)
                Response.Redirect("signin.aspx");
            Session["pass"] = null;
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM  users_tbl  WHERE user_id='" + Session["user_id"] + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);;
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
            con.Close();
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
            con.Close();
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
            con.Close();
        }

        void LoanWithdraw()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DateTime dateTime = DateTime.Parse("2022-02-26");
            List<string> Loan = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM balance_tbl WHERE user_id = '" + Session["user_id"] + "' AND type = 'Loan'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Loan.Add(reader[i].ToString());
                reader.Close();

                int monthsadded = 1 + alreadyWithdraw();
                decimal fee = decimal.Parse(Loan[4]) / 12 + decimal.Parse(Loan[4]) / 240;
                while (DateTime.Parse(Loan[3]).AddMonths(monthsadded) <= dateTime && DateTime.Parse(Loan[3]).AddYears(1) >= dateTime)
                {
                    decimal new_balance = decimal.Parse(Session["balance"].ToString()) - fee;
                    Session["balance"] = new_balance;
                    cmd = new SqlCommand("INSERT INTO balance_tbl (user_id, new_balance, date, transaction_amount, info, type)" +
                    " values(@user_id, @new_balance, @date, @transaction_amount, @info, @type)", con);
                    cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
                    cmd.Parameters.AddWithValue("@new_balance", new_balance);
                    cmd.Parameters.AddWithValue("@date", DateTime.Parse(Loan[3]).AddMonths(monthsadded));
                    cmd.Parameters.AddWithValue("@transaction_amount", fee * -1);
                    cmd.Parameters.AddWithValue("@info", "Monthly withdraw for loan");
                    cmd.Parameters.AddWithValue("@type", "WithDraw Loan");
                    cmd.ExecuteNonQuery();
                    monthsadded++;
                    cmd = new SqlCommand("UPDATE users_tbl SET balance = '" + new_balance.ToString().Replace(',', '.').Trim()
                    + "'WHERE user_id = '" + Session["user_id"] + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (DateTime.Parse(Loan[3]).AddYears(1) < dateTime)
                    EndLoan();
            }
            con.Close();
        }

        int alreadyWithdraw()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM balance_tbl WHERE user_id = '" + Session["user_id"] + "' AND type = 'WithDraw Loan'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                counter++;
            }
            return counter;
        }

        void EndLoan()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("UPDATE balance_tbl SET type = 'LoanEnd' WHERE user_id='" + Session["user_id"] + "' AND type = 'Loan' OR type = 'WithDraw Loan'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}