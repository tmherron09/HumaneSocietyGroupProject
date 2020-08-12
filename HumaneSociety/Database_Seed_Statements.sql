-- Initialization
-- Remove before final commit.

CREATE TABLE Employees (EmployeeId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), EmployeeNumber INTEGER, Email VARCHAR(50));
CREATE TABLE Categories (CategoryId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY, RoomNumber INTEGER, AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE AnimalShots (AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ShotId INTEGER FOREIGN KEY REFERENCES Shots(ShotId), DateReceived DATE, CONSTRAINT AnimalShotId PRIMARY KEY (AnimalId, ShotId));
CREATE TABLE USStates (USStateId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Abbreviation VARCHAR(2));
CREATE TABLE Addresses (AddressId INTEGER IDENTITY (1,1) PRIMARY KEY, AddressLine1 VARCHAR(50), City VARCHAR(50), USStateId INTEGER FOREIGN KEY REFERENCES USStates(USStateId),  Zipcode INTEGER); 
CREATE TABLE Clients (ClientId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), AddressId INTEGER FOREIGN KEY REFERENCES Addresses(AddressId), Email VARCHAR(50));
CREATE TABLE Adoptions(ClientId INTEGER FOREIGN KEY REFERENCES Clients(ClientId), AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ApprovalStatus VARCHAR(50), AdoptionFee INTEGER, PaymentCollected BIT, CONSTRAINT AdoptionId PRIMARY KEY (ClientId, AnimalId));

INSERT INTO USStates VALUES('Alabama','AL');
INSERT INTO USStates VALUES('Alaska','AK');
INSERT INTO USStates VALUES('Arizona','AZ');
INSERT INTO USStates VALUES('Arkansas','AR');
INSERT INTO USStates VALUES('California','CA');
INSERT INTO USStates VALUES('Colorado','CO');
INSERT INTO USStates VALUES('Connecticut','CT');
INSERT INTO USStates VALUES('Delaware','DE');
INSERT INTO USStates VALUES('Florida','FL');
INSERT INTO USStates VALUES('Georgia','GA');
INSERT INTO USStates VALUES('Hawaii','HI');
INSERT INTO USStates VALUES('Idaho','ID');
INSERT INTO USStates VALUES('Illinois','IL');
INSERT INTO USStates VALUES('Indiana','IN');
INSERT INTO USStates VALUES('Iowa','IA');
INSERT INTO USStates VALUES('Kansas','KS');
INSERT INTO USStates VALUES('Kentucky','KY');
INSERT INTO USStates VALUES('Louisiana','LA');
INSERT INTO USStates VALUES('Maine','ME');
INSERT INTO USStates VALUES('Maryland','MD');
INSERT INTO USStates VALUES('Massachusetts','MA');
INSERT INTO USStates VALUES('Michigan','MI');
INSERT INTO USStates VALUES('Minnesota','MN');
INSERT INTO USStates VALUES('Mississippi','MS');
INSERT INTO USStates VALUES('Missouri','MO');
INSERT INTO USStates VALUES('Montana','MT');
INSERT INTO USStates VALUES('Nebraska','NE');
INSERT INTO USStates VALUES('Nevada','NV');
INSERT INTO USStates VALUES('New Hampshire','NH');
INSERT INTO USStates VALUES('New Jersey','NJ');
INSERT INTO USStates VALUES('New Mexico','NM');
INSERT INTO USStates VALUES('New York','NY');
INSERT INTO USStates VALUES('North Carolina','NC');
INSERT INTO USStates VALUES('North Dakota','ND');
INSERT INTO USStates VALUES('Ohio','OH');
INSERT INTO USStates VALUES('Oklahoma','OK');
INSERT INTO USStates VALUES('Oregon','OR');
INSERT INTO USStates VALUES('Pennsylvania','PA');
INSERT INTO USStates VALUES('Rhode Island','RI');
INSERT INTO USStates VALUES('South Carolina','SC');
INSERT INTO USStates VALUES('South Dakota','SD');
INSERT INTO USStates VALUES('Tennessee','TN');
INSERT INTO USStates VALUES('Texas','TX');
INSERT INTO USStates VALUES('Utah','UT');
INSERT INTO USStates VALUES('Vermont','VT');
INSERT INTO USStates VALUES('Virginia','VA');
INSERT INTO USStates VALUES('Washington','WA');
INSERT INTO USStates VALUES('West Virgina','WV');
INSERT INTO USStates VALUES('Wisconsin','WI');
INSERT INTO USStates VALUES('Wyoming','WY');





-- Categories

INSERT INTO Categories
VALUES ('Dog');

INSERT INTO Categories
VALUES ('Cat');

INSERT INTO Categories
VALUES ('Critters');

INSERT INTO Categories
VALUES ('Farm');

INSERT INTO Categories
VALUES ('Other');

-- Diet Plans

INSERT INTO DietPlans
Values ('dry food', 'kibble', 2);

INSERT INTO DietPlans
Values ('wet food', 'wet food', 1);

