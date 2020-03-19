ALTER TABLE [Northwind].[Territories]
    ADD CONSTRAINT [FK_Territories_Region] FOREIGN KEY ([RegionID]) REFERENCES [Northwind].[Regions] ([RegionID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

