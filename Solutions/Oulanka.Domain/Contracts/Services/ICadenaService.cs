using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface ICadenaService
    {
        PagedList<Cadena> GetPagedList(int page = 0, int limit = 10);
        IList<Cadena> GetList();
        List<Cadena> GetList(bool onlineOnly);
        Cadena Get(Guid id);
        ActionConfirmation SaveOrUpdate(Cadena cadena);
        ActionConfirmation Delete(Guid id);
    }
}