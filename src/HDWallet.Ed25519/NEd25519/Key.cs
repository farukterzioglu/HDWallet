using System;
using NBitcoin.Base;

namespace NEd25519
{
    public class Ed25519Key : Key
    {
        public Ed25519Key(byte[] data) : base(data)
        {
        }
    }
}