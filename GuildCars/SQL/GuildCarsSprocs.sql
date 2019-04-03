USE GuildCars
GO

DROP PROCEDURE IF EXISTS GetUsers
GO

CREATE PROCEDURE GetUsers
AS
SELECT 
anu.Id as UserID,
anu.Email,
anr.[Name] AS [Role],
claimFirst.ClaimValue as FirstName,
claimLast.ClaimValue as LastName
FROM AspNetUsers anu
INNER JOIN AspNetUserClaims claimFirst on anu.Id = claimFirst.UserId AND claimFirst.ClaimType = 'FirstName'
INNER JOIN AspNetUserClaims claimLast on anu.Id = claimLast.UserId AND claimLast.ClaimType = 'LastName'
INNER JOIN AspNetUserRoles anur on anu.Id = anur.UserId
INNER JOIN AspNetRoles anr on anur.RoleId = anr.Id
GO

------------------------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS GetUserById
GO

CREATE PROCEDURE GetUserById
(@UserID nvarchar(128))
AS
SELECT 
anu.Id as UserID,
anu.Email,
anr.[Name] AS [Role],
claimFirst.ClaimValue as FirstName,
claimLast.ClaimValue as LastName
FROM AspNetUsers anu
INNER JOIN AspNetUserClaims claimFirst on anu.Id = claimFirst.UserId AND claimFirst.ClaimType = 'FirstName'
INNER JOIN AspNetUserClaims claimLast on anu.Id = claimLast.UserId AND claimLast.ClaimType = 'LastName'
INNER JOIN AspNetUserRoles anur on anu.Id = anur.UserId
INNER JOIN AspNetRoles anr on anur.RoleId = anr.Id
WHERE anu.Id = @UserID
GO

------------------------------------------------------------------------------------------
