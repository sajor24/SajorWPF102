CREATE PROCEDURE [dbo].[DeleteEmployee]
    @EmployeeId INT
AS
BEGIN
    DELETE FROM [dbo].[Employee]
    WHERE EmployeeId = @EmployeeId;
END
