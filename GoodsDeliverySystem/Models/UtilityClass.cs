using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace GoodsDeliverySystem.Models
{
    public partial class UtilityClass
    {
        public enum UserTypes { Customer, Staff, Manager };
        public enum DriverStatus { Available, InTransit, Inactive  };
        public enum TruckStatus { Idle = 1, Loading = 2, InTransit = 3, Inactive = 4 };
        public enum ShippingSpeed{ Normal, Express, Urgent };
        public const string START_CITY = "La Crosse, WI";

        /*
         * Pre:
         * Post: An email with the input subject and message is sent to the input email address
         * @returns true if the email is successfully sent and false otherwise
         */
        public static bool SendEmail(string to, string head, string content)
        {
            bool result = true;

            try
            {
                MailMessage message = new MailMessage("goodsdeliverysystem@gmail.com", to);
                message.Subject = head;
                message.Body = content;
                SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587);
                mailer.Credentials = new NetworkCredential("goodsdeliverysystem@gmail.com", "LaCrosse");
                mailer.EnableSsl = true;
                mailer.Send(message);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /*
         * Pre:  
         * Post: the system generates a random password that length is 10
         * @returns random password
         */
        public static string MakeRandomPassWord()
        {

            string tmpstr = "";
            string pwdchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //the length of random password
            int pwdlen = 10;
            int RandNum;

            Random rnd = new Random();

            for (int i = 0; i < pwdlen; i++)
            {
                RandNum = rnd.Next(pwdchars.Length);

                tmpstr += pwdchars[RandNum];
            }

            return tmpstr;
        }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();

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