DROP PROCEDURE GetFilteredCars
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetFilteredCars
    @Start INT,
    @Count INT,
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
    @SortOrder INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = '
    SELECT *
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

    SET @SQL = @SQL + ' ORDER BY ';

    IF @SortOrder = 1
        SET @SQL = @SQL + ' Year ASC';
    ELSE IF @SortOrder = 2
        SET @SQL = @SQL + ' Year DESC';
    ELSE IF @SortOrder = 3
        SET @SQL = @SQL + ' Mileage ASC';
    ELSE IF @SortOrder = 4
        SET @SQL = @SQL + ' Price ASC';
    ELSE IF @SortOrder = 5
        SET @SQL = @SQL + ' Price DESC';
    ELSE
        SET @SQL = @SQL + ' TimeStamp DESC';

    SET @SQL = @SQL + ' OFFSET ' + CAST(@Start AS NVARCHAR) + ' ROWS FETCH NEXT ' + CAST(@Count AS NVARCHAR) + ' ROWS ONLY';

    EXEC sp_executesql @SQL;
END
