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
    public class RespuestaComentarioRepository : NHibernateRepositoryWithTypedId<RespuestaComentario,Guid>, IRespuestaComentarioRepository
    {
        public IList<RespuestaComentario> GetListByRespuesta(Guid respuestaId)
        {
            var session = RepositoryHelper.GetSession();
            var criteria = session.CreateCriteria<RespuestaComentario>()
                .CreateAlias("Respuesta", "respuesta")
                .Add(Restrictions.Eq("respuesta.Id", respuestaId));

            return criteria.List<RespuestaComentario>();
        }

        IList<RespuestaComentario> IRespuestaComentarioRepository.GetListByEstandar(Guid standardId)
        {
            throw new NotImplementedException();
        }
    }
}