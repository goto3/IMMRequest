USE [TareaDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdditionalFields]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalFields](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PossibleValues] [nvarchar](max) NULL,
	[FieldType] [nvarchar](max) NULL,
	[TopicTypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_AdditionalFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdditionalFieldsData]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalFieldsData](
	[Id] [uniqueidentifier] NOT NULL,
	[AdditionalFieldId] [uniqueidentifier] NULL,
	[Data] [nvarchar](max) NULL,
	[RequestId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_AdditionalFieldsData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applicants]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applicants](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Applicants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[Id] [uniqueidentifier] NOT NULL,
	[Details] [nvarchar](max) NULL,
	[ApplicantId] [uniqueidentifier] NULL,
	[Status] [nvarchar](max) NULL,
	[StatusDescription] [nvarchar](max) NULL,
	[TopicTypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Token] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [uniqueidentifier] NOT NULL,
	[AreaId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopicTypes]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopicTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[TopicId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_TopicTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14-May-20 7:58:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdditionalFields]  WITH CHECK ADD  CONSTRAINT [FK_AdditionalFields_TopicTypes_TopicTypeId] FOREIGN KEY([TopicTypeId])
REFERENCES [dbo].[TopicTypes] ([Id])
GO
ALTER TABLE [dbo].[AdditionalFields] CHECK CONSTRAINT [FK_AdditionalFields_TopicTypes_TopicTypeId]
GO
ALTER TABLE [dbo].[AdditionalFieldsData]  WITH CHECK ADD  CONSTRAINT [FK_AdditionalFieldsData_AdditionalFields_AdditionalFieldId] FOREIGN KEY([AdditionalFieldId])
REFERENCES [dbo].[AdditionalFields] ([Id])
GO
ALTER TABLE [dbo].[AdditionalFieldsData] CHECK CONSTRAINT [FK_AdditionalFieldsData_AdditionalFields_AdditionalFieldId]
GO
ALTER TABLE [dbo].[AdditionalFieldsData]  WITH CHECK ADD  CONSTRAINT [FK_AdditionalFieldsData_Requests_RequestId] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Requests] ([Id])
GO
ALTER TABLE [dbo].[AdditionalFieldsData] CHECK CONSTRAINT [FK_AdditionalFieldsData_Requests_RequestId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Applicants_ApplicantId] FOREIGN KEY([ApplicantId])
REFERENCES [dbo].[Applicants] ([Id])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Applicants_ApplicantId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_TopicTypes_TopicTypeId] FOREIGN KEY([TopicTypeId])
REFERENCES [dbo].[TopicTypes] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_TopicTypes_TopicTypeId]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Users_UserId]
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Areas_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Areas_AreaId]
GO
ALTER TABLE [dbo].[TopicTypes]  WITH CHECK ADD  CONSTRAINT [FK_TopicTypes_Topics_TopicId] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([Id])
GO
ALTER TABLE [dbo].[TopicTypes] CHECK CONSTRAINT [FK_TopicTypes_Topics_TopicId]
GO
