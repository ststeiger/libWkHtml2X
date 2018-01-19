
DECLARE @BE_ID int 
SET @BE_ID = 12435 -- BE_Hash 

-- SELECT * FROM T_Benutzer 

DECLARE @in_svg uniqueidentifier 
DECLARE @in_dar_uid uniqueidentifier 
SET @in_svg = 'C0A7720B-B6D9-46E0-84C4-2D2C6990EC7C' -- EG
SET @in_svg = 'B558EB3D-52F0-4F38-9F2F-39620D9C674A' -- OG1
SET @in_dar_uid = '71C16DAD-DADB-4BA2-A380-7ED3029DEEE0'  


-- SELECT * FROM T_VWS_SVG WHERE SVG_SVG_UID IS NULL AND CURRENT_TIMESTAMP >= SVG_dateCreated AND SVG_dateDeleted IS NULL 



SELECT 
	 -- T_VWS_SVG.SVG_UID
	 T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_SVG_UID AS SVG_UID 
	--,T_VWS_SVG.SVG_SVG_UID
	--,T_VWS_SVG.SVG_Content
	--,T_VWS_SVG.SVG_UNI_UID
	--,T_VWS_SVG.SVG_toUNI
	--,T_VWS_SVG.SVG_Area
	--,T_VWS_SVG.SVG_CentroidX
	--,T_VWS_SVG.SVG_CentroidY
	--,T_VWS_SVG.SVG_dateCreated
	--,T_VWS_SVG.SVG_dateDeleted
	--,T_VWS_SVG.SVG_BEDeleted
	--,T_VWS_SVG._SVG_Handle


	-- ,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_PR_UID -- T_Premises
	-- ,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_FL_UID -- T_Floor
	-- ,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_RM_UID -- T_Room
	-- ,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_WP_UID -- T_Workplace
	-- ,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_AR_UID -- T_Art
	--,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_PN_UID -- T_Printer
	--,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_IN_UID -- T_Inventory
	--,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_FU_UID -- T_Furniture 
	--,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_WI_UID -- T_Window
	--,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_SVG_UID
	--,T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_OBJ_UID


	,T_AP_Standort.SO_UID
	,T_AP_Standort.SO_Nr 
	,T_AP_Standort.SO_Bezeichnung

	,T_AP_Standort.SO_IsSchnittstelleWP
	,T_AP_Ref_Freigabetyp.FGT_UID
	,T_AP_Ref_Freigabetyp.FGT_Code
	
	,
	CASE T_Benutzer.BE_Language 
		WHEN 'FR' THEN T_AP_Ref_Freigabetyp.FGT_Kurz_FR 
		WHEN 'IT' THEN T_AP_Ref_Freigabetyp.FGT_Kurz_IT 
		WHEN 'EN' THEN T_AP_Ref_Freigabetyp.FGT_Kurz_EN 
		ELSE T_AP_Ref_Freigabetyp.FGT_Kurz_DE
	END FGT_Kurz 

	,
	CASE T_Benutzer.BE_Language 
		WHEN 'FR' THEN T_AP_Ref_Freigabetyp.FGT_Lang_FR 
		WHEN 'IT' THEN T_AP_Ref_Freigabetyp.FGT_Lang_IT 
		WHEN 'EN' THEN T_AP_Ref_Freigabetyp.FGT_Lang_EN 
		ELSE T_AP_Ref_Freigabetyp.FGT_Lang_DE
	END FGT_Lang  
	

	,T_AP_Gebaeude.GB_Nr 
	,T_AP_Gebaeude.GB_Bezeichnung 
	
	,T_AP_Geschoss.GS_Nr
	,T_AP_Geschoss.GS_Bezeichnung 
	,T_AP_Geschoss.GS_IsAussengeschoss 
	,T_AP_Ref_Geschosstyp.GST_UID 
	
	
	,
	CASE T_Benutzer.BE_Language 
		WHEN 'FR' THEN T_AP_Ref_Geschosstyp.GST_Kurz_FR 
		WHEN 'IT' THEN T_AP_Ref_Geschosstyp.GST_Kurz_IT 
		WHEN 'EN' THEN T_AP_Ref_Geschosstyp.GST_Kurz_EN 
		ELSE T_AP_Ref_Geschosstyp.GST_Kurz_DE
	END GST_Kurz 
	
	,
	CASE T_Benutzer.BE_Language 
		WHEN 'FR' THEN T_AP_Ref_Geschosstyp.GST_Lang_FR 
		WHEN 'IT' THEN T_AP_Ref_Geschosstyp.GST_Lang_IT 
		WHEN 'EN' THEN T_AP_Ref_Geschosstyp.GST_Lang_EN 
		ELSE T_AP_Ref_Geschosstyp.GST_Lang_DE
	END GST_Lang 

	,ROUND(T_ZO_AP_Geschoss_Flaeche.ZO_GSFlaeche_Flaeche, 2) AS ZO_GSFlaeche_Flaeche 


	,
	CASE T_Benutzer.BE_Language 
		WHEN 'FR' THEN T_VWS_Ref_Darstellung.DAR_Lang_FR
		WHEN 'IT' THEN T_VWS_Ref_Darstellung.DAR_Lang_IT
		WHEN 'EN' THEN T_VWS_Ref_Darstellung.DAR_Lang_EN
		ELSE T_VWS_Ref_Darstellung.DAR_Lang_DE
	END Darstellung 



