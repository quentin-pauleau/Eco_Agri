using System;
using System.Collections.Generic;

namespace API_Agri.Entities;

public partial class StadePlante
{
    public int? StadeId { get; set; }

    public int? PlanteId { get; set; }

    public double? StadePlanteKc { get; set; }

    public virtual Plante? Plante { get; set; }

    public virtual Stade? Stade { get; set; }
}
