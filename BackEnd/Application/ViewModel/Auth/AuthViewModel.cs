using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel.Auth
{
    public class AuthViewModel
    {
        public string token { get; set; }
        public UserDto user { get; set; }
    }
}
