using System;
using AutoMapper;

namespace LxUtilities.Services.Mapping.AutoMapper
{
public     class MapSetting
    {
        public Type Source { get; }

        public Type Destination { get; }

        public Func<IMappingExpression, IMappingExpression> CustomMap { get; }

        public MapSetting(Type source, Type destination, Func<IMappingExpression, IMappingExpression> customMap)
        {
            Source = source;
            Destination = destination;
            CustomMap = customMap;
        }

        public MapSetting(Type source, Type destination, Action<IMappingExpression> customMap)
        {
            Source = source;
            Destination = destination;
            CustomMap = expression =>
            {
                customMap(expression);
                return expression;
            };
        }
    }
}
