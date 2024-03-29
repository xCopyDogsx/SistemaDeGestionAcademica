USE [master]
GO
/****** Object:  Database [sga]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE DATABASE [sga]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sga', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sga.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sga_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sga_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [sga] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sga].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sga] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sga] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sga] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sga] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sga] SET ARITHABORT OFF 
GO
ALTER DATABASE [sga] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sga] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sga] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sga] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sga] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sga] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sga] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sga] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sga] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sga] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sga] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sga] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sga] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sga] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sga] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sga] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sga] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sga] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [sga] SET  MULTI_USER 
GO
ALTER DATABASE [sga] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sga] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sga] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sga] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [sga] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [sga] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'sga', N'ON'
GO
ALTER DATABASE [sga] SET QUERY_STORE = OFF
GO
USE [sga]
GO
/****** Object:  Schema [sga]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE SCHEMA [sga]
GO
/****** Object:  Table [dbo].[docente_clase]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docente_clase](
	[Dccl_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Clas_ID] [bigint] NOT NULL,
	[Doc_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_docente_clase] PRIMARY KEY CLUSTERED 
(
	[Dccl_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materia_clase]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materia_clase](
	[Macl_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Clas_ID] [bigint] NOT NULL,
	[Mat_ID] [bigint] NOT NULL,
	[Doc_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_materia_clase] PRIMARY KEY CLUSTERED 
(
	[Macl_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[acudiente]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[acudiente](
	[Acu_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Acu_Doc] [nvarchar](50) NOT NULL,
	[Acu_Nom] [nvarchar](50) NOT NULL,
	[Acu_Apel] [varchar](200) NOT NULL,
	[Acu_Tel] [varchar](200) NOT NULL,
	[Acu_Email] [varchar](200) NOT NULL,
	[Acu_Passw] [varchar](200) NOT NULL,
	[Acu_FecReg] [date] NOT NULL,
	[Acu_Status] [int] NOT NULL,
 CONSTRAINT [PK_acudiente_Acu_ID] PRIMARY KEY CLUSTERED 
(
	[Acu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[administrador]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[administrador](
	[Adm_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Adm_Doc] [nvarchar](50) NOT NULL,
	[Adm_Nom] [nvarchar](50) NOT NULL,
	[Adm_Apel] [nvarchar](50) NOT NULL,
	[Adm_Tel] [nvarchar](50) NOT NULL,
	[Adm_Email] [nvarchar](100) NOT NULL,
	[Adm_Passw] [nvarchar](200) NOT NULL,
	[Adm_FecReg] [date] NOT NULL,
	[Adm_Status] [int] NOT NULL,
 CONSTRAINT [PK_administrador_Adm_ID] PRIMARY KEY CLUSTERED 
(
	[Adm_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[alumno]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[alumno](
	[Alum_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Alum_Doc] [nvarchar](15) NOT NULL,
	[Alum_Nom] [nvarchar](50) NOT NULL,
	[Alum_Apel] [nvarchar](50) NOT NULL,
	[Alum_Tel] [nvarchar](50) NOT NULL,
	[Alum_Email] [nvarchar](100) NOT NULL,
	[Alum_Passw] [nvarchar](200) NOT NULL,
	[Alum_FecReg] [date] NOT NULL,
	[Alum_Status] [int] NOT NULL,
	[Acu_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_alumno_Alum_ID] PRIMARY KEY CLUSTERED 
(
	[Alum_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[alumno_clase]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[alumno_clase](
	[Alcl_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Alum_ID] [bigint] NOT NULL,
	[Per_ID] [bigint] NOT NULL,
	[Clas_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_alumno_clase_Alcl_ID] PRIMARY KEY CLUSTERED 
(
	[Alcl_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[calificaciones]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[calificaciones](
	[Cal_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Cal_N1] [float] NOT NULL,
	[Cal_N2] [float] NOT NULL,
	[Cal_N3] [float] NOT NULL,
	[Cal_NF] [float] NOT NULL,
	[Mat_ID] [bigint] NOT NULL,
	[Alcl_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_calificaciones_Cal_ID] PRIMARY KEY CLUSTERED 
(
	[Cal_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[clase]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[clase](
	[Clas_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Clas_Capa] [int] NOT NULL,
	[Curs_ID] [bigint] NOT NULL,
	[Per_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_clase_Clas_ID] PRIMARY KEY CLUSTERED 
(
	[Clas_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[curso]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[curso](
	[Curs_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Curs_Nom] [varchar](150) NOT NULL,
 CONSTRAINT [PK_curso_1] PRIMARY KEY CLUSTERED 
(
	[Curs_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[dias]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[dias](
	[Dia_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Dia_Nom] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_dias_Dia_ID] PRIMARY KEY CLUSTERED 
(
	[Dia_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[docente]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[docente](
	[Doc_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Doc_Doc] [nvarchar](50) NOT NULL,
	[Doc_Nom] [nvarchar](50) NOT NULL,
	[Doc_Apel] [nvarchar](50) NOT NULL,
	[Doc_Tel] [nvarchar](50) NOT NULL,
	[Doc_Email] [nvarchar](100) NOT NULL,
	[Doc_Passw] [nvarchar](200) NOT NULL,
	[Doc_FecReg] [date] NOT NULL,
	[Doc_Status] [int] NOT NULL,
 CONSTRAINT [PK_docente_Doc_ID] PRIMARY KEY CLUSTERED 
(
	[Doc_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[horario]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[horario](
	[Hor_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Dia_ID] [bigint] NOT NULL,
	[Hor_Ini] [date] NOT NULL,
	[Hor_Fin] [date] NOT NULL,
	[Clas_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_horario_Hor_ID] PRIMARY KEY CLUSTERED 
(
	[Hor_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[materia]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[materia](
	[Mat_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Mat_Nom] [nvarchar](60) NOT NULL,
	[Mat_Desc] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_materia_Mat_ID] PRIMARY KEY CLUSTERED 
(
	[Mat_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [sga].[periodo]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[periodo](
	[Per_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Per_Nom] [nvarchar](50) NOT NULL,
	[Per_Ini] [date] NOT NULL,
	[Per_Fin] [date] NOT NULL,
 CONSTRAINT [PK_periodo_Per_ID] PRIMARY KEY CLUSTERED 
(
	[Per_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[docente_clase] ON 

INSERT [dbo].[docente_clase] ([Dccl_ID], [Clas_ID], [Doc_ID]) VALUES (5, 2, 2)
SET IDENTITY_INSERT [dbo].[docente_clase] OFF
GO
SET IDENTITY_INSERT [dbo].[materia_clase] ON 

INSERT [dbo].[materia_clase] ([Macl_ID], [Clas_ID], [Mat_ID], [Doc_ID]) VALUES (2, 2, 1, 2)
SET IDENTITY_INSERT [dbo].[materia_clase] OFF
GO
SET IDENTITY_INSERT [sga].[acudiente] ON 

INSERT [sga].[acudiente] ([Acu_ID], [Acu_Doc], [Acu_Nom], [Acu_Apel], [Acu_Tel], [Acu_Email], [Acu_Passw], [Acu_FecReg], [Acu_Status]) VALUES (1, N'7874545', N'Pedro ', N'Picapiedra', N'887254', N'pedropica@mail.com', N'0KGxPC1wsOW2xdllS7IqPwREFgF4SdmJ8+CSRutc+w3V9xDXiPYM3IQ7e1YVFidBkab8xFr4WS3geAJ9UfIU4Ka/TDZsXt4cqHB69bwM/Qc=', CAST(N'2001-07-21' AS Date), 1)
INSERT [sga].[acudiente] ([Acu_ID], [Acu_Doc], [Acu_Nom], [Acu_Apel], [Acu_Tel], [Acu_Email], [Acu_Passw], [Acu_FecReg], [Acu_Status]) VALUES (7, N'44554', N'Lucas', N'Alimaña', N'545454', N'alimana@mail.com', N'4414', CAST(N'2001-07-21' AS Date), 1)
INSERT [sga].[acudiente] ([Acu_ID], [Acu_Doc], [Acu_Nom], [Acu_Apel], [Acu_Tel], [Acu_Email], [Acu_Passw], [Acu_FecReg], [Acu_Status]) VALUES (8, N'548848646587', N'Carlos', N'Guevara', N'8777855', N'asd@mail.com', N'0KGxPC1wsOW2xdllS7IqPwREFgF4SdmJ8+CSRutc+w3V9xDXiPYM3IQ7e1YVFidBkab8xFr4WS3geAJ9UfIU4Ka/TDZsXt4cqHB69bwM/Qc=', CAST(N'0001-01-01' AS Date), 1)
SET IDENTITY_INSERT [sga].[acudiente] OFF
GO
SET IDENTITY_INSERT [sga].[administrador] ON 

INSERT [sga].[administrador] ([Adm_ID], [Adm_Doc], [Adm_Nom], [Adm_Apel], [Adm_Tel], [Adm_Email], [Adm_Passw], [Adm_FecReg], [Adm_Status]) VALUES (1, N'123456', N'Brian S', N'Cañon', N'478787', N'canonbrian123@gmail.com', N'0KGxPC1wsOW2xdllS7IqPwREFgF4SdmJ8+CSRutc+w3V9xDXiPYM3IQ7e1YVFidBkab8xFr4WS3geAJ9UfIU4Ka/TDZsXt4cqHB69bwM/Qc=', CAST(N'2001-07-21' AS Date), 1)
INSERT [sga].[administrador] ([Adm_ID], [Adm_Doc], [Adm_Nom], [Adm_Apel], [Adm_Tel], [Adm_Email], [Adm_Passw], [Adm_FecReg], [Adm_Status]) VALUES (4, N'848974', N'Prueba xd 2', N'Valor', N'4575', N'pruebadr@mail.com', N'0KGxPC1wsOW2xdllS7IqPwREFgF4SdmJ8+CSRutc+w3V9xDXiPYM3IQ7e1YVFidBkab8xFr4WS3geAJ9UfIU4Ka/TDZsXt4cqHB69bwM/Qc=', CAST(N'0001-01-01' AS Date), 1)
SET IDENTITY_INSERT [sga].[administrador] OFF
GO
SET IDENTITY_INSERT [sga].[alumno] ON 

INSERT [sga].[alumno] ([Alum_ID], [Alum_Doc], [Alum_Nom], [Alum_Apel], [Alum_Tel], [Alum_Email], [Alum_Passw], [Alum_FecReg], [Alum_Status], [Acu_ID]) VALUES (2, N'8998789', N'Juanito ', N'Gomez', N'8777855', N'juanito@mail.com', N'0KGxPC1wsOW2xdllS7IqPwREFgF4SdmJ8+CSRutc+w3V9xDXiPYM3IQ7e1YVFidBkab8xFr4WS3geAJ9UfIU4Ka/TDZsXt4cqHB69bwM/Qc=', CAST(N'0001-01-01' AS Date), 1, 1)
SET IDENTITY_INSERT [sga].[alumno] OFF
GO
SET IDENTITY_INSERT [sga].[alumno_clase] ON 

INSERT [sga].[alumno_clase] ([Alcl_ID], [Alum_ID], [Per_ID], [Clas_ID]) VALUES (7, 2, 2, 2)
SET IDENTITY_INSERT [sga].[alumno_clase] OFF
GO
SET IDENTITY_INSERT [sga].[clase] ON 

INSERT [sga].[clase] ([Clas_ID], [Clas_Capa], [Curs_ID], [Per_ID]) VALUES (2, 50, 2, 2)
INSERT [sga].[clase] ([Clas_ID], [Clas_Capa], [Curs_ID], [Per_ID]) VALUES (3, 60, 3, 2)
SET IDENTITY_INSERT [sga].[clase] OFF
GO
SET IDENTITY_INSERT [sga].[curso] ON 

INSERT [sga].[curso] ([Curs_ID], [Curs_Nom]) VALUES (2, N'Curso 302')
INSERT [sga].[curso] ([Curs_ID], [Curs_Nom]) VALUES (3, N'Curso 303')
SET IDENTITY_INSERT [sga].[curso] OFF
GO
SET IDENTITY_INSERT [sga].[docente] ON 

INSERT [sga].[docente] ([Doc_ID], [Doc_Doc], [Doc_Nom], [Doc_Apel], [Doc_Tel], [Doc_Email], [Doc_Passw], [Doc_FecReg], [Doc_Status]) VALUES (2, N'8998789', N'Carlos', N'Gomez', N'54545946', N'carlitos@mail.com', N'0KGxPC1wsOW2xdllS7IqPwREFgF4SdmJ8+CSRutc+w3V9xDXiPYM3IQ7e1YVFidBkab8xFr4WS3geAJ9UfIU4Ka/TDZsXt4cqHB69bwM/Qc=', CAST(N'0001-01-01' AS Date), 1)
SET IDENTITY_INSERT [sga].[docente] OFF
GO
SET IDENTITY_INSERT [sga].[materia] ON 

INSERT [sga].[materia] ([Mat_ID], [Mat_Nom], [Mat_Desc]) VALUES (1, N'Español', N'Lengua Castellana')
INSERT [sga].[materia] ([Mat_ID], [Mat_Nom], [Mat_Desc]) VALUES (4, N'Sociales', N'Sociales')
SET IDENTITY_INSERT [sga].[materia] OFF
GO
SET IDENTITY_INSERT [sga].[periodo] ON 

INSERT [sga].[periodo] ([Per_ID], [Per_Nom], [Per_Ini], [Per_Fin]) VALUES (2, N'Periodo 2021-1', CAST(N'2021-05-18' AS Date), CAST(N'2021-09-30' AS Date))
SET IDENTITY_INSERT [sga].[periodo] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [acudiente$Acu_Doc]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[acudiente] ADD  CONSTRAINT [acudiente$Acu_Doc] UNIQUE NONCLUSTERED 
(
	[Acu_Doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [acudiente$Acu_Email]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[acudiente] ADD  CONSTRAINT [acudiente$Acu_Email] UNIQUE NONCLUSTERED 
(
	[Acu_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [administrador$Adm_Doc]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[administrador] ADD  CONSTRAINT [administrador$Adm_Doc] UNIQUE NONCLUSTERED 
(
	[Adm_Doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [administrador$Adm_Email]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[administrador] ADD  CONSTRAINT [administrador$Adm_Email] UNIQUE NONCLUSTERED 
(
	[Adm_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [alumno$Alum_Doc]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[alumno] ADD  CONSTRAINT [alumno$Alum_Doc] UNIQUE NONCLUSTERED 
(
	[Alum_Doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [alumno$Alum_Email]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[alumno] ADD  CONSTRAINT [alumno$Alum_Email] UNIQUE NONCLUSTERED 
(
	[Alum_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Acu_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Acu_ID] ON [sga].[alumno]
(
	[Acu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Alum_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Alum_ID] ON [sga].[alumno_clase]
(
	[Alum_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Clas_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Clas_ID] ON [sga].[alumno_clase]
(
	[Clas_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Per_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Per_ID] ON [sga].[alumno_clase]
(
	[Per_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Curs_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Curs_ID] ON [sga].[clase]
(
	[Curs_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Per_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Per_ID] ON [sga].[clase]
(
	[Per_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [docente$Doc_Doc]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[docente] ADD  CONSTRAINT [docente$Doc_Doc] UNIQUE NONCLUSTERED 
(
	[Doc_Doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [docente$Doc_Email]    Script Date: 31/08/2021 11:31:09 a. m. ******/
