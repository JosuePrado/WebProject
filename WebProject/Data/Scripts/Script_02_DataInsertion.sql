INSERT INTO Users (Email, Username, Password, CreatedAt)
VALUES 
('john_doe@example.com', 'john_doe', 'password123', NOW()),
('jane_smith@example.com', 'jane_smith', 'securePass456', NOW()),
('alice_jones@example.com', 'alice_jones', 'alicePass789', NOW());

INSERT INTO Channels (ChannelName, IsPrivate, CreatedAt)
VALUES
('General', false, NOW()),
('Development', true, NOW()),
('HR', true, NOW());

INSERT INTO ChannelMembers (ChannelId, UserId, JoinedAt)
VALUES
(1, 1, NOW()), 
(1, 2, NOW()),
(2, 1, NOW()),
(2, 3, NOW()),
(3, 2, NOW());

INSERT INTO Messages (ChannelId, UserId, Content, SentAt)
VALUES
(1, 1, 'Hello everyone!', NOW()),
(1, 2, 'Hi John!', NOW()),
(2, 1, 'We need to fix the bug in the system.', NOW()),
(2, 3, 'I am working on the feature update.', NOW()),
(3, 2, 'Meeting scheduled for next Monday.', NOW());
