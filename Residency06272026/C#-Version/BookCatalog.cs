using System;
using System.Collections.Generic;
using System.Linq;

namespace BookCatalogSystem
{

    // Book class — represents a single book in the catalog
    class Book
    {
        public string Title  { get; set; }
        public string Author { get; set; }
        public string Genre  { get; set; }
        public int    Year   { get; set; }

        public Book(string title, string author, string genre, int year)
        {
            Title  = title;
            Author = author;
            Genre  = genre;
            Year   = year;
        }

        public override string ToString()
        {
            return $"  \"{Title}\" by {Author} [{Genre}, {Year}]";
        }
    }
    //  CatalogManager — manages the collection of books
    class CatalogManager
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            //Prevent exact duplicates (same title + author)
            bool exists = _books.Any(b =>
                b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(book.Author, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                Console.WriteLine($"\n  [!] \"{book.Title}\" by {book.Author} already exists in the catalog.");
                return;
            }

            _books.Add(book);
            Console.WriteLine($"\n  [+] Added: {book}");
        }

        public void RemoveBook(string title)
        {
            //LINQ - find first match (case-insensitive)
            Book found = _books.FirstOrDefault(b =>
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (found == null)
            {
                Console.WriteLine($"\n  [!] No book titled \"{title}\" found.");
                return;
            }

            _books.Remove(found);
            Console.WriteLine($"\n  [-] Removed: {found}");
        }

        //Search
        public void SearchByTitle(string query)
        {
            //LINQ: filter where title contains the query string
            var results = _books
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            PrintResults($"Search by title \"{query}\"", results);
        }

        public void SearchByAuthor(string query)
        {
            var results = _books
                .Where(b => b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            PrintResults($"Search by author \"{query}\"", results);
        }

        public void SearchByGenre(string query)
        {
            var results = _books
                .Where(b => b.Genre.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            PrintResults($"Search by genre \"{query}\"", results);
        }

        //Reporting
        public void DisplayByGenre()
        {
            if (_books.Count == 0) { PrintEmpty(); return; }

            Console.WriteLine("\n  ── Books grouped by Genre ──────────────────");

            //LINQ GroupBy returns groups keyed by genre
            var grouped = _books
                .OrderBy(b => b.Genre)
                .GroupBy(b => b.Genre);

            foreach (var group in grouped)
            {
                Console.WriteLine($"\n  [{group.Key}]");
                foreach (var book in group.OrderBy(b => b.Title))
                    Console.WriteLine(book);
            }
        }

        public void DisplayByAuthor()
        {
            if (_books.Count == 0) { PrintEmpty(); return; }

            Console.WriteLine("\n  ── Books grouped by Author ─────────────────");

            var grouped = _books
                .OrderBy(b => b.Author)
                .GroupBy(b => b.Author);

            foreach (var group in grouped)
            {
                Console.WriteLine($"\n  [{group.Key}]");
                foreach (var book in group.OrderBy(b => b.Year))
                    Console.WriteLine(book);
            }
        }

        public void DisplayAll()
        {
            if (_books.Count == 0) { PrintEmpty(); return; }

            Console.WriteLine($"\n  ── All Books ({_books.Count} total) ──────────────────");
            foreach (var book in _books.OrderBy(b => b.Title))
                Console.WriteLine(book);
        }

        //Helpers
        private void PrintResults(string header, List<Book> results)
        {
            Console.WriteLine($"\n  ── {header} ──");
            if (results.Count == 0)
                Console.WriteLine("  No matching books found.");
            else
                results.ForEach(b => Console.WriteLine(b));
        }

        private void PrintEmpty()
        {
            Console.WriteLine("\n  The catalog is empty.");
        }

        public int Count => _books.Count;
    }
    //  Program — text-based menu UI
    class Program
    {
        static CatalogManager catalog = new CatalogManager();

        static void Main(string[] args)
        {
            SeedCatalog();   //pre-load a few sample books
            RunMenu();
        }

        //Seed data
        static void SeedCatalog()
        {
            catalog.AddBook(new Book("The Hobbit",                  "J.R.R. Tolkien",   "Fantasy",      1937));
            catalog.AddBook(new Book("1984",                        "George Orwell",     "Dystopian",    1949));
            catalog.AddBook(new Book("To Kill a Mockingbird",       "Harper Lee",        "Fiction",      1960));
            catalog.AddBook(new Book("Dune",                        "Frank Herbert",     "Sci-Fi",       1965));
            catalog.AddBook(new Book("The Name of the Wind",        "Patrick Rothfuss",  "Fantasy",      2007));
            catalog.AddBook(new Book("Brave New World",             "Aldous Huxley",     "Dystopian",    1932));
            catalog.AddBook(new Book("Foundation",                  "Isaac Asimov",      "Sci-Fi",       1951));
            catalog.AddBook(new Book("The Great Gatsby",            "F. Scott Fitzgerald","Fiction",     1925));
        }

        //Main menu loop
        static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("\n╔══════════════════════════════════════╗");
                Console.WriteLine("║       Book Cataloging System (C#)    ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║  1. Add a book                       ║");
                Console.WriteLine("║  2. Remove a book                    ║");
                Console.WriteLine("║  3. Search by title                  ║");
                Console.WriteLine("║  4. Search by author                 ║");
                Console.WriteLine("║  5. Search by genre                  ║");
                Console.WriteLine("║  6. Display books by genre           ║");
                Console.WriteLine("║  7. Display books by author          ║");
                Console.WriteLine("║  8. Show all books                   ║");
                Console.WriteLine("║  9. Exit                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("  Choose an option: ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": AddBookMenu();    break;
                    case "2": RemoveBookMenu(); break;
                    case "3":
                        Console.Write("\n  Enter title to search: ");
                        catalog.SearchByTitle(Console.ReadLine() ?? "");
                        break;
                    case "4":
                        Console.Write("\n  Enter author to search: ");
                        catalog.SearchByAuthor(Console.ReadLine() ?? "");
                        break;
                    case "5":
                        Console.Write("\n  Enter genre to search: ");
                        catalog.SearchByGenre(Console.ReadLine() ?? "");
                        break;
                    case "6": catalog.DisplayByGenre();  break;
                    case "7": catalog.DisplayByAuthor(); break;
                    case "8": catalog.DisplayAll();      break;
                    case "9":
                        Console.WriteLine("\n  Goodbye!\n");
                        return;
                    default:
                        Console.WriteLine("\n  [!] Invalid option. Please enter 1-9.");
                        break;
                }
            }
        }
        //Sub-menus
        static void AddBookMenu()
        {
            Console.WriteLine("\n  ── Add a New Book ──────────────────────");
            Console.Write("  Title:            ");
            string title = Console.ReadLine() ?? "";
            Console.Write("  Author:           ");
            string author = Console.ReadLine() ?? "";
            Console.Write("  Genre:            ");
            string genre = Console.ReadLine() ?? "";
            int year = 0;
            while (year < 1000 || year > DateTime.Now.Year)
            {
                Console.Write("  Publication Year: ");
                int.TryParse(Console.ReadLine(), out year);
                if (year < 1000 || year > DateTime.Now.Year)
                    Console.WriteLine("  [!] Please enter a valid year.");
            }
            catalog.AddBook(new Book(title, author, genre, year));
        }
        static void RemoveBookMenu()
        {
            Console.Write("\n  Enter the title of the book to remove: ");
            string title = Console.ReadLine() ?? "";
            catalog.RemoveBook(title);
        }
    }
}