INSERT INTO Countries (IsoCode, Name, IsdCode)
VALUES
    ('dk', 'Danmark', '+45');
GO;

INSERT INTO ZipCodes(PostalCode, Name, CountryId)
VALUES
    ('2620', 'Albertslund', 'dk');
GO;


INSERT INTO Rights (Name, Description)
VALUES 
    ('Administrator', 'Giver administrator rettighed til alle med rollen.'),
    ('Edit', 'Giver administrator rettighed til alle med rollen.'),
    ('ManageTimes', 'Giver administrator rettighed til alle med rollen.'),
    ('ManageUsers', 'Giver administrator rettighed til alle med rollen.'),
    ('PureView', 'Giver administrator rettighed til alle med rollen.'),
    ('View', 'Giver administrator rettighed til alle med rollen.'),
    ('SetOwnWorkSchedule', 'Giver administrator rettighed til alle med rollen.'),
    ('ManuallySetOwnWorkTime', 'Giver administrator rettighed til alle med rollen.'),
    ('SetOwnJobTitle', 'Giver administrator rettighed til alle med rollen.'),
    ('SetAutomaticCheckInOut', 'Giver administrator rettighed til alle med rollen.');
GO;

INSERT INTO Roles (Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('DefaultRole', CURRENT_TIMESTAMP, NULL, NULL, NULL);
GO

INSERT INTO WorkSchedules (IsDefaultWorkSchedule)
VALUES (1);
GO

INSERT INTO Addresses (ParentId, CountryId, StreetName, HouseNumber, PostalCode, Door, Floor)
VALUES (NULL, 'dk', 'Telegrafvej', '9', '2620', null, null);
GO

INSERT INTO Organisations (Name, LogoUrl, AddressId, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('PlanSphere', NULL, IDENT_CURRENT('Addresses'), 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO OrganisationSettings (OrganisationId, DefaultRoleId, DefaultWorkScheduleId)
VALUES (IDENT_CURRENT('Organisations'), IDENT_CURRENT('Roles'), IDENT_CURRENT('WorkSchedules'));

INSERT INTO Companies (Name, LogoUrl, VAT, OrganisationId, AddressId, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('TEC', NULL, NULL, IDENT_CURRENT('Organisations'), IDENT_CURRENT('Addresses'), 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO CompanySettings (CompanyId, DefaultRoleId, DefaultWorkScheduleId, InheritDefaultWorkSchedule)
VALUES (IDENT_CURRENT('Companies'), IDENT_CURRENT('Roles'), IDENT_CURRENT('WorkSchedules'), 1);

INSERT INTO Departments (Name, Description, Building, LogoUrl, CompanyId, AddressId, InheritAddress, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('SKP', NULL, 'E', NULL, IDENT_CURRENT('Companies'), IDENT_CURRENT('Addresses'), 1, 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO DepartmentSettings (DepartmentId, DefaultRoleId, DefaultWorkScheduleId, InheritDefaultWorkSchedule)
VALUES (IDENT_CURRENT('Departments'), IDENT_CURRENT('Roles'), IDENT_CURRENT('WorkSchedules'), 1);

INSERT INTO Teams (Name, Description, Identifier, DepartmentId, AddressId, InheritAddress, SettingsId, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
VALUES ('Christians Team', NULL, NULL, IDENT_CURRENT('Departments'), IDENT_CURRENT('Addresses'), 1, 1, CURRENT_TIMESTAMP, NULL, NULL, NULL);

INSERT INTO TeamSettings (TeamId, DefaultRoleId, DefaultWorkScheduleId, InheritDefaultWorkSchedule)
VALUES (IDENT_CURRENT('Teams'), IDENT_CURRENT('Roles'), IDENT_CURRENT('WorkSchedules'), 1);
GO;
