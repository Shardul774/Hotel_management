using Hotel.BLL;
using Hotel.DAL; // Needed to recognize 'Guest' class
using System;
using System.Collections.Generic; // Needed for List<>
using System.Web.Http;

namespace Hotel.API.Controllers
{
    public class GuestController : ApiController
    {
        private GuestService _Gservice = new GuestService();

        // GET: hotel/Guest
        [HttpGet]
        //[Route("hotel/Guest")]
        public IHttpActionResult GetGuests()
        {
            try
            {
                // CHANGED: DataTable -> List<Guest>
                List<Guest> guests = _Gservice.GetAllGuests();
                return Ok(guests);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST stays the same because it calls SaveGuest with strings, 
        // which matches the BLL signature we just fixed.
        [HttpPost]
        //[Route("hotel/Guest")]
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