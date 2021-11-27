using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebToken.Services
{
    public class EncryptedSHA256
    {
        // Esta Clase se hace public y static para no instanciar objetos.
        public static string SHA256Encrypt(string data)
        {
            // Creamos una variable de tipo string (Clean Code) que guardará nuestro stringcodificado.
            string encripted = string.Empty;
            // Creamos un objeto md5 para obtener un hash
            using (SHA256 SHA256Handler = SHA256.Create())
            {
                // Codificamos la data a un array de bytes[]
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                // Creamos un hash usando el objeto md5Handler y le mandamos los bytes[]
                var hash = SHA256Handler.ComputeHash(bytes);

                // Instanciamos un objeto de la clase StringBuilder() 
                // nos permite agregar ir agregando varios valores a una fila o cola de elementos
                StringBuilder strBuilder = new StringBuilder();

                // Recorremos el hash para convertirlo en hexadecimal 
                foreach (byte b in hash)
                {
                    // Chance it into 2 hexadecimal digits
                    // for each byte
                    strBuilder.Append(b.ToString("x2"));
                }

                // Lo guardamos como string
                encripted = strBuilder.ToString();
            }
            return encripted;
        }

    }
}