using EmployeeAttendenceMangement.AMSConnecction;
using EmployeeAttendenceMangement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAttendenceMangement.Controllers
{
    public class EmployeeProjectMappingController : Controller
    {
        EmployeeProjectMapping_layer projectDataLayer = new EmployeeProjectMapping_layer();
        public ActionResult Project_EmployeeMapping()
        {

            ViewBag.ProjectLists = ProjectList();
            return View();
        }
        [HttpPost]
        public ActionResult Project_EmployeeMapping(EmployeeProjectMapping model)
        {
            try
            {
              
                    if (projectDataLayer.AddEmployeeProject(model))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                         
                return RedirectToAction("Index","Home", new { area = "" }); 
            }
            catch(Exception er)
            {
                return View();
            }
        }

        private static List<EmployeeProjectMapping> ProjectList()
        {
            List<EmployeeProjectMapping> Projects = new List<EmployeeProjectMapping>();
            string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select * from Project_Master";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Projects.Add(new EmployeeProjectMapping
                            {
                            ProjectName   = sdr["ProjectName"].ToString(),
                                Project_Id = Convert.ToInt32(sdr["ProjectId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return Projects;
        }

        public JsonResult GetEmployeeName(int ProjectCode)
        {
            DataSet ds = projectDataLayer.Get_EmployeeName(ProjectCode);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                statelist.Add(new SelectListItem { Text = dr["FirstName"].ToString(), Value = dr["EmployeeId"].ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }

 



    }
}
