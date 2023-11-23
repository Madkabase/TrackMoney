Money Tracking App
The Money Tracking App is a simple console application written in C# that allows you to track your expenses and incomes. It provides functionalities to add new items, edit existing items, and view items based on categories (All, Expenses, Incomes). The application also calculates your account balance based on the recorded transactions.

How to Use
Run the application and follow the instructions displayed on the console.

Upon launching the app, it will load any previously recorded items from the items.txt file, if it exists.

The main menu will be displayed with the following options:

Show items (All/Expenses/Incomes): View the recorded items based on the selected category.
Add New Expense/Income: Add a new item to the tracking list.
Edit Item: Edit the details of an existing item.
Save and Quit: Save the recorded items to the items.txt file and exit the application.
When choosing the Show items option, you can further specify whether you want to view all items, expenses only, or incomes only.

When selecting the Add New Expense/Income option, provide the title, amount, and month for the new item. You'll also need to indicate whether it's an expense or income by entering 'E' or 'I' accordingly.

If you choose the Edit Item option, you'll be prompted to enter the index of the item you want to edit. Then, you can modify the title, amount, and month of the selected item.

The application automatically calculates your account balance based on the recorded expenses and incomes.

To exit the application and save your changes, select the Save and Quit option.

File Management
The recorded items are stored in the items.txt file, which is created in the same directory as the application. Each line in the file represents a single item and consists of the following fields separated by semicolons: Title;Amount;Month;Type
Title: The title or description of the item.
Amount: The monetary amount associated with the item.
Month: The month in which the item was recorded.
Type: The type of item, either 'Expense' or 'Income'.

Dependencies
The Money Tracking App doesn't have any external dependencies. It's written using the .NET framework and utilizes standard C# libraries for basic console input/output and file operations.
