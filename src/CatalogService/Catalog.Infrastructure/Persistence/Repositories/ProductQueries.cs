using Azure;
using BuildingBlocks.Shared.Results;
using Dapper;
using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Linq.Expressions;
namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories
{
    public class ProductQueries : IProductQueries
    {
        private readonly IDbConnection _connection;
        public ProductQueries(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var sql = @"Select * from Product where Id = @id";
            return await _connection.QueryFirstOrDefaultAsync<Product>(sql, new { id });
            //return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, ct);
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken ct)
        {
            var sql = @"Select * from Product ";
            var command = new CommandDefinition(sql, cancellationToken: ct);
            return await _connection.QueryAsync<ProductDto>(command);
            //return await _context.Products.AsNoTracking().ToListAsync();
        }
        public async Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct)
        {
            var sql = @"Select * from Product where Id IN @ids";
            var command = new CommandDefinition(sql, new { ids }, cancellationToken: ct);
            var result = await _connection.QueryAsync<Product>(command);
            return result.ToList();
        }

        public Task<List<ProductDto?>> GetFeaturedProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto?>> GetNewProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto?>> GetProductsByCategoryAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsSkuUniqueAsync(string sku, CancellationToken ct)
        {
            // Returns true if SKU does NOT exist
            var sql = @"Select Case When Exists (Select 1 from Products where sku = @sku) Then Cast(1 as Bit) Else Cast(0 as Bit) End";
            var command = new CommandDefinition(sql, new { sku }, cancellationToken: ct);
            bool exists = await _connection.ExecuteScalarAsync<bool>(command);
            return !exists;
        }
    }
}
//Aap bilkul sahi direction mein ja rahi ho—ye 2 SQL patterns actually **bohat saare core concepts cover karte hain**. Main aapko in dono snippets ko tod kar **proper learning topics** ke naam de raha hoon, step-by - step, taake aap har cheez separately seekh sako.

//---

//# 🔷 Code 1: `SELECT CASE WHEN EXISTS`

//```sql
//SELECT CASE
//    WHEN EXISTS (
//        SELECT 1
//        FROM Product
//        WHERE SKU = @Sku
//    )
//    THEN CAST(1 AS BIT)
//    ELSE CAST(0 AS BIT)
//END;
//```

//## 📚 Is se seekhne wale topics:

//### 1. EXISTS Operator

//*Meaning: record exist karta hai ya nahi
//* Concept: boolean check(true/false logic in SQL)

//👉 Topic name: **SQL EXISTS predicate**

//---

//### 2. Subquery (Inner Query)

//```sql
//SELECT 1 FROM Product WHERE SKU=@Sku
//```

//* Query ke andar query
//* Outer query is pe depend karti hai

//👉 Topic name: **SQL Subqueries(Nested Queries) * *

//---

//### 3. SELECT 1 Pattern

//*Sirf existence check ke liye
//* Data return nahi hota

//👉 Topic name: **EXISTS optimization pattern**

//---

//### 4. CASE Expression

//```sql
//CASE WHEN ... THEN ... ELSE ...
//```

//* SQL ka IF/ELSE logic

//👉 Topic name: **SQL CASE WHEN expression**

//---

//### 5. Data Type Conversion (CAST)

//```sql
//CAST(1 AS BIT)
//```

//* 1/0 ko boolean type (BIT) mein convert karna

//👉 Topic name: **SQL CAST / Data type conversion**

//---

//### 6. Boolean Logic in SQL

//*SQL mein true/false directly nahi hota
//* BIT (1/0) use hota hai

//👉 Topic name: **SQL boolean representation**

//---

//### 7. Scalar Query (Single Value Return)

//*Result ek hi value hoti hai

//👉 Topic name: **Scalar subquery / scalar expression * *

//---

//# 🔷 Code 2: `IF EXISTS`

//```sql
//IF EXISTS(
//    SELECT 1
//    FROM Product
//    WHERE SKU=@Sku
//)
//BEGIN

//END
//```

//## 📚 Is se seekhne wale topics:

//---

//### 1. IF EXISTS Statement

//* Control flow logic in SQL
//* Condition true ho to block run hota hai

//👉 Topic name: **SQL Control - of - flow(IF EXISTS) * *

//---

//### 2. Control Flow in SQL

//*SQL mein decision making

//👉 Topic name: **SQL procedural logic**

//---

//### 3. BEGIN...END Block

//*Multiple statements group karna

//👉 Topic name: **SQL statement blocks**

//---

//### 4. Subquery inside IF EXISTS

//```sql
//SELECT 1 FROM Product WHERE SKU=@Sku
//```

//👉 Topic name: **Correlated / non - correlated subqueries * *

//---

//### 5. EXISTS Predicate (again core concept)

//*Same EXISTS concept but different usage context

//👉 Topic name: **EXISTS in control flow**

//---

//### 6. Conditional Execution (Very Important)

//*Code run hota hai ya nahi based on condition

//👉 Topic name: **SQL conditional execution**

//---

//# 🔥 Combined Master Topics List (Important Summary)

//Agar aap in dono codes se overall learning list banao, to ye complete topics ban jate hain:

//## ⭐ Core Topics (Must Learn)

//1.EXISTS operator
//2. Subqueries (nested queries)
//3. CASE WHEN expression
//4. IF EXISTS (control flow)
//5. BEGIN END blocks
//6. SELECT 1 pattern

//---

//## ⭐ Intermediate Topics

//7. Scalar queries (single value return)
//8.Boolean logic in SQL (BIT 1/0)
//9. Control-of-flow statements
//10. Query optimization patterns

//---

//## ⭐ Supporting Concepts

//11. CAST / CONVERT
//12. SQL Server data types (BIT etc.)
//13. Predicate logic (IN / EXISTS / ANY / ALL concepts)

//---

//# 🧠 Simple Understanding (Real Meaning)

//* `EXISTS` → check karta hai “data hai ya nahi”
//* `CASE` → value return karta hai based on condition
//* `IF EXISTS` → SQL code execute ya skip karta hai

//---

//# 📌 Practical Advice (Dapper / EF Core context)

//* Backend developers ke liye:

//  * `EXISTS + CASE` → **API response / boolean return **
//  * `IF EXISTS` → **SQL procedures / scripts / updates**

//---

//# 🚀 Agar aap properly master karna chahti ho

//Search these exact topics:

//👉 “SQL EXISTS vs IN”
//👉 “SQL CASE WHEN tutorial”
//👉 “SQL subqueries explained”
//👉 “SQL control of flow IF ELSE”
//👉 “SQL Server BIT data type”

//---

//Agar aap chaho to main aapko **Dapper ke real-world patterns (CRUD + EXISTS + JOIN + pagination)** ka ek proper roadmap bhi bana deta hoon jo job-ready bana deta hai.
