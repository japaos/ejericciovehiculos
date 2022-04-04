using System;
using System.Collections.Generic;

namespace INEXOEJE.Models
{
    public partial class Revision
    {
        public Revision()
        {
            Inspeccions = new HashSet<Inspeccion>();
        }

        public int Id { get; set; }
        public string? Observaciones { get; set; }
        public string? Aprovado { get; set; }
        public int VehiculoId { get; set; }
        public int PersonaId { get; set; }
        public DateTime? FechaInspeccion { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual Vehiculo Vehiculo { get; set; } = null!;
        public virtual ICollection<Inspeccion> Inspeccions { get; set; }
    }
}
