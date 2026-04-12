USE DeviceManagementDb;
GO


TRUNCATE TABLE DeviceManagementDb.dbo.Devices;
DELETE FROM DeviceManagementDb.dbo.AspNetUsers;
GO

INSERT INTO DeviceManagementDb.dbo.AspNetUsers (Id,Name,[Role],Location,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount) VALUES
	 (N'a8eb8f42-2e98-4264-81dd-aa96c5ac9159',N'Ana Marinescu',N'USER',N'Cluj-Napoca',N'ana_marinescu5@email.com',N'ANA_MARINESCU5@EMAIL.COM',N'ana_marinescu5@email.com',N'ANA_MARINESCU5@EMAIL.COM',0,N'AQAAAAIAAYagAAAAEPSfuMnkL0Ea8/ejQFslJUWnynf3Jj32h9UAL6/ak8VTu82kvaDrAa17WABntl3KYg==',N'LODV6UJARHHCMTFABKP6KFGXKXCUODF2',N'ec287706-6b64-4dfa-ae58-e6b2cc74a2e6',NULL,0,0,NULL,1,0),
	 (N'd886a8ee-3352-48f3-a383-ead51576e698',N'Amy Jones',N'USER',N'Sydney',N'amy_jones6@email.com',N'AMY_JONES6@EMAIL.COM',N'amy_jones6@email.com',N'AMY_JONES6@EMAIL.COM',0,N'AQAAAAIAAYagAAAAEDcdH84gtwOANCjK6yiNzoa+yHBfgNExUHo3sJdeTH5cBxtAgImoJugg1noYk4zenQ==',N'AMNUJ56H3XZ3B5HWQYYFI77LCI6ZRDIF',N'7a6d3a38-712d-4891-832a-b8dd02cf12e2',NULL,0,0,NULL,1,0),
	 (N'd9ea2c9b-ca56-49ba-9613-a6d852a22dd5',N'Cristian Ionescu',N'USER',N'Cluj-Napoca',N'cristian_ionescu4@email.com',N'CRISTIAN_IONESCU4@EMAIL.COM',N'cristian_ionescu4@email.com',N'CRISTIAN_IONESCU4@EMAIL.COM',0,N'AQAAAAIAAYagAAAAEDeGUpkgSWImRVjCGtSD95qkVdEcik73muKUFs77t++khR+M7dnbHMBR7v2EdrE9AA==',N'IIGEMHQTK4VVNHL36CAASITO4V3TZR3Q',N'fdba7ca3-50e7-45ee-bbd8-a315e0ca773c',NULL,0,0,NULL,1,0),
	 (N'df474245-9e69-4ee1-a594-6109e1d5c937',N'Mihai Popescu',N'ADMIN',N'Cluj-Napoca',N'mihai_popescu3@email.com',N'MIHAI_POPESCU3@EMAIL.COM',N'mihai_popescu3@email.com',N'MIHAI_POPESCU3@EMAIL.COM',0,N'AQAAAAIAAYagAAAAENWfG+A0WG5VY21orQGfTJqMgnVOL0uy7I9YN4tYMSUp5DNI08Aol8398GVuvH7DWA==',N'FKKHLLELHMH6VBYZVSYYMI5Q443JAK5V',N'7ee6704a-0710-4c02-86ae-45d3c7784459',NULL,0,0,NULL,1,0),
	 (N'ffcee6a6-fcac-476c-9c1d-72daa4b4b7b7',N'Matt Williams',N'ADMIN',N'Sydney',N'matt_williams7@email.com',N'MATT_WILLIAMS7@EMAIL.COM',N'matt_williams7@email.com',N'MATT_WILLIAMS7@EMAIL.COM',0,N'AQAAAAIAAYagAAAAEF2WlQFeIfuYPInxxIfTMZClEUVii5bPsF02YkLuAqWQXut2Ld8hpQrLtab/+6Tl5A==',N'4BK6SZP6KKZIBUVDZKLEJFGFHBAGVT44',N'8adfcef8-f895-4c40-b9a7-1211f33b7901',NULL,0,0,NULL,1,0);


INSERT INTO DeviceManagementDb.dbo.Devices (Name,Manufacturer,[Type],OperatingSystem,OSVersion,Processor,RAM,UserId,Description) VALUES
	 (N'Galaxy S25',N'Samsung',N'Phone',N'Android',N'15.0.6',N'Oryon V2 Phoenix',12,N'd886a8ee-3352-48f3-a383-ead51576e698',N'Latest release of the most compact Galaxy phone'),
	 (N'iPhone 16',N'Apple',N'Phone',N'iOS',N'18.1.1',N'A18',8,N'df474245-9e69-4ee1-a594-6109e1d5c937',N'The 2024 namesake of the series, bridging the gap between base models and pro ones'),
	 (N'Galaxy Tab S11',N'Samsung',N'Tablet',N'Android',N'16.2',N'Mediatek Dimensity 9400+',12,NULL,N'2025 release of the most popular Samsung tablet'),
	 (N'Apple iPad mini',N'Apple',N'Tablet',N'iOS',N'18.0',N'A17 Pro',12,NULL,N'The smoothest screen experience of any Apple tablet, released in 2024'),
	 (N'MacBook Pro 16',N'Apple',N'Laptop',N'macOS',N'16.1.7',N'M4 Pro',24,N'a8eb8f42-2e98-4264-81dd-aa96c5ac9159',N''),
	 (N'IdeaPad Slim 3 ',N'Lenovo',N'Laptop',N'No OS',N'',N'AMD Ryzen 7',24,NULL,N'The Lenovo IdeaPad Slim 3 is a lightweight and portable laptop ideal for everyday productivity tasks. This model features a powerful AMD Ryzen 7 processor and 24GB of RAM, ensuring smooth performance even with multiple applications running simultaneously. As a standard laptop with no operating system pre-installed (No OS), it offers flexibility for users to install their preferred operating system – making it suitable for diverse software environments and IT standardization policies. It’s a solid choice for employees needing reliable performance in a slim, easily transportable package.');
