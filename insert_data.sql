USE [dotnetweb]
GO
SET IDENTITY_INSERT [dbo].[genres] ON 

INSERT [dbo].[genres] ([Id], [Name]) VALUES (1, N'Hành động')
INSERT [dbo].[genres] ([Id], [Name]) VALUES (2, N'Phiêu lưu')
INSERT [dbo].[genres] ([Id], [Name]) VALUES (3, N'Viễn tưởng')
INSERT [dbo].[genres] ([Id], [Name]) VALUES (4, N'Giật gân')
INSERT [dbo].[genres] ([Id], [Name]) VALUES (5, N'Thần thoại')
INSERT [dbo].[genres] ([Id], [Name]) VALUES (6, N'Chính kịch')
SET IDENTITY_INSERT [dbo].[genres] OFF
GO
SET IDENTITY_INSERT [dbo].[nations] ON 

INSERT [dbo].[nations] ([Id], [Name]) VALUES (1, N'Mỹ')
INSERT [dbo].[nations] ([Id], [Name]) VALUES (2, N'Hàn Quốc')
INSERT [dbo].[nations] ([Id], [Name]) VALUES (3, N'Việt Nam')
SET IDENTITY_INSERT [dbo].[nations] OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON 

INSERT [dbo].[roles] ([Id], [Name]) VALUES (1, N'ROLE_USER')
INSERT [dbo].[roles] ([Id], [Name]) VALUES (2, N'ROLE_ADMIN')
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([Id], [UserName], [Email], [PasswordHash], [PasswordSalt], [Otp], [ResetPasswordExpiry], [IsPermit], [createdDate], [RoleId]) VALUES (1, N'admin', N'admin@gmail.com', 0x7151CEB2F5BF52D5DC7192FCFE96B6A704718BB124FA0EB50D5BCE09B11D281EDECD1B2F10C785DC59124B4F34907FFD64C82090574F124101D9A014FBD90CEF, 0x89A58DFAE62E4E13074C4EAE930A1411527ADD85192915A5C80E4671E832D26EA278E03217BB8CBDCF267C381818F90D7BAFB8456830BBAA6F272AD524BE29DB56F2414657A0BAE52A5C7E303AE002B6953FFA7F228EDD96C81586CD3441BDBE035C527C9642F03914DE1BA0F5DBD733CDDE54CAE878BAA8D989FFE54F6AFE8D, N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2023-10-23T20:52:30.1833654' AS DateTime2), 2)
INSERT [dbo].[users] ([Id], [UserName], [Email], [PasswordHash], [PasswordSalt], [Otp], [ResetPasswordExpiry], [IsPermit], [createdDate], [RoleId]) VALUES (2, N'user', N'user@gmail.com', 0x1B0390A905B391FE2A04604FAD310B639194469A450D1A68FD6318149BD5EC4C3C04FC7400E45DE6D3C4E9FB613449DC91F324BA77139D08F4187C83C615A13A, 0xF71ED19A7FE5CAD3133FA5F3D9B0F9F42B0FE805E7AA4C7607A8F52277403743F14F6B6D0D807123E0680438F38829138ADE9D1C7BDE6A417A1C74F2743285DABA9511A79F2FC08A589203C28715C03F4970A72269906DF599B43DD35EB84001AAFD91B4ECE521014B4650FE7A65578007D52BCC8DF644D3BC66796B853BAE02, N'', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2023-10-23T21:44:40.6662379' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET IDENTITY_INSERT [dbo].[films] ON 

INSERT [dbo].[films] ([Id], [Name], [ReleaseYear], [Description], [IMDBScore], [View], [BackgroundUrl], [LogoUrl], [PosterUrl], [TrailerUrl], [isActiveBanner], [UploadDate], [NationId], [UserId]) VALUES (19, N'Blue Beetle', N'2023', N'Cậu sinh viên mới tốt nghiệp Jaime Reyes trở về nhà với tràn trề niềm tin và hy vọng về tương lai, để rồi nhận ra quê nhà của anh đã thay đổi rất nhiều so với trước đây. Khi tìm kiếm mục đích sống trên thế giới này, Jamie đối mặt với bước ngoặt cuộc đời khi anh nhận ra mình sở hữu một di sản cổ đại của công nghệ sinh học ngoài hành tinh: Scarab (Bọ Hung). Khi Scarab chọn Jamie trở thành vật chủ, anh được ban tặng một bộ áo giáp với siêu sức mạnh đáng kinh ngạc không ai có thể lường trước. Số phận của Jamie hoàn toàn thay đổi khi giờ đây, anh đã là siêu anh hùng BLUE BEETLE.', 6.1, 2, N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Fblue-beetle%2FImage%2F0ca4b572-7717-4c1d-93af-7ced898e3a01.jpg?alt=media&token=3f4fe64a-4698-4cfe-9bfc-6ef10ec9f38a', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Fblue-beetle%2FImage%2F11d1dd84-5e08-43fd-b8b6-2d4ef75ab8d7.png?alt=media&token=359192db-ca67-470c-b1a3-06fbe21dbd1a', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Fblue-beetle%2FImage%2Fffd878b7-1b55-4551-91ba-d215ed31e2f4.jpg?alt=media&token=fc4c7ce5-ba5e-483c-862f-59beffb7bd04', N'https://www.youtube.com/watch?v=vS3_72Gb-bI', 1, CAST(N'2023-10-31T11:01:09.1150000' AS DateTime2), 1, 1)
INSERT [dbo].[films] ([Id], [Name], [ReleaseYear], [Description], [IMDBScore], [View], [BackgroundUrl], [LogoUrl], [PosterUrl], [TrailerUrl], [isActiveBanner], [UploadDate], [NationId], [UserId]) VALUES (20, N'Loki SS1', N'2021', N'Một chương mới của Marvel với Loki là trung tâm của câu chuyện.', 8.2, 0, N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FTVSeries%2Floki-ss1%2FImage%2Fa5a63b42-79a6-4c9b-8215-2cc754ce2e7f.jpg?alt=media&token=ab76ffef-597c-4403-b5e1-23b6d3feb72f', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FTVSeries%2Floki-ss1%2FImage%2F19196b20-1b44-47b0-b33d-a8a7b093dcfe.png?alt=media&token=025a6b38-f1a7-4d3c-bb5b-06cbff036f52', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FTVSeries%2Floki-ss1%2FImage%2F7514e0e9-8170-4d61-8865-69181ccb4bb4.jpg?alt=media&token=bcd6c635-4472-4773-980e-df08f1f2da03', N'https://www.youtube.com/watch?v=G4JuopziR3Q', 1, CAST(N'2023-10-31T11:05:04.6740000' AS DateTime2), 1, 1)
INSERT [dbo].[films] ([Id], [Name], [ReleaseYear], [Description], [IMDBScore], [View], [BackgroundUrl], [LogoUrl], [PosterUrl], [TrailerUrl], [isActiveBanner], [UploadDate], [NationId], [UserId]) VALUES (1015, N'Oppenheimer', N'2023', N'Với nhân vật trung tâm là J. Robert Oppenheimer, nhà vật lý lý thuyết người đứng đầu phòng thí nghiệm Los Alamos, thời kỳ Thế chiến II. Ông đóng vai trò quan trọng trong Dự án Manhattan, tiên phong trong nhiệm vụ phát triển vũ khí hạt nhân, và được coi là một trong những “cha đẻ của bom nguyên tử”.', 8.5, 1, N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Foppenheimer%2FImage%2Ff37987f3-bbdd-497f-98b5-2b1485d5c203.jpg?alt=media&token=b9624992-c674-4b1f-9682-28d5b3d8e8b1', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Foppenheimer%2FImage%2Fcec0aea1-fe1f-4fce-8ee2-b9496e54a8ec.png?alt=media&token=5fc6aa28-5278-4908-905d-2125ce11bc33', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Foppenheimer%2FImage%2F1eb2a37e-db1c-43f7-84f4-58a97702b8a2.jpg?alt=media&token=6f227b8f-0499-4d5e-ab5f-0dd7b463c798', N'https://www.youtube.com/watch?v=UdFeVo0cODs', 0, CAST(N'2023-11-18T10:40:00.4420000' AS DateTime2), 1, NULL)
SET IDENTITY_INSERT [dbo].[films] OFF
GO
SET IDENTITY_INSERT [dbo].[filmgenres] ON 

INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1004, 19, 3)
INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1005, 19, 1)
INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1006, 19, 2)
INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1007, 20, 3)
INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1008, 20, 5)
INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1009, 20, 6)
INSERT [dbo].[filmgenres] ([Id], [FilmId], [GenreId]) VALUES (1012, 1015, 3)
SET IDENTITY_INSERT [dbo].[filmgenres] OFF
GO
SET IDENTITY_INSERT [dbo].[directors] ON 

