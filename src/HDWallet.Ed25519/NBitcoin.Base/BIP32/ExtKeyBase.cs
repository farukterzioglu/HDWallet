using System;
namespace NBitcoin.Base
{
    public abstract class ExtKeyBase<TKey> where TKey : Key
    {
        private const int ChainCodeLength = 32;
        protected byte[] vchChainCode = new byte[ChainCodeLength];
        public byte[] ChainCode
		{
			get
			{
				byte[] chainCodeCopy = new byte[ChainCodeLength];
				Buffer.BlockCopy(vchChainCode, 0, chainCodeCopy, 0, ChainCodeLength);

				return chainCodeCopy;
			}
		}

        protected TKey key; 
		
		/// <summary>
		/// Get the private key of this extended key.
		/// </summary>
		public TKey PrivateKey
		{
			get
			{
				return key;
			}
		}


		protected byte[] seed;
		protected ExtKeyBase(byte[] seedBytes)
		{
			seed = seedBytes;
			(key, vchChainCode) = SetMaster();
		}

        protected abstract (TKey Key, byte[] ChainCode) SetMaster();
    }
}