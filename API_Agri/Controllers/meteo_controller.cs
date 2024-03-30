using API_Agri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace API_Agri.Controllers
{
    /*
    [ApiController]
    [Route("api/[controller]")]
    public class MeteoController : ControllerBase
    {
        private readonly AgriContext AgriContext;
        private readonly HttpClient client;
        public MeteoController(AgriContext AgriContext)
        {
            this.AgriContext = AgriContext;
            client = new HttpClient();
        }

        /// <summary>
        /// Définition du Web Service
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client a retourné</param>   
        /// <response code="200">client sélectionné</response>
        /// <response code="404">client introuvable pour l'id spécifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("GetMeteos")]
        public async Task<ActionResult<List<Root>>> Get()
        {
            var List = await AgriContext.Meteos.Select(
                s => new Meteo
                {
                    MeteoId = s.MeteoId,
                    MeteoInsee = s.MeteoInsee,
                    MeteoNom = s.MeteoNom,
                    MeteoSurface = s.MeteoSurface,
                    MeteoPlanteId= s.MeteoPlanteId
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in List)
                    if (item.MeteoPlanteId != null)
                    {
                        item.Plante = await AgriContext.Plantes.Select(
                                s => new Plante
                                {
                                    PlanteId = s.PlanteId,
                                    PlanteType = s.PlanteType,
                                    PlanteNom = s.PlanteNom,
                                })
                            .FirstOrDefaultAsync(s => s.PlanteId == item.MeteoPlanteId);
                    }

                return List;
            }
        }

        [HttpGet("GetMeteoByDay")]
        public async Task<ActionResult<Meteo>> GetMeteoById(int Day)
        {
            string link = $"https://api.meteo-concept.com/api/forecast/daily?insee=35238&" +
                $"start=0&end=6&token=b18e9fe2a6a5a1df9aca6464c1c07bb9670cfc62c0a093720d850683d3995da3";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string jsonString = reader.ReadToEnd();
                Jobject cityJson = new JsonArray() jsonString;

                meteos = serialiser.Deserialize<List<Meteo>>(new JsonTextReader(new StreamReader(terrainStream)));
            }

                Meteo? Meteo = await (
                    s => new Meteo
                    {
                        MeteoDate = s.MeteoInsee,
                        MeteoWeather = s.MeteoNom,
                    })
                .FirstOrDefaultAsync(s => s.MeteoId == Id);

            if (Meteo == null)
            {
                return NotFound();
            }
            else
            {

                return Meteo;
            }
        }

        [HttpGet("GetMeteoByInsee")]
        public async Task<ActionResult<Meteo>> GetMeteoByInsee(string Insee)
        {
            Meteo? Meteo = await AgriContext.Meteos.Select(
                s => new Meteo
                    {
                        MeteoId = s.MeteoId,
                        MeteoInsee = s.MeteoInsee,
                        MeteoNom = s.MeteoNom,
                        MeteoSurface = s.MeteoSurface,
                        MeteoPlanteId = s.MeteoPlanteId
                    })
                .FirstOrDefaultAsync(s => s.MeteoInsee == Insee);

            if (Meteo == null)
            {
                return NotFound();
            }
            else
            {
                if (Meteo.MeteoPlanteId != null)
                {
                    Meteo.Plante = await AgriContext.Plantes.Select(
                        s => new Plante
                        {
                            PlanteId = s.PlanteId,
                            PlanteType = s.PlanteType,
                            PlanteNom = s.PlanteNom,
                        })
                        .FirstOrDefaultAsync(s => s.PlanteId == Meteo.MeteoPlanteId);
                }

                return Meteo;
            }
        }

        [HttpGet("GetMeteoByPlante")]
        public async Task<ActionResult<List<Meteo>>> GetMeteoByPlante(int PlanteId)
        {
            var List = await AgriContext.Meteos.Select(
                s => new Meteo
                    {
                        MeteoId = s.MeteoId,
                        MeteoInsee = s.MeteoInsee,
                        MeteoNom = s.MeteoNom,
                        MeteoSurface = s.MeteoSurface,
                        MeteoPlanteId = s.MeteoPlanteId
                    }
                ).Where(s => s.MeteoPlanteId == PlanteId).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in List)
                    if (item.MeteoPlanteId != null)
                    {
                        item.Plante = await AgriContext.Plantes.Select(
                            s => new Plante
                            {
                                PlanteId = s.PlanteId,
                                PlanteType = s.PlanteType,
                                PlanteNom = s.PlanteNom,
                            })
                        .FirstOrDefaultAsync(s => s.PlanteId == item.MeteoPlanteId);
                    }

                return List;
            }
        }

        [HttpGet("GetAutonomieByData")]
        public async Task<ActionResult<double>> GetAutonomieByData(double kc, double surface, double volume)
        {
            return volume / (kc * surface);
        }

        [HttpGet("GetAutonomieByIds")]
        public async Task<ActionResult<double?>> GetAutonomieByIds(int MeteoId, int StadeId)
        {
            Dictionary<string, Double?> Autonomie = new Dictionary<string, Double?>();
            double? kc = null;
            double? volume = null;
            double? surface = null;

            var terrain = await AgriContext.Meteos.Select(
                s => new Meteo
                {
                    MeteoId = s.MeteoId,
                    MeteoSurface = s.MeteoSurface,
                    MeteoPlanteId = s.MeteoPlanteId
                })
                .FirstOrDefaultAsync(s => s.MeteoId == MeteoId);

            if (terrain != null)
            {
                surface = terrain.MeteoSurface;

                Stade? stade = await AgriContext.Stades.Select(
                    s => new Stade
                    {
                        StadeKc = s.StadeKc
                    })
                    .FirstOrDefaultAsync(s => s.StadeKc == StadeId);

                kc = stade != null ? stade.StadeKc : null;


                List<MeteoReserve> terrainsReserves = await AgriContext.MeteosReserves.Select(
                    s => new MeteoReserve
                    {
                        MeteoId = s.MeteoId,
                        ReserveId = s.ReserveId
                    })
                    .Where(s => s.MeteoId == terrain.MeteoId).ToListAsync();

                List<Reserve> reserves = new List<Reserve>();
                foreach (var terrainReserve in terrainsReserves)
                {
                    reserves.AddRange(await AgriContext.Reserves.Select(
                        s => new Reserve
                        {
                            ReserveActuel = s.ReserveActuel
                        })
                        .Where(s => s.ReserveId == terrainReserve.ReserveId).ToListAsync());
                    
                }

                if (reserves.Any())
                {
                    volume = 0;
                    foreach (var reserve in reserves)
                        volume += reserve.ReserveActuel;
                }
            }

            if (kc != null && volume != null && surface != null)
            {
                return volume / (kc * surface);
            }
            return NotFound();
        }

        [HttpPost("InsertMeteo")]
        public async Task<HttpStatusCode> InsertMeteo(Meteo Meteo)
        {
            var entity = new Meteo()
            {
                MeteoId = Meteo.MeteoId,
                MeteoInsee = Meteo.MeteoInsee,
                MeteoNom = Meteo.MeteoNom,
                MeteoSurface = Meteo.MeteoSurface,
                MeteoPlanteId = Meteo.MeteoPlanteId
            };

            AgriContext.Meteos.Add(entity);
            await AgriContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateMeteo")]
        public async Task<HttpStatusCode> UpdateMeteo(Meteo Meteo)
        {
            var entity = await AgriContext.Meteos.FirstOrDefaultAsync(s => s.MeteoId == Meteo.MeteoId);
            entity.MeteoId = Meteo.MeteoId;
            entity.MeteoInsee = Meteo.MeteoInsee;
            entity.MeteoNom = Meteo.MeteoNom;
            entity.MeteoSurface = Meteo.MeteoSurface;
            entity.MeteoPlanteId = Meteo.MeteoPlanteId;

            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteMeteo/{Id}")]
        public async Task<HttpStatusCode> DeleteMeteo(int Id)
        {
            var entity = new Meteo()
            {
                MeteoId = Id
            };
            AgriContext.Meteos.Attach(entity);
            AgriContext.Meteos.Remove(entity);
            await AgriContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
    */
}