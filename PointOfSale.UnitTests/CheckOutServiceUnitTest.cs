using PointOfSale.Services;
using Repository;
using System;
using Xunit;

namespace PointOfSale.UnitTests
{
    public class CheckOutServiceUnitTest
    {
        private readonly ICheckOutService _checkOutService;
        public CheckOutServiceUnitTest()
        {
            _checkOutService = CreateCheckOutService();
        }

        private ICheckOutService CreateCheckOutService()
        {
            var pricingRepo = new PricingRepo();

            var checkOutService = new CheckOutService(pricingRepo);

            return checkOutService;
        }


        [Theory]
        [InlineData("ABCDABA", "$13.25")]
        [InlineData("CCCCCCC", "$6.00")]
        [InlineData("ABCD", "$7.25")]
        [InlineData("CCCCCC", "$5.00")]
        [InlineData("AACCCCC", "$7.50")]
        [InlineData("AAACCCCC", "$8.00")]
        [InlineData("AAACCCCCC", "$8.00")]
        [InlineData("ACCACCACC", "$8.00")]
        [InlineData("CCCCCCAAA", "$8.00")]
        [InlineData("CABBBBAACCBBCCC", "$33.50")]
        [InlineData("A", "$1.25")]
        [InlineData("WERT", "$0.00")]
        [InlineData("ABZ", "$5.50")]
        public void CalculateTotal_InputStandard(string input, string expected)
        {
            var actual = _checkOutService.CheckOut(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(" ABCDABA ", "$13.25")]
        [InlineData(" ABCD ABA ", "$13.25")]
        [InlineData("CCCCCC ", "$5.00")]
        [InlineData(" ABCD", "$7.25")]
        public void CalculateTotal_InputWithSpaces(string input, string expected)
        {
            var actual = _checkOutService.CheckOut(input);

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("*&ABC?DABA/", "$13.25")]
        [InlineData("!CC!!!C@#$%CCC", "$5.00")]
        [InlineData("~A&BC^-D)", "$7.25")]
        [InlineData("~!@#$%^&*()_+-=`{}|[]:;<>,./?)", "$0.00")]
        public void CalculateTotal_InputWithSpecialCharacters(string input, string expected)
        {
            var actual = _checkOutService.CheckOut(input);
            
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void CalculateTotal_ThrowsNullReferenceException(string input)
        {
            void actual() => _checkOutService.CheckOut(input);

            Assert.Throws<NullReferenceException>(actual);
        }

    }
}
