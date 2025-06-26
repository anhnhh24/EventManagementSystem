namespace EventController.Models.DAO.Interfaces
{
    public interface ICategoryDAO
    {
        public List<Category> GetAllCategories();

        public Category GetCategoryById(int id);

        public void AddCategory(Category category);

        public void UpdateCategory(Category category);

        public void DeleteCategory(int id);

        public bool CategoryExists(int id);

        public List<Category> SearchCategoriesByName(string keyword);
    }
}
