SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.Tasks', 'U') IS NOT NULL
  DROP TABLE [dbo].[Tasks];

CREATE TABLE [dbo].[Tasks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NULL DEFAULT (sysutcdatetime()),
	[UserName] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_dbo.Tasks] PRIMARY KEY CLUSTERED ( [Id] ASC )
	WITH 
	(
		PAD_INDEX = OFF, 
		STATISTICS_NORECOMPUTE = OFF, 
		IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS = ON, 
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]