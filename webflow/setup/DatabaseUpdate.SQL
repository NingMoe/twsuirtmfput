CREATE TABLE [DatabaseUpdate] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Date] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[Name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[Version] [int] NULL ,
	CONSTRAINT [PK_DatabaseUpdate] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
) ON [PRIMARY]



