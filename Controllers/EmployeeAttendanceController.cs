using EmployeeAttendenceMangement.AMSConnecction;
using EmployeeAttendenceMangement.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAttendenceMangement.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        public ActionResult EmployeeAttandenceDetail(int? page, int? pageSize)
        {
            EmployeeAttandenceDateLayer EmpAttandence_layer = new EmployeeAttandenceDateLayer();           
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 10);
            ViewBag.psize = defaSize;
            ModelState.Clear();
            
            return View(EmpAttandence_layer.GetMarkAttandence(Convert.ToBoolean(Session["Is_admin"]), Convert.ToInt32(Session["EmployeeId"])).ToList().ToPagedList(pageIndex, defaSize));
        }
        
        public ActionResult CreateAttandence()
        {
         
                return View();
              
        }

        // POST: EmployeeAttendance/Create
        [HttpPost]
        public ActionResult CreateAttandence(EmployeeAttandenceModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeAttandenceDateLayer EmpAttandence_layer = new EmployeeAttandenceDateLayer();

                    if (EmpAttandence_layer.createAttandence(model))
                    {
                        ViewBag.Message = "Employee details added successfully";

                        Session["Date"] = model.Date.ToString();
                        
                        Session["EmployeeId"] = model.EmployeeId.ToString();
                        return Redirect("EmployeeAttandenceDetail");
                    }

                }

                return View();
            }
            catch(Exception er)
            {
                return View();
            }
        }


        public ActionResult UpdateAttendance(int EmpAtendenceId)
        {
            EmployeeAttandenceDateLayer EmpAttandence_layer = new EmployeeAttandenceDateLayer();

            return View(EmpAttandence_layer.GetMarkAttandence().Find(Emp => Emp.EmpAtendenceId == EmpAtendenceId));
        }

        
        [HttpPost]
        public ActionResult UpdateAttendance(EmployeeAttandenceModel obj)
        {
            try
            {
                EmployeeAttandenceDateLayer EmpAttandence_layer = new EmployeeAttandenceDateLayer();

                EmpAttandence_layer.UpdateEmployeeAttandence(obj);
                return Redirect("EmployeeAttandenceDetail");
            }
            catch (Exception er)
            {
                return View();
            }
        }

        public ActionResult EmpAttendanceDelete(String EmpAtendenceId)
        {

            try
            {
                EmployeeAttandenceDateLayer EmpAttandence_layer = new EmployeeAttandenceDateLayer();
                int result = EmpAttandence_layer.DeleteData(EmpAtendenceId);
                TempData["result3"] = result;
                ModelState.Clear();
                return RedirectToAction("EmployeeAttandenceDetail");
            }
            catch(Exception er)
            {
                return View();
            }
        }
    }
}
