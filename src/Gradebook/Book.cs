using System;
using System.Collections.Generic;

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
    public class Book : NamedObject // Encapsulation
    {
        private List<double> grades;

        public const string CATEGORY = "Science"; // static field, access by calling on the class e.g. Book.CATEGORY

        public event GradeAddedDelegate GradeAdded; 


        public Book(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }


        public void AddLetterGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    addGrade(90);
                    break;

                case 'B':
                    addGrade(80);
                    break;

                case 'C':
                    addGrade(70);
                    break;

                case 'D':
                    addGrade(60);
                    break;

                default:
                    addGrade(0);
                    break;
            }
        }
        public Stats getStats()
        {
            var result = new Stats();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

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

                result.High = Math.Max(grades[index], result.High); // Max checks the arguments passed and return the greatest one. 
                result.Low = Math.Min(grades[index], result.Low);
                result.Average += grades[index];

            }


            result.Average /= grades.Count;

            switch (result.Average)
            {
                case var d when d >= 90:
                    result.Letter = 'A';
                    break;

                case var d when d >= 80:
                    result.Letter = 'B';
                    break;

                case var d when d >= 70:
                    result.Letter = 'C';
                    break;

                case var d when d >= 60:
                    result.Letter = 'D';
                    break;

                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }

        public void addGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
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