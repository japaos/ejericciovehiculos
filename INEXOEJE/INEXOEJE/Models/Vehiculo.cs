using System;
using System.Collections.Generic;

namespace INEXOEJE.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Revisions = new HashSet<Revision>();
        }

        public int Id { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string Patente { get; set; } = null!;
        public int? Año { get; set; }
        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Revision> Revisions { get; set; }
    }
}
