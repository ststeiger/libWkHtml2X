
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_VWS_Ref_PdfLegendenKategorie]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_VWS_Ref_PdfLegendenKategorie](
	[PLK_UID] [uniqueidentifier] NOT NULL,
	[PLK_Name] [varchar](255) NULL,
	[PLK_IsDefault] [bit] NULL,
	[PLK_Status] [int] NULL,
	 CONSTRAINT [PK_T_VWS_Ref_PdfLegendenKategorie] PRIMARY KEY CLUSTERED 
	(
		[PLK_UID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_VWS_PdfLegende]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_VWS_PdfLegende](
	[PL_UID] [uniqueidentifier] NOT NULL,
	[PL_PS_UID] [uniqueidentifier] NULL,
	[PL_PLK_UID] [uniqueidentifier] NULL,
	[PL_Type] [varchar](255) NULL,
	[PL_Format] [varchar](255) NULL,
	[PL_X] [float] NULL,
	[PL_Y] [float] NULL,
	[PL_W] [float] NULL,
	[PL_H] [float] NULL,
	[PL_Angle] [float] NULL,
	[PL_AlignH] [nvarchar](255) NULL,
	[PL_AlignV] [nvarchar](255) NULL,
	[PL_Text_DE] [nvarchar](255) NULL,
	[PL_Text_FR] [nvarchar](255) NULL,
	[PL_Text_IT] [nvarchar](255) NULL,
	[PL_Text_EN] [nvarchar](255) NULL,
	[PL_Outline] [bit] NULL,
	[PL_Style] [varchar](8000) NULL,
	[PL_DataBind] [nvarchar](255) NULL,
	[PL_Sort] [int] NULL,
	[PL_Status] [int] NULL,
	 CONSTRAINT [PK_T_VWS_PdfLegende] PRIMARY KEY CLUSTERED 
	(
		[PL_UID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_VWS_ZO_Ref_Darstellung_PdfLegendenKategorie]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[T_VWS_ZO_Ref_Darstellung_PdfLegendenKategorie](
	[ZO_Darstellung_PdfLegendenKategorie_UID] [uniqueidentifier] NOT NULL,
	[ZO_Darstellung_PdfLegendenKategorie_DAR_UID] [uniqueidentifier] NULL,
	[ZO_Darstellung_PdfLegendenKategorie_PLK_UID] [uniqueidentifier] NULL,
	[ZO_Darstellung_PdfLegendenKategorie_Status] [int] NULL,
	 CONSTRAINT [PK_T_VWS_ZO_Ref_Darstellung_PdfLegendenKategorie] PRIMARY KEY CLUSTERED 
	(
		[ZO_Darstellung_PdfLegendenKategorie_UID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
