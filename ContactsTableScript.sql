USE [Contacts]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 3/17/2018 9:20:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Egn] [numeric](10, 0) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Telephone] [varchar](32) NOT NULL
) ON [PRIMARY]
GO


