namespace StopwatchApp
{
    // Delegate definition
    public delegate void StopwatchEventHandler(string message);

    public class Stopwatch
    {
        private TimeSpan _timeElapsed;
        private bool _isRunning;
        private Timer? _timer;

        // Events
        public event StopwatchEventHandler? OnStarted;
        public event StopwatchEventHandler? OnStopped;
        public event StopwatchEventHandler? OnReset;

        public Stopwatch()
        {
            _timeElapsed = TimeSpan.Zero;
            _isRunning = false;
        }

        public void Start()
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
            _timer = new Timer(Tick, null, 0, 1000); // Ticking every second
        }

        public void Stop()
        {
            if (!_isRunning)
            {
                return;
            }   

            _isRunning = false;
            _timer?.Dispose();
            OnStopped?.Invoke("Stopwatch Stopped!");
        }

        public void Reset()
        {
            Stop();
            _timeElapsed = TimeSpan.Zero;
            OnReset?.Invoke("Stopwatch Reset!");
        }

        private void Tick(object? state)
        {
            if (_isRunning)
            {
                _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
            }
        }

        public override string ToString()
        {
            return _timeElapsed.ToString(@"hh\:mm\:ss");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Subscribing to events
            stopwatch.OnStarted += DisplayMessage;
            stopwatch.OnStopped += DisplayMessage;
            stopwatch.OnReset += DisplayMessage;

            Console.WriteLine("Stopwatch Console Application");
            Console.WriteLine("Press S to Start, T to Stop, R to Reset, Q to Quit.");

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine($"Time Elapsed: {stopwatch}");
                Thread.Sleep(1000);

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.S:
                            stopwatch.Start();
                            break;

                        case ConsoleKey.T:
                            stopwatch.Stop();
                            break;

                        case ConsoleKey.R:
                            stopwatch.Reset();
                            break;

                        case ConsoleKey.Q:
                            running = false;
                            stopwatch.Stop();
                            Console.WriteLine("Exiting Application.");
                            break;

                        default:
                            Console.WriteLine("Invalid Key. Use S, T, R, or Q.");
                            break;
                    }
                }
            }
        }

        static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
