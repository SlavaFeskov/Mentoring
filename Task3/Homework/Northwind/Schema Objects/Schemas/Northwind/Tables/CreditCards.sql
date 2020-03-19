CREATE TABLE [Northwind].[CreditCards](
		[CardNumber] NVARCHAR(16) NOT NULL,
		[ExpireDate] DATE NOT NULL,
		[CardHolder] NVARCHAR(255) NOT NULL,
		[EmployeeID] INT NOT NULL,
		CONSTRAINT [PK_CardNumber] PRIMARY KEY ([CardNumber] ASC),
		CONSTRAINT [FK_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Northwind].[Employees]([EmployeeID])
)
