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

  
    public class ProjectMasterController : Controller
    {

        ProjectDataLayer Project_Layer = new ProjectDataLayer();
     
        public ActionResult ProjectDetail(int? page, int? pageSize)
        {
            ProjectDataLayer Project_Layer = new ProjectDataLayer();

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            ModelState.Clear();

            return View(Project_Layer.GetProjectMaster().ToList().ToPagedList(pageIndex, defaSize));
        }

     
        // GET: ProjectMaster/Create
        public ActionResult CreateProject()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult CreateProject(ProjectMasterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectDataLayer Project_Layer = new ProjectDataLayer();

                    if (Project_Layer.AddProject(model))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return Redirect("ProjectDetail");
            }
            catch(Exception er)
            {
                return View();
            }
        }


        public ActionResult UpdateProject(int ProjectId)
        {
            ProjectDataLayer Project_Layer = new ProjectDataLayer();

            return View(Project_Layer.GetProjectMaster().Find(Emp => Emp.ProjectId == ProjectId));
        }

        // POST: ProjectMaster/Edit/5
        [HttpPost]
        public ActionResult UpdateProject( ProjectMasterModel model)
        {
            try
            {
                ProjectDataLayer Project_Layer = new ProjectDataLayer();

                Project_Layer.UpdateProjectMaster(model);
                return RedirectToAction("Index","Home");
            }
            catch(Exception er)
            {
                return View();
            }
        }

        // GET: ProjectMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
