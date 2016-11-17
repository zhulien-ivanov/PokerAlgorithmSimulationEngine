using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerEngine.Models.Helpers
{
    public class PlayerInformation
    {
        private string name;
        private decimal money;

        public PlayerInformation(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
        }

        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }

        public decimal Money
        {
            get { return this.money; }
            internal set { this.money = value; }
        }
    }
}
