USE [master]
GO

if exists (select * from sys.databases where name = N'GuildCars')
begin
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'GuildCars';
	ALTER DATABASE GuildCars SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE GuildCars;
end
go

CREATE DATABASE GuildCars
GO

USE GuildCars
GO

CREATE TABLE [dbo].[Contact](
	[ContactId] [int] identity(0,1) NOT NULL,
	[ContactName] [varchar](50) NOT NULL,
	[ContactEmail] [varchar](50) NOT NULL,
	[ContactPhone] [varchar](50) NOT NULL,
	[ContactMessage] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Make](
	[MakeId] [int] identity(0,1) NOT NULL,
	[UserId] [varchar] (128) NOT NULL,
	[MakeName] [varchar](50) NOT NULL,
	[MakeDate] [date] NOT NULL,
 CONSTRAINT [PK_Make] PRIMARY KEY CLUSTERED 
(
	[MakeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Model](
	[ModelId] [int] identity(0,1) NOT NULL,
	[MakeId] [int] NOT NULL,
	[UserId] [varchar] (128) NOT NULL,
	[ModelDate] [date] NOT NULL,
	[ModelName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[ModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SaleInfo](
	[SaleId] [int] identity(0,1) NOT NULL,
	[UserId] [varchar] (128) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[SaleDate] [date] NOT NULL,
	[SaleName] [varchar](50) NOT NULL,
	[SalePhone] [varchar](50) NOT NULL,
	[SaleEmail] [varchar](50) NOT NULL,
	[SaleStreet1] [varchar](50) NOT NULL,
	[SaleStreet2] [varchar](50) NULL,
	[SaleCity] [varchar](50) NOT NULL,
	[SaleState] [nchar](10) NOT NULL,
	[SaleZip] [numeric](5, 0) NOT NULL,
	[SalePrice] [money] NOT NULL,
	[SaleType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SaleInfo] PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Special](
	[SpecialId] [int] identity(0,1) NOT NULL,
	[SpecialTitle] [varchar](50) NOT NULL,
	[SpecialNote] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Special] PRIMARY KEY CLUSTERED 
(
	[SpecialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Vehicle](
	[VehicleId] [int] identity(0,1) NOT NULL,
	[ModelId] [int] NOT NULL,
	[MakeId] [int] NOT NULL,
	[Year] [int] not null,
	[Description] [varchar](max) not null,
	[Type] [varchar](50) not null,
	[IsFeatured] [bit] NOT NULL,
	[BodyStyle] [varchar](50) NOT NULL,
	[Transmission] [varchar](50) NOT NULL,
	[Color] [varchar](50) NOT NULL,
	[Interior] [varchar](50) NOT NULL,
	[Mileage] [numeric](18, 0) NOT NULL,
	[VIN] [varchar](50) NOT NULL,
	[SalePrice] [money] NOT NULL,
	[MSRP] [money] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Model]  WITH CHECK ADD  CONSTRAINT [FK_Model_Make] FOREIGN KEY([MakeId])
REFERENCES [dbo].[Make] ([MakeId])
GO
ALTER TABLE [dbo].[Model] CHECK CONSTRAINT [FK_Model_Make]
GO
ALTER TABLE [dbo].[SaleInfo]  WITH CHECK ADD  CONSTRAINT [FK_SaleInfo_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([VehicleId])
GO
ALTER TABLE [dbo].[SaleInfo] CHECK CONSTRAINT [FK_SaleInfo_Vehicle]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Model] FOREIGN KEY([ModelId])
REFERENCES [dbo].[Model] ([ModelId])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_Model]
GO

ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Make] FOREIGN KEY([MakeId])
REFERENCES [dbo].[Make] ([MakeId])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_Make]
GO

