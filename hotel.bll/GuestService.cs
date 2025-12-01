using Hotel.DAL;
using System;
using System.Collections.Generic; // Required for List<>

namespace Hotel.BLL
{
    public class GuestService
    {
        private GuestRepository _repo = new GuestRepository();

        // CHANGED: Return List<Guest> instead of DataTable
        public List<Guest> GetAllGuests()
        {
            return _repo.GetGuests();
        }

        // CHANGED: Logic to create a Guest object
        public string SaveGuest(string fName, string mName, string lName, string address, string contact, string email, string gender)
        {
            // 1. Validation Logic (Kept the same)
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

            // 2. Create the Guest Object (New EF Logic)
            Guest newGuest = new Guest
            {
                GuestFName = fName,
                GuestMName = mName ?? "",
                GuestLName = lName,
                GuestAddress = address,
                GuestContactNumber = contact,
                GuestEmail = email,
                GuestGender = gender,
                // Default values are handled in DAL or here
                Status = "Active",
                Remarks = "Available"
            };

            // 3. Pass the object to DAL
            try
            {
                bool isSuccess = _repo.AddGuest(newGuest);
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