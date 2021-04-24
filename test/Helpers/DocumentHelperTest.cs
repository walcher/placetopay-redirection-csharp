using NUnit.Framework;
using PlacetoPay.Redirection.Helpers;
using System;
using System.Linq;

namespace PlacetoPay.RedirectionTests.Helpers
{
    [TestFixture]
    public class DocumentHelperTest
    {
        [Test]
        public void Should_Exclude_The_Given_Document_Type()
        {
            string[] documentTypes = DocumentHelper.DocumentTypes();

            Assert.IsTrue(documentTypes.Contains(DocumentHelper.TYPE_CI), "Document CI Exists");
            Assert.IsTrue(documentTypes.Contains(DocumentHelper.TYPE_CC), "Document CC Exists");
            Assert.IsTrue(documentTypes.Contains(DocumentHelper.TYPE_NIT), "Document NIT Exists");

            string[] exclude = { DocumentHelper.TYPE_CI, DocumentHelper.TYPE_CC };

            documentTypes = DocumentHelper.DocumentTypes(exclude);

            Assert.IsFalse(documentTypes.Contains(DocumentHelper.TYPE_CI), "Document CI Excluded");
            Assert.IsFalse(documentTypes.Contains(DocumentHelper.TYPE_CC), "Document CC Excluded");
            Assert.IsTrue(documentTypes.Contains(DocumentHelper.TYPE_NIT), "Document NIT Exists");
        }

        [Test]
        public void Should_Validate_Correctly_The_CI()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CI, "1002606430"));
        }

        [Test]
        public void Should_Validate_Correctly_The_RUC()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_RUC, "1798288377001"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_RUC, "1798288377"));
        }

        [Test]
        public void Should_Validate_Correctly_The_NIT()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_NIT, "860000038"));
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_NIT, "86000003"));
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_NIT, "8600000384"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_NIT, "8600000384-42"));
        }
    }
}
