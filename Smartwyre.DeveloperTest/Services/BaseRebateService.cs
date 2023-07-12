using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public abstract class BaseRebateService : IRebateService
    {
        private IncentiveType _incentiveType;
        private IRebateService _nextChain;

        protected BaseRebateService()
        {
        }
        protected BaseRebateService(IncentiveType incentiveType)
        {
            _incentiveType = incentiveType;
        }
        protected BaseRebateService(IRebateService nextChain, IncentiveType incentiveType) : this(incentiveType)
        {
            if (nextChain == null)
            {
                throw new ArgumentNullException("nextChain");
            }

            _nextChain = nextChain;
        }
        protected CalculateRebateResult result { get; set; }

        public abstract CalculateRebateResult Calculate(CalculateRebateRequest request);
     
      
    }
}
