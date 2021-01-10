using System;
using System.Collections.Generic;
using System.Text;

namespace awa_assignment_2
{
    class Guest
    {
        public string Name { get; set; }
        public double YearsAsFriends { get; set; }
        public string Comment { get; set; }
        private double surpriseSaladGrade;

        public Guest(string name, double yearsAsFriends)
        {
            this.Name = name;
            this.YearsAsFriends = yearsAsFriends;

            Console.WriteLine($"NAME: {name}\nYEARS AS FRIENDS: {yearsAsFriends}\n");
        }
        public Guest(string name, double surpriseSaladGrade, string comment)
        {
            this.Name = name;
            this.SurpriseSaladGrade = surpriseSaladGrade;
            this.Comment = comment;

            string areYouWelcome;
            if (WelcomeAgain())
            {
            areYouWelcome = "You can count on an invitation next time around!";
            }
            else
            {
                areYouWelcome = "I'm sorry you didn't like my surprise salad. So I think our roads diverse here...";
            }

            Console.WriteLine($"NAME: {name}\nGRADE: {surpriseSaladGrade}\nCOMMENT: {comment}\nCOMMENT FROM YOU TO GUEST: {areYouWelcome}\n");
        }

        public double SurpriseSaladGrade
        {
            get { return surpriseSaladGrade; }
            set
            {
                if (value <= 5 && value >= 0)
                    surpriseSaladGrade = value;
                else
                    surpriseSaladGrade = -1;
            }
        }

        public bool WelcomeAgain()
        {
            return surpriseSaladGrade >= 4;
        }
    }
}
