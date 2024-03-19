using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        static public async Task<List<Stade>> GetListStadesFromPlante(Plante plante)
        {
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
    }
}