INSERT [dbo].[directors] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (3, N'Angel Manuel Soto', CAST(N'1983-01-28T00:00:00.0000000' AS DateTime2), N'Male', N'Santurce, Puerto Rico', N'Thiên thần Manuel Soto sinh ra ở Santurce, Puerto Rico. Con trai của một nhân viên bán xe và một tiếp viên hàng không. Nghiên cứu kiến trúc và quảng cáo. Luôn yêu thích những bộ phim. Bây giờ anh ấy làm chúng. Anh ấy là một người mê điện ảnh. Anh ấy đi khắp thế giới để làm việc đó, bao gồm Úc, Thái Lan, Campuchia, Pháp, Mỹ và Puerto Rico. Anh ấy không có ý định dừng lại.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Directors%2FAvatars%2F55f9871f-fa34-415f-b3ec-0233dce8381c.jpg?alt=media&token=a51e42c1-c3c4-45c4-8ca5-11c5dd429a32')
INSERT [dbo].[directors] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (6, N'Christopher McQuarrie', CAST(N'1968-10-25T00:00:00.0000000' AS DateTime2), N'Male', N'Princeton Junction, New Jersey, USA', N'Christopher McQuarrie là nhà biên kịch, nhà sản xuất và đạo diễn người Mỹ. Các kịch bản phim của anh bao gồm Những nghi phạm thông thường, anh đã giành được Giải thưởng Học viện năm 1996, Con đường của súng và Valkyrie. Ông là tác giả của loạt phim truyền hình NBC Persons Unknown.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Directors%2FAvatars%2F39accbe2-40ce-4c12-b1f5-35238b6abb88.jpg?alt=media&token=be1fad31-29ce-408f-88d3-1b6f8084b966')
INSERT [dbo].[directors] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (7, N'James Machine Gunn', CAST(N'1966-05-08T00:00:00.0000000' AS DateTime2), N'Male', N'St. Louis, Missouri, USA', N'James Gunn là một nhà văn, nhà làm phim, diễn viên, nhạc sĩ và họa sĩ truyện tranh người Mỹ.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Directors%2FAvatars%2Ff6c8902e-306f-4123-aa45-e68ccf8206bd.jpg?alt=media&token=4c85ef22-7568-4fd0-b79c-caf3d09216ff')
INSERT [dbo].[directors] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (8, N'Michael Waldron', CAST(N'1987-04-23T00:00:00.0000000' AS DateTime2), N'Male', N'West Orange, New Jersey, USA', N'Michael Waldron là nhà văn kiêm nhà sản xuất, được biết đến với Loki (2021) và Doctor Strange In the Multiverse of Madness (2021).', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Directors%2FAvatars%2Ff69c2c77-cfe0-4111-b486-7a11b3bafc2b.jpg?alt=media&token=484ffe88-7964-4ea5-a556-a98107af3d27')
SET IDENTITY_INSERT [dbo].[directors] OFF
GO
SET IDENTITY_INSERT [dbo].[filmdirectors] ON 

INSERT [dbo].[filmdirectors] ([Id], [DirectorId], [FilmId]) VALUES (1014, 3, 19)
INSERT [dbo].[filmdirectors] ([Id], [DirectorId], [FilmId]) VALUES (1015, 8, 20)
SET IDENTITY_INSERT [dbo].[filmdirectors] OFF
GO
SET IDENTITY_INSERT [dbo].[casts] ON 

INSERT [dbo].[casts] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (7, N'Xolo Mariduena Jr', CAST(N'2001-09-06T00:00:00.0000000' AS DateTime2), N'Male', N'Los Angeles, California, USA', N'Xolo Maridueña sinh ra ở Los Angeles, California vào ngày 9 tháng 6 năm 2001, Maridueña mới được ba ngày tuổi khi lần đầu tiên được nếm thử những pha hành động hậu trường trong một studio của đài phát thanh. Xolo đã lớn lên khi xem các nghệ sĩ biểu diễn tận mắt, điều này đã khơi dậy niềm hứng thú mãnh liệt của anh ấy trong việc trở thành một nghệ sĩ biểu diễn theo đúng nghĩa của mình.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Casts%2FAvatars%2F30209aa6-4770-4577-92d5-b0ef8cb793eb.jpg?alt=media&token=95a3c16c-4f44-466b-8a39-145ab4547a71')
INSERT [dbo].[casts] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (8, N'Bruna Marquezine', CAST(N'1995-08-04T00:00:00.0000000' AS DateTime2), N'Female', N'Duque de Caxias, Rio de Janeiro, Brazil', N'Bruna Reis Maia (sinh ngày 4 tháng 8 năm 1995), được biết đến với cái tên Bruna Marquezine, là một nữ diễn viên người Brazil. Marquezine bắt đầu sự nghiệp truyền hình của mình vào năm 2000. Kể từ đó cô đã tham gia diễn xuất trong nhiều chương trình và phim của Brazil. Cô đóng vai đầu tiên trong một bộ phim nói tiếng Anh khi đóng vai Roseli trong Breaking Through (2015). Tuy nhiên, vai diễn lớn nhất của cô ở Hollywood là vai diễn trong Blue Beetle (2023), khi cô đóng vai Jenny Kord, người yêu của Jaime Reyes.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Casts%2FAvatars%2Ff3f0ce56-0340-4514-8bbc-61d794d6e42d.jpg?alt=media&token=2ed937ff-af2a-4030-9376-a8e51e1baf80')
INSERT [dbo].[casts] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (13, N'Tom Cruise', CAST(N'1962-03-07T00:00:00.0000000' AS DateTime2), N'Male', N'Syracuse, New York, USA', N'Một diễn viên và nhà làm phim người Mỹ. Anh đã được đề cử ba giải Oscar và đã giành được ba giải Quả cầu vàng. Anh bắt đầu sự nghiệp ở tuổi 19 trong bộ phim Tình yêu bất tận năm 1981. Sau khi thể hiện vai phụ trong Taps (1981) và The Outsiders (1983), vai chính đầu tiên của anh là Risky Business, được phát hành vào tháng 8 năm 1983. Cruise trở thành một ngôi sao điện ảnh đầy đủ sau khi đóng vai chính Pete "Maverick" Mitchell trong Top Gun ( 1986). Anh từ năm 1996 nổi tiếng với vai trò là điệp viên bí mật Ethan Hunt trong loạt phim Mission: Impossible. Một trong những ngôi sao điện ảnh lớn nhất Hollywood, Cruise đã tham gia nhiều bộ phim thành công, bao gồm The Color of Money (1986), Cocktail (1988), Rain Man (1988), Sinh ra vào thứ tư của tháng 7 (1989), Far and Away (1992), A Few Good Men (1992), The Firm (1993), Phỏng vấn ma cà rồng: Biên niên sử ma cà rồng (1994), Jerry Maguire (1996), Eyes Wide Shut (1999), Magnolia (1999), Vanilla Sky (2001), Báo cáo thiểu số (2002), Samurai cuối cùng (2003), Tài sản thế chấp (2004), Chiến tranh thế giới (2005), Lions for Lambs (2007), Valkyrie (2008), Hiệp sĩ và ngày (2010), Jack Reacher (2012), Oblivion (2013) và Edge of Tomorrow (2014). Năm 2012, Cruise là diễn viên được trả lương cao nhất Hollywood. Mười lăm bộ phim của ông đã thu về hơn 100 triệu đô la trong nước; Hai mươi mốt đã thu về hơn 200 triệu đô la trên toàn thế giới. Cruise được biết đến với sự hỗ trợ của ông cho Nhà thờ Khoa học và các chương trình xã hội trực thuộc.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Casts%2FAvatars%2Ffb7bb8b1-af56-4ddd-8d7d-2bd34a5c7b28.jpg?alt=media&token=fe7924fe-3e7b-4016-b17b-bf6f80d70a1c')
INSERT [dbo].[casts] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (15, N'Chris Pratt', CAST(N'1979-06-21T00:00:00.0000000' AS DateTime2), N'Male', N'Virginia, Minnesota, USA', N'Christopher Michael "Chris" Pratt (sinh ngày 21 tháng 6 năm 1979) là một diễn viên người Mỹ, nổi tiếng với vai Harold Brighton "Bright" Abbott trong loạt phim truyền hình Everwood, nhân vật định kỳ Winchester "Ché" Cook trong phần 4 của The OC và Andy Dwyer trong loạt phim truyền hình Công viên và Giải trí.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Casts%2FAvatars%2Fb4ada1ec-4fc2-46b6-b24e-9f60a6bedfa7.jpg?alt=media&token=c3a7344e-fa63-425c-aabd-f2f61a5b0762')
INSERT [dbo].[casts] ([Id], [Name], [BirthDate], [Gender], [Nation], [Description], [AvatarUrl]) VALUES (19, N'Tom Hiddleston', CAST(N'1981-02-09T00:00:00.0000000' AS DateTime2), N'Male', N'Westminster, London, England, UK', N'Thomas William "Tom" Hiddleston (sinh ngày 9 tháng 2 năm 1981) là một diễn viên người Anh. Anh ta có lẽ được biết đến nhiều nhất khi đóng vai Loki trong bộ phim hành động năm 2011 Thor.', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Casts%2FAvatars%2F6e50f991-2ca8-4a4b-9b0c-4b8348a9809c.jpg?alt=media&token=9ad052d3-f790-4e07-ad47-2b66418f7fcd')
SET IDENTITY_INSERT [dbo].[casts] OFF
GO
SET IDENTITY_INSERT [dbo].[filmcasts] ON 

INSERT [dbo].[filmcasts] ([Id], [FilmId], [CastId], [Role]) VALUES (1022, 19, 7, N'Jaime Reyes')
INSERT [dbo].[filmcasts] ([Id], [FilmId], [CastId], [Role]) VALUES (1023, 19, 8, N'Jenny Kord')
INSERT [dbo].[filmcasts] ([Id], [FilmId], [CastId], [Role]) VALUES (1024, 20, 19, N'Loki')
SET IDENTITY_INSERT [dbo].[filmcasts] OFF
GO
INSERT [dbo].[movies] ([Id], [Time], [Link]) VALUES (19, N'2 giờ 7 phút', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Fblue-beetle%2FVideo%2Fc75265af-b083-47a6-883f-e84b8a29d7f0.mp4?alt=media&token=06ab9832-7b0a-47e3-9be0-c51af63c7a42')
INSERT [dbo].[movies] ([Id], [Time], [Link]) VALUES (1015, N'3 giờ 20 phút', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FMovies%2Foppenheimer%2FVideo%2Ffa2eddad-f55c-419a-9974-910cd9be91b2.mp4?alt=media&token=54d2d953-9c10-463a-883a-c338f9196c27')
GO
INSERT [dbo].[tvseries] ([Id]) VALUES (20)
GO
SET IDENTITY_INSERT [dbo].[episodes] ON 

INSERT [dbo].[episodes] ([Id], [EpisodeName], [Time], [Link], [View], [SeriesId], [EpisodeNumber]) VALUES (9, N'Glorious Purpose', N'59 phút', N'https://firebasestorage.googleapis.com/v0/b/dotnetproject-339c8.appspot.com/o/Films%2FTVSeries%2Floki-ss1%2Fglorious-purpose%2FVideo%2Fbbe31176-3af1-4f74-ae2e-d6d88f4a3eb7.mp4?alt=media&token=e353e504-58fa-4675-8f2c-aa01c0af5931', 0, 20, 1)
SET IDENTITY_INSERT [dbo].[episodes] OFF
GO
