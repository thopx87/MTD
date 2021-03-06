USE [MTD]
GO
/****** Object:  User [mtd]    Script Date: 06/23/2018 22:13:31 ******/
CREATE USER [mtd] FOR LOGIN [mtd] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[tblWords]    Script Date: 06/23/2018 22:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](2000) NOT NULL,
	[Value] [ntext] NOT NULL,
	[Dict_Type] [int] NOT NULL,
	[State] [int] NOT NULL,
	[Update_By] [int] NOT NULL,
	[Update_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_tblWords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Đang hoạt động, 0: Không hoạt động' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWords', @level2type=N'COLUMN',@level2name=N'State'
GO
/****** Object:  Table [dbo].[tblWordHistory]    Script Date: 06/23/2018 22:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWordHistory](
	[Word_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[IsDisplay] [int] NOT NULL,
	[Update_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_tblWordHistory] PRIMARY KEY CLUSTERED 
(
	[Word_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trạng thái hiển thị. 1: Hiển thị, 0: Không hiển thị' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblWordHistory', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
/****** Object:  Table [dbo].[tblRole]    Script Date: 06/23/2018 22:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRole](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Text] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tblRole] ([Id], [Code], [Text]) VALUES (1, N'Member', N'Thành viên thường')
INSERT [dbo].[tblRole] ([Id], [Code], [Text]) VALUES (777, N'Admin', N'Quản lý')
/****** Object:  Table [dbo].[tblDicts]    Script Date: 06/23/2018 22:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDicts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](200) NOT NULL,
	[State] [int] NOT NULL,
	[Update_By] [int] NULL,
	[Update_Date] [datetime] NULL,
 CONSTRAINT [PK_DictType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Đang hoạt động, 0: không hoạt động' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblDicts', @level2type=N'COLUMN',@level2name=N'State'
GO
/****** Object:  Table [dbo].[tblAccount]    Script Date: 06/23/2018 22:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Register_Date] [datetime] NOT NULL,
	[Active_Code] [nvarchar](200) NOT NULL,
	[Active_Date] [datetime] NOT NULL,
	[State] [smallint] NOT NULL,
	[Del_Flag] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_tblAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblAccount] ON
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (3, N'nguyendinhphu1993', N'nguyendinhphu1993@gmail.com', N'abc123', CAST(0x0000A90800B4DCC2 AS DateTime), N'e07f3add-1d98-4745-86cc-bb851e7e9960', CAST(0x0000A90800B4DCC2 AS DateTime), 1, 0, 777)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (4, N'lientran', N'lientran@gmail.com', N'abc123', CAST(0x0000A90800B61A16 AS DateTime), N'19bddd48-5c62-47b8-8fd2-96a025b3c602', CAST(0x0000A90800B61A16 AS DateTime), 1, 0, 1)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (5, N'abcdef1', N'abcdef1@gmail.com', N'abc123', CAST(0x0000A90800B7A495 AS DateTime), N'643f6258-573e-49f7-8ce4-86c2a5ad6640', CAST(0x0000A90800B7A495 AS DateTime), 1, 1, 1)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (6, N'abcdef2', N'abcdef2@gmail.com', N'abc123', CAST(0x0000A90800B80E9C AS DateTime), N'9b9c0c77-ba05-47ea-8d54-b14393005741', CAST(0x0000A90800B80E9C AS DateTime), 1, 1, 1)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (7, N'abcdef3', N'abcdef3@gmail.com', N'abc123', CAST(0x0000A90800B86A75 AS DateTime), N'c2133df5-9ea6-48be-b572-20ce1b70e446', CAST(0x0000A90800B86A75 AS DateTime), 0, 0, 777)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (8, N'abcdef4', N'abcdef4@gmail.com', N'abc123', CAST(0x0000A90800B9F739 AS DateTime), N'4c500440-bc8d-400c-9b54-23f91e99ddd6', CAST(0x0000A90800B9F739 AS DateTime), 1, 0, 1)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (9, N'abcdef5', N'abcdef5@gmail.com', N'abc123', CAST(0x0000A908010E380C AS DateTime), N'e2cf6037-e777-42fb-b1d1-9fd6d24dd156', CAST(0x0000A908010E380D AS DateTime), 1, 0, 1)
INSERT [dbo].[tblAccount] ([Id], [UserName], [Email], [Password], [Register_Date], [Active_Code], [Active_Date], [State], [Del_Flag], [RoleId]) VALUES (10, N'abcdef6', N'abcdef6@gmail.com', N'abc123', CAST(0x0000A908013570E4 AS DateTime), N'98de41f6-b525-4bcb-9f96-c4fd3271273b', CAST(0x0000A908013570E4 AS DateTime), 1, 0, 1)
SET IDENTITY_INSERT [dbo].[tblAccount] OFF
/****** Object:  Table [dbo].[DictUserDisplay]    Script Date: 06/23/2018 22:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictUserDisplay](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DictTypeId] [int] NOT NULL,
	[State] [bit] NOT NULL,
	[Color] [nvarchar](50) NOT NULL,
	[SortOrder] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Default [DF_tblDicts_State]    Script Date: 06/23/2018 22:13:30 ******/
ALTER TABLE [dbo].[tblDicts] ADD  CONSTRAINT [DF_tblDicts_State]  DEFAULT ((1)) FOR [State]
GO
/****** Object:  Default [DF_tblWordHistory_IsDisplay]    Script Date: 06/23/2018 22:13:30 ******/
ALTER TABLE [dbo].[tblWordHistory] ADD  CONSTRAINT [DF_tblWordHistory_IsDisplay]  DEFAULT ((1)) FOR [IsDisplay]
GO
/****** Object:  Default [DF_tblWords_State]    Script Date: 06/23/2018 22:13:30 ******/
ALTER TABLE [dbo].[tblWords] ADD  CONSTRAINT [DF_tblWords_State]  DEFAULT ((1)) FOR [State]
GO
