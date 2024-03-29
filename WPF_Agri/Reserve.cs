using System;
using System.Collections.Generic;

namespace WPF_Agri
{
    public partial class Reserve
    {
        public int ReserveId { get; set; }

        public double? ReserveMax { get; set; }

        public double? ReserveActuel { get; set; }

        public override string ToString()
        {
            return $"Reserve n°{ReserveId} - "+Eau();
        }

        public string Eau()
        {
            return (ReserveActuel.ToString() != "" ? ReserveActuel.ToString() : "?")
                + " / " + (ReserveMax.ToString() != "" ? ReserveMax.ToString() : "?");
        }
    }
}