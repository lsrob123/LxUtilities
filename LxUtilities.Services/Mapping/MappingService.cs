using System;
using System.Linq;
using AutoMapper;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Mapping
{
    public class MappingService : IMappingService
    {
        //static MappingService()
        //{
        //    var mapRegisterType = typeof (IMapRegister);
        //    var registers = AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(assembly => assembly.GetTypes()
        //            .Where(type => mapRegisterType.IsAssignableFrom(type)))
        //        .Select(type => (IMapRegister) Activator.CreateInstance(type))
        //        .ToList();

        //    foreach (var register in registers)
        //    {
        //        register.RegisterMaps();
        //    }
        //}

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}