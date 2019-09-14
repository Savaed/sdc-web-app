using System;

namespace SDCWebApp.Models.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime LoggedOn { get; set; }
    }
}
