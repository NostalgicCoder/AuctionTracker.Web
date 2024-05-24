using AuctionTracker.Web.Class;
using FluentAssertions;

namespace AuctionTracker.Tests.Class
{
    [TestClass]
    public class GeneralHelperTests
    {
        private GeneralHelper _generalHelper = new GeneralHelper();

        [TestMethod]
        public void CallConvertDateTimeResultShouldMatchExpectedAndPass()
        {
            DateTime val = new DateTime(2020, 12, 1);

            string result = _generalHelper.ConvertDateTime(val);

            result.Should().Be("Dec 2020");
        }

        [TestMethod]
        public void CallConvertDateTimeResultShouldNotMatchExpectedAndPass()
        {
            DateTime val = new DateTime(2021, 12, 1);

            string result = _generalHelper.ConvertDateTime(val);

            result.Should().NotBe("Dec 2020");
        }

        [TestMethod]
        public void CallValidDecimalNumberWithValidDecimalShouldPass1()
        {
            decimal val = 12.50m;

            bool result = _generalHelper.ValidDecimalNumber(val);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void CallValidDecimalNumberWithValidDecimalShouldPass2()
        {
            decimal val = 2.50m;

            bool result = _generalHelper.ValidDecimalNumber(val);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void CallValidDecimalNumberWithValidDecimalShouldPass3()
        {
            decimal val = 1000000.50m;

            bool result = _generalHelper.ValidDecimalNumber(val);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void CallValidDecimalNumberWithInvalidDecimalLengthShouldFail1()
        {
            decimal val = 12.501m;

            bool result = _generalHelper.ValidDecimalNumber(val);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void CallValidDecimalNumberWithInvalidDecimalLengthShouldFail2()
        {
            decimal val = 12.5m;

            bool result = _generalHelper.ValidDecimalNumber(val);

            result.Should().BeFalse();
        }
    }
}