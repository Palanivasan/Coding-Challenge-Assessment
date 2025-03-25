-- Task 1: Initialize Database and Task 4: SQL Script to handle potential errors, such as if the database or tables already exist.

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PetPals')
BEGIN
    CREATE DATABASE PetPals
END

IF OBJECT_ID('dbo.Participants', 'U') IS NOT NULL DROP TABLE Participants
IF OBJECT_ID('dbo.AdoptionEvents', 'U') IS NOT NULL DROP TABLE AdoptionEvents
IF OBJECT_ID('dbo.Donations', 'U') IS NOT NULL DROP TABLE Donations
IF OBJECT_ID('dbo.Shelters', 'U') IS NOT NULL DROP TABLE Shelters
IF OBJECT_ID('dbo.Pets', 'U') IS NOT NULL DROP TABLE Pets

--  Task 2 and 3: Create Tables and Define Constraints

-- Creation of Pets Table

CREATE TABLE Pets 
(
    PetID INT IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Breed VARCHAR(100) NOT NULL,
    Type VARCHAR(50) NOT NULL,
    AvailableForAdoption BIT NOT NULL,
	CONSTRAINT pk_petId PRIMARY KEY (PetID)
)

-- Creation of Shelters Table

CREATE TABLE Shelters 
(
    ShelterID INT IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Location VARCHAR(100) NOT NULL,
	CONSTRAINT pk_shelterId PRIMARY KEY (ShelterID)
)

-- Creation of Donations Table 

CREATE TABLE Donations 
(
    DonationID INT IDENTITY(1,1),
    DonorName VARCHAR(100) NOT NULL,
    DonationType VARCHAR(50) NOT NULL,
    DonationAmount MONEY,
    DonationItem VARCHAR(100),
    DonationDate DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT pk_donationId PRIMARY KEY (DonationID)
)

-- Creation of AdoptionEvents Table

CREATE TABLE AdoptionEvents 
(
    EventID INT IDENTITY(1,1),  
    EventName VARCHAR(100) NOT NULL,        
    EventDate DATETIME NOT NULL,           
    Location VARCHAR(100) NOT NULL , 
	CONSTRAINT pk_eventId PRIMARY KEY (EventID)
)

-- Creation of Participants Table

CREATE TABLE Participants 
(
    ParticipantID INT IDENTITY(1,1),  
    ParticipantName VARCHAR(100) NOT NULL,        
    ParticipantType VARCHAR(100) NOT NULL CHECK (ParticipantType IN ('Shelter', 'Adopter', 'Volunteer', 'Sponsor')), 
    EventID INT NULL,  
	CONSTRAINT pk_participantId PRIMARY KEY (ParticipantID),
    CONSTRAINT fk_eventId FOREIGN KEY (EventID) REFERENCES AdoptionEvents(EventID) ON DELETE CASCADE
)

-- Insert Sample Data into Shelters

INSERT INTO Shelters (Name, Location) VALUES 
('Paws Haven', 'Chennai'),
('Happy Tails', 'Madurai'),
('Golden Paws', 'Coimbatore'),
('Furry Friends', 'Trichy'),
('Safe Paws', 'Salem'),
('Pet Home', 'Tirunelveli'),
('Care for Paws', 'Vellore'),
('Rescue Hub', 'Thanjavur'),
('Friendly Paws', 'Erode'),
('Animal Care', 'Kanyakumari')

-- Insert Sample Data into Pets

INSERT INTO Pets (Name, Age, Breed, Type, AvailableForAdoption) VALUES 
('Goldy', 2, 'Labrador', 'Dog', 1),
('Shadow', 4, 'Persian', 'Cat', 1),
('Bruno', 6, 'Golden Retriever', 'Dog', 0),
('Whiskers', 3, 'Siamese', 'Cat', 1),
('Jimmy', 5, 'Beagle', 'Dog', 1),
('Rocky', 1, 'Pug', 'Dog', 1),
('Tommy', 7, 'Bulldog', 'Dog', 0),
('Simba', 2, 'Maine Coon', 'Cat', 1),
('Buddy', 4, 'Husky', 'Dog', 1),
('Leo', 6, 'Ragdoll', 'Cat', 0)

-- Insert Sample Data into AdoptionEvents

INSERT INTO AdoptionEvents (EventName, EventDate, Location) VALUES 
('Summer Pet Fest', '2025-06-15', 'Chennai'),
('Monsoon Adoption Drive', '2025-07-10', 'Madurai'),
('New Year Pet Carnival', '2025-01-05', 'Coimbatore'),
('Valentine’s Pet Love', '2025-02-14', 'Trichy'),
('Holi Pet Mela', '2025-03-25', 'Salem'),
('Diwali Pet Lights', '2025-11-10', 'Tirunelveli'),
('Christmas Pet Joy', '2025-12-25', 'Vellore'),
('Pongal Pet Fair', '2025-01-15', 'Thanjavur'),
('Independence Pet Fest', '2025-08-15', 'Erode'),
('Republic Day Adoption', '2025-01-26', 'Kanyakumari')

