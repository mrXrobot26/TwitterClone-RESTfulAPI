using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.AppUsers
{
    public class ApplicationUserToAddUserDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public DateOnly Birthday { get; set; }
        public DateTime JoinTime { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        public string Password { get; set; }

        public string phoneNumber { get; set; }

    }
}
