# Book Cataloging System
**MSCS 632 – Advanced Programming Languages**  
Kshitiz Subba & Sanjay Subedi | University of the Cumberlands | 2026

---

## Overview
The same Book Cataloging application implemented in **C#** and **Ruby**.  
Both versions offer identical functionality through a text-based menu.

## Features
| # | Feature |
|---|---------|
| 1 | Add a book (title, author, genre, year) |
| 2 | Remove a book by title |
| 3 | Search by title |
| 4 | Search by author |
| 5 | Search by genre |
| 6 | Display books grouped by genre |
| 7 | Display books grouped by author |
| 8 | Show all books |

---

## Repository Structure
```
Book-Cataloging-System/
├── C#-Version/
│   ├── BookCatalog.cs       ← All C# source code (single file)
│   └── BookCatalog.csproj   ← .NET 8 project file
├── Ruby-Version/
│   └── book_catalog.rb      ← All Ruby source code (single file)
└── README.md
```
---
## How to Run
### C# Version
Requires [.NET 8 SDK](https://dotnet.microsoft.com/download)
```bash
cd C#-Version
dotnet run
```
### Ruby Version
Requires [Ruby 3+](https://www.ruby-lang.org/)
```bash
cd Ruby-Version
ruby book_catalog.rb
```
---
## Language-Specific Highlights

### C#
- **Classes with properties** — `Book` uses auto-properties (`{ get; set; }`)
- **LINQ** — `Where`, `FirstOrDefault`, `GroupBy`, `OrderBy`, `Any` for all search and reporting
- **Strong static typing** — every variable and parameter has an explicit type
- **`List<Book>`** — generic typed collection
### Ruby
- **Dynamic typing** — no type declarations needed anywhere
- **Blocks** — `{ |b| ... }` passed to `select`, `find`, `any?`, `each`, `sort_by`
- **`group_by`** — built-in `Enumerable` method returns a `Hash` keyed by the block value
- **Symbol-to-proc** — `&:title`, `&:genre` as concise block shorthand
- **`case/when`** — Ruby's expressive pattern-matching alternative to `switch`
