# TaskManagementSystem

SQL Data Base

Create Database TaskManagementSystem
Use TaskManagementSystem


CREATE TABLE Employees(
empID INT PRIMARY KEY IDENTITY,
empName VARCHAR(50),
empEmail VARCHAR(30) UNIQUE,
EmpPassword VARCHAR(20),
isActive BIT
)

CREATE TABLE Clients(
clientID INT PRIMARY KEY IDENTITY,
clientName VARCHAR(50),
isActive BIT
)

CREATE TABLE Projects(
projectID INT PRIMARY KEY IDENTITY,
projectName VARCHAR(50),
clientID INT REFERENCES Clients(clientID),
isActive BIT
)

CREATE TABLE DataBases(
dataBaseID INT PRIMARY KEY IDENTITY,
dataBaseName VARCHAR(50),
projectID INT REFERENCES Projects(projectID),
isActive BIT
)

CREATE TABLE Categories(
categoryID INT PRIMARY KEY IDENTITY,
categoryName VARCHAR(50),
isActive BIT
)

CREATE TABLE Tasks(
taskID INT PRIMARY KEY IDENTITY,
dataBaseID INT REFERENCES DataBases(dataBaseID),
categoryID INT REFERENCES Categories(categoryID),
taskTilte VARCHAR(100),
taskDescription VARCHAR(200),
assignedBy VARCHAR(50),
empID INT REFERENCES Employees(empID),
createdDate DateTime,
isActive Bit )




Create Table TaskNotes(
notesId Int PRIMARY KEY IDENTITY,
taskID INT REFERENCES Tasks(taskID),
empID INT  REFERENCES Employees(empID),
notes VARCHAR(200),
workHours Float,
isActive Bit 
)

-------------------------------------------
--Data

INSERT INTO Employees (empName, empEmail, EmpPassword, isActive) VALUES
('Aarav Patel', 'aarav.patel@example.com', 'password123', 1),
('Isha Sharma', 'isha.sharma@example.com', 'password123', 1),
('Ravi Kumar', 'ravi.kumar@example.com', 'password123', 1),
('Neha Gupta', 'neha.gupta@example.com', 'password123', 0);


INSERT INTO Clients (clientName, isActive) VALUES
('Satyam Enterprises', 1),
('Innovate Solutions', 1),
('TechCorp India', 0),
('NexGen Solutions', 1);

INSERT INTO Projects (projectName, clientID, isActive) VALUES
('E-commerce Platform', 1, 1),
('Mobile Payment App', 2, 1),
('Cloud Storage Migration', 1, 0),
('ERP System Development', 4, 1);

INSERT INTO DataBases (dataBaseName, projectID, isActive) VALUES
('CustomerData', 1, 1),
('PaymentDB', 2, 1),
('OldSystemDB', 3, 0),
('ERP_DB', 4, 1);

INSERT INTO Categories (categoryName, isActive) VALUES
('Feature', 1),
('Bug', 1),
('Improvement', 1),
('Maintenance', 1);
----------------------------------------
--StoredProcedure For Task Management System--
	Use TaskManagementSystem
--Login Employee
CREATE PROCEDURE SPR_Login
@Email VARCHAR(30),
@Password VARCHAR(20)
AS
BEGIN
	Select * from Employees
	Where empEmail=@Email AND empPassword=@Password
END;

--Get Active Clients
CREATE PROCEDURE SPR_Clients
AS
BEGIN
	Select * from Clients
	Where isActive=1
END;

--Clients Project
CREATE PROCEDURE SPR_ClientProjects
@ClientId INT
AS
BEGIN
	Select * from Projects
	Where clientID=@ClientId And isActive=1
END;

--Get DataBases of Projects

CREATE PROCEDURE SPR_ProjectDataBase
@ProjectId INT
AS
BEGIN
	Select * from DataBases
	Where projectID=@ProjectId and isActive=1
END;


CREATE PROCEDURE SPI_Task
    @dataBaseID INT,
    @categoryID INT,
    @taskTitle VARCHAR(100),
    @taskDescription VARCHAR(200),
    @assignedBy VARCHAR(50),
    @empID INT,
    @success INT OUTPUT
AS
BEGIN
    SET @success = 0;
    BEGIN TRY

        INSERT INTO Tasks (dataBaseID,categoryID,taskTilte,  taskDescription,assignedBy,empID,createdDate,isActive)
        VALUES (@dataBaseID,@categoryID,@taskTitle,@taskDescription,@assignedBy,@empID,SYSDATETIME(),1);

        SET @success = 1;
    END TRY
    BEGIN CATCH
        
        SET @success = 0;
   END CATCH;
END;

--Retrive Tasks
Create Procedure SPR_Task
AS
BEGIN
	Select * from Tasks
	order by taskId Desc;
END;

--Insert Notes
Create Procedure SPI_TaskNotes
 @taskID INT,
 @empID INT,
 @notes VARCHAR(200),
 @workHours FLOAT,
 @isActive BIT
AS
BEGIN
    INSERT INTO TaskNotes (taskID, empID, notes, workHours, isActive)
    VALUES (@taskID, @empID, @notes, @workHours, @isActive);
END;

--Retrive Specific Task Notes
CREATE PROCEDURE SPR_TaskNotes
@TaskId INT
AS
BEGIN
	SELECT *
	FROM TaskNotes t
	join Employees e
	on e.empID=t.empId
	WHERE taskId=@TaskId
	ORDER BY notesId DESC
END;

--
