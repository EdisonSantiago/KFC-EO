using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NHibernate.Criterion;
using NHibernate.Transform;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class EvaluacionRepository : NHibernateRepositoryWithTypedId<Evaluacion,Guid>, IEvaluacionRepository
    {
        public PagedList<Evaluacion> GetPagedList(int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Evaluacion>()
                .AddOrder(Order.Asc("Nombre"));

            return criteria.PagedList<Evaluacion>(session, page, limit);
        }

        public PagedList<EvaluacionDto> GetDtoByCadenaPagedList(Guid cadenaId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var query = session.GetNamedQuery("GetEvaluacionListByCadena")
                .SetParameter("cadenaId", cadenaId)
                .SetResultTransformer(Transformers.AliasToBean<EvaluacionDto>());

            var list =  query.List<EvaluacionDto>().Skip(page).Take(limit).ToList();
            var totalCount = query.List<EvaluacionDto>().Count;

            var pagedList = new PagedList<EvaluacionDto>(list,totalCount,page,limit);

            return pagedList;
        }

        public PagedList<EvaluacionDto> GetDtoByLocalPagedList(Guid localId, int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var query = session.GetNamedQuery("GetEvaluacionListByLocal")
                .SetParameter("localId", localId)
                .SetResultTransformer(Transformers.AliasToBean<EvaluacionDto>());

            var list =  query.List<EvaluacionDto>().Skip(page).Take(limit).ToList();
            var totalCount = query.List<EvaluacionDto>().Count;

            var pagedList = new PagedList<EvaluacionDto>(list,totalCount,page,limit);

            return pagedList;
        }
    }
}