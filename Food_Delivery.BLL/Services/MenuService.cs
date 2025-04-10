using FoodDelivery.DAL.Data;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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
            // Використовуємо Include та ThenInclude для явного завантаження зв'язаних об'єктів
            return _context.Menus
                .Where(m => m.DayOfWeekId == dayOfWeekId)
                .Include(m => m.MenuDishes)
                .ThenInclude(md => md.Dish)
                .SelectMany(m => m.MenuDishes.Select(md => md.Dish))
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