 DECLARE @locDateFrom DATETIME,
		@locDateTo DATETIME,
		@locProjectName NVARCHAR(MAX)

SET @locDateFrom = DATEFROMPARTS(2020,12,1)
SET @locDateTo = DATEFROMPARTS(2020,12,16)
SET @locProjectName = '1386_LogMeIn'
		
DECLARE	@TFS2011 INT,
		@TFS2012 INT,
		@FSA INT,
		@Jotun INT

SET @TFS2011 = 2
SET @TFS2012 = 3
SET @FSA = 4
SET @Jotun = 5


	SELECT 
		[TimeReports].[TaskId] AS [Id], 
		[TimeReports].[Date] AS [Date], 
		CASE [TimeReports].[Type]
		WHEN 0 THEN [TimeReports].[Hours]
		ELSE 0
		END 
		AS [Time],
		case [TimeReports].[Type]
		when 1 then [TimeReports].[Hours]
		else 0
		end
		As [Overtime],
		(SELECT top(1) [Status] FROM [TaskStatuses]
		WHERE [TaskStatuses].ProjectId = [TimeReports].ProjectId
			  AND [TaskStatuses].[UserId] = [TimeReports].[UserId] 
			  AND [TaskStatuses].[TaskId] = [TimeReports].[TaskId] 
			  AND DATEDIFF(DAY, [TaskStatuses].StartDate, [TimeReports].[Date]) <= 6
			  AND DATEPART(isoww,[TaskStatuses].[StartDate]) = DATEPART(isoww,[TimeReports].[Date])
			  )
		AS [Status]
FROM TimeReports
		INNER JOIN [Project] ON [TimeReports].[projectId] = [Project].[id]
		INNER JOIN [ProjectCollection] ON [Project].projectCollectionId = [ProjectCollection].[id]
		INNER JOIN [User] ON [User].[UserId] = [TimeReports].[userId]
		
	WHERE [Date] >= @locDateFrom AND [Date] <= @locDateTo 
	--AND [ProjectCollection].[id] = @Jotun 
	AND (@locProjectName IS NULL OR [Project].name = @locProjectName)



Added
Approved
Declined
WaitingForApprove