//dotnet run --project MiniProjects\MiniProject1

namespace MiniProject1;

class Program
{
    static string[] months = ["january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"];

    static void Main(string[] args)
    {
        Console.WriteLine("\n");
        int birthMonthValue = GetMonthFromUser("Which month were you born in?");
        int currentMonthValue = GetMonthFromUser("What is the current month?");
        int monthsTillBDay = birthMonthValue - currentMonthValue;
        //Determines which operation to commit
        Console.WriteLine("\nWhat do you think about next year? (Answer 1, 2, or 3)\n\t1) I look forward to it.\n\t2) It's a government conspiracy.\n\t3) The world will end tonight.");
        string userAnswerNextYear = "";
        while(userAnswerNextYear == ""){
            userAnswerNextYear = Console.ReadLine()!;
            switch(userAnswerNextYear)
            {
                //Normal calculation
                case "1":
                    if (monthsTillBDay == 0)
                    {
                        Console.WriteLine("Your birthday is this month. Congratulations on surviving a discreet quantity of years! May the rest be happier than the first.");
                    }
                    else
                    {
                        if(monthsTillBDay < 0)
                        {
                            monthsTillBDay += months.Length;
                        }
                        Console.WriteLine("Your next birthday is " + monthsTillBDay + " months away. May they pass in peace.");
                    }
                    break;
                //No birthday if it's next year
                case "2":
                    if (monthsTillBDay == 0)
                    {
                        Console.WriteLine("Your birthday is this month. Congratulations on surviving a discreet quantity of years! May the rest be happier than the first.");
                    }
                    else if(monthsTillBDay < 0)
                    {
                        Console.WriteLine("Unfortunately, due to the conspiracy, you will never know another birthday. May you find forgiveness in your heart.");
                    }
                    else
                    {
                        Console.WriteLine("Your next birthday is " + monthsTillBDay + " months away. May they pass in peace.");
                    }
                    break;
                //You don't get a calculation cause you're a doomer
                case "3":
                    Console.WriteLine("We disapprove of your pessimistic attitude and request you leave our establishment. May you one day find a glass half full.");
                    break;
                default:
                    Console.WriteLine("Please choose 1, 2, or 3.");
                    break;
            }
        }
    }

    /// <summary>
    /// Aquires a month from the user & returns its position in the calendar.
    /// </summary>
    /// <param name="promptText">The sentence used to ask the user for the month</param>
    /// <returns></returns>
    static int GetMonthFromUser(String promptText)
    {
        int monthValue = -1;
        while(monthValue == -1)
        {
            Console.WriteLine("\n" + promptText);
            string monthInput = Console.ReadLine()!;
            monthValue = GetMonthValue(monthInput);
            if(monthValue == -1)
            {
                Console.WriteLine(monthInput + " is not a real month!");
            }
        }
        return monthValue;
    }

    /// <summary>
    /// Returns which position (starting from 1) the month occupies in the calendar. Returns -1 if not a valid month
    /// </summary>
    /// <param name="monthName"></param>
    /// <returns></returns>
    static int GetMonthValue(string monthName)
    {
        monthName = monthName.ToLower();
        for(int i = 0; i < months.Length; i++)
        {
            if(monthName == months[i]){
                return i + 1;
            }
        }
        return -1;
    }
}
