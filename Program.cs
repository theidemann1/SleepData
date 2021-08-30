using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            //moved declaration to allow for program loop
            string resp;
            do{
                // ask for input
                Console.WriteLine("Enter 1 to create data file.");
                Console.WriteLine("Enter 2 to parse data.");
                Console.WriteLine("Enter anything else to quit.");
                // input response
                resp = Console.ReadLine();
                Console.WriteLine();

                if (resp == "1")
                {
                    // create data file

                    // ask a question
                    Console.WriteLine("How many weeks of data is needed?");
                    // input the response (convert to int)
                    int weeks = int.Parse(Console.ReadLine());

                    // determine start and end date
                    DateTime today = DateTime.Now;
                    // we want full weeks sunday - saturday
                    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                    // subtract # of weeks from endDate to get startDate
                    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                    
                    // random number generator
                    Random rnd = new Random();

                    // create file
                    StreamWriter sw = new StreamWriter("data.txt");
                    // loop for the desired # of weeks
                    while (dataDate < dataEndDate)
                    {
                        // 7 days in a week
                        int[] hours = new int[7];
                        for (int i = 0; i < hours.Length; i++)
                        {
                            // generate random number of hours slept between 4-12 (inclusive)
                            hours[i] = rnd.Next(4, 13);
                        }
                        // M/d/yyyy,#|#|#|#|#|#|#
                        //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                        // add 1 week to date
                        dataDate = dataDate.AddDays(7);
                    }
                    sw.Close();
                }
                else if (resp == "2")
                {
                    StreamReader sr = new StreamReader("data.txt");
                    do
                    {
                        //get line from file
                        string line = sr.ReadLine();
                        //seperate date from line and store into DateTime
                        string[] dateSplit = line.Split(',');
                        DateTime dateHeader = DateTime.Parse(dateSplit[0]);
                        //seperate daily hours
                        string[] hourSplit = dateSplit[1].Split('|');

                        double total = 0;
                        double average;
                        //loop to get total weekly hours
                        for(int i = 0; i <= 6; i++)
                        {
                            total = total + int.Parse(hourSplit[i]);
                        }
                        //calc average from total
                        average = total/7;

                        Console.WriteLine("Week of {0:MMM, dd, yyyy}",dateHeader);
                        Console.WriteLine($" {"Mo",2} {"Tu",2} {"We",2} {"Th",2} {"Fr",2} {"Sa",2} {"Su",2} {"Tot",3} {"Avg",3}");
                        Console.WriteLine($" {"--",2} {"--",2} {"--",2} {"--",2} {"--",2} {"--",2} {"--",2} {"---",3} {"---",3}");
                        Console.WriteLine($" {hourSplit[0],2} {hourSplit[1],2} {hourSplit[2],2} {hourSplit[3],2} {hourSplit[4],2} {hourSplit[5],2} {hourSplit[6],2} {total,3} {average,3:F1}");
                        Console.WriteLine();
                    }while(!sr.EndOfStream);
                    sr.Close();
                }
            }while(resp == "1" || resp == "2");
        }
    }
}
