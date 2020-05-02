using System;
using System.Collections.Generic;
using System.IO;

namespace Gradebook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Stats GetStats();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);

        public abstract Stats GetStats();

    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {

        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }


        }

        public override Stats GetStats()
        {
           var result = new Stats();

           using (var reader = File.OpenText($"{Name}.txt"))
           {
               var line = reader.ReadLine();
               while (line != null)
               {
                   var number = double.Parse(line);
                   result.Add(number);
                   line =  reader.ReadLine();
               }
           }

           return result;
        }
    }


    public class InMemoryBook : Book // Encapsulation
    {
        private List<double> grades;

        public const string CATEGORY = "Science"; // static field, access by calling on the class e.g. Book.CATEGORY

        public override event GradeAddedDelegate GradeAdded;


        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }


        public void AddLetterGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                case 'D':
                    AddGrade(60);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }
        public override Stats GetStats()
        {
            var result = new Stats();
           

            // var index = 0;
            // // "do while" loop will at least execute once
            // // "while" loop will check the condition first hence it may not run at all 
            // while (index < grades.Count)
            // {
            //     //alternatively - if statement to find highest mark:
            //     // if(grade > result.High)
            //     // {
            //     //     result.High = grade;
            //     // }

            //     result.High = Math.Max(grades[index], result.High);
            //     // Max checks the arguments passed and return the greatest one. 

            //     result.Low = Math.Min(grades[index], result.Low);
            //     result.Average += grades[index];
            //     index += 1; // or index++;
            // }

            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);
               
    

            }


           

            return result;
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }


    }
}