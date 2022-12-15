USE [exCode]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 12/15/2022 8:22:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[DepartmentId] [int] NULL,
	[ManagerId] [int] NULL
) ON [PRIMARY]
GO


