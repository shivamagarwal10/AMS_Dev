using EmployeeAttendenceMangement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
namespace EmployeeAttendenceMangement.AMSConnecction
{
    public class AMS_Access_Layer
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ToString();
                con = new SqlConnection(constr);
            }
            catch (Exception er)
            {

            } 
        }
        
        public List<EmployeeCreateModel> GetEmployee(bool is_admin, int EmployeeId)
        {
            connection();
            List<EmployeeCreateModel> EmpList = new List<EmployeeCreateModel>();

            string str = "";
            if (is_admin == true)
            {
                str = " em.is_admin  in(1,0) and em.Status in(1) ";
            }
            else
            {
                str = " em.is_admin=0 and em.EmployeeId =" + EmployeeId + " ";
            }

            SqlCommand com = new SqlCommand("select em.EmployeeId,em.City,em.Contact_No,em.EmailId," +
                "em.Emp_Joining_Date, em.FirstName, em.Address,em.StateId ,em.CountryId," +
                "em.Gender,em.LastName,em.DateofBirth,em.PinCode,c.CountryName,em.AlternateContact_No," +
                " st.StateName ,em.Marital_status from Employee as em " +
                "left join Country as c on em.CountryId = c.CountryId " +
                "left join State as st on st.StateId = em.StateId   where " + str + "  ORDER BY em.EmployeeId DESC ", con);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(

                    new EmployeeCreateModel
                    {

                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"]),
                        Contact_No = Convert.ToString(dr["Contact_No"]),
                        EmailId = Convert.ToString(dr["EmailId"]),
                        Emp_Joining_Date = Convert.ToDateTime(dr["Emp_Joining_Date"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        DateofBirth = Convert.ToDateTime(dr["DateofBirth"]),
                        PinCode = Convert.ToString(dr["PinCode"]),
                        AlternateContact_No = Convert.ToString(dr["AlternateContact_No"]),
                        Marital_status = Convert.ToString(dr["Marital_status"]),
                        CountryName = Convert.ToString(dr["CountryName"]),
                        StateName = Convert.ToString(dr["StateName"]),
                        CountryId = Convert.ToInt32(dr["CountryId"]),
                        StateId = Convert.ToInt32(dr["StateId"]),

                    }
                    );
            }

            return EmpList;
        }
        public List<EmployeeCreateModel> GetEmployee()
        {
            connection();
            List<EmployeeCreateModel> EmpList = new List<EmployeeCreateModel>();


            SqlCommand com = new SqlCommand("select em.EmployeeId,em.City,em.Contact_No,em.EmailId," +
                "em.Emp_Joining_Date, em.FirstName, em.Address,em.StateId ,em.CountryId," +
                "em.Gender,em.LastName,em.DateofBirth,em.PinCode,c.CountryName,em.AlternateContact_No," +
                " st.StateName ,em.Marital_status from Employee as em " +
                "left join Country as c on em.CountryId = c.CountryId " +
                "left join State as st on st.StateId = em.StateId  ORDER BY em.EmployeeId DESC", con);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new EmployeeCreateModel
                    {

                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"]),
                        Contact_No = Convert.ToString(dr["Contact_No"]),
                        EmailId = Convert.ToString(dr["EmailId"]),
                        Emp_Joining_Date = Convert.ToDateTime(dr["Emp_Joining_Date"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        DateofBirth = Convert.ToDateTime(dr["DateofBirth"]),
                        PinCode = Convert.ToString(dr["PinCode"]),
                        AlternateContact_No = Convert.ToString(dr["AlternateContact_No"]),
                        Marital_status = Convert.ToString(dr["Marital_status"]),
                        CountryName = Convert.ToString(dr["CountryName"]),
                        StateName = Convert.ToString(dr["StateName"]),
                        CountryId = Convert.ToInt32(dr["CountryId"]),
                        StateId = Convert.ToInt32(dr["StateId"]),

                    }
                    );
            }

            return EmpList;
        }
        //public List<EmployeeCreateModel> ValidateEmailId1()
        //{
        //    connection();
        //    List<EmployeeCreateModel> EmpList = new List<EmployeeCreateModel>();


        //    SqlCommand com = new SqlCommand("select EmailId from Employee", con);

        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //       EmpList.Add(

        //            new EmployeeCreateModel
        //            {
        //                EmailId = Convert.ToString(dr["EmailId"]),
        //            }
        //            );
        //    }

        //    return EmpList;
        //}
        public int ValidateEmailId(string emailId)
        {   
            connection();
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select EmailId from Employee  where EmailId=@EmailId ", con);
            cmd.Parameters.AddWithValue("@emailId", emailId);
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int ValidatealtEmailId(string AlternateEmailId)
        {
            connection();
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select AlternateEmailId from Employee  where AlternateEmailId = @AlternateEmailId ", con);
            cmd.Parameters.AddWithValue("@AlternateEmailId", AlternateEmailId);
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public String GetEmployeeId(EmployeeCreateModel model)
        {
            connection();
           var CtxYear =  DateTime.Now.Year.ToString(); 

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("SELECT EmployeeId FROM Employee WHERE EmployeeId LIKE '%"+ CtxYear + "%' ORDER BY EmployeeId DESC ", con);
            
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                model.EmployeeId  = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);

                return "1";
            }
            else
            {
               

                return "0";
            }
        }
       

        public bool AddEmployee(EmployeeCreateModel obj)
        {
            connection();
            string msg = "";
            byte[] encode = new byte[obj.Password.Length];
            encode = Encoding.UTF8.GetBytes(obj.Password);
             msg = Convert.ToBase64String(encode);
            string strpsd = msg;
            int ckhEmail=  ValidateEmailId(obj.EmailId);

            if (ckhEmail > 0)
            {
                return false;
                
            }
            else
            {
                var query = "Insert into Employee ( EmployeeId,AlternateEmailId, FirstName,City,Address," +
                    "Marital_status,Emp_Joining_Date,CountryId,StateId,AlternateContact_No," +
                    "LastName,PinCode,DateofBirth,Gender," +
                    "Contact_No,Password,EmailId) " +
                    "values( @EmployeeId,@AlternateEmailId,@FirstName,@City ,@Address,@Marital_status," +
                    "@Emp_Joining_Date,@CountryId,@StateId,@AlternateContact_No,@LastName,@PinCode," +
                    "@DateofBirth,@Gender,@Contact_No,@Password,@EmailId)";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                com.Parameters.AddWithValue("@AlternateEmailId", obj.AlternateEmailId);
                com.Parameters.AddWithValue("@FirstName", obj.FirstName);
                com.Parameters.AddWithValue("@City", obj.City);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@EmailId", obj.EmailId);
                com.Parameters.AddWithValue("@CountryId", obj.CountryId);
                com.Parameters.AddWithValue("@StateId", obj.StateId);
                com.Parameters.AddWithValue("@Marital_status", obj.Marital_status);
                com.Parameters.AddWithValue("@Gender", obj.Gender);
                com.Parameters.AddWithValue("@LastName", obj.LastName); 
                com.Parameters.AddWithValue("@Password", strpsd);
                com.Parameters.AddWithValue("@PinCode", obj.PinCode);
                com.Parameters.AddWithValue("@AlternateContact_No", obj.AlternateContact_No);
                com.Parameters.AddWithValue("@Contact_No", obj.Contact_No);
                com.Parameters.AddWithValue("@Emp_Joining_Date", obj.Emp_Joining_Date);
                com.Parameters.AddWithValue("@DateofBirth", obj.DateofBirth);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }


        public DataSet Get_State(int CountryId)
        {
            connection();
            SqlCommand com = new SqlCommand("Select * from State where CountryId=@CountryId", con);
            com.Parameters.AddWithValue("@CountryId", CountryId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public int Get_StateByEmpId(int empId)
        {
            int res = 0;
            try
            {

                connection();
                SqlCommand com = new SqlCommand("Select CountryId from Employee where EmployeeId =@EmployeeId", con);
                com.Parameters.AddWithValue("@EmployeeId", empId);
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    res = Convert.ToInt32(dt.Rows[0]["CountryId"]);
                }
                else
                {
                    res = 0;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return res;
        }

        public int DeleteData(String EmployeeId)
        {
            connection();
            int result;
            try
            {

                SqlCommand com = new SqlCommand("Update Employee set Status =" + 0 + "  where  EmployeeId =@EmployeeId", con);

                com.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                con.Open();
                result = com.ExecuteNonQuery();
                return result;
            }
            catch (Exception er)
            {
                return result = 0;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateEmployee(EmployeeCreateModel obj)
        {
            connection();

            SqlCommand com = new SqlCommand("Update Employee set FirstName=@FirstName,City = @City" +
            ",Address = @Address, Marital_status = @Marital_status,Emp_Joining_Date = @Emp_Joining_Date," +
            "AlternateContact_No = @AlternateContact_No, LastName = @LastName," +
            "DateofBirth = @DateofBirth, Gender = @Gender, Contact_No = @Contact_No," +
            "CountryId=@CountryId,StateId=@StateId, EmailId = @EmailId,PinCode = @PinCode  " +
            "where  EmployeeId =@EmployeeId", con);
        
            com.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);

            com.Parameters.AddWithValue("@FirstName", obj.FirstName);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);
            com.Parameters.AddWithValue("@EmailId", obj.EmailId);
            com.Parameters.AddWithValue("@CountryId", obj.CountryId);

            com.Parameters.AddWithValue("@StateId", obj.StateId);
            com.Parameters.AddWithValue("@Marital_status", obj.Marital_status);
            com.Parameters.AddWithValue("@Gender", obj.Gender);
            com.Parameters.AddWithValue("@LastName", obj.LastName);
            //com.Parameters.AddWithValue("@Password", obj.Password);
            com.Parameters.AddWithValue("@PinCode", obj.PinCode);
            com.Parameters.AddWithValue("@AlternateContact_No", obj.AlternateContact_No);
            com.Parameters.AddWithValue("@Contact_No", obj.Contact_No);
            com.Parameters.AddWithValue("@Emp_Joining_Date", obj.Emp_Joining_Date);
            com.Parameters.AddWithValue("@DateofBirth", obj.DateofBirth);
            con.Open();

            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool GetLogin(EmployeeCreateModel obj1)
        {
            connection();
            string msg = "";
            byte[] encode = new byte[obj1.Password.Length];
            encode = Encoding.UTF8.GetBytes(obj1.Password);
            msg = Convert.ToBase64String(encode);
            string strpsd = msg;

            DataTable dt = new DataTable();

            SqlDataAdapter adp = new SqlDataAdapter();

            try
            {
                string query = "Select EmployeeId,FirstName,LastName,Is_Admin, Status from Employee where EmailId = @EmailId and Password = '" + strpsd + "'  and Status = 'True'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmailId", obj1.EmailId.Trim());
                cmd.Parameters.AddWithValue("@password", obj1.Password.Trim());
                cmd.Parameters.AddWithValue("@Is_admin", obj1.Is_admin.ToString()); 

                adp.SelectCommand = cmd;
                adp.Fill(dt);
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    obj1.FirstName = dt.Rows[0]["FirstName"].ToString();
                    obj1.LastName = dt.Rows[0]["LastName"].ToString();
                    obj1.Is_admin = Convert.ToBoolean(dt.Rows[0]["Is_admin"]);                    
                    obj1.EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                dt.Clear();
                dt.Dispose();         
                adp.Dispose();
            }
        }


        public bool GetForgetPassword(EmployeeCreateModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("Update Employee Set Password = @password where EmailId =@EmailId ", con);
            com.Parameters.AddWithValue("@EmailId", obj.EmailId);
            com.Parameters.AddWithValue("@Password", obj.Password);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}