using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedCashAmountRebateService : IRebateService
    {
        private readonly IProductDataStore _productDataStore;
        private readonly IRebateDataStore _rebateDataStore;
        public FixedCashAmountRebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore)
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
            else if (rebate.Incentive == IncentiveType.FixedCashAmount)
            {
                var rebateAmount = 0m;
                
                if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
                {
                    result.Success = false;
                }
                else if (rebate.Amount == 0)
                {
                    result.Success = false;
                }
                else
                {
                    rebateAmount = rebate.Amount;
                    result.Success = true;
                }
            }
            else if (Next != null)
            {
                return Next.Calculate(request);
            }
            return result;
        }
    }
}
