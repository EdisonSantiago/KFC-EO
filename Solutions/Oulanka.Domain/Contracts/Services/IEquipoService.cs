using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEquipoService
    {
        PagedList<Equipo> GetPagedList(int page = 0, int limit = 10);
        IList<Equipo> GetList();
        IList<Equipo> GetListByTipo(Guid tipoId, bool onlineOnly);
        Equipo Get(Guid id);
        ActionConfirmation SaveOrUpdate(Equipo equipo);
        ActionConfirmation Delete(Guid id);

        PagedList<TipoEquipo> GetTipoEquipoPagedList(int page = 0, int limit = 10);
        IList<TipoEquipo> GetTipoEquipoList();
        IList<TipoEquipo> GetTipoEquipoList(bool onlineOnly);
        TipoEquipo GetTipoEquipo(Guid id);
        ActionConfirmation SaveOrUpdateTipoEquipo(TipoEquipo tipoEquipo);
        ActionConfirmation DeleteTipoEquipo(Guid id);
    }
}