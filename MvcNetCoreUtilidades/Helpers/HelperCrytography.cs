using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCrytography
    {
        //Creamos un string para el salt
        public static string Salt { get; set; }
        //Metodo para generar un salt aleatorio
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 0; i <= 40; i++)
            {
                //Generamos un aleatorio
                int num = random.Next(1, 255);
                //Convertimos el numero a char y lo añadimos al salt
                char letra = Convert.ToChar(num);
                salt += letra;
            }
            return salt;
        }
        //CRREAMOS UN METODO EFICIENTE PARA EL CIFRADO
        public static string CifrarContenido(string contenido, bool comparar)
        {
            if (comparar == false)
            {
                //NO queremos comprar asi que creamos un nuevo salt
                Salt = GenerateSalt();
            }
            //REalizamos el cifrado
            string contenidoSalt = contenido + Salt;
            SHA512 managed = SHA512.Create();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] salida;
            salida = encoding.GetBytes(contenidoSalt);
            //Realizar N interacciones sobre el propio cifrado
            for (int i = 0; i < 19; i++)
            {
                //Cifrado sobre cifrado
                salida = managed.ComputeHash(salida);
            }
            //Debemos liberar la memoria
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;

        }
        //Creamos los metodos de tipo static
        //Simplemente devolvemos un texto cifrado simple
        public static string EncriptarTextoBasico(string contenido)
        {
            //El cifrado se realiza a nivel de bytes
            //Debemos convertir el texto de entrada a byte[]
            byte[] entrada;
            //Despues de cifrar los bytes, nos dara una salida de byte[]
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            //Necesitamos un objeto de tipo encriptacion
            SHA1 manage = SHA1.Create();
            //Convertimos el texto de entrada a byte[]
            entrada = encoding.GetBytes(contenido);
            //Los Objetos de cifrado tienen un metodo llamado ComputeHash, que recibe un byte[] y devuelve un byte[] cifrado
            salida = manage.ComputeHash(entrada);
            //Convertimos los bytes a texto
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
