namespace ODM.Domain.Configs
{
    public static class ParameterMetadataExtensions
    {
        public static IParameterMetadataSetter Set(this ParameterMetadata parameterMetadata)
        {
            var pm = parameterMetadata;
            var setter = new ParameterMetadataSetter(pm);
            return setter;
        }

        public interface IParameterMetadataSetter
        {
            IParameterMetadataSetter Maximum(int maxValue);
            IParameterMetadataSetter Minimum(int minValue);
            IParameterMetadataSetter DefaultValue(int defaultValue);
            IParameterMetadataSetter Description(string desc);
            IParameterMetadataSetter CatalogName(string desc);
            IParameterMetadataSetter GroupName(string desc);
        }

        private class ParameterMetadataSetter : IParameterMetadataSetter
        {
            private readonly ParameterMetadata _parameterMetadata;

            public ParameterMetadataSetter(ParameterMetadata parameterMetadata)
            {
                _parameterMetadata = parameterMetadata;
            }

            public IParameterMetadataSetter Maximum(int maxValue)
            {
                _parameterMetadata.Maximum = maxValue;
                return this;
            }

            public IParameterMetadataSetter Minimum(int minValue)
            {
                _parameterMetadata.Minimum = minValue;
                return this;
            }

            public IParameterMetadataSetter DefaultValue(int defaultValue)
            {
                _parameterMetadata.DefaultValue = defaultValue;
                return this;
            }

            public IParameterMetadataSetter Description(string desc)
            {
                _parameterMetadata.Description = desc;
                return this;
            }

            public IParameterMetadataSetter CatalogName(string desc)
            {
                _parameterMetadata.CatalogName = desc;
                return this;
            }

            public IParameterMetadataSetter GroupName(string desc)
            {
                _parameterMetadata.GroupName = desc;
                return this;
            }
        }
    }
}