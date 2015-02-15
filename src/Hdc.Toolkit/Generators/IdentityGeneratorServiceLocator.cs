namespace Hdc.Generators
{
    public static class IdentityGeneratorServiceLocator
    {
        private static IIdentityGenerator _identityGenerator;
        public static IIdentityGenerator IdentityGenerator
        {
            get
            {
                if(_identityGenerator==null)
                {
                    _identityGenerator = new SequentialIdentityGenerator();
                }
                return _identityGenerator;
            }
            set { _identityGenerator = value; }
        }
    }
}