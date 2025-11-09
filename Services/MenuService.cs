using Restaurant_site.Models;

namespace Restaurant_site.Services;

public class MenuService
{
    private readonly List<MenuItem> _menuItems;

    public MenuService()
    {
        _menuItems = new List<MenuItem>
        {
            // Закуски
            new MenuItem
            {
                Id = 1,
                Name = "Брускетта с томатами",
                Description = "Хрустящий хлеб с сочными помидорами, базиликом и оливковым маслом",
                Category = MenuCategory.Appetizers,
                Price = 450,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 2,
                Name = "Салат Цезарь",
                Description = "Классический салат с курицей, пармезаном, сухариками и соусом Цезарь",
                Category = MenuCategory.Appetizers,
                Price = 550,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 3,
                Name = "Капрезе",
                Description = "Моцарелла, свежие томаты, базилик и бальзамический уксус",
                Category = MenuCategory.Appetizers,
                Price = 480,
                ImageUrl = ""
            },

            // Основные блюда
            new MenuItem
            {
                Id = 4,
                Name = "Паста Карбонара",
                Description = "Спагетти с беконом, яичным желтком, пармезаном и черным перцем",
                Category = MenuCategory.Main,
                Price = 720,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 5,
                Name = "Стейк Рибай",
                Description = "Сочный стейк из мраморной говядины 250г с гарниром на выбор",
                Category = MenuCategory.Main,
                Price = 1850,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 6,
                Name = "Лосось на гриле",
                Description = "Филе лосося с овощами гриль и лимонным соусом",
                Category = MenuCategory.Main,
                Price = 1200,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 7,
                Name = "Ризотто с белыми грибами",
                Description = "Кремовое ризотто с белыми грибами, пармезаном и трюфельным маслом",
                Category = MenuCategory.Main,
                Price = 890,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 8,
                Name = "Равиоли со шпинатом и рикоттой",
                Description = "Домашние равиоли с начинкой из шпината и рикотты в сливочном соусе",
                Category = MenuCategory.Main,
                Price = 680,
                ImageUrl = ""
            },

            // Десерты
            new MenuItem
            {
                Id = 9,
                Name = "Тирамису",
                Description = "Классический итальянский десерт с маскарпоне и кофе",
                Category = MenuCategory.Desserts,
                Price = 420,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 10,
                Name = "Панна-котта",
                Description = "Нежный молочный десерт с ягодным соусом",
                Category = MenuCategory.Desserts,
                Price = 380,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 11,
                Name = "Чизкейк Нью-Йорк",
                Description = "Классический чизкейк на песочной основе с ягодами",
                Category = MenuCategory.Desserts,
                Price = 450,
                ImageUrl = ""
            },

            // Напитки
            new MenuItem
            {
                Id = 12,
                Name = "Эспрессо",
                Description = "Классический итальянский эспрессо",
                Category = MenuCategory.Drinks,
                Price = 150,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 13,
                Name = "Капучино",
                Description = "Эспрессо с взбитым молоком",
                Category = MenuCategory.Drinks,
                Price = 220,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 14,
                Name = "Домашний лимонад",
                Description = "Освежающий лимонад с мятой",
                Category = MenuCategory.Drinks,
                Price = 280,
                ImageUrl = ""
            },
            new MenuItem
            {
                Id = 15,
                Name = "Апельсиновый фреш",
                Description = "Свежевыжатый апельсиновый сок",
                Category = MenuCategory.Drinks,
                Price = 320,
                ImageUrl = ""
            }
        };
    }

    public List<MenuItem> GetAllMenuItems()
    {
        return _menuItems;
    }

    public List<MenuItem> GetMenuItemsByCategory(MenuCategory category)
    {
        if (category == MenuCategory.All)
        {
            return _menuItems;
        }

        return _menuItems.Where(item => item.Category == category).ToList();
    }

    public MenuItem? GetMenuItemById(int id)
    {
        return _menuItems.FirstOrDefault(item => item.Id == id);
    }
}
