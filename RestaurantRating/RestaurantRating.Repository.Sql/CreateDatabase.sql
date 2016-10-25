--Clear Database 
DROP TABLE dbo.Review;
DROP TABLE dbo.Restaurant;
DROP TABLE dbo.Cuisine;
DROP TABLE dbo.AppUser;

--Create Schema 
CREATE TABLE dbo.AppUser (
    Id          INT           NOT NULL IDENTITY(1,1),
    UserName    VARCHAR (10) NOT NULL,
    FirstName   VARCHAR (25) NOT NULL,
    LastName    VARCHAR (35)    NOT NULL,
    CreatedBy   INT           NULL,
    UpdatedBy   INT           NULL,
    CreatedDate DATETIME      NOT NULL DEFAULT(getdate()),
    UpdatedDate DATETIME      NOT NULL DEFAULT(getdate()),
    PRIMARY KEY CLUSTERED (Id ASC)
);

CREATE TABLE dbo.Cuisine 
(
    Id          INT        NOT NULL IDENTITY(1,1),
    Name        VARCHAR (10) NOT NULL,
    CreatedBy   INT        NULL,
    UpdatedBy   INT        NULL,
    CreatedDate DATETIME      NOT NULL DEFAULT(getdate()),
    UpdatedDate DATETIME      NOT NULL DEFAULT(getdate()),
	PRIMARY KEY CLUSTERED (Id ASC)
);

CREATE TABLE dbo.Restaurant 
(
    Id          INT           NOT NULL IDENTITY(1,1),
    Name        VARCHAR (50) NOT NULL,
    CreatedBy   INT           NULL,
    UpdatedBy   INT           NULL,
    CreatedDate DATETIME      NOT NULL DEFAULT(getdate()),
    UpdatedDate DATETIME      NOT NULL DEFAULT(getdate()),
	CuisineId	INT			NOT NULL,
    PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT Cuisine_FK FOREIGN KEY (CuisineID) 
		REFERENCES dbo.Cuisine(ID)
);

CREATE TABLE dbo.Review (
    ReviewNumber INT            NOT NULL IDENTITY(1,1),
    Rating       INT            NOT NULL,
    PostedDate   DATETIME       NOT NULL,
    Comment      NVARCHAR (160) NULL,
    ReviewUser   INT            NULL,
    CreatedBy    INT            NULL,
    UpdatedBy    INT            NULL,
    CreatedDate DATETIME      NOT NULL DEFAULT(getdate()),
    UpdatedDate DATETIME      NOT NULL DEFAULT(getdate()),
	RestaurantId INT			NOT NULL
    PRIMARY KEY CLUSTERED (ReviewNumber ASC),
	CONSTRAINT Restaurant_FK FOREIGN KEY (RestaurantId) 
		REFERENCES dbo.Restaurant(ID),
	CONSTRAINT AppUser_FK FOREIGN KEY (ReviewUser) 
		REFERENCES dbo.AppUser(Id)
);

CREATE INDEX reviewDtRest ON dbo.Review(RestaurantId, PostedDate);

---
--Seeding
USE [Restaurant]
GO
SET IDENTITY_INSERT [dbo].[AppUser] ON 

