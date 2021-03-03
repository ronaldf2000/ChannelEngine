using ChannelEngine.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChannelEngine.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestTop5()
        {
            TestOrderClient client = new TestOrderClient();
            AssessmentLogic logic = new AssessmentLogic(client, new NullLogger<AssessmentLogic>());

            var result = await logic.RetreiveTop(5);
            Assert.Equal(5, result.Count());
            result = await logic.RetreiveTop(3);
            Assert.Equal(3, result.Count());
            //The total quantity should aggregated
            Assert.Equal(55, result.First().Quantity);
        }
    }
}
