﻿using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using Libs.SFML.Vertices;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Data;

public static class DataLoader
{
    public static IEnumerable<IShape> LoadData()
    {
        var windowSize = new Vector2u( 800, 600 );
        
        yield return new Rectangle( new Vector2f( 20, 20 ) )
            .SetFillColor( Color.Black )
            .SetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );

        yield return new Rectangle( new Vector2f( 20, 20 ) )
            .SetFillColor( Color.Black )
            .SetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );

        yield return new Rectangle( new Vector2f( 20, 20 ) )
            .SetFillColor( Color.Black )
            .SetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );

        yield return new Circle( 35 )
            .SetFillColor( Color.Black )
            .SetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );
    }
}