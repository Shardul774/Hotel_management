using Hotel.DAL;
using System;
using System.Data;

namespace Hotel.BLL
{
    public class GuestService
    {
        private GuestRepository _repo = new GuestRepository();

        public DataTable GetAllGuests()
        {
            return _repo.GetGuests();
        }
        public string SaveGuest(string fName, string mName, string lName, string address, string contact, string email, string gender)
        {
            // Business Logic Rules
            if (string.IsNullOrWhiteSpace(fName) || string.IsNullOrWhiteSpace(lName))
            {
                return "Error: First and Last Name are required.";
            }

            if (contact.Length < 10)
            {
                return "Error: Phone number must be at least 10 digits.";
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                return "Error: Invalid Email format.";
            }

            // call dal method
            try
            {
                bool isSuccess = _repo.AddGuest(fName, mName, lName, address, contact, email, gender);
                if (isSuccess)
                    return "Success";
                else
                    return "Error: Guest was not saved.";
            }
            catch (Exception ex)
            {
                return "Critical Error: " + ex.Message;
            }
        }
    }
}