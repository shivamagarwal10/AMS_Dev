using EmployeeAttendenceMangement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.AMSConnecction
{
    public class EmployeeProjectMapping_layer
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ToString();
            con = new SqlConnection(constr);

        }


        public DataSet Get_EmployeeName(int ProjectCode)
        {
            connection();
            SqlCommand com = new SqlCommand("select  em.FirstName , em.EmployeeId from Employee as em  where em.EmployeeId  not in (select Employee_Id from Emp_Project_Mapping where Project_Id = @ProjectCode)", con);
            
            com.Parameters.AddWithValue("@ProjectCode", ProjectCode);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public bool AddEmployeeProject(EmployeeProjectMapping obj)
        {
            connection();     
            SqlCommand com = new SqlCommand("insert into Emp_Project_Mapping Values(@Project_Id,@Employee_Id)", con);
            
            com.Parameters.AddWithValue("@Project_Id", obj.Project_Id);
            com.Parameters.AddWithValue("@Employee_Id", obj.Employee_Id);
           

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