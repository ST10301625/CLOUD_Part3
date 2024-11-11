using Microsoft.EntityFrameworkCore;

namespace CloudPart3.Repositories;

public interface IStyleRepository
{
    Task AddStyle(Style style); 
    Task UpdateStyle(Style style);
    Task<Style?> GetStyleById(int id);
    Task DeleteStyle(Style style);
    Task<IEnumerable<Style>> GetStyles();
}
public class StyleRepository : IStyleRepository
{
    private readonly ApplicationDbContext _context;
    public StyleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddStyle(Style style)
    {
        _context.Styles.Add(style);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateStyle(Style style)
    {
        _context.Styles.Update(style);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStyle(Style style)
    {
        _context.Styles.Remove(style);
        await _context.SaveChangesAsync();
    }

    public async Task<Style?> GetStyleById(int id)
    {
        return await _context.Styles.FindAsync(id);
    }

    public async Task<IEnumerable<Style>> GetStyles()
    {
        return await _context.Styles.ToListAsync();
    }

    
}
