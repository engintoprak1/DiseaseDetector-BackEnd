using Core.Entities;
using Core.Utilities.Security.JWT;

namespace Entities.Dtos.User
{
    public class UserLoginResultDto : IDto
    {
        public AccessToken AccessToken { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gsm { get; set; }
    }
}
