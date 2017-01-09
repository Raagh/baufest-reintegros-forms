using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaufestReintegros.Model.Helpers;
using System.Net;

namespace BaufestReintegros.Test
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void Authenticate()
        {
            NetworkCredential credential = new NetworkCredential("user","password","domain");
            var result = ServiceHelper.AuthenticateCredentials(credential);

            Assert.IsTrue(result);
        }
    }
}
