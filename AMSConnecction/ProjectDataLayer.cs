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
    public class ProjectDataLayer
    {

        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ToString();
            con = new SqlConnection(constr);
        }

        public List<ProjectMasterModel> GetProjectMaster()
        {
            connection();
            List<ProjectMasterModel> ProjectList = new List<ProjectMasterModel>();


            SqlCommand com = new SqlCommand("Select * from  Project_Master ORDER BY ProjectId DESC  ", con);
         
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                ProjectList.Add(

                    new ProjectMasterModel
                    {

                        ProjectId = Convert.ToInt32(dr["ProjectId"]),
                        ProjectName = Convert.ToString(dr["ProjectName"]),
                        Description = Convert.ToString(dr["Description"]),
                        ProjectCode = Convert.ToInt32(dr["ProjectCode"])

                    }
                    );
            }

            return ProjectList;
        }

        public bool AddProject(ProjectMasterModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("Insert into Project_Master( ProjectName ,Description , ProjectCode )values(@ProjectName , @Description ,  @ProjectCode ) ", con);
           
            com.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
            com.Parameters.AddWithValue("@Description", obj.Description);
            com.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);


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
        public bool UpdateProjectMaster(ProjectMasterModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("Update Project_Master set  ProjectName=@ProjectName,Description = @Description,ProjectCode = @ProjectCode Where ProjectId=@ProjectId", con);

         
            com.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
            com.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
            com.Parameters.AddWithValue("@Description", obj.Description);
            com.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);

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