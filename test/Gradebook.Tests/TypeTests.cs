using System;
using Xunit;

namespace Gradebook.Tests
{
    public class TypeTests
    {
        int count = 0;
        public delegate string WriteLogDelegate(string logMessage);

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = RetrunMessage;
            log += RetrunMessage;
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal(3,count);

        }
        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string RetrunMessage(string message)
        {
            count++;
            return message;
        }

        Book GetBook(string name)
        {
            return new Book(name);
        }

        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(88, x);
        }
        private int GetInt()
        {
            return 3;
        }
        private void SetInt(ref int i)
        {
            i = 88;
        }
        [Fact]
        public void CanSetNmaeFromReference()
        {
            // arrange
            var book1 = GetBook("Book 1");
            SetName(book1, "New name");

            Assert.Equal("New name", book1.Bookname);
        }

        private void SetName(Book book, string name)
        {
            book.Bookname = name;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            // arrange
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New name");

            Assert.Equal("Book 1", book1.Bookname);
        }

        private void GetBookSetName(Book book, string name)
        {
            book = new Book(name);
            book.Bookname = name;
        }

        [Fact]
        public void CSharpIsPassByRef()
        {
            // arrange
            var book1 = GetBook("Book 1");
            GetBookResetName(ref book1, "New name");

            Assert.Equal("New name", book1.Bookname);
        }

        private void GetBookResetName(ref Book book, string name) //can also use "out" opposed to "ref" - with compulsory output 
        {
            book = new Book(name);

        }


        [Fact] //attribute attached to Test1
        public void GetBookReturnsDifferentObjects()
        {
            // arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Bookname);
            Assert.Equal("Book 2", book2.Bookname);
            Assert.NotSame(book1, book2);
        }


        [Fact]
        public void TwoVarsCanRefSameObj()
        {
            // arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));

        }

        [Fact]
        public void StringsbehaveLikeValueTypes()
        {
            string name = "Chloe";
            var upper = MakeUpperCase(name);

            Assert.Equal("Chloe", name);
            Assert.Equal("CHLOE", upper);
        }

        private string MakeUpperCase(string param)
        {
            return param.ToUpper();
        }
    }
}
