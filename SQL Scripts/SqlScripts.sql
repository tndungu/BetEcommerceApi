USE BetEcommerce
GO

/****** Object:  Table [dbo].[Cart]    Script Date: 2022/03/06 16:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 2022/03/06 16:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2022/03/06 16:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdersItem]    Script Date: 2022/03/06 16:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OrdersItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2022/03/06 16:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ImageId] [nvarchar](50) NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2022/03/06 16:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[PasswordHash] [varbinary](250) NOT NULL,
	[PasswordSalt] [varbinary](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220228172920_initial', N'6.0.2')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220302131004_TableRename', N'6.0.2')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (1, N'Converse All Star', N'0000002.JPG', CAST(625.80 AS Decimal(18, 2)), N'The classical sneaker!', CAST(N'2022-03-03T18:10:56.5666667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (2, N'SUPERSTAR SHOE', N'0000012.PNG', CAST(900.00 AS Decimal(18, 2)), N'THE STREETWEAR CLASSIC WITH THE SHELL TOE.', CAST(N'2022-03-03T18:10:56.6600000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (3, N'PlayStation 4', N'0000010.JPG', CAST(3500.00 AS Decimal(18, 2)), N'THE STREETWEAR CLASSIC WITH THE SHELL TOE.', CAST(N'2022-03-03T18:10:56.6633333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (4, N'WHITE SOCCER BALL', N'0000068.PNG', CAST(350.00 AS Decimal(18, 2)), N'TANGO WHITE SOCCER', CAST(N'2022-03-03T18:10:56.6666667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (5, N' YELLOW SOCCER BALL', N'0000065.PNG', CAST(330.00 AS Decimal(18, 2)), N'TANGO WHITE SOCCER', CAST(N'2022-03-03T18:10:56.6666667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (6, N'Green SOCCER BALL', N'0000067.PNG', CAST(312.00 AS Decimal(18, 2)), N'TANGO BLUE SOCCER', CAST(N'2022-03-03T18:10:56.6666667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (7, N'Adidas Blue PLAYING BALL', N'0000066.PNG', CAST(350.00 AS Decimal(18, 2)), N'TANGO BLUE SOCCER', CAST(N'2022-03-03T18:10:56.6833333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (8, N'Custom Flak Sunglasses', N'0000017.JPG', CAST(78.00 AS Decimal(18, 2)), N'Custom Flak Sunglasses', CAST(N'2022-03-03T18:10:56.6866667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (9, N'SUPERSTAR SHOE', N'0000002.JPG', CAST(625.80 AS Decimal(18, 2)), N'THE STREETWEAR CLASSIC WITH THE SHELL TOE.', CAST(N'2022-03-03T18:10:56.6900000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (10, N'Seiko Mechanical Automatic', N'0000054.JPG', CAST(625.80 AS Decimal(18, 2)), N'Seiko Mechanical Automatic SRPA49K1', CAST(N'2022-03-03T18:10:56.6900000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (11, N'Tissot T-Touch Expert Solar', N'0000018.PNG', CAST(625.80 AS Decimal(18, 2)), N'Tissot T-Touch Expert Solar', CAST(N'2022-03-03T18:10:56.6900000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (12, N'GBB Epic Golf Sub Zero Driver', N'0000058.JPG', CAST(985.80 AS Decimal(18, 2)), N'GBB Epic Golf Sub Zero Driver', CAST(N'2022-03-03T18:10:56.6900000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (13, N'Supreme Golfball', N'0000057.JPG', CAST(321.00 AS Decimal(18, 2)), N'Tissot T-Touch Expert Solar', CAST(N'2022-03-03T18:10:56.6933333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (14, N'KANUKA POINT JACKET M', N'0000026.JPG', CAST(1220.00 AS Decimal(18, 2)), N'KANUKA POINT JACKET M', CAST(N'2022-03-03T18:10:56.6933333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (15, N'Sleeveless shirt Meccanica', N'0000152.JPG', CAST(858.00 AS Decimal(18, 2)), N'Sleeveless shirt Meccanica', CAST(N'2022-03-03T18:10:56.6933333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (16, N'Le Corbusier LC 6 dining table (1929)', N'0000223.JPG', CAST(321.00 AS Decimal(18, 2)), N'Le Corbusier LC 6 dining table (1929)', CAST(N'2022-03-03T18:10:56.6933333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (17, N'Charles Eames Lounge Chair (1956)', N'0000234.JPG', CAST(321.00 AS Decimal(18, 2)), N'Charles Eames Lounge Chair (1956)', CAST(N'2022-03-03T18:10:56.6966667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (18, N'Isamu Noguchi couch table', N'0000230.JPG', CAST(321.00 AS Decimal(18, 2)), N'Isamu Noguchi couch table, Coffee Table (1945)', CAST(N'2022-03-03T18:10:56.6966667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (19, N'Ultimate Apple Pro Hipster Bundle', N'0000106.JPG', CAST(590.00 AS Decimal(18, 2)), N'Ultimate Apple Pro Hipster Bundle', CAST(N'2022-03-03T18:10:56.7000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (20, N'Fast Cars, Image Calendar 2013', N'0000240.JPG', CAST(5600.00 AS Decimal(18, 2)), N'Fast Cars, Image Calendar 2013', CAST(N'2022-03-03T18:10:56.7000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (21, N'iPhone 12 Pro Max', N'0000080.JPG', CAST(321.00 AS Decimal(18, 2)), N'iPhone 12 Pro Max', CAST(N'2022-03-03T18:10:56.7000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (22, N'Converse All Star', N'0000002.JPG', CAST(625.80 AS Decimal(18, 2)), N'The classical sneaker!', CAST(N'2022-03-03T18:25:21.5600000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (23, N'iPhone 12 Pro Max', N'0000080.JPG', CAST(321.00 AS Decimal(18, 2)), N'iPhone 12 Pro Max', CAST(N'2022-03-03T18:25:21.5800000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (24, N'Charles Eames Lounge Chair (1956)', N'0000234.JPG', CAST(321.00 AS Decimal(18, 2)), N'Charles Eames Lounge Chair (1956)', CAST(N'2022-03-03T18:25:21.5833333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (25, N'SUPERSTAR SHOE', N'0000012.PNG', CAST(900.00 AS Decimal(18, 2)), N'THE STREETWEAR CLASSIC WITH THE SHELL TOE.', CAST(N'2022-03-03T18:25:21.5900000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (26, N'PlayStation 4', N'0000010.JPG', CAST(3500.00 AS Decimal(18, 2)), N'THE STREETWEAR CLASSIC WITH THE SHELL TOE.', CAST(N'2022-03-03T18:25:21.5933333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (27, N'WHITE SOCCER BALL', N'0000068.PNG', CAST(350.00 AS Decimal(18, 2)), N'TANGO WHITE SOCCER', CAST(N'2022-03-03T18:25:21.6000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (28, N' YELLOW SOCCER BALL', N'0000065.PNG', CAST(330.00 AS Decimal(18, 2)), N'TANGO WHITE SOCCER', CAST(N'2022-03-03T18:25:21.6300000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (29, N'SUPERSTAR SHOE', N'0000002.JPG', CAST(625.80 AS Decimal(18, 2)), N'THE STREETWEAR CLASSIC WITH THE SHELL TOE.', CAST(N'2022-03-03T18:25:21.6300000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (30, N'Seiko Mechanical Automatic', N'0000054.JPG', CAST(625.80 AS Decimal(18, 2)), N'Seiko Mechanical Automatic SRPA49K1', CAST(N'2022-03-03T18:25:21.6333333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (31, N'Tissot T-Touch Expert Solar', N'0000018.PNG', CAST(625.80 AS Decimal(18, 2)), N'Tissot T-Touch Expert Solar', CAST(N'2022-03-03T18:25:21.6333333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (32, N'GBB Epic Golf Sub Zero Driver', N'0000058.JPG', CAST(985.80 AS Decimal(18, 2)), N'GBB Epic Golf Sub Zero Driver', CAST(N'2022-03-03T18:25:21.6333333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (33, N'Supreme Golfball', N'0000057.JPG', CAST(321.00 AS Decimal(18, 2)), N'Tissot T-Touch Expert Solar', CAST(N'2022-03-03T18:25:21.6366667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (34, N'Custom Flak Sunglasses', N'0000017.JPG', CAST(78.00 AS Decimal(18, 2)), N'Custom Flak Sunglasses', CAST(N'2022-03-03T18:25:21.6366667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (35, N'Sleeveless shirt Meccanica', N'0000152.JPG', CAST(858.00 AS Decimal(18, 2)), N'Sleeveless shirt Meccanica', CAST(N'2022-03-03T18:25:21.6366667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (36, N'Le Corbusier LC 6 dining table (1929)', N'0000223.JPG', CAST(321.00 AS Decimal(18, 2)), N'Le Corbusier LC 6 dining table (1929)', CAST(N'2022-03-03T18:25:21.6400000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (37, N'Isamu Noguchi couch table', N'0000230.JPG', CAST(321.00 AS Decimal(18, 2)), N'Isamu Noguchi couch table, Coffee Table (1945)', CAST(N'2022-03-03T18:25:21.6400000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (38, N'KANUKA POINT JACKET M', N'0000026.JPG', CAST(1220.00 AS Decimal(18, 2)), N'KANUKA POINT JACKET M', CAST(N'2022-03-03T18:25:21.6400000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (39, N'Green SOCCER BALL', N'0000067.PNG', CAST(312.00 AS Decimal(18, 2)), N'TANGO BLUE SOCCER', CAST(N'2022-03-03T18:25:21.6600000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (40, N'Adidas Blue PLAYING BALL', N'0000066.PNG', CAST(350.00 AS Decimal(18, 2)), N'TANGO BLUE SOCCER', CAST(N'2022-03-03T18:25:21.6600000' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (41, N'Ultimate Apple Pro Hipster Bundle', N'0000106.JPG', CAST(590.00 AS Decimal(18, 2)), N'Ultimate Apple Pro Hipster Bundle', CAST(N'2022-03-03T18:25:21.6633333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (42, N'Fast Cars, Image Calendar 2013', N'0000240.JPG', CAST(5600.00 AS Decimal(18, 2)), N'Fast Cars, Image Calendar 2013', CAST(N'2022-03-03T18:25:21.6666667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (43, N'Supreme Golfball', N'0000057.JPG', CAST(321.00 AS Decimal(18, 2)), N'Tissot T-Touch Expert Solar', CAST(N'2022-03-03T18:27:53.4733333' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (44, N'Custom Flak Sunglasses', N'0000017.JPG', CAST(78.00 AS Decimal(18, 2)), N'Custom Flak Sunglasses', CAST(N'2022-03-03T18:27:53.4866667' AS DateTime2))
GO
INSERT [dbo].[Products] ([Id], [Name], [ImageId], [price], [Description], [CreatedOn]) VALUES (45, N'Sleeveless shirt Meccanica', N'0000152.JPG', CAST(858.00 AS Decimal(18, 2)), N'Sleeveless shirt Meccanica', CAST(N'2022-03-03T18:27:53.4900000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Email], [PasswordHash], [PasswordSalt], [IsActive], [CreatedOn]) VALUES (1, N'antony@yahoo.com', 0xBF0E3D9801627C2B4642A1CA275AF53AD21C7C777F188139CD09C3D4CE53A655C67624D7EF3E98FF64DB8F8C61F4F2E830A03F2B197994EA389F16CBD952A2BD, 0x13D95394282D09C90621C2990B645FFFDB83A56DADE2D4378DF9CAF9ECA01BB1A78A6CCA1E67E8F170CE2E47B846C6A959935B2C3E43BAAB06A59B4CF5355062BF90F49104E51C805803D89CE2CDB1C91998038CEE515445D0C48E0942DEA81793EE4018D9BB7CBFD6A333FB705831FC99DA85FBF8B174A9839BA6F8E266265E, 1, CAST(N'2022-02-28T19:49:42.5026501' AS DateTime2))
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_CartItem_CartId]    Script Date: 2022/03/06 16:46:50 ******/
CREATE NONCLUSTERED INDEX [IX_CartItem_CartId] ON [dbo].[CartItem]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrdersItem_OrderId]    Script Date: 2022/03/06 16:46:50 ******/
CREATE NONCLUSTERED INDEX [IX_OrdersItem_OrderId] ON [dbo].[OrdersItem]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Cart_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Cart] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Cart_CartId]
GO
ALTER TABLE [dbo].[OrdersItem]  WITH CHECK ADD  CONSTRAINT [FK_OrdersItem_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrdersItem] CHECK CONSTRAINT [FK_OrdersItem_Orders_OrderId]
GO
USE [master]
GO
ALTER DATABASE [BetEcommerce] SET  READ_WRITE 
GO
