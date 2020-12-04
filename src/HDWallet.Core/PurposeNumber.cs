namespace HDWallet.Core
{
    // About CIP1852 : https://github.com/input-output-hk/implementation-decisions/blob/e2d1bed5e617f0907bc5e12cf1c3f3302a4a7c42/text/1852-hd-chimeric.md#decision-2-a-new-purpose-value 
    public enum PurposeNumber : ushort
    {
        PURPOSE0 = 0,
        BIP44 = 44,
        BIP49 = 49,
        BIP84 = 84,
        CIP1852 = 1852 
    }
}