USE DeviceManagementDb;
GO


INSERT INTO Users
VALUES
    (N'Mihai Popescu', N'USER', N'mihai_popescu@email.com', N'', N'Cluj-Napoca'),
    (N'Cristian Ionescu', N'ADMIN', N'cristian_ionescu@email.com', N'', N'Cluj-Napoca'),
    (N'Ana Marinescu', N'USER', N'ana_marinescu@email.com', N'', N'Cluj-Napoca'),
    (N'Amy Jones', N'USER', N'amy_jones@email.com', N'', N'Sydney'),
    (N'Matt Williams', N'ADMIN', N'matt_williams@email.com', N'', N'Sydney');

INSERT INTO Devices
VALUES
    (N'Galaxy S25', N'Samsung', N'Phone', N'Android', N'15.0.6', N'Oryon V2 Phoenix', 12, NULL, N'Latest release of the most compact Galaxy phone'),
    (N'iPhone 16', N'Apple', N'Phone', N'iOS', N'18.1.1', N'A18', 8, 4, N'The 2024 namesake of the series, bridging the gap between base models and pro ones'),
    (N'Galaxy Tab S11', N'Samsung', N'Tablet', N'Android', N'16.2', N'Mediatek Dimensity 9400+', 12, NULL, N'2025 release of the most popular Samsung tablet'),
    (N'iPad mini', N'Apple', N'Tablet', N'iOS', N'18.0', N'A17 Pro', 12, NULL, N'The smoothest screen experience of any Apple tablet, released in 2024'),
    (N'MacBook Pro 16', N'Apple', N'Laptop', N'macOS', N'16.1.7', N'M4 Pro', 24, NULL, N''),
    (N'IdeaPad Slim 3 ', N'Lenovo', N'Laptop', N'No OS', N'' , N'AMD Ryzen 7', 24, NULL, N'');
