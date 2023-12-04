﻿using SFML.Graphics;
using SFML.System;

namespace Lab2.Models.Extensions;

public static class ShapeExtensions
{
    public static T FluentSetPosition<T>( this T shape, float x, float y ) where T : Shape
    {
        shape.Position = new Vector2f( x, y );
        return shape;
    }
    
    public static T FluentSetPosition<T>( this T shape, Vector2f position ) where T : Shape
    {
        shape.Position = position;
        return shape;
    }

    public static T FluentSetOutlineColor<T>( this T shape, Color outlineColor ) where T : Shape
    {
        shape.OutlineColor = outlineColor;
        return shape;
    }

    public static T FluentSetOutlineThickness<T>( this T shape, float thickness ) where T : Shape
    {
        shape.OutlineThickness = thickness;
        return shape;
    }

    public static T FluentSetFillColor<T>( this T shape, Color fillColor ) where T : Shape
    {
        shape.FillColor = fillColor;
        return shape;
    }
}