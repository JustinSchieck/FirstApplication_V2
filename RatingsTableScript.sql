CREATE TABLE dbo.Ratings{
	RatingId nvarchar(128) NOT NULL DEFAULT (newid()),
	UserId nvarchar(128) NOT NULL,
	GameId nvarchar(128) NOT NULL,
	Rating decimal DEFAULT(9) NOT NULL CHECK(Rating BETWEEN 0 AND 9),
	CreateDate datetime NOT NULL DEFAULT (getdate()),
 	EditDate datetime NOT NULL DEFAULT (getdate()),
	CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED
	{
		[RatingId] ASC
	}WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = on)
};

ALTER TABLE dbo.Ratings With CHECK ADD CONSTRAINT [FK_Ratings_AppNetUsers] FOREIGN KEY([UserId]) REFERENCES dbo.AspNetUsers([Id]);

ALTER TABLE dbo.Ratings With CHECK ADD CONSTRAINT [FK_Ratings_Games] FOREIGN KEY([GameId]) REFERENCES dbo.Games([GameId]);