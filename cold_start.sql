USE [CarRental]
GO
/****** Object:  Schema [rental]    Script Date: 12/3/2024 9:21:41 PM ******/
CREATE SCHEMA [rental]
GO
/****** Object:  Table [rental].[BasePrice]    Script Date: 12/3/2024 9:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rental].[BasePrice](
	[idBasePrice] [int] IDENTITY(1,1) NOT NULL,
	[PriceName] [nvarchar](100) NOT NULL,
	[Amount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBasePrice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rental].[Customer]    Script Date: 12/3/2024 9:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rental].[Customer](
	[idCustomer] [int] IDENTITY(1,1) NOT NULL,
	[SSN] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[idCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rental].[Rental]    Script Date: 12/3/2024 9:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rental].[Rental](
	[idRental] [int] IDENTITY(1,1) NOT NULL,
	[fkCustomer] [int] NOT NULL,
	[fkVehicle] [int] NOT NULL,
	[BookingNumber] [nvarchar](10) NOT NULL,
	[PickupDateTime] [datetime] NOT NULL,
	[ReturnDateTime] [datetime] NULL,
	[PickupMeeterKm] [int] NOT NULL,
	[ReturnMeterKm] [int] NULL,
	[KmPrice] [decimal](10, 2) NOT NULL,
	[DayPrice] [decimal](10, 2) NOT NULL,
	[RentalPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idRental] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rental].[Vehicle]    Script Date: 12/3/2024 9:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rental].[Vehicle](
	[idVehicle] [int] IDENTITY(1,1) NOT NULL,
	[fkVehicleType] [int] NULL,
	[RegistrationNumber] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idVehicle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rental].[VehicleType]    Script Date: 12/3/2024 9:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rental].[VehicleType](
	[idVehicleType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[KmFactor] [decimal](10, 2) NOT NULL,
	[DayFactor] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idVehicleType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [rental].[BasePrice] ON 

INSERT [rental].[BasePrice] ([idBasePrice], [PriceName], [Amount]) VALUES (1, N'BasePriceKm', CAST(10.00 AS Decimal(10, 2)))
INSERT [rental].[BasePrice] ([idBasePrice], [PriceName], [Amount]) VALUES (2, N'BasePriceDay', CAST(200.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [rental].[BasePrice] OFF
GO
SET IDENTITY_INSERT [rental].[Vehicle] ON 

INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (2, 1, N'XYZ 111 ')
INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (3, 2, N'XYZ 222')
INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (4, 3, N'XYZ 333 ')
INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (5, 1, N'XYZ 444 ')
INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (6, 2, N'XYZ 555 ')
INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (7, 3, N'XYZ 666')
INSERT [rental].[Vehicle] ([idVehicle], [fkVehicleType], [RegistrationNumber]) VALUES (8, 1, N'XYZ 777 ')
SET IDENTITY_INSERT [rental].[Vehicle] OFF
GO
SET IDENTITY_INSERT [rental].[VehicleType] ON 

INSERT [rental].[VehicleType] ([idVehicleType], [Name], [KmFactor], [DayFactor]) VALUES (1, N'Small car', CAST(0.00 AS Decimal(10, 2)), CAST(1.00 AS Decimal(10, 2)))
INSERT [rental].[VehicleType] ([idVehicleType], [Name], [KmFactor], [DayFactor]) VALUES (2, N'Combi', CAST(1.00 AS Decimal(10, 2)), CAST(1.30 AS Decimal(10, 2)))
INSERT [rental].[VehicleType] ([idVehicleType], [Name], [KmFactor], [DayFactor]) VALUES (3, N'Truck', CAST(1.50 AS Decimal(10, 2)), CAST(1.50 AS Decimal(10, 2)))
SET IDENTITY_INSERT [rental].[VehicleType] OFF
GO
/****** Object:  Index [idBasePrice_UNIQUE]    Script Date: 12/3/2024 9:21:41 PM ******/
ALTER TABLE [rental].[BasePrice] ADD  CONSTRAINT [idBasePrice_UNIQUE] UNIQUE NONCLUSTERED 
(
	[idBasePrice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idCustomer_UNIQUE]    Script Date: 12/3/2024 9:21:41 PM ******/
ALTER TABLE [rental].[Customer] ADD  CONSTRAINT [idCustomer_UNIQUE] UNIQUE NONCLUSTERED 
(
	[idCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Rental__AAC320BF99EC189F]    Script Date: 12/3/2024 9:21:41 PM ******/
ALTER TABLE [rental].[Rental] ADD UNIQUE NONCLUSTERED 
(
	[BookingNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehicle__E8864602019C889B]    Script Date: 12/3/2024 9:21:41 PM ******/
ALTER TABLE [rental].[Vehicle] ADD UNIQUE NONCLUSTERED 
(
	[RegistrationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idCarType_UNIQUE]    Script Date: 12/3/2024 9:21:41 PM ******/
ALTER TABLE [rental].[VehicleType] ADD  CONSTRAINT [idCarType_UNIQUE] UNIQUE NONCLUSTERED 
(
	[idVehicleType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [rental].[Rental]  WITH CHECK ADD  CONSTRAINT [rental_customer] FOREIGN KEY([fkCustomer])
REFERENCES [rental].[Customer] ([idCustomer])
GO
ALTER TABLE [rental].[Rental] CHECK CONSTRAINT [rental_customer]
GO
ALTER TABLE [rental].[Rental]  WITH CHECK ADD  CONSTRAINT [rental_vehicle] FOREIGN KEY([fkVehicle])
REFERENCES [rental].[Vehicle] ([idVehicle])
GO
ALTER TABLE [rental].[Rental] CHECK CONSTRAINT [rental_vehicle]
GO
ALTER TABLE [rental].[Vehicle]  WITH CHECK ADD  CONSTRAINT [vehicle_vehicletype] FOREIGN KEY([fkVehicleType])
REFERENCES [rental].[VehicleType] ([idVehicleType])
GO
ALTER TABLE [rental].[Vehicle] CHECK CONSTRAINT [vehicle_vehicletype]
GO
