using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Linq;

namespace BankingSoftware
{
    public partial class signup : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            bool[] check = { checkTextBox(), checkUserExists(), checkPhoneNumber(),
                Age(),checkPassworrd(), Pswrd.Text == CPswrd.Text, checkPin()};
            if (check.All(x => x))
                SignUp();
            else
            {
                if (!check[0])
                    Response.Write("<script>alert('Please fill in all fields!');</script>");
                else if (!check[1])
                    Response.Write("<script>alert('This User ID,Email or Phone numebr are taken, try another one!');</script>");
                else if (!check[2])
                    Response.Write("<script>alert('Phone number is incorrect');</script>");
                else if (!check[3])
                    Response.Write("<script>alert('Must be at least 18 years of age');</script>");
                else if (!check[4])
                    Response.Write("<script>alert('Your password must contain at least 8 characters, one capital letter and one number!');</script>");
                else if (!check[5])
                    Response.Write("<script>alert('Passwords do not match!');</script>");
                else if (!check[6])
                    Response.Write("<script>alert('Pin code is incorrect!');</script>");
            }
        }
        void SignUp()
        {
            try
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('Sign up Successfully!');window.location ='signin.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkTextBox()
        {
            try
            {
                return (Name.Text.Trim() != string.Empty && Surname.Text.Trim() != string.Empty && Uname.Text.Trim() != string.Empty
                    && Email.Text.Trim() != string.Empty && PNumber.Text.Trim() != string.Empty && DoB.Text.Trim() != string.Empty
                    && Pswrd.Text.Trim() != string.Empty && CPswrd.Text.Trim() != string.Empty && State.Text != string.Empty
                    && City.Text != string.Empty && Pin.Text.Trim() != string.Empty && FAdress.Text != string.Empty);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM users_tbl WHERE user_id='" + Uname.Text.Trim() + 
                    "' OR email = '" + Email.Text.Trim() + "'OR number = '" + PNumber.Text.Trim() + "';", con);
                SqlDataReader reader = cmd.ExecuteReader();
                return !reader.Read();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        bool checkPhoneNumber()
        {
            try
            {
                return PNumber.Text.Length == 10 && !Regex.IsMatch(PNumber.Text.Trim(), "[^0-9]");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }

        }
        bool Age()
        {
            try
            {
                DateTime date = DateTime.Now.Date;
                DateTime dateofbirth = DateTime.Parse(DoB.Text);
                TimeSpan d = date.Subtract(dateofbirth);
                return (d.Days >= 6575);
            }
            catch (Exception)
            {
                return false;
            }
        }
        bool checkPassworrd()
        {
            try
            {
                char[] password = Pswrd.Text.ToCharArray();
                bool length = password.Length >= 8;
                bool anynumber = Pswrd.Text.Any(c => char.IsDigit(c));
                bool uppercase = default;
                foreach (char character in password)
                {
                    uppercase = Char.IsUpper(character);
                    if (uppercase)
                        break;
                }
                return (length && anynumber && uppercase);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false ;
            }

        }
        bool checkPin()
        {
            try
            {
                return (Pin.Text.Length == 4) ;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
    }
}