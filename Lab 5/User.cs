using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    public class User
    {
        public static List<User> users = new List<User>();
        public static List<User> admins = new List<User>();
        public string Name { get; set; }
        public string MailAddress { get; set; }

        public User(string name, string mailAddress)
        {
            Name = name;
            MailAddress = mailAddress;
        }
    }
}
