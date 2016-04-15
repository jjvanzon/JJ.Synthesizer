drop index Curve.IX_Curve_XDimensionID
drop index Curve.IX_Curve_YDimensionID
alter table Curve drop constraint FK_Curve_XDimension
alter table Curve drop constraint FK_Curve_YDimension
alter table Curve drop column XDimensionID
alter table Curve drop column YDimensionID