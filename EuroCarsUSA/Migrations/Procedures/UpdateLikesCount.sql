DROP PROCEDURE IF EXISTS UpdateLikesCount
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE UpdateLikesCount
    @CarId UNIQUEIDENTIFIER,
    @Increment BIT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM CarStatistics WHERE CarId = @CarId)
    BEGIN
        INSERT INTO CarStatistics (Id, CarId, Likes, Views)
        VALUES (NEWID(), @CarId, 0, 0);
    END

    IF @Increment = 1
    BEGIN
        UPDATE CarStatistics
        SET Likes = Likes + 1
        WHERE CarId = @CarId;
    END
    ELSE
    BEGIN
        UPDATE CarStatistics
        SET Likes = Likes - 1
        WHERE CarId = @CarId;
    END
END;

GO