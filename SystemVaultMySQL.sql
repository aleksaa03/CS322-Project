DROP DATABASE IF EXISTS SystemVault;

CREATE DATABASE IF NOT EXISTS SystemVault;
USE SystemVault;

CREATE TABLE User (
	Id BIGINT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password TEXT NOT NULL,
    RoleId SMALLINT NOT NULL
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
('Backup file 10', 'C:\\Logs', 10000, '2025-01-05 15:15:00', 1),
('Backup file 11', 'C:\\', 11000, '2025-01-05 15:20:00', 1),
('Backup file 12', 'D:\\', 12000, '2025-01-05 15:25:00', 1),
('Backup file 13', 'E:\\', 13000, '2025-01-05 15:30:00', 1),
('Backup file 14', 'C:\\Backups', 14000, '2025-01-05 15:35:00', 1),
('Backup file 15', 'D:\\Backups', 15000, '2025-01-05 15:40:00', 1),
('Backup file 16', 'E:\\Archives', 16000, '2025-01-05 15:45:00', 1),
('Backup file 17', 'C:\\Archives', 17000, '2025-01-05 15:50:00', 1),
('Backup file 18', 'D:\\Logs', 18000, '2025-01-05 15:55:00', 1),
('Backup file 19', 'E:\\Logs', 19000, '2025-01-05 16:00:00', 1),
('Backup file 20', 'C:\\Logs', 20000, '2025-01-05 16:05:00', 1),
('Backup file 21', 'C:\\', 21000, '2025-01-05 16:10:00', 1),
('Backup file 22', 'D:\\', 22000, '2025-01-05 16:15:00', 1),
('Backup file 23', 'E:\\', 23000, '2025-01-05 16:20:00', 1),
('Backup file 24', 'C:\\Backups', 24000, '2025-01-05 16:25:00', 1),
('Backup file 25', 'D:\\Backups', 25000, '2025-01-05 16:30:00', 1),
('Backup file 26', 'E:\\Archives', 26000, '2025-01-05 16:35:00', 1),
('Backup file 27', 'C:\\Archives', 27000, '2025-01-05 16:40:00', 1),
('Backup file 28', 'D:\\Logs', 28000, '2025-01-05 16:45:00', 1),
('Backup file 29', 'E:\\Logs', 29000, '2025-01-05 16:50:00', 1),
('Backup file 30', 'C:\\Logs', 30000, '2025-01-05 16:55:00', 1),
('Backup file 31', 'C:\\', 31000, '2025-01-05 17:00:00', 1),
('Backup file 32', 'D:\\', 32000, '2025-01-05 17:05:00', 1),
('Backup file 33', 'E:\\', 33000, '2025-01-05 17:10:00', 1),
('Backup file 34', 'C:\\Backups', 34000, '2025-01-05 17:15:00', 1),
('Backup file 35', 'D:\\Backups', 35000, '2025-01-05 17:20:00', 1),
('Backup file 36', 'E:\\Archives', 36000, '2025-01-05 17:25:00', 1),
('Backup file 37', 'C:\\Archives', 37000, '2025-01-05 17:30:00', 1),
('Backup file 38', 'D:\\Logs', 38000, '2025-01-05 17:35:00', 1),
('Backup file 39', 'E:\\Logs', 39000, '2025-01-05 17:40:00', 1),
('Backup file 40', 'C:\\Logs', 40000, '2025-01-05 17:45:00', 1),
('Backup file 41', 'C:\\', 41000, '2025-01-05 17:50:00', 1),
('Backup file 42', 'D:\\', 42000, '2025-01-05 17:55:00', 1),
('Backup file 43', 'E:\\', 43000, '2025-01-05 18:00:00', 1),
('Backup file 44', 'C:\\Backups', 44000, '2025-01-05 18:05:00', 1),
('Backup file 45', 'D:\\Backups', 45000, '2025-01-05 18:10:00', 1),
('Backup file 46', 'E:\\Archives', 46000, '2025-01-05 18:15:00', 1),
('Backup file 47', 'C:\\Archives', 47000, '2025-01-05 18:20:00', 1),
('Backup file 48', 'D:\\Logs', 48000, '2025-01-05 18:25:00', 1),
('Backup file 49', 'E:\\Logs', 49000, '2025-01-05 18:30:00', 1),
('Backup file 50', 'C:\\Logs', 50000, '2025-01-05 18:35:00', 1);