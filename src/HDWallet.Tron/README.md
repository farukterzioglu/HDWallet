### Tron HD Wallet

An HD wallet for for Tron. 
Tron HD wallets can be generated from mnemonic (w/ or w/o passphrase) or from extended private key (xprv) and non-hd wallets can be generated from Tron private key.  

Tron HD wallets can derive sub accounts, and from that accounts external (deposit) wallets or internal (change) wallets can be derived. Using generated wallets, addresses can be retrived.

#### Samples  
Sample for generating HD Wallet for Tron (purpose: BIP44, coin type: 195) from mnemonic and getting the first account's first deposit wallet;  
```csharp
IHDWallet<TronWallet> tronHDWallet = new TronHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
var account0 = tronHDWallet.GetAccount(0);
TronWallet wallet0 = account0.GetExternalWallet(0);

Assert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ",wallet0.Address);
```  

Sample for generating Tron Wallet (non-hd) from private key;  
```csharp
var tronWallet = new TronWallet("fa0a0d3dcd475a04d99cf777dc166e2160f88fbd1c8bdeca74bdffb61430e7d9");
Assert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ",tronWallet.Address );
```

Sample to create & sign & broadcast Tron (TRX) transaction;  
```csharp
IHDWallet<TronWallet> wallet = new TronHDWallet("push wrong tribe amazing again cousin hill belt silent found sketch monitor");
TronWallet wallet0 = wallet.GetAccount(0).GetExternalWallet(0);
string wallet0Address = wallet0.Address;

var url = "https://api.shasta.trongrid.io";

// Create transaction
string resultContent;
using (var client = new HttpClient(){ BaseAddress = new Uri(url)})
{
    var req = new {
        to_address = "TEmqPQsgkdHijWkLX6sxJRTcaRVfBTMGpn",
        owner_address = "TWroNNekzseGNC6x1BHGd5H7f9b9u6mdHE",
        amount = "10000"
    };

    var serializedData = JsonConvert.SerializeObject(req);
    
    var requestData = new StringContent(
        serializedData,
        Encoding.UTF8,
        "application/json");

    var result = await client.PostAsync("wallet/createtransaction", requestData);
    resultContent = await result.Content.ReadAsStringAsync();
}

var transaction = JsonConvert.DeserializeObject<TrxTransaction>(resultContent);

// Sign
var txId = Helper.FromHexToByteArray(transaction.txID);
Signature signature = wallet0.Sign(txId);
TronSignature tronSignature = new TronSignature(signature);
var signatureHex = Helper.ToHexString(tronSignature.SignatureBytes);
transaction.signature.Add(signatureHex);

// Broadcast
using (var client = new HttpClient(){ BaseAddress = new Uri(url)})
{
    var req = new {
        raw_data = JsonConvert.SerializeObject(transaction.raw_data),
        txID = transaction.txID,
        signature = new List<string>()
    };
    req.signature.Add(transaction.signature.First());

    var serializedData = JsonConvert.SerializeObject(req);
    
    var requestData = new StringContent(
        serializedData,
        Encoding.UTF8,
        "application/json");

    var result = await client.PostAsync("wallet/broadcasttransaction", requestData);
    resultContent = await result.Content.ReadAsStringAsync();
}

```