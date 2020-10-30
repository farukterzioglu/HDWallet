namespace NBitcoin.Base
{
    public abstract class KeyBase<TKey> where TKey: KeyBase<TKey>
    {
        byte[] vch = new byte[0];
        public KeyBase(byte[] data)
        {
            vch = data;
        }

        public bool IsCompressed { get; protected set;}
        public PubKey PubKey { get; protected set;}

        public byte[] ToBytes()
        {
            return vch;
        }

        public abstract TKey Derivate(byte[] chainCode, uint nChild, out byte[] chainCodeChild);
    }
}