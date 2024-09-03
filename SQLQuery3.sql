<<<<<<< HEAD
﻿SET IDENTITY_INSERT [Comment] ON;


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

INSERT INTO [Comment] ( [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
( 1, 1, 'C# is the best', 'Maybe. I will have to ponder on it.', SYSDATETIME());
=======
﻿USE [TabloidMVC]
GO

SET IDENTITY_INSERT [UserType] ON
INSERT INTO [UserType] ([ID], [Name]) VALUES (1, 'Admin'), (2, 'Author');
SET IDENTITY_INSERT [UserType] OFF


SET IDENTITY_INSERT [Category] ON
INSERT INTO [Category] ([Id], [Name]) 
VALUES (1, 'Technology'), (2, 'Close Magic'), (3, 'Politics'), (4, 'Science'), (5, 'Improv'), 
	   (6, 'Cthulhu Sightings'), (7, 'History'), (8, 'Home and Garden'), (9, 'Entertainment'), 
	   (10, 'Cooking'), (11, 'Music'), (12, 'Movies'), (13, 'Regrets');
SET IDENTITY_INSERT [Category] OFF


SET IDENTITY_INSERT [Tag] ON
INSERT INTO [Tag] ([Id], [Name])
VALUES (1, 'C#'), (2, 'JavaScript'), (3, 'Cyclopean Terrors'), (4, 'Family');
SET IDENTITY_INSERT [Tag] OFF
>>>>>>> 7639dcc496cd5683452628c19f8f5e58c265f224

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
	[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
<<<<<<< HEAD
VALUES (2, 'Fiona', 'Wee', 'fiona', 'fiona@example.com', SYSDATETIME(), NULL, 1);
SET IDENTITY_INSERT [UserProfile] OFF

INSERT INTO [Comment] ( [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
( 1, 2, 'C# is the best', 'I will need more time to decide.', SYSDATETIME());

SELECT * FROM [Comment]

DELETE FROM Comment
WHERE Id = 3;


SET IDENTITY_INSERT [Comment] ON;

INSERT INTO [Comment] ([Id], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
(3, 1, 1, 'C# is the best', 'If you say so..', SYSDATETIME());

INSERT INTO [Comment] ([Id], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
(4, 1, 2, 'C# is the best', 'I like it so far.', SYSDATETIME());

SET IDENTITY_INSERT [Comment] OFF;

SET IDENTITY_INSERT [Comment] ON;
INSERT INTO [Comment] ([Id], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
(10, 2, 1, 'Javascript for Beginners', 'It really is a good one for beginners.', SYSDATETIME());

INSERT INTO [Comment] ([Id], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
(11, 2, 2, 'Javascript for Beginners', 'Ok.', SYSDATETIME());

SET IDENTITY_INSERT [Comment] OFF;
=======
VALUES (1, 'Admina', 'Strator', 'admin', 'admin@example.com', SYSDATETIME(), NULL, 1);
SET IDENTITY_INSERT [UserProfile] OFF

SET IDENTITY_INSERT [Post] ON
INSERT INTO [Post] (
	[Id], [Title], [Content], [ImageLocation], [CreateDateTime], [PublishDateTime], [IsApproved], [CategoryId], [UserProfileId])
VALUES (
	1, 'C# is the Best Language', 
'There are those' + char(10) + 'who do not believe' + char(10) + 'C# is the best.' + char(10) + 'They are wrong.',
    'https://gizmodiva.com/wp-content/uploads/2017/10/SCOTT-A-WOODWARD_1SW1943-1170x689.jpg',SYSDATETIME(), SYSDATETIME(), 1, 1, 1);
SET IDENTITY_INSERT [Post] OFF
>>>>>>> 7639dcc496cd5683452628c19f8f5e58c265f224
