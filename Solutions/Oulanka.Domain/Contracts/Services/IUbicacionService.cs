using System;
using System.Collections;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Ubicacion;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IUbicacionService
    {
        IList<Region> GetRegiones();
        PagedList<Region> GetRegionesPagedList(int page = 0, int limit = 10);
        Region GetRegion(Guid id);
        ActionConfirmation SaveOrUpdateRegion(Region region);
        PagedList<Provincia> GetProvinciasPagedList(int page = 0, int limit = 10);
        ActionConfirmation SaveOrUpdateProvincia(Provincia provincia);
        Provincia GetProvincia(Guid id);
        PagedList<Ciudad> GetCiudadesPagedList(int i, int limit);
        IList<Provincia> GetProvincias();
        ActionConfirmation SaveOrUpdateCiudad(Ciudad ciudad);
        Ciudad GetCiudad(Guid id);
        IList<Ciudad> GetCiudades();
    }
}