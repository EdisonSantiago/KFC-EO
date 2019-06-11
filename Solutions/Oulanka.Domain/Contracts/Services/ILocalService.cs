using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Domain.Contracts.Services
{
    public interface ILocalService
    {
        //Locales
        PagedList<Local> GetPagedList(int page = 0, int limit = 10);
        PagedList<Local> GetPagedList(Guid cadenaId, int page = 0, int limit = 10);
        PagedList<Local> GetPagedList(Guid cadenaId,bool onlineOnly, int page = 0, int limit = 10);
        IList<Local> GetList();
        IList<Local> GetList(Guid cadenaId);
        IList<Local> GetList(Guid cadenaId, bool onlineOnly);
        Local Get(Guid id);
        Local GetByCode(string code);
        ActionConfirmation SaveOrUpdate(Local local);
        ActionConfirmation Delete(Guid id);

        //Tipo Locales
        PagedList<TipoLocal> GetTiposPagedList(int page = 0, int limit = 10);
        IList<TipoLocal> GetTiposList();
        TipoLocal GetTipo(Guid id);
        ActionConfirmation SaveOrUpdateTipo(TipoLocal item);
        ActionConfirmation DeleteTipo(Guid id);
        PagedList<ImagenLocal> GetImages(Guid localId, int page = 0, int limit = 10);
        IList<ImagenLocal> GetListImages(Guid localId);
        ImagenLocal GetImage(Guid id);
        ActionConfirmation SaveOrUpdateImagen(ImagenLocal imagenLocal);
    }
}