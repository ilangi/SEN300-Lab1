use master
--drop database checkoutdb
create database checkoutdb
GO

use checkoutdb
GO
create table checkout(
	id bigint not null IDENTITY(1,1) PRIMARY KEY,
	[name] varchar(1000),
	[ShippingAddress] varchar(1000),
	CreditCardNumber bigint,
	CreditCardExperationMonth int,
	CreditCardExperationYear int,
	CreditCardCVV int
);
GO
insert into checkout ([name],ShippingAddress,CreditCardNumber,CreditCardExperationMonth,CreditCardExperationYear,CreditCardCVV)
VALUES ('John Kaminga','500 North Street', 11122223333, 12, 2024, 888),
('Sammy Soo','900 South Blv', 20230034004, 3, 2023, 432),
('Jay Boog','2001 W 600 N', 30344445555, 1, 2026, 563)

--	Made cart with a reference to checkout in order to store the List<Item>
create table cart
(
	id bigint not null IDENTITY(1,1) PRIMARY KEY,
	checkoutID bigint not null FOREIGN KEY REFERENCES checkout(id),
	title varchar(1000),
	details varchar(1500),
	unit_price decimal,
	quantity bigint
);

insert into cart (title,checkoutID, details, unit_price, quantity)
	VALUES ('Wooden Chair', 1,'The best oak was used for this product', 199.99, 1),
	('Race Chair', 1, 'The best gamers were used for this product', 599.99, 1),
	('Office Chair', 1, 'Relax and recline as you work your deadend 9-5 job', 199.99, 1)

select * from checkout

select * from cart