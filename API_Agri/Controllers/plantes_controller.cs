using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Agri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanteController : ControllerBase
    {
        private readonly AgriContext AgriContext;

        public PlanteController(AgriContext AgriContext)
        {
            this.AgriContext = AgriContext;
        }

        /// <summary>
        /// Définition du Web Service
        /// </summary>
        /// <response code="200">client sélectionné</response>
        /// <response code="404">client introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("GetPlantes")]
        public async Task<ActionResult<List<Plante>>> Get()
        {
            var List = await AgriContext.Plantes.Select(
                s => new Plante
                {
                    PlanteId = s.PlanteId,
                    PlanteType = s.PlanteType,
                    PlanteNom = s.PlanteNom,
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

        [HttpGet("GetPlanteById")]
        public async Task<ActionResult<Plante>> GetPlanteById(int Id)
        {
            Plante? Plante = await AgriContext.Plantes.Select(
                    s => new Plante
                    {
                        PlanteId = s.PlanteId,
                        PlanteType = s.PlanteType,
                        PlanteNom = s.PlanteNom,
                    })
                .FirstOrDefaultAsync(s => s.PlanteId == Id);

            if (Plante == null)
            {
                return NotFound();
            }
            else
            {
                return Plante;
            }
        }

        [HttpPost("InsertPlante")]
        public async Task<HttpStatusCode> InsertPlante(Plante Plante)
        {
            var entity = new Plante()
            {
                PlanteId = Plante.PlanteId,
                PlanteType = Plante.PlanteType,
                PlanteNom = Plante.PlanteNom,
            };

            AgriContext.Plantes.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdatePlante")]
        public async Task<HttpStatusCode> UpdatePlante(Plante Plante)
        {
            var entity = await AgriContext.Plantes.FirstOrDefaultAsync(s => s.PlanteId == Plante.PlanteId);
            if (entity != null)
            {
                entity.PlanteId = Plante.PlanteId;
                entity.PlanteType = Plante.PlanteType;
                entity.PlanteNom = Plante.PlanteNom;

                await AgriContext.SaveChangesAsync();
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        [HttpDelete("DeletePlante/{Id}")]
        public async Task<HttpStatusCode> DeletePlante(int Id)
        {
            var entity = new Plante()
            {
                PlanteId = Id
            };
            AgriContext.Plantes.Attach(entity);
            AgriContext.Plantes.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}

