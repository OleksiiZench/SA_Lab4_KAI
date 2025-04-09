using Microsoft.EntityFrameworkCore;
using FoodDelivery.DAL.Entities;

namespace FoodDelivery.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<FoodDelivery.DAL.Entities.DayOfWeek> DaysOfWeek { get; set; }
        public DbSet<MenuDish> MenuDishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FoodDelivery.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuDish>()
                .HasKey(md => new { md.MenuId, md.DishId });

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            // Додавання днів тижня
            modelBuilder.Entity<Entities.DayOfWeek>().HasData(
                new Entities.DayOfWeek { Id = 1, Name = "Понеділок" },
                new Entities.DayOfWeek { Id = 2, Name = "Вівторок" },
                new Entities.DayOfWeek { Id = 3, Name = "Середа" },
                new Entities.DayOfWeek { Id = 4, Name = "Четвер" },
                new Entities.DayOfWeek { Id = 5, Name = "П'ятниця" },
                new Entities.DayOfWeek { Id = 6, Name = "Субота" },
                new Entities.DayOfWeek { Id = 7, Name = "Неділя" }
            );

            // Додавання категорій страв
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Салати" },
                new Category { Id = 2, Name = "Основні страви" },
                new Category { Id = 3, Name = "Десерти" },
                new Category { Id = 4, Name = "Напої" }
            );

            // Додавання страв
            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Салат Цезар", Description = "Класичний салат з куркою", Price = 120.00m, CategoryId = 1 },
                new Dish { Id = 2, Name = "Борщ", Description = "Традиційний український борщ", Price = 90.50m, CategoryId = 2 },
                new Dish { Id = 3, Name = "Вареники з картоплею", Description = "Вареники з картоплею та цибулею", Price = 75.00m, CategoryId = 2 },
                new Dish { Id = 4, Name = "Тірамісу", Description = "Італійський десерт", Price = 80.00m, CategoryId = 3 },
                new Dish { Id = 5, Name = "Лимонад", Description = "Освіжаючий лимонад", Price = 35.00m, CategoryId = 4 },
                new Dish { Id = 6, Name = "Салат Грецький", Description = "Салат зі свіжих овочів та фети", Price = 110.00m, CategoryId = 1 },
                new Dish { Id = 7, Name = "Карбонара", Description = "Паста з беконом та яйцем", Price = 135.00m, CategoryId = 2 }
            );

            // Додавання меню на понеділок (DayOfWeekId = 1)
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, DayOfWeekId = 1 }
            );

            // Додавання страв до меню на понеділок (через MenuDish)
            modelBuilder.Entity<MenuDish>().HasData(
                new MenuDish { MenuId = 1, DishId = 1 }, // Салат Цезар
                new MenuDish { MenuId = 1, DishId = 2 }, // Борщ
                new MenuDish { MenuId = 1, DishId = 4 }, // Тірамісу
                new MenuDish { MenuId = 1, DishId = 5 }  // Лимонад
            );

            // Додавання меню на вівторок (DayOfWeekId = 2)
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 2, DayOfWeekId = 2 }
            );

            // Додавання страв до меню на вівторок
            modelBuilder.Entity<MenuDish>().HasData(
                new MenuDish { MenuId = 2, DishId = 6 }, // Салат Грецький
                new MenuDish { MenuId = 2, DishId = 3 }, // Вареники з картоплею
                new MenuDish { MenuId = 2, DishId = 4 }, // Тірамісу (повторення страви)
                new MenuDish { MenuId = 2, DishId = 5 }  // Лимонад (повторення страви)
            );
        }
    }
}