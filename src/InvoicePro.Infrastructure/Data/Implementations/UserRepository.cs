using InvoicePro.Domain.Entities;
using InvoicePro.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoicePro.Infrastructure.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users
          .FirstOrDefaultAsync(u => u.Email == email.ToLower());
    }


    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await  _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
}