using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Agri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerrainReserveController : ControllerBase
    {
        private readonly AgriContext AgriContext;

        public TerrainReserveController(AgriContext AgriContext)
        {
            this.AgriContext = AgriContext;
        }

        /// <summary>
        /// Définition du Web Service
        /// </summary>
        /// <response code="200">client sélectionné</response>
        /// <response code="404">client introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("GetTerrainsReserves")]
        public async Task<ActionResult<List<TerrainReserve>>> Get()
        {
            var List = await AgriContext.TerrainsReserves.Select(
                s => new TerrainReserve
                {
                    TerrainId = s.TerrainId,
                    ReserveId = s.ReserveId,
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

        /*
        [HttpGet("GetTerrainReserveById")]
        public async Task<ActionResult<TerrainReserve>> GetTerrainReserveById(int Id)
        {
            TerrainReserve TerrainReserve = await AgriContext.TerrainsReserves.Select(
                    s => new TerrainReserve
                    {
                        TerrainId = s.TerrainId,
                        ReserveId = s.ReserveId
                    })
                .FirstOrDefaultAsync(s => s.TerrainReserveId == Id);

            if (TerrainReserve == null)
            {
                return NotFound();
            }
            else
            {
                return TerrainReserve;
            }
        }
        */

        [HttpPost("InsertTerrainReserve")]
        public async Task<HttpStatusCode> InsertTerrainReserve(TerrainReserve TerrainReserve)
        {
            var entity = new TerrainReserve()
            {
                TerrainId = TerrainReserve.TerrainId,
                ReserveId = TerrainReserve.ReserveId
            };

            AgriContext.TerrainsReserves.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        /*
        [HttpPut("UpdateTerrainReserve")]
        public async Task<HttpStatusCode> UpdateTerrainReserve(TerrainReserve TerrainReserve)
        {
            var entity = await AgriContext.TerrainsReserves.FirstOrDefaultAsync(s => s.TerrainReserveId == TerrainReserve.TerrainReserveId);

            entity.TerrainReserveId = TerrainReserve.TerrainReserveId;
            entity.TerrainReserveType = TerrainReserve.TerrainReserveType;
            entity.TerrainReserveNom = TerrainReserve.TerrainReserveNom;

            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        

        [HttpDelete("DeleteTerrainReserve/{Id}")]
        public async Task<HttpStatusCode> DeleteTerrainReserve(int Id)
        {
            var entity = new TerrainReserve()
            {
                TerrainReserveId = Id
            };
            AgriContext.TerrainsReserves.Attach(entity);
            AgriContext.TerrainsReserves.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        */
    }
}

