using Hotel.BLL;      
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http; 

namespace Hotel.API.Controllers
{
    public class GuestController : ApiController
    {
        private GuestService _Gservice = new GuestService();

        // GET: hotel/Guest
        // Show list of guests
        [HttpGet]
        public IHttpActionResult GetGuests()
        {
            try
            {
                DataTable dt = _Gservice.GetAllGuests();
                // ASP.NET automatically converts objects/tables to JSON
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // 3. POST: api/Guest
        // Add guest
        [HttpPost]
        public IHttpActionResult AddGuest([FromBody] GuestModel model)
        {
            if (model == null) return BadRequest("No data received");

            string result = _Gservice.SaveGuest(
                model.FName, model.MName, model.LName,
                model.Address, model.Contact, model.Email, model.Gender
            );

            if (result == "Success")
                return Ok("Guest Added");
            else
                return BadRequest(result);
        }
    }

    // helper class to receive the JSON data
    public class GuestModel
    {
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}