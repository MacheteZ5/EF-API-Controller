/*
	USE MASTER;
	GO
	DROP TABLE WebChat;
	GO
*/

IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'WebChat')
   print 'db exists'
ELSE
	CREATE DATABASE WebChat
	print 'db created successfully'
GO

USE WebChat
GO

CREATE TABLE [User] (
	[UserName] CHAR(25) Primary Key,
	[Password] CHAR(25) NOT NULL,
	[FirstName] CHAR(100) NOT NULL,
	[LastName] CHAR(100) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[Email] CHAR(50) NOT NULL,
	[FecTransac] DATETIME2 NOT NULL,
	[StatusID] INTEGER NOT NULL
);
GO

CREATE TABLE [StatusUser] (
	[ID] INTEGER PRIMARY KEY IDENTITY(1,1),
	[Name] CHAR(25) NOT NULL,
	[Description] CHAR(200) NOT NULL,
	[Vigente] BIT NOT NULL,
	[FecTransac] DATETIME2 NOT NULL
);
GO

CREATE TABLE [ChatsList] (
	[ID] BIGINT PRIMARY KEY IDENTITY(1,1),
	[FUser] CHAR(25) NOT NULL,
	[SUser] CHAR(25) NOT NULL,
	[FecTransac] DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE [Message] (
	[ID] BIGINT PRIMARY KEY IDENTITY(1,1),
	[ContactID] BIGINT NOT NULL,
	[Message] VARCHAR(8000) NOT NULL,
	[StatusMessageID] INT NOT NULL,
	[FecTransac] DATETIME2 NOT NULL
);
GO

CREATE TABLE [StatusMessage] (
	[ID] INT PRIMARY KEY IDENTITY(1,1),
	[Name] CHAR(25) NOT NULL,
	[Description] CHAR(200) NOT NULL,
	[Vigente] BIT NOT NULL,
	[FecTransac] DATETIME2 NOT NULL
);
GO

ALTER TABLE [User] ADD CONSTRAINT FK_UserStatusID FOREIGN KEY([StatusID]) REFERENCES [StatusUser]([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [ChatsList] ADD CONSTRAINT FK_FirstUser FOREIGN KEY([FUser]) REFERENCES [User]([UserName]) ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [ChatsList] ADD CONSTRAINT FK_SecondUser FOREIGN KEY([SUser]) REFERENCES [User]([UserName]) ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Message] ADD CONSTRAINT FK_MessageChatsList FOREIGN KEY([ContactID]) REFERENCES [ChatsList]([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Message] ADD CONSTRAINT FK_MessageStatusID FOREIGN KEY([StatusMessageID]) REFERENCES [StatusMessage]([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION;
GO

INSERT INTO StatusUser (Name, Description, Vigente, FecTransac)
VALUES ('Activo', 'El usuario se encuentra activo y puede hacer uso de la aplicación', 1, GETDATE()), ('Inactivo', 'El usuario se encuentra inactivo y no puede hacer uso de la aplicación', 1, GETDATE());
GO

INSERT INTO StatusMessage (Name, Description, Vigente, FecTransac)
VALUES ('Activo', 'El Mensaje se encuentra activo y puede ser leído en la aplicación', 1, GETDATE()), ('Eliminado', 'El Mensaje se encuentra eliminado y no puede ser leído en la aplicación', 1, GETDATE());
GO

INSERT INTO [User] (FirstName, LastName, BirthDate, UserName, Password, Email, FecTransac, StatusID)
VALUES ('José Alejandro', 'Montenegro Monzón', '04/03/1999', 'MacheteZ5', 'MHLionOfJuda21+', 'jamontenegromonzon@hotmail.com', GETDATE(), 1)
GO

INSERT INTO [Message] ([ContactID],[Message],[StatusMessageID],[FecTransac]) Values ('2','Hola Fernando',1,GETDATE())
INSERT INTO [Message] ([ContactID],[Message],[StatusMessageID],[FecTransac]) Values ('1','¿Qué tal estás?',1,GETDATE())

SELECT * FROM StatusUser
GO
SELECT * FROM StatusMessage
GO
SELECT * FROM [User]
GO
SELECT * FROM ChatsList
GO
SELECT * FROM Message
GO