USE [master]
GO
/****** Object:  Database [PersonDB]    Script Date: 6/15/2020 21:24:14 ******/
CREATE DATABASE [PersonDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PersonDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PersonDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PersonDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PersonDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PersonDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PersonDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PersonDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PersonDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PersonDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PersonDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PersonDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PersonDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PersonDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PersonDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PersonDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PersonDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PersonDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PersonDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PersonDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PersonDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PersonDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PersonDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PersonDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PersonDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PersonDB] SET RECOVERY FULL 
GO
ALTER DATABASE [PersonDB] SET  MULTI_USER 
GO
ALTER DATABASE [PersonDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PersonDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PersonDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PersonDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PersonDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PersonDB', N'ON'
GO
ALTER DATABASE [PersonDB] SET QUERY_STORE = OFF
GO
USE [PersonDB]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAge]    Script Date: 6/15/2020 21:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetAge] ( @DateOfBirth    DATETIME )
RETURNS INT
AS
BEGIN

    DECLARE @Age         INT
	DECLARE @CurrentDate Date
	SET @CurrentDate = GETDATE();
    
    IF @DateOfBirth >= @CurrentDate
        RETURN 0

    SET @Age = DATEDIFF(YY, @DateOfBirth, @CurrentDate)

    IF MONTH(@DateOfBirth) > MONTH(@CurrentDate) OR
      (MONTH(@DateOfBirth) = MONTH(@CurrentDate) AND
       DAY(@DateOfBirth)   > DAY(@CurrentDate))
        SET @Age = @Age - 1

    RETURN @Age
