
/****** Object:  StoredProcedure [dbo].[GetFilteredCarsCount]    Script Date: 1/3/2025 10:55:51 PM ******/
DROP PROCEDURE GetFilteredCarsCount
GO

/****** Object:  StoredProcedure [dbo].[GetFilteredCarsCount]    Script Date: 1/3/2025 10:55:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE GetFilteredCarsCount
    @Make NVARCHAR(MAX) = NULL,
    @Model NVARCHAR(MAX) = NULL,
    @MinPrice INT = NULL,
    @MaxPrice INT = NULL,
    @MinYear INT = NULL,
    @MaxYear INT = NULL,
    @MinEngineVolume INT = NULL,
    @MaxEngineVolume INT = NULL,
    @MinMileage INT = NULL,
    @MaxMileage INT = NULL,
    @FuelType NVARCHAR(MAX) = NULL,
    @CarType NVARCHAR(MAX) = NULL,
    @Transmission NVARCHAR(MAX) = NULL,
    @Color NVARCHAR(MAX) = NULL,
    @DateFrom DATETIME = NULL,
    @DateTo DATETIME = NULL,
    @Status NVARCHAR(MAX) = NULL,
    @Count INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = '
    SELECT @Count = COUNT(*)
    FROM Cars
    WHERE 1=1';

    IF @Make IS NOT NULL
        SET @SQL = @SQL + ' AND Make IN (' + @Make + ')';
    IF @Model IS NOT NULL
        SET @SQL = @SQL + ' AND Model LIKE ''%' + @Model + '%''';
    IF @MinPrice IS NOT NULL
        SET @SQL = @SQL + ' AND Price >= ' + CAST(@MinPrice AS NVARCHAR);
    IF @MaxPrice IS NOT NULL
        SET @SQL = @SQL + ' AND Price <= ' + CAST(@MaxPrice AS NVARCHAR);
    IF @MinYear IS NOT NULL
        SET @SQL = @SQL + ' AND Year >= ' + CAST(@MinYear AS NVARCHAR);
    IF @MaxYear IS NOT NULL
        SET @SQL = @SQL + ' AND Year <= ' + CAST(@MaxYear AS NVARCHAR);
    IF @MinEngineVolume IS NOT NULL
        SET @SQL = @SQL + ' AND EngineVolume >= ' + CAST(@MinEngineVolume AS NVARCHAR);
    IF @MaxEngineVolume IS NOT NULL
        SET @SQL = @SQL + ' AND EngineVolume <= ' + CAST(@MaxEngineVolume AS NVARCHAR);
    IF @MinMileage IS NOT NULL
        SET @SQL = @SQL + ' AND Mileage >= ' + CAST(@MinMileage AS NVARCHAR);
    IF @MaxMileage IS NOT NULL
        SET @SQL = @SQL + ' AND Mileage <= ' + CAST(@MaxMileage AS NVARCHAR);
    IF @FuelType IS NOT NULL
        SET @SQL = @SQL + ' AND FuelType IN (' + @FuelType + ')';
    IF @CarType IS NOT NULL
        SET @SQL = @SQL + ' AND Type IN (' + @CarType + ')';
    IF @Transmission IS NOT NULL
        SET @SQL = @SQL + ' AND Transmission IN (' + @Transmission + ')';
    IF @Color IS NOT NULL
        SET @SQL = @SQL + ' AND Color IN (' + @Color + ')';
    IF @DateFrom IS NOT NULL
        SET @SQL = @SQL + ' AND TimeStamp >= ''' + CAST(@DateFrom AS NVARCHAR) + '''';
    IF @DateTo IS NOT NULL
        SET @SQL = @SQL + ' AND TimeStamp <= ''' + CAST(@DateTo AS NVARCHAR) + '''';
    IF @Status IS NOT NULL
        SET @SQL = @SQL + ' AND Status IN (' + @Status + ')';

    EXEC sp_executesql @SQL, N'@Count INT OUTPUT', @Count OUTPUT;
END

GO


