using System.Collections.Generic;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;

namespace Oulanka.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMemberRepository _memberRepository;

        public ProjectService(IProjectRepository projectRepository, IProjectMemberRepository memberRepository)
        {
            _projectRepository = projectRepository;
            _memberRepository = memberRepository;
        }

        public PagedList<Project> GetProjectsPagedList(int page = 0, int limit = 10)
        {
            return _projectRepository.GetPagedList(page, limit);
        }

        public IList<ProjectDto> GetProjectsByUser(int userId)
        {
            return _projectRepository.GetByUser(userId);
        }

        public IList<ProjectDto> GetProjectsDto()
        {
            return _projectRepository.GetDtoList();
        }

        public Project GetProjectByIdentifier(string identifier)
        {
            return _projectRepository.GetByIdentifier(identifier);
        }

        public Project GetProject(int projectId)
        {
            return _projectRepository.Get(projectId);
        }

        public ActionConfirmation SaveOrUpdate(Project project)
        {
            if (!project.IsValid()) return ActionConfirmation.CreateFailure("proyecto no valido");

            try
            {
                _projectRepository.SaveOrUpdate(project);
                _projectRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");

            }
            catch (System.Exception exception)
            {
                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation SaveOrUpdateMember(ProjectMember member)
        {
            if(!member.IsValid()) return ActionConfirmation.CreateFailure("miembro no válido");

            try
            {
                _memberRepository.SaveOrUpdate(member);
                _memberRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("member created");
            }
            catch (System.Exception exception)
            {
                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }
    }
}