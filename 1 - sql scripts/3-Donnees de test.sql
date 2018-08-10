USE [DiagnoInfoTerrainSource]
GO

INSERT INTO [dbo].[Donneesource]
           ([s_util_nom],[s_util_prenom],[s_sect_nom],[s_catal_principal]
           ,[s_catal_autre_principal],[s_vers_nom],[s_com_premier_date_id],[s_com_premier_date]
           ,[s_com_dernier_date_id],[s_com_derniere_date],[s_com_premier_facture_date_id],[s_com_premier_facture_date]
           ,[s_com_dernier_facture_date_id],[s_com_derniere_facture_date],[s_vis_date],[s_tab_nom],[s_tab_ligne_comptage],[s_tab_nom_index])
     VALUES
           ('deiva','Rajaram', 'COLOMIERS','DP','Par défaut','5.3.0.0', 
            1,'27-06-2018 10:23:10', 2,'28-06-2018 10:23:10', 
            1,'25-06-2018 10:23:10', 2,'26-06-2018 10:23:10', 
            '25-06-2018 10:25:01',
            'table1;table2;table3;table4', 
            '1;5;6;9', 
            'table1:index1,index2;table2:indexz,indexw')
			GO

INSERT INTO [dbo].[Donneesource]
           ([s_util_nom],[s_util_prenom],[s_sect_nom],[s_catal_principal]
           ,[s_catal_autre_principal],[s_vers_nom],[s_com_premier_date_id],[s_com_premier_date]
           ,[s_com_dernier_date_id],[s_com_derniere_date],[s_com_premier_facture_date_id],[s_com_premier_facture_date]
           ,[s_com_dernier_facture_date_id],[s_com_derniere_facture_date],[s_vis_date],[s_tab_nom],[s_tab_ligne_comptage],[s_tab_nom_index])
     VALUES
           ('deiva2','Raja', 'France','W','Par défaut','5.3.0.0', 
            1,'27-05-2018 10:23:10', 2,'28-05-2018 10:23:10', 
            1,'25-05-2018 10:23:10', 2,'26-05-2018 10:23:10', 
            '25-5-2018 10:25:01',
            'table1;table2;table3;table4', 
            '1;5;6;9', 
            'table1:index1,index2;table2:indexz,indexw;table3:indexzR,indexwR')
			GO

INSERT INTO [dbo].[Donneesource]
           ([s_util_nom],[s_util_prenom],[s_sect_nom],[s_catal_principal]
           ,[s_catal_autre_principal],[s_vers_nom],[s_com_premier_date_id],[s_com_premier_date]
           ,[s_com_dernier_date_id],[s_com_derniere_date],[s_com_premier_facture_date_id],[s_com_premier_facture_date]
           ,[s_com_dernier_facture_date_id],[s_com_derniere_facture_date],[s_vis_date],[s_tab_nom],[s_tab_ligne_comptage],[s_tab_nom_index])
     VALUES
           ('Seb','nico', 'IDF','DP','Par défaut','5.4.0.0', 
            3,'01-07-2018 10:23:10', 4,'02-07-2018 10:23:10', 
            5,'03-07-2018 10:23:10', 6,'04-07-2018 10:23:10', 
            '05-07-2018 10:25:01',
            'table11;table21;table31;table41', 
            '1;5;6;9', 
            'table11:index1,index2;table21:indexz,indexw')
			GO

INSERT INTO [dbo].[Donneesource]
           ([s_util_nom],[s_util_prenom],[s_sect_nom],[s_catal_principal]
           ,[s_catal_autre_principal],[s_vers_nom],[s_com_premier_date_id],[s_com_premier_date]
           ,[s_com_dernier_date_id],[s_com_derniere_date],[s_com_premier_facture_date_id],[s_com_premier_facture_date]
           ,[s_com_dernier_facture_date_id],[s_com_derniere_facture_date],[s_vis_date],[s_tab_nom],[s_tab_ligne_comptage],[s_tab_nom_index])
     VALUES
           ('LEP','NICO', 'ajjaccio','W','Par défaut','5.6.0.0', 
            7,'06-05-2018 10:23:10', 8,'07-05-2018 10:23:10', 
            9,'08-05-2018 10:23:10', 10,'09-05-2018 10:23:10', 
            '10-5-2018 10:25:01',
            'table1;table2;table3;table4;table5', 
            '1;5;6;9;23', 
            'table1:index10,index20;table2:indexz0,indexw0;table3:indexzR0,indexwR0')
			GO