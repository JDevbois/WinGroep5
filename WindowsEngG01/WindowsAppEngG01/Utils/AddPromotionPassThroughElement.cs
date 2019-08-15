using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.Utils
{
    public class AddPromotionPassThroughElement
    {

        public enum IDENTIFIERS
        {
            EVENT = 0,
            DISCOUNTCODE = 1,
            PROMOTION = 2
        }

        public AddPromotionPassThroughElement(string id, int identifier)
        {
            this.Id = id;
            this.Identifier = identifier;
        }

        public string Id { get; set; }
        public int Identifier { get; set; }
    }
}
