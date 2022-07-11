drop database itemdb
create database itemdb
GO

use itemdb
GO
create table items(
	id bigint not null IDENTITY(1,1) PRIMARY KEY,
	title varchar(1000),
	details varchar(1500),
	unit_price decimal,
	quantity bigint
);
GO

insert into items (title, details, unit_price, quantity)
	VALUES ('Wooden Chair', 'The best oak was used for this product', 199.99, 51),
	('Race Chair', 'The best gamers were used for this product', 599.99, 22),
	('Office Chair', 'Relax and recline as you work your deadend 9-5 job', 199.99, 253),
	('Kid Chair', 'Children were not used to make this product', 199.99, 54),
	('Stereo Speaker', 'Makes a lot of noise', 49.99, 2555),
	('Car Speaker', 'Let the car do the talking', 299.99, 156),
	('Shower Speaker', 'Blocks out the shower thoughts', 1199.99, 957),
	('Head Speaker', 'Strap the speaker to your ears', 1349.99, 2998),
	('Paper (Single Sheet)','Trees died for this', 1.99, 500959),
	('Green Paper', 'Dead tree dyed green', 2.99, 5550),
	('Yellow Paper', 'Yellow Edition', 3.99, 25001),
	('Red Paper', 'The best oak was used for this product', 4.99, 2552);

select * from items