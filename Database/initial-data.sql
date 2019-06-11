/****** Object:  Table [dbo].[IdentityRoles]    Script Date: 01/28/2016 15:18:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

SET IDENTITY_INSERT [dbo].[Grupo] ON
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [Descripcion], [CreadoEn], [ActualizadoEn], CreadoPor, [ActualizadoPor]) VALUES (1, N'users', N'Usuarios', CAST(0x0000A54600B37EA5 AS DateTime), CAST(0x0000A54600B37EA5 AS DateTime), N'admin', N'admin')
INSERT [dbo].[Grupo] ([IdGrupo], Nombre, [Descripcion], [CreadoEn], [ActualizadoEn], [CreadoPor], [ActualizadoPor]) VALUES (2, N'operators', N'Operadores', CAST(0x0000A54600B384B4 AS DateTime), CAST(0x0000A54600B384B4 AS DateTime), N'admin', N'admin')
INSERT [dbo].[Grupo] ([IdGrupo], Nombre, [Descripcion], [CreadoEn], [ActualizadoEn], [CreadoPor], [ActualizadoPor]) VALUES (3, N'admins', N'Administradores', CAST(0x0000A55200FEFD15 AS DateTime), CAST(0x0000A55200FEFD15 AS DateTime), N'admin', N'admin')
SET IDENTITY_INSERT [dbo].[Grupo] OFF
GO
SET IDENTITY_INSERT [dbo].[Projects] ON
INSERT [dbo].[Projects] ([IdProject], [Name], [Description], [Identifier], [IsPrivate], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdateBy]) VALUES (1, N'Sistemas', N'Proyecto de Sistemas', N'sistemas', 0, CAST(0x0000A57D00C5BF52 AS DateTime), N'admin', CAST(0x0000A57D00C5BF52 AS DateTime), N'admin')
INSERT [dbo].[Projects] ([IdProject], [Name], [Description], [Identifier], [IsPrivate], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdateBy]) VALUES (2, N'Reclamos', N'Proyecto de reclamos', N'reclamos', 0, CAST(0x0000A58301129F20 AS DateTime), N'admin', CAST(0x0000A58301129F20 AS DateTime), N'admin')
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON
INSERT [dbo].[Settings] ([IdSetting], [Name], [OptionName], [Value], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (1, N'email_sender', N'tickets', N'outlanka@kfc.com.ec', N'admin', N'admin', CAST(0x0000A55C00B56968 AS DateTime), CAST(0x0000A55C00B56968 AS DateTime))
INSERT [dbo].[Settings] ([IdSetting], [Name], [OptionName], [Value], [CreatedBy], [UpdatedBy], [CreatedOn], [UpdatedOn]) VALUES (2, N'session_timeout', N'global', N'45', N'admin', N'admin', CAST(0x0000A56300BE45B5 AS DateTime), CAST(0x0000A56300BE45B5 AS DateTime))
SET IDENTITY_INSERT [dbo].[Settings] OFF

GO

SET IDENTITY_INSERT [dbo].[Usuario] ON
INSERT [dbo].[Usuario] ([IdUsuario], [NombreUsuario], [NombreMostrar], [Email], [LocalPassword], [EstaEnLinea], [EsLdap], [EstaBloqueado], [CreadoEn], [ActualizadoEn], [CuentaAccesosFallidos], [UltimaActividadEn], [UltimoLoginEn]) VALUES (1, N'admin', N'Administrador General', N'admin@email.com', N'7knXhUC55S9mC/kUao+q+A==', 0, 0, 0, CAST(0x0000A54600B3A2B8 AS DateTime), CAST(0x0000A54600B3A2B8 AS DateTime), 0, CAST(0x0000A59B00E1E6DC AS DateTime), CAST(0x0000A59B00E1C990 AS DateTime))
SET IDENTITY_INSERT [dbo].[Usuario] OFF

SET IDENTITY_INSERT [dbo].[PerfilUsuario] ON 
INSERT [dbo].[PerfilUsuario] ([IdPerfilUsuario], [Nombre], [Apellido], [Direccion], [TElefono], [IdUsuario]) VALUES (1, N'Administrador', N'General', N'', N'00000000', 1)
SET IDENTITY_INSERT [dbo].[PerfilUsuario] OFF

SET IDENTITY_INSERT [dbo].[ProjectMembers] ON
INSERT [dbo].[ProjectMembers] ([IdProjectMember], [CreatedBy], [CreatedOn], [IdUsuario], [IdProject], [IdGrupo]) VALUES (31, N'admin', CAST(0x0000A57D00C8E1C3 AS DateTime), 1, 1, 3)
SET IDENTITY_INSERT [dbo].[ProjectMembers] OFF


INSERT [dbo].[GruposUsuario] ([IdGrupo], [IdUsuario]) VALUES (3, 1)
