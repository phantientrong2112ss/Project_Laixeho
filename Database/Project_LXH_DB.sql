USE [master]
GO
/****** Object:  Database [GPChauffeur]    Script Date: 19/7/2023 5:05:11 PM ******/
CREATE DATABASE [GPChauffeur]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GPChauffeur', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\GPChauffeur.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GPChauffeur_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\GPChauffeur_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GPChauffeur] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GPChauffeur].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GPChauffeur] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GPChauffeur] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GPChauffeur] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GPChauffeur] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GPChauffeur] SET ARITHABORT OFF 
GO
ALTER DATABASE [GPChauffeur] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GPChauffeur] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GPChauffeur] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GPChauffeur] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GPChauffeur] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GPChauffeur] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GPChauffeur] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GPChauffeur] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GPChauffeur] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GPChauffeur] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GPChauffeur] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GPChauffeur] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GPChauffeur] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GPChauffeur] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GPChauffeur] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GPChauffeur] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GPChauffeur] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GPChauffeur] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GPChauffeur] SET  MULTI_USER 
GO
ALTER DATABASE [GPChauffeur] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GPChauffeur] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GPChauffeur] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GPChauffeur] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GPChauffeur] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GPChauffeur] SET QUERY_STORE = OFF
GO
USE [GPChauffeur]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[username] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[PhoneNumber] [nvarchar](200) NOT NULL,
	[Image] [nvarchar](200) NOT NULL,
	[Role] [nvarchar](200) NOT NULL,
	[AccountBalance] [nvarchar](200) NOT NULL,
	[UserId] [nvarchar](200) NOT NULL,
	[CurrentLocation] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](200) NOT NULL,
	[Remembertoken] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComAndRate]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComAndRate](
	[Id] [nvarchar](200) NOT NULL,
	[CRId] [nvarchar](200) NOT NULL,
	[UserId] [nvarchar](200) NOT NULL,
	[Describe] [nvarchar](200) NOT NULL,
	[Displaystatus] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Sex] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[PhoneNumber] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailInvoice]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailInvoice](
	[Id] [nvarchar](200) NOT NULL,
	[IdInvoice] [nvarchar](200) NOT NULL,
	[IdService] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiscountCode]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountCode](
	[id] [nvarchar](200) NOT NULL,
	[DiscountCode] [nvarchar](200) NOT NULL,
	[DiscountRate] [int] NOT NULL,
	[DiscountType] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[Id] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Sex] [nvarchar](200) NOT NULL,
	[Birthday] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[PhoneNumber] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[DriverLicense] [nvarchar](200) NOT NULL,
	[Rate] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imgs]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imgs](
	[Id] [nvarchar](200) NOT NULL,
	[TId] [nvarchar](200) NOT NULL,
	[Urlimg] [nvarchar](2000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [nvarchar](200) NOT NULL,
	[IdCustomer] [nvarchar](200) NOT NULL,
	[IdDriver] [nvarchar](200) NULL,
	[TotalAmount] [int] NOT NULL,
	[Paymentid] [nvarchar](200) NOT NULL,
	[InvoiceStatus] [nvarchar](200) NOT NULL,
	[Note] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [nvarchar](200) NOT NULL,
	[LocationName] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Coordinates] [nvarchar](200) NOT NULL,
	[Describe] [nvarchar](2000) NOT NULL,
	[Locationp1] [nvarchar](2000) NOT NULL,
	[Locationp2] [nvarchar](2000) NOT NULL,
	[Locationp3] [nvarchar](2000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [nvarchar](200) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](200) NOT NULL,
	[Tag] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [nvarchar](200) NOT NULL,
	[UserId] [nvarchar](200) NOT NULL,
	[AmountM] [int] NOT NULL,
	[PaymentType] [nvarchar](200) NOT NULL,
	[PaymentStatus] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TServices]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TServices](
	[Id] [nvarchar](200) NOT NULL,
	[IdVehicle] [nvarchar](200) NOT NULL,
	[Describe] [nvarchar](200) NOT NULL,
	[Discount] [nvarchar](200) NOT NULL,
	[PickUpPoint] [nvarchar](200) NOT NULL,
	[Destination] [nvarchar](200) NOT NULL,
	[Distance] [nvarchar](200) NOT NULL,
	[TransTime] [nvarchar](200) NOT NULL,
	[PriceBDistance] [int] NOT NULL,
	[ServicePrice] [int] NOT NULL,
	[Note] [nvarchar](200) NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 19/7/2023 5:05:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [nvarchar](200) NOT NULL,
	[VehiclesName] [nvarchar](200) NOT NULL,
	[Cbrand] [nvarchar](200) NOT NULL,
	[Illustration] [nvarchar](200) NOT NULL,
	[DlicenseRequired] [nvarchar](200) NOT NULL,
	[Describe] [nvarchar](2000) NOT NULL,
	[SPrice] [int] NOT NULL,
	[created_at] [nvarchar](200) NOT NULL,
	[updated_at] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [GPChauffeur] SET  READ_WRITE 
GO
