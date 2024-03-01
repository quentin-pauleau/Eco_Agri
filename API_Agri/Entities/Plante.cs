using System;
using System.Collections.Generic;

namespace API_Agri.Entities;

public partial class Plante
{
    public int PlanteId { get; set; }

    public string? PlanteType { get; set; }

    public string? PlanteNom { get; set; }
}
