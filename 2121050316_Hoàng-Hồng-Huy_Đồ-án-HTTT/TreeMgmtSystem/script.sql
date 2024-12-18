USE [master]
GO
/****** Object:  Database [treemgmt]    Script Date: 2024-12-02 21:23:55 ******/
CREATE DATABASE [treemgmt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'treemgmt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\treemgmt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'treemgmt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\treemgmt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [treemgmt] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [treemgmt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [treemgmt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [treemgmt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [treemgmt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [treemgmt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [treemgmt] SET ARITHABORT OFF 
GO
ALTER DATABASE [treemgmt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [treemgmt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [treemgmt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [treemgmt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [treemgmt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [treemgmt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [treemgmt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [treemgmt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [treemgmt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [treemgmt] SET  DISABLE_BROKER 
GO
ALTER DATABASE [treemgmt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [treemgmt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [treemgmt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [treemgmt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [treemgmt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [treemgmt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [treemgmt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [treemgmt] SET RECOVERY FULL 
GO
ALTER DATABASE [treemgmt] SET  MULTI_USER 
GO
ALTER DATABASE [treemgmt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [treemgmt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [treemgmt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [treemgmt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [treemgmt] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [treemgmt] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'treemgmt', N'ON'
GO
ALTER DATABASE [treemgmt] SET QUERY_STORE = ON
GO
ALTER DATABASE [treemgmt] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [treemgmt]
GO
/****** Object:  Table [dbo].[History]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[HistoryId] [int] IDENTITY(1,1) NOT NULL,
	[EventType] [nvarchar](100) NULL,
	[EventDescription] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[UserId] [int] NULL,
	[TreeId] [int] NULL,
	[ServiceId] [int] NULL,
	[ReportId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[HistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[ReportId] [int] NOT NULL,
	[Annunciator] [int] NULL,
	[ReportDate] [date] NOT NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ServiceId] [int] NOT NULL,
	[ServiceType] [nvarchar](100) NULL,
	[RequestDate] [date] NULL,
	[Status] [nvarchar](50) NULL,
	[UserID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Species]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Species](
	[SpeciesId] [int] NOT NULL,
	[SpeciesName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SpeciesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] NOT NULL,
	[SupplierName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tree]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tree](
	[TreeId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Species] [int] NULL,
	[Age] [int] NULL,
	[Height] [int] NULL,
	[Diameter] [int] NULL,
	[HealthStatus] [nvarchar](50) NULL,
	[Note] [nvarchar](255) NULL,
	[Location] [nvarchar](100) NULL,
	[ReminderDate] [date] NULL,
	[SupplierId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TreeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2024-12-02 21:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[PhoneNumber] [varchar](15) NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Sex] [bit] NULL,
	[Email] [nvarchar](100) NULL,
	[Role] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[History] ON 

INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (11, N'Dịch vụ hoàn thành', N'Hoàn thành tỉa cành cây ID 1', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 1, 1, 5, NULL)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (12, N'Báo cáo tạo mới', N'Báo cáo về cây bị ngập úng do mưa lớn', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (13, N'Dịch vụ bắt đầu', N'Bắt đầu phun thuốc cho cây ID 2', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 2, 2, 3, NULL)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (14, N'Báo cáo tạo mới', N'Báo cáo về cây cần chăm sóc khẩn cấp', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 2, NULL, NULL, 2)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (15, N'Dịch vụ hoàn thành', N'Hoàn thành tưới nước cây ID 3', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 3, 3, 2, NULL)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (16, N'Báo cáo tạo mới', N'Báo cáo về cây có dấu hiệu bệnh nấm', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 3, NULL, NULL, 3)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (17, N'Dịch vụ hủy bỏ', N'Hủy bỏ dịch vụ kiểm tra sức khỏe cây ID 4', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 4, 4, 6, NULL)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (18, N'Báo cáo tạo mới', N'Báo cáo về cây cần tỉa cành để tránh nguy hiểm', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 4, NULL, NULL, 4)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (19, N'Dịch vụ hoàn thành', N'Hoàn thành bón phân cho cây ID 5', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 5, 5, 4, NULL)
INSERT [dbo].[History] ([HistoryId], [EventType], [EventDescription], [Date], [UserId], [TreeId], [ServiceId], [ReportId]) VALUES (20, N'Báo cáo tạo mới', N'Báo cáo về cây bị tổn thương do va chạm xe cộ', CAST(N'2024-12-02T06:11:45.490' AS DateTime), 5, NULL, NULL, 5)
SET IDENTITY_INSERT [dbo].[History] OFF
GO
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (1, 1, CAST(N'2024-10-10' AS Date), N'Báo cáo cây xanh bị ngập úng do mưa lớn')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (2, 2, CAST(N'2024-10-11' AS Date), N'Báo cáo cây xanh cần kiểm tra do sâu bệnh lan rộng')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (3, 3, CAST(N'2024-10-12' AS Date), N'Báo cáo cây xanh cần chăm sóc khẩn cấp do bị đổ gãy')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (4, 4, CAST(N'2024-10-13' AS Date), N'Báo cáo cây xanh mọc che khuất lối đi bộ')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (5, 5, CAST(N'2024-10-14' AS Date), N'Báo cáo cây xanh cần tỉa cành để tránh nguy hiểm')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (6, 6, CAST(N'2024-10-15' AS Date), N'Báo cáo cây xanh có dấu hiệu khô héo bất thường')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (7, 7, CAST(N'2024-10-16' AS Date), N'Báo cáo cây xanh bị tổn thương do va chạm xe cộ')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (8, 8, CAST(N'2024-10-17' AS Date), N'Báo cáo cây xanh cần kiểm tra sức khỏe định kỳ')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (9, 9, CAST(N'2024-10-18' AS Date), N'Báo cáo cây xanh bị hỏng do mưa bão')
INSERT [dbo].[Report] ([ReportId], [Annunciator], [ReportDate], [Description]) VALUES (10, 10, CAST(N'2024-10-19' AS Date), N'Báo cáo cây xanh cần được thay thế bằng cây mới')
GO
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (1, N'Trồng mới', CAST(N'2024-10-05' AS Date), N'Hoàn thành', 1)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (2, N'Tưới nước', CAST(N'2024-10-06' AS Date), N'Đang xử lý', 2)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (3, N'Bón phân', CAST(N'2024-10-07' AS Date), N'Hoàn thành', 3)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (4, N'Phun thuốc', CAST(N'2024-10-08' AS Date), N'Chờ xử lý', 4)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (5, N'Cắt tỉa', CAST(N'2024-10-09' AS Date), N'Đang xử lý', 5)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (6, N'Kiểm tra sức khỏe', CAST(N'2024-10-10' AS Date), N'Hoàn thành', 6)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (7, N'Chặt cây', CAST(N'2024-10-11' AS Date), N'Chờ xử lý', 7)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (8, N'Trồng lại', CAST(N'2024-10-12' AS Date), N'Hoàn thành', 8)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (9, N'Phun thuốc', CAST(N'2024-10-13' AS Date), N'Đang xử lý', 9)
INSERT [dbo].[Services] ([ServiceId], [ServiceType], [RequestDate], [Status], [UserID]) VALUES (10, N'Kiểm tra sức khỏe', CAST(N'2024-10-14' AS Date), N'Hoàn thành', 10)
GO
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (7, N'Cây Bằng Lăng')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (4, N'Cây Cọ Xẻ')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (10, N'Cây Gạo')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (3, N'Cây Lát Hoa')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (1, N'Cây Lim Xanh')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (8, N'Cây Long Não')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (9, N'Cây Phượng Vĩ')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (2, N'Cây Sến')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (6, N'Cây Tràm')
INSERT [dbo].[Species] ([SpeciesId], [SpeciesName]) VALUES (5, N'Cây Xà Cừ')
GO
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (1, N'Nhà cung cấp A', N'0988992233')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (2, N'Nhà cung cấp B', N'0977883344')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (3, N'Nhà cung cấp C', N'0966774455')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (4, N'Nhà cung cấp D', N'0955665566')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (5, N'Nhà cung cấp E', N'0944556677')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (6, N'Nhà cung cấp F', N'0933447788')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (7, N'Nhà cung cấp G', N'0922338899')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (8, N'Nhà cung cấp H', N'0911229900')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (9, N'Nhà cung cấp I', N'0900112233')
INSERT [dbo].[Supplier] ([SupplierId], [SupplierName], [PhoneNumber]) VALUES (10, N'Nhà cung cấp K', N'0899001122')
GO
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (1, 1, 1, 5, 300, 40, N'Khỏe mạnh', N'Không có ghi chú', N'Đường Hoàng Diệu', CAST(N'2024-10-20' AS Date), 10)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (2, 2, 2, 7, 400, 50, N'Cần tỉa', N'Cây mọc quá cao', N'Công viên D', CAST(N'2024-10-22' AS Date), 9)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (3, 3, 3, 10, 600, 70, N'Bệnh', N'Xuất hiện sâu bệnh', N'Trường học Y', CAST(N'2024-10-25' AS Date), 8)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (4, 4, 4, 3, 150, 20, N'Yếu', N'Lá cây chuyển màu vàng', N'Đường Trần Hưng Đạo', CAST(N'2024-10-28' AS Date), 7)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (5, 5, 5, 15, 1000, 90, N'Khỏe mạnh', N'Không có ghi chú', N'Khu đô thị A', CAST(N'2024-11-01' AS Date), 6)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (6, 6, 6, 8, 500, 60, N'Cần tỉa', N'Cành cây che khuất đèn đường', N'Khu dân cư B', CAST(N'2024-11-05' AS Date), 5)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (7, 7, 7, 6, 350, 45, N'Yếu', N'Cần chăm sóc thêm', N'Đường Nguyễn Văn Cừ', CAST(N'2024-11-10' AS Date), 4)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (8, 8, 8, 12, 800, 85, N'Khỏe mạnh', N'Không có ghi chú', N'Công viên E', CAST(N'2024-11-15' AS Date), 3)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (9, 9, 9, 9, 700, 75, N'Bệnh', N'Thân cây bị nứt', N'Đường Võ Thị Sáu', CAST(N'2024-11-20' AS Date), 2)
INSERT [dbo].[Tree] ([TreeId], [UserId], [Species], [Age], [Height], [Diameter], [HealthStatus], [Note], [Location], [ReminderDate], [SupplierId]) VALUES (10, 10, 10, 4, 250, 30, N'Cần tỉa', N'Cành cây mọc quá dài', N'Sân vận động B', CAST(N'2024-11-25' AS Date), 1)
GO
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (1, N'0987123456', N'user1', N'password1', N'User A', 1, N'user1@gmail.com', N'User', N'12 Đường Lê Lợi, Hà Nội')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (2, N'0909222333', N'user2', N'password2', N'User B', 0, N'user2@gmail.com', N'User', N'34 Đường Nguyễn Huệ, Đà Nẵng')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (3, N'0911445566', N'user3', N'password3', N'User C', 1, N'user3@gmail.com', N'User', N'56 Đường Lý Thái Tổ, TP.HCM')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (4, N'0922333444', N'user4', N'password4', N'User D', 0, N'user4@gmail.com', N'Admin', N'78 Đường Hùng Vương, Huế')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (5, N'0933445566', N'user5', N'password5', N'User E', 1, N'user5@gmail.com', N'User', N'90 Đường Trần Hưng Đạo, Hà Nội')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (6, N'0944556677', N'user6', N'password6', N'User F', 1, N'user6@gmail.com', N'User', N'23 Đường Phan Châu Trinh, Đà Nẵng')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (7, N'0955667788', N'user7', N'password7', N'User G', 0, N'user7@gmail.com', N'User', N'67 Đường Võ Thị Sáu, TP.HCM')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (8, N'0966778899', N'user8', N'password8', N'User H', 1, N'user8@gmail.com', N'Admin', N'45 Đường Nguyễn Trãi, Hà Nội')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (9, N'0977889900', N'user9', N'password9', N'User I', 0, N'user9@gmail.com', N'User', N'89 Đường Lê Duẩn, Đà Nẵng')
INSERT [dbo].[Users] ([UserId], [PhoneNumber], [UserName], [Password], [FullName], [Sex], [Email], [Role], [Address]) VALUES (10, N'0988990011', N'user10', N'password10', N'User K', 1, N'user10@gmail.com', N'User', N'101 Đường Nguyễn Văn Linh, TP.HCM')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Species__304D4C0DDF2FFCB9]    Script Date: 2024-12-02 21:23:56 ******/
ALTER TABLE [dbo].[Species] ADD UNIQUE NONCLUSTERED 
(
	[SpeciesName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534AEC59F8A]    Script Date: 2024-12-02 21:23:56 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__C9F2845635530A84]    Script Date: 2024-12-02 21:23:56 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([ReportId])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_Report]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([ServiceId])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_Service]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_Tree] FOREIGN KEY([TreeId])
REFERENCES [dbo].[Tree] ([TreeId])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_Tree]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_User]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD FOREIGN KEY([Annunciator])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tree]  WITH CHECK ADD FOREIGN KEY([Species])
REFERENCES [dbo].[Species] ([SpeciesId])
GO
ALTER TABLE [dbo].[Tree]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[Tree]  WITH CHECK ADD  CONSTRAINT [FK_Tree_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[Tree] CHECK CONSTRAINT [FK_Tree_Supplier]
GO
USE [master]
GO
ALTER DATABASE [treemgmt] SET  READ_WRITE 
GO
