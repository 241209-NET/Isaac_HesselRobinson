-- On the Chinook DB, practice writing queries with the following exercises

USE MyDatabase;
-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
    SELECT CustomerId, FirstName, LastName, Country FROM [dbo].[Customer] WHERE Country != 'USA';
-- List all customers from Brazil
    SELECT CustomerId, FirstName, LastName, Country FROM [dbo].[Customer] WHERE Country = 'Brazil';
-- List all sales agents
    SELECT EmployeeID, FirstName, LastName, Country FROM [dbo].[Employee];
-- Retrieve a list of all countries in billing addresses on invoices
    SELECT BillingCountry FROM [dbo].[Invoice];
-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?
    SELECT year(InvoiceDate),
        COUNT(year(InvoiceDate)) as 'Total Invoices',
        SUM(Total) as 'Sales Total' 
        FROM [dbo].[Invoice]
        GROUP BY year(InvoiceDate)
        HAVING year(InvoiceDate) = 2009;
    -- (challenge: find the invoice count sales total for every year using one query)
    SELECT year(InvoiceDate),
        COUNT(year(InvoiceDate)) as 'Total Invoices',
        SUM(Total) as 'Sales Total' 
        FROM [dbo].[Invoice]
        GROUP BY year(InvoiceDate)

-- how many line items were there for invoice #37
    SELECT InvoiceId,
        COUNT(InvoiceLineId) as 'Lines in Invoice'
        FROM [dbo].InvoiceLine
        WHERE InvoiceId = 37
        GROUP BY InvoiceId;

-- how many invoices per country? BillingCountry  # of invoices -
    SELECT BillingCountry,
        COUNT(BillingCountry) as 'Total Invoices'
        FROM [dbo].[Invoice]
        GROUP BY BillingCountry;

-- Retrieve the total sales per country, ordered by the highest total sales first.
    SELECT BillingCountry,
        SUM(Total) as 'Sales Total'
        FROM [dbo].[Invoice]
        GROUP BY BillingCountry
        ORDER BY 'Sales Total' DESC;


-- JOINS CHALLENGES
-- Every Album by Artist
    SELECT Artist.Name, Album.Title
        FROM [dbo].[Album]
        INNER JOIN [dbo].[Artist]
        ON Album.ArtistId = Artist.ArtistId
        ORDER BY Artist.Name;
-- All songs of the rock genre
    SELECT Track.Name
        FROM [dbo].[Track]
        INNER JOIN [dbo].[Genre]
        ON Track.GenreId = Genre.GenreId
        WHERE Genre.Name = 'Rock';
-- Show all invoices of customers from brazil (mailing address not billing)
    SELECT Invoice.InvoiceDate, CONCAT(Customer.FirstName, ' ', Customer.LastName) as 'Customer', CONCAT(Invoice.BillingAddress, ', ', Invoice.BillingCity, ', ', Invoice.BillingState, ', ', Invoice.BillingCountry) as 'Billing Address', Invoice.Total
        FROM [dbo].[Invoice]
        INNER JOIN [dbo].[Customer]
        ON Invoice.CustomerId = Customer.CustomerId
        WHERE Customer.Country = 'Brazil'
        ORDER BY Invoice.InvoiceDate;

-- Show all invoices together with the name of the sales agent for each one
    SELECT Invoice.InvoiceDate, CONCAT(Employee.FirstName, ' ', Employee.LastName) as 'Sales Agent', CONCAT(Customer.FirstName, ' ', Customer.LastName) as 'Customer', CONCAT(Invoice.BillingAddress, ', ', Invoice.BillingCity, ', ', Invoice.BillingState, ', ', Invoice.BillingCountry) as 'Billing Address', Invoice.Total
        FROM [dbo].[Invoice]
        INNER JOIN [dbo].[Customer]
            ON Invoice.CustomerId = Customer.CustomerId
        INNER JOIN [dbo].[Employee]
            ON Customer.SupportRepId = Employee.EmployeeId
        ORDER BY Invoice.InvoiceDate;

-- Which sales agent made the most sales in 2009?
    SELECT Employee.LastName,--CONCAT(Employee.FirstName, ' ', Employee.LastName) as 'Highest Selling Agent',
        MAX(sales_count) as 'Number of Sales'
        FROM(
            SELECT Employee.LastName,
                COUNT(Invoice.InvoiceId) AS sales_count
                FROM [dbo].[Employee]
                INNER JOIN [dbo].[Customer]
                    ON Customer.SupportRepId = Employee.EmployeeId
                INNER JOIN [dbo].[Invoice]
                    ON Invoice.CustomerId = Customer.CustomerId
                GROUP BY Employee.lastName
        ) AS counted
        INNER JOIN [dbo].[Employee]
            ON counted.LastName = Employee.LastName
        GROUP BY Employee.LastName;


-- How many customers are assigned to each sales agent?

-- Which track was purchased the most ing 20010?

-- Show the top three best selling artists.

-- Which customers have the same initials as at least one other customer?



-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.


-- DML exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.
