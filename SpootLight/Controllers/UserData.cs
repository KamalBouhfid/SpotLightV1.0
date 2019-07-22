using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpootLight.Controllers
{
    class UserData
    {
        private int id;
        private string _login;
        private string pass;
        private string email;

        public string Pass { get => pass; set => pass = value; }
        public string Email { get => email; set => email = value; }
        public string Login { get => _login; set => _login = value; }
        public int Id { get => id; set => id = value; }
    }
}
