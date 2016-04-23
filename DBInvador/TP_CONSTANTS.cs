using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Techphernalia.DBInvader
{
    class TP_CONSTANTS
    {
        private TP_CONSTANTS() { }
        public const string DURGESH_CHAUDHARY = "Durgesh Chaudhary";
        public const string TECHPHERNALIA = "techPhernalia";
        public const string TECHPHERNALIA_COM = "techPhernalia.com";
        public const string DB_Invader = "DB Invader : from " + TECHPHERNALIA_COM + " : by " + DURGESH_CHAUDHARY;

        public static string GetTitle()
        {
            return DB_Invader;
        }
    }
}
