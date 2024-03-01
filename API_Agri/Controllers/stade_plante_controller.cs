using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Agri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StadePlanteController : ControllerBase
    {
        private readonly AgriContext AgriContext;

        public StadePlanteController(AgriContext AgriContext)
        {
            this.AgriContext = AgriContext;
        }

        /// <summary>
        /// Définition du Web Service
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client a retourné</param>   
        /// <response code="200">client sélectionné</response>
        /// <response code="404">client introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("GetStadePlantes")]
        public async Task<ActionResult<List<StadePlante>>> Get()
        {
            var List = await AgriContext.StadePlantes.Select(
                s => new StadePlante
                {
                    StadeId = s.StadeId,
                    PlanteId = s.PlanteId,
                    StadePlanteKc= s.StadePlanteKc,
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetStadePlanteById")]
        public async Task<ActionResult<StadePlante>> GetStadePlanteById(int Id)
        {
            StadePlante StadePlante = await AgriContext.StadePlantes.Select(
                    s => new StadePlante
                    {
                        StadeId = s.StadeId,
                        PlanteId = s.PlanteId,
                        StadePlanteKc= s.StadePlanteKc,
                    })
                .FirstOrDefaultAsync(s => s.StadeId == Id);

            if (StadePlante == null)
            {
                return NotFound();
            }
            else
            {
                return StadePlante;
            }
        }

        [HttpPost("InsertStadePlante")]
        public async Task<HttpStatusCode> InsertStadePlante(StadePlante StadePlante)
        {
            var entity = new StadePlante()
            {
                StadeId = StadePlante.StadeId,
                PlanteId = StadePlante.PlanteId,
                StadePlanteKc = StadePlante.StadePlanteKc
            };

            AgriContext.StadePlantes.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateStadePlante")]
        public async Task<HttpStatusCode> UpdateStadePlante(StadePlante StadePlante)
        {
            var entity = await AgriContext.StadePlantes.FirstOrDefaultAsync(s => s.StadeId == StadePlante.StadeId);

            entity.StadeId = StadePlante.StadeId;
            entity.PlanteId = StadePlante.PlanteId;
            entity.StadePlanteKc = StadePlante.StadePlanteKc;

            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteStadePlante/{Id}")]
        public async Task<HttpStatusCode> DeleteStadePlante(int Id)
        {
            var entity = new StadePlante()
            {
                StadeId = Id
            };
            AgriContext.StadePlantes.Attach(entity);
            AgriContext.StadePlantes.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}