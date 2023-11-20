using FluentValidation;
using JwtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Searches.Controllers
{


    public class UsersController : ApiController
    {
        LoginContext Context = new LoginContext();

        [Authorize(Roles = "Admin, user")]
        public IEnumerable<Users> Get()
        {

            return Context.Users.ToList();
        }

        [Authorize(Roles = "Admin, user")]
        public Users Get(Guid id)
        {
            return Context.Users.Find(id);
        }

        [Authorize(Roles = "Admin , user")]
        // POST api/values
        public HttpResponseMessage Post(Users row)
        {
            if (User.IsInRole("user"))
            {
               
                return Request.CreateResponse(HttpStatusCode.Forbidden, "You do not have permission to perform this action. Admin only access the action");
            }

            Context.Users.Add(row);
            Context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Details have been created");
        }





        [Authorize(Roles = "Admin")]
        // PUT api/values/5
        public string Put(Guid id, [FromBody] Users New_details)
        {
            if (New_details == null)
                return "Invalid Data...";
            Users Previous_data = Context.Users.Find(id);
            if (Previous_data == null)
                return "Data Not Found";

            Previous_data.FirstName = New_details.FirstName;
            Previous_data.LastName = New_details.LastName;
            Previous_data.FullName = New_details.FullName;
            Previous_data.Email = New_details.Email;
            Previous_data.ReportsTo = New_details.ReportsTo;
            Previous_data.Title = New_details.Title;
            Previous_data.IsActive = New_details.IsActive;
            Previous_data.CreateDate = New_details.CreateDate;
            Previous_data.LastLoggedInDate = New_details.LastLoggedInDate;

            Context.SaveChanges();
            return "Modified the row!";
        }
        [Authorize(Roles = "Admin")]
        // DELETE api/values/5
        public IHttpActionResult Delete(Guid id)
        {
            Context.Users.Remove(Context.Users.Find(id));
            Context.SaveChanges();
            return Ok("Deleted the row ");
        }
    }
}