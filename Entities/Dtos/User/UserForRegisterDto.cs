using Core.Entities;

namespace Entities.Dtos.User
{
    public class UserForRegisterDto : IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentificationNumber { get; set; }
        public string Gsm { get; set; }
        public string Password { get; set; }
    }
}
