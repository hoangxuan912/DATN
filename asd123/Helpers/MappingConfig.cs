using asd123.Helpers.DataRequestMapping;

namespace asd123.Helpers
{
    public static class MappingConfig
    {
        public static IServiceCollection AutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateUserMapping));
            services.AddAutoMapper(typeof(DepartmentMapping));
            services.AddAutoMapper(typeof(MajorMapping));
            services.AddAutoMapper(typeof(SubjectMapping));
            return services;
        }
    }
}
