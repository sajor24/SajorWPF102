CREATE PROCEDURE [dbo].[ReadPOSById]
 @ProductID INT
AS
BEGIN
    SELECT *
    FROM [dbo].[POS]
    WHERE ProductID = @ProductID;
END