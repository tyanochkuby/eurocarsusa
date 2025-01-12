DROP PROCEDURE IF EXISTS UpdateViewsCount
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name> Nastassia Zhuravel
-- Create date: <Create Date,,> 12.11.2024
-- Description:	<Description,,> Increments views count for a certain car
-- =============================================

CREATE PROCEDURE UpdateViewsCount
    @CarId UNIQUEIDENTIFIER
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM CarStatistics WHERE CarId = @CarId)
    BEGIN
        INSERT INTO CarStatistics (Id, CarId, Likes, Views)
        VALUES (NEWID(), @CarId, 0, 0);
    END

    UPDATE CarStatistics
    SET Views = Views + 1
    WHERE CarId = @CarId;

    INSERT INTO EventHistory (Id, CarId, EventDate, EventType)
    VALUES (NEWID(), @CarId, GETDATE(), 'View');
END;

GO