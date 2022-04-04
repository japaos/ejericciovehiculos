using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INEXOEJE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionesController : ControllerBase
    {
        [HttpGet(Name ="GetInspeccion")]
        public ActionResult Get([FromBody] Models.Solicitudes.Param P)
        {
            using (Models.EjericioContext db =new Models.EjericioContext())
            {
                string placa = P.placa;
                List<Models.Inspeccion> inspecciones=null;
                if (!string.IsNullOrEmpty(placa))
                {
                    var auto = (from d in db.Vehiculos where placa == d.Patente select d).FirstOrDefault();
                    if (auto != null)
                    {
                        var revision = (from d in db.Revisions where d.VehiculoId == auto.Id select d).FirstOrDefault();
                        if (revision!=null)
                        {
                            inspecciones= (from inspeccion in db.Inspeccions where inspeccion.RevisionId==revision.Id select inspeccion).ToList();
                            return Ok(inspecciones);
                        }
                    }
                }
                return Ok(inspecciones);

            }
        }

        [HttpPost(Name ="POSTInsspecciones")]
        public ActionResult Post([FromBody] Models.Solicitudes.Param p)
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                string Placa = p.placa;
                int idTipo = 0;
                List<Models.Inspeccion> inspecciones =null;
                if (!string.IsNullOrEmpty(Placa) && idTipo>0)
                {
                    Models.Revision revision = new Models.Revision();//new RevisionesController().BuscarRevisionPlaca(Placa);
                    if (revision != null)
                    {
                         inspecciones = (from d in db.Inspeccions where d.RevisionId == revision.Id select d).ToList<Models.Inspeccion>();
                    }
                }
                return Ok(inspecciones);

            }
        }
    }

}