-- Insert Sample Data into Participants

INSERT INTO Participants (ParticipantName, ParticipantType, EventID) VALUES 
('Gowri', 'Shelter', 1),
('Keerthiga', 'Adopter', 1),
('Jayam', 'Adopter', 2),
('Shahjahan', 'Shelter', 2),
('Bharathi', 'Volunteer', 3),
('Palanivasan', 'Sponsor', 4),
('Parivendhan', 'Shelter', 5),
('JD', 'Shelter', 6),
('Rajesh', 'Adopter', 7),
('Kavitha', 'Volunteer', 8)

-- Insert Sample Data into Donations

INSERT INTO Donations (DonorName, DonationType, DonationAmount, DonationItem, DonationDate) VALUES 
('Arun', 'Cash', 5000.00, NULL, '2025-06-01'),
('Meena', 'Item', NULL, 'Dog Food', '2025-06-05'),
('Suresh', 'Cash', 3000.00, NULL, '2025-06-10'),
('Divya', 'Item', NULL, 'Cat Toys', '2025-07-01'),
('Manoj', 'Cash', 8000.00, NULL, '2025-07-15'),
('Deepa', 'Item', NULL, 'Pet Beds', '2025-08-10'),
('Ravi', 'Cash', 6000.00, NULL, '2025-08-20'),
('Lakshmi', 'Item', NULL, 'Pet Blankets', '2025-09-05'),
('Senthil', 'Cash', 7500.00, NULL, '2025-09-25'),
('Priya', 'Item', NULL, 'Pet Shampoo', '2025-10-15')

-- Task 5: Retrieve a list of available pets from the "Pets" table.

SELECT PetID, Name, Age, Breed, Type 
FROM Pets
WHERE AvailableForAdoption = 1

-- Task 6: Retrieve the names of participants registered for a specific adoption event.

SELECT p.ParticipantName
FROM Participants p
JOIN AdoptionEvents a
ON a.EventID = p.EventID
WHERE a.EventID = 1

-- Task 8: Calculate and retrieve the total donation amount for each shelter from the "Donations" table. 

ALTER TABLE Donations
ADD ShelterID INT

ALTER TABLE Donations
ADD CONSTRAINT fk_shelterId FOREIGN KEY (ShelterID) REFERENCES Shelters(ShelterID) ON DELETE CASCADE

UPDATE Donations
SET ShelterID = 1
WHERE DonationID = 1

UPDATE Donations
SET ShelterID = 2
WHERE DonationID = 2

UPDATE Donations
SET ShelterID = 3
WHERE DonationID = 3

UPDATE Donations
SET ShelterID = 4
WHERE DonationID = 4

UPDATE Donations
SET ShelterID = 5
WHERE DonationID = 5

UPDATE Donations
SET ShelterID = 6
WHERE DonationID = 6

UPDATE Donations
SET ShelterID = 7
WHERE DonationID = 7

UPDATE Donations
SET ShelterID = 8
WHERE DonationID = 8

UPDATE Donations
SET ShelterID = 9
WHERE DonationID = 9

UPDATE Donations
SET ShelterID = 10
WHERE DonationID = 10

SELECT s.Name, COALESCE(SUM(d.DonationAmount), 0) AS [Donation Amount]
FROM Donations d
JOIN Shelters s
ON d.ShelterID = s.ShelterID
GROUP BY s.Name

-- Task 9: Retrieve the names of pets from the "Pets" table that do not have an owner.

ALTER TABLE Pets
ADD OwnerID INT

UPDATE Pets
SET OwnerID = 3
WHERE AvailableForAdoption = 0 AND PetID = 3

UPDATE Pets
SET OwnerID = 2
WHERE AvailableForAdoption = 0 AND PetID = 7

UPDATE Pets
SET OwnerID = 9
WHERE AvailableForAdoption = 0 AND PetID = 10

SELECT PetID, Name, Age, Breed, Type
FROM Pets
WHERE OwnerID IS NULL

-- Task 10: Retrieve the total donation amount for each month and year

SELECT FORMAT(DonationDate, 'MMMM yyyy') AS Month_Year, COALESCE(SUM(DonationAmount), 0) AS Total_Donation_Amount
FROM Donations
GROUP BY FORMAT(DonationDate, 'MMMM yyyy')
ORDER BY MIN(DonationDate)

-- Task 11: Retrieve a list of distinct breeds for all pets that are either aged between 1 and 3 years or older than 5 years.

SELECT DISTINCT Breed
FROM Pets
WHERE (Age >= 1 AND Age <= 3) OR Age > 5

-- Task 12: Retrieve a list of pets and their respective shelters where the pets are currently available for adoption.

