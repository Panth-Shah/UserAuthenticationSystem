CREATE TABLE [dbo].[ApplicationUser]
(
	[ApplicationUserId] TINYINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserFirstName] NVARCHAR(256) NOT NULL, 
    [UserFamilyName] NVARCHAR(256) NOT NULL, 
    [EmailID] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL
)
