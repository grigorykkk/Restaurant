namespace Restaurant_site.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // Независимая инициализация типов столиков
            if (!context.TableTypes.Any())
            {
                var tableTypes = new[]
                {
                    new TableType { IdTableType = 1, Title = "У окна", Description = "Столики у окна" },
                    new TableType { IdTableType = 2, Title = "У прохода", Description = "Столики у прохода" },
                    new TableType { IdTableType = 3, Title = "В глубине зала", Description = "Столики в глубине зала" }
                };
                context.TableTypes.AddRange(tableTypes);
                await context.SaveChangesAsync();
            }

            // Независимая инициализация типов блюд
            if (!context.DishTypes.Any())
            {
                var dishTypes = new[]
                {
                    new DishType { IdDishType = 1, Title = "Салаты" },
                    new DishType { IdDishType = 2, Title = "Основные блюда" },
                    new DishType { IdDishType = 3, Title = "Десерты" },
                    new DishType { IdDishType = 4, Title = "Напитки" }
                };
                context.DishTypes.AddRange(dishTypes);
                await context.SaveChangesAsync();
            }

            // Независимая инициализация столиков
            if (!context.Tables.Any())
            {
                var tables = new[]
                {
                    new Table { IdType = 1, Title = "У окна", SeatsCount = 2 },
                    new Table { IdType = 1, Title = "В глубине зала", SeatsCount = 2 },
                    new Table { IdType = 2, Title = "У окна", SeatsCount = 4 },
                    new Table { IdType = 2, Title = "У прохода", SeatsCount = 4 },
                    new Table { IdType = 3, Title = "В глубине зала", SeatsCount = 6 },
                    new Table { IdType = 3, Title = "У прохода", SeatsCount = 6 }
                };
                context.Tables.AddRange(tables);
                await context.SaveChangesAsync();
            }

            // Независимая инициализация блюд
            if (!context.Dishes.Any())
            {
                await SeedDishes(context);
            }
        }

        private static async Task SeedDishes(ApplicationDbContext context)
        {

            var dishes = new[]
            {
                // Салаты и закуски
                new Dish
                {
                    IdDishType = 1,
                    Title = "Брускетта с томатами",
                    Description = "Хрустящий хлеб с сочными помидорами, базиликом и оливковым маслом",
                    Cost = 450,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 1,
                    Title = "Салат Цезарь",
                    Description = "Классический салат с курицей, пармезаном, сухариками и соусом Цезарь",
                    Cost = 550,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 1,
                    Title = "Капрезе",
                    Description = "Моцарелла, свежие томаты, базилик и бальзамический уксус",
                    Cost = 480,
                    Image = ""
                },

                // Основные блюда
                new Dish
                {
                    IdDishType = 2,
                    Title = "Паста Карбонара",
                    Description = "Спагетти с беконом, яичным желтком, пармезаном и черным перцем",
                    Cost = 720,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 2,
                    Title = "Стейк Рибай",
                    Description = "Сочный стейк из мраморной говядины 250г с гарниром на выбор",
                    Cost = 1850,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 2,
                    Title = "Лосось на гриле",
                    Description = "Филе лосося с овощами гриль и лимонным соусом",
                    Cost = 1200,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 2,
                    Title = "Ризотто с белыми грибами",
                    Description = "Кремовое ризотто с белыми грибами, пармезаном и трюфельным маслом",
                    Cost = 890,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 2,
                    Title = "Равиоли со шпинатом и рикоттой",
                    Description = "Домашние равиоли с начинкой из шпината и рикотты в сливочном соусе",
                    Cost = 680,
                    Image = ""
                },

                // Десерты
                new Dish
                {
                    IdDishType = 3,
                    Title = "Тирамису",
                    Description = "Классический итальянский десерт с маскарпоне и кофе",
                    Cost = 420,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 3,
                    Title = "Панна-котта",
                    Description = "Нежный молочный десерт с ягодным соусом",
                    Cost = 380,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 3,
                    Title = "Чизкейк Нью-Йорк",
                    Description = "Классический чизкейк на песочной основе с ягодами",
                    Cost = 450,
                    Image = ""
                },

                // Напитки
                new Dish
                {
                    IdDishType = 4,
                    Title = "Эспрессо",
                    Description = "Классический итальянский эспрессо",
                    Cost = 150,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 4,
                    Title = "Капучино",
                    Description = "Эспрессо с взбитым молоком",
                    Cost = 220,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 4,
                    Title = "Домашний лимонад",
                    Description = "Освежающий лимонад с мятой",
                    Cost = 280,
                    Image = ""
                },
                new Dish
                {
                    IdDishType = 4,
                    Title = "Апельсиновый фреш",
                    Description = "Свежевыжатый апельсиновый сок",
                    Cost = 320,
                    Image = ""
                }
            };
            context.Dishes.AddRange(dishes);

            await context.SaveChangesAsync();
        }
    }
}