using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpootLight.Models
{
    class Globals
    {
        public static Type type;
        public static List<string> analyseSession = new List<string>();
        public static List<string> user = new List<string>();
        public static void setAnalyse(List<string> analyseSession)
        {
            Globals.analyseSession = analyseSession;
        }
        public static List<string> getAnalyse()
        {
            return analyseSession;
        }
        public static void setUser(List<string> user)
        {
            Globals.user = user;
        }
        public static List<string> getUser()
        {
            return user;
        }
    }
}
