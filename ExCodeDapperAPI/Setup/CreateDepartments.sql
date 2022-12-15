USE [exCode]
GO

/****** Object:  Table [dbo].[Departments]    Script Date: 12/15/2022 8:18:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL
) ON [PRIMARY]
GO


