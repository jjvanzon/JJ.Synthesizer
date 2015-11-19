alter table Inlet drop constraint FK_Inlet_InletType;
alter table Inlet drop column InletTypeID;
alter table Outlet drop constraint FK_Outlet_OutletType;
alter table Outlet drop column OutletTypeID;
