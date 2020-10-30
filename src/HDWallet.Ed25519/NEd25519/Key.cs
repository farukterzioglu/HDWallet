using System;
using NBitcoin.Base;

namespace NEd25519
{
    public class Ed25519Key : KeyBase<Ed25519Key>
    {
        public Ed25519Key(byte[] data) : base(data)
        {
        }

        public override Ed25519Key Derivate(byte[] chainCode, uint nChild, out byte[] chainCodeChild)
        {
            throw new NotImplementedException();
        }
    }
}