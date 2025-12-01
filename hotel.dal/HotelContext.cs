using System.Data.Entity;

namespace Hotel.DAL
{
    public class HotelContext : DbContext
    {
        // "name=HotelDB" tells it to look for a connection string named "HotelDB" in Web.config
        public HotelContext() : base("name=HotelDB")
        {
        }

        // This creates the link: C# Code <---> SQL Table
        public DbSet<Guest> Guests { get; set; }
    }
}