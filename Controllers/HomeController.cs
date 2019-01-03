using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sp_Entity.Models;
using System.Data.SqlClient;
namespace Sp_Entity.Controllers

{
    public class HomeController : Controller
    {
       

        public SqlParameter SqlParameter { get; private set; }

        // GET: Home
        public ActionResult Index()
        {
            var db = new ProjectDataContext();
            var result = db.sp_Student_All();
            return View(result.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var db = new ProjectDataContext();
            SqlParameter par = new SqlParameter("@StudentId",id);
            var data = db.Database.SqlQuery<Student>("exec sp_Student_id @StudentId", par).SingleOrDefault();
            return View(data);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                SqlParameter par  = new SqlParameter("@Name", student.Name);
                SqlParameter par1 = new SqlParameter("@Contact", student.Contact);
                SqlParameter par2 = new SqlParameter("@City", student.City);
                SqlParameter par3 = new SqlParameter("@Address", student.Address);
                var db = new ProjectDataContext();
               var data = db.Database.ExecuteSqlCommand("sp_Student_Add @Name,@Contact,@City,@Address", par, par1, par2, par3);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var db = new ProjectDataContext();
            SqlParameter par = new SqlParameter("@StudentId", id);
            var data = db.Database.SqlQuery<Student>("sp_Student_id @StudentId", par).SingleOrDefault();
            return View(data);
           
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                var db = new ProjectDataContext();
                SqlParameter par = new SqlParameter("@StudentId", student.StudentId);
                SqlParameter par1 = new SqlParameter("@Name", student.Name);
                SqlParameter par2 = new SqlParameter("@Contact", student.Contact);
                SqlParameter par3 = new SqlParameter("@City", student.City);
                SqlParameter par4 = new SqlParameter("@Address", student.Address);
                var data = db.Database.ExecuteSqlCommand("sp_Student_Update @StudentId, @Name,@Contact,@City,@Address", par, par1, par2, par3);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var db = new ProjectDataContext();
            SqlParameter par = new SqlParameter("@StudentId", id);
            var data = db.Database.SqlQuery<Student>("exec sp_Student_id @StudentId", par).SingleOrDefault();
            return View(data);
            
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var db = new ProjectDataContext();
                SqlParameter par = new SqlParameter("@StudentId", id);
                var data = db.Database.SqlQuery<Student>("exec sp_Student_Delete @StudentId", par).SingleOrDefault();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
