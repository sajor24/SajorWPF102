CREATE PROCEDURE [dbo].[CreatePOS]
    @ProductID NCHAR(10),
    @ProductName NCHAR(10),
    @Price INT,
    @Stock NCHAR(10)
AS
BEGIN
    INSERT INTO [dbo].[POS] (ProductID, ProductName, Price, Stock)
    VALUES (@ProductID, @ProductName, @Price, @Stock);
    
END