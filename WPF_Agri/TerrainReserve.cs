using System;
using System.Collections.Generic;

namespace WPF_Agri
{
    public partial class TerrainReserve
    {
        public int? TerrainId { get; set; }

        public int? ReserveId { get; set; }

        public virtual Terrain Terrain { get; set; }

        public virtual Reserve Reserve { get; set; }
    }
}