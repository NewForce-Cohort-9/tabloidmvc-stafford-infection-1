SET IDENTITY_INSERT [Comment] ON;

INSERT INTO [Comment] ([Id], [PostId], [UserProfileId], [Subject], [Content], [CreateDateTime]) 
VALUES 
(1, 1, 1, 'C# is the best', 'I do agree', SYSDATETIME());

SET IDENTITY_INSERT [Comment] OFF;

SELECT* FROM Comment

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

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
	[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
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