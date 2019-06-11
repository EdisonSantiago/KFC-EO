using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IImagenLocalRepository : IRepositoryWithTypedId<ImagenLocal, Guid>
    {
        PagedList<ImagenLocal> GetPagedListByLocal(Guid localId, int page = 0, int limit = 10);
        IList<ImagenLocal> GetListByLocal(Guid localId);
    }
}