--FROM T_VWS_SVG

--LEFT JOIN T_VWS_ZO_SVG_AP_Objekt
--	ON T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_SVG_UID = T_VWS_SVG.SVG_UID 
--	-- AND CURRENT_TIMESTAMP BETWEEN T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateFrom AND T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateTo 
--	AND T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateFrom <= CURRENT_TIMESTAMP 
--	AND 
--	(
--		T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateTo IS NULL 
--		OR 
--		T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateTo >= CURRENT_TIMESTAMP
--	)
FROM T_VWS_ZO_SVG_AP_Objekt 

LEFT JOIN T_VWS_Ref_Darstellung 
	ON T_VWS_Ref_Darstellung.DAR_UID = @in_dar_uid 

LEFT JOIN T_AP_Geschoss 
	ON T_AP_Geschoss.GS_UID = T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_GS_UID 

LEFT JOIN T_AP_Gebaeude 
	ON 
	(
		T_AP_Gebaeude.GB_UID = T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_GB_UID 
		OR 
		GB_UID = GS_GB_UID 
	)
	
INNER JOIN T_AP_Standort  
	ON 
	(
		T_AP_Standort.SO_UID = T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_SO_UID 
		OR 
		T_AP_Standort.SO_UID = T_AP_Gebaeude.GB_SO_UID 
	) 
	-- AND T_AP_Standort.SO_Status = 1 
	-- AND CONVERT(char(8), {fn now()}, 112) BETWEEN CONVERT(char(8), T_AP_Standort.SO_DatumVon, 112) AND T_AP_Standort.SO_DatumBis 

LEFT JOIN T_AP_Ref_Freigabetyp
	ON T_AP_Ref_Freigabetyp.FGT_Code = T_AP_Standort.SO_IsSchnittstelleWP 
	AND T_AP_Ref_Freigabetyp.FGT_Status = 1 

LEFT JOIN T_AP_Ref_Geschosstyp 
	ON T_AP_Ref_Geschosstyp.GST_UID = T_AP_Geschoss.GS_GST_UID 

LEFT JOIN T_ZO_AP_Geschoss_Flaeche 
	ON T_ZO_AP_Geschoss_Flaeche.ZO_GSFlaeche_GS_UID = GS_UID 
	AND T_ZO_AP_Geschoss_Flaeche.ZO_GSFlaeche_Status = 1 
	AND CONVERT(char(8), CURRENT_TIMESTAMP, 112) BETWEEN CONVERT(char(8), T_ZO_AP_Geschoss_Flaeche.ZO_GSFlaeche_DatumVon, 112) AND T_ZO_AP_Geschoss_Flaeche.ZO_GSFlaeche_DatumBis 
	
LEFT JOIN T_AP_Ref_Ort 
	ON T_AP_Ref_Ort.ORT_UID = T_AP_Standort.SO_ORT_UID 

LEFT JOIN T_AP_Ref_Region 
	ON T_AP_Ref_Region.RG_UID = T_AP_Ref_Ort.ORT_RG_UID 

LEFT JOIN T_AP_Ref_Land 
	ON T_AP_Ref_Land.LD_UID = T_AP_Ref_Region.RG_LD_UID 

LEFT JOIN T_AP_Ref_Landesteile 
	ON T_AP_Ref_Landesteile.LT_UID = T_AP_Ref_Land.LD_LT_UID 

LEFT JOIN T_Benutzer 
	ON CAST(BE_ID AS varchar(50)) = CAST(@BE_ID AS varchar(50))
	OR BE_Hash = CAST(@BE_ID AS varchar(50))

-- WHERE T_VWS_SVG.SVG_UID = @in_svg 
WHERE T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_SVG_UID = @in_svg 
