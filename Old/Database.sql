CREATE TABLE Account (
    Id nchar(16) NOT NULL,
    Name nvarchar(32) NOT NULL,
    PasswordHash nvarchar(MAX) NOT NULL,
    IsAdmin bit NOT NULL,
    CONSTRAINT PK_Account PRIMARY KEY CLUSTERED (Id ASC)
)

GO


CREATE TABLE Item (
    Code varchar(13) NOT NULL,
    Name nvarchar(20) NOT NULL,
    Price money NOT NULL,
    UpdatedAt datetime2(7) NOT NULL,
    CONSTRAINT PK_Item PRIMARY KEY CLUSTERED (Code ASC)
)

GO


CREATE TABLE Event (
    Id BIGINT IDENTITY(1,1) NOT NULL,
    EventAt datetimeoffset(7) NOT NULL,
    EventType int NOT NULL,
    Detail nvarchar(MAX) NOT NULL
)

GO

CREATE CLUSTERED INDEX IX_Event_1 ON Event (
    EventAt DESC
)

GO


CREATE VIEW EventSummary
AS
SELECT
    (SELECT COUNT(*) FROM Event Where EventType = 0) AS InformationCount,
    (SELECT COUNT(*) FROM Event Where EventType = 1) AS WarningCount,
    (SELECT COUNT(*) FROM Event Where EventType = 2) AS ErrorCount

GO