END
GO
/****** Object:  Table [dbo].[City]    Script Date: 6/15/2020 21:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 6/15/2020 21:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[PrivateNumber] [nvarchar](11) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[CityId] [int] NOT NULL,
	[Picture] [nvarchar](max) NULL,
 CONSTRAINT [PK_Table_2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonConnection]    Script Date: 6/15/2020 21:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonConnection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[ConnectionType] [nvarchar](20) NOT NULL,
	[ConnectedPersonId] [int] NOT NULL,
 CONSTRAINT [PK_PersonConnection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone]    Script Date: 6/15/2020 21:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Number] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([Id], [Name]) VALUES (1, N'თბილისი')
INSERT [dbo].[City] ([Id], [Name]) VALUES (2, N'ქუთაისი')
INSERT [dbo].[City] ([Id], [Name]) VALUES (3, N'ბორჯომი')
INSERT [dbo].[City] ([Id], [Name]) VALUES (4, N'რუსთავი')
INSERT [dbo].[City] ([Id], [Name]) VALUES (5, N'London')
INSERT [dbo].[City] ([Id], [Name]) VALUES (6, N'Paris')
INSERT [dbo].[City] ([Id], [Name]) VALUES (7, N'New York')
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (37, N'John', N'Smith', N'კაცი', N'01011111111', CAST(N'1999-12-02' AS Date), 7, N'whhsq522.cvn.png')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (38, N'გიორგი', N'მაისურაძე', N'კაცი', N'10111111112', CAST(N'1999-02-22' AS Date), 3, NULL)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (40, N'ააბ', N'აა', N'კაცი', N'11111389541', CAST(N'1999-02-02' AS Date), 1, NULL)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (42, N'Test', N'test', N'კაცი', N'11111111148', CAST(N'1999-02-02' AS Date), 2, NULL)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (46, N'ლუკა', N'მაისურაძე', N'ქალი', N'01011111113', CAST(N'1992-03-05' AS Date), 1, NULL)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (47, N'ანა', N'გვარიშვილი', N'ქალი', N'01011111113', CAST(N'1992-03-05' AS Date), 4, NULL)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (48, N'ქეთევან', N'გვარიძე', N'ქალი', N'01011111128', CAST(N'1992-03-05' AS Date), 5, NULL)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Gender], [PrivateNumber], [DateOfBirth], [CityId], [Picture]) VALUES (49, N'თამარ', N'გულია', N'ქალი', N'01011551113', CAST(N'1992-03-05' AS Date), 2, NULL)
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
SET IDENTITY_INSERT [dbo].[PersonConnection] ON 

INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (1, 37, N'სხვა', 38)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (2, 37, N'კოლეგა', 40)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (3, 42, N'ნაცნობი', 47)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (4, 42, N'ნათესავი', 48)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (5, 46, N'სხვა', 37)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (6, 46, N'ნათესავი', 38)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (7, 46, N'ნათესავი', 40)
INSERT [dbo].[PersonConnection] ([Id], [PersonId], [ConnectionType], [ConnectedPersonId]) VALUES (9, 46, N'ნათესავი', 38)
SET IDENTITY_INSERT [dbo].[PersonConnection] OFF
GO
SET IDENTITY_INSERT [dbo].[Phone] ON 

INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (3, 37, N'სახლის', N'515555    ')
INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (4, 37, N'ოფისის', N'543513513 ')
INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (7, 42, N'მობილური', N'543513513 ')
INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (8, 42, N'სახლის', N'515555    ')
INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (10, 38, N'მობილური', N'555555664 ')
INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (11, 46, N'ოფისის', N'555555664 ')
INSERT [dbo].[Phone] ([Id], [PersonId], [Type], [Number]) VALUES (12, 46, N'მობილური', N'555555664 ')
SET IDENTITY_INSERT [dbo].[Phone] OFF
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_City]
GO
ALTER TABLE [dbo].[PersonConnection]  WITH CHECK ADD  CONSTRAINT [FK_PersonConnection_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonConnection] CHECK CONSTRAINT [FK_PersonConnection_Person]
GO
ALTER TABLE [dbo].[PersonConnection]  WITH CHECK ADD  CONSTRAINT [FK_PersonConnection_PersonConnection] FOREIGN KEY([ConnectedPersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonConnection] CHECK CONSTRAINT [FK_PersonConnection_PersonConnection]
GO
ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [FK_Phone_Person]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_Age] CHECK  (([dbo].[GetAge]([DateOfBirth])>(17)))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_Age]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_FirstName_Latin_Georgian] CHECK  ((NOT [FirstName] like '%[^a-zA-Z]%' OR NOT [FirstName] like '%[^ა-ჰ]%'))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_FirstName_Latin_Georgian]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_FirstName_Length] CHECK  ((len([FirstName])>(1) AND len([FirstName])<(51)))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_FirstName_Length]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_Gender] CHECK  (([Gender]='ქალი' OR [Gender]='კაცი'))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_Gender]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_LastName_Latin_Georgian] CHECK  ((NOT [LastName] like '%[^a-zA-Z]%' OR NOT [LastName] like '%[^ა-ჰ]%'))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_LastName_Latin_Georgian]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_LastName_Length] CHECK  ((len([LastName])>(1) AND len([LastName])<(51)))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_LastName_Length]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_PrivateNumber_Lengh] CHECK  ((len([PrivateNumber])=(11) AND NOT [PrivateNumber] like '%[^0-9]%'))
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_PrivateNumber_Lengh]
GO
ALTER TABLE [dbo].[PersonConnection]  WITH CHECK ADD  CONSTRAINT [CK_PersonConnection] CHECK  (([ConnectionType]='სხვა' OR [ConnectionType]='ნათესავი' OR [ConnectionType]='ნაცნობი' OR [ConnectionType]='კოლეგა'))
GO
ALTER TABLE [dbo].[PersonConnection] CHECK CONSTRAINT [CK_PersonConnection]
GO
ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [CK_Phone_Number_Length] CHECK  ((len([Number])>(3) AND len([Number])<(51)))
GO
ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [CK_Phone_Number_Length]
GO
USE [master]
GO
ALTER DATABASE [PersonDB] SET  READ_WRITE 
GO
