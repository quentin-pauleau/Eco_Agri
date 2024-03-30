using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace API_Agri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StadeController : ControllerBase
    {
        private readonly AgriContext AgriContext;

        public StadeController(AgriContext AgriContext)
        {
            this.AgriContext = AgriContext;
        }

        /// <summary>
        /// Définition du Web Service
        /// </summary>
        /// <response code="200">client sélectionné</response>
        /// <response code="404">client introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("GetStades")]
        public async Task<ActionResult<List<Stade>>> Get()
        {
            var List = await AgriContext.Stades.Select(
                s => new Stade
                {
                    StadeId = s.StadeId,
                    StadeDescription = s.StadeDescription,
                    StadePlanteId = s.StadePlanteId,
                    StadeKc = s.StadeKc
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in List)
                    if (item.StadePlanteId != null)
                    {
                        item.Plante = await AgriContext.Plantes.Select(
                                s => new Plante
                                {
                                    PlanteId = s.PlanteId,
                                    PlanteType = s.PlanteType,
                                    PlanteNom = s.PlanteNom,
                                })
                            .FirstOrDefaultAsync(s => s.PlanteId == item.StadePlanteId);
                    }

                return List;
            }
        }

        [HttpGet("GetStadeById")]
        public async Task<ActionResult<Stade>> GetStadeById(int Id)
        {
            Stade? Stade = await AgriContext.Stades.Select(
                    s => new Stade
                    {
                        StadeId = s.StadeId,
                        StadeDescription = s.StadeDescription,
                        StadePlanteId = s.StadePlanteId,
                        StadeKc = s.StadeKc
                    })
                .FirstOrDefaultAsync(s => s.StadeId == Id);

            if (Stade == null)
            {
                return NotFound();
            }
            else
            {
                if (Stade.StadePlanteId != null)
                {
                    Stade.Plante = await AgriContext.Plantes.Select(
                            s => new Plante
                            {
                                PlanteId = s.PlanteId,
                                PlanteType = s.PlanteType,
                                PlanteNom = s.PlanteNom,
                            })
                        .FirstOrDefaultAsync(s => s.PlanteId == Stade.StadePlanteId);
                }

                return Stade;
            }
        }

        [HttpGet("GetStadeByPlante")]
        public async Task<ActionResult<List<Stade>>> GetStadeByPlante(int PlanteId)
        {
            var List = await AgriContext.Stades.Select(
                s => new Stade
                {
                    StadeId = s.StadeId,
                    StadeDescription = s.StadeDescription,
                    StadePlanteId = s.StadePlanteId,
                    StadeKc = s.StadeKc
                }
            ).Where(s => s.StadePlanteId == PlanteId).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in List)
                    if (item.StadePlanteId != null)
                    {
                        item.Plante = await AgriContext.Plantes.Select(
                                s => new Plante
                                {
                                    PlanteId = s.PlanteId,
                                    PlanteType = s.PlanteType,
                                    PlanteNom = s.PlanteNom,
                                })
                            .FirstOrDefaultAsync(s => s.PlanteId == item.StadePlanteId);
                    }

                return List;
            }
        }

        [HttpPost("InsertStade")]
        public async Task<HttpStatusCode> InsertStade(Stade Stade)
        {
            var entity = new Stade()
            {
                StadeId = Stade.StadeId,
                StadeDescription = Stade.StadeDescription,
                StadePlanteId = Stade.StadePlanteId,
                StadeKc = Stade.StadeKc
            };

            AgriContext.Stades.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateStade")]
        public async Task<HttpStatusCode> UpdateStade(Stade Stade)
        {
            var entity = await AgriContext.Stades.FirstOrDefaultAsync(s => s.StadeId == Stade.StadeId);
            if (entity != null)
            {
                entity.StadeId = Stade.StadeId;
                entity.StadeDescription = Stade.StadeDescription;
                entity.StadePlanteId = Stade.StadePlanteId;
                entity.StadeKc = Stade.StadeKc;

                await AgriContext.SaveChangesAsync();
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.BadRequest;
        }

        [HttpDelete("DeleteStade/{Id}")]
        public async Task<HttpStatusCode> DeleteStade(int Id)
        {
            var entity = new Stade()
            {
                StadeId = Id
            };
            AgriContext.Stades.Attach(entity);
            AgriContext.Stades.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}