using Microsoft.EntityFrameworkCore;
using Restaurant_site.Models;

namespace Restaurant_site.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.DishTypes.Any())
            {
                await SeedData(context);
            }
        }

        private static async Task SeedData(ApplicationDbContext context)
        {
            var tableTypes = new[]
            {
                new TableType { Title = "Стандартный", Description = "Обычный стол для 2-4 человек" },
                new TableType { Title = "VIP", Description = "Стол в отдельной зоне с улучшенным обслуживанием" },
                new TableType { Title = "Банкетный", Description = "Большой стол для компаний от 6 человек" }
            };
            context.TableTypes.AddRange(tableTypes);

            var dishTypes = new[]
            {
                new DishType { Title = "Супы" },
                new DishType { Title = "Салаты" },
                new DishType { Title = "Основные блюда" },
                new DishType { Title = "Десерты" },
                new DishType { Title = "Напитки" }
            };
            context.DishTypes.AddRange(dishTypes);

            await context.SaveChangesAsync();

            var tables = new[]
            {
                new Table { IdType = 1, Title = "Стол 1", SeatsCount = 4 },
                new Table { IdType = 1, Title = "Стол 2", SeatsCount = 2 },
                new Table { IdType = 2, Title = "VIP 1", SeatsCount = 4 },
                new Table { IdType = 3, Title = "Банкетный 1", SeatsCount = 8 }
            };
            context.Tables.AddRange(tables);

            var dishes = new[]
            {
                new Dish {
                    Title = "Борщ",
                    Image = "/images/borsh.jpg",
                    Description = "Традиционный украинский борщ",
                    Cost = 280.00m,
                    IdDishType = 1
                },
                new Dish {
                    Title = "Цезарь с курицей",
                    Image = "/images/caesar.jpg",
                    Description = "Салат с курицей, гренками и соусом цезарь",
                    Cost = 320.00m,
                    IdDishType = 2
                },
                new Dish {
                    Title = "Стейк Рибай",
                    Image = "/images/steak.jpg",
                    Description = "Сочный стейк с овощами на гриле",
                    Cost = 890.00m,
                    IdDishType = 3
                },
                new Dish {
                    Title = "Тирамису",
                    Image = "/images/tiramisu.jpg",
                    Description = "Итальянский десерт с кофе и маскарпоне",
                    Cost = 240.00m,
                    IdDishType = 4
                },
                new Dish {
                    Title = "Кока-Кола",
                    Image = "/images/cola.jpg",
                    Description = "Освежающий газированный напиток",
                    Cost = 120.00m,
                    IdDishType = 5
                }
            };
            context.Dishes.AddRange(dishes);

            await context.SaveChangesAsync();
        }
    }
}