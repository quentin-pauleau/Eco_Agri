using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPF_Agri
{
    internal class Api_request
    {
        static private Api_request _Instance; // instance de connection a l'API
        static public Api_request Instance // variable sécurisée d'accés a l'API
        {
            get
            {
                if (_Instance == null) // s'il n'y a aucune connection
                    _Instance = new Api_request(); // création d'une nouvelle connection
                return _Instance; // retourne la connection
            }
            set { _Instance = value; }
        }


        static HttpClient client;

        Api_request()
        {
            client = new HttpClient();
        }

        #region ======================= Request API Agri
        static public async Task<List<Stade>> GetListStadesFromPlante(Plante plante)
        {
            //if (plante != null && plante.PlanteId != null)
            List<Stade> stades = null;
            string link = $"https://localhost:7273/api/Stade/GetStadeByPlante?PlanteId={plante.PlanteId}";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var stadeStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                stades = serialiser.Deserialize<List<Stade>>(new JsonTextReader(new StreamReader(stadeStream)));
            }
            return stades;
        }

        static public async Task<List<Plante>> GetListPlantes()
        {
            List<Plante> plantes = null;
            string link = "https://localhost:7273/api/Plante/GetPlantes";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var planteStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                plantes = serialiser.Deserialize<List<Plante>>(new JsonTextReader(new StreamReader(planteStream)));
            }
            return plantes;
        }

        static public async Task<List<Terrain>> GetListTerrains()
        {
            List<Terrain> terrains = null;
            string link = "https://localhost:7273/api/Terrain/GetTerrains";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                terrains = serialiser.Deserialize<List<Terrain>>(new JsonTextReader(new StreamReader(terrainStream)));
            }
            return terrains;
        }

        static public async Task<List<Terrain>> GetListTerrainsByInsee(int insee)
        {
            //* get all terrain with the matching Insee
            List<Terrain> terrains = null;
            string link = $"https://localhost:7273/api/Terrain/GetTerrainByInsee?Insee={insee}";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                terrains = serialiser.Deserialize<List<Terrain>>(new JsonTextReader(new StreamReader(terrainStream)));
            }
            return terrains;
        }

        static public async Task<List<Terrain>> GetListTerrainsByName(string name)
        {
            List<Terrain> terrains = null;
            string link = $"https://localhost:7273/api/Terrain/GetTerrainByName?Name={name}";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                terrains = serialiser.Deserialize<List<Terrain>>(new JsonTextReader(new StreamReader(terrainStream)));
            }
            return terrains;
        }

        static public async Task<bool> CreateTerrain(Terrain terrain)
        {
            string link = $"{terrain}";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        static public async Task<List<Reserve>> GetListReservesByTerrainId(int terrainId)
        {
            List<Reserve> reserves = null;
            string link = $"https://localhost:7273/api/Reserve/GetReservesByTerrainId?Id={terrainId}";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                reserves = serialiser.Deserialize<List<Reserve>>(new JsonTextReader(new StreamReader(terrainStream)));
            }
            return reserves;
        }

        static public async Task<double> GetKcByIds(int terrainId, int stadeId)
        {
            double kc = 0;
            string link = $"https://localhost:7273/api/Terrain/GetAutonomieByIds?TerrainId={terrainId}&StadeId={stadeId}";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                var serialiser = new JsonSerializer();
                kc = serialiser.Deserialize<double>(new JsonTextReader(new StreamReader(terrainStream)));
            }
            return kc;
        }

        #endregion


        #region Request Api Meteo
        static public async Task<List<Meteo>> GetMeteo(string insee)
        {
            List<Meteo> meteos = null;
            string link = $"https://api.meteo-concept.com/api/forecast/daily?insee={insee}&start=0&end=6" +
                $"&token=b18e9fe2a6a5a1df9aca6464c1c07bb9670cfc62c0a093720d850683d3995da3";
            HttpResponseMessage response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {
                var terrainStream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(terrainStream, Encoding.UTF8);
                JObject meteoJson = JObject.Parse(reader.ReadToEnd());

                meteos = new List<Meteo>();
                foreach ( JObject meteo in meteoJson["forecast"] )
                {
                    meteos.Add(new Meteo()
                    {
                        MeteoId = meteo.Value<int>("day"),
                        MeteoDate = meteo.Value<string>("datetime"),
                        MeteoWeather = MeteoInfo(meteo.Value<int>("weather"))
                    });
                }
            }
            return meteos;
        }

        private static string MeteoInfo(int weatherId)
        {
            switch (weatherId)
            {
                case 0:
                    return "Soleil";
                case 1:
                    return "Peu nuageux";
                case 2:
                    return "Ciel voilé";
                case 3:
                    return "Nuageux";
                case 4:
                    return "Très nuageux";
                case 5:
                    return "Couvert";
                case 10:
                    return "Pluie faible";
                case 11:
                    return "Pluie modérée";
                case 12:
                    return "Pluie forte";
                default:
                    return "Autre";
            }
        }
        #endregion
    }
}
