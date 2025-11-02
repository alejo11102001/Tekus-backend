INSERT INTO Countries (IsoCode, Name) VALUES
('US', 'United States'),
('CO', 'Colombia'),
('MX', 'Mexico'),
('BR', 'Brazil'),
('AR', 'Argentina'),
('CL', 'Chile'),
('PE', 'Peru'),
('EC', 'Ecuador'),
('CA', 'Canada'),
('ES', 'Spain');

INSERT INTO Providers (Nit, Name, Email, CustomFieldsJson) VALUES
('900001', 'Importaciones Tekus S.A', 'contacto@tekus.com', '{"Número de contacto en marte":"12345"}'),
('900002', 'Soluciones Digitales Ltda', 'info@soluciones.com', '{"Cantidad de mascotas en la nómina":"3"}'),
('900003', 'GlobalSoft', 'support@globalsoft.com', '{}'),
('900004', 'AndesTech', 'ventas@andestech.com', '{}'),
('900005', 'SmartCloud', 'info@smartcloud.com', '{}'),
('900006', 'DataVision', 'contact@datavision.com', '{}'),
('900007', 'NovaTech', 'info@novatech.com', '{}'),
('900008', 'Andina Solutions', 'info@andina.com', '{}'),
('900009', 'SkyNet Services', 'contact@skynet.com', '{}'),
('900010', 'FutureCorp', 'admin@futurecorp.com', '{}');

INSERT INTO Services (ProviderId, Name, HourlyRateUsd) VALUES
(1, 'Soporte técnico remoto', 40.00),
(1, 'Mantenimiento de hardware', 60.00),
(2, 'Consultoría en TI', 80.00),
(3, 'Desarrollo de software', 100.00),
(4, 'Administración de redes', 50.00),
(5, 'Monitoreo en la nube', 70.00),
(6, 'Seguridad informática', 90.00),
(7, 'Desarrollo web', 75.00),
(8, 'Auditoría de sistemas', 65.00),
(9, 'Análisis de datos', 85.00);

INSERT INTO ServiceCountries (ServiceId, CountryId) VALUES
(1, 2),
(1, 3),
(2, 2),
(3, 4),
(4, 5),
(5, 1),
(6, 6),
(7, 7),
(8, 8),
(9, 9);
