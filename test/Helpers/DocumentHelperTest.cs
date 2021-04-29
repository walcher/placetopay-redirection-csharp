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

        [Test]
        public void Should_Validate_Correctly_The_DNI()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DNI, "12345678"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DNI, "1234859"));
        }

        [Test]
        public void Should_Validate_Correctly_The_CRCPF()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CRCPF, "123485989"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CRCPF, "12348598"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CRCPF, "02348598"));
        }

        [Test]
        public void Should_Validate_Correctly_The_CPJ()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CPJ, "1234567894"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CPJ, "123456789"));
        }

        [Test]
        public void Should_Validate_Correctly_The_DIMEX()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIMEX, "12345678949"));
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIMEX, "123456789491"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIMEX, "1234567894911"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIMEX, "1234567894"));
        }

        [Test]
        public void Should_Validate_Correctly_The_DIDI()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIDI, "12345678949"));
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIDI, "123456789491"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIDI, "1234567894911"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_DIDI, "1234567894"));
        }

        [Test]
        public void Should_Validate_Correctly_The_CL_RUT()
        {
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CLRUT, "12.345.678-5"));
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CLRUT, "30.686.957-4"));
            Assert.IsTrue(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CLRUT, "13.342.430-K"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CLRUT, "13.342.430-L"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CLRUT, "1798288377"));
            Assert.IsFalse(DocumentHelper.IsValidDocument(DocumentHelper.TYPE_CLRUT, "Gdsdfgdfghfg"));
        }
    }
}
