using Manager.Infra.Interfaces;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositiries{
    public class UserRepository : BaseRepository<User>, IUserRepository{
        
        private readonly ManagerContext _context;

        public UserRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                            .Where
                            (
                                x => x.Email.ToLower() == email.ToLower()
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<User> Auth(string email, string password)
        {
            var user = await _context.Users
                            .Where(x => 
                                x.Email.ToLower() == email.ToLower() &&
                                x.Password.ToLower() == password.ToLower()
                            )
                            .AsNoTracking()
                            .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<List<User>> SearchByEmail(string email)
        {
            var allUsers = await _context.Users
                                    .Where(
                                        x => x.Email.ToLower().Contains(email.ToLower())
                                    )
                                    .AsNoTracking()
                                    .ToListAsync();

            return allUsers;
        }

        public async Task<List<User>> SearchByName(string name)
        {
           var allUsers = await _context.Users
                                .Where(
                                    x => x.Name.ToLower().Contains(name.ToLower())
                                )
                                .AsNoTracking()
                                .ToListAsync();

            return allUsers;
        }
        
    }

}