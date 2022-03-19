using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;
using TaxCalculator.Services.Services;
using Xunit;

namespace TaxCalculator.UnitTests
{
    public class TaxJarTaxCalculatorTest : IDisposable
    {

        private Mock<IHttpClientFactory> mockFactory;
        private IConfiguration configuration;
        private TaxJarTaxCalculator taxJarTaxCalculator;

        // setup
        public TaxJarTaxCalculatorTest()
        {
            this.mockFactory = new Mock<IHttpClientFactory>();
            var inMemorySettings = new Dictionary<string, string> {
                {"ClientIdentity:TaxJar:API", "https://api.taxjar.com/v2/"},
                {"ClientIdentity:TaxJar:Token", "Token token=\"5da2f821eee4035db4771edab942a4cc\""},
            };
            this.configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        // teardown
        public void Dispose()
        {
            // Dispose here
            this.mockFactory = null;
            this.configuration = null;
            this.taxJarTaxCalculator = null;
        }

        ///<name>
        /// Unit Test GetTaxRatesForLocation Sunny Test 01
        ///</name>
        ///<summary>
        /// Given the correct modal the method should return the correct decimal value
        ///</summary>
        [Fact]
        public async Task GetTaxRatesForLocation_SunnyTest_01()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'rate':{'combined_rate':'0.1025'}}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            TaxJarTaxCalculator taxJarTaxCalculator = new TaxJarTaxCalculator(mockFactory.Object, configuration);

            var location = new Location
            {
                ZipCode = "90404",
                Country = "US",
                State = "",
                City = "Santa Monica",
                Street = "",
            };
            var expected = 0.1025;
            
            //Act
            var actual = await taxJarTaxCalculator.GetTaxRatesForLocation(location);

            //Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        ///<name>
        /// Unit Test GetTaxRatesForLocation Rainy Test 01
        ///</name>
        ///<summary>
        /// The method should return an exception if the zip code is missing
        ///</summary>
        [Fact]
        public async Task GetTaxRatesForLocation_RainyTest_01()
        {
            //Arrange
            TaxJarTaxCalculator taxJarTaxCalculator = new TaxJarTaxCalculator(mockFactory.Object, configuration);

            var location = new Location
            {
                ZipCode = null,
                Country = "US",
                State = "",
                City = "Santa Monica",
                Street = "",
            };
            var expected = "Zip Code is a required field";

            //Act
            var actual = await Assert.ThrowsAsync<ArgumentNullException>(() => taxJarTaxCalculator.GetTaxRatesForLocation(location));

            //Assert
            Assert.Equal(expected, actual.ParamName);
        }

        ///<name>
        /// Unit Test GetTaxesForOrder Sunny Test 01
        ///</name>
        ///<summary>
        /// Exception Should Thrown When Zip Code is Null
        ///</summary>
        [Fact]
        public async Task GetTaxesForOrder_SunnyTest_01()
        {
            //Arrange
            TaxJarTaxCalculator taxJarTaxCalculator = new TaxJarTaxCalculator(mockFactory.Object, configuration);
            var lineItem = new LineItem
            {
                Quantity = 1,
                Unit_Price = 15.0M,
                Product_Tax_Code = "31000"
            };
            var lineItemList = new LineItem[] { lineItem };

            var order = new Order
            {
                  From_Country = "US",
                  From_Zip = "07001",
                  From_State = "NJ",
                  To_Country = "US",
                  To_Zip = "07446",
                  To_State = "NJ",
                  Amount = 16.50M,
                  Shipping = 1.5M,
                  Line_Items = lineItemList
            };
            var expected = 1.09;

            //Act
            var actual = await taxJarTaxCalculator.GetTaxesForOrder(order);

            //Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        ///<name>
        /// Unit Test GetTaxesForOrder Rainy Test 01
        ///</name>
        ///<summary>
        /// The method should return an exception if the To_Country is missing
        ///</summary>
        [Fact]
        public async Task GetTaxesForOrder_RainyTest_01()
        {
            //Arrange
            TaxJarTaxCalculator taxJarTaxCalculator = new TaxJarTaxCalculator(mockFactory.Object, configuration);
            var lineItem = new LineItem
            {
                Quantity = 1,
                Unit_Price = 15.0M,
                Product_Tax_Code = "31000"
            };
            var lineItemList = new LineItem[] { lineItem };

            var order = new Order
            {
                From_Country = "US",
                From_Zip = "07001",
                From_State = "NJ",
                To_Country = null,
                To_Zip = "07446",
                To_State = "NJ",
                Amount = 16.50M,
                Shipping = 1.5M,
                Line_Items = lineItemList
            };
            var expected = "To Country is a required field";

            //Act
            var actual = await Assert.ThrowsAsync<ArgumentNullException>(() => taxJarTaxCalculator.GetTaxesForOrder(order));

            //Assert
            Assert.Equal(expected, actual.ParamName);
        }
    }
}
