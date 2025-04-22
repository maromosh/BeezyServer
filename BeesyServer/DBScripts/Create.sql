Use master
Go

IF EXISTS (select * from sys.databases WHERE name = N'BeezyDB')
BEGIN

DROP  DATABASE BeezyDB;

END

Create Database BeezyDB
Go
Use BeezyDB
Go

Create Table Users
(
UserId int Primary Key Identity(1,1),
UserName nvarchar(50) Not Null,
UserEmail nvarchar(50) Not Null,
UserPassword nvarchar(50) Unique Not Null,
UserPhone nvarchar(50) Unique Not Null,
UserCity nvarchar(50),
UserAddress nvarchar(50) Not Null,
IsManeger bit Not Null Default 0,
)

Create Table Beekeeper
(
BeeKeeperId int Primary Key REFERENCES Users(UserId),
BeekeeperRadius int Not Null,
BeekeeperKind nvarchar(50) Not Null,
BeekeeperIsActive bit Not Null Default 1,
)

Create Table Report
(
ReportId int Primary Key Identity(1,1),
UserId int Not Null FOREIGN KEY REFERENCES Users(UserId),
BeeKeeperId int FOREIGN KEY REFERENCES Beekeeper(BeeKeeperId),
GooglePlaceID NVARCHAR(100) not null,
[Address] nvarchar(500) not null,
ReportDirectionsExplanation nvarchar(2000) Not Null,
ReportUserNumber nvarchar(50) Not Null,
ReportExplanation nvarchar(2000) Not Null,
[Status] int not null --0 new, 1 in process, 2 done
)


Create Table ReportPictures
(
	PicId int Primary Key Identity(1,1),
	ReportId int foreign key references Report(ReportId)
)

Create Table Workshop
(
WorkshopId int Primary Key Identity(1,1),
BeeKeeperId int Not Null FOREIGN KEY REFERENCES Beekeeper(BeeKeeperId),
WorkshopName nvarchar(100) Not Null,
WorkshopDate DateTime Not Null,
WorkshopPrice nvarchar(100) Not Null,
WorkshopMaxReg int Not Null,
WorkshopDescription nvarchar(4000) Not Null,
)

Create Table WorkshopPic
(
WorkshopPicID int Primary Key Identity(1,1),
WorkshopId int FOREIGN KEY REFERENCES Workshop(WorkshopId),
WorkshopPicEx nvarchar(100),
)

Create Table WorkshopRegisters
(
WorkshopRegisters int Primary Key Identity(1,1),
WorkshopId int FOREIGN KEY REFERENCES Workshop(WorkshopId),
UserId int Not Null FOREIGN KEY REFERENCES Users(UserId),
WorkshopRegistersIsPaid bit Not Null Default 0,
)

Create Table ChatBeekeepers
(
ChatBeekeepers int Primary Key Identity(1,1),
BeeKeeperId int Not Null FOREIGN KEY REFERENCES Beekeeper(BeeKeeperId),
ChatBeekeepersText nvarchar(3000) Not Null,
ChatBeekeepersTIme DateTime Not Null,
ChatBeekeepersPic nvarchar(100),
)

Create Table ChatQuestionsAswers
(
ChatQuestionsAswersId int Primary Key Identity(1,1),
UserId int Not Null FOREIGN KEY REFERENCES Users(UserId),
ChatQuestionsAswersText nvarchar(3000) Not Null,
ChatQuestionsAswersTIme DateTime Not Null,
ChatQuestionsAswersPic nvarchar(50),
)

Create Table ChatPic
(
ChatPicID int Primary Key Identity(1,1),
ChatQuestionsAswersId int FOREIGN KEY REFERENCES ChatQuestionsAswers(ChatQuestionsAswersId),
ChatPicEx nvarchar(50),
)

INSERT INTO Users (UserName, UserEmail, UserPassword, UserPhone, UserCity, UserAddress, IsManeger)
VALUES ('marom hai', 'marom.hai@gmail.com', 'Mm16012008', '0538226255', 'Hod Hashron', 'Tavor Street 2, Hod Hasharon, Israel', 1);
--Tavor Street 2, Hod Hasharon, Israel
--ChIJ3TbABs45HRURfRVJMV7uES4

INSERT INTO Beekeeper (BeeKeeperId, BeekeeperRadius,BeekeeperKind,BeekeeperIsActive)
VALUES (1, 10, 'Organic / biodynamic beekeepers', 1);

INSERT INTO Users (UserName, UserEmail, UserPassword, UserPhone, UserCity, UserAddress, IsManeger)
VALUES ('mosh', 'mosh@gmail.com', '111', '0535226255', 'Hod Hashron', 'HaShahar 57, Raanana, Israel', 0);
--HaShahar 57, Ra'anana, Israel
--ChIJ6b_npYtHHRURbLS99aRqa5k
Go

