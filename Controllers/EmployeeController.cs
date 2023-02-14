using EmployeeAttendenceMangement.AMSConnecction;
using EmployeeAttendenceMangement.Models;
using PagedList;
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
    public class EmployeeController : Controller
    {
        AMS_Access_Layer access_Layer = new AMS_Access_Layer();

        public ActionResult EmployeeDetail(int? page, int? pageSize)
        {
            AMS_Access_Layer access_Layer = new AMS_Access_Layer();
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 10);
            ViewBag.psize = defaSize;
            ModelState.Clear();
            
            return View(access_Layer.GetEmployee(Convert.ToBoolean(Session["Is_admin"]), Convert.ToInt32(Session["EmployeeId"])).ToList().ToPagedList(pageIndex, defaSize));

        }   
        public ActionResult Create()
        {
            ViewBag.CountryList = CountryList();
            EmployeeCreateModel model = new EmployeeCreateModel();
            int maxId = access_Layer.GetEmployeeId(model);

            if (maxId == 0)
            {
                model.EmployeeId = 2019-000001;
            }
            else
            {
                model.EmployeeId = Convert.ToInt32(DateTime.Now.Year.ToString() + "" + "000" + "" + (model.EmployeeId+1).ToString());
               
            }


            if (Convert.ToBoolean(Session["Is_admin"]) == true)
            {
                return View(model);
            }

            return Redirect("EmployeeDetail");
        }
        private static List<Country> CountryList()
        {
            List<Country> countries = new List<Country>();
            string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select * from Country";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            countries.Add(new Country
                            {
                                CountryName = sdr["CountryName"].ToString(),
                                CountryId = Convert.ToInt32(sdr["CountryId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return countries;
        }

        public JsonResult State_Bind(int CountryId)
        {

            DataSet ds = access_Layer.Get_State(CountryId);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                statelist.Add(new SelectListItem { Text = dr["StateName"].ToString(), Value = dr["StateId"].ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(String EmployeeId)   
        {

            try
            {
                AMS_Access_Layer access_Layer = new AMS_Access_Layer();
                int result = access_Layer.DeleteData(EmployeeId);
                TempData["result3"] = result;
                ModelState.Clear();
                return RedirectToAction("EmployeeDetail");
            }
            catch (Exception er)
            {
                return View();
            }
        }
        public string ValidateEmailId(string emailId)
        {
            AMS_Access_Layer access_Layer = new AMS_Access_Layer();
        
            if (access_Layer.ValidateEmailId(emailId))
            {
                return "1";
            }
            else
            {
                return "0";
            }

        }

        [HttpPost]
        public ActionResult Create(EmployeeCreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AMS_Access_Layer access_Layer = new AMS_Access_Layer();

                    if (access_Layer.AddEmployee(model))
                    {
                        ViewBag.Message = "Employee details added successfully";
                        return RedirectToAction("EmployeeDetail");
                    }
                }

                TempData["EmailValidation"] = "EmailId already  registerted";
                return View();
            }
            catch (Exception er)
            {
                return View();
            }
        }

        private static List<State> GetStateByEmp(int countryid)
        {
            List<State> states = new List<State>();
            string constr = ConfigurationManager.ConnectionStrings["AttendanceManagementConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select * from State where CountryId ="+countryid+" ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            states.Add(new State
                            {
                                StateName = sdr["StateName"].ToString(),
                                StateId = Convert.ToInt32(sdr["StateId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return states;
        }
        public ActionResult Edit(int EmployeeId)
        {
            AMS_Access_Layer access_Layer = new AMS_Access_Layer();
            int StateByEmpId = access_Layer.Get_StateByEmpId(EmployeeId);
            ViewBag.CountryList = CountryList();
            ViewBag.CountryList1 = GetStateByEmp(StateByEmpId);
            return View(access_Layer.GetEmployee().Find(EmployeeCreateModel => EmployeeCreateModel.EmployeeId == EmployeeId));
        }

        [HttpPost]
        public ActionResult Edit(EmployeeCreateModel obj,int EmployeeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AMS_Access_Layer access_Layer = new AMS_Access_Layer();
                    if (access_Layer.UpdateEmployee(obj))
                    {
                        return RedirectToAction("EmployeeDetail");
                    }
                }
                int StateByEmpId = access_Layer.Get_StateByEmpId(EmployeeId);
                ViewBag.CountryList = CountryList();
                ViewBag.CountryList1 = GetStateByEmp(StateByEmpId);
                return View();
            }

            catch (Exception er)
            {
                return View();
            }
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EmployeeCreateModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    AMS_Access_Layer access_Layer = new AMS_Access_Layer();

                    if (access_Layer.GetLogin(model))
                    { 
                        Session["FirstName"] = model.FirstName;
                        Session["Is_admin"] = model.Is_admin;
                        Session["LastName"] = model.LastName;                       
                        Session["EmployeeId"] = model.EmployeeId;
                     

                        ViewBag.Message = "Employee details added successfully";
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (model.FirstName != null)
                {
                    TempData["msg2"] = " Invalid Password  Retry !!";
                }
                else
                {
                    TempData["msg1"] = "No account found for this email. Retry !!";
                }

                return View();
            }
            catch (Exception er)
            {
                return View();
            }
        }


        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("Login");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(EmployeeCreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AMS_Access_Layer access_Layer = new AMS_Access_Layer();
                    if (access_Layer.GetForgetPassword(model))
                    {
                        ViewBag.Message = "Employee details added successfully";
                        return Redirect("Login");
                    }

                }
                TempData["msg"] = "No account found for this email. Retry  !!";
                return View();
            }
            catch
            {
                return View();

            }
        }

    }
}
