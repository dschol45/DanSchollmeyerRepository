use Hotel
go

--1
select
*
from Reservation
where EndDate = '2019-08-25'
go

--2
select *
from Room
join ReservationRoom on Room.RoomID = ReservationRoom.RoomID
join ReservationGuest on ReservationRoom.ReservationID = ReservationGuest.ReservationID
where ReservationGuest.GuestID = 1

--3
select *
from Room
where room.RoomID not in(
select room.RoomID
from Room
join RoomAmenity ra on Room.RoomID = ra.RoomID
where ra.AmenityID = 2)

--4
--select *
--from Room r
--where r.RoomID not in(
--select r.RoomID
--from Room r
--join ReservationRoom rr on r.RoomID = rr.RoomID
--join Reservation res on rr.ReservationID = res.ReservationID
--where
