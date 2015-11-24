CREATE VIEW [dbo].[客戶清單]
	AS
		SELECT
			[客戶資料].[Id],
			[客戶資料].[客戶名稱],
			COUNT([客戶聯絡人].[客戶Id]) AS [聯絡人數量],
			COUNT([客戶銀行資訊].[客戶Id]) AS [銀行帳戶數量]
		FROM
			[客戶資料]
		LEFT JOIN
			[客戶聯絡人]
		ON
			[客戶資料].[Id] = [客戶聯絡人].[客戶Id]
		LEFT JOIN
			[客戶銀行資訊]
		ON
			[客戶資料].[Id] = [客戶銀行資訊].[客戶Id]
		GROUP BY
			[客戶資料].[Id],
			[客戶資料].[客戶名稱]