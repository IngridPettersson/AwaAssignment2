using System;
using System.Collections.Generic;
using System.Text;

namespace awa_assignment_2
{
    class Guest
    {
        public string name;
        public double yearsAsFriends;
        private double surpriseSaladGrade;
        public string comment;

        public Guest(string name, double yearsAsFriends)
        {
            this.name = name;
            this.yearsAsFriends = yearsAsFriends;

            Console.WriteLine($"NAME: {name}\nYEARS AS FRIENDS: {yearsAsFriends}\n");
        }
        public Guest(string name, double surpriseSaladGrade, string comment)
        {
            this.name = name;
            SurpriseSaladGrade = surpriseSaladGrade;
            this.comment = comment;

            string areYouWelcome;
            if (WelcomeAgain())
            {
            areYouWelcome = "You can count on an invitation next time around!";
            }
            else
            {
                areYouWelcome = "I'm sorry you didn't like my surprise salad. So I think our roads diverse here...";
            }

            Console.WriteLine($"NAME: {name}\nGRADE: {SurpriseSaladGrade}\nCOMMENT: {comment}\nCOMMENT FROM YOU TO GUEST: {areYouWelcome}\n\n");
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
            if (surpriseSaladGrade >= 4)
                return true;
            else
                return false;
        }
    }
}
