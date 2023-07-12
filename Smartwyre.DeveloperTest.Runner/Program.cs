using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Linq;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var serviceProvider = ServiceRegistration.Configure();

            var services = serviceProvider.GetServices<IRebateService>();

            var fixedRateRebateService = services.FirstOrDefault(x => x.GetType() == typeof(FixedRateRebateService));
            var amountPerUomRebateService = services.FirstOrDefault(x => x.GetType() == typeof(AmountPerUomRebateService));
            var fixedCashAmountRebateService = services.FirstOrDefault(x => x.GetType() == typeof(FixedCashAmountRebateService));


            fixedRateRebateService.Next = amountPerUomRebateService;
            amountPerUomRebateService.Next = fixedCashAmountRebateService;

            bool Continue = true;
            while (Continue)
            {
                CalculateRebateRequest calculateRebateRequest = new CalculateRebateRequest();

                Console.WriteLine("Starting the rebate calculation program \n");
                Console.WriteLine("Enter Rebate Identifier\n");

                calculateRebateRequest.RebateIdentifier = Console.ReadLine();

                Console.WriteLine("\n ProductIdentifier \n");
                calculateRebateRequest.ProductIdentifier = Console.ReadLine();

                Console.WriteLine("\n Volume\n");
                calculateRebateRequest.Volume = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("\n");
                Console.WriteLine("Please select the incentive type because database is not developed yet.\n" +
                              "1. FixedRateRebate \n" +
                              "2. AmountPerUom \n" +
                              "3. FixedCashAmount \n");

                calculateRebateRequest.IncentiveType = (IncentiveType)Convert.ToInt32(Console.ReadLine()) - 1;

                var calculateRebateResult = fixedRateRebateService.Calculate(calculateRebateRequest);

                Console.WriteLine("Rebate calculation result :  " + calculateRebateResult.Success);
                Console.WriteLine("\n");
                Console.WriteLine("Do you want to continue? please type 'Yes' or 'No' \n");

                var choice = Console.ReadLine();
                Console.WriteLine();
                if (choice.ToLower() == "yes")
                    Continue = true;
                else
                    Continue = false;
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
