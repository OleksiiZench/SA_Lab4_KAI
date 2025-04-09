using FoodDelivery.DAL.Data;
using FoodDelivery.DAL.Entities;

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
            return _context.Dishes
                .Where(d => d.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}