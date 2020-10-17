using System;
namespace NBitcoin
{
    public abstract class ExtKeyBase
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

        protected byte[] key; 
		
		/// <summary>
		/// Get the private key of this extended key.
		/// </summary>
		public byte[] PrivateKey
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

        protected abstract (byte[] Key, byte[] ChainCode) SetMaster();
    }
}