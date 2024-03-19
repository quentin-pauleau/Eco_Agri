using System;
using System.Collections.Generic;

namespace API_Agri.Entities;

public partial class Terrain
{
    public int TerrainId { get; set; }

    public string? TerrainInsee { get; set; }

    public string? TerrainNom { get; set; }

    public int? TerrainSurface { get; set; }

    public int? TerrainPlanteId { get; set; }

    public virtual Plante? Plante { get; set; }
}
