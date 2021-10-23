using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApp.Models
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Poderes { get; set; }

        public string Tipo { get; set; }
        public string Imagem { get; set; }
    }
}
