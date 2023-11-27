-- Crea la base de datos
DROP DATABASE IF EXISTS SistemaDeParqueos;
CREATE DATABASE SistemaDeParqueos;

-- Cambia al contexto de la nueva base de datos
USE SistemaDeParqueos;

-- Crear la tabla Empleado
CREATE TABLE Empleado (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Id con autoincremento
    Parqueo INT,
    FechaIngreso DATE,
    Nombre VARCHAR(255),
    Apellidos VARCHAR(255),
    FechaDeNacimiento DATE,
    Cedula BIGINT,
    Direccion VARCHAR(255),
    Email VARCHAR(255),
    Telefono INT,
    PersonaDeContacto VARCHAR(255)
);

-- Crear la tabla Parqueo
CREATE TABLE Parqueo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255),
    CapacidadMaxima INT,
    HoraApertura DATETIME,
    HoraCierre DATETIME
);

-- Crear la tabla Tiquete
CREATE TABLE Tiquete (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Id con autoincremento
    Parqueo INT,
    FechaYHoraEntrada DATETIME,
    FechaYHoraSalida DATETIME,
    Placa VARCHAR(20),
    TarifaPorHora FLOAT,
    TarifaPorMediaHora FLOAT
);

-- Insertar un empleado por default
INSERT INTO Empleado (Parqueo, FechaIngreso, Nombre, Apellidos, FechaDeNacimiento, Cedula, Direccion, Email, Telefono, PersonaDeContacto)
VALUES 
(1, '2023-11-26', 'Juan', 'Pérez', '1990-05-15', 12345678901, 'Calle 123, Ciudad', 'juan.perez@email.com', 123456789, 'Maria Gómez');

-- Insertar un parqueo por default
INSERT INTO Parqueo (Nombre, CapacidadMaxima, HoraApertura, HoraCierre)
VALUES 
('Parqueo Ejemplo', 50, '08:00:00', '20:00:00');

-- Insertar un tiquete por default
INSERT INTO Tiquete (Parqueo, FechaYHoraEntrada, FechaYHoraSalida, Placa, TarifaPorHora, TarifaPorMediaHora)
VALUES 
(1, '2023-11-26 10:30:00', '2023-11-26 15:45:00', 'ABC123', 5.00, 2.50);