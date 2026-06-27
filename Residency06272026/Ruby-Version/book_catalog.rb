#  Book Cataloging System — Ruby Implementation
#  Authors: Kshitiz Subba & Sanjay Subedi
#  Course:  MSCS 632 – Advanced Programming Languages
#Book — stores details for a single book
class Book
  attr_accessor :title, :author, :genre, :year

  def initialize(title, author, genre, year)
    @title  = title
    @author = author
    @genre  = genre
    @year   = year
  end
  #Human-readable representation used throughout the app
  def to_s
    "  \"#{@title}\" by #{@author} [#{@genre}, #{@year}]"
  end
end
#  CatalogManager — manages the collection of books
class CatalogManager
  def initialize
    @books = []   # Ruby Array; dynamic typing — no explicit type needed
  end

  #Add
  def add_book(book)
    # Ruby block with 'any?' — functional-style check for duplicates
    if @books.any? { |b| b.title.casecmp?(book.title) && b.author.casecmp?(book.author) }
      puts "\n  [!] \"#{book.title}\" by #{book.author} already exists in the catalog."
      return
    end
    @books << book
    puts "\n  [+] Added: #{book}"
  end

  # Remove
  def remove_book(title)
    # 'find' returns the first element for which the block returns true
    found = @books.find { |b| b.title.casecmp?(title) }
    if found.nil?
      puts "\n  [!] No book titled \"#{title}\" found."
      return
    end
    @books.delete(found)
    puts "\n  [-] Removed: #{found}"
  end

  #Search
  def search_by_title(query)
    #'select' filters elements using a block — functional iteration
    results = @books.select { |b| b.title.downcase.include?(query.downcase) }
    print_results("Search by title \"#{query}\"", results)
  end

  def search_by_author(query)
    results = @books.select { |b| b.author.downcase.include?(query.downcase) }
    print_results("Search by author \"#{query}\"", results)
  end

  def search_by_genre(query)
    results = @books.select { |b| b.genre.downcase.include?(query.downcase) }
    print_results("Search by genre \"#{query}\"", results)
  end

  #Reporting
  def display_by_genre
    return print_empty if @books.empty?

    puts "\n  ── Books grouped by Genre ──────────────────"
    #group_by is a built-in Enumerable method that returns a Hash
    #keyed by the block's return value
    @books.sort_by(&:genre).group_by(&:genre).each do |genre, books|
      puts "\n  [#{genre}]"
      books.sort_by(&:title).each { |b| puts b }
    end
  end

  def display_by_author
    return print_empty if @books.empty?

    puts "\n  ── Books grouped by Author ─────────────────"
    @books.sort_by(&:author).group_by(&:author).each do |author, books|
      puts "\n  [#{author}]"
      books.sort_by(&:year).each { |b| puts b }
    end
  end

  def display_all
    return print_empty if @books.empty?

    puts "\n  ── All Books (#{@books.length} total) ──────────────────"
    @books.sort_by(&:title).each { |b| puts b }
  end

  def count
    @books.length
  end
  private
  #Helpers
  def print_results(header, results)
    puts "\n  ── #{header} ──"
    if results.empty?
      puts "  No matching books found."
    else
      results.each { |b| puts b }
    end
  end

  def print_empty
    puts "\n  The catalog is empty."
  end
end
#UI helpers — keep the menu logic separate from catalog logic
def prompt(message)
  print message
  gets.chomp
end

def add_book_menu(catalog)
  puts "\n  ── Add a New Book ──────────────────────"
  title  = prompt("  Title:            ")
  author = prompt("  Author:           ")
  genre  = prompt("  Genre:            ")

  year = 0
  until year.between?(1000, Time.now.year)
    input = prompt("  Publication Year: ")
    year  = input.to_i
    puts "  [!] Please enter a valid year." unless year.between?(1000, Time.now.year)
  end

  catalog.add_book(Book.new(title, author, genre, year))
end

def remove_book_menu(catalog)
  title = prompt("\n  Enter the title of the book to remove: ")
  catalog.remove_book(title)
end
# Seed data — pre-loaded sample books
def seed_catalog(catalog)
  catalog.add_book(Book.new("The Hobbit",           "J.R.R. Tolkien",      "Fantasy",   1937))
  catalog.add_book(Book.new("1984",                 "George Orwell",       "Dystopian", 1949))
  catalog.add_book(Book.new("To Kill a Mockingbird","Harper Lee",          "Fiction",   1960))
  catalog.add_book(Book.new("Dune",                 "Frank Herbert",       "Sci-Fi",    1965))
  catalog.add_book(Book.new("The Name of the Wind", "Patrick Rothfuss",    "Fantasy",   2007))
  catalog.add_book(Book.new("Brave New World",      "Aldous Huxley",       "Dystopian", 1932))
  catalog.add_book(Book.new("Foundation",           "Isaac Asimov",        "Sci-Fi",    1951))
  catalog.add_book(Book.new("The Great Gatsby",     "F. Scott Fitzgerald", "Fiction",   1925))
end
#Main menu loop
def run_menu(catalog)
  loop do
    puts "\n╔══════════════════════════════════════╗"
    puts "║     Book Cataloging System (Ruby)    ║"
    puts "╠══════════════════════════════════════╣"
    puts "║  1. Add a book                       ║"
    puts "║  2. Remove a book                    ║"
    puts "║  3. Search by title                  ║"
    puts "║  4. Search by author                 ║"
    puts "║  5. Search by genre                  ║"
    puts "║  6. Display books by genre           ║"
    puts "║  7. Display books by author          ║"
    puts "║  8. Show all books                   ║"
    puts "║  9. Exit                             ║"
    puts "╚══════════════════════════════════════╝"

    choice = prompt("  Choose an option: ")

    case choice
    when "1" then add_book_menu(catalog)
    when "2" then remove_book_menu(catalog)
    when "3"
      query = prompt("\n  Enter title to search: ")
      catalog.search_by_title(query)
    when "4"
      query = prompt("\n  Enter author to search: ")
      catalog.search_by_author(query)
    when "5"
      query = prompt("\n  Enter genre to search: ")
      catalog.search_by_genre(query)
    when "6" then catalog.display_by_genre
    when "7" then catalog.display_by_author
    when "8" then catalog.display_all
    when "9"
      puts "\n  Goodbye!\n\n"
      break
    else
      puts "\n  [!] Invalid option. Please enter 1-9."
    end
  end
end

# ----------------------------------------------------------------
#  Entry point
# ----------------------------------------------------------------
catalog = CatalogManager.new
seed_catalog(catalog)
run_menu(catalog)
