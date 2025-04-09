using FoodDelivery.DAL.Data;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.BLL.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder()
        {
            var newOrder = new Order { OrderDate = DateTime.Now, OrderStatus = "Нове" };
            _context.Orders.Add(newOrder);
            _context.SaveChanges(); // Важливо зберегти зміни, щоб отримати Id замовлення
            return newOrder;
        }

        public void AddDishToOrder(Order order, Dish dish, int quantity)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                DishId = dish.Id,
                Quantity = quantity,
                Price = dish.Price // Зберігаємо ціну на момент замовлення
            };
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }

        public List<OrderItem> GetOrderItems(int orderId)
        {
            return _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Dish) // Включаємо інформацію про страву
                .ToList();
        }

        public decimal CalculateTotalOrderPrice(int orderId)
        {
            return _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Sum(oi => oi.Price * oi.Quantity);
        }
    }
}