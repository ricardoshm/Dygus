using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DYGUS_SAT_BASEAPP
{
    public class Passwords
    {
        public static string CreatePassword(int length)
        {
            Random rnd = new Random();
            StringBuilder blr = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                switch (rnd.Next(2)) // which kind of character to add ?
                {
                    //					case 1 : // adds an uppercase character
                    //						blr.Append((char) ('A' + rnd.Next(26)));
                    //						break;
                    case 1: // adds an lowercase char
                        blr.Append((char)('a' + rnd.Next(26)));
                        break;
                    case 0: // adds a numeric char
                        blr.Append((char)('0' + rnd.Next(10)));
                        break;
                }
            }
            return blr.ToString();
        }

        public string Hash(string psValue)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();

            byte[] string_bytes = System.Text.UnicodeEncoding.Unicode.GetBytes(psValue);
            byte[] md5_bytes = sh.ComputeHash(string_bytes);

            return BytesToString(md5_bytes);
        }

        private static string BytesToString(byte[] paBytes)
        {
            string ret_val = "";

            foreach (byte temp_byte in paBytes)
            {
                ret_val += temp_byte.ToString("X");
            }

            return ret_val;
        }

        public string Encrypt(string data, string key)
        {
            return CryptStep2(CryptStep1(data), key);
        }

        private static string CryptStep2(string data, string k)
        {
            string result = "";
            int pt = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int nd = Convert.ToInt32(data.Substring(i, 1));
                int nk = Convert.ToInt32(k.Substring(pt, 1));
                result = result + ((nd ^ nk)).ToString("X");
                pt++;
                if (pt >= k.Length)
                {
                    pt = 0;
                }
            }
            return result;
        }

        private static string CryptStep1(string data)
        {
            string result = "";
            string temp = "";
            for (int i = 0; i < data.Length; i++)
            {
                temp = "000" + Convert.ToInt32(data.Substring(i, 1)[0]);
                result = result + temp.Substring(temp.Length - 3, 3);
            }
            return result;
        }

        public string Decrypt(string data, string key)
        {
            return DecryptStep1(DecryptStep2(data, key));
        }

        private static string DecryptStep1(string data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i += 3)
            {
                result = result + Convert.ToChar(Convert.ToInt32(data.Substring(i, 3))).ToString();
            }
            return result;
        }

        private static string DecryptStep2(string data, string k)
        {
            string result = "";
            int pt = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int nd = Convert.ToInt32(Dec(data.Substring(i, 1)));
                int nk = Convert.ToInt32(k.Substring(pt, 1));
                result = result + ((nd ^ nk)).ToString();
                pt++;
                if (pt >= k.Length)
                {
                    pt = 0;
                }
            }
            return result;
        }

        private static int Dec(string data)
        {
            try
            {
                return Convert.ToInt32(data);
            }
            catch
            {
                return ((Convert.ToChar(data.ToUpper()) - 'A') + 10);
            }
        }
    }
}