using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private ChatProjectApplicationDBContext _ctx;

        public AuthRepository(ChatProjectApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public bool ValidateByLogin(string login)
        {

            try
            {
                var user = _ctx.Users.Where(c => c.Login == login).FirstOrDefault();
                return user == null ? false : true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public IQueryable<User> GetUsers()
        {
            return _ctx.Users;
        }
    }
}
