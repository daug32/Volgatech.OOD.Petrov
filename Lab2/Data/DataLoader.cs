using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Vertices;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Data;

public static class DataLoader
{
    public static IEnumerable<CashedShape> LoadData()
    {
        var windowSize = new Vector2u( 800, 600 );
        
        yield return CashedShape.Create(
            new RectangleShape( new Vector2f( 20, 20 ) ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );

        yield return CashedShape.Create(
            new RectangleShape( new Vector2f( 20, 20 ) ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );

        yield return CashedShape.Create(
            new RectangleShape( new Vector2f( 20, 20 ) ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );

        yield return CashedShape.Create(
            new CircleShape( 35 ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2Utils.GetRandomInBounds( windowSize ) );
    }
}