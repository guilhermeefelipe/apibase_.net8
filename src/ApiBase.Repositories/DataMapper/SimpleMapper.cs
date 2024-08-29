namespace ApiBase.Repositories.DataMapper;

public static class SimpleMapper
{
    private readonly static SimpleMapperWorker mapper = new(new SimpleMapperCache());

    public static IEnumerable<string> Map<T, U>(T source, U target) where T : class where U : class
        => mapper.Map(source, target);

    public static U Map<U>(object source) where U : class, new()
    {
        ArgumentNullException.ThrowIfNull(source);

        var target = new U();

        var sourceType = source.GetType();
        var targetType = typeof(U);

        Map(source, target, mapper.GetMapping(sourceType, targetType));

        return target;
    }

    public static void Map(object source, object target, IEnumerable<Mapping> mappings)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(mappings);

        foreach (var item in mappings)
            item.Target.SetValue(target, item.Source.GetValue(source));
    }
}

