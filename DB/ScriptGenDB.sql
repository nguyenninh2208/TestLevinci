USE [TestLevinci]
GO
/****** Object:  Table [dbo].[User]    Script Date: 02/02/2023 23:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Role] [varchar](100) NOT NULL,
	[Email] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([UserID], [Username], [Password], [Name], [Role], [Email]) VALUES (1, N'admin', N'202cb962ac59075b964b07152d234b70', N'admin', N'admin', N'a@gmail.com')
INSERT [dbo].[User] ([UserID], [Username], [Password], [Name], [Role], [Email]) VALUES (2, N'user', N'202cb962ac59075b964b07152d234b70', N'user', N'user', N'a@gmail.com')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  StoredProcedure [dbo].[SP_Mana_UserUpdate]    Script Date: 02/02/2023 23:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Mana_UserUpdate]
	@pUserID INT,
	@pUsername VARCHAR(50),
	@pName VARCHAR(255),
	@pRole VARCHAR(100),
	@pEmail VARCHAR(255)
AS
BEGIN
	DECLARE @vResultID INT = -1, 
	@vMessages NVARCHAR(200) = N'Thất bại',
	@vEffect BIT = 0;
	
	IF(EXISTS (SELECT TOP 1 1 FROM [User](NOLOCK) WHERE UserID = @pUserID))
	BEGIN
	    UPDATE [User]
	    SET [Username] = @pUsername,
			[Name] = @pName,
			[Role] = @pRole,
			[Email] = @pEmail
		WHERE [UserID] = @pUserID
		
		IF (@@ROWCOUNT > 0)
		BEGIN
		    SET @vEffect = 1;
		END
		
	END
	
	IF (@vEffect = 1)
	BEGIN
	    SET @vResultID = 1;
	    SET @vMessages = N'Thành công';
	END;
	
	SELECT @vResultID AS ResultID, @vMessages AS [Message]
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Mana_UserLogin]    Script Date: 02/02/2023 23:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Mana_UserLogin]
	@Username VARCHAR(50),
	@Password VARCHAR(50)
AS
BEGIN
	SELECT users.[UserID], 
			users.[Username], 
			users.[Password], 
			users.[Name], 
			users.[Role], 
			users.[Email]
	FROM [TestLevinci].[dbo].[User] users
	WHERE users.[Username] = @Username AND users.Password = @Password
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Mana_UserInsert]    Script Date: 02/02/2023 23:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Mana_UserInsert]
	@pUsername VARCHAR(50),
	@pPassword VARCHAR(255),
	@pName VARCHAR(255),
	@pRole VARCHAR(100),
	@pEmail VARCHAR(255)
AS
BEGIN
	DECLARE @vResultID INT = -1, 
	@vMessages NVARCHAR(200) = N'Thất bại',
	@vEffect BIT = 0;
	
	INSERT INTO [User] (Username, Password,Name,Role,Email)
	VALUES (@pUsername, @pPassword, @pName, @pRole, @pEmail)
	IF (@vEffect = 1)
	BEGIN
	    SET @vResultID = 1;
	    SET @vMessages = N'Thành công';
	END;
	
	SELECT @vResultID AS ResultID, @vMessages AS [Message]
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Mana_UserDelete]    Script Date: 02/02/2023 23:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Mana_UserDelete]
	@pUserID INT
AS
BEGIN
	DECLARE @vResultID INT = -1, 
	@vMessages NVARCHAR(200) = N'Thất bại',
	@vEffect BIT = 0;
	
	IF EXISTS
	(
		SELECT TOP (1)
		1
		FROM [dbo].[User] (NOLOCK)
		WHERE UserID = @pUserID
	)
	BEGIN
	    DELETE FROM [dbo].[User]
	    WHERE UserID = @pUserID
	    
	    IF @@ROWCOUNT > 0
	    BEGIN
	         SET @vResultID = 1;
			 SET @vMessages = N'Thành công';
	    END  
	END
	
	SELECT @vResultID AS ResultID, @vMessages AS [Message]
	
END
GO
