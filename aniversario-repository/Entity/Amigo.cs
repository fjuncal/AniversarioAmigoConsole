using System;

namespace aniversario_repository.Entity
{
    public class Amigo
    {
        public Amigo() { }

        public int IdAmigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataAniversario { get; set; }
    }
}