Use Hotel
GO

insert into AddOn (Name, Category, Amount) values
('Ball Pit (inflatable)','Entertainment',100),
('Caribou Lou','Alcohol',50),
('Champagne','Alcohol',50)

select * from AddOn

insert into Amenity (AmenityName, AmenityPrice) Values
('Mini-Fridge',null),
('Hot Tub',50.00)

select * from Amenity

insert into Guest (FirstName,LastName,Age,Email,Phone) values
('Dan','Scholly',33,'scholly.dan@asdf.com','612-111-1111'),
('Mister','Dude',18,'dude@asdf.com','612-222-2222'),
('Adam','Grill',30,'adam.grill@asdf.com','612-333-3333'),
('Cameron','Moesta',31,'cameron.moesta@asdf.com','612-444-4444'),
('Eric','Stafford',32,'eric.stafford@asdf.com','612-555-5555')
select* from Guest

insert into Rate (RateName,Rate) values
('King',125),
('Queen',100),
('2 Queen',150)
select * from Rate

insert into Promo (PromoCode,PromoPercent,PromoAmount,PromoExpiration) values
('dollaroff',null,25,null),
('percentoff',.25,null,null),
('free',1,null,null)

select * from Promo

insert into Room (Number,Floor,Limit,RateID) values
(101,1,3,1),
(102,1,3,2),
(103,1,3,2),
(104,1,3,1),
(201,2,4,3),
(202,2,4,3),
(203,2,3,3),
(204,2,4,1)
select*from Room

insert into Reservation (StartDate,EndDate,PromoID) values
('2018-08-18','2018-08-26',null),--1
('2019-08-17','2019-08-25',1),--2
('2019-08-19','2019-08-25',2),--dude
('2019-08-17','2019-08-23',null),
('2019-08-20','2019-08-25',null),
('2019-08-18','2019-08-25',null),
('2020-08-15','2020-08-23',null),--book all rooms
('2021-08-14','2021-08-22',3),
('2022-08-13','2021-08-21',3),
('2023-08-12','2023-08-20',3),
('2024-08-17','2024-08-25',3)

select * from reservation


insert into ReservationGuest (ReservationID,GuestID) values
(1,1),
(2,1),
(3,2),
(4,3),
(5,4),
(6,5),
(7,1),
(7,2),
(7,3),
(7,4),
(7,5),
(8,1),
(9,1),
(10,1),
(11,1)

select * from ReservationGuest

insert into ReservationRoom (ReservationID,RoomID) values
(1,8),
(2,2),
(3,3),
(4,4),
(5,5),
(6,1),
(6,2),
(6,3),
(6,4),
(6,5),
(6,6),
(6,7),
(6,8),
(7,8),
(8,8),
(9,8),
(10,8)
select * from ReservationRoom

insert into ReservationRoomAddOn(ReservationID,RoomID,AddOnID) values
(1,8,1),
(1,8,2),
(2,2,2),
(2,2,3)
select * from ReservationRoomAddOn

insert into RoomAmenity(RoomID,AmenityID) values
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1),
(7,1),
(8,1),
(5,2),
(6,2),
(7,2),
(8,2)
select * from RoomAmenity

insert into Invoice (InvoiceDate,InvoiceStatus,ReservationID) values
('2018-08-26','Paid',1)
select * from Invoice


