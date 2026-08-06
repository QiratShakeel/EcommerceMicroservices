/* ------------Catalog Db Database */
use CatalogDb

/* ------------Selection Of tables */
SET STATISTICS IO ON;	/* performance measurment tool*/
select * from catalog.Categories
select * from catalog.Products
select * from catalog.ProductCategories
select * from catalog.ProductImages
SELECT TOP 5 * FROM catalog.Products ORDER BY Price DESC;

/* ------Check primary key cluster---*/
SELECT i.name,i.type_desc FROM sys.indexes i WHERE i.object_id = OBJECT_ID('catalog.Products');

/* ------Check any column in table cluster---*/
SELECT i.name, c.name AS column_name
FROM sys.indexes i
JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE i.object_id = OBJECT_ID('catalog.Products')
  AND i.type_desc = 'CLUSTERED';

/* ------------Insertion In Category Table: */
DECLARE @ElectronicsId UNIQUEIDENTIFIER = NEWID();

INSERT INTO catalog.Categories (Id, Name, Description, ParentCategoryId)
VALUES (@ElectronicsId, 'Electronics', 'All electronic items', NULL);

INSERT INTO catalog.Categories (Id, Name, Description, ParentCategoryId) VALUES
(NEWID(), 'Mobile Phones', 'Smartphones and accessories', @ElectronicsId),
(NEWID(), 'Laptops', 'Personal and professional laptops', @ElectronicsId),
(NEWID(), 'Televisions', 'LED, OLED, and Smart TVs', @ElectronicsId),
(NEWID(), 'Headphones', 'Wireless and wired headphones', @ElectronicsId),
(NEWID(), 'Gaming Consoles', 'PlayStation, Xbox, and others', @ElectronicsId),
(NEWID(), 'Cameras', 'DSLR, mirrorless, and compact cameras', @ElectronicsId),
(NEWID(), 'Accessories', 'Chargers, cables, and other add-ons', @ElectronicsId);

/* ------------Insertion In Product Table: */
DECLARE @Now DATETIME = GETUTCDATE();

INSERT INTO catalog.Products(Id, Name, Description, SKU, Price, Status, Inventory_StockQuantity, Inventory_ReservedQuantity, Inventory_WarehouseLocation, CreatedDate, UpdatedDate)
VALUES
(NEWID(), 'iPhone 14 Pro', 'Apple flagship smartphone', 'SKU-IPH14PRO', 999.99,  1, 50, 5, 'A1-Rack-1', @Now, NULL),
(NEWID(), 'Samsung Galaxy S23', 'High-end Android smartphone', 'SKU-SAMS23', 899.99,  1, 40, 3, 'A1-Rack-2', @Now, NULL),
(NEWID(), 'Dell XPS 13', 'Compact and powerful ultrabook', 'SKU-DELLXPS13', 1199.00,  1, 25, 2, 'B2-Rack-1', @Now, NULL),
(NEWID(), 'MacBook Air M2', 'Lightweight Apple laptop', 'SKU-MACM2', 1299.00, 1, 30, 4, 'B2-Rack-2', @Now, NULL),
(NEWID(), 'Sony WH-1000XM5', 'Noise cancelling headphones', 'SKU-SONYWH5', 349.99,  1, 60, 10, 'C1-Rack-1', @Now, NULL),
(NEWID(), 'LG OLED C2 55"', 'OLED Smart TV with stunning visuals', 'SKU-LGOLED55', 1499.99,  1, 15, 1, 'D1-Rack-3', @Now, NULL),
(NEWID(), 'PlayStation 5', 'Next-gen gaming console', 'SKU-PS5', 499.99,  1, 20, 5, 'E1-Rack-1', @Now, NULL),
(NEWID(), 'Canon EOS R6', 'Professional mirrorless camera', 'SKU-CANONR6', 2499.00,  1, 10, 2, 'F1-Rack-2', @Now, NULL);

/* ------------Insertion In Product Image Table: */
INSERT INTO catalog.ProductImages(Url, AltText, FileType, ProductId)
Select 'uploads/products/E3F1DEB5-DDC0-40BF-9CF4-DFDB7AEE8D06.png', 'iPhone 14 Pro Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-IPH14PRO'
UNION ALL
Select 'uploads/products/D8D07A22-ED9E-47CE-9FA4-903EE4C5E899.png', 'Samsung Galaxy S23 Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-SAMS23'
UNION ALL
Select 'uploads/products/9B571CEE-B8DA-471C-819F-1FE7401787BA.png', 'Dell XPS 13 Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-DELLXPS13'
UNION ALL
Select 'uploads/products/DDAA0162-D55A-46AE-979B-8FC402DF5C62.png', 'MacBook Air M2 Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-MACM2'
UNION ALL
SELECT 'uploads/products/FA9E2513-BDCB-4A3C-82AE-590BB7E7FBCE.png', 'Sony Headphones Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-SONYWH5'
UNION ALL
SELECT 'uploads/products/91C6F85A-60E6-471D-9A84-7D12F017EF4F.png', 'LG OLED TV Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-LGOLED55'
UNION ALL
SELECT 'uploads/products/F059E19A-8979-4D04-BFE8-74B48123606E.jpg', 'PlayStation 5 Image', 'jpg', Id FROM catalog.Products WHERE SKU = 'SKU-PS5'
UNION ALL
SELECT 'uploads/products/FB5E111F-64FD-4616-BDD4-D488AC80E5AD.png', 'Canon Camera Image', 'png', Id FROM catalog.Products WHERE SKU = 'SKU-CANONR6';

/* ------------Insertion In Product Category Table: */
INSERT INTO catalog.ProductCategories (ProductId, CategoryId)
SELECT p.Id, c.Id
FROM catalog.Products p
JOIN catalog.Categories c ON c.Name = 'Electronics';

INSERT INTO catalog.ProductCategories (ProductId, CategoryId)
Values('8018EFA5-6F6C-4ACF-88C3-43271CDC628E','7FB639FA-32CD-4AA0-80F3-D710950C7928')
('7A151458-8E3E-40B2-968B-199DA3C72937','F136BC4D-4B66-4091-A306-CF1D61074E20'),
('2B3EB8D7-7E95-427A-B0F6-1C8C3FB00565','7FB639FA-32CD-4AA0-80F3-D710950C7928'),
('4DA90FB3-326E-4652-A276-28028ADD92BB','CA438CDB-7B92-4291-A0E9-59C1209423A2'),
('E5F395D2-3CE7-491E-A82C-2E5AE1576E80','4BE18589-1346-4B4A-9868-B1D247D263DB')


/* -------------Query to Fetch Top 3 Different Categories with any of their product image*/
SELECT TOP 3 name, Description, Url
FROM (
    SELECT cat.name, cat.Description, pi.Url,
           ROW_NUMBER() OVER (PARTITION BY cat.Id ORDER BY p.Id) AS rn
    FROM catalog.Categories cat
    JOIN catalog.ProductCategories pc ON cat.Id = pc.CategoryId
    JOIN catalog.Products p ON pc.ProductId = p.Id
    JOIN catalog.ProductImages pi ON p.Id = pi.ProductId
) t
WHERE rn = 1;