ALTER TABLE [sga].[docente] ADD  CONSTRAINT [docente$Doc_Email] UNIQUE NONCLUSTERED 
(
	[Doc_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Clas_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Clas_ID] ON [sga].[horario]
(
	[Clas_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Dia_ID]    Script Date: 31/08/2021 11:31:09 a. m. ******/
CREATE NONCLUSTERED INDEX [Dia_ID] ON [sga].[horario]
(
	[Dia_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [sga].[acudiente] ADD  CONSTRAINT [DF__acudiente__Acu_F__398D8EEE]  DEFAULT (getdate()) FOR [Acu_FecReg]
GO
ALTER TABLE [sga].[administrador] ADD  DEFAULT (getdate()) FOR [Adm_FecReg]
GO
ALTER TABLE [sga].[alumno] ADD  DEFAULT (getdate()) FOR [Alum_FecReg]
GO
ALTER TABLE [sga].[docente] ADD  DEFAULT (getdate()) FOR [Doc_FecReg]
GO
ALTER TABLE [dbo].[docente_clase]  WITH CHECK ADD  CONSTRAINT [FK_docente_clase_clase] FOREIGN KEY([Clas_ID])
REFERENCES [sga].[clase] ([Clas_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[docente_clase] CHECK CONSTRAINT [FK_docente_clase_clase]
GO
ALTER TABLE [dbo].[docente_clase]  WITH CHECK ADD  CONSTRAINT [FK_docente_clase_docente] FOREIGN KEY([Doc_ID])
REFERENCES [sga].[docente] ([Doc_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[docente_clase] CHECK CONSTRAINT [FK_docente_clase_docente]
GO
ALTER TABLE [dbo].[materia_clase]  WITH CHECK ADD  CONSTRAINT [FK_materia_clase_clase] FOREIGN KEY([Clas_ID])
REFERENCES [sga].[clase] ([Clas_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[materia_clase] CHECK CONSTRAINT [FK_materia_clase_clase]
GO
ALTER TABLE [dbo].[materia_clase]  WITH CHECK ADD  CONSTRAINT [FK_materia_clase_docente] FOREIGN KEY([Doc_ID])
REFERENCES [sga].[docente] ([Doc_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[materia_clase] CHECK CONSTRAINT [FK_materia_clase_docente]
GO
ALTER TABLE [dbo].[materia_clase]  WITH CHECK ADD  CONSTRAINT [FK_materia_clase_materia] FOREIGN KEY([Mat_ID])
REFERENCES [sga].[materia] ([Mat_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[materia_clase] CHECK CONSTRAINT [FK_materia_clase_materia]
GO
ALTER TABLE [sga].[alumno]  WITH CHECK ADD  CONSTRAINT [alumno$alumno_ibfk_1] FOREIGN KEY([Acu_ID])
REFERENCES [sga].[acudiente] ([Acu_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[alumno] CHECK CONSTRAINT [alumno$alumno_ibfk_1]
GO
ALTER TABLE [sga].[alumno_clase]  WITH CHECK ADD  CONSTRAINT [alumno_clase$alumno_clase_ibfk_2] FOREIGN KEY([Alum_ID])
REFERENCES [sga].[alumno] ([Alum_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[alumno_clase] CHECK CONSTRAINT [alumno_clase$alumno_clase_ibfk_2]
GO
ALTER TABLE [sga].[alumno_clase]  WITH CHECK ADD  CONSTRAINT [alumno_clase$alumno_clase_ibfk_3] FOREIGN KEY([Per_ID])
REFERENCES [sga].[periodo] ([Per_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[alumno_clase] CHECK CONSTRAINT [alumno_clase$alumno_clase_ibfk_3]
GO
ALTER TABLE [sga].[alumno_clase]  WITH CHECK ADD  CONSTRAINT [alumno_clase$alumno_clase_ibfk_4] FOREIGN KEY([Clas_ID])
REFERENCES [sga].[clase] ([Clas_ID])
GO
ALTER TABLE [sga].[alumno_clase] CHECK CONSTRAINT [alumno_clase$alumno_clase_ibfk_4]
GO
ALTER TABLE [sga].[calificaciones]  WITH CHECK ADD  CONSTRAINT [FK_calificaciones_alumno_clase] FOREIGN KEY([Alcl_ID])
REFERENCES [sga].[alumno_clase] ([Alcl_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[calificaciones] CHECK CONSTRAINT [FK_calificaciones_alumno_clase]
GO
ALTER TABLE [sga].[calificaciones]  WITH CHECK ADD  CONSTRAINT [FK_calificaciones_materia] FOREIGN KEY([Mat_ID])
REFERENCES [sga].[materia] ([Mat_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[calificaciones] CHECK CONSTRAINT [FK_calificaciones_materia]
GO
ALTER TABLE [sga].[clase]  WITH CHECK ADD  CONSTRAINT [clase$clase_ibfk_1] FOREIGN KEY([Per_ID])
REFERENCES [sga].[periodo] ([Per_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[clase] CHECK CONSTRAINT [clase$clase_ibfk_1]
GO
ALTER TABLE [sga].[clase]  WITH CHECK ADD  CONSTRAINT [FK_clase_curso] FOREIGN KEY([Curs_ID])
REFERENCES [sga].[curso] ([Curs_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[clase] CHECK CONSTRAINT [FK_clase_curso]
GO
ALTER TABLE [sga].[horario]  WITH CHECK ADD  CONSTRAINT [horario$horario_ibfk_1] FOREIGN KEY([Clas_ID])
REFERENCES [sga].[clase] ([Clas_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[horario] CHECK CONSTRAINT [horario$horario_ibfk_1]
GO
ALTER TABLE [sga].[horario]  WITH CHECK ADD  CONSTRAINT [horario$horario_ibfk_2] FOREIGN KEY([Dia_ID])
REFERENCES [sga].[dias] ([Dia_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[horario] CHECK CONSTRAINT [horario$horario_ibfk_2]
GO
/****** Object:  StoredProcedure [dbo].[InsertarClase]    Script Date: 31/08/2021 11:31:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarClase]
@ClassCapa INT,
@Perid BIGINT
AS
BEGIN
DECLARE @Curs BIGINT
SELECT @Curs = IDENT_CURRENT('sga.curso');
INSERT INTO sga.clase (Clas_Capa,Curs_ID,Per_ID) values(@ClassCapa,@Curs,@Perid);
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.acudiente' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'acudiente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.administrador' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'administrador'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.alumno' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'alumno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.alumno_clase' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'alumno_clase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.calificaciones' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'calificaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.clase' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'clase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.dias' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'dias'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.docente' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'docente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.horario' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'horario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.materia' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'materia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'sga.periodo' , @level0type=N'SCHEMA',@level0name=N'sga', @level1type=N'TABLE',@level1name=N'periodo'
GO
USE [master]
GO
ALTER DATABASE [sga] SET  READ_WRITE 
GO
