﻿// See https://aka.ms/new-console-template for more information

/*
    Go to %AppData%\Roaming\NuGet

    Comment line from nuget config
*/

using Helpers;

bool cont = true;
string outPut;

while (cont)
{
    Console.WriteLine("Enter the number of the day that you want to solve, type \"end\" to end the program\n");
    try
    {
        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            throw new ApplicationException("Please input a day");
        }

        if (input.Equals("end", StringComparison.CurrentCultureIgnoreCase))
        {
            outPut = " \nGoodbye!";
            cont = false;
        }
        else
        {
            outPut = InputHelper.GetDayResponse(input);
        }
    }
    catch (Exception e)
    {
        outPut = e.Message;
    }

    Console.WriteLine("\n" + outPut + "\n");
}
