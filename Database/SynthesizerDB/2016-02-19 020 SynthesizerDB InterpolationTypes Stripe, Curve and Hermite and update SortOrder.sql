	update InterpolationType set SortOrder = 1 where ID = 1;
	update InterpolationType set SortOrder = 3 where ID = 2;

	insert into InterpolationType (ID, Name, SortOrder) values (3, 'Stripe', 2);
	insert into InterpolationType (ID, Name, SortOrder) values (4, 'Curve', 4);
	insert into InterpolationType (ID, Name, SortOrder) values (5, 'Hermite', 5);
