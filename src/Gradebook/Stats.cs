using System;

namespace Gradebook
{
    public class Stats
    {

        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90:
                        return 'A';


                    case var d when d >= 80:
                        return 'B';


                    case var d when d >= 70:
                        return 'C';


                    case var d when d >= 60:
                        return 'D';


                    default:
                        return 'F';

                }
            }
        }

        public double Sum;
        public int Count;
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public Stats()
        {
            Count = 0;
            Sum = 0;
            High = double.MinValue;
            Low = double.MaxValue;
        }

        public void Add(double grade)
        {
            Sum += grade;
            Count++;
            High = Math.Max(grade, High); // Max checks the arguments passed and return the greatest one. 
            Low = Math.Min(grade, Low);
        }
    }
}