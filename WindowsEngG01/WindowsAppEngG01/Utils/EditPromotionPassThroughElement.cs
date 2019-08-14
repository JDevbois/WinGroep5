using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.Utils
{
    public class EditPromotionPassThroughElement
    {
        private Promotion paramPromotion;

        public String CompanyId { get; set; }
        public String PromotionId
        {
            get { return paramPromotion.Id; }
        }
        public int Identifier { get; set; }

        public EditPromotionPassThroughElement(Promotion paramPromotion, Company paramCompany, int paramIdentifier)
        {
            this.paramPromotion = paramPromotion;
            this.CompanyId = paramCompany.Id;
            this.Identifier = paramIdentifier;
        }
    }
}
