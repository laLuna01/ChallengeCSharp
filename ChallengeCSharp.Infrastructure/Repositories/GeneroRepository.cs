using Oracle.ManagedDataAccess.Client;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Domain.Interfaces;
using System.Data;
using ChallengeCSharp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCSharp.Infrastructure.Repositories;

public class GeneroRepository : IGeneroRepository
{
    private readonly ApplicationDbContext _context;

    public GeneroRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genero>> GetAllAsync() =>
        await _context.Generos.ToListAsync();

    public async Task<Genero?> GetByIdAsync(int id) =>
        await _context.Generos.FindAsync(id);

    public async Task AddAsync(Genero genero)
    {
        _context.Generos.Add(genero);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Genero genero)
    {
        _context.Generos.Update(genero);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genero = await GetByIdAsync(id);
        if (genero is not null)
        {
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
        }
    }
}