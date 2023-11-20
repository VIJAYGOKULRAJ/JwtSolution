using JwtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace JwtDemo.Controllers
{
    public class SavedSearchViewsController : ApiController
    {

        LoginContext Context = new LoginContext();
        [Authorize(Roles = "Admin, user")]

        public IEnumerable<SavedSearchViews> Get()
        {
            return Context.SavedSearchViews.ToList();
        }
        [Authorize(Roles = "Admin, user")]
        public SavedSearchViews Get(int id)
        {
            return Context.SavedSearchViews.Find(id);
        }
        [Authorize(Roles = "Admin, user")]
        public string Post(int id, SavedSearchViews row)
        {
            if (!Context.SavedSearches.Any(value => value.Id == id))
            {
                return "Invalid id. Data not saved.";
            }

            Context.SavedSearchViews.Add(row);
            Context.SaveChanges();
            return "Data has been Stored";


        }

        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Put(int id, [FromBody] SavedSearchViews New_details)
        {
            if (New_details == null)
            {
                return NotFound();
            }
            SavedSearchViews Previous_data = Context.SavedSearchViews.Find(id);
            if (Previous_data == null)
                return NotFound();
            Previous_data.FieldNameJson = New_details.FieldNameJson;
            Previous_data.Created = New_details.Created;
            Previous_data.CreatedBy = New_details.CreatedBy;
            Previous_data.LastModified = New_details.LastModified;
            Previous_data.LastModifiedBy = New_details.LastModifiedBy;
            Previous_data.Name = New_details.Name;
            Previous_data.UserId = New_details.UserId;
            Previous_data.Type = New_details.Type;

            Context.SaveChanges();
            return Ok("Data modified...");
        }
        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Delete(Guid id)
        {
            Context.SavedSearchViews.Remove(Context.SavedSearchViews.Find(id));
            Context.SaveChanges();
            return Ok("Deleted the row ");
        }
    }
}