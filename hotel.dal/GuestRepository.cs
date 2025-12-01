using System;
using System.Collections.Generic;
using System.Linq; // Required for .ToList()

namespace Hotel.DAL
{
    public class GuestRepository
    {
        // 1. Get All Guests
        // REPLACED: "SELECT * FROM..." with Entity Framework
        public List<Guest> GetGuests()
        {
            using (var context = new HotelContext())
            {
                // This fetches all rows and automatically converts them to Guest objects
                return context.Guests.ToList();
            }
        }

        // 2. Add a Guest
        // REPLACED: "INSERT INTO..." with Entity Framework
        public bool AddGuest(Guest guest)
        {
            using (var context = new HotelContext())
            {
                try
                {
                    // Set default values if they are missing
                    // (EF doesn't always use the SQL defaults automatically)
                    if (string.IsNullOrEmpty(guest.Status)) guest.Status = "Active";
                    if (string.IsNullOrEmpty(guest.Remarks)) guest.Remarks = "Available";

                    // Add the object to the database context
                    context.Guests.Add(guest);

                    // Commit changes to the actual SQL Database
                    context.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    // Log error if needed
                    return false;
                }
            }
        }
    }
}