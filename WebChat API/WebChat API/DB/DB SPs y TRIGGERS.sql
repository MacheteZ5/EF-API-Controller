Use WebChat;
GO

CREATE OR ALTER Trigger trg_ValidateUserCreation on [User] INSTEAD OF INSERT
AS
BEGIN
	  INSERT INTO [User] (FirstName, LastName, BirthDate, UserName, Password, Email, FecTransac, StatusID)
      SELECT i.FirstName, i.LastName, i.BirthDate, i.UserName, i.Password, i.Email, GETDATE(), 1
      FROM inserted as i;
END;
GO

CREATE OR ALTER Trigger trg_ValidateChatsListCreation on [ChatsList] AFTER INSERT
AS
BEGIN

	DECLARE @SUser NVARCHAR(25);
	DECLARE @FUser NVARCHAR(25);

	SELECT @FUser = i.FUser, @SUser = i.SUser
	FROM inserted AS i

	INSERT INTO [ChatsList] (FUser, SUser)
    SELECT @SUser, @FUser
END;