using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Helpers
    {
        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encodign = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encodign.GetBytes(str));
            for (int i = 0; i < stream.Length; ++i) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public string Limpiador(string cadena)
        {
           
                return cadena;
        }
    }
}