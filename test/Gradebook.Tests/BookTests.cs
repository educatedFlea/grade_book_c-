using System;
using Xunit;

namespace Gradebook.Tests
{
    public class BookTests
    {
        [Fact] //attribute attached to Test1
        public void BookCalculatesStats()
        {
            // arrange
            var book = new Book("");
            book.addGrade(91);
            book.addGrade(90);
            book.addGrade(89);

            // act 
            var result = book.getStats();

            // assert
            Assert.Equal(90,result.Average, 1);
            Assert.Equal(91, result.High, 1);
            Assert.Equal(89, result.Low, 1);
            Assert.Equal('A', result.Letter);
        }
    }
}
