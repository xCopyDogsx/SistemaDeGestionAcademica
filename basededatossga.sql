USE [master]
GO
/****** Object:  Database [sga]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE DATABASE [sga]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sga', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\sga.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sga_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\sga_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [sga] SET QUERY_STORE = OFF
GO
USE [sga]
GO
/****** Object:  Schema [sga]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE SCHEMA [sga]
GO
/****** Object:  Table [sga].[acudiente]    Script Date: 23/04/2021 3:59:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[acudiente](
	[Acu_ID] [bigint] NOT NULL,
	[Acu_Doc] [nvarchar](50) NOT NULL,
	[Acu_Nom] [nvarchar](50) NOT NULL,
	[Acu_Apel] [varchar](200) NULL,
	[Acu_Tel] [varchar](200) NULL,
	[Acu_Email] [varchar](200) NULL,
	[Acu_Passw] [varchar](200) NULL,
	[Acu_FecReg] [date] NOT NULL,
	[Acu_Status] [int] NOT NULL,
 CONSTRAINT [PK_acudiente_Acu_ID] PRIMARY KEY CLUSTERED 
(
	[Acu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[administrador]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
/****** Object:  Table [sga].[alumno]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [alumno$Alum_Doc] UNIQUE NONCLUSTERED 
(
	[Alum_Doc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [alumno$Alum_Email] UNIQUE NONCLUSTERED 
(
	[Alum_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[alumno_clase]    Script Date: 23/04/2021 3:59:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[alumno_clase](
	[Alcl_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Alum_ID] [bigint] NOT NULL,
	[Per_ID] [bigint] NOT NULL,
	[Clas_ID] [bigint] NOT NULL,
	[Cal_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_alumno_clase_Alcl_ID] PRIMARY KEY CLUSTERED 
(
	[Alcl_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[calificaciones]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
 CONSTRAINT [PK_calificaciones_Cal_ID] PRIMARY KEY CLUSTERED 
(
	[Cal_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[clase]    Script Date: 23/04/2021 3:59:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sga].[clase](
	[Clas_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Mat_ID] [bigint] NOT NULL,
	[Doc_ID] [bigint] NOT NULL,
	[Clas_Capa] [int] NOT NULL,
	[Per_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_clase_Clas_ID] PRIMARY KEY CLUSTERED 
(
	[Clas_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sga].[dias]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
/****** Object:  Table [sga].[docente]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
/****** Object:  Table [sga].[horario]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
/****** Object:  Table [sga].[materia]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
/****** Object:  Table [sga].[periodo]    Script Date: 23/04/2021 3:59:22 p. m. ******/
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
/****** Object:  Index [Acu_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Acu_ID] ON [sga].[alumno]
(
	[Acu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Alum_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Alum_ID] ON [sga].[alumno_clase]
(
	[Alum_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Cal_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Cal_ID] ON [sga].[alumno_clase]
(
	[Cal_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Clas_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Clas_ID] ON [sga].[alumno_clase]
(
	[Clas_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Per_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Per_ID] ON [sga].[alumno_clase]
(
	[Per_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Doc_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Doc_ID] ON [sga].[clase]
(
	[Doc_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Mat_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Mat_ID] ON [sga].[clase]
(
	[Mat_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Per_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Per_ID] ON [sga].[clase]
(
	[Per_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Clas_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Clas_ID] ON [sga].[horario]
(
	[Clas_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Dia_ID]    Script Date: 23/04/2021 3:59:22 p. m. ******/
CREATE NONCLUSTERED INDEX [Dia_ID] ON [sga].[horario]
(
	[Dia_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [sga].[acudiente] ADD  DEFAULT (getdate()) FOR [Acu_FecReg]
GO
ALTER TABLE [sga].[administrador] ADD  DEFAULT (getdate()) FOR [Adm_FecReg]
GO
ALTER TABLE [sga].[alumno] ADD  DEFAULT (getdate()) FOR [Alum_FecReg]
GO
ALTER TABLE [sga].[docente] ADD  DEFAULT (getdate()) FOR [Doc_FecReg]
GO
ALTER TABLE [sga].[alumno]  WITH CHECK ADD  CONSTRAINT [alumno$alumno_ibfk_1] FOREIGN KEY([Acu_ID])
REFERENCES [sga].[acudiente] ([Acu_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[alumno] CHECK CONSTRAINT [alumno$alumno_ibfk_1]
GO
ALTER TABLE [sga].[alumno_clase]  WITH CHECK ADD  CONSTRAINT [alumno_clase$alumno_clase_ibfk_1] FOREIGN KEY([Cal_ID])
REFERENCES [sga].[calificaciones] ([Cal_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[alumno_clase] CHECK CONSTRAINT [alumno_clase$alumno_clase_ibfk_1]
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
ALTER TABLE [sga].[clase]  WITH CHECK ADD  CONSTRAINT [clase$clase_ibfk_1] FOREIGN KEY([Per_ID])
REFERENCES [sga].[periodo] ([Per_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[clase] CHECK CONSTRAINT [clase$clase_ibfk_1]
GO
ALTER TABLE [sga].[clase]  WITH CHECK ADD  CONSTRAINT [clase$clase_ibfk_2] FOREIGN KEY([Mat_ID])
REFERENCES [sga].[materia] ([Mat_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[clase] CHECK CONSTRAINT [clase$clase_ibfk_2]
GO
ALTER TABLE [sga].[clase]  WITH CHECK ADD  CONSTRAINT [clase$clase_ibfk_3] FOREIGN KEY([Doc_ID])
REFERENCES [sga].[docente] ([Doc_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [sga].[clase] CHECK CONSTRAINT [clase$clase_ibfk_3]
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
