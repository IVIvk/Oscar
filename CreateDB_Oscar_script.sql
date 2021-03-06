﻿CREATE DATABASE Oscar
GO

USE [Oscar]
GO
/****** Object:  Table [dbo].[Actors]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actors](
	[ActorId] [uniqueidentifier] NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActorsInFilms]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorsInFilms](
	[FilmId] [uniqueidentifier] NULL,
	[ActorId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Films]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Films](
	[FilmId] [uniqueidentifier] NULL,
	[FilmTitle] [nvarchar](100) NULL,
	[ReleaseYear] [int] NULL,
	[FilmLengthInMinutes] [int] NULL,
	[FilmRating] [float] NULL,
	[AmountOfRatings] [int] NULL,
	[FilmPlot] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenreId] [uniqueidentifier] NULL,
	[GenreName] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenresInFilms]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenresInFilms](
	[FilmId] [uniqueidentifier] NULL,
	[GenreId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewId] [uniqueidentifier] NULL,
	[ReviewContent] [nvarchar](max) NULL,
	[ReviewScore] [int] NULL,
	[UserId] [nvarchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewsForFilms]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewsForFilms](
	[FilmId] [uniqueidentifier] NULL,
	[ReviewId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/06/2019 13:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [nvarchar](100) NULL,
	[UserPassword] [nvarchar](100) NULL,
	[UserAdmin] [nvarchar](5) NULL
) ON [PRIMARY]
GO
		     
INSERT INTO Users(UserId, UserPassword, UserAdmin)
VALUES ('Admin', 'Admin', 'true'); 
GO
