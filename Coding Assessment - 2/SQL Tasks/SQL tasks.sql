-- Create Database
CREATE DATABASE CareerHubDB

-- Create Companies Table
CREATE TABLE Companies (
    CompanyID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName VARCHAR(255) NOT NULL,
    Location VARCHAR(255) NOT NULL
)

-- Create Jobs Table with Foreign Key
CREATE TABLE Jobs (
    JobID INT PRIMARY KEY IDENTITY(1,1),
    CompanyID INT,
    JobTitle VARCHAR(255) NOT NULL,
    JobDescription TEXT,
    JobLocation VARCHAR(255) NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    JobType VARCHAR(50),
    PostedDate DATETIME NOT NULL,
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID) ON DELETE CASCADE
)

-- Create Applicants Table
CREATE TABLE Applicants (
    ApplicantID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Phone VARCHAR(20),
    Resume VARCHAR(255)
)

-- Create Applications Table with Foreign Key
CREATE TABLE Applications (
    ApplicationID INT PRIMARY KEY IDENTITY(1,1),
    JobID INT,
    ApplicantID INT,
    ApplicationDate DATETIME NOT NULL,
    CoverLetter TEXT,
    FOREIGN KEY (JobID) REFERENCES Jobs(JobID) ON DELETE CASCADE,
    FOREIGN KEY (ApplicantID) REFERENCES Applicants(ApplicantID) ON DELETE CASCADE
)

-- Insert Sample Data into Companies Table
INSERT INTO Companies (CompanyName, Location) 
VALUES 
    ('Chennai Technologies', 'Chennai'),
    ('Madurai Innovations', 'Madurai'),
    ('Coimbatore Solutions', 'Coimbatore'),
    ('Trichy Systems', 'Trichy'),
    ('Tirunelveli Enterprises', 'Tirunelveli'),
    ('Salem Industries', 'Salem'),
    ('Vellore Tech', 'Vellore'),
    ('Erode Digital', 'Erode'),
    ('Tanjore Works', 'Tanjore'),
    ('Karur Manufacturing', 'Karur')

-- Insert Sample Data into Jobs Table
INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) 
VALUES 
    (1, 'Software Developer', 'Develop and maintain software solutions.', 'Chennai', 95000, 'Full-time', GETDATE()),
    (2, 'Product Manager', 'Lead product strategy and development.', 'Madurai', 120000, 'Full-time', GETDATE()),
    (3, 'Web Developer', 'Design and implement web applications.', 'Coimbatore', 80000, 'Part-time', GETDATE()),
    (4, 'UI/UX Designer', 'Create and design user interfaces for mobile apps.', 'Trichy', 70000, 'Contract', GETDATE()),
    (5, 'Marketing Manager', 'Lead marketing campaigns and customer engagement.', 'Tirunelveli', 90000, 'Full-time', GETDATE()),
    (6, 'Network Engineer', 'Manage and optimize network infrastructure.', 'Salem', 85000, 'Full-time', GETDATE()),
    (7, 'Data Scientist', 'Analyze data to extract meaningful insights.', 'Vellore', 110000, 'Full-time', GETDATE()),
    (8, 'Business Analyst', 'Assess business processes and recommend improvements.', 'Erode', 95000, 'Full-time', GETDATE()),
    (9, 'Quality Assurance Engineer', 'Test and verify software products for defects.', 'Tanjore', 75000, 'Part-time', GETDATE()),
    (10, 'HR Manager', 'Manage employee relations and recruitment.', 'Karur', 95000, 'Full-time', GETDATE())

-- Insert Sample Data into Applicants Table
INSERT INTO Applicants (FirstName, LastName, Email, Phone, Resume)
VALUES 
    ('Arun', 'Raj', 'arun.raj@example.com', '9876543210', 'arun_raj_resume.pdf'),
    ('Priya', 'Kumar', 'priya.kumar@example.com', '8765432109', 'priya_kumar_resume.pdf'),
    ('Ravi', 'Nair', 'ravi.nair@example.com', '7654321098', 'ravi_nair_resume.pdf'),
    ('Anjali', 'Reddy', 'anjali.reddy@example.com', '6543210987', 'anjali_reddy_resume.pdf'),
    ('Suresh', 'V', 'suresh.v@example.com', '5432109876', 'suresh_v_resume.pdf'),
    ('Kavya', 'M', 'kavya.m@example.com', '4321098765', 'kavya_m_resume.pdf'),
    ('Manoj', 'S', 'manoj.s@example.com', '3210987654', 'manoj_s_resume.pdf'),
    ('Divya', 'P', 'divya.p@example.com', '2109876543', 'divya_p_resume.pdf'),
    ('Sathish', 'Kumar', 'sathish.kumar@example.com', '1098765432', 'sathish_kumar_resume.pdf'),
    ('Deepa', 'J', 'deepa.j@example.com', '0987654321', 'deepa_j_resume.pdf')

-- Insert Sample Data into Applications Table
INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter)
VALUES 
    (1, 1, GETDATE(), 'I am interested in the Software Developer position at Chennai Technologies.'),
    (2, 2, GETDATE(), 'I am highly qualified for the Product Manager position at Madurai Innovations.'),
    (3, 3, GETDATE(), 'I would like to apply for the Web Developer role at Coimbatore Solutions.'),
    (4, 4, GETDATE(), 'I am passionate about UI/UX design and would love to join Trichy Systems.'),
    (5, 5, GETDATE(), 'I have extensive experience in marketing and want to apply for the Marketing Manager role at Tirunelveli Enterprises.'),
    (6, 6, GETDATE(), 'I am skilled in network engineering and interested in the position at Salem Industries.'),
    (7, 7, GETDATE(), 'As a data scientist, I am eager to contribute at Vellore Tech.'),
    (8, 8, GETDATE(), 'I am excited about the Business Analyst role at Erode Digital.'),
    (9, 9, GETDATE(), 'I am interested in the Quality Assurance Engineer position at Tanjore Works.'),
    (10, 10, GETDATE(), 'I am keen to join Karur Manufacturing as the HR Manager and improve employee relations.')
	select * from Applications
	select * from Jobs2