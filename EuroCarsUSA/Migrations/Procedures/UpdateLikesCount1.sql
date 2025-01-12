/****** Object:  StoredProcedure [dbo].[UpdateLikesCount]    Script Date: 1/12/2025 6:19:07 PM ******/
DROP PROCEDURE IF EXISTS UpdateLikesCount
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name> Nastassia Zhuravel
-- Create date: <Create Date,,> 12.11.2024
-- Description:	<Description,,> Increments likes count for a certain car
-- =============================================

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

    INSERT INTO EventHistory (Id, CarId, EventDate, EventType)
    VALUES (NEWID(), @CarId, GETDATE(), 'Like');
END;

GO