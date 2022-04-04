using System;
using System.Collections.Generic;

namespace INEXOEJE.Models
{
    public partial class TipoRevision
    {
        public TipoRevision()
        {
            Inspeccions = new HashSet<Inspeccion>();
        }

        public int Id { get; set; }
        public string? NombreTipo { get; set; }

        public virtual ICollection<Inspeccion> Inspeccions { get; set; }
    }
}
