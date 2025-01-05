DROP DATABASE IF EXISTS SystemVault;

CREATE DATABASE IF NOT EXISTS SystemVault;
USE SystemVault;

CREATE TABLE User (
	Id BIGINT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL,
    Password TEXT NOT NULL
);

CREATE TABLE Category (
	Id BIGINT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE ServiceFile (
	Id BIGINT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Path TEXT NOT NULL,
    Size BIGINT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CategoryId BIGINT,
    FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);

INSERT INTO Category(Name)
VALUES ('Backup');

INSERT INTO ServiceFile (Name, Path, Size, CreatedAt, CategoryId)
VALUES 
('Backup file 1', 'C:\\', 1000, '2025-01-05 14:30:00', 1),
('Backup file 2', 'D:\\', 2000, '2025-01-05 14:35:00', 1),
('Backup file 3', 'E:\\', 3000, '2025-01-05 14:40:00', 1),
('Backup file 4', 'C:\\Backups', 4000, '2025-01-05 14:45:00', 1),
('Backup file 5', 'D:\\Backups', 5000, '2025-01-05 14:50:00', 1),
('Backup file 6', 'E:\\Archives', 6000, '2025-01-05 14:55:00', 1),
('Backup file 7', 'C:\\Archives', 7000, '2025-01-05 15:00:00', 1),
('Backup file 8', 'D:\\Logs', 8000, '2025-01-05 15:05:00', 1),
('Backup file 9', 'E:\\Logs', 9000, '2025-01-05 15:10:00', 1),
('Backup file 10', 'C:\\Logs', 10000, '2025-01-05 15:15:00', 1);
