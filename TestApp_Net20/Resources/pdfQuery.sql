
SELECT 
	 T_VWS_PdfLegende.PL_UID
	,T_VWS_PdfLegende.PL_PS_UID
	,T_VWS_PdfLegende.PL_PLK_UID
	,T_VWS_PdfLegende.PL_Type
	,T_VWS_PdfLegende.PL_Format
	,T_VWS_PdfLegende.PL_X
	,T_VWS_PdfLegende.PL_Y
	,T_VWS_PdfLegende.PL_W
	,T_VWS_PdfLegende.PL_H
	,T_VWS_PdfLegende.PL_Angle
	,T_VWS_PdfLegende.PL_AlignH
	,T_VWS_PdfLegende.PL_AlignV
	,T_VWS_PdfLegende.PL_Text_DE
	,T_VWS_PdfLegende.PL_Text_FR
	,T_VWS_PdfLegende.PL_Text_IT
	,T_VWS_PdfLegende.PL_Text_EN
	,T_VWS_PdfLegende.PL_Outline
	,T_VWS_PdfLegende.PL_Style
	,T_VWS_PdfLegende.PL_DataBind
	,T_VWS_PdfLegende.PL_Sort
	 
	,T_VWS_Ref_PdfLegendenKategorie.PLK_Name
	,T_VWS_Ref_PdfLegendenKategorie.PLK_IsDefault
	 
	,T_VWS_Ref_PaperSize.PS_Lang_DE
	,T_VWS_Ref_PaperSize.PS_Lang_FR
	,T_VWS_Ref_PaperSize.PS_Lang_IT
	,T_VWS_Ref_PaperSize.PS_Lang_EN
	,T_VWS_Ref_PaperSize.PS_Width_mm
	,T_VWS_Ref_PaperSize.PS_Height_mm
	,T_VWS_Ref_PaperSize.PS_Sort
	 
	,T_VWS_Ref_Darstellung.DAR_UID
	,T_VWS_Ref_Darstellung.DAR_DAR_UID
	,T_VWS_Ref_Darstellung.DAR_SVG_UID
	,T_VWS_Ref_Darstellung.DAR_Spread_UID
	,T_VWS_Ref_Darstellung.DAR_Lang_DE
	,T_VWS_Ref_Darstellung.DAR_Lang_FR
	,T_VWS_Ref_Darstellung.DAR_Lang_IT
	,T_VWS_Ref_Darstellung.DAR_Lang_EN
	,T_VWS_Ref_Darstellung.DAR_isHairline
	,T_VWS_Ref_Darstellung.DAR_isBlack
	,T_VWS_Ref_Darstellung.DAR_isDefault
	,T_VWS_Ref_Darstellung.DAR_Sort
	,T_VWS_Ref_Darstellung.DAR_LI_UID
FROM T_VWS_PdfLegende 

LEFT JOIN T_VWS_Ref_PdfLegendenKategorie
	ON T_VWS_Ref_PdfLegendenKategorie.PLK_UID = T_VWS_PdfLegende.PL_PLK_UID 
	AND T_VWS_Ref_PdfLegendenKategorie.PLK_Status = 1 

LEFT JOIN T_VWS_Ref_PaperSize 
	ON T_VWS_Ref_PaperSize.PS_UID = T_VWS_PdfLegende.PL_PS_UID 
	AND T_VWS_Ref_PaperSize.PS_Status = 1 

LEFT JOIN T_VWS_ZO_Ref_Darstellung_PdfLegendenKategorie 
	ON T_VWS_ZO_Ref_Darstellung_PdfLegendenKategorie.ZO_Darstellung_PdfLegendenKategorie_PLK_UID = T_VWS_PdfLegende.PL_PLK_UID 
	AND ZO_Darstellung_PdfLegendenKategorie_Status = 1 

LEFT JOIN T_VWS_Ref_Darstellung 
	ON T_VWS_Ref_Darstellung.DAR_UID = T_VWS_ZO_Ref_Darstellung_PdfLegendenKategorie.ZO_Darstellung_PdfLegendenKategorie_DAR_UID 
	AND T_VWS_Ref_Darstellung.DAR_Status = 1 

