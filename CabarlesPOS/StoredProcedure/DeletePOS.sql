CREATE PROCEDURE [dbo].[DeletePOS]
 @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[POS]
    WHERE ProductID = @ProductID;
END