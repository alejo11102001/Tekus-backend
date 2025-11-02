CREATE DATABASE TekusDb;

USE TekusDb;

CREATE TABLE Countries (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IsoCode NVARCHAR(10) NOT NULL,
    Name NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Providers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nit NVARCHAR(MAX) NOT NULL,
    Name NVARCHAR(MAX) NOT NULL,
    Email NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    CustomFieldsJson NVARCHAR(MAX)
);

CREATE TABLE Services (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProviderId INT NOT NULL,
    Name NVARCHAR(MAX) NOT NULL,
    HourlyRateUsd DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (ProviderId) REFERENCES Providers(Id)
);

CREATE TABLE ServiceCountries (
    ServiceId INT NOT NULL,
    CountryId INT NOT NULL,
    PRIMARY KEY (ServiceId, CountryId),
    FOREIGN KEY (ServiceId) REFERENCES Services(Id),
    FOREIGN KEY (CountryId) REFERENCES Countries(Id)
);
