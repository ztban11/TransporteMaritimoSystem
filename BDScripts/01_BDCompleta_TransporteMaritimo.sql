USE [TransporteMaritimoDB]
GO
/****** Object:  Table [dbo].[AsignacionesTripulacion]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AsignacionesTripulacion](
	[AsignacionId] [int] IDENTITY(1,1) NOT NULL,
	[PersonalId] [int] NULL,
	[BarcoId] [int] NULL,
	[Cargo] [nvarchar](100) NULL,
	[FechaInicio] [datetime] NULL,
	[FechaFin] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AsignacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Barcos]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Barcos](
	[BarcoId] [int] IDENTITY(1,1) NOT NULL,
	[NombreBarco] [nvarchar](100) NULL,
	[Tipo] [nvarchar](100) NULL,
	[Matricula] [nvarchar](50) NOT NULL,
	[CapacidadCarga] [decimal](10, 2) NOT NULL,
	[PuertoBase] [nvarchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BarcoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Barcos_Matricula] UNIQUE NONCLUSTERED 
(
	[Matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialCambiosRol]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialCambiosRol](
	[CambioRolId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[RolAnteriorId] [int] NULL,
	[RolNuevoId] [int] NOT NULL,
	[ModificadoPorUsuarioId] [int] NOT NULL,
	[FechaCambio] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CambioRolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Licencias]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licencias](
	[LicenciaId] [int] IDENTITY(1,1) NOT NULL,
	[PersonalId] [int] NOT NULL,
	[TipoLicencia] [nvarchar](100) NULL,
	[FechaExpiracion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LicenciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motores]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motores](
	[MotorId] [int] IDENTITY(1,1) NOT NULL,
	[BarcoId] [int] NOT NULL,
	[Modelo] [nvarchar](100) NOT NULL,
	[PotenciaHP] [int] NOT NULL,
	[HorasUso] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MotorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdenesServicio]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdenesServicio](
	[OrdenId] [int] IDENTITY(1,1) NOT NULL,
	[BarcoId] [int] NOT NULL,
	[TipoMantenimiento] [nvarchar](50) NOT NULL,
	[Prioridad] [nvarchar](20) NOT NULL,
	[Descripcion] [nvarchar](500) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaLimite] [datetime] NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
	[InformeCierre] [nvarchar](max) NULL,
	[FechaCierreReal] [datetime] NULL,
	[UsuarioCierre] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrdenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdenServicioPersonal]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdenServicioPersonal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrdenId] [int] NOT NULL,
	[PersonalId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permisos]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permisos](
	[PermisoId] [int] IDENTITY(1,1) NOT NULL,
	[NombrePermiso] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[PermisoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personal](
	[PersonalId] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [nvarchar](200) NOT NULL,
	[Identificacion] [nvarchar](50) NOT NULL,
	[RolPrimario] [nvarchar](50) NOT NULL,
	[FechaContratacion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RolId] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolPermisos]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolPermisos](
	[RolPermisoId] [int] IDENTITY(1,1) NOT NULL,
	[RolId] [int] NOT NULL,
	[PermisoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RolPermisoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TravesiaPersonal]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TravesiaPersonal](
	[TravesiaPersonalId] [int] IDENTITY(1,1) NOT NULL,
	[TravesiaId] [int] NOT NULL,
	[PersonalId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TravesiaPersonalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Travesias]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Travesias](
	[TravesiaId] [int] IDENTITY(1,1) NOT NULL,
	[BarcoId] [int] NOT NULL,
	[PuertoOrigen] [nvarchar](100) NOT NULL,
	[PuertoDestino] [nvarchar](100) NOT NULL,
	[FechaSalidaPrevista] [datetime2](7) NOT NULL,
	[FechaLlegadaPrevista] [datetime2](7) NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
	[FechaCierreReal] [datetime2](7) NULL,
	[UsuarioCierre] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[TravesiaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioRoles]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioRoles](
	[UsuarioRolId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[RolId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioRolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 20/04/2026 23:28:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[Activo] [bit] NOT NULL,
	[IntentosFallidos] [int] NULL,
	[BloqueadoHasta] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Barcos] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[HistorialCambiosRol] ADD  DEFAULT (getdate()) FOR [FechaCambio]
GO
ALTER TABLE [dbo].[OrdenesServicio] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[OrdenesServicio] ADD  DEFAULT ('Abierta') FOR [Estado]
GO
ALTER TABLE [dbo].[Personal] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [IntentosFallidos]
GO
ALTER TABLE [dbo].[AsignacionesTripulacion]  WITH CHECK ADD FOREIGN KEY([BarcoId])
REFERENCES [dbo].[Barcos] ([BarcoId])
GO
ALTER TABLE [dbo].[AsignacionesTripulacion]  WITH CHECK ADD FOREIGN KEY([PersonalId])
REFERENCES [dbo].[Personal] ([PersonalId])
GO
ALTER TABLE [dbo].[HistorialCambiosRol]  WITH CHECK ADD FOREIGN KEY([ModificadoPorUsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[HistorialCambiosRol]  WITH CHECK ADD FOREIGN KEY([RolAnteriorId])
REFERENCES [dbo].[Roles] ([RolId])
GO
ALTER TABLE [dbo].[HistorialCambiosRol]  WITH CHECK ADD FOREIGN KEY([RolNuevoId])
REFERENCES [dbo].[Roles] ([RolId])
GO
ALTER TABLE [dbo].[HistorialCambiosRol]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Licencias]  WITH CHECK ADD FOREIGN KEY([PersonalId])
REFERENCES [dbo].[Personal] ([PersonalId])
GO
ALTER TABLE [dbo].[Motores]  WITH CHECK ADD  CONSTRAINT [FK_Motores_Barcos] FOREIGN KEY([BarcoId])
REFERENCES [dbo].[Barcos] ([BarcoId])
GO
ALTER TABLE [dbo].[Motores] CHECK CONSTRAINT [FK_Motores_Barcos]
GO
ALTER TABLE [dbo].[OrdenesServicio]  WITH CHECK ADD FOREIGN KEY([BarcoId])
REFERENCES [dbo].[Barcos] ([BarcoId])
GO
ALTER TABLE [dbo].[OrdenServicioPersonal]  WITH CHECK ADD FOREIGN KEY([OrdenId])
REFERENCES [dbo].[OrdenesServicio] ([OrdenId])
GO
ALTER TABLE [dbo].[OrdenServicioPersonal]  WITH CHECK ADD FOREIGN KEY([PersonalId])
REFERENCES [dbo].[Personal] ([PersonalId])
GO
ALTER TABLE [dbo].[RolPermisos]  WITH CHECK ADD FOREIGN KEY([PermisoId])
REFERENCES [dbo].[Permisos] ([PermisoId])
GO
ALTER TABLE [dbo].[RolPermisos]  WITH CHECK ADD FOREIGN KEY([RolId])
REFERENCES [dbo].[Roles] ([RolId])
GO
ALTER TABLE [dbo].[TravesiaPersonal]  WITH CHECK ADD FOREIGN KEY([PersonalId])
REFERENCES [dbo].[Personal] ([PersonalId])
GO
ALTER TABLE [dbo].[TravesiaPersonal]  WITH CHECK ADD FOREIGN KEY([TravesiaId])
REFERENCES [dbo].[Travesias] ([TravesiaId])
GO
ALTER TABLE [dbo].[Travesias]  WITH CHECK ADD FOREIGN KEY([BarcoId])
REFERENCES [dbo].[Barcos] ([BarcoId])
GO
ALTER TABLE [dbo].[UsuarioRoles]  WITH CHECK ADD FOREIGN KEY([RolId])
REFERENCES [dbo].[Roles] ([RolId])
GO
ALTER TABLE [dbo].[UsuarioRoles]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
