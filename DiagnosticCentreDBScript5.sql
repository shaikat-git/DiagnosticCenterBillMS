USE [DiagnosticCentreDB]
GO
/****** Object:  Database [DiagnosticCentreDB]    Script Date: 11/05/2016 4:58:57 PM ******/
--CREATE DATABASE [DiagnosticCentreDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DiagnosticCentreDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DiagnosticCentreDB.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DiagnosticCentreDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DiagnosticCentreDB_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DiagnosticCentreDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiagnosticCentreDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiagnosticCentreDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DiagnosticCentreDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiagnosticCentreDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiagnosticCentreDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DiagnosticCentreDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiagnosticCentreDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DiagnosticCentreDB] SET  MULTI_USER 
GO
ALTER DATABASE [DiagnosticCentreDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiagnosticCentreDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiagnosticCentreDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiagnosticCentreDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DiagnosticCentreDB', N'ON'
GO
USE [DiagnosticCentreDB]
GO
/****** Object:  User [test2]    Script Date: 11/05/2016 4:58:57 PM ******/
CREATE USER [test2] FOR LOGIN [test2] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [test2]
GO
/****** Object:  StoredProcedure [dbo].[PaymentSP]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PaymentSP]
	@FromDate Date,
	@ToDate Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SUM(Test.Fee) AS Amount,Request.DueDate, Request.BillNo,Request.MobileNo FROM RequestDetails
INNER JOIN Request
ON Request.Id=RequestDetails.RequestId
INNER JOIN Test
ON RequestDetails.TestId=Test.Id
WHERE Request.DueDate BETWEEN @FromDate and @ToDate
GROUP BY Request.DueDate,Request.BillNo,Request.MobileNo

END



GO
/****** Object:  StoredProcedure [dbo].[TestRequestSP]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TestRequestSP]
@BillNo nvarchar(15),
@MobileNo nvarchar(11)
AS
BEGIN
SET NOCOUNT ON;
SELECT r.Id RequestId, r.PatientName, r.Dob, r.MobileNo, r.BillNo, r.DueDate, r.PaymentFlag, t.Fee, t.TestName
FROM dbo.Request AS r INNER JOIN
 dbo.RequestDetails AS rd ON r.Id = rd.RequestId INNER JOIN
 dbo.Test AS t ON rd.TestId = t.Id
WHERE (r.BillNo = @BillNo) OR (r.MobileNo = @MobileNo)
END



GO
/****** Object:  StoredProcedure [dbo].[TestWiseReportSP]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TestWiseReportSP]
	-- Add the parameters for the stored procedure here
	@FromDate date , 
	@ToDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
SELECT Test.Id,Test.TestName, IsNull(count(Test.TestName),0) AS [TotalTest], Sum(Test.Fee) AS [TotalAmount] FROM RequestDetails
INNER JOIN Request
ON Request.Id=RequestDetails.RequestId
INNER JOIN Test
ON RequestDetails.TestId=Test.Id
WHERE Request.DueDate BETWEEN @FromDate and @ToDate 
GROUP BY Test.Id,Test.TestName
END



GO
/****** Object:  StoredProcedure [dbo].[TypeWiseReportSP]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TypeWiseReportSP]
@FromDate date , 
@ToDate date
AS
BEGIN
SET NOCOUNT ON;

--SELECT [Type].TypeName AS [TestTypeName], count([Type].TypeName) AS [TotalNoOfTest], Sum(Test.Fee) AS [TotalAmount] FROM RequestDetails
--INNER JOIN Request
--ON Request.Id=RequestDetails.RequestId
--INNER JOIN Test
--ON RequestDetails.TestId=Test.Id 
--INNER JOIN [Type]
--ON [Type].Id=Test.Id
--WHERE Request.DueDate BETWEEN @FromDate and @ToDate 
--GROUP BY [Type].TypeName

SELECT        ty.TypeName AS TestTypeName, COUNT(rd.TestId) AS TotalNoOfTest, SUM(t.Fee) AS TotalAmount
FROM            dbo.Type AS ty RIGHT OUTER JOIN
                         dbo.Test AS t ON ty.Id = t.TypeId RIGHT OUTER JOIN
                         dbo.RequestDetails AS rd ON t.Id = rd.TestId RIGHT OUTER JOIN
                         dbo.Request AS r ON rd.RequestId = r.Id
WHERE r.DueDate BETWEEN @FromDate and @ToDate
GROUP BY ty.TypeName
END



GO
/****** Object:  StoredProcedure [dbo].[UnpaidBillReportSP]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UnpaidBillReportSP]
	@FromDate date , 
	@ToDate date
AS
BEGIN
	SET NOCOUNT ON;

SELECT Request.BillNo [BillNumber],Request.MobileNo [ContactNo], Request.PatientName [PatientName] , SUM(Test.Fee) [BillAmount] FROM RequestDetails
INNER JOIN Request
ON Request.Id=RequestDetails.RequestId
INNER JOIN Test
ON RequestDetails.TestId=Test.Id
where Request.PaymentFlag=0 and Request.DueDate BETWEEN @FromDate and @ToDate 
GROUP BY Request.PatientName ,Request.BillNo,Request.MobileNo



END



GO
/****** Object:  UserDefinedFunction [dbo].[NEW_BillNo]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[NEW_BillNo]
(

)
RETURNS nvarchar(15)
AS
BEGIN
--RETURN (SELECT 'D'+ convert(varchar(15),(ISNULL(MAX(Id),0) + 100)) FROM [Request])
RETURN 'D' + (SELECT convert(varchar(15),(ISNULL(MAX(Id),0) + 100)) AS MaxID FROM [Request])
--RETURN (SELECT CONCAT('D', (ISNULL(MAX(Id),0) + 100)) FROM [Request])
END


GO
/****** Object:  Table [dbo].[Request]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](100) NULL,
	[Dob] [date] NULL,
	[MobileNo] [nvarchar](11) NOT NULL,
	[BillNo] [nvarchar](15) NOT NULL,
	[DueDate] [date] NOT NULL,
	[PaymentFlag] [bit] NOT NULL,
 CONSTRAINT [PK_Request_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestDetails]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
 CONSTRAINT [PK_RequestDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Test]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestName] [nvarchar](100) NOT NULL,
	[Fee] [money] NOT NULL,
	[TypeId] [int] NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Type_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[PaymentView]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PaymentView]

AS

SELECT SUM(Test.Fee)AS AMOUNT,Request.DueDate, Request.BillNo,Request.MobileNo FROM RequestDetails
INNER JOIN Request
ON Request.Id=RequestDetails.RequestId
INNER JOIN Test
ON RequestDetails.TestId=Test.Id
GROUP BY Request.DueDate,Request.BillNo,Request.MobileNo



GO
/****** Object:  View [dbo].[TestView]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TestView]
AS
SELECT        dbo.Test.Id AS TestId, dbo.Test.TestName, dbo.Test.Fee AS TestFee, dbo.Type.TypeName
FROM            dbo.Test INNER JOIN
                         dbo.Type ON dbo.Test.TypeId = dbo.Type.Id



GO
/****** Object:  View [dbo].[TestWiseView]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE VIEW PaymentView

--AS

--SELECT SUM(Test.Fee)AS AMOUNT,Request.DueDate, Request.BillNo,Request.MobileNo FROM RequestDetails
--INNER JOIN Request
--ON Request.Id=RequestDetails.RequestId
--INNER JOIN Test
--ON RequestDetails.TestId=Test.Id
--GROUP BY Request.DueDate,Request.BillNo,Request.MobileNo


CREATE VIEW [dbo].[TestWiseView]	
as

SELECT Test.Id,Test.TestName, count(Test.TestName) AS [Total Test], Sum(Test.Fee) AS [Total Amount] FROM RequestDetails
INNER JOIN Request
ON Request.Id=RequestDetails.RequestId
INNER JOIN Test
ON RequestDetails.TestId=Test.Id
GROUP BY Test.Id,Test.TestName



GO
/****** Object:  View [dbo].[TypeWiseView]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TypeWiseView]
AS
SELECT        ty.TypeName AS TestTypeName, COUNT(rd.TestId) AS TotalNoOfTest, SUM(t.Fee) AS TotalAmount
FROM            dbo.Type AS ty RIGHT OUTER JOIN
                         dbo.Test AS t ON ty.Id = t.TypeId RIGHT OUTER JOIN
                         dbo.RequestDetails AS rd ON t.Id = rd.TestId RIGHT OUTER JOIN
                         dbo.Request AS r ON rd.RequestId = r.Id
GROUP BY ty.TypeName

GO
/****** Object:  View [dbo].[UnpaidBillView]    Script Date: 11/05/2016 4:58:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[UnpaidBillView]
as

SELECT Request.BillNo [Bill Number],Request.MobileNo [Contact No], Request.PatientName [Patient Name] , SUM(Test.Fee) [Bill Amount] FROM RequestDetails
INNER JOIN Request
ON Request.Id=RequestDetails.RequestId
INNER JOIN Test
ON RequestDetails.TestId=Test.Id
where Request.PaymentFlag=0
GROUP BY Request.PatientName ,Request.BillNo,Request.MobileNo



GO
SET IDENTITY_INSERT [dbo].[Request] ON 

INSERT [dbo].[Request] ([Id], [PatientName], [Dob], [MobileNo], [BillNo], [DueDate], [PaymentFlag]) VALUES (38, N'Mohshin', CAST(0x293C0B00 AS Date), N'12345678901', N'D100', CAST(0x103C0B00 AS Date), 1)
INSERT [dbo].[Request] ([Id], [PatientName], [Dob], [MobileNo], [BillNo], [DueDate], [PaymentFlag]) VALUES (39, N'Mohshin', CAST(0x283C0B00 AS Date), N'12345678902', N'D138', CAST(0x103C0B00 AS Date), 0)
INSERT [dbo].[Request] ([Id], [PatientName], [Dob], [MobileNo], [BillNo], [DueDate], [PaymentFlag]) VALUES (40, N'Mohshin', CAST(0x103C0B00 AS Date), N'12222222222', N'D139', CAST(0x103C0B00 AS Date), 0)
INSERT [dbo].[Request] ([Id], [PatientName], [Dob], [MobileNo], [BillNo], [DueDate], [PaymentFlag]) VALUES (41, N'Nasir', CAST(0x8C2D0B00 AS Date), N'12345678904', N'D140', CAST(0x103C0B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[Request] OFF
SET IDENTITY_INSERT [dbo].[RequestDetails] ON 

INSERT [dbo].[RequestDetails] ([Id], [RequestId], [TestId]) VALUES (68, 38, 15)
INSERT [dbo].[RequestDetails] ([Id], [RequestId], [TestId]) VALUES (69, 39, 15)
INSERT [dbo].[RequestDetails] ([Id], [RequestId], [TestId]) VALUES (70, 39, 10)
INSERT [dbo].[RequestDetails] ([Id], [RequestId], [TestId]) VALUES (71, 40, 16)
INSERT [dbo].[RequestDetails] ([Id], [RequestId], [TestId]) VALUES (72, 41, 5)
INSERT [dbo].[RequestDetails] ([Id], [RequestId], [TestId]) VALUES (73, 41, 10)
SET IDENTITY_INSERT [dbo].[RequestDetails] OFF
SET IDENTITY_INSERT [dbo].[Test] ON 

INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (5, N'Complete Blood count (Total Count-Different Count-ESR, Hb %)', 400.0000, 1)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (6, N'RBS', 150.0000, 1)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (7, N'S. Creatinine', 350.0000, 1)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (8, N'Lipid profile', 450.0000, 1)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (9, N'Hand X-Ray', 200.0000, 2)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (10, N'Feet X-Ray', 300.0000, 2)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (11, N'LS Spine', 1100.0000, 2)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (12, N'Lower Abdomen', 550.0000, 3)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (13, N'Whole Abdomen', 800.0000, 3)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (14, N'Pregnancy Profile', 550.0000, 3)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (15, N'ECG', 150.0000, 4)
INSERT [dbo].[Test] ([Id], [TestName], [Fee], [TypeId]) VALUES (16, N'Echo', 1000.0000, 5)
SET IDENTITY_INSERT [dbo].[Test] OFF
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([Id], [TypeName]) VALUES (1, N'Blood')
INSERT [dbo].[Type] ([Id], [TypeName]) VALUES (4, N'ECG')
INSERT [dbo].[Type] ([Id], [TypeName]) VALUES (5, N'Echo')
INSERT [dbo].[Type] ([Id], [TypeName]) VALUES (3, N'USG')
INSERT [dbo].[Type] ([Id], [TypeName]) VALUES (2, N'X-Ray')
SET IDENTITY_INSERT [dbo].[Type] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Request_BillNo]    Script Date: 11/05/2016 4:58:57 PM ******/
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [UK_Request_BillNo] UNIQUE NONCLUSTERED 
(
	[BillNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Request_MobileNo]    Script Date: 11/05/2016 4:58:57 PM ******/
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [UK_Request_MobileNo] UNIQUE NONCLUSTERED 
(
	[MobileNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Test]    Script Date: 11/05/2016 4:58:57 PM ******/
ALTER TABLE [dbo].[Test] ADD  CONSTRAINT [IX_Test] UNIQUE NONCLUSTERED 
(
	[TestName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Type_TypeName]    Script Date: 11/05/2016 4:58:57 PM ******/
ALTER TABLE [dbo].[Type] ADD  CONSTRAINT [UK_Type_TypeName] UNIQUE NONCLUSTERED 
(
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_BillNo]  DEFAULT ([dbo].[NEW_BillNo]()) FOR [BillNo]
GO
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_DueDate]  DEFAULT (getdate()) FOR [DueDate]
GO
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_PaymentFlag]  DEFAULT ((0)) FOR [PaymentFlag]
GO
ALTER TABLE [dbo].[RequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_RequestDetails_Request] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Request] ([Id])
GO
ALTER TABLE [dbo].[RequestDetails] CHECK CONSTRAINT [FK_RequestDetails_Request]
GO
ALTER TABLE [dbo].[RequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_RequestDetails_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[RequestDetails] CHECK CONSTRAINT [FK_RequestDetails_Test]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Type] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Type] ([Id])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Type]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Test"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Type"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 101
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "rd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 118
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 135
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 120
               Left = 38
               Bottom = 249
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ty"
            Begin Extent = 
               Top = 138
               Left = 262
               Bottom = 233
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TypeWiseView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TypeWiseView'
GO
USE [master]
GO
ALTER DATABASE [DiagnosticCentreDB] SET  READ_WRITE 
GO
