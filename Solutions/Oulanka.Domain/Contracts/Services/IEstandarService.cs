using System;
using System.Collections;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEstandarService
    {
        PagedList<Estandar> GetPagedList(int i, int limit);
        PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, int page = 0, int limit = 10);
        PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor, int page = 0, int limit = 10);
        PagedList<Estandar> GetByParentPagedList(Guid parentId, int page = 0, int limit = 10);
        IList<Estandar> GetByGrupo(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor);
        IList<Estandar> GetByParent(Guid parentId);
        IList<Estandar> GetPicklist(Guid parentId);
        ActionConfirmation SaveOrUpdate(Estandar estandar);
        Estandar Get(Guid id);
        Estandar GetByCodigo(string codigo);

        ActionConfirmation Delete(Guid id);

        //Grupos Estandar
        PagedList<GrupoEstandar> GetGrupoPagedList(int page = 0, int limit = 10);
        IList<GrupoEstandar> GetGruposList();
        GrupoEstandar GetGrupo(Guid id);
        GrupoEstandar GetGrupo(string codigo);
        ActionConfirmation SaveOrUpdateGrupo(GrupoEstandar grupoEstandar);
        ActionConfirmation DeleteGrupo(Guid id);

        PagedList<Calificacion> GetCalificacionesPagedList(int page = 0, int limit = 10);
        IEnumerable<Calificacion> GetCalificacionList();
        Calificacion GetCalificacion(Guid id);
        ActionConfirmation SaveOrUpdateCalificacion(Calificacion item);
        ActionConfirmation DeleteCalificacion(Guid id);

        PagedList<Clasificacion> GetClasificacionPagedList(int page = 0, int limit = 10);
        IEnumerable<Clasificacion> GetClasificacionList();
        Clasificacion GetClasificacion(Guid id);
        ActionConfirmation SaveOrUpdateClasificacion(Clasificacion item);
        ActionConfirmation DeleteClasificacion(Guid id);

        PagedList<Categoria> GetCategoriaPagedList(int page = 0, int limit = 10);
        IEnumerable<Categoria> GetCategoriaList();
        Categoria GetCategoria(Guid id);
        ActionConfirmation SaveOrUpdateCategoria(Categoria item);
        ActionConfirmation DeleteCategoria(Guid id);

        PagedList<Nivel> GetNivelPagedList(int page = 0, int limit = 10);
        IEnumerable<Nivel> GetNivelList();
        Nivel GetNivel(Guid id);
        ActionConfirmation SaveOrUpdateNivel(Nivel item);
        ActionConfirmation DeleteNivel(Guid id);

        PagedList<Opcion> GetOpcionPagedList(int page = 0, int limit = 10);
        IEnumerable<Opcion> GetOpcionList();
        Opcion GetOpcion(Guid id);
        ActionConfirmation SaveOrUpdateOpcion(Opcion item);
        ActionConfirmation DeleteOpcion(Guid id);

        PagedList<Sistema> GetSistemaPagedList(int page = 0, int limit = 10);
        IList<Sistema> GetSistemaList();
        Sistema GetSistema(Guid id);
        ActionConfirmation SaveOrUpdateSistema(Sistema item);
        ActionConfirmation DeleteSistema(Guid id);
    }
}