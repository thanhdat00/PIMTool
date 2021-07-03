USE [master]
GO
/****** Object:  Database [PIMDB]    Script Date: 7/3/2021 10:18:46 AM ******/
CREATE DATABASE [PIMDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PIMDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PIMDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PIMDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PIMDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PIMDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PIMDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PIMDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PIMDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PIMDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PIMDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PIMDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PIMDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PIMDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PIMDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PIMDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PIMDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PIMDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PIMDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PIMDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PIMDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PIMDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PIMDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PIMDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PIMDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PIMDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PIMDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PIMDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PIMDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PIMDB] SET RECOVERY FULL 
GO
ALTER DATABASE [PIMDB] SET  MULTI_USER 
GO
ALTER DATABASE [PIMDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PIMDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PIMDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PIMDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PIMDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PIMDB', N'ON'
GO
ALTER DATABASE [PIMDB] SET QUERY_STORE = OFF
GO
USE [PIMDB]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/3/2021 10:18:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VISA] [varchar](3) NOT NULL,
	[FirstName] [nvarchar](51) NOT NULL,
	[LastName] [nvarchar](51) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[RowVersion] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeProjects]    Script Date: 7/3/2021 10:18:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeProjects](
	[ProjectID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 7/3/2021 10:18:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupLeaderID] [int] NOT NULL,
	[RowVersion] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 7/3/2021 10:18:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NOT NULL,
	[ProjectNumber] [int] NOT NULL,
	[Name] [nvarchar](51) NOT NULL,
	[Customer] [nvarchar](51) NOT NULL,
	[Status] [varchar](3) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[FinishDate] [datetime] NULL,
	[RowVersion] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 7/3/2021 10:18:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[ProjectID] [int] NOT NULL,
	[RowVersion] [int] NOT NULL,
	[DeadlineDate] [datetime] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskAUD]    Script Date: 7/3/2021 10:18:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskAUD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[ProjectID] [int] NOT NULL,
	[RowVersion] [int] NOT NULL,
	[DeadlineDate] [datetime] NULL,
	[Action] [nvarchar](100) NULL,
	[TaskID] [int] NOT NULL,
 CONSTRAINT [PK_TaskAUD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [VISA], [FirstName], [LastName], [BirthDate], [RowVersion]) VALUES (1, N'DOH', N'Thanh', N'Dat', CAST(N'1894-07-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Employee] ([ID], [VISA], [FirstName], [LastName], [BirthDate], [RowVersion]) VALUES (2, N'KTY', N'Kim', N'Taeyeon', CAST(N'1894-07-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Employee] ([ID], [VISA], [FirstName], [LastName], [BirthDate], [RowVersion]) VALUES (3, N'BLH', N'Bui', N'Huong', CAST(N'1894-07-11T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[EmployeeProjects] ([ProjectID], [EmployeeID]) VALUES (35, 1)
INSERT [dbo].[EmployeeProjects] ([ProjectID], [EmployeeID]) VALUES (34, 1)
INSERT [dbo].[EmployeeProjects] ([ProjectID], [EmployeeID]) VALUES (34, 3)
INSERT [dbo].[EmployeeProjects] ([ProjectID], [EmployeeID]) VALUES (36, 1)
INSERT [dbo].[EmployeeProjects] ([ProjectID], [EmployeeID]) VALUES (39, 1)
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([ID], [GroupLeaderID], [RowVersion]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (1, 1, 123, N'PIM', N'ELCA', N'NEW', CAST(N'1894-06-16T00:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (3, 1, 124, N'aaa', N'Dat', N'NEW', CAST(N'1894-06-16T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (7, 1, 125, N'aaa', N'ThanhDatne', N'NEW', CAST(N'1894-06-16T00:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (17, 1, 1001, N'a', N'dat', N'NEW', CAST(N'2021-06-11T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (18, 1, 1, N'Eros', N'Eriksen', N'PLA', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (19, 1, 2, N'FirendZone', N'Prof.Triet', N'INP', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (20, 1, 4, N'Euro', N'FCB', N'PLA', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 4)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (23, 1, 8, N'BetterCallSaul', N'Jimmy', N'FIN', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (24, 1, 10, N'Breaking Bad', N'Mr.White', N'PLA', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (25, 1, 13, N'LaLaLand', N'E', N'NEW', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (26, 1, 15, N'Inception', N'Nolan', N'NEW', CAST(N'2021-06-13T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (34, 1, 133, N'Casino', N'Martin', N'NEW', CAST(N'2021-06-16T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (35, 1, 11, N'abc', N'dat', N'NEW', CAST(N'2021-06-18T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (36, 1, 1234, N'Chuking Express', N'Kai', N'NEW', CAST(N'2021-06-17T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Project] ([ID], [GroupID], [ProjectNumber], [Name], [Customer], [Status], [StartDate], [FinishDate], [RowVersion]) VALUES (39, 1, 111, N'LadyBird', N'Coppola', N'PLA', CAST(N'2020-11-20T00:00:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
/****** Object:  Index [UQ__Project__C66B6F6ABA430BC0]    Script Date: 7/3/2021 10:18:47 AM ******/
ALTER TABLE [dbo].[Project] ADD UNIQUE NONCLUSTERED 
(
	[ProjectNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeGroup] FOREIGN KEY([GroupLeaderID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_EmployeeGroup]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_GroupProject] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_GroupProject]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Project]
GO
USE [master]
GO
ALTER DATABASE [PIMDB] SET  READ_WRITE 
GO
