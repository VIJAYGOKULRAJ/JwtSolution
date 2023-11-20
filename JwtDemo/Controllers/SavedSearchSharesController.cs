using FluentValidation;
using JwtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace JwtDemo.Controllers
{
    public class SavedSearchSharesController : ApiController
    {
        LoginContext Context = new LoginContext();
        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Get()
        {

            var result = (from savedsearch in Context.SavedSearches
                          join savedsearchshare in Context.SavedSearchShares on savedsearch.Id equals savedsearchshare.Id

                          select new
                          {
                              savedsearchshare.Id,
                              savedsearchshare.SavedSearchId,
                              savedsearchshare.SharedUserId,
                              savedsearchshare.Created,
                              savedsearchshare.CreatedBy,
                              savedsearchshare.LastModified,
                              savedsearchshare.LastModifiedBy
                          }).ToList();

            return Ok(result);
        }
        [Authorize(Roles = "Admin, user")]
        public SavedSearchShares Get(int id)
        {
            return Context.SavedSearchShares.Find(id);
        }
        [Authorize(Roles = "Admin, user")]
        public HttpResponseMessage Post(Guid SharedUserId, SavedSearchShares mail)
        {
            var validator = new InlineValidator<SavedSearchShares>();

            validator.RuleFor(valid => valid.SavedSearchId)
                .NotEmpty().WithMessage("Please enter the Valid ID");
            validator.RuleFor(valid => valid.SharedUserId)
                .NotEmpty().WithMessage("Please enter the Valid ID");


            var validationResult = validator.Validate(mail);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }
            using (var Context = new LoginContext())
            {
                Context.SavedSearchShares.Add(mail);
                Context.SaveChanges();
            }


            return Request.CreateResponse(HttpStatusCode.OK, "Data Saved");
        }

        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Put(int id, [FromBody] SavedSearchShares New_details)
        {
            if (New_details == null)
            {
                return NotFound();
            }
            SavedSearchShares Previous_data = Context.SavedSearchShares.Find(id);
            if (Previous_data == null)
                return NotFound();
            Previous_data.SavedSearchId = New_details.SavedSearchId;
            Previous_data.SharedUserId = New_details.SharedUserId;
            Previous_data.Created = New_details.Created;
            Previous_data.CreatedBy = New_details.CreatedBy;
            Previous_data.LastModified = New_details.LastModified;
            Previous_data.LastModifiedBy = New_details.LastModifiedBy;
            Context.SaveChanges();
            return Ok("Data modified...");
        }


        [Authorize(Roles = "Admin, user")]
        public IHttpActionResult Delete(int id)
        {
            Context.SavedSearchShares.Remove(Context.SavedSearchShares.Find(id));
            Context.SaveChanges();
            return Ok("Deleted the row ");
        }
    }
}