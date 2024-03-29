using System;
using System.Collections.Generic;
using WPF_Agri;

namespace WPF_Agri
{
    public partial class Terrain
    {
        public int TerrainId { get; set; }

        public string TerrainInsee { get; set; }

        public string TerrainNom { get; set; }

        public int? TerrainSurface { get; set; }

        public int? TerrainPlanteId { get; set; }

        public virtual Plante Plante { get; set; }

        public override string ToString()
        {
            if (TerrainSurface != null)
            {
                if (Plante != null)
                    return $"{TerrainInsee} - {TerrainNom} : {TerrainSurface}m² de {Plante}";
                else
                    return $"{TerrainInsee} - {TerrainNom} : {TerrainSurface}m²";
            }
            return $"{TerrainNom}";
        }

        public string TerrainInfo()
        {
            if (TerrainSurface != null)
            {
                if (Plante != null)
                    return $"{TerrainSurface}m² de {Plante}";
                else
                    return $"{TerrainSurface}m²";
            }
            return "";
        }
    }
}