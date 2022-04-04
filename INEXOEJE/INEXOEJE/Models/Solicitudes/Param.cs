namespace INEXOEJE.Models.Solicitudes
{
    public class Param
    {
        public string placa { get; set; }   
        public string persona { get; set; }      
        public string observacion { get; set; }


    public Param()
        {
            placa = "";
            persona = "";
            observacion = "";
        }

        public Models.Revision BuscarRevisionPlaca(string Placa)
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                Models.Vehiculo auto = (from d in db.Vehiculos
                                        where d.Patente == Placa
                                        select d).FirstOrDefault();
                Models.Revision revision = null;

                if (auto != null)
                {
                    var revisiones = (from d in db.Revisions where auto.Id == d.VehiculoId select d);
                    revision = revisiones.FirstOrDefault();
                }

                return (revision == null ? new Models.Revision() : revision);

            }
        }

    }
}
