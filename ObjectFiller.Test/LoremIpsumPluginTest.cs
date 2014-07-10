using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Library;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class LoremIpsumPluginTest
    {
        [TestMethod]
        public void TestPlugin()
        {
            int isbnWordCount = 1000;
            int nameWordCount = 500;
            Filler<Book> bookFill = new Filler<Book>();
            bookFill.Setup()
                .OnProperty(x => x.ISBN).Use(new LoremIpsum(isbnWordCount))
                .OnProperty(x => x.Name).Use(new LoremIpsum(nameWordCount));

            Book book = bookFill.Create();

            Assert.IsNotNull(book);
            Assert.IsNotNull(book.ISBN);
            Assert.IsNotNull(book.Name);
            Assert.AreEqual(isbnWordCount, book.ISBN.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);
            Assert.AreEqual(nameWordCount, book.Name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);
        }

        [TestMethod]
        public void TestNewLoremIpsum()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.InDerFremde, paragraphs: 3));

            var b = book.Create();

            Assert.IsNotNull(b);
            Assert.AreEqual(3, b.ISBN.Split('\n').Length -1);
        }
    }
}
