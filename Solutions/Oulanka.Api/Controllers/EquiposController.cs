using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.Services;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;

namespace Oulanka.Api.Controllers
{
    [Route("api/[Controller]")]
    public class EquiposController : BaseApiController
    {
        private readonly IEquipoService _equipoService;
        private readonly IStatusService _statusService;
        private readonly IAuthorizationService _authorizationService;

        public EquiposController()
        {
            _equipoService = ServiceLocator.Current.GetInstance<IEquipoService>();
            _statusService = ServiceLocator.Current.GetInstance<IStatusService>();
            _authorizationService = new AuthorizationService();
        }

        [Route("api/equipos/tree")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTree()
        {
            var nodeList = new List<OulankaTreeNode>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var tipoEquipos = _equipoService.GetTipoEquipoList(true);
                    foreach (var tipo in tipoEquipos)
                    {
                        var node = new OulankaTreeNode
                        {
                            Id = tipo.Id,
                            Text = tipo.Nombre,
                            NodeType = "tipoEquipo"
                        };

                        var equipos = _equipoService.GetListByTipo(tipo.Id, true);
                        foreach (var equipo in equipos)
                        {
                            node.Nodes.Add( new OulankaTreeNode
                            {
                                Id= equipo.Id,
                                Text = equipo.Modelo,
                                ParentId = tipo.Id,
                                NodeType = "equipo"
                            });
                        }

                        nodeList.Add(node);
                    }
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(nodeList);
        }

    }
}