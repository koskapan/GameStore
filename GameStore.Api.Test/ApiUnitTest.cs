using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.Api.Test
{
    public class ApiUnitTest
    {
        public ApiUnitTest()
        {
        }

        [Fact]
        public void Addition_Test()
        {
            Assert.Equal(4, 2 + 2);
        }

    }
}
