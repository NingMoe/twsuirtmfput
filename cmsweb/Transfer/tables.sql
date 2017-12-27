CREATE TABLE [CMS_DTC_RELATION] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[FromResId] [bigint] NOT NULL ,
	[FromFieldName] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[FromFieldDescription] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ToResId] [bigint] NOT NULL ,
	[ToFieldName] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ToFieldDescription] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CMS_DTC_RELATION] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


