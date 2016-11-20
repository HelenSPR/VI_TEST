IF (OBJECT_ID(N'dbo.Station','U') IS NULL)
CREATE TABLE [dbo].[Station](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO


IF (OBJECT_ID(N'Track','U') IS NULL)

CREATE TABLE [dbo].[Track](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[weight] [int] NOT NULL,
	[comment] [nvarchar](max)  NULL,
 CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



GO
IF (OBJECT_ID(N'dbo.Station_Track','U') IS NULL)
BEGIN

CREATE TABLE [dbo].[Station_Track](
	[idstation_start] [int] NOT NULL,
	[idstation_stop] [int] NOT NULL,
	[idtrack] [int] NOT NULL,
 CONSTRAINT [PK_Station_Track] PRIMARY KEY CLUSTERED 
(
	[idstation_start] ASC,
	[idstation_stop] ASC,
	[idtrack] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Station_Track]  WITH CHECK ADD  CONSTRAINT [FK_Station_Track_Station] FOREIGN KEY([idstation_start])
REFERENCES [dbo].[Station] ([id])
GO

ALTER TABLE [dbo].[Station_Track] CHECK CONSTRAINT [FK_Station_Track_Station]
GO

ALTER TABLE [dbo].[Station_Track]  WITH CHECK ADD  CONSTRAINT [FK_Station_Track_Station1] FOREIGN KEY([idstation_stop])
REFERENCES [dbo].[Station] ([id])
GO

ALTER TABLE [dbo].[Station_Track] CHECK CONSTRAINT [FK_Station_Track_Station1]
GO

ALTER TABLE [dbo].[Station_Track]  WITH CHECK ADD  CONSTRAINT [FK_Station_Track_Track] FOREIGN KEY([idtrack])
REFERENCES [dbo].[Track] ([id])
GO

ALTER TABLE [dbo].[Station_Track] CHECK CONSTRAINT [FK_Station_Track_Track]
GO
GO


