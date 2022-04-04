using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INEXOEJE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        /// <summary>
        /// Retorna el carro en funcion de la placa
        /// </summary>
        /// <param name="Placa">Placa del vahiculo</param>
        /// <returns></returns>
        [HttpGet(Name ="GetVehiculo")]
        public ActionResult Get([FromBody] Models.Solicitudes.Param P )
        {
            string Placa = P.placa;
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                var vehiculos = (from d in db.Vehiculos
                             select d);                
                Models.Vehiculo auto = vehiculos.Where(v=>v.Patente==Placa).FirstOrDefault();
                return Ok(auto==null? new Models.Vehiculo():auto);

            }
        }

        [HttpPost(Name ="POSTVehiculo")]
        public ActionResult Post([FromBody] Models.Solicitudes.VehiculoEnt vehiculo)
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                if (vehiculo != null)
                {
                    Models.Persona Propietario = db.Personas.Find(vehiculo.PersonaId);
                    Models.Vehiculo VehiculoEditar = null;
                    if (Propietario == null)
                    {
                        Models.Solicitudes.PersonaEnt P = new Models.Solicitudes.PersonaEnt();
                        P.guardar(Propietario);
                    }
                    if (!string.IsNullOrEmpty(vehiculo.Patente))
                    {
                        VehiculoEditar = new Models.Solicitudes.VehiculoEnt().BuscarVehiculo(vehiculo.Patente);
                        VehiculoEditar.Modelo = vehiculo.Modelo;
                        VehiculoEditar.Patente = vehiculo.Patente;
                        VehiculoEditar.Año = vehiculo.Año;
                        VehiculoEditar.Marca = vehiculo.Marca;
                        VehiculoEditar.PersonaId = vehiculo.PersonaId;
                    }
                    else
                    {
                        VehiculoEditar.Modelo = vehiculo.Modelo;
                        VehiculoEditar.Patente = vehiculo.Patente;
                        VehiculoEditar.Año = vehiculo.Año;
                        VehiculoEditar.Marca = vehiculo.Marca;
                        VehiculoEditar.PersonaId = vehiculo.PersonaId;
                        db.Vehiculos.Add(VehiculoEditar);
                    }
                    db.SaveChanges();

                }
            }
            return Ok("");

        }


        [HttpDelete(Name ="BORRARVehiculo")]
        public ActionResult Delete([FromBody] int IdVehiculo)
        {
            using(Models.EjericioContext db=new Models.EjericioContext())
            {
                Models.Vehiculo VehiculoEditar = db.Vehiculos.Find(IdVehiculo);
                if (VehiculoEditar != null)
                {
                    db.Vehiculos.Remove(VehiculoEditar);
                    db.SaveChanges();
                }
                    
            }
            return Ok("");
        }
    }
}
