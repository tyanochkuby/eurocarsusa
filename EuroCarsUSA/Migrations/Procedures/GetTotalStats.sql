CREATE or ALTER PROCEDURE GetTotalStats
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