namespace INEXOEJE.Models.Solicitudes
{
    public class PersonaEnt
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string identificacion { get; set; }

        public int id { get; set; }

        public PersonaEnt()
        {
            this.nombre = "";
            this.apellido = "";
            this.identificacion = "";
            this.id = -1;
        }

        /// <summary>
        /// Cambia a formato bd de persona
        /// </summary>
        /// <returns></returns>
        private Models.Persona Conver() {
            Models.Persona nuevo = new Models.Persona();
            if (this != null) { 
                nuevo.Nombre = this.nombre;
                nuevo.Apellido = this.apellido; 
                nuevo.Identificacion = this.identificacion;
                if (this.id > 0) 
                    nuevo.Id = this.id;
            }
            return nuevo;
        }



        public void guardar( Models.Persona P=null)
        {
            try
            {
                using (Models.EjericioContext db = new EjericioContext())
                {
                    Models.Persona ObPersona = null;

                    if (P != null)
                    {
                        ObPersona = P;
                    }
                    else
                    {
                        ObPersona=Conver();
                    }
                    Models.Persona PersonaEditar = db.Personas.Find(ObPersona.Id);
                    if (PersonaEditar != null)
                    {
                        PersonaEditar = ObPersona;
                        db.Entry(PersonaEditar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        db.Personas.Add(ObPersona);
                        db.SaveChanges();

                    }

                }

            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
           
        }

        public Models.Persona BuscarPersona(string identificacion)
        {
            using (Models.EjericioContext db = new Models.EjericioContext())
            {
                var lista = (from d in db.Personas where d.Identificacion == identificacion select d).FirstOrDefault();

                return (lista != null ? lista : new Models.Persona());
            }
        }

        //public void borrar()
        //{
        //    try
        //    {
        //        using (Models.EjericioContext db = new EjericioContext())
        //        {

        //            Models.Persona ObPersona = Conver();
        //            Models.Persona PersonaBorrar = db.Personas.Find(ObPersona.Id);
        //            if (PersonaBorrar != null)
        //            {
        //                db.Personas.Remove(PersonaBorrar);
        //                db.SaveChanges();
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    { Console.WriteLine(ex.ToString()); }

        //}
    }
}  