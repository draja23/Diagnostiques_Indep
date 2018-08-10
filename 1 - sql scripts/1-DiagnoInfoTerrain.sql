Create database [DiagnoInfoTerrain]
GO

USE [DiagnoInfoTerrain]
GO

/****** Object:  Table [dbo].[secteur]    Script Date: 27/06/2018 15:23:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[utilisateur](
	[util_id] [int]  NOT NULL IDENTITY(1,1),
	[source_id] [int] NOT NULL,
	[util_nom] [varchar](100) NULL,
	[util_prenom] [varchar](100) NULL
PRIMARY KEY CLUSTERED 
(
	[util_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[secteur](
	[sect_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[sect_nom] [varchar](100) NULL	
PRIMARY KEY CLUSTERED 
(
	[sect_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[catalogue](
	[catal_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[catal_principal] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[catal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[catalogue_autre](
	[catal_autre_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[catal_autre_principal] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[catal_autre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[version](
	[vers_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[vers_nom] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[vers_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[commande](
	[com_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[com_premier_date_id] INT NULL,
	[com_premier_date] [datetime] NULL,
	[com_dernier_date_id] INT NULL,
	[com_derniere_date] [datetime] NULL,
	[com_premier_facture_date_id] INT NULL,
	[com_premier_facture_date] [datetime] NULL,
	[com_dernier_facture_date_id] INT NULL,
	[com_derniere_facture_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[com_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[visite](
	[vis_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[vis_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[vis_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[table_nom_comptage](
	[table_nc_id] [int] NOT NULL IDENTITY(1,1),	
	[id_util] [int] NOT NULL FOREIGN KEY REFERENCES utilisateur(util_id),
	[tab_nom] [varchar](255) NULL,	
	[tab_ligne_comptage] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[table_nc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[table_index](
	[table_index_id] [int] NOT NULL IDENTITY(1,1),	
	[id_nc_table] [int] NOT NULL FOREIGN KEY REFERENCES table_nom_comptage(table_nc_id),
	[tab_nom_index] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[table_index_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[login_utilisateur](
	[id_utilisateur] [int] IDENTITY(1,1) NOT NULL,
	[nom_util] [varchar](50) NOT NULL,
	[prenom_util] [varchar](50) NOT NULL,
	[mail_util] [varchar](100) NOT NULL,
	[mdp_util] [varchar](50) NOT NULL,
 CONSTRAINT [PK_login_utilisateur] PRIMARY KEY CLUSTERED 
(
	[id_utilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [login_utilisateur] ([nom_util] , [prenom_util] , [mail_util] ,[mdp_util] ) VALUES ('deiva','raja','drfr@gmail.com','123456')
INSERT INTO [login_utilisateur] ([nom_util] , [prenom_util] , [mail_util] ,[mdp_util] ) VALUES ('deiva2','raja2','drfr2@gmail.com','1234562')

--CREATE PROCEDURE dbo.GetTableInfo 
--AS 
--SELECT o.name AS Table_Name, i.name AS Index_Name, SUM(p.[rows]) AS [TotalRowCount]
--FROM sys.indexes i
--INNER JOIN sys.objects o ON i.object_id = o.object_id
--INNER JOIN sys.schemas sc ON o.schema_id = sc.schema_id
--inner join sys.partitions AS p ON o.[object_id] = p.[object_id] AND p.index_id IN ( 0, 1 )
--WHERE i.name IS NOT NULL
--AND o.type = 'U' 
--GROUP BY o.name, i.name, p.[rows]

--GO

--Scaffold-DbContext "Server=NDV1289\MSSQLSERVER1;Database=DiagnoInfoTerrain;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables visite, commande, version,catalogue_autre, catalogue,secteur,utilisateur

--Scaffold-DbContext "Server=NDV1289\MSSQLSERVER1;Database=DiagnoInfoTerrain;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -FORCE

--Scaffold-DbContext "Server=NDV1289\MSSQLSERVER1;Database=DiagnoInfoTerrain;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelLogin -Tables login_utilisateur



--"destinationConnection": "Server=NDV1289;Database=DiagnoInfoTerrain;User Id=sa;Password=Draja2310@@;",
--"sourceConnection": "Server=NDV1289;Database=DiagnoInfoTerrainSource;User Id=sa;Password=Draja2310@@;"


--    "sourceConnection": "Server=NDV1289\\MSSQLSERVER1;Database=DiagnoInfoTerrainSource;Trusted_Connection=True;",
--    "destinationConnection": "Server=NDV1289\\MSSQLSERVER1;Database=DiagnoInfoTerrain;Trusted_Connection=True;"

--Scaffold-DbContext "Server=NDV1289;Database=DiagnoInfoTerrain;User Id=sa;Password=Draja2310@@;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelLogin -Tables login_utilisateur
--Scaffold-DbContext "Server=NDV1289;Database=DiagnoInfoTerrainSource;User Id=sa;Password=Draja2310@@;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelSource -FORCE
--Scaffold-DbContext "Server=NDV1289;Database=DiagnoInfoTerrain;User Id=sa;Password=Draja2310@@;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelDestination -FORCE