using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class UserCredentials
    {
        public string username { get; private set; }
        public string password { get; private set; }
        public UtilityClass.UserTypes userType { get; private set; }

        public UserCredentials(string username, string password, UtilityClass.UserTypes userType, bool encodePassword)
        {
            this.username = username;
            this.userType = userType;

            if (encodePassword)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, password);

                    this.password = hash;
                }
            }
            else
                this.password = password;
        }

        /*
         * Pre:
         * Post: Determines whether or not the current credentials are valid
         * @returns true if the credentials are valid and false otherwise
         */
        public bool Login()
        {
            return DbInterfacePerson.Login(username, password, userType);
        }

        /*
         * Pre:
         * Post: Encodes the input string and converts the byte array to a hexidecimal string
         * @param md5Hash is the instance of MD5
         * @param input is the string to be encoded
         * @returns the original input string's encoded, hexidecimal value
         */
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            //convert input string to byte array
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            //create a stringbuilder to collect bytes and create a string
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
            
            //loop through each byte and format as hex string
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }
    }
}