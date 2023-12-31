﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public class UserPostDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