INSERT INTO DietPlans
Values ('organic', 'mixed leaf', 1);

INSERT INTO DietPlans
Values ('farm', 'farm feed', 10);

INSERT INTO DietPlans
Values ('high risk', 'special blend', 1);

-- Employees

INSERT INTO Employees
Values ('Arnold', 'Rimmer', 'AceRimmer', 'kipper', 1001, 'AJRimmer@JMC.com');

INSERT INTO Employees
Values ('John', 'Wick', 'DogLover5', 'billAndted', 1002, 'Excellent@Adventure.com');

INSERT INTO Employees
Values ('Tim', 'Herron', 'Timsy', 'Magnus', 1003, 'tmherron09@gmail.com');

INSERT INTO Employees
Values ('Choua', 'Cha', 'Chouac1', 'Chacha123', 1004, 'chouac100@gmail.com');

INSERT INTO Employees
Values ('Charles', 'Barkley', 'CharlieLakers', 'StillMVP', 105, 'charlesbarkley123@gmail.com');



-- Addresses for Clients in db
INSERT INTO Addresses
Values ('516 S. Kirkwood Rd.', 'Kirkwood', 25, 63122);

INSERT INTO Addresses
Values ('221B Baker St.', 'England', 35, 43004);

INSERT INTO Addresses
Values ('124 Conch St.', 'Bikini Bottom', 9, 32006);

INSERT INTO Addresses
Values ('313 N Plankinton Ave Suite #209', 'Milwaukee', 49, 53203);

INSERT INTO Addresses
Values ('1054 272nd St.', 'Hollywood', 5, 33004);


-- Animals
INSERT INTO ANIMALS
VALUES ('ChickenLittle', 2, 1, 'timid', 'true', 'false', 'female', 'approved', 4, 4, 1);

INSERT INTO ANIMALS
VALUES ('Harry', 60, 8, 'curious', 'true', 'true', 'male', 'unapproved', 1, 1, 2);

INSERT INTO ANIMALS
VALUES ('Garfield', 8, 5, 'friendly', 'true', 'true', 'female', 'pending', 2, 1, 3);

INSERT INTO ANIMALS
VALUES ('Leon', 1 , 2, 'agressive', 'false', 'false', 'male', 'approved', 3, 2, 4);

INSERT INTO ANIMALS
VALUES ('Boa', 33, 20, 'agressive', 'false', 'false', 'female', 'unapproved', 5, 5, 5);


-- Rooms

INSERT INTO Rooms
VALUES (111, 1);

INSERT INTO Rooms
VALUES (112, 2);

INSERT INTO Rooms
VALUES (113, 3);

INSERT INTO Rooms
VALUES (114, 4);

INSERT INTO Rooms
VALUES (115, 5);

INSERT INTO Rooms (RoomNumber)
VALUES (116);

INSERT INTO Rooms (RoomNumber)
VALUES (117);

INSERT INTO Rooms (RoomNumber)
VALUES (118);

INSERT INTO Rooms (RoomNumber)
VALUES (119);

INSERT INTO Rooms (RoomNumber)
VALUES (120);


-- Clients
INSERT INTO Clients
Values ('Kent', 'Emerson', 'magichouse', 'vandegraff', 1, 'Magic@House.com');

INSERT INTO Clients
Values ('Sherlock', 'Holmes', 'detectiveguy2011', 'dearwatson', 2, 'detectivesherlock@holmes.net');

INSERT INTO Clients
Values ('Bob', 'Squarepants', 'jellyfishfan09', 'patrick', 3, 'spongebob@crabbypatty.com');

INSERT INTO Clients
Values ('Brechar', 'Miknevdav', 'dCCInstructor20', 'csharprules', 4, 'programmer@campcodedev.com');

INSERT INTO Clients
Values ('Normal', 'Bates', 'loveyourmother', 'whitepine', 1, 'Magic@House.com');

-- Shots
INSERT INTO Shots
VALUES ('Rabies'), --any
      ('DHPP'), --dogs
	  ('Bordetella'), --dogs
	  ('FVRCP'), -- cats
	  ('DIVA'); -- farm animals

-- Animal Shot Records

INSERT INTO AnimalShots (AnimalId, ShotId)
SELECT  Animals.AnimalId, Shots.ShotId
FROM Animals CROSS JOIN Shots
WHERE Animals.CategoryId = 1
AND Shots.Name = 'DHPP';

INSERT INTO AnimalShots (AnimalId, ShotId)
SELECT  Animals.AnimalId, Shots.ShotId
FROM Animals CROSS JOIN Shots
WHERE Animals.CategoryId = 2
AND Shots.Name = 'FVRCP';

INSERT INTO AnimalShots (AnimalId, ShotId)
SELECT  Animals.AnimalId, Shots.ShotId
FROM Animals CROSS JOIN Shots
WHERE Animals.CategoryId IN (1, 2, 3, 4)
AND Shots.Name = 'Rabies';

