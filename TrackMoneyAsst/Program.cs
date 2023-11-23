using System;
using System.Collections.Generic;
using System.IO;

namespace MoneyTrackingApp
{
    class Program
    {
        static List<Item> items = new List<Item>();
        static string filePath = "items.txt";

        static void Main(string[] args)
        {
            LoadItemsFromFile();

            Console.WriteLine("Welcome to TrackMoney");
            //Console.WriteLine($"You have currently ({GetAccountBalance()} kr on your account.");

            bool quit = false;
            while (!quit)
            {
                Console.WriteLine($"You have currently {GetAccountBalance()} kr on your account.");
                Console.WriteLine("Pick an option:");
                Console.WriteLine("(1) Show items (All/Expense(s)/Income(s))");
                Console.WriteLine("(2) Add New Expense/Income");
                Console.WriteLine("(3) Edit Item (edit, remove)");
                Console.WriteLine("(4) Save and Quit.");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        ShowItems();
                        break;
                    case "2":
                        AddItem();
                        break;
                    case "3":
                        EditItem();
                        break;
                    case "4":
                        SaveItemsToFile();
                        quit = true;
                        break;
                    
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ShowItems()
        {
            Console.WriteLine("Show items:");
            Console.WriteLine("(1) All");
            Console.WriteLine("(2) Expenses");
            Console.WriteLine("(3) Incomes");

            string input = Console.ReadLine();
            Console.WriteLine();

            List<Item> filteredItems = new List<Item>();
            switch (input)
            {
                case "1":
                    filteredItems = items;
                    break;
                case "2":
                    filteredItems = GetExpenses();
                    break;
                case "3":
                    filteredItems = GetIncomes();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    return;
            }

            if (filteredItems.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            Console.WriteLine("Items:");
            foreach (var item in filteredItems)
            {
                Console.WriteLine($"{item.Title} - {item.Amount} kr - {item.Month}");
            }
        }

        static void RemoveItem()
        {
            Console.WriteLine("Enter the title of the item you want to remove:");
            string title = Console.ReadLine();

            var item = items.FirstOrDefault(i => i.Title == title);
            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            items.Remove(item);
            Console.WriteLine("Item removed.");
        }
        static void AddItem()
        {
            Console.WriteLine("Add New Expense/Income:");
            Console.WriteLine("Enter title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter amount:");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount. Please enter a number.");
                return;
            }

            Console.WriteLine("Enter month (yyyy-MM):");
            DateTime month;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out month))
            {
                Console.WriteLine("Invalid month. Please enter in the format yyyy-MM.");
                return;
            }

            Console.WriteLine("Is it an expense or income? (E/I)");
            string typeInput = Console.ReadLine().ToUpper();
            ItemType type = (typeInput == "E") ? ItemType.Expense : ItemType.Income;

            items.Add(new Item(title, amount, month, type));
            Console.WriteLine("Item added successfully.");
        }

        static void EditItem()
        {
            Console.WriteLine("Edit Item:");

            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            Console.WriteLine("Enter the index of the item to edit:");
            int index = int.Parse(Console.ReadLine());

            if (index < 0 || index >= items.Count)
            {
                Console.WriteLine("Invalid index. Please try again.");
                return;
            }

            Item item = items[index];

            Console.WriteLine($"Item: {item.Title} - {item.Amount} kr - {item.Month}");
            Console.WriteLine("Enter new title (or press Enter to keep the current value):");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle))
            {
                item.Title = newTitle;
            }

            Console.WriteLine("Enter new amount (or press Enter to keep the current value):");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount. Please enter a number.");
                return;
            }
            item.Amount = amount;

            Console.WriteLine("Enter new month (yyyy-MM):");
            DateTime month;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out month))
            {
                Console.WriteLine("Invalid month. Please enter in the format yyyy-MM.");
                return;
            }
            item.Month = month;

            Console.WriteLine("Item edited successfully.");
        }

        static void LoadItemsFromFile()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(';');
                    string title = parts[0];
                    decimal amount = decimal.Parse(parts[1]);
                    DateTime month = DateTime.Parse(parts[2]);
                    ItemType type = (ItemType)Enum.Parse(typeof(ItemType), parts[3]);

                    items.Add(new Item(title, amount, month, type));
                }
            }
        }

        static void SaveItemsToFile()
        {
            List<string> lines = new List<string>();

            foreach (var item in items)
            {
                string line = $"{item.Title};{item.Amount};{item.Month};{item.Type}";
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }

        static decimal GetAccountBalance()
        {
            decimal balance = 0;

            foreach (var item in items)
            {
                if (item.Type == ItemType.Expense)
                {
                    balance -= item.Amount;
                }
                else if (item.Type == ItemType.Income)
                {
                    balance += item.Amount;
                }
            }

            return balance;
        }

        static List<Item> GetExpenses()
        {
            List<Item> expenses = new List<Item>();

            foreach (var item in items)
            {
                if (item.Type == ItemType.Expense)
                {
                    expenses.Add(item);
                }
            }

            return expenses;
        }

        static List<Item> GetIncomes()
        {
            List<Item> incomes = new List<Item>();

            foreach (var item in items)
            {
                if (item.Type == ItemType.Income)
                {
                    incomes.Add(item);
                }
            }

            return incomes;
        }
    }

    enum ItemType
    {
        Expense,
        Income
    }

    class Item
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Month { get; set; }
        public ItemType Type { get; set; }

        public Item(string title, decimal amount, DateTime month, ItemType type)
        {
            Title = title;
            Amount = amount;
            Month = month;
            Type = type;
        }
    }
}