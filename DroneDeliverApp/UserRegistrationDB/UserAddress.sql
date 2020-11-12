CREATE TABLE [dbo].[UserAddress]
(
	[AddressId] TINYINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ApplicationUserId] TINYINT NOT NULL, 
    [Address1] NVARCHAR(256) NOT NULL, 
    [Address2] NVARCHAR(50) NULL, 
    [Address3] NVARCHAR(50) NULL, 
    [City] NVARCHAR(256) NOT NULL, 
    [ZipCode] CHAR(10) NOT NULL, 
    [State] NVARCHAR(10) NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [LastUpdate] DATETIME NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_UserAddress_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId])
)
