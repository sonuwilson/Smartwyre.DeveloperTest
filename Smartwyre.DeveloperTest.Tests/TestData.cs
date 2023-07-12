using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> GetProductAndRebateForUom()
        {
            //Test case for null rebate
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "testuom" } ,
                                    null,
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.AmountPerUom },
                                    new CalculateRebateResult(){ Success = false} };
            // test case for null product
            yield return new object[] { null,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.AmountPerUom, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.AmountPerUom },
                                    new CalculateRebateResult(){ Success = false} };

            //test case for Product.SupportedIncentives not equal to IncentiveType.AmountPerUom
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.AmountPerUom, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.AmountPerUom },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for 0 rebate amount
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "testuom" } ,
                                    new Rebate() { Amount = 0, Identifier = "testrebate", Incentive = Types.IncentiveType.AmountPerUom, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.AmountPerUom },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for 0 request voulme
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.AmountPerUom, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 0,IncentiveType = IncentiveType.AmountPerUom },
                                    new CalculateRebateResult(){ Success = false} };


            // test case for to get CalculateRebateResult success true value
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.AmountPerUom, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.AmountPerUom },
                                    new CalculateRebateResult(){ Success = true} };
        }

        public static IEnumerable<object[]> GetProductAndRebateForFixedRate()
        {
            //Test case for null rebate
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    null,
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = false} };
            // test case for null product
            yield return new object[] { null,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = false} };

            //test case for Product.SupportedIncentives not equal to IncentiveType.FixedRateRebate
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for 0 rebate percentage
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 0 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for 0 product price
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 0, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for 0 request voulme
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 0,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = false} };


            // test case for to get CalculateRebateResult success true value
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedRateRebate, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedRateRebate },
                                    new CalculateRebateResult(){ Success = true} };
        }

        public static IEnumerable<object[]> GetProductAndRebateForFixedCashAmount()
        {
            //Test case for null rebate
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount, Uom = "testuom" } ,
                                    null,
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedCashAmount },
                                    new CalculateRebateResult(){ Success = false} };
          
            //test case for Product.SupportedIncentives not equal to IncentiveType.FixedCashAmount
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedCashAmount, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedCashAmount },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for 0 rebate amount
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount, Uom = "testuom" } ,
                                    new Rebate() { Amount = 0, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedCashAmount, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedCashAmount },
                                    new CalculateRebateResult(){ Success = false} };

            // test case for to get CalculateRebateResult success true value
            yield return new object[] { new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount, Uom = "testuom" } ,
                                    new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedCashAmount, Percentage = 20 },
                                    new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10,IncentiveType = IncentiveType.FixedCashAmount },
                                    new CalculateRebateResult(){ Success = true} };
        }
    }
}
