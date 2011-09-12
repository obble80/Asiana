using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Asiana.Catalog.Search.Solr.Client.Query;

namespace Asiana.Catalog.Search.Solr.Client.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SolrClientResponseTests
    {
        public SolrClientResponseTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //[TestMethod]
        //public void QueryAllStringTest()
        //{
        //    var client = new SolrClient(@"\Metadata\Solr\Asiana.xml");
        //    var response = client.Execute(new SolrQuery());

        //    foreach (var facet in response.Facets)
        //    {
        //        var name = facet.Name;
        //    }

        //    foreach (dynamic doc in response.Documents)
        //    {
        //        var name = doc.Name;
        //    }
        //}

     
    }
}
