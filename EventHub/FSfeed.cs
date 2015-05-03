using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub
{




    public class Rootobject2
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string location { get; set; }
        public string city { get; set; }
        public int id { get; set; }
    }

    public class uporabnik
    {
        public static string user;
        public static int user_id;
    }

    public class ajdi
    {
        public static int aj;
    }







    public class Rootobject
    {
        public Class2[] Property1 { get; set; }
    }

    public class Class2
    {
        public string username { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string id { get; set; }
    }








}
