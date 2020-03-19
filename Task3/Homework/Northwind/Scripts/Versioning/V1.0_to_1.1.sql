IF EXISTS (SELECT * FROM sys.objects 
	WHERE OBJECT_ID = OBJECT_ID(N'[Northwind].[CreditCards]') AND type IN (N'U'))
BEGIN
	DROP TABLE [Northwind].[CreditCards]
END

CREATE TABLE [Northwind].[CreditCards](
		[CardNumber] NVARCHAR(16) NOT NULL,
		[ExpireDate] DATE NOT NULL,
		[CardHolder] NVARCHAR(255) NOT NULL,
		[EmployeeID] INT NOT NULL,
		CONSTRAINT [PK_CardNumber] PRIMARY KEY ([CardNumber] ASC),
		CONSTRAINT [FK_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Northwind].[Employees]([EmployeeID])
)