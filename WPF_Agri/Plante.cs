using System;
using System.Collections.Generic;

namespace WPF_Agri
{
    public partial class Plante
    {
        public int PlanteId { get; set; }

        public string PlanteType { get; set; }

        public string PlanteNom { get; set; }

        public override string ToString()
        {
            return $"{PlanteNom}";
        }
    }
}