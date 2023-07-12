using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class FixedCashAmountRebateServiceTests
    {
        [Fact]
        public void Calculate_Throws_Exception_if_request_is_null()
        {
            // Arrange
            Mock<IProductDataStore> mockProductDataStore = new Mock<IProductDataStore>();
            mockProductDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom, Uom = "testuom" });

            Mock<IRebateDataStore> mockRebateDataStore = new Mock<IRebateDataStore>();
            mockRebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.AmountPerUom, Percentage = 20 });

            FixedCashAmountRebateService fixedCashAmountRebateService = new FixedCashAmountRebateService(mockProductDataStore.Object, mockRebateDataStore.Object);

            CalculateRebateRequest request = null;

            // Act
            Action action = () => fixedCashAmountRebateService.Calculate(request);

            // Assert
            var caughtException = Assert.Throws<ArgumentNullException>(action);
        }

        [Theory]
        [MemberData(nameof(TestData.GetProductAndRebateForFixedCashAmount), MemberType = typeof(TestData))]
        public void Calculate_Reabate_Tests(Product product, Rebate rebate, CalculateRebateRequest request, CalculateRebateResult expectedResult)
        {
            // Arrange
            Mock<IProductDataStore> mockProductDataStore = new Mock<IProductDataStore>();
            mockProductDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(product);

            Mock<IRebateDataStore> mockRebateDataStore = new Mock<IRebateDataStore>();
            mockRebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(rebate);

            FixedCashAmountRebateService fixedCashAmountRebateService = new FixedCashAmountRebateService(mockProductDataStore.Object, mockRebateDataStore.Object);

            // Act

            CalculateRebateResult calculateRebateResult = fixedCashAmountRebateService.Calculate(request);

            // Assert

            Assert.NotNull(calculateRebateResult);
            Assert.Equal(expectedResult.Success, calculateRebateResult.Success);

        }

        [Fact]
        public void Verfiy_Next_Service_Is_Invoked()
        {
            // Arrange
            Mock<IProductDataStore> mockProductDataStore = new Mock<IProductDataStore>();
            mockProductDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(new Product() { Id = 1, Identifier = "testproduct", Price = 100, SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount, Uom = "testuom" });

            Mock<IRebateDataStore> mockRebateDataStore = new Mock<IRebateDataStore>();
            mockRebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(new Rebate() { Amount = 500, Identifier = "testrebate", Incentive = Types.IncentiveType.FixedCashAmount, Percentage = 20 });

            FixedCashAmountRebateService fixedCashAmountRebateService = new FixedCashAmountRebateService(mockProductDataStore.Object, mockRebateDataStore.Object);

            fixedCashAmountRebateService.Next = new FixedRateRebateService(mockProductDataStore.Object, mockRebateDataStore.Object);

            // setting incentive type as AmountPerUom , then it will call the Next service
            CalculateRebateRequest request = new CalculateRebateRequest() { ProductIdentifier = "testproduct", RebateIdentifier = "testrebate", Volume = 10, IncentiveType = IncentiveType.AmountPerUom };

            // Act
            var result = fixedCashAmountRebateService.Calculate(request);

            // Assert
            Assert.NotNull(result);
        }

    }
}
