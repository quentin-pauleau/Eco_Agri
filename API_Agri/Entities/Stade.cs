using System;
using System.Collections.Generic;

namespace API_Agri.Entities;

public partial class Stade
{
    public int StadeId { get; set; }

    public string? StadeDescription { get; set; }

    public int? StadePlanteId { get; set; }

    public virtual Plante? Plante { get; set; }

    public double? StadeKc { get; set;}
}
