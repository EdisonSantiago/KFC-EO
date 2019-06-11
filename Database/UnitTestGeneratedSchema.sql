
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2F87092369F3AA74]') AND parent_object_id = OBJECT_ID('Cadena'))
alter table Cadena  drop constraint FK2F87092369F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC671050A69F3AA74]') AND parent_object_id = OBJECT_ID('Calificacion'))
alter table Calificacion  drop constraint FKC671050A69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9AE6767269F3AA74]') AND parent_object_id = OBJECT_ID('Categoria'))
alter table Categoria  drop constraint FK9AE6767269F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK33E1341569F3AA74]') AND parent_object_id = OBJECT_ID('Ciudad'))
alter table Ciudad  drop constraint FK33E1341569F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK33E134157EA60DF5]') AND parent_object_id = OBJECT_ID('Ciudad'))
alter table Ciudad  drop constraint FK33E134157EA60DF5


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDCB5519769F3AA74]') AND parent_object_id = OBJECT_ID('Clasificacion'))
alter table Clasificacion  drop constraint FKDCB5519769F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB0AB54ED69F3AA74]') AND parent_object_id = OBJECT_ID('Comentario'))
alter table Comentario  drop constraint FKB0AB54ED69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB0AB54ED3BFD0DDB]') AND parent_object_id = OBJECT_ID('Comentario'))
alter table Comentario  drop constraint FKB0AB54ED3BFD0DDB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK73A000A64B06DCA7]') AND parent_object_id = OBJECT_ID('EquipoLocal'))
alter table EquipoLocal  drop constraint FK73A000A64B06DCA7


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK73A000A61F4CBDC9]') AND parent_object_id = OBJECT_ID('EquipoLocal'))
alter table EquipoLocal  drop constraint FK73A000A61F4CBDC9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3239344269F3AA74]') AND parent_object_id = OBJECT_ID('Equipo'))
alter table Equipo  drop constraint FK3239344269F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK32393442B155B376]') AND parent_object_id = OBJECT_ID('Equipo'))
alter table Equipo  drop constraint FK32393442B155B376


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK323934421F4CBDC9]') AND parent_object_id = OBJECT_ID('Equipo'))
alter table Equipo  drop constraint FK323934421F4CBDC9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DE8B4DF0DB]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DE8B4DF0DB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DE69F3AA74]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DE69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DEDD5D4C78]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DEDD5D4C78


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DEDE24641F]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DEDE24641F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DE2C6C496B]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DE2C6C496B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DEDCFF6836]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DEDCFF6836


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DE9ABE0AB9]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DE9ABE0AB9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF73DD6DED4770A9C]') AND parent_object_id = OBJECT_ID('Estandar'))
alter table Estandar  drop constraint FKF73DD6DED4770A9C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1B1427669ABE0AB9]') AND parent_object_id = OBJECT_ID('EstandarSistema'))
alter table EstandarSistema  drop constraint FK1B1427669ABE0AB9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1B1427663BFD0DDB]') AND parent_object_id = OBJECT_ID('EstandarSistema'))
alter table EstandarSistema  drop constraint FK1B1427663BFD0DDB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBFC562F2D4770A9C]') AND parent_object_id = OBJECT_ID('TipoLocalEstandar'))
alter table TipoLocalEstandar  drop constraint FKBFC562F2D4770A9C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBFC562F23BFD0DDB]') AND parent_object_id = OBJECT_ID('TipoLocalEstandar'))
alter table TipoLocalEstandar  drop constraint FKBFC562F23BFD0DDB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC8F07AF69F3AA74]') AND parent_object_id = OBJECT_ID('Evaluacion'))
alter table Evaluacion  drop constraint FKC8F07AF69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC8F07AFE27188D5]') AND parent_object_id = OBJECT_ID('Evaluacion'))
alter table Evaluacion  drop constraint FKC8F07AFE27188D5


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC8F07AF1B0CA1B7]') AND parent_object_id = OBJECT_ID('Evaluacion'))
alter table Evaluacion  drop constraint FKC8F07AF1B0CA1B7


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC8F07AF1F4CBDC9]') AND parent_object_id = OBJECT_ID('Evaluacion'))
alter table Evaluacion  drop constraint FKC8F07AF1F4CBDC9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK399BC6BC69F3AA74]') AND parent_object_id = OBJECT_ID('GerenteGeneral'))
alter table GerenteGeneral  drop constraint FK399BC6BC69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK50BCC20E69F3AA74]') AND parent_object_id = OBJECT_ID('GerenteNacional'))
alter table GerenteNacional  drop constraint FK50BCC20E69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK50BCC20EBC1D5800]') AND parent_object_id = OBJECT_ID('GerenteNacional'))
alter table GerenteNacional  drop constraint FK50BCC20EBC1D5800


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK50BCC20EE0B8CE2C]') AND parent_object_id = OBJECT_ID('GerenteNacional'))
alter table GerenteNacional  drop constraint FK50BCC20EE0B8CE2C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8D44362D69F3AA74]') AND parent_object_id = OBJECT_ID('GerenteRegional'))
alter table GerenteRegional  drop constraint FK8D44362D69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8D44362DC3B7F36E]') AND parent_object_id = OBJECT_ID('GerenteRegional'))
alter table GerenteRegional  drop constraint FK8D44362DC3B7F36E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE8279AE669F3AA74]') AND parent_object_id = OBJECT_ID('GrupoEstandar'))
alter table GrupoEstandar  drop constraint FKE8279AE669F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK634A896825013C81]') AND parent_object_id = OBJECT_ID('GruposUsuario'))
alter table GruposUsuario  drop constraint FK634A896825013C81


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK634A896836334802]') AND parent_object_id = OBJECT_ID('GruposUsuario'))
alter table GruposUsuario  drop constraint FK634A896836334802


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK216A2AC69F3AA74]') AND parent_object_id = OBJECT_ID('ImagenEstandar'))
alter table ImagenEstandar  drop constraint FK216A2AC69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK216A2AC4F078D59]') AND parent_object_id = OBJECT_ID('ImagenEstandar'))
alter table ImagenEstandar  drop constraint FK216A2AC4F078D59


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1C87419169F3AA74]') AND parent_object_id = OBJECT_ID('ImagenEvaluacion'))
alter table ImagenEvaluacion  drop constraint FK1C87419169F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1C874191E491A45]') AND parent_object_id = OBJECT_ID('ImagenEvaluacion'))
alter table ImagenEvaluacion  drop constraint FK1C874191E491A45


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE2CF8A821F4CBDC9]') AND parent_object_id = OBJECT_ID('ImagenLocal'))
alter table ImagenLocal  drop constraint FKE2CF8A821F4CBDC9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC6AE60FC69F3AA74]') AND parent_object_id = OBJECT_ID('JefeArea'))
alter table JefeArea  drop constraint FKC6AE60FC69F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC6AE60FC38971873]') AND parent_object_id = OBJECT_ID('JefeArea'))
alter table JefeArea  drop constraint FKC6AE60FC38971873


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBA56958169F3AA74]') AND parent_object_id = OBJECT_ID('Local'))
alter table Local  drop constraint FKBA56958169F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBA569581D4770A9C]') AND parent_object_id = OBJECT_ID('Local'))
alter table Local  drop constraint FKBA569581D4770A9C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBA569581BC1D5800]') AND parent_object_id = OBJECT_ID('Local'))
alter table Local  drop constraint FKBA569581BC1D5800


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBA56958142AB4D7A]') AND parent_object_id = OBJECT_ID('Local'))
alter table Local  drop constraint FKBA56958142AB4D7A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBA56958169159F7A]') AND parent_object_id = OBJECT_ID('Local'))
alter table Local  drop constraint FKBA56958169159F7A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK4B07A90869F3AA74]') AND parent_object_id = OBJECT_ID('Nivel'))
alter table Nivel  drop constraint FK4B07A90869F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK173F950569F3AA74]') AND parent_object_id = OBJECT_ID('Opcion'))
alter table Opcion  drop constraint FK173F950569F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK173F9505EB81B60B]') AND parent_object_id = OBJECT_ID('Opcion'))
alter table Opcion  drop constraint FK173F9505EB81B60B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC4E76C6125013C81]') AND parent_object_id = OBJECT_ID('PerfilUsuario'))
alter table PerfilUsuario  drop constraint FKC4E76C6125013C81


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK4420169469F3AA74]') AND parent_object_id = OBJECT_ID('Persona'))
alter table Persona  drop constraint FK4420169469F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKFD95CA4769F3AA74]') AND parent_object_id = OBJECT_ID('Posicion'))
alter table Posicion  drop constraint FKFD95CA4769F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKFD95CA47BC1D5800]') AND parent_object_id = OBJECT_ID('Posicion'))
alter table Posicion  drop constraint FKFD95CA47BC1D5800


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7DAD617F25013C81]') AND parent_object_id = OBJECT_ID('ProjectMembers'))
alter table ProjectMembers  drop constraint FK7DAD617F25013C81


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7DAD617F557351F5]') AND parent_object_id = OBJECT_ID('ProjectMembers'))
alter table ProjectMembers  drop constraint FK7DAD617F557351F5


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7DAD617F36334802]') AND parent_object_id = OBJECT_ID('ProjectMembers'))
alter table ProjectMembers  drop constraint FK7DAD617F36334802


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2FCBC58469F3AA74]') AND parent_object_id = OBJECT_ID('Provincia'))
alter table Provincia  drop constraint FK2FCBC58469F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2FCBC584987692E6]') AND parent_object_id = OBJECT_ID('Provincia'))
alter table Provincia  drop constraint FK2FCBC584987692E6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9FB469A469F3AA74]') AND parent_object_id = OBJECT_ID('Region'))
alter table Region  drop constraint FK9FB469A469F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK42EDA5964F078D59]') AND parent_object_id = OBJECT_ID('RespuestaComentario'))
alter table RespuestaComentario  drop constraint FK42EDA5964F078D59


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3B204E273BF95419]') AND parent_object_id = OBJECT_ID('Respuesta'))
alter table Respuesta  drop constraint FK3B204E273BF95419


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3B204E273BFD0DDB]') AND parent_object_id = OBJECT_ID('Respuesta'))
alter table Respuesta  drop constraint FK3B204E273BFD0DDB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK55D60E0669F3AA74]') AND parent_object_id = OBJECT_ID('Sistema'))
alter table Sistema  drop constraint FK55D60E0669F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK698DC32169F3AA74]') AND parent_object_id = OBJECT_ID('TipoEquipo'))
alter table TipoEquipo  drop constraint FK698DC32169F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK57544FF6E27188D5]') AND parent_object_id = OBJECT_ID('TipoEvaluacionGrupoEstandar'))
alter table TipoEvaluacionGrupoEstandar  drop constraint FK57544FF6E27188D5


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK57544FF62C6C496B]') AND parent_object_id = OBJECT_ID('TipoEvaluacionGrupoEstandar'))
alter table TipoEvaluacionGrupoEstandar  drop constraint FK57544FF62C6C496B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKFCEDB9F869F3AA74]') AND parent_object_id = OBJECT_ID('TipoEvaluacion'))
alter table TipoEvaluacion  drop constraint FKFCEDB9F869F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1E4B502469F3AA74]') AND parent_object_id = OBJECT_ID('TipoLocal'))
alter table TipoLocal  drop constraint FK1E4B502469F3AA74


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7BDEBACE2500F343]') AND parent_object_id = OBJECT_ID('ValorAtributo'))
alter table ValorAtributo  drop constraint FK7BDEBACE2500F343


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA135DE33C211593]') AND parent_object_id = OBJECT_ID('RolesToUsers'))
alter table RolesToUsers  drop constraint FKA135DE33C211593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA135DE3DB7ED1C8]') AND parent_object_id = OBJECT_ID('RolesToUsers'))
alter table RolesToUsers  drop constraint FKA135DE3DB7ED1C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD3D2ED76D3252E64]') AND parent_object_id = OBJECT_ID('IdentityUserClaim'))
alter table IdentityUserClaim  drop constraint FKD3D2ED76D3252E64


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD3D2ED763C211593]') AND parent_object_id = OBJECT_ID('IdentityUserClaim'))
alter table IdentityUserClaim  drop constraint FKD3D2ED763C211593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA422A8223C211593]') AND parent_object_id = OBJECT_ID('IdentityUserLogin'))
alter table IdentityUserLogin  drop constraint FKA422A8223C211593


    if exists (select * from dbo.sysobjects where id = object_id(N'AplicacionCliente') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AplicacionCliente

    if exists (select * from dbo.sysobjects where id = object_id(N'Atributo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Atributo

    if exists (select * from dbo.sysobjects where id = object_id(N'Cadena') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Cadena

    if exists (select * from dbo.sysobjects where id = object_id(N'Calificacion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Calificacion

    if exists (select * from dbo.sysobjects where id = object_id(N'Categoria') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Categoria

    if exists (select * from dbo.sysobjects where id = object_id(N'Ciudad') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Ciudad

    if exists (select * from dbo.sysobjects where id = object_id(N'Clasificacion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Clasificacion

    if exists (select * from dbo.sysobjects where id = object_id(N'Comentario') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Comentario

    if exists (select * from dbo.sysobjects where id = object_id(N'EmailQueue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmailQueue

    if exists (select * from dbo.sysobjects where id = object_id(N'Enumerations') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Enumerations

    if exists (select * from dbo.sysobjects where id = object_id(N'EquipoLocal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EquipoLocal

    if exists (select * from dbo.sysobjects where id = object_id(N'Equipo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Equipo

    if exists (select * from dbo.sysobjects where id = object_id(N'Estado') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Estado

    if exists (select * from dbo.sysobjects where id = object_id(N'Estandar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Estandar

    if exists (select * from dbo.sysobjects where id = object_id(N'EstandarSistema') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EstandarSistema

    if exists (select * from dbo.sysobjects where id = object_id(N'TipoLocalEstandar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TipoLocalEstandar

    if exists (select * from dbo.sysobjects where id = object_id(N'Evaluacion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Evaluacion

    if exists (select * from dbo.sysobjects where id = object_id(N'GerenteGeneral') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table GerenteGeneral

    if exists (select * from dbo.sysobjects where id = object_id(N'GerenteNacional') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table GerenteNacional

    if exists (select * from dbo.sysobjects where id = object_id(N'GerenteRegional') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table GerenteRegional

    if exists (select * from dbo.sysobjects where id = object_id(N'GrupoEstandar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table GrupoEstandar

    if exists (select * from dbo.sysobjects where id = object_id(N'Grupo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Grupo

    if exists (select * from dbo.sysobjects where id = object_id(N'GruposUsuario') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table GruposUsuario

    if exists (select * from dbo.sysobjects where id = object_id(N'ImagenEstandar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ImagenEstandar

    if exists (select * from dbo.sysobjects where id = object_id(N'ImagenEvaluacion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ImagenEvaluacion

    if exists (select * from dbo.sysobjects where id = object_id(N'ImagenLocal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ImagenLocal

    if exists (select * from dbo.sysobjects where id = object_id(N'JefeArea') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table JefeArea

    if exists (select * from dbo.sysobjects where id = object_id(N'Local') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Local

    if exists (select * from dbo.sysobjects where id = object_id(N'EventLog') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EventLog

    if exists (select * from dbo.sysobjects where id = object_id(N'Nivel') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Nivel

    if exists (select * from dbo.sysobjects where id = object_id(N'Opcion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Opcion

    if exists (select * from dbo.sysobjects where id = object_id(N'PerfilUsuario') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PerfilUsuario

    if exists (select * from dbo.sysobjects where id = object_id(N'Persona') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Persona

    if exists (select * from dbo.sysobjects where id = object_id(N'Posicion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Posicion

    if exists (select * from dbo.sysobjects where id = object_id(N'Projects') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Projects

    if exists (select * from dbo.sysobjects where id = object_id(N'ProjectMembers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ProjectMembers

    if exists (select * from dbo.sysobjects where id = object_id(N'Provincia') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Provincia

    if exists (select * from dbo.sysobjects where id = object_id(N'Region') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Region

    if exists (select * from dbo.sysobjects where id = object_id(N'RespuestaComentario') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RespuestaComentario

    if exists (select * from dbo.sysobjects where id = object_id(N'Respuesta') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Respuesta

    if exists (select * from dbo.sysobjects where id = object_id(N'Settings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Settings

    if exists (select * from dbo.sysobjects where id = object_id(N'Sistema') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Sistema

    if exists (select * from dbo.sysobjects where id = object_id(N'TipoEquipo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TipoEquipo

    if exists (select * from dbo.sysobjects where id = object_id(N'TipoEvaluacionGrupoEstandar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TipoEvaluacionGrupoEstandar

    if exists (select * from dbo.sysobjects where id = object_id(N'TipoEvaluacion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TipoEvaluacion

    if exists (select * from dbo.sysobjects where id = object_id(N'TipoLocal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TipoLocal

    if exists (select * from dbo.sysobjects where id = object_id(N'Usuario') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Usuario

    if exists (select * from dbo.sysobjects where id = object_id(N'ValorAtributo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ValorAtributo

    if exists (select * from dbo.sysobjects where id = object_id(N'IdentityRole') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table IdentityRole

    if exists (select * from dbo.sysobjects where id = object_id(N'RolesToUsers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RolesToUsers

    if exists (select * from dbo.sysobjects where id = object_id(N'IdentityUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table IdentityUser

    if exists (select * from dbo.sysobjects where id = object_id(N'IdentityUserClaim') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table IdentityUserClaim

    if exists (select * from dbo.sysobjects where id = object_id(N'IdentityUserLogin') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table IdentityUserLogin

    create table AplicacionCliente (
        IdAplicacionCliente UNIQUEIDENTIFIER not null,
       AppId NVARCHAR(50) default ''  not null,
       Secret NVARCHAR(500) default ''  not null,
       Nombre NVARCHAR(100) default ''  not null,
       TipoAplicacion SMALLINT default 0  not null,
       EstaActiva BIT default 0  not null,
       RefreshTokenLifeTime INT default 0  not null,
       AllowedOrigin NVARCHAR(500) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       primary key (IdAplicacionCliente)
    )

    create table Atributo (
        IdAtributo UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Etiqueta NVARCHAR(255) default ''  not null,
       TipoDeDato SMALLINT default 0  not null,
       Entidad NVARCHAR(255) default ''  not null,
       Datos NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       primary key (IdAtributo)
    )

    create table Cadena (
        IdCadena UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Manual NVARCHAR(500) default ''  not null,
       Logo NVARCHAR(500) default ''  not null,
       FechaFundacion DATETIME default getdate()  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdCadena)
    )

    create table Calificacion (
        IdCalificacion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdCalificacion)
    )

    create table Categoria (
        IdCategoria UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdCategoria)
    )

    create table Ciudad (
        IdCiudad UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdProvincia UNIQUEIDENTIFIER not null,
       primary key (IdCiudad)
    )

    create table Clasificacion (
        IdClasificacion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdClasificacion)
    )

    create table Comentario (
        IdComentario UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Valor NVARCHAR(MAX) default ''  not null,
       TipoComentario SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdEstandar UNIQUEIDENTIFIER not null,
       primary key (IdComentario)
    )

    create table EmailQueue (
        IdEmailQueueItem INT IDENTITY NOT NULL,
       Priority SMALLINT default 1  not null,
       IsBodyHtml BIT default 0  not null,
       Subject NVARCHAR(1024) not null,
       [From] NVARCHAR(256) not null,
       [To] NVARCHAR(1024) not null,
       Cc NVARCHAR(256) null,
       Bcc NVARCHAR(256) null,
       [Body] NVARCHAR(MAX) null,
       NextTryTime DATETIME null,
       NumberOfTries INT not null,
       CreatedOn DATETIME not null,
       primary key (IdEmailQueueItem)
    )

    create table Enumerations (
        IdEnumeration INT IDENTITY NOT NULL,
       OptionGroup NVARCHAR(255) default ''  not null,
       OptionName NVARCHAR(255) default ''  not null,
       Name NVARCHAR(255) default ''  not null,
       Value NVARCHAR(255) default ''  not null,
       Position INT default 0  not null,
       IsDefault BIT default 0  not null,
       primary key (IdEnumeration)
    )

    create table EquipoLocal (
        IdEquipoLocal UNIQUEIDENTIFIER not null,
       Utilidad SMALLINT default 0  not null,
       Control SMALLINT default 0  not null,
       Cantidad INT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEquipo UNIQUEIDENTIFIER not null,
       IdLocal UNIQUEIDENTIFIER not null,
       primary key (IdEquipoLocal)
    )

    create table Equipo (
        IdEquipo UNIQUEIDENTIFIER not null,
       Modelo NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdTipoEquipo UNIQUEIDENTIFIER not null,
       IdLocal UNIQUEIDENTIFIER null,
       primary key (IdEquipo)
    )

    create table Estado (
        IdEstado UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Grupo NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       primary key (IdEstado)
    )

    create table Estandar (
        IdEstandar UNIQUEIDENTIFIER not null,
       Codigo NVARCHAR(50) default ''  not null unique,
       Nombre NVARCHAR(255) default ''  not null,
       Descripcion NVARCHAR(MAX) default ''  not null,
       NotasEspeciales NVARCHAR(MAX) default ''  not null,
       TipoEstandar SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstandarPadre UNIQUEIDENTIFIER null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdNivel UNIQUEIDENTIFIER not null,
       IdCategoria UNIQUEIDENTIFIER not null,
       IdGrupoEstandar UNIQUEIDENTIFIER null,
       IdClasificacion UNIQUEIDENTIFIER null,
       IdSistema UNIQUEIDENTIFIER null,
       IdTipoLocal UNIQUEIDENTIFIER null,
       primary key (IdEstandar)
    )

    create table EstandarSistema (
        IdEstandar UNIQUEIDENTIFIER not null,
       IdSistema UNIQUEIDENTIFIER not null
    )

    create table TipoLocalEstandar (
        IdEstandar UNIQUEIDENTIFIER not null,
       IdTipoLocal UNIQUEIDENTIFIER not null
    )

    create table Evaluacion (
        IdEvaluacion UNIQUEIDENTIFIER not null,
       FechaEvaluacion DATETIME default getdate()  not null,
       HoraEvaluacion DATETIME default getdate()  not null,
       NombreRGM NVARCHAR(100) default ''  not null,
       NombreMIC NVARCHAR(255) default ''  not null,
       ParteDelDia SMALLINT default 0  not null,
       TipoVisita SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdTipoEvaluacion UNIQUEIDENTIFIER not null,
       IdPosicionMIC UNIQUEIDENTIFIER not null,
       IdLocal UNIQUEIDENTIFIER not null,
       primary key (IdEvaluacion)
    )

    create table GerenteGeneral (
        IdGerenteGeneral UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdGerenteGeneral)
    )

    create table GerenteNacional (
        IdGerenteNacional UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdCadena UNIQUEIDENTIFIER not null,
       IdGerenteGeneral UNIQUEIDENTIFIER not null,
       primary key (IdGerenteNacional)
    )

    create table GerenteRegional (
        IdGerenteRegional UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdGerenteNacional UNIQUEIDENTIFIER not null,
       primary key (IdGerenteRegional)
    )

    create table GrupoEstandar (
        IdGrupoEstandar UNIQUEIDENTIFIER not null,
       Codigo NVARCHAR(50) default ''  not null unique,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Imagen NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdGrupoEstandar)
    )

    create table Grupo (
        IdGrupo INT IDENTITY NOT NULL,
       Nombre NVARCHAR(100) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       CreadoPor NVARCHAR(100) default ''  not null,
       ActualizadoPor NVARCHAR(100) default ''  not null,
       primary key (IdGrupo)
    )

    create table GruposUsuario (
        IdGrupo INT not null,
       IdUsuario INT not null
    )

    create table ImagenEstandar (
        IdImagenEstandar UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Imagen NVARCHAR(255) default ''  not null,
       TipoImagen SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdRespuesta UNIQUEIDENTIFIER not null,
       primary key (IdImagenEstandar)
    )

    create table ImagenEvaluacion (
        IdImagenEvaluacion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Imagen NVARCHAR(255) default ''  not null,
       TipoImagen SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdEvaluacion UNIQUEIDENTIFIER not null,
       primary key (IdImagenEvaluacion)
    )

    create table ImagenLocal (
        IdImagenLocal UNIQUEIDENTIFIER not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Imagen NVARCHAR(255) default ''  not null,
       Tipo SMALLINT default 0  not null,
       Orden INT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdLocal UNIQUEIDENTIFIER not null,
       primary key (IdImagenLocal)
    )

    create table JefeArea (
        IdJefeArea UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdGerenteRegional UNIQUEIDENTIFIER not null,
       primary key (IdJefeArea)
    )

    create table Local (
        IdLocal UNIQUEIDENTIFIER not null,
       Codigo NVARCHAR(50) default ''  not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Imagen NVARCHAR(255) default ''  not null,
       Direccion NVARCHAR(255) default ''  not null,
       Telefono NVARCHAR(255) default ''  not null,
       Email NVARCHAR(255) default ''  not null,
       Ruc NVARCHAR(255) default ''  not null,
       Logo NVARCHAR(255) default ''  not null,
       OpClave NVARCHAR(255) default ''  not null,
       Propietario NVARCHAR(255) default ''  not null,
       AC NVARCHAR(255) default ''  not null,
       Concepto SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdTipoLocal UNIQUEIDENTIFIER not null,
       IdCadena UNIQUEIDENTIFIER not null,
       IdCiudad UNIQUEIDENTIFIER not null,
       IdJefeArea UNIQUEIDENTIFIER not null,
       primary key (IdLocal)
    )

    create table EventLog (
        IdLogItem INT IDENTITY NOT NULL,
       IsVisible BIT null,
       Message NVARCHAR(MAX) not null,
       MessageDescription NVARCHAR(MAX) not null,
       ObjectId NVARCHAR(255) null,
       ObjectType NVARCHAR(255) null,
       Source NVARCHAR(255) null,
       Username NVARCHAR(255) null,
       Category NVARCHAR(255) null,
       EventDate DATETIME null,
       EventType SMALLINT null,
       primary key (IdLogItem)
    )

    create table Nivel (
        IdNivel UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdNivel)
    )

    create table Opcion (
        IdOpcion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Etiqueta NVARCHAR(255) default ''  not null,
       Valor NVARCHAR(MAX) default ''  not null,
       TipoOpcion SMALLINT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdComentario UNIQUEIDENTIFIER null,
       primary key (IdOpcion)
    )

    create table PerfilUsuario (
        IdPerfilUsuario BIGINT IDENTITY NOT NULL,
       Nombre NVARCHAR(50) default ''  not null,
       Apellido NVARCHAR(50) default ''  not null,
       Direccion NVARCHAR(200) default ''  not null,
       Telefono NVARCHAR(20) default ''  not null,
       IdUsuario INT null unique,
       primary key (IdPerfilUsuario)
    )

    create table Persona (
        IdPersona UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Apellido NVARCHAR(255) default ''  not null,
       FechaNacimiento DATETIME default getdate()  not null,
       Email NVARCHAR(255) default ''  not null,
       Telefono NVARCHAR(20) default ''  not null,
       Direccion NVARCHAR(255) default ''  not null,
       Fotografia NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdPersona)
    )

    create table Posicion (
        IdPosicion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdCadena UNIQUEIDENTIFIER not null,
       primary key (IdPosicion)
    )

    create table Projects (
        IdProject INT IDENTITY NOT NULL,
       Name NVARCHAR(250) default ''  not null,
       Description NVARCHAR(500) default ''  not null,
       Identifier NVARCHAR(50) default ''  not null,
       IsPrivate BIT default 0  not null,
       CreatedOn DATETIME default getdate()  not null,
       CreatedBy NVARCHAR(50) default ''  not null,
       UpdatedOn DATETIME default getdate()  not null,
       UpdateBy NVARCHAR(50) default ''  not null,
       primary key (IdProject)
    )

    create table ProjectMembers (
        IdProjectMember INT IDENTITY NOT NULL,
       CreatedBy NVARCHAR(50) default ''  not null,
       CreatedOn DATETIME default getdate()  not null,
       IdUsuario INT not null,
       IdProject INT not null,
       IdGrupo INT not null,
       primary key (IdProjectMember)
    )

    create table Provincia (
        IdProvincia UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       IdRegion UNIQUEIDENTIFIER not null,
       primary key (IdProvincia)
    )

    create table Region (
        IdRegion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdRegion)
    )

    create table RespuestaComentario (
        IdRespuestaComentario UNIQUEIDENTIFIER not null,
       Valor NVARCHAR(MAX) default ''  not null,
       Detalle NVARCHAR(MAX) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdRespuesta UNIQUEIDENTIFIER not null,
       primary key (IdRespuestaComentario)
    )

    create table Respuesta (
        IdRespuesta UNIQUEIDENTIFIER not null,
       Valor BIT default ''  not null,
       Detalle NVARCHAR(MAX) default ''  not null,
       FechaRespuesta DATETIME default getdate()  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEvaluacion UNIQUEIDENTIFIER not null,
       IdEstandar UNIQUEIDENTIFIER not null,
       primary key (IdRespuesta)
    )

    create table Settings (
        IdSetting INT IDENTITY NOT NULL,
       Name NVARCHAR(500) default ''  not null,
       OptionName NVARCHAR(MAX) default ''  not null,
       Value NVARCHAR(MAX) default ''  not null,
       CreatedBy NVARCHAR(50) default ''  not null,
       UpdatedBy NVARCHAR(50) default ''  not null,
       CreatedOn DATETIME default getdate()  not null,
       UpdatedOn DATETIME default getdate()  not null,
       primary key (IdSetting)
    )

    create table Sistema (
        IdSistema UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdSistema)
    )

    create table TipoEquipo (
        IdTipoEquipo UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdTipoEquipo)
    )

    create table TipoEvaluacionGrupoEstandar (
        IdTipoEvaluacionGrupoEstandar UNIQUEIDENTIFIER not null,
       Orden INT default 0  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdTipoEvaluacion UNIQUEIDENTIFIER not null,
       IdGrupoEstandar UNIQUEIDENTIFIER not null,
       primary key (IdTipoEvaluacionGrupoEstandar)
    )

    create table TipoEvaluacion (
        IdTipoEvaluacion UNIQUEIDENTIFIER not null,
       Nombre NVARCHAR(50) default ''  not null,
       Descripcion NVARCHAR(255) default ''  not null,
       Observaciones NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdTipoEvaluacion)
    )

    create table TipoLocal (
        IdTipoLocal UNIQUEIDENTIFIER not null,
       Detalle NVARCHAR(255) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdEstado UNIQUEIDENTIFIER not null,
       primary key (IdTipoLocal)
    )

    create table Usuario (
        IdUsuario INT IDENTITY NOT NULL,
       NombreUsuario NVARCHAR(100) default ''  not null,
       NombreMostrar NVARCHAR(100) default ''  not null,
       Email NVARCHAR(100) default ''  not null,
       EstaBloqueado BIT default 0  not null,
       EstaEnLinea BIT default 1  not null,
       EsLdap BIT default 0  not null,
       LocalPassword NVARCHAR(255) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       CuentaAccesosFallidos INT default 0  not null,
       UltimoLoginEn DATETIME default getdate()  not null,
       UltimaActividadEn DATETIME default getdate()  not null,
       primary key (IdUsuario)
    )

    create table ValorAtributo (
        IdValorAtributo UNIQUEIDENTIFIER not null,
       Entidad NVARCHAR(255) default ''  not null,
       Valor NVARCHAR(MAX) default ''  not null,
       Extra NVARCHAR(MAX) default ''  not null,
       CreadoPor NVARCHAR(50) default ''  not null,
       ActualizadoPor NVARCHAR(50) default ''  not null,
       CreadoEn DATETIME default getdate()  not null,
       ActualizadoEn DATETIME default getdate()  not null,
       IdAtributo UNIQUEIDENTIFIER not null,
       primary key (IdValorAtributo)
    )

    create table IdentityRole (
        IdIdentityRole NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       primary key (IdIdentityRole)
    )

    create table RolesToUsers (
        IdIdentityRole NVARCHAR(255) not null,
       IdIdentityUser NVARCHAR(255) not null
    )

    create table IdentityUser (
        IdIdentityUser NVARCHAR(255) not null,
       AccessFailedCount INT null,
       Email NVARCHAR(255) null,
       EmailConfirmed BIT null,
       LockoutEnabled BIT null,
       LockoutEndDateUtc DATETIME null,
       PasswordHash NVARCHAR(255) null,
       PhoneNumber NVARCHAR(255) null,
       PhoneNumberConfirmed BIT null,
       TwoFactorEnabled BIT null,
       UserName NVARCHAR(255) null,
       SecurityStamp NVARCHAR(255) null,
       primary key (IdIdentityUser)
    )

    create table IdentityUserClaim (
        IdIdentityUserClaim INT IDENTITY NOT NULL,
       ClaimType NVARCHAR(255) null,
       ClaimValue NVARCHAR(255) null,
       IdUser NVARCHAR(255) null,
       IdIdentityUser NVARCHAR(255) null,
       primary key (IdIdentityUserClaim)
    )

    create table IdentityUserLogin (
        IdIdentityUserLogin INT IDENTITY NOT NULL,
       LoginProvider NVARCHAR(255) null,
       ProviderKey NVARCHAR(255) null,
       IdIdentityUser NVARCHAR(255) null,
       primary key (IdIdentityUserLogin)
    )

    alter table Cadena 
        add constraint FK2F87092369F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Calificacion 
        add constraint FKC671050A69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Categoria 
        add constraint FK9AE6767269F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Ciudad 
        add constraint FK33E1341569F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Ciudad 
        add constraint FK33E134157EA60DF5 
        foreign key (IdProvincia) 
        references Provincia

    alter table Clasificacion 
        add constraint FKDCB5519769F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Comentario 
        add constraint FKB0AB54ED69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Comentario 
        add constraint FKB0AB54ED3BFD0DDB 
        foreign key (IdEstandar) 
        references Estandar

    alter table EquipoLocal 
        add constraint FK73A000A64B06DCA7 
        foreign key (IdEquipo) 
        references Equipo

    alter table EquipoLocal 
        add constraint FK73A000A61F4CBDC9 
        foreign key (IdLocal) 
        references Local

    alter table Equipo 
        add constraint FK3239344269F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Equipo 
        add constraint FK32393442B155B376 
        foreign key (IdTipoEquipo) 
        references TipoEquipo

    alter table Equipo 
        add constraint FK323934421F4CBDC9 
        foreign key (IdLocal) 
        references Local

    alter table Estandar 
        add constraint FKF73DD6DE8B4DF0DB 
        foreign key (IdEstandarPadre) 
        references Estandar

    alter table Estandar 
        add constraint FKF73DD6DE69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Estandar 
        add constraint FKF73DD6DEDD5D4C78 
        foreign key (IdNivel) 
        references Nivel

    alter table Estandar 
        add constraint FKF73DD6DEDE24641F 
        foreign key (IdCategoria) 
        references Categoria

    alter table Estandar 
        add constraint FKF73DD6DE2C6C496B 
        foreign key (IdGrupoEstandar) 
        references GrupoEstandar

    alter table Estandar 
        add constraint FKF73DD6DEDCFF6836 
        foreign key (IdClasificacion) 
        references Clasificacion

    alter table Estandar 
        add constraint FKF73DD6DE9ABE0AB9 
        foreign key (IdSistema) 
        references Sistema

    alter table Estandar 
        add constraint FKF73DD6DED4770A9C 
        foreign key (IdTipoLocal) 
        references TipoLocal

    alter table EstandarSistema 
        add constraint FK1B1427669ABE0AB9 
        foreign key (IdSistema) 
        references Sistema

    alter table EstandarSistema 
        add constraint FK1B1427663BFD0DDB 
        foreign key (IdEstandar) 
        references Estandar

    alter table TipoLocalEstandar 
        add constraint FKBFC562F2D4770A9C 
        foreign key (IdTipoLocal) 
        references TipoLocal

    alter table TipoLocalEstandar 
        add constraint FKBFC562F23BFD0DDB 
        foreign key (IdEstandar) 
        references Estandar

    alter table Evaluacion 
        add constraint FKC8F07AF69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Evaluacion 
        add constraint FKC8F07AFE27188D5 
        foreign key (IdTipoEvaluacion) 
        references TipoEvaluacion

    alter table Evaluacion 
        add constraint FKC8F07AF1B0CA1B7 
        foreign key (IdPosicionMIC) 
        references Posicion

    alter table Evaluacion 
        add constraint FKC8F07AF1F4CBDC9 
        foreign key (IdLocal) 
        references Local

    alter table GerenteGeneral 
        add constraint FK399BC6BC69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table GerenteNacional 
        add constraint FK50BCC20E69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table GerenteNacional 
        add constraint FK50BCC20EBC1D5800 
        foreign key (IdCadena) 
        references Cadena

    alter table GerenteNacional 
        add constraint FK50BCC20EE0B8CE2C 
        foreign key (IdGerenteGeneral) 
        references GerenteGeneral

    alter table GerenteRegional 
        add constraint FK8D44362D69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table GerenteRegional 
        add constraint FK8D44362DC3B7F36E 
        foreign key (IdGerenteNacional) 
        references GerenteNacional

    alter table GrupoEstandar 
        add constraint FKE8279AE669F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table GruposUsuario 
        add constraint FK634A896825013C81 
        foreign key (IdUsuario) 
        references Usuario

    alter table GruposUsuario 
        add constraint FK634A896836334802 
        foreign key (IdGrupo) 
        references Grupo

    alter table ImagenEstandar 
        add constraint FK216A2AC69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table ImagenEstandar 
        add constraint FK216A2AC4F078D59 
        foreign key (IdRespuesta) 
        references Respuesta

    alter table ImagenEvaluacion 
        add constraint FK1C87419169F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table ImagenEvaluacion 
        add constraint FK1C874191E491A45 
        foreign key (IdEvaluacion) 
        references Respuesta

    alter table ImagenLocal 
        add constraint FKE2CF8A821F4CBDC9 
        foreign key (IdLocal) 
        references Local

    alter table JefeArea 
        add constraint FKC6AE60FC69F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table JefeArea 
        add constraint FKC6AE60FC38971873 
        foreign key (IdGerenteRegional) 
        references GerenteRegional

    alter table Local 
        add constraint FKBA56958169F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Local 
        add constraint FKBA569581D4770A9C 
        foreign key (IdTipoLocal) 
        references TipoLocal

    alter table Local 
        add constraint FKBA569581BC1D5800 
        foreign key (IdCadena) 
        references Cadena

    alter table Local 
        add constraint FKBA56958142AB4D7A 
        foreign key (IdCiudad) 
        references Ciudad

    alter table Local 
        add constraint FKBA56958169159F7A 
        foreign key (IdJefeArea) 
        references JefeArea

    alter table Nivel 
        add constraint FK4B07A90869F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Opcion 
        add constraint FK173F950569F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Opcion 
        add constraint FK173F9505EB81B60B 
        foreign key (IdComentario) 
        references Comentario

    alter table PerfilUsuario 
        add constraint FKC4E76C6125013C81 
        foreign key (IdUsuario) 
        references Usuario

    alter table Persona 
        add constraint FK4420169469F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Posicion 
        add constraint FKFD95CA4769F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Posicion 
        add constraint FKFD95CA47BC1D5800 
        foreign key (IdCadena) 
        references Cadena

    alter table ProjectMembers 
        add constraint FK7DAD617F25013C81 
        foreign key (IdUsuario) 
        references Usuario

    alter table ProjectMembers 
        add constraint FK7DAD617F557351F5 
        foreign key (IdProject) 
        references Projects

    alter table ProjectMembers 
        add constraint FK7DAD617F36334802 
        foreign key (IdGrupo) 
        references Grupo

    alter table Provincia 
        add constraint FK2FCBC58469F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table Provincia 
        add constraint FK2FCBC584987692E6 
        foreign key (IdRegion) 
        references Region

    alter table Region 
        add constraint FK9FB469A469F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table RespuestaComentario 
        add constraint FK42EDA5964F078D59 
        foreign key (IdRespuesta) 
        references Respuesta

    alter table Respuesta 
        add constraint FK3B204E273BF95419 
        foreign key (IdEvaluacion) 
        references Evaluacion

    alter table Respuesta 
        add constraint FK3B204E273BFD0DDB 
        foreign key (IdEstandar) 
        references Estandar

    alter table Sistema 
        add constraint FK55D60E0669F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table TipoEquipo 
        add constraint FK698DC32169F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table TipoEvaluacionGrupoEstandar 
        add constraint FK57544FF6E27188D5 
        foreign key (IdTipoEvaluacion) 
        references TipoEvaluacion

    alter table TipoEvaluacionGrupoEstandar 
        add constraint FK57544FF62C6C496B 
        foreign key (IdGrupoEstandar) 
        references GrupoEstandar

    alter table TipoEvaluacion 
        add constraint FKFCEDB9F869F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table TipoLocal 
        add constraint FK1E4B502469F3AA74 
        foreign key (IdEstado) 
        references Estado

    alter table ValorAtributo 
        add constraint FK7BDEBACE2500F343 
        foreign key (IdAtributo) 
        references Atributo

    alter table RolesToUsers 
        add constraint FKA135DE33C211593 
        foreign key (IdIdentityUser) 
        references IdentityUser

    alter table RolesToUsers 
        add constraint FKA135DE3DB7ED1C8 
        foreign key (IdIdentityRole) 
        references IdentityRole

    alter table IdentityUserClaim 
        add constraint FKD3D2ED76D3252E64 
        foreign key (IdUser) 
        references IdentityUser

    alter table IdentityUserClaim 
        add constraint FKD3D2ED763C211593 
        foreign key (IdIdentityUser) 
        references IdentityUser

    alter table IdentityUserLogin 
        add constraint FKA422A8223C211593 
        foreign key (IdIdentityUser) 
        references IdentityUser
