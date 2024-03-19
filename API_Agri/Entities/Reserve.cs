using System;
using System.Collections.Generic;

namespace API_Agri.Entities;

public partial class Reserve
{
    public int ReserveId { get; set; }

    public double? ReserveMax { get; set; }

    public double? ReserveActuel { get; set; }
}
