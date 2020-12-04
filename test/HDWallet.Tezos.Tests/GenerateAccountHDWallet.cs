using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Tezos.Tests
{
    public class GenerateAccountHDWallet
    {
        [Test]
        public void ShouldCreateAccount()
        {
            string words = "hen gaze tank solid purchase beef six earth donor leopard valve crystal dizzy donkey leopard";
            IHDWallet<TezosWallet> wallet = new TezosHDWalletSecp256k1(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); // m/44'/1729'/0'/0/0

            var publicKey = account0wallet0.PublicKey.ToHex();
            Assert.AreEqual("024790e31fd295d2ed7eee9b77da60403cc96a8088a0cb120509c3c8999ea4cd8b", publicKey);
        }
    }
}