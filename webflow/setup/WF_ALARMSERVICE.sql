CREATE TABLE [WF_ALARMSERVICE] (
	[ID] [bigint] NOT NULL ,
	[FLOWINSTID] [bigint] NOT NULL ,
	[ACTINSTID] [bigint] NOT NULL ,
	[WORKLISTITEMID] [bigint] NOT NULL ,
	[USERCODE] [nvarchar] (50),
	[USERNAME] [nvarchar] (50),
	[EXECDATE] [datetime] NOT NULL ,
	[COMMENTS] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	CONSTRAINT [PK_ALARMSERVICE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
) ON [PRIMARY]


