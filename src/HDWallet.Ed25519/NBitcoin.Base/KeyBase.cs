namespace NBitcoin.Base
{
    public class Key
    {
        byte[] vch = new byte[0];
        public Key(byte[] data)
        {
            vch = data;
        }

        public bool IsCompressed { get; protected set;}
        public PubKey PubKey { get; protected set;}

        public byte[] ToBytes()
        {
            return vch;
        }
    }
}