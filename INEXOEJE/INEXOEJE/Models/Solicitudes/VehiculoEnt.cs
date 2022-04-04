namespace INEXOEJE.Models.Solicitudes
{
    public class VehiculoEnt
    {
        public int Id { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string Patente { get; set; } = null!;
        public int? Año { get; set; }
        public int PersonaId { get; set; }

        public VehiculoEnt()
        {
            this.Id= 0;
            this.Modelo= "";
            this.Marca= "";
            this.Año = 0;
            this.Patente = "";
            this.PersonaId = 0;
        }

    public Models.Vehiculo BuscarVehiculo(string placa)
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                var lista = (from d in db.Vehiculos where d.Patente == placa select d).FirstOrDefault();

                return (lista != null ? lista : new Models.Vehiculo());
            }
        }
    }


    
}
