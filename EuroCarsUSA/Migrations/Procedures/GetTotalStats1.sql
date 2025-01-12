DROP PROCEDURE IF EXISTS GetTotalStats

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name> Nastassia Zhuravel
-- Create date: <Create Date,,> 02.01.2025
-- Description:	<Description,,> Returs total likes, views, custom orders and detail page forms
-- =============================================

CREATE PROCEDURE GetTotalStats
AS
BEGIN
    DECLARE @TotalLikes INT;
    DECLARE @TotalViews INT;
    DECLARE @TotalCustomOrders INT;
    DECLARE @TotalDetailPageForms INT;
    DECLARE @StartDate DATETIME = DATEADD(MONTH, -1, GETDATE());

    SELECT 
        @TotalLikes = SUM(Likes),
        @TotalViews = SUM(Views)
    FROM CarStatistics;

    SELECT 
        @TotalCustomOrders = COUNT(*)
    FROM CustomOrders;

    SELECT 
        @TotalDetailPageForms = COUNT(*)
    FROM DetailPageForms;

    SELECT 
        COUNT(*) AS LikesLastMonth
    FROM EventHistory
    WHERE EventType = 'Like' AND EventDate >= @StartDate;

    SELECT 
        COUNT(*) AS ViewsLastMonth
    FROM EventHistory
    WHERE EventType = 'View' AND EventDate >= @StartDate;

    SELECT 
        @TotalLikes AS TotalLikes,
        @TotalViews AS TotalViews,
        @TotalCustomOrders AS TotalCustomOrders,
        @TotalDetailPageForms AS TotalDetailPageForms,
        (SELECT COUNT(*) FROM EventHistory WHERE EventType = 'Like' AND EventDate >= @StartDate) AS LikesLastMonth,
        (SELECT COUNT(*) FROM EventHistory WHERE EventType = 'View' AND EventDate >= @StartDate) AS ViewsLastMonth;
END;
GO