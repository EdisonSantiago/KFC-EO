using System;
using System.Collections;
using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IStatusService
    {
        IList<Estado> GetItems();
        IList<Estado> GetItems(string grupo);
        Estado Get(Guid id);
        Estado Online();
        ActionConfirmation SaveOrUpdate(Estado estado);
        ActionConfirmation Delete(Guid id);
    }
}