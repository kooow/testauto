use master;
go

CREATE DATABASE TestAutoDb;
go


use TestAutoDb;
go

-- telehely tábla létrehozása

CREATE TABLE [dbo].[Site]
(
	[SiteId] INT IDENTITY(1,1) NOT NULL, 
    [City] NVARCHAR(MAX) NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL,  
	[PostCode] INT NOT NULL,
    [ParkingSpace] INT NOT NULL,

CONSTRAINT [PK_dbo.Site] PRIMARY KEY CLUSTERED 
(
    [SiteId] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


-- autó tábla létrehozása

CREATE TABLE [dbo].[Car]
(
	[CarId] INT IDENTITY(1,1) NOT NULL, 
	[Manufacturer] NVARCHAR(MAX) NULL, 
    [Type] NVARCHAR(MAX) NOT NULL, 
	[Year] int NOT NULL,
	[ProductionDate] datetime NOT NULL,
	[Condition]  NVARCHAR(MAX) NULL,
	[Owners] int NULL,
	[SiteId] int NOT NULL FOREIGN KEY (SiteId) REFERENCES Site(SiteId),

CONSTRAINT [PK_dbo.Car] PRIMARY KEY CLUSTERED 
(
    [CarId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

go



