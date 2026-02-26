CREATE PROCEDURE [dbo].[ReadEmployeeById]
    @EmployeeId INT
AS
BEGIN
    SELECT *
    FROM [dbo].[Employee]
    WHERE EmployeeId = @EmployeeId;
END