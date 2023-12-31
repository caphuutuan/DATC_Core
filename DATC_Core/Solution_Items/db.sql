USE [DATC_Core_Mine]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/19/2023 6:37:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [varchar](12) NULL,
	[Email] [nvarchar](50) NULL,
	[Salt] [nchar](6) NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](150) NULL,
	[Active] [bit] NOT NULL,
	[RoleID] [int] NULL,
	[LastLogin] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoryies]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoryies](
	[CateID] [int] IDENTITY(1,1) NOT NULL,
	[CateName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[ParentID] [int] NULL,
	[Levels] [int] NULL,
	[Ordering] [int] NULL,
	[Published] [bit] NULL,
	[Thumb] [nvarchar](250) NULL,
	[Title] [nvarchar](250) NULL,
	[Alias] [nvarchar](250) NULL,
	[MetaDesc] [nvarchar](250) NULL,
	[MetaKey] [nvarchar](250) NULL,
	[Cover] [nvarchar](250) NULL,
	[SchemaMarkup] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categoryies] PRIMARY KEY CLUSTERED 
(
	[CateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[DOB] [date] NULL,
	[Avatar] [nvarchar](max) NULL,
	[Address] [nvarchar](300) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [varchar](12) NULL,
	[LocationID] [int] NULL,
	[District] [int] NULL,
	[Ward] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Password] [nvarchar](50) NULL,
	[Salt] [nchar](8) NULL,
	[LastLogin] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Type] [nvarchar](20) NULL,
	[Slug] [nvarchar](100) NULL,
	[NameWithType] [nvarchar](300) NULL,
	[PayWithType] [nvarchar](300) NULL,
	[ParentCode] [int] NULL,
	[Levels] [int] NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[OrderNumber] [int] NULL,
	[Quantity] [int] NULL,
	[Discount] [int] NULL,
	[Total] [int] NULL,
	[ShipDate] [datetime] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[TransactStatusID] [int] NULL,
	[Deleted] [bit] NULL,
	[Paid] [bit] NULL,
	[PaymentID] [int] NULL,
	[Note] [nvarchar](300) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[PageID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](300) NOT NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](300) NULL,
	[Published] [bit] NULL,
	[Title] [nvarchar](300) NULL,
	[MetaDesc] [nvarchar](300) NULL,
	[MetaKey] [nvarchar](300) NULL,
	[Alias] [nvarchar](300) NULL,
	[CreatedAt] [datetime] NULL,
	[Ordering] [int] NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Image] [nvarchar](300) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostCategorys]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategorys](
	[CateID] [int] IDENTITY(1,1) NOT NULL,
	[CateName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[ParentID] [int] NULL,
	[Levels] [int] NULL,
	[Ordering] [int] NULL,
	[Published] [bit] NULL,
	[Cover] [nvarchar](300) NULL,
 CONSTRAINT [PK_PostCategorys] PRIMARY KEY CLUSTERED 
(
	[CateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](300) NOT NULL,
	[SContents] [nvarchar](300) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](300) NULL,
	[Published] [bit] NOT NULL,
	[Alias] [nvarchar](300) NULL,
	[CreateDate] [datetime] NULL,
	[Author] [nvarchar](300) NULL,
	[AccountID] [int] NULL,
	[Tags] [nvarchar](max) NULL,
	[CateID] [int] NULL,
	[IsHot] [bit] NULL,
	[IsNew] [bit] NULL,
	[MetaDesc] [nvarchar](300) NULL,
	[MetaKey] [nvarchar](300) NULL,
	[Views] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](300) NOT NULL,
	[ShortDesc] [nvarchar](300) NULL,
	[Description] [nvarchar](max) NULL,
	[CateID] [int] NULL,
	[Price] [int] NULL,
	[Discount] [int] NULL,
	[Thumb] [nvarchar](max) NULL,
	[Video] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[BestSeller] [bit] NULL,
	[IsHome] [bit] NULL,
	[Active] [bit] NOT NULL,
	[Title] [nvarchar](300) NULL,
	[Tags] [nvarchar](max) NULL,
	[Alias] [nvarchar](300) NULL,
	[MetaDesc] [nvarchar](300) NULL,
	[MetaKey] [nvarchar](300) NULL,
	[UnitsInStock] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shippers]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shippers](
	[ShipperID] [int] IDENTITY(1,1) NOT NULL,
	[ShipperName] [nvarchar](150) NOT NULL,
	[Phone] [nchar](12) NULL,
	[Company] [nvarchar](150) NULL,
	[ShipDate] [datetime] NULL,
 CONSTRAINT [PK_Shippers] PRIMARY KEY CLUSTERED 
(
	[ShipperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactStatuss]    Script Date: 11/19/2023 6:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactStatuss](
	[TransactStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_TransactStatuss] PRIMARY KEY CLUSTERED 
(
	[TransactStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountID], [Phone], [Email], [Salt], [Password], [FullName], [Active], [RoleID], [LastLogin], [CreateDate], [ModifiedDate]) VALUES (1, N'0398272747', N'caphuutuan1@gmail.com', NULL, N'1', N'Cáp Hữu Tuấn', 1, NULL, NULL, NULL, CAST(N'2023-11-14T02:15:27.443' AS DateTime))
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categoryies] ON 

INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1, N'Bánh Tráng Phơi Sương 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Bánh Tráng Phơi Sương', N'Bánh Tráng Phơi Sương', NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (2, N'Bánh tráng trộn', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Bánh tráng trộn', NULL, NULL, NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (3, N'Trà sữa', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Trà sữa', NULL, N'Trà sữa', NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1002, N'Da heo giòn', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Da heo giòn', NULL, N'Da heo giòn', NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1003, N'Xoài lắc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Xoài lắc', NULL, N'Xoài lắc', NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1004, N'Cá viên chiên', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Cá viên chiên', NULL, N'Cá viên chiên', NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1005, N'Lẩu tứ xuyên', N'Lẩu tứ xuyên', NULL, NULL, NULL, NULL, NULL, N'Lẩu tứ xuyên', N'Lẩu tứ xuyên', N'Lẩu tứ xuyên', N'Lẩu tứ xuyên', NULL, NULL)
INSERT [dbo].[Categoryies] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Thumb], [Title], [Alias], [MetaDesc], [MetaKey], [Cover], [SchemaMarkup]) VALUES (1006, N'Bánh đồng xu', N'Bánh đồng xu', NULL, NULL, NULL, NULL, NULL, N'Bánh đồng xu', N'Bánh đồng xu', N'Bánh đồng xu', N'Bánh đồng xu', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categoryies] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (1, N'Cáp Hữu Tuấn', CAST(N'2002-03-25' AS Date), N'cphutun.jpg', NULL, N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-11-14T03:48:34.360' AS DateTime))
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (2, N'Cáp Hữu Tuấn 1', NULL, N'cphutun.jpg', NULL, N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (3, N'Cáp Hữu Tuấn', NULL, N'cphutun.jpg', NULL, N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (4, N'Cáp Hữu Tuấn', CAST(N'2023-11-08' AS Date), N'cphutun.jpg', NULL, N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2023-11-14T03:48:53.643' AS DateTime))
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (5, N'Cáp Hữu Tuấn', CAST(N'2023-11-15' AS Date), N'cphutun.jpg', NULL, N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, CAST(N'2023-11-15T19:12:38.800' AS DateTime), N'1', NULL, NULL, 0, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (6, N'Cáp Hữu Tuấn 4', CAST(N'2023-11-16' AS Date), N'cphutun.jpg', NULL, N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, CAST(N'2023-11-16T17:54:29.263' AS DateTime), N'1', NULL, NULL, 0, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (7, N'Cáp Hữu Tuấn', CAST(N'2023-11-24' AS Date), N'cphutun.jpg', N'134 DT717', N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, CAST(N'2023-11-19T03:09:26.617' AS DateTime), N'1', NULL, NULL, 1, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (8, N'Cáp Hữu Tuấn', NULL, N'cphutun.jpg', N'134 DT717', N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, CAST(N'2023-11-19T05:43:42.420' AS DateTime), N'1', NULL, NULL, 1, NULL)
INSERT [dbo].[Customers] ([CustomerID], [FullName], [DOB], [Avatar], [Address], [Email], [Phone], [LocationID], [District], [Ward], [CreateDate], [Password], [Salt], [LastLogin], [Active], [ModifiedDate]) VALUES (10, N'Cáp Hữu Tuấn', NULL, N'cphutun.jpg', N'134 DT717', N'caphuutuan1@gmail.com', N'0398272747', NULL, NULL, NULL, CAST(N'2023-11-19T05:49:15.640' AS DateTime), N'1', NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentID], [Name], [Image]) VALUES (1, N'Thanh toán bằng tiền mặt', NULL)
INSERT [dbo].[Payments] ([PaymentID], [Name], [Image]) VALUES (2, N'Thanh toán bằng Thẻ ATM, Visa, Master Card', NULL)
INSERT [dbo].[Payments] ([PaymentID], [Name], [Image]) VALUES (3, N'Thanh toán bằng PayPal', NULL)
INSERT [dbo].[Payments] ([PaymentID], [Name], [Image]) VALUES (4, N'Thanh toán bằng Ví điện tử', NULL)
INSERT [dbo].[Payments] ([PaymentID], [Name], [Image]) VALUES (6, N'Thanh toán 2', NULL)
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[PostCategorys] ON 

INSERT [dbo].[PostCategorys] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Cover]) VALUES (1, N'Đời sống', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[PostCategorys] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Cover]) VALUES (2, N'Xã hội', NULL, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[PostCategorys] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Cover]) VALUES (3, N'Chính trị', NULL, NULL, 0, NULL, 1, NULL)
INSERT [dbo].[PostCategorys] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Cover]) VALUES (4, N'Văn hoá', NULL, NULL, 0, NULL, 1, NULL)
INSERT [dbo].[PostCategorys] ([CateID], [CateName], [Description], [ParentID], [Levels], [Ordering], [Published], [Cover]) VALUES (5, N'Nghệ thuật', NULL, 4, 0, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[PostCategorys] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([PostID], [Title], [SContents], [Contents], [Thumb], [Published], [Alias], [CreateDate], [Author], [AccountID], [Tags], [CateID], [IsHot], [IsNew], [MetaDesc], [MetaKey], [Views]) VALUES (3, N'abc', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Posts] ([PostID], [Title], [SContents], [Contents], [Thumb], [Published], [Alias], [CreateDate], [Author], [AccountID], [Tags], [CateID], [IsHot], [IsNew], [MetaDesc], [MetaKey], [Views]) VALUES (4, N'Abc', N'Abc', N'Abc', N'abc.jpg', 0, N'abc', CAST(N'2023-11-19T06:22:01.693' AS DateTime), NULL, NULL, NULL, 1, 0, 0, N'Abc', N'Abc', 0)
INSERT [dbo].[Posts] ([PostID], [Title], [SContents], [Contents], [Thumb], [Published], [Alias], [CreateDate], [Author], [AccountID], [Tags], [CateID], [IsHot], [IsNew], [MetaDesc], [MetaKey], [Views]) VALUES (5, N'Title', N'Title', N'Title', N'title.jpg', 0, N'title', CAST(N'2023-11-19T06:22:32.317' AS DateTime), NULL, NULL, NULL, NULL, 0, 0, N'Title', N'Title', 0)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CateID], [Price], [Discount], [Thumb], [Video], [CreateDate], [ModifiedDate], [BestSeller], [IsHome], [Active], [Title], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitsInStock]) VALUES (2019, N'Test Sp 1', NULL, N'Test Sp 1', NULL, 0, 0, N'oripicpng.png', NULL, CAST(N'2023-11-16T03:51:02.820' AS DateTime), CAST(N'2023-11-16T17:17:37.497' AS DateTime), NULL, 0, 1, N'Test Sp 1', NULL, N'testsp1', N'Test Sp 1', N'Test Sp 1', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CateID], [Price], [Discount], [Thumb], [Video], [CreateDate], [ModifiedDate], [BestSeller], [IsHome], [Active], [Title], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitsInStock]) VALUES (2020, N'Test Sp 1', NULL, N'Test Sp 1', 2, 0, 0, NULL, NULL, CAST(N'2023-11-19T05:17:19.263' AS DateTime), NULL, NULL, 0, 0, N'Test Sp 1', N'2', N'testsp1', N'Test Sp 1', N'Test Sp 1', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CateID], [Price], [Discount], [Thumb], [Video], [CreateDate], [ModifiedDate], [BestSeller], [IsHome], [Active], [Title], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitsInStock]) VALUES (2021, N'Test Sp 11', NULL, N'Test Sp 11', NULL, 0, 0, NULL, NULL, CAST(N'2023-11-19T18:32:01.967' AS DateTime), NULL, NULL, 0, 0, N'Test Sp 11', NULL, N'test-sp-11', N'Test Sp 11', N'Test Sp 11', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ShortDesc], [Description], [CateID], [Price], [Discount], [Thumb], [Video], [CreateDate], [ModifiedDate], [BestSeller], [IsHome], [Active], [Title], [Tags], [Alias], [MetaDesc], [MetaKey], [UnitsInStock]) VALUES (2022, N'Test Sp 11', NULL, N'Test Sp 11', 2, 0, 0, NULL, NULL, CAST(N'2023-11-19T18:33:24.503' AS DateTime), NULL, NULL, 0, 0, N'Test Sp 11', NULL, N'test-sp-11', N'Test Sp 11', N'Test Sp 11', 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (1, N'Admin', N'Quản trị viên')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (2, N'Staff', N'Nhân Viên')
INSERT [dbo].[Roles] ([RoleID], [RoleName], [Description]) VALUES (3, N'Customer', N'Khách Hàng')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Shippers] ON 

