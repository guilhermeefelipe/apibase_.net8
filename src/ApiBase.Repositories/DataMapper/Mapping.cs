﻿using System.Reflection;

namespace ApiBase.Repositories.DataMapper;
public class Mapping
{
    public PropertyInfo Source { get; }

    public Mapping(PropertyInfo source, PropertyInfo target)
    {
        Source = source;
        Target = target;
    }

    public PropertyInfo Target { get; }

}
