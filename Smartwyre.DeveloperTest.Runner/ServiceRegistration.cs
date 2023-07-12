using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner
{
    public class ServiceRegistration
    {
        public static ServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection().
                                         AddTransient<IRebateDataStore, RebateDataStore>()
                                        .AddTransient<IProductDataStore, ProductDataStore>()
                                        .AddTransient<IRebateService, FixedRateRebateService>()
                                        .AddTransient<IRebateService, AmountPerUomRebateService>()
                                        .AddTransient<IRebateService, FixedCashAmountRebateService>();
                                      
                                      
                                        
            return serviceCollection.BuildServiceProvider();
        }
    }
}
