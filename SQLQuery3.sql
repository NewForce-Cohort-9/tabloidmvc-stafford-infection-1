

SET IDENTITY_INSERT [Comment] OFF;

SELECT* FROM Comment
SET IDENTITY_INSERT [Category] ON
INSERT INTO [Category] ([Id], [Name]) 
VALUES (1, 'Technology'), (2, 'Close Magic'), (3, 'Politics'), (4, 'Science'), (5, 'Improv'), 
	   (6, 'Cthulhu Sightings'), (7, 'History'), (8, 'Home and Garden'), (9, 'Entertainment'), 
	   (10, 'Cooking'), (11, 'Music'), (12, 'Movies'), (13, 'Regrets');
SET IDENTITY_INSERT [Category] OFF

SELECT 
    c.Id, 
    c.Subject, 
    c.Content, 
    c.CreateDateTime, 
    c.PostId, 
    u.DisplayName
FROM Comment c
LEFT JOIN UserProfile u ON c.UserProfileId = u.Id


SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
	[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
SET IDENTITY_INSERT [UserProfile] OFF

