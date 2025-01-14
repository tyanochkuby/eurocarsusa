DROP PROCEDURE IF EXISTS GetTotalStats

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name> Nastassia Zhuravel
-- Create date: <Create Date,,> 12.11.2024
-- Description:	<Description,,> Returs total likes, views, custom orders and detail page forms
-- =============================================

CREATE PROCEDURE GetTotalStats
AS
BEGIN
    DECLARE @TotalLikes INT;
    DECLARE @TotalViews INT;
    DECLARE @TotalCustomOrders INT;
    DECLARE @TotalDetailPageForms INT;

    SELECT 
        @TotalLikes = SUM(Likes),
        @TotalViews = SUM(Views)
    FROM CarStatistics;


    SELECT 
        @TotalCustomOrders = COUNT(*)
    FROM CustomOrders

    SELECT 
        @TotalDetailPageForms = COUNT(*)
    FROM DetailPageForms

    SELECT 
        @TotalLikes AS TotalLikes,
        @TotalViews AS TotalViews,
        @TotalCustomOrders AS TotalCustomOrders,
        @TotalDetailPageForms AS TotalDetailPageForms;
END;