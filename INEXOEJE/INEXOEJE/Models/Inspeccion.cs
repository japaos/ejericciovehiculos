using System;
using System.Collections.Generic;

namespace INEXOEJE.Models
{
    public partial class Inspeccion
    {
        public int Id { get; set; }
        public int? RevisionId { get; set; }
        public int TipoInspeccion { get; set; }
        public string? Observacion { get; set; }
        public bool? Estado { get; set; }
        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual Revision? Revision { get; set; }
        public virtual TipoRevision TipoInspeccionNavigation { get; set; } = null!;
    }
}
