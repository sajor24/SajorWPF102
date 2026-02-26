CREATE PROCEDURE [dbo].[CreateEmployee]
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Age INT,
    @Position NVARCHAR(100)
AS
BEGIN
    INSERT INTO [dbo].[Employee] (FirstName, LastName, Age, Position)
    VALUES (@FirstName, @LastName, @Age, @Position);
    
END