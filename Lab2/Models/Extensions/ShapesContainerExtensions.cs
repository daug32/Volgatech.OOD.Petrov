﻿using Lab2.Data;
using Libs.SFML.Shapes;

namespace Lab2.Models.Extensions;

public static class ShapesContainerExtensions
{
    public static CashedShape? FindByPosition( this ShapesContainer container, float x, float y )
    {
        return container.FirstOrDefault( shape => shape
            .GetGlobalBounds()
            .Contains( x, y ) );
    }
}