ALTER TABLE Pets
ADD ShelterID INT

ALTER TABLE Pets
ADD CONSTRAINT fk_ShelterId_2
FOREIGN KEY (ShelterID) REFERENCES Shelters(ShelterID)

UPDATE Pets
SET ShelterID = 1
WHERE PetID IN (1, 2, 3, 4)

UPDATE Pets
SET ShelterID = 2
WHERE PetID IN (5, 6, 7, 8)

UPDATE Pets
SET ShelterID = 3
WHERE PetID IN (9, 10)

SELECT p.Name AS PetName, s.Name AS ShelterName
FROM Pets p
JOIN Shelters s ON p.ShelterID = s.ShelterID
WHERE p.AvailableForAdoption = 1

-- Task 13: Find the total number of participants in events organized by shelters located in specific city.

ALTER TABLE AdoptionEvents
ADD ShelterID INT

ALTER TABLE AdoptionEvents
ADD CONSTRAINT fk_shelterId_3 FOREIGN KEY (ShelterID) REFERENCES Shelters(ShelterID)

UPDATE AdoptionEvents
SET ShelterID = 2
WHERE EventID = 1

UPDATE AdoptionEvents
SET ShelterID = 3
WHERE EventID = 2

UPDATE AdoptionEvents
SET ShelterID = 4
WHERE EventID = 3

UPDATE AdoptionEvents
SET ShelterID = 5
WHERE EventID = 4

UPDATE AdoptionEvents
SET ShelterID = 6
WHERE EventID = 5

UPDATE AdoptionEvents
SET ShelterID = 2
WHERE EventID = 6

UPDATE AdoptionEvents
SET ShelterID = 3
WHERE EventID = 7

UPDATE AdoptionEvents
SET ShelterID = 4
WHERE EventID = 8

UPDATE AdoptionEvents
SET ShelterID = 5
WHERE EventID = 9

UPDATE AdoptionEvents
SET ShelterID = 6
WHERE EventID = 10

SELECT COUNT(p.ParticipantID) AS [Total Participants]
FROM Participants p
JOIN AdoptionEvents ae ON p.EventID = ae.EventID
JOIN Shelters s ON ae.ShelterID = s.ShelterID
WHERE s.Location LIKE '%Madurai%'

-- Task 14: Retrieve a list of unique breeds for pets with ages between 1 and 5 years.

SELECT DISTINCT Breed
FROM Pets
WHERE Age BETWEEN 1 AND 5

-- Task 15: Find the pets that have not been adopted by selecting their information from the 'Pet' table.

SELECT PetID, Name, Age, Breed, Type
FROM Pets
WHERE OwnerID IS NULL -- Used OwnerID IS NULL. Reason: If pet is not adopted, they have no owner.

-- Task 16: Retrieve the names of all adopted pets along with the adopter's name from the 'Adoption' (Pets) and 'User' (Participant) tables.
	
ALTER TABLE Pets
ADD CONSTRAINT fk_ownerId_ FOREIGN KEY (OwnerID) REFERENCES Participants(ParticipantID) -- Using OwnerID as FK linking Participant table, since there is no User table.
	
SELECT p.Name AS PetName, part.ParticipantName AS AdopterName
FROM Pets p
JOIN Participants part ON p.OwnerID = part.ParticipantID
WHERE p.OwnerID IS NOT NULL AND part.ParticipantType = 'Adopter'

-- Task 17: Retrieve a list of all shelters along with the count of pets currently available for adoption in each shelter.

SELECT s.Name AS ShelterName, COUNT(p.PetID) AS [Available Pets Count]
FROM Shelters s
LEFT JOIN Pets p ON s.ShelterID = p.ShelterID AND p.AvailableForAdoption = 1
GROUP BY s.Name

-- Task 18: Find pairs of pets from the same shelter that have the same breed.

SELECT p1.Name AS [Pet 1 Name], p2.Name AS [Pet 2 Name], p1.Breed AS Breed, p1.ShelterID
FROM Pets p1
JOIN Pets p2 
ON p1.ShelterID = p2.ShelterID 
AND p1.Breed = p2.Breed 
AND p1.PetID < p2.PetID

-- Task 19: List all possible combinations of shelters and adoption events.

SELECT s.Name AS [Shelter Name], e.EventName AS "Adoption Event Name"
FROM Shelters s
CROSS JOIN AdoptionEvents e
ORDER BY s.Name, e.EventName

-- Task 20: Determine the shelter that has the highest number of adopted pets.

SELECT TOP 1 s.Name AS ShelterName, COUNT(p.PetID) AS [Number Of Adopted Pets] 
FROM Shelters s 
JOIN Pets p 
ON s.ShelterID = p.ShelterID 
WHERE p.OwnerID IS NOT NULL 
GROUP BY s.Name 
ORDER BY [Number Of Adopted Pets] DESC
