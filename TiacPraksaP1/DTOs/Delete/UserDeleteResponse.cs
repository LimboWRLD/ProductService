﻿using TiacPraksaP1.Models;

namespace TiacPraksaP1.DTOs.Delete
{
    public class UserDeleteResponse
    {
        public int Id { get; set; } 
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }


    }
}
