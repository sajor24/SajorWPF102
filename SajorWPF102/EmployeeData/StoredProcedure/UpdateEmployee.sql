CREATE PROCEDURE [dbo].[UpdateEmployee]
    @EmployeeId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Age INT,
    @Position NVARCHAR(100)
AS
BEGIN
    UPDATE [dbo].[Employee]
    SET
        FirstName = @FirstName,
        LastName  = @LastName,
        Age       = @Age,
        Position  = @Position
    WHERE EmployeeId = @EmployeeId;
END
