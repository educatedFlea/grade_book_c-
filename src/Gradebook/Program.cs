using System;
using System.Collections.Generic;

namespace Gradebook
{

    class Program
    {

        static void Main(string[] args)
        {	
            var book = new Book("Chloe's grade book");
            book.GradeAdded += OnGradeAdded;
            
            while(true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if(input == "q")
                {
                    break;
                }

                try 
                {

                    var grades = double.Parse(input);
                    book.addGrade(grades);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);

                }

                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }


            }

          


            var result = book.getStats();

            Console.WriteLine($"For the book {book.Name}");
            Console.WriteLine($"The highest grade: {result.High:N2}");
            Console.WriteLine($"The lowest grade: {result.Low:N2}");
            Console.WriteLine($"The average grade: {result.Average:N2}");
            Console.WriteLine($"The letter grade: {result.Letter}");
           
        }

        static void OnGradeAdded(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
