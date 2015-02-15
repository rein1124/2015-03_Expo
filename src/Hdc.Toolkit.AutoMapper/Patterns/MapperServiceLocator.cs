namespace Hdc.Patterns
{
    public static class MapperServiceLocator
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if(_mapper==null)
                {
                    _mapper = new Hdc.AutoMapper.AutoMapper();
                }

                return _mapper;
            }
            set { _mapper = value; }
        }
    }
}