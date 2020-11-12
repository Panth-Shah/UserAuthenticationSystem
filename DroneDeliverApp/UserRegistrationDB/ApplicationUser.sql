CREATE TABLE [dbo].[ApplicationUser]
(
	[ApplicationUserId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserFirstName] NVARCHAR(256) NOT NULL, 
    [UserFamilyName] NVARCHAR(256) NOT NULL, 
    [EmailID] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Address1] NVARCHAR(256) NOT NULL, 
    [Address2] NVARCHAR(50) NULL, 
    [Address3] NVARCHAR(50) NULL, 
    [City] NVARCHAR(256) NOT NULL, 
    [ZipCode] CHAR(10) NOT NULL, 
    [State] NVARCHAR(10) NOT NULL, 
    [CreateDate] DATETIME2 NULL DEFAULT GETDATE(), 
    [LastUpdate] DATETIME2 NULL DEFAULT GETDATE()
)
