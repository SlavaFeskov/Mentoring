IF NOT EXISTS (SELECT * FROM sys.objects
	WHERE OBJECT_ID = OBJECT_ID(N'[Northwind].[Regions]') AND type in (N'U'))
AND EXISTS (SELECT * FROM sys.objects
	WHERE OBJECT_ID = OBJECT_ID(N'[Northwind].[Region]') AND type in (N'U'))
BEGIN
	EXEC SP_RENAME '[Northwind].[Region]', 'Regions'
END

IF NOT EXISTS (SELECT * FROM sys.columns 
	WHERE OBJECT_ID = OBJECT_ID(N'[Northwind].[Customers]') AND Name = N'CreationDate')
BEGIN 
	ALTER TABLE [Northwind].[Customers]
	ADD [CreationDate] DATE NULL
END