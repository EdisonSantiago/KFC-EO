using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Models;
using SharpArch.NHibernate;

namespace Oulanka.Infrastructure.Repositories
{
    public class ProjectMemberRepository : NHibernateRepository<ProjectMember>, IProjectMemberRepository
    {
         
    }
}