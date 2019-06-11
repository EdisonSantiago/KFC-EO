using System;
using System.Collections.Generic;
using Oulanka.Domain.Models;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IStatusRepository : IRepositoryWithTypedId<Estado, Guid>
    {
        IList<Estado> GetItems(string grupo);
    }
}