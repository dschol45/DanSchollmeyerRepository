Use GuildCars
go

--Clear seed data
DELETE FROM Special;
DELETE FROM Contact;
DELETE FROM Make;
DELETE FROM Model;
DELETE FROM Vehicle;
DELETE FROM SaleInfo;
GO

--Start Seed Data
--INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed,  TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	--VALUES('00000000-0000-0000-0000-000000000000', 'Dan', 'Schollmeyer', 'test@test.com', 1, 0,  0, 0, 0, 'Test');

--INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed,  TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
--	VALUES('00000000-0000-0000-0000-000000000001', 'Adam', 'Grill', 'test2@test.com', 1, 0,  0, 0, 0, 'Test2');

INSERT INTO AspNetRoles(Id,Name,Discriminator) VALUES
(

SET IDENTITY_INSERT Contact ON
INSERT INTO Contact (ContactId, ContactName, ContactEmail, ContactPhone, ContactMessage)
VALUES (0, 'Bob Dole', 'bd@gmail.com', '111-111-1111', '1AAAA11111A111111'),
    (1, 'Bob Ross', 'br@gmail.com', '222-222-2222', '2BBBB22222B222222')
SET IDENTITY_INSERT Contact OFF
GO

SET IDENTITY_INSERT Make ON
INSERT INTO Make (MakeId, MakeName, MakeDate, UserId)
VALUES (0, 'Ford', '2010-1-1', '00000000-0000-0000-0000-000000000000'),
    (1, 'Chevy', '2020-2-2', '11111111-1111-1111-1111-111111111111')
SET IDENTITY_INSERT Make OFF
GO

SET IDENTITY_INSERT Model ON
INSERT INTO Model (ModelId, MakeId, ModelName, ModelDate, UserId)
VALUES (0, 0, 'F-150', '2010-1-1', '00000000-0000-0000-0000-000000000000'),
    (1, 1, 'Cobalt', '2015-2-2', '11111111-1111-1111-1111-111111111111')
SET IDENTITY_INSERT Model OFF
Go

SET IDENTITY_INSERT Special ON
INSERT INTO Special (SpecialId, SpecialTitle, SpecialNote)
VALUES (0, 'Good', 'A good special'),
    (1, 'Great', 'A great special')
SET IDENTITY_INSERT Special OFF
GO

SET IDENTITY_INSERT Vehicle ON
INSERT INTO Vehicle (VehicleId, MakeId, ModelId, Year, BodyStyle, Transmission, Color, Interior, Mileage, VIN, SalePrice, MSRP, Description, Type, IsFeatured)
VALUES (0, 0, 0, 2000, 'Truck', 'Automatic', 'Red', 'Green', 123456, '1AAAA11111A111111', 1000, 1001, 'In good condition', 'Used',1),
    (1, 1, 1, 2010, 'Car', 'Manual', 'Black', 'Yellow', 100, '2BBBB22222B222222', 5000, 5100, 'In great condition', 'New',1)
SET IDENTITY_INSERT Vehicle OFF
GO

SET IDENTITY_INSERT SaleInfo ON
INSERT INTO SaleInfo (SaleId, UserId, VehicleId, SaleDate, SaleName, SalePhone, SaleEmail, SaleStreet1, SaleStreet2, SaleCity, SaleState, SaleZip, SalePrice, SaleType)
VALUES (0, 0, 0, '2020-1-1', 'Bill', '000-000-0000', 'bill@gmail.com', '123 Fake St', null, 'Faketown', 'Fake', '11111', 999, 'Cash'),
    (1, 1, 1, '2020-2-2', 'Bruce', '111-111-1111', 'bruce@gmail.com', '987 Fake St', null, 'Fakeville', 'Real', '22222', 4999, 'Bank Finance')
SET IDENTITY_INSERT SaleInfo OFF
GO
