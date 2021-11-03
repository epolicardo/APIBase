using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Usuario : IdentityUser
    {


        public string  Password { get; set; }
        public Persona Persona { get; set; }

        public List<Grupo> Grupos { get; set; }
      
        public string Avatar { get; set; }


    }
}
