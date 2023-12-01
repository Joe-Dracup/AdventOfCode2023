// See https://aka.ms/new-console-template for more information

/*
    Go to %AppData%\Roaming\NuGet

    Comment line from nuget config
*/

using Days;

bool cont = true;
while(cont)
{
    Console.WriteLine("Enter the number of the day that you want to solve, press any other key to end");

    var input = Console.ReadKey();
    
    Console.WriteLine(Environment.NewLine);
    
    string outPut;

    switch(input.KeyChar)
    {
        case '1':
            outPut = new Day1().Solve();
            break;
        default:
            outPut = "Goodbye!";
            cont = false;
            break;
    }

    Console.WriteLine(outPut + Environment.NewLine);
}
