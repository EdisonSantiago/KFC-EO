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
using Oulanka.Domain.Models.Respuestas;
using Oulanka.Infrastructure.Extensions;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class RespuestaRepository : NHibernateRepositoryWithTypedId<Respuesta,Guid>, IRespuestaRepository
    {
        public IList<Respuesta> GetListByEvaluacion(Guid evaluacionId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Respuesta>()
                .CreateAlias("Evaluacion", "evaluacion")
                .Add(Restrictions.Eq("evaluacion.Id", evaluacionId));

            return criteria.List<Respuesta>();

        }

        public IList<Respuesta> GetListByEstandar(Guid standardId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<Respuesta>()
                .CreateAlias("Estandard", "estandard")
                .Add(Restrictions.Eq("estandard.Id", standardId));

            return criteria.List<Respuesta>();
        }
    }
}