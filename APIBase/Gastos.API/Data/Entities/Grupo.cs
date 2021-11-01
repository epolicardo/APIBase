using System.Collections.Generic;

namespace Data.Entities
{
    public class Grupo : EntityBase
    {
        public string Nombre { get; set; }

        //UserGroupId
        public string UGI { get; set; }
        public List<Usuario> Integrantes { get; set; }


    }
}
