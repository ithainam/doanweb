USE [master]
GO
/****** Object:  Database [WebShop]    Script Date: 5/4/2021 10:14:20 AM ******/
CREATE DATABASE [WebShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WebShop.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WebShop_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WebShop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebShop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WebShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebShop] SET  MULTI_USER 
GO
ALTER DATABASE [WebShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [WebShop]
GO
/****** Object:  StoredProcedure [dbo].[Sp_Account_Login]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sp_Account_Login] @UserName varchar(50), @Password varchar(50) 
as
begin
	declare @count int
	declare @res bit

	select @count = count (*)
	from UserAcc where UserName = @UserName and Password = @Password

	if @count > 0
	   set @res = 1
	else 
		set @res = 0
	select @res
end
GO
/****** Object:  Table [dbo].[Account]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_Feedback_CreateDate]  DEFAULT (getdate()),
	[Status] [bit] NULL,
	[Content] [nvarchar](250) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](50) NULL,
	[Link] [nvarchar](250) NULL,
	[DispkayOrder] [int] NULL,
	[Target] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[TypeID] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuType]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuType](
	[MenuTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_MenuType] PRIMARY KEY CLUSTERED 
(
	[MenuTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Images] [nvarchar](250) NULL,
	[MoreImage] [xml] NULL,
	[CategoryID] [bigint] NULL,
	[Detail] [ntext] NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](100) NULL,
	[MetaDescriptions] [nchar](100) NULL,
	[Status] [bit] NULL,
	[TopHot] [datetime] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsCategory]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsCategory](
	[NewCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[MetaTitle] [varchar](100) NULL,
	[DisplayOrder] [int] NULL,
	[SeoTitle] [nvarchar](100) NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](100) NULL,
	[MetaDescriptions] [nchar](100) NULL,
	[Status] [bit] NULL,
	[ShownOnHome] [bit] NULL,
 CONSTRAINT [PK_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[NewCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_Order_CreateDate]  DEFAULT (getdate()),
	[CustomerID] [bigint] NULL,
	[ShipName] [nvarchar](250) NULL,
	[ShipPhone] [nchar](10) NULL,
	[ShipAddress] [nvarchar](250) NULL,
	[Status] [int] NULL,
	[ShipEmail] [nvarchar](250) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [bigint] NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[Quantity] [int] NULL CONSTRAINT [DF_OrderDetail_Quantity]  DEFAULT ((1)),
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [varchar](50) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Images] [nvarchar](250) NULL,
	[MoreImage] [xml] NULL,
	[Price] [decimal](18, 0) NULL CONSTRAINT [DF_Table1_Price]  DEFAULT ((0)),
	[PromotionPrice] [decimal](18, 0) NULL,
	[Quantity] [int] NULL CONSTRAINT [DF_Table1_Quantity]  DEFAULT ((0)),
	[CategoryID] [bigint] NULL,
	[Detail] [ntext] NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_Table1_CreateDate]  DEFAULT (getdate()),
	[CreateBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](100) NULL,
	[MetaDescriptions] [nchar](100) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Table1_Status]  DEFAULT ((1)),
	[TopHot] [datetime] NULL,
	[Image2] [varchar](250) NULL,
	[Image3] [varchar](250) NULL,
 CONSTRAINT [PK_Table1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[CategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[MetaTitle] [varchar](100) NULL,
	[DisplayOrder] [int] NULL CONSTRAINT [DF_ProductCategory_DisplayOrder]  DEFAULT ((0)),
	[SeoTitle] [nvarchar](100) NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_ProductCategory_CreateDate]  DEFAULT (getdate()),
	[CreateBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](100) NULL,
	[MetaDescriptions] [nchar](100) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_ProductCategory_Status]  DEFAULT ((1)),
	[ShownOnHome] [bit] NULL CONSTRAINT [DF_ProductCategory_ShownOnHome]  DEFAULT ((0)),
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemConfig]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemConfig](
	[SysID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Value] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED 
(
	[SysID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAcc]    Script Date: 5/4/2021 10:14:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAcc](
	[UserID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nchar](10) NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_User_CreateDate]  DEFAULT (getdate()),
	[CreateBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Account] ([Username], [Password]) VALUES (N'Admin', N'Admin123')
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([FeedbackId], [Name], [Phone], [Email], [Address], [CreateDate], [Status], [Content]) VALUES (1, N'Pham Thanh', N'0346896303', N'phamtrungthanh30@gmail.com', N'Ha Noi', CAST(N'2021-05-04 10:06:10.897' AS DateTime), NULL, N'111
                        ')
SET IDENTITY_INSERT [dbo].[Feedback] OFF
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [CreateDate], [CustomerID], [ShipName], [ShipPhone], [ShipAddress], [Status], [ShipEmail]) VALUES (1, CAST(N'2021-05-03 10:55:59.670' AS DateTime), NULL, N'Phạm Trung Thành', N'0346896303', N'ThaiBinh', NULL, NULL)
INSERT [dbo].[Order] ([OrderID], [CreateDate], [CustomerID], [ShipName], [ShipPhone], [ShipAddress], [Status], [ShipEmail]) VALUES (2, CAST(N'2021-05-03 10:57:52.300' AS DateTime), NULL, N'Phạm Trung Thành', N'0346896303', N'HaNoi', NULL, NULL)
INSERT [dbo].[Order] ([OrderID], [CreateDate], [CustomerID], [ShipName], [ShipPhone], [ShipAddress], [Status], [ShipEmail]) VALUES (3, CAST(N'2021-05-04 09:15:30.317' AS DateTime), NULL, N'Phạm Trung Thành', N'0346896303', N'Ha Noi', NULL, N'phamtrungthanh30@gmail.com')
SET IDENTITY_INSERT [dbo].[Order] OFF
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [Quantity], [Price]) VALUES (1, 3, 1, CAST(668000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [Quantity], [Price]) VALUES (3, 1, 1, CAST(668000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [Quantity], [Price]) VALUES (3, 2, 2, CAST(668000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [Quantity], [Price]) VALUES (4, 2, 1, CAST(668000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Images], [MoreImage], [Price], [PromotionPrice], [Quantity], [CategoryID], [Detail], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [Image2], [Image3]) VALUES (1, N'Váy áo đôi VXH-067', N'VXH-067', NULL, NULL, N'VayAoDoiVXH-067.jpg', NULL, CAST(668000 AS Decimal(18, 0)), NULL, 100, 1, NULL, CAST(N'2021-04-27 00:21:15.400' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2021-08-08 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Images], [MoreImage], [Price], [PromotionPrice], [Quantity], [CategoryID], [Detail], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [Image2], [Image3]) VALUES (2, N'Váy áo đôi VXH-070', N'VXH-070', NULL, NULL, N'VayAoDoiVXH-070.jpg', NULL, CAST(668000 AS Decimal(18, 0)), NULL, 100, 1, NULL, CAST(N'2021-04-27 00:23:32.523' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2021-08-08 00:00:00.000' AS DateTime), N'VayAoDoiVXH-070-1.jpg', N'VayAoDoiVXH-070.jpg')
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Images], [MoreImage], [Price], [PromotionPrice], [Quantity], [CategoryID], [Detail], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [Image2], [Image3]) VALUES (3, N'Váy áo đôi VXH-071', N'VXH-071', NULL, NULL, N'VayAoDoiVXH-071.jpg', NULL, CAST(668000 AS Decimal(18, 0)), NULL, 100, 2, NULL, CAST(N'2021-04-27 00:25:50.767' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2021-08-08 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Images], [MoreImage], [Price], [PromotionPrice], [Quantity], [CategoryID], [Detail], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [Image2], [Image3]) VALUES (4, N'Váy Áo Đôi VXH-072', N'VXH-072', NULL, NULL, N'VayAoDoiVXH-072.jpg', NULL, CAST(668000 AS Decimal(18, 0)), NULL, 0, 2, NULL, CAST(N'2021-04-27 00:26:38.527' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2021-08-08 00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([CategoryID], [Name], [MetaTitle], [DisplayOrder], [SeoTitle], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShownOnHome]) VALUES (1, N'Váy áo đôi', N'vay-ao-doi', 0, NULL, CAST(N'2021-04-28 09:01:28.770' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[ProductCategory] ([CategoryID], [Name], [MetaTitle], [DisplayOrder], [SeoTitle], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShownOnHome]) VALUES (2, N'Áo đôi', N'ao-doi', 0, NULL, CAST(N'2021-04-28 09:01:57.550' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[ProductCategory] ([CategoryID], [Name], [MetaTitle], [DisplayOrder], [SeoTitle], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShownOnHome]) VALUES (3, N'Đồ đi biển', N'do-di-bien', 0, NULL, CAST(N'2021-04-28 09:02:10.390' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[ProductCategory] ([CategoryID], [Name], [MetaTitle], [DisplayOrder], [SeoTitle], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShownOnHome]) VALUES (4, N'Bộ đồ ngủ', N'bo-do-ngu', 0, NULL, CAST(N'2021-04-28 09:02:23.327' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[ProductCategory] ([CategoryID], [Name], [MetaTitle], [DisplayOrder], [SeoTitle], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShownOnHome]) VALUES (5, N'Sản phẩm khác', N'san-pham-khac', 0, NULL, CAST(N'2021-04-28 09:02:37.543' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
SET IDENTITY_INSERT [dbo].[UserAcc] ON 

INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (1, N'Admin', N'202cb962ac59075b964b07152d234b70', N'Thanh', NULL, NULL, NULL, CAST(N'2021-04-07 23:35:08.093' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (3, N'Admin2', N'202cb962ac59075b964b07152d234b70', N'Pham Trung Thanh', N'aaaa', N'phamtrungthanh301@gmail.com', N'12345678  ', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (10003, N'Admin3', N'c8f3c7fc80cf9be66ea3bdf64ba1c82d', N'TrungThanh', NULL, N'phamtrungthanh301@gmail.com', N'12345678  ', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (10004, N'Admin4', N'202cb962ac59075b964b07152d234b70', N'Thanh', N'Thái Bình', N'phamtrungthanh301@gmail.com', N'0346896303', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (10005, N'Admin5', N'202cb962ac59075b964b07152d234b70', N'Pham Trung Thanh', N'Thái Bình', N'phamtrungthanh3012@gmail.com', N'0346896303', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (20003, N'Admin6', N'e10adc3949ba59abbe56e057f20f883e', N'TrungThanh', N'ThuyPhong', N'phamtrungthanh301@gmail.com', N'12345678  ', CAST(N'2021-05-04 01:34:10.487' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[UserAcc] ([UserID], [UserName], [Password], [Name], [Address], [Email], [Phone], [CreateDate], [CreateBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (20004, N'Admin7', N'e10adc3949ba59abbe56e057f20f883e', N'TrungThanh', N'ThaiBinh', N'phamtrungthanh301@gmail.com', N'12345678  ', CAST(N'2021-05-04 01:46:48.550' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserAcc] OFF
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[NewsCategory] ADD  CONSTRAINT [DF_NewsCategory_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[NewsCategory] ADD  CONSTRAINT [DF_NewsCategory_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[NewsCategory] ADD  CONSTRAINT [DF_NewsCategory_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[NewsCategory] ADD  CONSTRAINT [DF_NewsCategory_ShownOnHome]  DEFAULT ((0)) FOR [ShownOnHome]
GO
USE [master]
GO
ALTER DATABASE [WebShop] SET  READ_WRITE 
GO
