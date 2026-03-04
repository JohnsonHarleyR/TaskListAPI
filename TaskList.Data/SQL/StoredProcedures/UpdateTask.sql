CREATE PROCEDURE UpdateTask
(@Id INT,
@Description varchar(255),
@DueDate DATE,
@IsCompleted BIT)
AS
BEGIN
	UPDATE dbo.TaskList
	SET Description = @Description,
		DueDate = @DueDate,
		IsCompleted = @IsCompleted
	WHERE Id = @Id
END