INSERT INTO Beekeeper (BeeKeeperId, BeekeeperRadius,BeekeeperKind,BeekeeperIsActive)
VALUES (2, 10, 'Non-professional beekeepers', 1);
Go

INSERT INTO Users (UserName, UserEmail, UserPassword, UserPhone, UserCity, UserAddress, IsManeger)
VALUES ('amit batzir', 'amitbatzir55555@gmail.com', 'a123', '0549959870', 'Hod Hashron', 'Golda Meir Street 7, Hod Hasharon, Israel', 0);
--Golda Me'ir Street 7, Hod Hasharon, Israel
--ChIJKcDaqjE4HRURBB9fAeZZtmY
Go

INSERT INTO Beekeeper (BeeKeeperId, BeekeeperRadius,BeekeeperKind,BeekeeperIsActive)
VALUES (3, 20, 'Agricultural beekeepers', 1);
Go

INSERT INTO Users (UserName, UserEmail, UserPassword, UserPhone, UserCity, UserAddress, IsManeger)
VALUES ('ori geva', 'geva.ori1@gmail.com', '111o', '0584038201', 'Hod Hashron', 'Mazal Arieh Street, Hod HaSharon, Israel', 0);
--Mazal Arieh Street, Hod HaSharon, Israel
Go

INSERT INTO Report (UserId, BeeKeeperId, GooglePlaceID, [Address], ReportDirectionsExplanation, ReportUserNumber, ReportExplanation, [Status])
VALUES (3, 1, 'ChIJ0fKgAM45HRURGg90BZ7zyds', 'Tavor Street 4, Hod Hasharon, Israel', 'behind the car', '0538226255', 'the hive is on my backyard and disturbs my neighbors and me', 1);
Go

INSERT INTO Report (UserId, BeeKeeperId, GooglePlaceID, [Address], ReportDirectionsExplanation, ReportUserNumber, ReportExplanation, [Status])
VALUES (2, 1, 'ChIJOW6nyYBMHRUR78bjsxSyBzg', 'Pinsker Street 5, Tel Aviv-Yafo, Israel', 'Its under my house in the street', '0538226255', 'the hive is on the strees in a tree right under my house', 1);
Go

INSERT INTO Report (UserId, GooglePlaceID, [Address], ReportDirectionsExplanation, ReportUserNumber, ReportExplanation, [Status])
VALUES (2, 'ChIJ9c-0GAM2HRURD8FM7U2e7bs', 'Ofer Grand Mall Petach Tikva, Zeev Jabotinsky Street, Petah Tikva, Israel', 'in the back of the resturnt', '0589226255', 'The hive is in the parking lot on the ground floor and needs urgent evacuation.', 0);
Go

INSERT INTO Report (UserId, GooglePlaceID, [Address], ReportDirectionsExplanation, ReportUserNumber, ReportExplanation, [Status])
VALUES (3, 'ChIJtxEDRZA3HRUR0a7jrkQDYLs', 'NONO, Jabotinski Street, Hod HaSharon, Israel', 'Behind the restaurant', '0538226255', 'The hive was located on the tree in the yard.', 0);
Go

--INSERT INTO Report (UserId, BeeKeeperId, GooglePlaceID, [Address], ReportDirectionsExplanation, ReportUserNumber, ReportExplanation, [Status])
--VALUES (3, 1, 'GhIJQWDl0CIeQUARxks3icF8U8A', 'golda meir 5', 'behind the car', '0538226255', 'the hive is on my backyard and disturbs my neighbors and me', 0);
--Go

--INSERT INTO Report (UserId, BeeKeeperId, GooglePlaceID, [Address], ReportDirectionsExplanation, ReportUserNumber, ReportExplanation, [Status])
--VALUES (3, 1, 'GhIJQWDl0CIeQUARxks3icF8U8A', 'golda meir 5', 'behind the car', '0538226255', 'the hive is on my backyard and disturbs my neighbors and me', 0);
--Go

-- Create a login for the admin user
CREATE LOGIN [BeezyAdminLogin] WITH PASSWORD = 'thePassword';
Go

-- Create a user in the BeezyDB database for the login
CREATE USER [BeezyAdminUser] FOR LOGIN [BeezyAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [BeezyAdminUser];
Go


select * from  Beekeeper

--update Beekeeper SET BeekeeperKind=N'דבוראים טיפוליים'

select * from Report


--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=BeezyDB;User ID=BeezyAdminLogin;Password=thePassword;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context BeezyDbContext -DataAnnotations –force

