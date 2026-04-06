USE master;
GO

IF NOT EXISTS (SELECT *
FROM sys.databases
WHERE name = 'DeviceManagementDb')
BEGIN
    CREATE DATABASE DeviceManagementDb;
END;
GO

USE DeviceManagementDb;
GO


IF OBJECT_ID(N'Users', N'U') IS NULL
CREATE TABLE Users
(
    id INT PRIMARY KEY IDENTITY (1, 1),
    name TEXT NOT NULL,
    role TEXT NOT NULL,
    email TEXT NOT NULL,
    passwordHash TEXT NOT NULL,
    location TEXT NOT NULL
);

IF OBJECT_ID(N'Devices', N'U') IS NULL
CREATE TABLE Devices
(
    id INT PRIMARY KEY IDENTITY (1, 1),
    name TEXT NOT NULL,
    manufacturer TEXT NOT NULL,
    type TEXT NOT NULL,
    operatingSystem TEXT NOT NULL,
    osVersion TEXT,
    processor TEXT NOT NULL,
    ram INT NOT NULL,
    userId INT NULL REFERENCES Users(Id),
    description TEXT
);
