using System;
using System.Collections.Generic;

namespace INEXOEJE.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Inspeccions = new HashSet<Inspeccion>();
            Revisions = new HashSet<Revision>();
            Vehiculos = new HashSet<Vehiculo>();
            Id = 0;
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Identificacion { get; set; }

        public virtual ICollection<Inspeccion> Inspeccions { get; set; }
        public virtual ICollection<Revision> Revisions { get; set; }
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
