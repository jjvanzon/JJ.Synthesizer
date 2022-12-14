
CREATE NONCLUSTERED INDEX IX_AudioFileOutput_OutletID ON AudioFileOutput (OutletID ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

ALTER TABLE AudioFileOutput WITH CHECK 
ADD CONSTRAINT FK_AudioFileOutput_Outlet FOREIGN KEY(OutletID)
REFERENCES Outlet (ID)

ALTER TABLE AudioFileOutput CHECK CONSTRAINT FK_AudioFileOutput_Outlet
