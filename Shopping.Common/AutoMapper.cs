﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AutoMappers
{
    public static T MapTo<T>(this object source)
    {
        if (source == null) return default(T);

        MapperConfiguration mapperConfiguration = new MapperConfiguration(m => m.CreateMap(source.GetType(), typeof(T)));
        IMapper mapper = mapperConfiguration.CreateMapper();
        return mapper.Map<T>(source);
    }

    public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> objList)
    {
        MapperConfiguration config = new MapperConfiguration(m => m.CreateMap<TSource, TDestination>());
        IMapper mapper = config.CreateMapper();
        return mapper.Map<IEnumerable<TSource>, List<TDestination>>(objList);
    }

    public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> objList, string Ignore)
    {
        MapperConfiguration config = new MapperConfiguration(m => m.CreateMap<TSource, TDestination>().ForMember(Ignore, a => a.Ignore()));
        IMapper mapper = config.CreateMapper();
        return mapper.Map<IEnumerable<TSource>, List<TDestination>>(objList);
    }

    public static IEnumerable<TDestination> MapToIEnumerable<TSource, TDestination>(this IEnumerable<TSource> objList)
    {
        MapperConfiguration config = new MapperConfiguration(m => m.CreateMap<TSource, TDestination>());
        IMapper mapper = config.CreateMapper();
        return mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(objList);
    }
}
