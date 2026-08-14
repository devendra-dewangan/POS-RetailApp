namespace POS.Repos.Person;

using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Person;

public class PersonRepo : IPersonRepo
{
    private readonly AppDbContext _context;

    public PersonRepo(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Person value)
    {
        await _context.Persons.AddAsync(value);
    }

    public Task DeleteAsync(Person value)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Person>?> GetAllAsync()
    {
        return await _context.Persons.ToArrayAsync();
    }

    public async Task<Person?> GetByIDAsync(int id)
    {
        return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
    }
    public Task UpdateAsync(Person value)
    {
        throw new NotImplementedException();
    }
}
