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
                con.Close();

                if( Session["Type"] == null)
                    Session["Type"] = "All";
                getPageRows(1);
                int pageCount = rows();
                if (pageCount == 1) Right.Visible = false;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void getPageRows(int page)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd =default;
            if(Session["Type"].ToString() == "Income")
                cmd = new SqlCommand("DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber = " + page + " SET @RowsOfPage = 7 SELECT * FROM balance_tbl WHERE user_id ='" + Session["user_id"] + "' AND transaction_amount > 0 ORDER BY transaction_id OFFSET (@PageNumber - 1) * @RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY", con);
            else if(Session["Type"].ToString() == "Cost")
                cmd = new SqlCommand("DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber = " + page + " SET @RowsOfPage = 7 SELECT * FROM balance_tbl WHERE user_id ='" + Session["user_id"] + "' AND transaction_amount < 0 ORDER BY transaction_id OFFSET (@PageNumber - 1) * @RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY", con);
            else
                cmd = new SqlCommand("DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT SET @PageNumber = " + page + " SET @RowsOfPage = 7 SELECT * FROM balance_tbl WHERE user_id ='" + Session["user_id"] + "' ORDER BY transaction_id OFFSET (@PageNumber - 1) * @RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY", con);
            
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            DataSet de = new DataSet();
            DataTable dt = new DataTable();
            db.Fill(de); //Fill Dataset
            dt = de.Tables[0];
            if (dt.Rows.Count > 0)
            {
                All.Visible = true;
                Income.Visible = true;
                Costs.Visible = true;

                if (page == 1) 
                    Left.Visible = false;
                else  
                    Left.Visible = true;

                if (dt.Rows.Count % 7 != 0)
                    Right.Visible = false;
                else
                    Right.Visible = true;

                Transaction.DataSource = de;
                Transaction.DataBind();
            }            
            else
            {
                Pagenumber.Text = (page-1).ToString();
                Right.Visible = false;
            }
            con.Close();
        }

        int rows()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmdRows = new SqlCommand("SELECT COUNT(1) FROM balance_tbl WHERE user_id='" + Session["user_id"] + "';", con);
            int UserExist = (int)cmdRows.ExecuteScalar();

            int pageCount = UserExist / 7;

            if (UserExist % 7 != 0)
                pageCount++;
            return pageCount;
        }

        protected void Right_Click(object sender, EventArgs e)
        {
            int num = int.Parse(Pagenumber.Text.ToString());
            int pageCount = rows();
            num++;
            if (num <= pageCount)
            {
                Pagenumber.Text = num.ToString();                
                getPageRows(num);                
            }
            if(num == pageCount)
            {
                Right.Visible = false;
            }
        }

        protected void Left_Click(object sender, EventArgs e)
        {
            int num = int.Parse(Pagenumber.Text.ToString());
            num--;
            if (num <= 0)  
                Left.Visible = false;
            else
            {
                Pagenumber.Text = num.ToString();
                getPageRows(num);
            }
        }

        public void All_Click(object sender, EventArgs e)
        {
            Session["Type"] = "All";
            Pagenumber.Text = "1";
            getPageRows(1);
        }

        public void Income_Click(object sender, EventArgs e)
        {
            Session["Type"] = "Income";
            Pagenumber.Text = "1";
            getPageRows(1);
        }

        public void Costs_Click(object sender, EventArgs e)
        {
            Session["Type"] = "Cost";
            Pagenumber.Text = "1";
            getPageRows(1);
        }

        void LoanWithdraw()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DateTime dateTime = DateTime.Today;
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
                while (DateTime.Parse(Loan[3]).AddMonths(monthsadded) <= dateTime && monthsadded < 12)
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
                counter++;
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