using Microsoft.EntityFrameworkCore;

namespace UltimateUtils.EF.Extensions;

public static class ModelBuilderExtensions
{
    private const string ApplyConfigurationMethodName = "ApplyConfiguration";

    private const string EntityTypeConfigurationInterfaceName = "IEntityTypeConfiguration`1";

    public static ModelBuilder ApplyEntityTypeConfiguration<T>(this ModelBuilder modelBuilder)
        where T : class, new()
    {
        var entityConfigurationType = typeof(T);
        var entityTypeConfigurationType =
            entityConfigurationType.GetInterface(EntityTypeConfigurationInterfaceName)
            ?? throw new InvalidOperationException($"Type '{entityConfigurationType.FullName}' does not implement IEntityTypeConfiguration.");

        var entityType = entityTypeConfigurationType.GetGenericArguments().Single();

        var methodInfo =
            typeof(ModelBuilder).GetMethod(ApplyConfigurationMethodName)
            ?? throw new InvalidOperationException($"Method '{ApplyConfigurationMethodName}' does not exist in 'ModelBuilder'.");

        var applyMethod = methodInfo.MakeGenericMethod(entityType);
        applyMethod.Invoke(modelBuilder, [new T()]);

        return modelBuilder;
    }
}
