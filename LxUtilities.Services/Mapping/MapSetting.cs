using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxUtilities.Services.Mapping
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
    }
}
