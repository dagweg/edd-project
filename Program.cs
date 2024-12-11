using static System.Console;
using StopwatchApp;

public class Program
{
    static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();

        // Subscribing to events
        stopwatch.OnStarted += DisplayMessage;
        stopwatch.OnStopped += DisplayMessage;
        stopwatch.OnReset += DisplayMessage;


        bool running = true;
        while (running)
        {
            Clear();
            WriteLine("Stopwatch Console Application");
            WriteLine("Press S to Start, T to Stop, R to Reset, Q to Quit.");
            WriteLine($"Time Elapsed: {stopwatch}");
            Thread.Sleep(1000);

            if (KeyAvailable)
            {
                ConsoleKey key = ReadKey(true).Key;

                if (key == ConsoleKey.S)
                    stopwatch.Start();
                else if (key == ConsoleKey.T)
                    stopwatch.Stop();
                else if (key == ConsoleKey.R)
                    stopwatch.Reset();
                else if (key == ConsoleKey.Q)
                {
                    running = false;
                    stopwatch.Stop();
                    WriteLine("Exiting Application.");
                }
                else
                {
                    WriteLine("Invalid Key. Use S, T, R, or Q.");
                }
            }
        }
    }

    static void DisplayMessage(string message) => WriteLine(message);
}

