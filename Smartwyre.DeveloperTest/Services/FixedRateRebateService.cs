using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedRateRebateService : IRebateService
    {
        private readonly IProductDataStore _productDataStore;
        private readonly IRebateDataStore _rebateDataStore;
        public FixedRateRebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore)
        {
            _productDataStore = productDataStore;
            _rebateDataStore = rebateDataStore;
        }
 
        public IRebateService? Next { get; set; }
        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            var result = new CalculateRebateResult();

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            Product product = _productDataStore.GetProduct(request.ProductIdentifier);

            if (rebate != null)
                rebate.Incentive = request.IncentiveType; // taking incentive type from CalculateRebateRequest object  because database is not developed yet

            if (rebate == null)
            {
                result.Success = false;
            }
            else if (rebate.Incentive == IncentiveType.FixedRateRebate)
            {
                var rebateAmount = 0m;
                if (product == null)
                {
                    result.Success = false;
                }
                else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
                {
                    result.Success = false;
                }
                else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
                {
                    result.Success = false;
                }
                else
                {
                    rebateAmount += product.Price * rebate.Percentage * request.Volume;
                    result.Success = true;
                }
            }
            else if(Next !=null)
            {
                return Next.Calculate(request);
            }
            return result;
        }
    }
}