INSERT [dbo].[AppUser] ([Id], [UserName], [FirstName], [LastName], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (1, N'TK', N'Tharindu', N'Kumarasinghe', 1, 1, CAST(N'2016-10-24T18:27:26.020' AS DateTime), CAST(N'2016-10-24T18:27:26.020' AS DateTime))
INSERT [dbo].[AppUser] ([Id], [UserName], [FirstName], [LastName], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (2, N'John', N'John', N'Smith', 1, 1, CAST(N'2016-10-24T18:27:42.293' AS DateTime), CAST(N'2016-10-24T18:27:42.293' AS DateTime))
INSERT [dbo].[AppUser] ([Id], [UserName], [FirstName], [LastName], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (3, N'Steph', N'srephenie', N'Mayer', 1, 1, CAST(N'2016-10-24T18:28:18.357' AS DateTime), CAST(N'2016-10-24T18:28:18.357' AS DateTime))
INSERT [dbo].[AppUser] ([Id], [UserName], [FirstName], [LastName], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (4, N'Mike', N'Michal', N'Wolski', 1, 1, CAST(N'2016-10-24T18:29:00.397' AS DateTime), CAST(N'2016-10-24T18:29:00.397' AS DateTime))
INSERT [dbo].[AppUser] ([Id], [UserName], [FirstName], [LastName], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (5, N'Jane', N'Jane', N'Wolski', 1, 1, CAST(N'2016-10-24T18:29:00.397' AS DateTime), CAST(N'2016-10-24T18:29:00.397' AS DateTime))
SET IDENTITY_INSERT [dbo].[AppUser] OFF
SET IDENTITY_INSERT [dbo].[Cuisine] ON 

INSERT [dbo].[Cuisine] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (1, N'American', 1, 1, CAST(N'2016-10-24T18:37:46.317' AS DateTime), CAST(N'2016-10-24T18:37:46.317' AS DateTime))
INSERT [dbo].[Cuisine] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (2, N'Indian', 1, 1, CAST(N'2016-10-24T18:38:34.250' AS DateTime), CAST(N'2016-10-24T18:38:34.250' AS DateTime))
INSERT [dbo].[Cuisine] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (3, N'Italian', 2, NULL, CAST(N'2016-10-24T18:38:47.240' AS DateTime), CAST(N'2016-10-24T18:38:47.240' AS DateTime))
INSERT [dbo].[Cuisine] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (4, N'Mexican', 1, 1, CAST(N'2016-10-24T18:39:16.067' AS DateTime), CAST(N'2016-10-24T18:39:16.067' AS DateTime))
INSERT [dbo].[Cuisine] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (5, N'Thai', 1, 1, CAST(N'2016-10-24T18:39:44.547' AS DateTime), CAST(N'2016-10-24T18:39:44.547' AS DateTime))
INSERT [dbo].[Cuisine] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate]) VALUES (6, N'Chinese', 1, 1, CAST(N'2016-10-24T18:40:22.557' AS DateTime), CAST(N'2016-10-24T18:40:22.557' AS DateTime))
SET IDENTITY_INSERT [dbo].[Cuisine] OFF
SET IDENTITY_INSERT [dbo].[Restaurant] ON 

INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (1, N'Great Wall Restaurant', 1, NULL, CAST(N'2016-10-24T18:41:54.903' AS DateTime), CAST(N'2016-10-24T18:41:54.903' AS DateTime), 6)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (2, N'Ni Hao', 1, NULL, CAST(N'2016-10-24T18:42:15.287' AS DateTime), CAST(N'2016-10-24T18:42:15.287' AS DateTime), 6)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (3, N'Chen''s Gourmet Chinese Restaurant', 2, NULL, CAST(N'2016-10-24T18:42:33.127' AS DateTime), CAST(N'2016-10-24T18:42:33.127' AS DateTime), 6)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (4, N'Xee Yoo', 2, 2, CAST(N'2016-10-24T18:42:49.753' AS DateTime), CAST(N'2016-10-24T18:42:49.753' AS DateTime), 6)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (5, N'Chinatown Kitchen', 1, 1, CAST(N'2016-10-24T18:43:05.030' AS DateTime), CAST(N'2016-10-24T18:43:05.030' AS DateTime), 6)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (6, N'Pattaya Thai', 1, 1, CAST(N'2016-10-24T18:43:31.997' AS DateTime), CAST(N'2016-10-24T18:43:31.997' AS DateTime), 5)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (7, N'Singha Thai Restaurant', 1, 1, CAST(N'2016-10-24T18:43:45.440' AS DateTime), CAST(N'2016-10-24T18:43:45.440' AS DateTime), 5)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (8, N'Thai Lotus', 1, 1, CAST(N'2016-10-24T18:44:03.443' AS DateTime), CAST(N'2016-10-24T18:44:03.443' AS DateTime), 5)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (9, N'Thai-Namite', 1, NULL, CAST(N'2016-10-24T18:44:20.713' AS DateTime), CAST(N'2016-10-24T18:44:20.713' AS DateTime), 5)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (11, N'Su Casa Grande', 1, NULL, CAST(N'2016-10-24T18:44:54.400' AS DateTime), CAST(N'2016-10-24T18:44:54.400' AS DateTime), 4)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (12, N'Chipotle Mexican Grill', 1, NULL, CAST(N'2016-10-24T18:45:07.770' AS DateTime), CAST(N'2016-10-24T18:45:07.770' AS DateTime), 4)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (13, N'India Garden', 1, NULL, CAST(N'2016-10-24T18:45:32.763' AS DateTime), CAST(N'2016-10-24T18:45:32.763' AS DateTime), 1)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (14, N'Taste of India', 1, NULL, CAST(N'2016-10-24T18:45:42.757' AS DateTime), CAST(N'2016-10-24T18:45:42.757' AS DateTime), 1)
INSERT [dbo].[Restaurant] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [CuisineId]) VALUES (15, N'Maharaja', 1, NULL, CAST(N'2016-10-24T18:46:01.390' AS DateTime), CAST(N'2016-10-24T18:46:01.390' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Restaurant] OFF
