using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL
{
    // This tells EF to look for a table named "tblGuest"
    [Table("tblGuest")]
    public class Guest
    {
        [Key] // Marks this as the Primary Key (Auto-Increment)
        public int ID { get; set; }

        public string GuestFName { get; set; }
        public string GuestMName { get; set; }
        public string GuestLName { get; set; }
        public string GuestAddress { get; set; }
        public string GuestContactNumber { get; set; }
        public string GuestGender { get; set; }
        public string GuestEmail { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}