INSERT [dbo].[Shippers] ([ShipperID], [ShipperName], [Phone], [Company], [ShipDate]) VALUES (1, N'Cáp Hữu Tuấn', N'0398272747  ', N'ABC Coop', CAST(N'2023-11-19T10:35:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Shippers] OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[Categoryies]  WITH CHECK ADD  CONSTRAINT [FK_Categoryies_Categoryies] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Categoryies] ([CateID])
GO
ALTER TABLE [dbo].[Categoryies] CHECK CONSTRAINT [FK_Categoryies_Categoryies]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Locations]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Payments] FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payments] ([PaymentID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Payments]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_TransactStatuss] FOREIGN KEY([TransactStatusID])
REFERENCES [dbo].[TransactStatuss] ([TransactStatusID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_TransactStatuss]
GO
ALTER TABLE [dbo].[PostCategorys]  WITH CHECK ADD  CONSTRAINT [FK_PostCategorys_PostCategorys] FOREIGN KEY([ParentID])
REFERENCES [dbo].[PostCategorys] ([CateID])
GO
ALTER TABLE [dbo].[PostCategorys] CHECK CONSTRAINT [FK_PostCategorys_PostCategorys]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Accounts]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_PostCategorys] FOREIGN KEY([CateID])
REFERENCES [dbo].[PostCategorys] ([CateID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_PostCategorys]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categoryies] FOREIGN KEY([CateID])
REFERENCES [dbo].[Categoryies] ([CateID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categoryies]
GO
