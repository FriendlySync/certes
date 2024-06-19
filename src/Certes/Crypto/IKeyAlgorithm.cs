namespace Certes.Crypto
{
    internal interface IKeyAlgorithm
    {
        ISigner CreateSigner(IKey keyPair);
        IKey GenerateKey(int? keySize = null);
        IKey GetKey(byte[] der);
        IKey GetKey(string pem);
    }
}
