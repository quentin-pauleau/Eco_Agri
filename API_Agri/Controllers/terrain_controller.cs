using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace API_Agri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerrainController : ControllerBase
    {
        private readonly AgriContext AgriContext;

        public TerrainController(AgriContext AgriContext)
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
        [HttpGet("GetTerrains")]
        public async Task<ActionResult<List<Terrain>>> Get()
        {
            var List = await AgriContext.Terrains.Select(
                s => new Terrain
                {
                    TerrainId = s.TerrainId,
                    TerrainInsee = s.TerrainInsee,
                    TerrainNom = s.TerrainNom,
                    TerrainSurface = s.TerrainSurface,
                    TerrainPlanteId= s.TerrainPlanteId
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in List)
                    if (item.TerrainPlanteId != null)
                    {
                        item.Plante = await AgriContext.Plantes.Select(
                                s => new Plante
                                {
                                    PlanteId = s.PlanteId,
                                    PlanteType = s.PlanteType,
                                    PlanteNom = s.PlanteNom,
                                })
                            .FirstOrDefaultAsync(s => s.PlanteId == item.TerrainPlanteId);
                    }

                return List;
            }
        }

        [HttpGet("GetTerrainById")]
        public async Task<ActionResult<Terrain>> GetTerrainById(int Id)
        {
            Terrain Terrain = await AgriContext.Terrains.Select(
                    s => new Terrain
                    {
                        TerrainId = s.TerrainId,
                        TerrainInsee = s.TerrainInsee,
                        TerrainNom = s.TerrainNom,
                        TerrainSurface = s.TerrainSurface,
                        TerrainPlanteId = s.TerrainPlanteId

                    })
                .FirstOrDefaultAsync(s => s.TerrainId == Id);

            if (Terrain == null)
            {
                return NotFound();
            }
            else
            {
                if (Terrain.TerrainPlanteId != null)
                {
                    Terrain.Plante = await AgriContext.Plantes.Select(
                            s => new Plante
                            {
                                PlanteId = s.PlanteId,
                                PlanteType = s.PlanteType,
                                PlanteNom = s.PlanteNom,
                            })
                        .FirstOrDefaultAsync(s => s.PlanteId == Terrain.TerrainPlanteId);
                }

                return Terrain;
            }
        }

        [HttpGet("GetTerrainByInsee")]
        public async Task<ActionResult<Terrain>> GetTerrainByInsee(string Insee)
        {
            Terrain Terrain = await AgriContext.Terrains.Select(
                s => new Terrain
                    {
                        TerrainId = s.TerrainId,
                        TerrainInsee = s.TerrainInsee,
                        TerrainNom = s.TerrainNom,
                        TerrainSurface = s.TerrainSurface,
                        TerrainPlanteId = s.TerrainPlanteId
                    })
                .FirstOrDefaultAsync(s => s.TerrainInsee == Insee);

            if (Terrain == null)
            {
                return NotFound();
            }
            else
            {
                if (Terrain.TerrainPlanteId != null)
                {
                    Terrain.Plante = await AgriContext.Plantes.Select(
                        s => new Plante
                        {
                            PlanteId = s.PlanteId,
                            PlanteType = s.PlanteType,
                            PlanteNom = s.PlanteNom,
                        })
                        .FirstOrDefaultAsync(s => s.PlanteId == Terrain.TerrainPlanteId);
                }

                return Terrain;
            }
        }

        [HttpGet("GetTerrainByPlante")]
        public async Task<ActionResult<List<Terrain>>> GetTerrainByPlante(int PlanteId)
        {
            var List = await AgriContext.Terrains.Select(
                s => new Terrain
                    {
                        TerrainId = s.TerrainId,
                        TerrainInsee = s.TerrainInsee,
                        TerrainNom = s.TerrainNom,
                        TerrainSurface = s.TerrainSurface,
                        TerrainPlanteId = s.TerrainPlanteId
                    }
                ).Where(s => s.TerrainPlanteId == PlanteId).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in List)
                    if (item.TerrainPlanteId != null)
                    {
                        item.Plante = await AgriContext.Plantes.Select(
                            s => new Plante
                            {
                                PlanteId = s.PlanteId,
                                PlanteType = s.PlanteType,
                                PlanteNom = s.PlanteNom,
                            })
                        .FirstOrDefaultAsync(s => s.PlanteId == item.TerrainPlanteId);
                    }

                return List;
            }
        }

        [HttpPost("InsertTerrain")]
        public async Task<HttpStatusCode> InsertTerrain(Terrain Terrain)
        {
            var entity = new Terrain()
            {
                TerrainId = Terrain.TerrainId,
                TerrainInsee = Terrain.TerrainInsee,
                TerrainNom = Terrain.TerrainNom,
                TerrainSurface = Terrain.TerrainSurface,
                TerrainPlanteId = Terrain.TerrainPlanteId
            };

            AgriContext.Terrains.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTerrain")]
        public async Task<HttpStatusCode> UpdateTerrain(Terrain Terrain)
        {
            var entity = await AgriContext.Terrains.FirstOrDefaultAsync(s => s.TerrainId == Terrain.TerrainId);
            entity.TerrainId = Terrain.TerrainId;
            entity.TerrainInsee = Terrain.TerrainInsee;
            entity.TerrainNom = Terrain.TerrainNom;
            entity.TerrainSurface = Terrain.TerrainSurface;
            entity.TerrainPlanteId = Terrain.TerrainPlanteId;

            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteTerrain/{Id}")]
        public async Task<HttpStatusCode> DeleteTerrain(int Id)
        {
            var entity = new Terrain()
            {
                TerrainId = Id
            };
            AgriContext.Terrains.Attach(entity);
            AgriContext.Terrains.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}