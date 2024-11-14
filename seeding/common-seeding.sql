INSERT INTO Countries (IsoCode, Name, IsdCode)
VALUES ('dk', 'Danmark', '+45');
GO

INSERT INTO ZipCodes (PostalCode, Name, CountryId)
VALUES ('2620', 'Ballerup', 'dk');
GO

INSERT INTO Roles (Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('DefaultRole', CURRENT_TIMESTAMP, NULL, NULL, NULL);
GO

SET IDENTITY_INSERT Rights ON;
INSERT INTO Rights (Id, Name, Description)
VALUES
    (10, 'Administrator', 'Giver administrator rettighed.'),
    (20, 'Edit', 'Giver redigerings rettighed.'),
    (30, 'ManageTimes', N'Giver rettighed til at håndtere registrerede tider for andre brugere.'),
    (40, 'ManageUsers', 'Giver rettighed til at oprette, redigere, og slette brugere.'),
    (50, 'PureView', 'Giver rettighed til at se alt information uagtet om noget er skjult.'),
    (60, 'View', 'Giver rettighed til at se.'),
    (70, 'SetOwnWorkSchedule', N'Giver rettigheden til at sætte sit eget arbejdsskema.'),
    (80, 'ManuallySetOwnWorkTime', N'Giver rettighed til at manuelt sætte arbejdstider.'),
    (90, 'SetOwnJobTitle', N'Giver rettigheden til at sætte sin egne job titler.'),
    (100, 'SetAutomaticCheckInOut', N'Giver rettighed til at tænde og slukke for automatisk tjek ind og ud.');
SET IDENTITY_INSERT Rights OFF;
GO;

INSERT INTO Roles (Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES
    ('DefaultRole', CURRENT_TIMESTAMP, NULL, NULL, NULL),
    ('__System_Administrator__', CURRENT_TIMESTAMP, NULL, NULL, NULL);
GO

INSERT INTO WorkSchedules (IsDefaultWorkSchedule)
VALUES (1), (1), (1), (1);

GO

INSERT INTO Addresses (ParentId, CountryId, StreetName, HouseNumber, PostalCode, Door, Floor)
VALUES (NULL, 'dk', 'Telegrafvej', '9', '2620', null, null);
GO

INSERT INTO Organisations (Name, LogoUrl, AddressId, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('PlanSphere', NULL, IDENT_CURRENT('Addresses'), 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO OrganisationSettings (OrganisationId, DefaultRoleId, DefaultWorkScheduleId)
VALUES (IDENT_CURRENT('Organisations'), IDENT_CURRENT('Roles'), 1);

INSERT INTO Companies (Name, LogoUrl, VAT, OrganisationId, AddressId, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('TEC', NULL, NULL, IDENT_CURRENT('Organisations'), IDENT_CURRENT('Addresses'), 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO CompanySettings (CompanyId, DefaultRoleId, DefaultWorkScheduleId, InheritDefaultWorkSchedule)
VALUES (IDENT_CURRENT('Companies'), IDENT_CURRENT('Roles'), 2, 1);

INSERT INTO Departments (Name, Description, Building, LogoUrl, CompanyId, AddressId, InheritAddress, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('SKP', NULL, 'E', NULL, IDENT_CURRENT('Companies'), IDENT_CURRENT('Addresses'), 1, 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO DepartmentSettings (DepartmentId, DefaultRoleId, DefaultWorkScheduleId, InheritDefaultWorkSchedule)
VALUES (IDENT_CURRENT('Departments'), IDENT_CURRENT('Roles'), 3, 1);

INSERT INTO Teams (Name, Description, Identifier, DepartmentId, AddressId, InheritAddress, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('Christians Team', NULL, NULL, IDENT_CURRENT('Departments'), IDENT_CURRENT('Addresses'), 1, 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO TeamSettings (TeamId, DefaultRoleId, DefaultWorkScheduleId, InheritDefaultWorkSchedule)
VALUES (IDENT_CURRENT('Teams'), IDENT_CURRENT('Roles'), 4, 1);
GO;
