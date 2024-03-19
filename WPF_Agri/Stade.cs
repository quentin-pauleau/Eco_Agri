using System;
using System.Collections.Generic;

namespace WPF_Agri
{

    public partial class Stade
{
        public int StadeId { get; set; }

        public string StadeDescription { get; set; }

        public int? StadePlanteId { get; set; }

        public virtual Plante Plante { get; set; }

        public double? StadeKc { get; set;}

        public override string ToString()
        {
            return $"{StadeDescription}";
        }
    }
}