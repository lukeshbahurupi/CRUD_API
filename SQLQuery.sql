create database CURD_API_DB;
go
use CURD_API_DB;

go

CREATE TABLE Users
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    IsAdmin BIT DEFAULT 0, -- BIT is used for boolean values, default is 0 (false)
    Age INT NOT NULL,
    Hobbies NVARCHAR(MAX) NULL -- NVARCHAR(MAX) can store large strings, NULLable
);

-- Insert Admin Users
INSERT INTO Users (Username, Password, IsAdmin, Age, Hobbies)
VALUES
    ('Admin1', 'Admin@123', 1, 30, '["Reading", "Programming"]'),
    ('Admin2', 'Admin@456', 1, 35, '["Playing Chess", "Travelling"]'),
    ('Admin3', 'Admin@789', 1, 40, '["Watching Movies", "Cooking"]'),
    ('Admin4', 'Admin@987', 1, 32, '["Gardening", "Painting"]'),
    ('Admin5', 'Admin@654', 1, 28, '["Photography", "Cycling"]');

-- Insert Normal Users
INSERT INTO Users (Username, Password, IsAdmin, Age, Hobbies)
VALUES
    ('User1', 'User@123', 0, 25, '["Listening to Music", "Playing Guitar"]'),
    ('User2', 'User@456', 0, 22, '["Running", "Swimming"]'),
    ('User3', 'User@789', 0, 27, '["Hiking", "Reading Books"]'),
    ('User4', 'User@987', 0, 31, '["Cooking", "Gaming"]'),
    ('User5', 'User@654', 0, 29, '["Yoga", "Traveling"]'),
    ('User6', 'User@321', 0, 33, '["Watching TV", "Fishing"]'),
    ('User7', 'User@567', 0, 26, '["Drawing", "Camping"]'),
    ('User8', 'User@890', 0, 24, '["Dancing", "Skiing"]'),
    ('User9', 'User@345', 0, 23, '["Meditation", "Playing Piano"]'),
    ('User10', 'User@678', 0, 30, '["Shopping", "Biking"]');


TRUNCATE TABLE Users;


select * from Users
