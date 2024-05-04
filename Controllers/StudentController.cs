using StudentCRUD.context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentCRUD.Controllers
{
    public class StudentController : Controller
    {
        CRUDStudentEntities dbObj= new CRUDStudentEntities();

        // GET: Student
        public ActionResult Student(Student obj)
        {
            if(obj != null)
            {
                return View(obj);
            }
            else 
            { 
                return View(); 
            }
           
        }
        [HttpPost]
        public ActionResult AddStudent(Student model)
        {
            if(ModelState.IsValid)
            {
                Student obj = new Student();
                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.Fname = model.Fname;
                obj.Email = model.Email;
                obj.Phone = model.Phone;
                obj.Description = model.Description;

                if(model.ID == 0) 
                {
                    dbObj.Students.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                    

               
            }
            
            ModelState.Clear();
            return View("Student");
        }

        public ActionResult StudentList()
        {
            var res=dbObj.Students.ToList();
            return View(res);
        }
        public ActionResult UpdateStudent()
        {
            return View();
        }

        public ActionResult DeleteStudent(int id)
        {
            var res = dbObj.Students.Where(x => x.ID == id).First();
            dbObj.Students.Remove(res);
            dbObj.SaveChanges();
            var list = dbObj.Students.ToList();
            return View("StudentList",list);
        }
    }
}