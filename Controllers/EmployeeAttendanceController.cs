﻿using EmployeeAttendenceMangement.AMSConnecction;
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
            EmployeeAttandenceModel model = new EmployeeAttandenceModel();
            int EmpAttendance = EmpAttandence_layer.GetEmployeeId(EmpAtendenceId,model);
           
            Session["AltlastName"] = model.LastName;
            Session["AltEmployeeId"] = model.EmployeeId;
            Session["AltFirstName"] = model.FirstName;
            var outtime = model.OutTime;


            var Data1 = outtime.ToString().Substring(0, 5);
            if (Data1 == "00:00" )
           {
                Session["Outtime"] = null;

           }
            else
            {
                Session["Outtime"] = model.OutTime.ToString().Substring(0, 5);
                 
            }


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
        public string CreateBtnHide()
        {
            EmployeeAttandenceDateLayer EmpAttandence_layer = new EmployeeAttandenceDateLayer();

            if (EmpAttandence_layer.CreateBtnHide(Convert.ToInt32(Session["EmployeeId"])))
            {
                return "1";
            }
            else
            {
                return "0";
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
