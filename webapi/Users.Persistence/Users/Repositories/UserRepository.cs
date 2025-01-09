using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Users.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.Queries;
using Users.Persistence.Repositories.Interfaces;

namespace Users.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = command.Name,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Birthday = command.Birthday,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {command.Id} not found.");

            if (!string.IsNullOrEmpty(command.Name))
                user.Name = command.Name;

            if (!string.IsNullOrEmpty(command.Email))
                user.Email = command.Email;

            if (!string.IsNullOrEmpty(command.PhoneNumber))
                user.PhoneNumber = command.PhoneNumber;

            if (command.Birthday.HasValue)
                user.Birthday = command.Birthday.Value;

            user.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.Birthday,
                CreatedAt = user.CreatedAt,
                ModifiedAt = user.ModifiedAt
            };
        }

        public async Task<PaginatedList<UserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var totalItems = await _context.Users.CountAsync(cancellationToken);

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Birthday = u.Birthday,
                CreatedAt = u.CreatedAt,
                ModifiedAt = u.ModifiedAt
            }).ToList();

            return new PaginatedList<UserDto>(userDtos, totalItems, page, pageSize);
        }

    }
}
