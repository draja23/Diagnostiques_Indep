Create database [DiagnoInfoTerrainSource]
GO

USE [DiagnoInfoTerrainSource]
GO

/****** Object:  Table [dbo].[secteur]    Script Date: 27/06/2018 15:23:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donneesource](
	[col_id] [int] IDENTITY(1,1) NOT NULL,
	[s_util_nom] [varchar](100) NULL,
	[s_util_prenom] [varchar](100) NULL,
	[s_sect_nom] [varchar](100) NULL,
	[s_catal_principal] [varchar](100) NULL,
	[s_catal_autre_principal] [varchar](100) NULL,
	[s_vers_nom] [varchar](100) NULL,
	[s_com_premier_date_id] [int] NULL,
	[s_com_premier_date] [datetime] NULL,
	[s_com_dernier_date_id] [int] NULL,
	[s_com_derniere_date] [datetime] NULL,
	[s_com_premier_facture_date_id] [int] NULL,
	[s_com_premier_facture_date] [datetime] NULL,
	[s_com_dernier_facture_date_id] [int] NULL,
	[s_com_derniere_facture_date] [datetime] NULL,
	[s_vis_date] [datetime] NULL,
	[s_tab_nom] [varchar](5000) NULL,
	[s_tab_ligne_comptage] [varchar](1000) NULL,
	[s_tab_nom_index] [varchar](5000) NULL,
	[est_copie_destination] [bit] NULL,
 CONSTRAINT [PK_Donneesource] PRIMARY KEY CLUSTERED 
(
	[col_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Donneesource] ADD  DEFAULT ((0)) FOR [est_copie_destination]
GO