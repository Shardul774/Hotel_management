using Hotel.DAL;
using static Hotel.DAL.GuestRepository;

namespace Hotel.BLL
{
    public class UserService
    {
        private UserRepository _repo = new UserRepository();

        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            return _repo.ValidateUser(username, password);
        }
    }
}