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
    }
}