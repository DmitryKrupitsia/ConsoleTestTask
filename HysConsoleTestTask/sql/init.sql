-- Создание базы данных
CREATE DATABASE HysTestTaskDb;
GO

USE HysTestTaskDb;
GO

-- Таблица Customers
CREATE TABLE Customers (
    Id INT PRIMARY KEY,
    Email NVARCHAR(100),
    Address NVARCHAR(100)
);

-- Таблица TvProducts
CREATE TABLE TvProducts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    Product NVARCHAR(50),
    StartDate DATE,
    EndDate DATE
);

-- Таблица DslProducts
CREATE TABLE DslProducts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    Product NVARCHAR(50),
    StartDate DATE,
    EndDate DATE
);

-- Вставка данных в Customers
INSERT INTO Customers (Id, Email, Address) VALUES
(1, 'max74@gmail.com', 'Lviv'),
(2, 'anna26@gmail.com', 'Kyiv'),
(3, 'john4@gmail.com', 'Dnipro'),
(4, 'kate39@gmail.com', 'Kyiv'),
(5, 'peter33@gmail.com', 'Odesa'),
(6, 'lisa78@gmail.com', 'Odesa'),
(7, 'ivan81@gmail.com', 'Dnipro'),
(8, 'olga25@gmail.com', 'Lviv'),
(9, 'sasha9@gmail.com', 'Kharkiv'),
(10, 'mike64@gmail.com', 'Lviv'),
(11, 'max60@gmail.com', 'Lviv'),
(12, 'anna28@gmail.com', 'Kyiv'),
(13, 'john20@gmail.com', 'Odesa'),
(14, 'kate7@gmail.com', 'Kyiv'),
(15, 'peter88@gmail.com', 'Lviv'),
(16, 'lisa43@gmail.com', 'Kharkiv'),
(17, 'ivan18@gmail.com', 'Odesa'),
(18, 'olga49@gmail.com', 'Kyiv'),
(19, 'sasha5@gmail.com', 'Dnipro'),
(20, 'mike31@gmail.com', 'Odesa'),
(21, 'max14@gmail.com', 'Kharkiv'),
(22, 'anna97@gmail.com', 'Dnipro'),
(23, 'john24@gmail.com', 'Lviv'),
(24, 'kate95@gmail.com', 'Odesa'),
(25, 'peter11@gmail.com', 'Kyiv'),
(26, 'lisa83@gmail.com', 'Kharkiv'),
(27, 'ivan47@gmail.com', 'Dnipro'),
(28, 'olga72@gmail.com', 'Odesa'),
(29, 'sasha59@gmail.com', 'Kyiv'),
(30, 'mike6@gmail.com', 'Lviv'),
(31, 'max74@gmail.com', 'Lviv'),
(32, 'anna26@gmail.com', 'Kyiv');

-- TvProducts с прошедшей датой
INSERT INTO TvProducts (CustomerId, Product, StartDate, EndDate) VALUES
(13, 'TV_P9', '2024-06-22', NULL),
(9, 'TV_P7', '2024-06-07', '2025-07-07'),
(18, 'TV_P10', '2024-06-04', NULL),
(21, 'TV_P7', '2024-06-04', NULL),
(18, 'TV_P7', '2024-06-21', NULL),
(31, 'TV_P1', '2024-06-10', NULL),
(5, 'TV_OLD', '2023-04-01', '2023-05-01'),
(12, 'TV_OLD2', '2023-02-15', '2023-03-01'),
(20, 'TV_OLD3', '2023-01-10', '2023-02-20');

-- DslProducts с прошедшей датой
INSERT INTO DslProducts (CustomerId, Product, StartDate, EndDate) VALUES
(9, 'DSL_P9', '2024-06-19', '2025-07-10'),
(22, 'DSL_P5', '2024-06-20', '2025-08-02'),
(7, 'DSL_P1', '2024-06-09', NULL),
(24, 'DSL_P10', '2024-06-02', '2025-06-19'),
(16, 'DSL_P3', '2024-06-09', NULL),
(1, 'DSL_P1', '2024-06-15', NULL),
(6, 'DSL_OLD', '2023-03-15', '2023-04-10'),
(3, 'DSL_OLD2', '2023-02-01', '2023-03-01'),
(19, 'DSL_OLD3', '2023-01-05', '2023-02-10');

