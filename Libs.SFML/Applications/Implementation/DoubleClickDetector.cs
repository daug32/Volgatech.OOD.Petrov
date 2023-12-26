using SFML.Window;

namespace Libs.SFML.Applications.Implementation;

internal class DoubleClickDetector
{
    private class ClickData
    {
        public DateTime Time { get;  }
        public bool IsDoubleClick { get; }

        public ClickData() : this( DateTime.Now, false )
        {
        }

        public ClickData( DateTime dateTime, bool isDoubleClick )
        {
            Time = dateTime;
            IsDoubleClick = isDoubleClick;
        }
    }
    
    private const long DoubleClickMaxMillisecondsInterval = 200;

    private readonly Dictionary<Mouse.Button, ClickData> _lastClickDates = new()
    {
        { Mouse.Button.Left, new ClickData() },
        { Mouse.Button.Right, new ClickData() }
    };

    public bool IsDoubleClick( MouseButtonEventArgs buttonData )
    {
        // Check if button is supported
        if ( !_lastClickDates.ContainsKey( buttonData.Button ) )
        {
            return false;
        }

        DateTime clickTime = DateTime.Now;
        ClickData lastClickData = _lastClickDates[buttonData.Button];

        // Check if click was performed within the time range of a double click 
        double millisecondsPassed = clickTime
            .Subtract( lastClickData.Time )
            .TotalMilliseconds;
        bool isInTimeRange = millisecondsPassed <= DoubleClickMaxMillisecondsInterval;

        if ( !isInTimeRange )
        {
            _lastClickDates[buttonData.Button] = new ClickData( clickTime, false );
            return false;
        }

        // Check if previous click was a double click
        // Double click is a click that performed after a default click
        bool isDoubleClick = !lastClickData.IsDoubleClick;
        _lastClickDates[buttonData.Button] = new ClickData( clickTime, isDoubleClick );

        return isDoubleClick;
    }
}