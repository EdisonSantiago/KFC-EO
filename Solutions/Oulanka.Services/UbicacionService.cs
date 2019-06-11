using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Ubicacion;

namespace Oulanka.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly ICiudadRepository _ciudadRepository;
        private readonly IEventLogService _eventLogService;


        public UbicacionService(IRegionRepository regionRepository, IProvinciaRepository provinciaRepository, ICiudadRepository ciudadRepository)
        {
            _regionRepository = regionRepository;
            _provinciaRepository = provinciaRepository;
            _ciudadRepository = ciudadRepository;
        }

        public IList<Region> GetRegiones()
        {
            return _regionRepository.GetAll();
        }


        public PagedList<Region> GetRegionesPagedList(int page = 0, int limit = 10)
        {
            return _regionRepository.GetPagedList(page, limit);
        }

        public Region GetRegion(Guid id)
        {
            return _regionRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateRegion(Region region)
        {
            if (!region.IsValid()) return ActionConfirmation.CreateFailure("not valid");

            try
            {
                _regionRepository.SaveOrUpdate(region);
                _regionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public PagedList<Provincia> GetProvinciasPagedList(int page = 0, int limit = 10)
        {
            return _provinciaRepository.GetPagedList(page, limit);
        }

        public ActionConfirmation SaveOrUpdateProvincia(Provincia provincia)
        {
            if (!provincia.IsValid()) ActionConfirmation.CreateFailure("provincia no válida");

            try
            {
                _provinciaRepository.SaveOrUpdate(provincia);
                _provinciaRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, provincia.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public Provincia GetProvincia(Guid id)
        {
            return _provinciaRepository.Get(id);
        }

        public PagedList<Ciudad> GetCiudadesPagedList(int page = 0, int limit = 10)
        {
            return _ciudadRepository.GetPagedList(page, limit);
        }

        public IList<Provincia> GetProvincias()
        {
            return _provinciaRepository.GetAll().OrderBy(x => x.Nombre).ToList();
        }

        public ActionConfirmation SaveOrUpdateCiudad(Ciudad ciudad)
        {
            if (!ciudad.IsValid()) ActionConfirmation.CreateFailure("ciudad no válida");

            try
            {
                _ciudadRepository.SaveOrUpdate(ciudad);
                _ciudadRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, ciudad.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }

        public Ciudad GetCiudad(Guid id)
        {
            return _ciudadRepository.Get(id);
        }

        public IList<Ciudad> GetCiudades()
        {
            return _ciudadRepository.GetAll();
        }
    }
}