using System;
using NHibernate.Criterion;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class ImagenEvaluacionRepository : NHibernateRepository<ImagenEvaluacion> , IImagenEvaluacionRepository 
    {
       
        public PagedList<ImagenEvaluacion> GetPagedListByEvaluacion(Guid evaluationId,int page = 0, int limit = 10)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<ImagenEvaluacion>()
                .CreateAlias("Evaluacion", "evaluacion")
                .Add(Restrictions.Eq("evaluacion.Id", evaluationId));

            return criteria.PagedList<ImagenEvaluacion>(session, page, limit);
        }
    }
}