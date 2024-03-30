using System;
using System.Collections.Generic;

namespace API_Agri.Entities;

public partial class TerrainReserve
{
    public int TerrainReserveId { get; set; }
    public int? TerrainId { get; set; }

    public int? ReserveId { get; set; }

    public virtual Terrain? Terrain { get; set; }

    public virtual Reserve? Reserve { get; set; }
}
