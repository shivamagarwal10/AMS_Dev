﻿using EmployeeAttendenceMangement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.AMSConnecction
{
    public class EmployeeAttandenceDateLayer
    {

        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ToString();
            con = new SqlConnection(constr);

        }
        public List<EmployeeAttandenceModel> GetMarkAttandence(bool is_admin, int EmployeeId)
        {
                connection();

            List<EmployeeAttandenceModel> EmpAttandenceList = new List<EmployeeAttandenceModel>();

            string str = "";
            if (is_admin == true)
            {
                str = " c.is_admin  in(1,0) ";
            }
            else
            {
                str = " c.is_admin=0 and c.EmployeeId =" + EmployeeId + " ";
            }
            var query = " select ea.EmpAtendenceId,ea.EmployeeId, c.FirstName,ea.Duration,ea.Date,ea.Intime,ea.OutTime,ea.latitude,ea.longitude From EmpAtendance as ea INNER JOIN Employee as c  on ea.EmployeeId =c.EmployeeId  where " + str + " ORDER BY ea.EmpAtendenceId DESC ";

            SqlCommand com = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                EmpAttandenceList.Add(
                    new EmployeeAttandenceModel
                    {


                        FirstName = Convert.ToString(dr["FirstName"]),
                        EmpAtendenceId = Convert.ToInt32(dr["EmpAtendenceId"]),
                        Intime = TimeSpan.Parse(dr["Intime"].ToString()),
                        OutTime = (TimeSpan)(dr["OutTime"] != DBNull.Value ? dr["OutTime"] : new TimeSpan()),
                        latitude = Convert.ToString(dr["latitude"]),
                        longitude = Convert.ToString(dr["longitude"]),

                        Duration = Convert.ToString(dr["Duration"] != null ? dr["Duration"] : (object)DBNull.Value),

                        Date = Convert.ToDateTime(dr["Date"]),
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),

                    }
                    );
            }

            return EmpAttandenceList;
        }
        public List<EmployeeAttandenceModel> GetMarkAttandence()
        {
            connection();

            List<EmployeeAttandenceModel> EmpAttandenceList = new List<EmployeeAttandenceModel>();


            var query = " select ea.EmpAtendenceId,ea.EmployeeId, c.FirstName,ea.Duration,ea.Date,ea.Intime,ea.OutTime,ea.latitude,ea.longitude From EmpAtendance as ea INNER JOIN Employee as c  on ea.EmployeeId =c.EmployeeId   ORDER BY EmpAtendenceId DESC ";

            SqlCommand com = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();



            foreach (DataRow dr in dt.Rows)
            {
                EmpAttandenceList.Add(
                    new EmployeeAttandenceModel
                    {
                        FirstName = Convert.ToString(dr["FirstName"]),
                        EmpAtendenceId = Convert.ToInt32(dr["EmpAtendenceId"]),
                        Intime = TimeSpan.Parse(dr["Intime"].ToString()),
                        OutTime = (TimeSpan)(dr["OutTime"] != DBNull.Value ? dr["OutTime"] : new TimeSpan()),
                        latitude = Convert.ToString(dr["latitude"]),
                        longitude = Convert.ToString(dr["longitude"]),

                        Duration = Convert.ToString(dr["Duration"] != null ? dr["Duration"] : (object)DBNull.Value),

                        Date = Convert.ToDateTime(dr["Date"]),
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                    }
                    );
            }

            return EmpAttandenceList;
        }
        //public bool GetLogin(EmployeeAttandenceModel obj1)
        //{
        //    connection();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter adp = new SqlDataAdapter();

        //    try
        //    {

        //        SqlCommand cmd = new SqlCommand("Select * from EmpAtendance   );
        //        cmd.Parameters.AddWithValue("@Date", obj1.Date);


        //        adp.SelectCommand = cmd;
        //        adp.Fill(dt);
        //        cmd.Dispose();
        //        if (dt.Rows.Count > 0)
        //        {
        //            obj1.Date = dt.Rows[0]["Date"].;



        //            return true;

        //        }
        //        else
        //        {
        //            return false;

        //        }
        //    }
        //    finally
        //    {
        //        dt.Clear();
        //        dt.Dispose();
        //        adp.Dispose();
        //    }
        //}
        public bool createAttandence(EmployeeAttandenceModel obj)
        {
            connection();
            var query = " insert into EmpAtendance(EmployeeId,Intime,OutTime,latitude,longitude,Duration,Date) Values(@EmployeeId,@Intime,@OutTime,@latitude,@longitude,@Duration,@Date)";
            SqlCommand com = new SqlCommand(query, con);


            com.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);


            com.Parameters.AddWithValue("@OutTime", obj.OutTime != null ? obj.OutTime : (object)DBNull.Value);

            com.Parameters.AddWithValue("@Duration", obj.Duration != null ? obj.Duration : (object)DBNull.Value);
            com.Parameters.AddWithValue("@Intime", obj.Intime);

            com.Parameters.AddWithValue("@latitude", obj.latitude);
            com.Parameters.AddWithValue("@longitude", obj.longitude);

            com.Parameters.AddWithValue("@Date", obj.Date);



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
        public bool UpdateEmployeeAttandence(EmployeeAttandenceModel obj)
        {

            connection();
            var query = "update EmpAtendance set EmployeeId = @EmployeeId,Intime=@Intime, OutTime=@OutTime,latitude=@latitude, Duration=@Duration,Date=@Date where EmpAtendenceId =@EmpAtendenceId";
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("@EmpAtendenceId", obj.EmpAtendenceId);
            com.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);

            com.Parameters.AddWithValue("@Intime", obj.Intime);
            com.Parameters.AddWithValue("@OutTime", obj.OutTime);
            com.Parameters.AddWithValue("@latitude", obj.latitude);
            com.Parameters.AddWithValue("@longitude", obj.longitude);
            com.Parameters.AddWithValue("@Duration", obj.Duration);
            com.Parameters.AddWithValue("@Date", obj.Date);

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