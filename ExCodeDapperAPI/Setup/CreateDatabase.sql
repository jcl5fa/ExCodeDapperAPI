USE [master]
GO

/****** Object:  Database [exCode]    Script Date: 12/15/2022 8:16:48 AM ******/
CREATE DATABASE [exCode]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'exCode', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\exCode.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'exCode_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\exCode_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [exCode].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [exCode] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [exCode] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [exCode] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [exCode] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [exCode] SET ARITHABORT OFF 
GO

ALTER DATABASE [exCode] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [exCode] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [exCode] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [exCode] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [exCode] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [exCode] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [exCode] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [exCode] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [exCode] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [exCode] SET  DISABLE_BROKER 
GO

ALTER DATABASE [exCode] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [exCode] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [exCode] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [exCode] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [exCode] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [exCode] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [exCode] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [exCode] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [exCode] SET  MULTI_USER 
GO

ALTER DATABASE [exCode] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [exCode] SET DB_CHAINING OFF 
GO

ALTER DATABASE [exCode] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [exCode] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [exCode] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [exCode] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [exCode] SET QUERY_STORE = OFF
GO

ALTER DATABASE [exCode] SET  READ_WRITE 
GO


