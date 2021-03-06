
DECLARE @BE_ID int 
SET @BE_ID = 12435 -- BE_Hash 
-- SELECT * FROM T_Benutzer 

DECLARE @in_svg uniqueidentifier 
DECLARE @in_dar_uid uniqueidentifier 
SET @in_svg = '5BF0A3D6-FF11-47BE-B0AD-47F1DBF81B00' -- EG
SET @in_svg = '185E6006-E700-4223-B530-7D32B18BF46A' -- OG1
SET @in_dar_uid = '71C16DAD-DADB-4BA2-A380-7ED3029DEEE0'  



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


	,T_Ref_Location.LC_Lang_en
	,T_Premises.PR_Name
	
	,ISNULL(T_Ref_FloorType.FT_Lang_en + ' ', '') + CAST(T_Floor.FL_Level AS varchar(10)) AS FloorDisplayString  
	,T_Ref_FloorType.FT_Lang_en
	,T_Floor.FL_Level
	,T_Floor.FL_Sort
	 
	,T_ZO_Premises_DWG.ZO_PRDWG_ApertureDWG
	,T_ZO_Premises_DWG.ZO_PRDWG_ApertureObjID
	 
	,T_ZO_Floor_DWG.ZO_FLDWG_ApertureDWG
	,T_ZO_Floor_DWG.ZO_FLDWG_ApertureObjID
	,T_ZO_Floor_Area.ZO_FLArea_Area

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
--	AND T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateFrom <= CURRENT_TIMESTAMP 
--	AND 
--	( 
--		T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateTo IS NULL 
--		OR 
--		T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_dateTo >= CURRENT_TIMESTAMP 
--	) 

FROM T_VWS_ZO_SVG_AP_Objekt 
	
LEFT JOIN T_Benutzer 
	ON CAST(BE_ID AS varchar(50)) = CAST(@BE_ID AS varchar(50))
	OR BE_Hash = CAST(@BE_ID AS varchar(50))

LEFT JOIN T_VWS_Ref_Darstellung 
	ON T_VWS_Ref_Darstellung.DAR_UID = @in_dar_uid 

LEFT JOIN T_Floor 
	ON T_Floor.FL_UID = T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_FL_UID 
	AND T_Floor.FL_Status = 1 
	AND CURRENT_TIMESTAMP BETWEEN  T_Floor.FL_DateFrom AND T_Floor.FL_DateTo 

LEFT JOIN T_Ref_FloorType
	ON T_Ref_FloorType.FT_UID = T_Floor.FL_FT_UID 
	AND T_Ref_FloorType.FT_Status = 1 

LEFT JOIN T_ZO_Floor_DWG 
	ON T_ZO_Floor_DWG.ZO_FLDWG_FL_UID = T_Floor.FL_UID 
	AND T_ZO_Floor_DWG.ZO_FLDWG_Status = 1 
	AND CURRENT_TIMESTAMP BETWEEN  T_ZO_Floor_DWG.ZO_FLDWG_DateFrom AND T_ZO_Floor_DWG.ZO_FLDWG_DateTo 
	
LEFT JOIN T_ZO_Floor_Area
	ON T_ZO_Floor_Area.ZO_FLArea_FL_UID = T_Floor.FL_UID 
	AND T_ZO_Floor_Area.ZO_FLArea_Status = 1 
	AND CURRENT_TIMESTAMP BETWEEN  T_ZO_Floor_Area.ZO_FLArea_DateFrom AND T_ZO_Floor_Area.ZO_FLArea_DateTo 
	
LEFT JOIN T_Premises 
	ON 
	(
		T_Premises.PR_UID = T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_PR_UID 
		OR 
		T_Premises.PR_UID = T_Floor.FL_PR_UID 
	)
	AND T_Premises.PR_Status = 1 
	AND CURRENT_TIMESTAMP BETWEEN T_Premises.PR_DateFrom AND T_Premises.PR_DateTo 

LEFT JOIN T_Ref_Location 
	ON LC_UID = PR_LC_UID 
	AND T_Ref_Location.LC_Status = 1 

LEFT JOIN T_Ref_Country
	ON T_Ref_Country.CTR_UID = T_Ref_Location.LC_CTR_UID 
	AND T_Ref_Country.CTR_Status = 1 

LEFT JOIN T_Ref_Region
	ON T_Ref_Region.RG_UID = T_Ref_Country.CTR_RG_UID 
	AND T_Ref_Region.RG_Status = 1 
	
LEFT JOIN T_ZO_Premises_DWG
	ON T_ZO_Premises_DWG.ZO_PRDWG_PR_UID = PR_UID 
	AND T_ZO_Premises_DWG.ZO_PRDWG_Status = 1 
	AND CURRENT_TIMESTAMP BETWEEN  T_ZO_Premises_DWG.ZO_PRDWG_DateFrom AND T_ZO_Premises_DWG.ZO_PRDWG_DateTo 

-- WHERE T_VWS_SVG.SVG_UID = @in_svg 
WHERE T_VWS_ZO_SVG_AP_Objekt.ZO_SVGOBJ_SVG_UID = @in_svg 
