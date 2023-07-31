using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PSR_Add_Document.Models.GlobalClass
{
    public static class UrlEncryption
    {

        public static string Decrypt(string cypherString)
        {
            try
            {
                if (cypherString == "''" || cypherString == null)
                    return cypherString;
                return Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static int? DecryptIneger(string cypherString)
        {
            if (cypherString == "''" || cypherString == null)
                return null;

            string enc = Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));

            int id = Convert.ToInt32(enc);
            return id;
        }

        public static string Encrypt(string ToEncrypt)
        {
            if (ToEncrypt == null)
                return ToEncrypt;
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
        }

        public static string EncryptInteger(int? ToEncrypt)
        {
            string enc = ToEncrypt.ToString();
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(enc));
        }
    }
}