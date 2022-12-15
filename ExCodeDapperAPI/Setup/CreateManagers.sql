USE [exCode]
GO

/****** Object:  Table [dbo].[Managers]    Script Date: 12/15/2022 8:23:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Managers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL
) ON [PRIMARY]
GO


