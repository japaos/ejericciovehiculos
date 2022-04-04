using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INEXOEJE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisionesController : ControllerBase
    {
        [HttpGet(Name ="GETSRevisiones")]
        public ActionResult Get([FromBody] Models.Solicitudes.Param P)
        {
            string Placa = P.placa;
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                Models.Vehiculo auto = (from d in db.Vehiculos where d.Patente==Placa
                             select d ).FirstOrDefault();
                Models.Revision revision = null;

                if (auto!=null)
                {
                   var  revisiones = (from d in db.Revisions where auto.Id==d.VehiculoId select d);                   
                    revision = revisiones.FirstOrDefault();
                }

                return Ok(revision==null? new Models.Revision():revision);

            }
        }

        


        [HttpPost(Name ="Postrevisiones")]
        public ActionResult Post([FromBody] Models.Solicitudes.Param P)
        {   

            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                
                string Placa=""; string encargado=""; string observaciones="";
                if (P != null)
                {
                    Placa = P.placa;encargado = P.persona;observaciones = P.observacion;
                }
                if (!string.IsNullOrEmpty(Placa) && !string.IsNullOrEmpty(encargado) && !string.IsNullOrEmpty(observaciones))
                {
                    Models.Solicitudes.PersonaEnt p = new Models.Solicitudes.PersonaEnt();
                    Models.Persona PersonaEncargada = p.BuscarPersona(encargado);
                    if (PersonaEncargada.Id==0)
                    {
                        PersonaEncargada.Identificacion = encargado;                        
                        db.Personas.Add(PersonaEncargada);
                        db.SaveChanges();
                        PersonaEncargada = p.BuscarPersona(encargado); 

                    }
                    Models.Vehiculo vehiculorevisado = new Models.Solicitudes.VehiculoEnt().BuscarVehiculo(Placa);
                    if (vehiculorevisado.Id==0)
                    {
                        return Ok("No existe vehiculo");
                    }
                    Models.Revision Rev = new Models.Revision();
                    Rev.Observaciones = observaciones;
                    Rev.Vehiculo = vehiculorevisado;
                    Rev.Persona = PersonaEncargada;
                    Rev.VehiculoId = vehiculorevisado.Id;
                    Rev.PersonaId = PersonaEncargada.Id;
                    Rev.Aprovado = "NO";   
                    db.Revisions.Add(Rev);
                    db.SaveChanges(); 
                }
            }
            return Ok("ExitoGuardar");
        }
    }
}
