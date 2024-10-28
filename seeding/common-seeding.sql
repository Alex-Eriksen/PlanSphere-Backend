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