using FoodDelivery.DAL.Data;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.BLL.Services
{
    public class DishService
    {
        private readonly AppDbContext _context;

        public DishService(AppDbContext context)
        {
            _context = context;
        }

        public List<Dish> GetAllDishes()
        {
            return _context.Dishes.ToList();
        }

        public Dish GetDishById(int id)
        {
            return _context.Dishes.FirstOrDefault(d => d.Id == id);
        }

        public List<Dish> SearchDishesByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<Dish>();

            // Спочатку отримуємо всі страви з бази даних
            var allDishes = _context.Dishes.ToList();

            // Потім фільтруємо їх у пам'яті
            return allDishes.Where(d =>
                d.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
    }
}