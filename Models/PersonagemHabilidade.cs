using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RpgApi.Models

{
    public class PersonagemHabilidade
    {
        public int PersonagemId{ get; set; } 

        public Personagem Personagems { get; set; }

        public int HabilidadeId { get; set; }

        public Habilidade Habilidades { get; set; }

        //public List<PersonagemHabilidade> PersonagemHabilidades { get; set; }

    }
}