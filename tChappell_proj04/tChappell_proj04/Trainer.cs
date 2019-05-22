using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tChappell_proj04
{
    class Trainer
    {
        public string tname;
        public string rname;
        public string starter;
        public string displayName
        {
            get
            {
                return tname;
            }
        }

        public Trainer(string TrainerName, string RivalName, string Starter)
        {
            this.tname = TrainerName;
            this.rname = RivalName;
            this.starter = Starter;
        }
    }
}
