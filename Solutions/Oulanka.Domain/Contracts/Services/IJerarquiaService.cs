using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Jerarquias;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IJerarquiaService
    {
        IList<JefeArea> GetJefesAreasList();
        IList<JefeArea> GetJefesAreasList(Guid gerenteRegionalId);
        JefeArea GetJefeArea(Guid id);
        PagedList<JefeArea> GetJefesAreasPagedList(int page = 0, int limit = 10);
        PagedList<JefeArea> GetJefesAreasPagedList(Guid parentId, int page = 0, int limit = 10);
        IList<JefeArea> GetJefesAreaByCadena(Guid cadenaId);
        ActionConfirmation SaveOrUpdateJefeArea(JefeArea jefeArea);
        ActionConfirmation DeleteJefeArea(Guid id);


        PagedList<GerenteGeneral> GetGeneralPagedList(int page = 0, int limit = 10);
        GerenteGeneral GetGeneral();
        GerenteGeneral GetGeneral(Guid id);
        ActionConfirmation SaveOrUpdateGeneral(GerenteGeneral gerenteGeneral);


        PagedList<GerenteNacional> GetNacionalesPagedList(int page = 0, int limit = 10);
        PagedList<GerenteNacional> GetNacionalesPagedList(Guid parentId, int page = 0, int limit = 10);
        IList<GerenteNacional> GetNacionalesList(Guid parentId);
        GerenteNacional GetNacional(Guid id);
        ActionConfirmation SaveOrUpdateNacional(GerenteNacional nacional);
        ActionConfirmation DeleteNacional(Guid id);


        PagedList<GerenteRegional> GetRegionalesPagedList(int page = 0, int limit = 10);
        PagedList<GerenteRegional> GetRegionalesPagedList(Guid parentId, int page = 0, int limit = 10);
        IList<GerenteRegional> GetRegionalesList(Guid parentId);
        GerenteRegional GetRegional(Guid id);
        ActionConfirmation SaveOrUpdateRegional(GerenteRegional regional);
        ActionConfirmation DeleteRegional(Guid id);

    }
}