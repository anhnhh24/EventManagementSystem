using EventController.Models.DAO.Interfaces;
using EventController.Models.Data.DBcontext;


public class CategoryDAO:ICategoryDAO
{
    private readonly DBContext _context;

    public CategoryDAO(DBContext context)
    {
        _context = context;
    }

    public List<Category> GetAllCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.Categories.Find(id);
    }

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }

    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.CategoryID == id);
    }

    public List<Category> SearchCategoriesByName(string keyword)
    {
        return _context.Categories
            .Where(c => c.CategoryName.Contains(keyword))
            .ToList();
    }
}
