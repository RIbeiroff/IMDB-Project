namespace IMDB.WebAPI.Models.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string ChaveSenha { get; set; }
        public string Senha { get; set; }
        public bool Deletado { get; set; }
        public string Regra { get; set; }

    }
    
}