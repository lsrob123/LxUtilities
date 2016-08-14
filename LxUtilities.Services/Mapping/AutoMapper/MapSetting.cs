using System;
using AutoMapper;

namespace LxUtilities.Services.Mapping.AutoMapper
{
    public class MapSetting
    {
        public MapSetting(Type source, Type destination,
            Func<IMappingExpression, IMappingExpression> customMapFunc = null)
        {
            Source = source;
            Destination = destination;
            CustomMap = customMapFunc;
        }

        public MapSetting(Type source, Type destination, Action<IMappingExpression> customMapAction = null)
        {
            Source = source;
            Destination = destination;
            CustomMap = expression =>
            {
                customMapAction?.Invoke(expression);
                return expression;
            };
        }

        public Type Source { get; }
        public Type Destination { get; }
        public Func<IMappingExpression, IMappingExpression> CustomMap { get; }
    }
}