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

    public bool IsDoubleClick( MouseButtonEventArgs e )
    {
        if ( !_lastClickDates.ContainsKey( e.Button ) )
        {
            return false;
        }

        DateTime clickTime = DateTime.Now;
        ClickData lastClickData = _lastClickDates[e.Button];
        
        double millisecondsPassed = clickTime.Subtract( lastClickData.Time ).TotalMilliseconds;
        bool isInTimeRange = millisecondsPassed <= DoubleClickMaxMillisecondsInterval;
        
        if ( !isInTimeRange )
        {
            _lastClickDates[e.Button] = new ClickData( clickTime, false );
            return false;
        }

        var clickData = new ClickData( clickTime, isInTimeRange && !lastClickData.IsDoubleClick );
        _lastClickDates[e.Button] = clickData;

        return clickData.IsDoubleClick;
    }
}