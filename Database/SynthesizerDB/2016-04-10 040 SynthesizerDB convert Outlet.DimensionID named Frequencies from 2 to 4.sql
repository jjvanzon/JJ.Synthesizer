with OutletsToUpdate (ID, Current_DimensionID, New_DimensionID) as
(
	select 
		o.ID,
		o.DimensionID as Current_DimensionID,
		(select ID from Dimension where Name = 'Frequencies') as New_DimensionID
	from Outlet o
	where DimensionID = (select ID from OutletType where Name = 'Frequencies')
)
--select * from OutletsToUpdate order by Current_DimensionID;
update OutletsToUpdate set Current_DimensionID = New_DimensionID;
