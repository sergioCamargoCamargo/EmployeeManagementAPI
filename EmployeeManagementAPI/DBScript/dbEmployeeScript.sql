USE [DBEMPLOYEE]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 26/08/2024 8:26:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[HireDate] [datetime] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddEmployee]    Script Date: 26/08/2024 8:26:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Camargo
-- Create date: 26/08/2024
-- Description:	Stored procedure for employee creation
-- =============================================
CREATE PROCEDURE [dbo].[AddEmployee] 
	-- Add the parameters for the stored procedure here
	@FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @HireDate DATETIME
AS
BEGIN
	INSERT INTO Employees (FirstName, LastName, Email, PhoneNumber, HireDate)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @HireDate);

    SELECT SCOPE_IDENTITY() AS EmployeeId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 26/08/2024 8:26:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Camargo
-- Create date: 26/08/2024
-- Description:	Stored procedure to delete employee
-- =============================================
CREATE PROCEDURE [dbo].[DeleteEmployee] 
	-- Add the parameters for the stored procedure here
	@EmployeeId INT
AS
BEGIN
	 DELETE FROM Employees
    WHERE EmployeeId = @EmployeeId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployees]    Script Date: 26/08/2024 8:26:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Camargo
-- Create date: 26/08/2024
-- Description:	Stored procedure to get all employees
-- =============================================
CREATE PROCEDURE [dbo].[GetAllEmployees] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	SELECT EmployeeId, FirstName, LastName, Email, PhoneNumber, HireDate
    FROM Employees;
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeById]    Script Date: 26/08/2024 8:26:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Camargo
-- Create date: 26/08/2024
-- Description:	Stored procedure to get employee
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeeById] 
	-- Add the parameters for the stored procedure here
	@EmployeeId INT
AS
BEGIN
	SELECT EmployeeId, FirstName, LastName, Email, PhoneNumber, HireDate
    FROM Employees
    WHERE EmployeeId = @EmployeeId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeesHiredAfter]    Script Date: 26/08/2024 8:26:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Camargo
-- Create date: 26/08/2024
-- Description:	Stored procedure to get employees hired on a specific date 
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeesHiredAfter]
	-- Add the parameters for the stored procedure here
	@HireDate DATETIME
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM Employees WHERE HireDate > @HireDate;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 26/08/2024 8:26:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Camargo
-- Create date: 26/08/2024
-- Description:	Stored procedure to update employee
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEmployee] 
	-- Add the parameters for the stored procedure here
	@EmployeeId INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @HireDate DATETIME
AS
BEGIN
	UPDATE Employees
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        HireDate = @HireDate
    WHERE EmployeeId = @EmployeeId;
END
GO
