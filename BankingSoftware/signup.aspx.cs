using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BankingSoftware
{
    public partial class signup : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            if (checkUserExists())
            {
                SignUp();

            }
            else
                Response.Write("<script>alert('This User ID is taken, try another one!');</script>");
        }

        void SignUp()
        {
            try
            {
                if (Pswrd.Text == CPswrd.Text)
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string name = Name.Text.Trim() + " " + Surname.Text.Trim();
                    string address = Country.Text + " " + State.Text + " " + City.Text + " " + Pin.Text.Trim() + " " + FAdress.Text;

                    SqlCommand cmd = new SqlCommand("INSERT INTO users_tbl (user_id, name, email, number, dob, password, address, balance) " +
                        "values(@user_id, @name, @email, @number, @dob, @password, @address,"+ 0 +" )",con);
                    cmd.Parameters.AddWithValue("@user_id", Uname.Text.Trim());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", Email.Text.Trim());
                    cmd.Parameters.AddWithValue("@number", PNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@dob", DoB.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", Pswrd.Text.Trim());
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("signin.aspx",false);

                }
                else
                    Response.Write("<script>alert('Passwords do not match!');</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkUserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from users_tbl where user_id='" + Uname.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                    return false;
                else
                    return true;


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
    }

}