using System.IO;
using System.Security.Cryptography;

namespace Certes.Crypto
{
    internal sealed class EllipticCurveAlgorithm : IKeyAlgorithm
    {
        private readonly string curveName;
        private readonly string signingAlgorithm;
        private readonly string hashAlgorithm;

        //private KeyAlgorithm Algorithm
        //{
        //    get
        //    {
        //        switch (curveName)
        //        {
        //            case "P-256": return KeyAlgorithm.ES256;
        //            default: return KeyAlgorithm.ES256;
        //        }
        //    }
        //}

        public EllipticCurveAlgorithm(string curveName, string signingAlgorithm, string hashAlgorithm)
        {
            this.curveName = curveName;
            this.signingAlgorithm = signingAlgorithm;
            this.hashAlgorithm = hashAlgorithm;
        }

        public ISigner CreateSigner(IKey key)
        {
            return new EllipticCurveSigner(key);
        }

        public IKey GenerateKey(int? keySize = null)
        {
            ECDsa privateECKey = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            return new ECKey(privateECKey);
        }

        public IKey GetKey(byte[] der)
        {
            ECDsa privateECKey = ECDsa.Create();
            privateECKey.ImportECPrivateKey(der, out int bytesRead);
            return new ECKey(privateECKey);
        }

        public IKey GetKey(string pem)
        {
            ECDsa privateECKey = ECDsa.Create();
            string privateECKeyText = File.ReadAllText(pem);
            privateECKey.ImportFromPem(privateECKeyText);
            return new ECKey(privateECKey);
        }
    }
}
