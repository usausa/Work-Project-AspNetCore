CREATE TABLE Item (
    Id bigint NOT NULL,
    Name nvarchar(16) NOT NULL,
	ItemType int NOT NULL,
    CreatedAt datetime2(7) NOT NULL,
    CONSTRAINT PK_Item PRIMARY KEY CLUSTERED (Id ASC)
)

GO
