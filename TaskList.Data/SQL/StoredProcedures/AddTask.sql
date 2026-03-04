CREATE PROCEDURE AddTask
(@Description varchar(255),
@DueDate DATE)
AS
BEGIN
	INSERT INTO dbo.TaskList
	(Description, DueDate)
	VALUES
	(@Description, @DueDate)
END