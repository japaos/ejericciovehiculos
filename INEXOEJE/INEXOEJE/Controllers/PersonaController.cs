using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INEXOEJE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get([FromBody] Models.Solicitudes.Param P)
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {

                var persona = new Models.Solicitudes.PersonaEnt().BuscarPersona(P.persona);
                return Ok(persona);
            }
        }

        //public Models.Persona BuscarPersona(string identificacion)
        //{
        //    using (Models.EjericioContext db = new Models.EjericioContext())
        //    {
        //        var lista = (from d in db.Personas where d.Identificacion == identificacion select d).FirstOrDefault();

        //        return (lista != null ? lista : new Models.Persona());
        //    }
        //}


        [HttpPost] 
        public ActionResult Post([FromBody] Models.Solicitudes.PersonaEnt modelo )
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                Models.Persona persona = new Models.Solicitudes.PersonaEnt().BuscarPersona(modelo.identificacion);
                if (persona.Id <0)
                {
                    Models.Persona ObPersona = new Models.Persona();
                    ObPersona.Identificacion = modelo.identificacion;
                    ObPersona.Nombre = modelo.nombre;
                    ObPersona.Apellido = modelo.apellido;
                    db.Personas.Add(ObPersona);
                    db.SaveChanges();
                    return Ok("Guardado Exitoso");

                }
                else
                {
                    return Ok("Ya existe persona");
                }

                

            }
        }



    }
}
