using System;
namespace NBitcoin.Base
{
    public abstract class ExtKeyBase<TKey, TExtKey> 
		where TKey : KeyBase<TKey> 
		where TExtKey : ExtKeyBase<TKey, TExtKey>, new()
    {
		byte nDepth;
		/// <summary>
		/// Gets the depth of this extended key from the root key.
		/// </summary>
		public byte Depth
		{
			get
			{
				return nDepth;
			}
		}

		uint nChild;
		/// <summary>
		/// Gets the child number of this key (in reference to the parent).
		/// </summary>
		public uint Child
		{
			get
			{
				return nChild;
			}
		}

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

		protected ExtKeyBase()
		{
		}

        protected abstract (TKey Key, byte[] ChainCode) SetMaster();


		/// <summary>
		/// Derives a new extended key in the hierarchy as the given child number.
		/// </summary>
		public TExtKey Derive(uint index)
		{
			var result = new TExtKey()
			{
				nDepth = (byte)(nDepth + 1),
				// parentFingerprint = this.key.PubKey.GetHDFingerPrint(),
				nChild = index
			};
			result.key = key.Derivate(this.vchChainCode, index, out result.vchChainCode);
			return result;
		}
    }
}