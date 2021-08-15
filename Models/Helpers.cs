using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Helpers
    {
        private static readonly int tamanyoClave = 32;
        private static readonly int tamanyoVector = 16;

        // Define la palabra clave para el cifrado y
        private static readonly string Clave = "Prog Web 301XD";
        private static readonly string Vector = "Estoes una clavexd837723";

        // se convierte el vector y la key a bytes

        public static byte[] Key = UTF8Encoding.UTF8.GetBytes(Clave);
        public static byte[] IV = UTF8Encoding.UTF8.GetBytes(Vector);
        public string CifradoTexto(String txtPlano)

        {
            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);
            // se instancia el Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();
            // se establece cifrado
            MemoryStream memoryStream = new MemoryStream();
            // se crea el flujo de datos de cifrado
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateEncryptor(Key, IV),
                CryptoStreamMode.Write);
            // se obtine la información a cifrar
            byte[] txtPlanoBytes = UTF8Encoding.UTF8.GetBytes(txtPlano);
            // se cifran los datos
            cryptoStream.Write(txtPlanoBytes, 0, txtPlanoBytes.Length);
            cryptoStream.FlushFinalBlock();
            // se obtinen los datos cifrados
            byte[] cipherMessageBytes = memoryStream.ToArray();
            // se cierra todo
            memoryStream.Close();
            cryptoStream.Close();
            // Se devuelve la cadena cifrada
            return Convert.ToBase64String(cipherMessageBytes);
        }
        public string GetSHA256(string str)
        {
            string password;
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encodign = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encodign.GetBytes(str));
            for (int i = 0; i < stream.Length; ++i) sb.AppendFormat("{0:x2}", stream[i]);
            password = CifradoTexto(sb.ToString());
            return password;
        }
        public string Limpiador(string cadena)
        {
           
                return cadena;
        }
    }
}