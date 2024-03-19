using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Agri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReserveController : ControllerBase
    {
        private readonly AgriContext AgriContext;

        public ReserveController(AgriContext AgriContext)
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
        [HttpGet("GetReserves")]
        public async Task<ActionResult<List<Reserve>>> Get()
        {
            var List = await AgriContext.Reserves.Select(
                s => new Reserve
                {
                    ReserveId = s.ReserveId,
                    ReserveMax = s.ReserveMax,
                    ReserveActuel = s.ReserveActuel,
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

        [HttpGet("GetReserveById")]
        public async Task<ActionResult<Reserve>> GetReserveById(int Id)
        {
            Reserve Reserve = await AgriContext.Reserves.Select(
                    s => new Reserve
                    {
                        ReserveId = s.ReserveId,
                        ReserveMax = s.ReserveMax,
                        ReserveActuel = s.ReserveActuel,
                    })
                .FirstOrDefaultAsync(s => s.ReserveId == Id);

            if (Reserve == null)
            {
                return NotFound();
            }
            else
            {
                return Reserve;
            }
        }

        [HttpGet("GetReservesByTerrainId")]
        public async Task<ActionResult<Reserve>> GetReservesByTerrainId(int Id)
        {
            TerrainReserve TerrainReserve = await AgriContext.TerrainsReserves.Select(
                    s => new TerrainReserve
                    {
                        ReserveId = s.ReserveId,
                    })
                .FirstOrDefaultAsync(s => s.TerrainId == Id);

            if (TerrainReserve == null)
            {
                return NotFound();
            }
            else
            {
                Reserve Reserve = await AgriContext.Reserves.Select(
                        s => new Reserve
                        {
                            ReserveId = s.ReserveId,
                            ReserveMax = s.ReserveMax,
                            ReserveActuel = s.ReserveActuel,
                        })
                    .FirstOrDefaultAsync(s => s.ReserveId == TerrainReserve.ReserveId);

                if (Reserve == null)
                {
                    return NotFound();
                }
                else
                {
                    return Reserve;
                }
            }
        }

        [HttpPost("InsertReserve")]
        public async Task<HttpStatusCode> InsertReserve(Reserve Reserve)
        {
            var entity = new Reserve()
            {
                ReserveId = Reserve.ReserveId,
                ReserveMax = Reserve.ReserveMax,
                ReserveActuel = Reserve.ReserveActuel,
            };

            AgriContext.Reserves.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateReserve")]
        public async Task<HttpStatusCode> UpdateReserve(Reserve Reserve)
        {
            var entity = await AgriContext.Reserves.FirstOrDefaultAsync(s => s.ReserveId == Reserve.ReserveId);

            entity.ReserveId = Reserve.ReserveId;
            entity.ReserveMax = Reserve.ReserveMax;
            entity.ReserveActuel = Reserve.ReserveActuel;

            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteReserve/{Id}")]
        public async Task<HttpStatusCode> DeleteReserve(int Id)
        {
            var entity = new Reserve()
            {
                ReserveId = Id
            };
            AgriContext.Reserves.Attach(entity);
            AgriContext.Reserves.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}

