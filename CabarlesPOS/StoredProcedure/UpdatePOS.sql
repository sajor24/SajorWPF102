CREATE PROCEDURE [dbo].[UpdatePOS]
 @ProductID INT,
    @ProductName NVARCHAR(100),
    @Price INT,
    @Stock INT
    
AS
BEGIN
    UPDATE [dbo].[POS]
    SET
        ProductName= @ProductName,
        Price  = @Price,
        Stock     = @Stock
    WHERE ProductID = @ProductID;
    END