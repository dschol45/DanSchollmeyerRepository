USE [Master]
GO
/****** Object:  Table [dbo].[AddOn]    Script Date: 2/25/2019 10:18:07 AM ******/

if exists (select * from sys.databases where name = N'Hotel')
begin
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Hotel';
	ALTER DATABASE Hotel SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE Hotel;
end
go

create database Hotel
go

USE [Hotel]
GO
/****** Object:  Table [dbo].[AddOn]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddOn](
	[AddOnID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Category] [nchar](20) NOT NULL,
 CONSTRAINT [PK__AddOns__682701A48AC08EFC] PRIMARY KEY CLUSTERED 
(
	[AddOnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Amenity]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenity](
	[AmenityID] [int] IDENTITY(1,1) NOT NULL,
	[AmenityName] [nvarchar](25) NOT NULL,
	[AmenityPrice] [money] NULL,
 CONSTRAINT [PK__Amenity__842AF52BFC50BDD4] PRIMARY KEY CLUSTERED 
(
	[AmenityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guest]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guest](
	[GuestID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[Age] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1000,1) NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[InvoiceStatus] [nchar](10) NOT NULL,
	[ReservationID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promo]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promo](
	[PromoID] [int] IDENTITY(1,1) NOT NULL,
	[PromoCode] [nchar](15) NOT NULL,
	[PromoPercent] [decimal](6, 2) NULL,
	[PromoAmount] [money] NULL,
	[PromoExpiration] [date] NULL,
 CONSTRAINT [PK__Promo__33D334D00567E77C] PRIMARY KEY CLUSTERED 
(
	[PromoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rate]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rate](
	[RateID] [int] IDENTITY(1,1) NOT NULL,
	[RateName] [nchar](10) NULL,
	[Rate] [money] NULL,
 CONSTRAINT [PK_Rate] PRIMARY KEY CLUSTERED 
(
	[RateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 2/25/2019 2:39:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[ReservationID] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[PromoID] [int] NULL,
 CONSTRAINT [PK__Reservat__B7EE5F0431DC5E96] PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationGuest]    Script Date: 2/25/2019 2:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationGuest](
	[GuestID] [int] NULL,
	[ReservationID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationPromo]    Script Date: 2/25/2019 2:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationPromo](
	[ReservationID] [int] NOT NULL,
	[PromoID] [int] NOT NULL,
 CONSTRAINT [PK_ReservationPromo] PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC,
	[PromoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationRoom]    Script Date: 2/25/2019 2:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationRoom](
	[ReservationID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
 CONSTRAINT [PK_ReservationRoom] PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC,
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationRoomAddOn]    Script Date: 2/25/2019 2:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationRoomAddOn](
	[RoomID] [int] NOT NULL,
	[AddOnID] [int] NOT NULL,
	[ReservationID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 2/25/2019 2:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[Floor] [int] NOT NULL,
	[Limit] [int] NOT NULL,
	[RateID] [int] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomAmenity]    Script Date: 2/25/2019 2:39:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomAmenity](
	[RoomID] [int] NULL,
	[AmenityID] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Reservation] FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Reservation]
GO
ALTER TABLE [dbo].[ReservationGuest]  WITH CHECK ADD  CONSTRAINT [FK_ReservationGuest_Guest] FOREIGN KEY([GuestID])
REFERENCES [dbo].[Guest] ([GuestID])
GO
ALTER TABLE [dbo].[ReservationGuest] CHECK CONSTRAINT [FK_ReservationGuest_Guest]
GO
ALTER TABLE [dbo].[ReservationGuest]  WITH CHECK ADD  CONSTRAINT [FK_ReservationGuest_Reservation] FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[ReservationGuest] CHECK CONSTRAINT [FK_ReservationGuest_Reservation]
GO
ALTER TABLE [dbo].[ReservationPromo]  WITH CHECK ADD  CONSTRAINT [FK_ReservationPromo_Promo] FOREIGN KEY([PromoID])
REFERENCES [dbo].[Promo] ([PromoID])
GO
ALTER TABLE [dbo].[ReservationPromo] CHECK CONSTRAINT [FK_ReservationPromo_Promo]
GO
ALTER TABLE [dbo].[ReservationPromo]  WITH CHECK ADD  CONSTRAINT [FK_ReservationPromo_Reservation] FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[ReservationPromo] CHECK CONSTRAINT [FK_ReservationPromo_Reservation]
GO
ALTER TABLE [dbo].[ReservationRoom]  WITH CHECK ADD  CONSTRAINT [FK_ReservationRoom_Reservation] FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[ReservationRoom] CHECK CONSTRAINT [FK_ReservationRoom_Reservation]
GO
ALTER TABLE [dbo].[ReservationRoom]  WITH CHECK ADD  CONSTRAINT [FK_ReservationRoom_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([RoomID])
GO
ALTER TABLE [dbo].[ReservationRoom] CHECK CONSTRAINT [FK_ReservationRoom_Room]
GO
ALTER TABLE [dbo].[ReservationRoomAddOn]  WITH CHECK ADD  CONSTRAINT [FK_ReservationRoomAddOn_AddOn] FOREIGN KEY([AddOnID])
REFERENCES [dbo].[AddOn] ([AddOnID])
GO
ALTER TABLE [dbo].[ReservationRoomAddOn] CHECK CONSTRAINT [FK_ReservationRoomAddOn_AddOn]
GO
ALTER TABLE [dbo].[ReservationRoomAddOn]  WITH CHECK ADD  CONSTRAINT [FK_ReservationRoomAddOn_ReservationRoom] FOREIGN KEY([ReservationID], [RoomID])
REFERENCES [dbo].[ReservationRoom] ([ReservationID], [RoomID])
GO
ALTER TABLE [dbo].[ReservationRoomAddOn] CHECK CONSTRAINT [FK_ReservationRoomAddOn_ReservationRoom]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Rate] FOREIGN KEY([RateID])
REFERENCES [dbo].[Rate] ([RateID])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Rate]
GO
ALTER TABLE [dbo].[RoomAmenity]  WITH CHECK ADD  CONSTRAINT [FK_RoomAmenity_Amenity] FOREIGN KEY([AmenityID])
REFERENCES [dbo].[Amenity] ([AmenityID])
GO
ALTER TABLE [dbo].[RoomAmenity] CHECK CONSTRAINT [FK_RoomAmenity_Amenity]
GO
ALTER TABLE [dbo].[RoomAmenity]  WITH CHECK ADD  CONSTRAINT [FK_RoomAmenity_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([RoomID])
GO
ALTER TABLE [dbo].[RoomAmenity] CHECK CONSTRAINT [FK_RoomAmenity_Room]
GO
