/*namespace Hdc.Mercury.Communication
{
    public class AccessItemIdentifier
    {
        public static uint InstanceCounter;

        private uint _instanceIndex;

        public AccessItemIdentifier()
        {
            _instanceIndex = InstanceCounter;
            InstanceCounter++;
        }

        private AccessItemIdentifier(uint id)
            : this()
        {
            Id = id;
        }

        public uint Id { get; set; }

        public override string ToString()
        {
            return string.Format("AccessItemIdentifier.Id = {0}", Id);
        }

        public static implicit operator AccessItemIdentifier(uint id)
        {
            return new AccessItemIdentifier(id);
        }

        public static implicit operator uint(AccessItemIdentifier idItem)
        {
            return idItem.Id;
        }
    }
}*/