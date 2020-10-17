using System;
using System.Security.Cryptography;
using System.Text;

namespace IMDB.WebAPI.Services
{
    public class EncriptacaoService : IEncriptacaoService
    {
        public string CriarChave(int tamanho){
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[tamanho];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string CriarHashSenha(string senha, string chave){
            if (!string.IsNullOrEmpty(senha) && !string.IsNullOrEmpty(chave)){
                var algoritmoHash = HashAlgorithm.Create("SHA1");
                if (algoritmoHash == null)
                    throw new ArgumentException("Erro ao processar hash", "HashError");
                var hashByteArray = algoritmoHash.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(senha, chave)));
                return BitConverter.ToString(hashByteArray).Replace("-", "");
            }
            return null;
        }
    }
}