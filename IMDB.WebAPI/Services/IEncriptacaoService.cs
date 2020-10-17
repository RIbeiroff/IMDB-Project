namespace IMDB.WebAPI.Services
{
    public interface IEncriptacaoService
    {
        /// <summary>
        /// Criar chave para hash da senha
        /// </summary>
        /// <param name="tamanho">Tamanho da chave</param>
        /// <returns>Chave</returns>
        string CriarChave(int tamanho);
    
        /// <summary>
        /// Criar hash da senha com base numa chave
        /// </summary>
        /// <param name="senha">Senha</param>
        /// <param name="chave">Chave</param>
        /// <returns>Senha Criptografada</returns>
        string CriarHashSenha(string senha, string chave);
    }
}