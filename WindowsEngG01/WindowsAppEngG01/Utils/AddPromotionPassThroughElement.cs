using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.Utils
{
    public class AddPromotionPassThroughElement
    {
        private string id;
        private int identifier;

        public enum IDENTIFIERS
        {
            EVENT = 0,
            DISCOUNTCODE = 1,
            PROMOTION = 2
        }

        public AddPromotionPassThroughElement(string id, int identifier)
        {
            this.id = id;
            this.identifier = identifier;
        }

        public string Id { get => id; set => id = value; }
        public int Identifier { get => identifier; set => identifier = value; }
    }
}
