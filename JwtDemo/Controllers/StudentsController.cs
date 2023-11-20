using JwtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace JwtDemo.Controllers
{
    [Authorize(Roles =  "user")]
    public class StudentsController : ApiController
    {
        LoginContext Context = new LoginContext();
        // GET api/values
        public IEnumerable<object> Get()
        {
            var students = Context.Student.AsQueryable();

        /*    if (!string.IsNullOrEmpty(gender))
            {
                students = students.Where(s => s.Gender == gender);
            }*/

            var result = students.Select(s => new
            {
                Name = s.Name,
                Password = s.Password,
                Gender = s.Gender,
            }).AsEnumerable();

            return result;
        }



        // GET api/values/5
        public StudentDetails Get(int id)
        {
            return Context.Student.Find(id);
        }

        // POST api/values
        public HttpResponseMessage Post(StudentDetails row)
        {
           

            Context.Student.Add(row);
            Context.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Details have been created");
        }



        // PUT api/values/5
        public string Put(int id, [FromBody] StudentDetails Student_item)
        {
            if (Student_item == null)
                return "Invalid data....";



            StudentDetails prev_details = Context.Student.Find(id);

            if (prev_details == null)
                return "Data Not Found";


            prev_details.ID = Student_item.ID;
            prev_details.Name = Student_item.Name;
            prev_details.Age = Student_item.Age;
            prev_details.Gender = Student_item.Gender;
            prev_details.Class = Student_item.Class;
            prev_details.Password = Student_item.Password;

            Context.SaveChanges();
            return "Modified the row!";
        }


        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            Context.Student.Remove(Context.Student.Find(id));
            Context.SaveChanges();
            return Ok("Deleted the row ");
        }
    }
}