using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator;

namespace TestElevators
{
    [TestClass]
    public class ExceptionRepositoryTest
    {
        [TestMethod]
        public void VerifyDeveloperExceptionError_AreEqual()
        {
            Assert.AreEqual("-001: Developer Error - Key cannot be found in exception dictionary.", ExceptionRepository.GetException(-2));
        }

        [TestMethod]
        public void VerifyLegitimateExceptionError_AreEqual()
        {
            Assert.AreEqual("1001: The floor requested is {0}, but must be between 0 and {1}.", ExceptionRepository.GetException(1001));
        }
    }
}