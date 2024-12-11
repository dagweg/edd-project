using StopwatchApp.EventHandlers;

namespace StopwatchApp;

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
      return;

    _isRunning = true;
    OnStarted?.Invoke("Stopwatch Started!");
    _timer = new Timer(Tick, null, 0, 1000); // Ticking every second
  }

  public void Stop()
  {
    if (!_isRunning)
      return;

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
      _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
  }

  public override string ToString() => _timeElapsed.ToString(@"hh\:mm\:ss");
}