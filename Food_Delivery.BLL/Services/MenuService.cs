using FoodDelivery.DAL.Data;
using FoodDelivery.DAL.Entities;

namespace FoodDelivery.BLL.Services
{
    public class MenuService
    {
        private readonly AppDbContext _context;

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        public List<Dish> GetMenuForDay(int dayOfWeekId)
        {
            return _context.Menus
                .Where(m => m.DayOfWeekId == dayOfWeekId)
                .SelectMany(m => m.Dishes)
                .ToList();
        }

        public List<Dish> GetDishesByCategory(int categoryId)
        {
            return _context.Dishes
                .Where(d => d.CategoryId == categoryId)
                .ToList();
        }
    }
}