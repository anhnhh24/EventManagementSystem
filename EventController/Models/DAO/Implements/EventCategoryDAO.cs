
using EventController.Models.Data.DBcontext;
using Microsoft.EntityFrameworkCore;


public class EventCategoryDAO
{
    private readonly DBContext _context;

    public EventCategoryDAO(DBContext context)
    {
        _context = context;
    }

    public List<EventCategory> GetAllCategories()
    {
        return _context.EventCategories
                       .Include(c => c.Events)
                       .ToList();
    }

    public EventCategory GetCategoryById(int id)
    {
        return _context.EventCategories
                       .Include(c => c.Events)
                       .FirstOrDefault(c => c.CategoryID == id);
    }

    public void AddCategory(EventCategory category)
    {
        _context.EventCategories.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(EventCategory category)
    {
        _context.EventCategories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = _context.EventCategories.Find(id);
        if (category != null)
        {
            _context.EventCategories.Remove(category);
            _context.SaveChanges();
        }
    }

    public bool CategoryExists(int id)
    {
        return _context.EventCategories.Any(c => c.CategoryID == id);
    }

    public List<EventCategory> SearchCategoriesByName(string keyword)
    {
        return _context.EventCategories
            .Where(c => c.CategoryName.Contains(keyword))
            .ToList();
    }
}
