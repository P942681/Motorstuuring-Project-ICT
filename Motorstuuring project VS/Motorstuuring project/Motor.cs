using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorstuuring_project
{
    internal class Motor
    {
        public Motor() 
        {
            richting = "0";
        }

        private string richting;

        public string Richting
        {
            get { return richting; }
            set { richting = value; }
        }

        public void vooruit ()
        {
            Richting = "1";
        }

        public void achteruit()
        {
            richting = "2";
        }

        public void links()
        {
            richting = "3";
        }

        public void rechts()
        {
            richting = "4";
        }       
        
        public void Stop()
        {
            richting = "0";
        }
    }
}
