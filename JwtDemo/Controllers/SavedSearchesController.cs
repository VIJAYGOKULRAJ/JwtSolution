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
    public class SavedSearchesController : ApiController
    {
        
            LoginContext Context = new LoginContext();

        [Authorize(Roles = "Admin, user")]
        public IEnumerable<object> Get()
            {
                return Context.SavedSearches.Select(value => new
                {
                    SearchName = value.Name,
                    User = value.UserId,
                    DateLastModified = value.LastModifiedBy
                }).ToList();
            }
        [Authorize(Roles = "Admin, user")]
        public SavedSearches Get(int id)
            {
                return Context.SavedSearches.Find(id);
            }
        [Authorize(Roles = "Admin, user")]
        public string Post(SavedSearches row)
            {
                if (Context.SavedSearches.Any(value => value.Name == row.Name))
                {
                    return "Duplicate name. Data not saved.";
                }


                Context.SavedSearches.Add(row);
                Context.SaveChanges();
                return "Data has been Stored";
            }
        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Put(int id, [FromBody] SavedSearches New_details)
            {
                if (New_details == null)
                {
                    return NotFound();
                }
                SavedSearches Previous_data = Context.SavedSearches.Find(id);
                if (Previous_data == null)
                    return NotFound();
                Previous_data.Name = New_details.Name;
                Previous_data.UserId = New_details.UserId;
                Previous_data.SearchQueryJson = New_details.SearchQueryJson;
                Previous_data.Created = New_details.Created;
                Previous_data.CreatedBy = New_details.CreatedBy;
                Previous_data.LastModified = New_details.LastModified;
                Previous_data.LastModifiedBy = New_details.LastModifiedBy;
                Previous_data.SavedSearchViewId = New_details.SavedSearchViewId;
                Previous_data.Type = New_details.Type;



                Context.SaveChanges();
                return Ok("Data modified...");
            }
        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Delete(int id)
            {
                Context.SavedSearches.Remove(Context.SavedSearches.Find(id));
                Context.SaveChanges();
                return Ok("Deleted the row ");
            }
        }
    
}