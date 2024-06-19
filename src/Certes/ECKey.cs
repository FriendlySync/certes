using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Certes.Jws;

namespace Certes
{
    internal class ECKey : IKey
    {
        private ECDsa privateECKey;

        public ECKey(ECDsa privateECKey)
        {
            this.privateECKey = privateECKey;
        }

        KeyAlgorithm IKey.Algorithm => Algorithm();

        JsonWebKey IKey.JsonWebKey => JsonWebKey();

        public KeyAlgorithm Algorithm()
        {
            return KeyAlgorithm.ES256;
        }

        public JsonWebKey JsonWebKey()
        {
            EcJsonWebKey ecJsonWebKey = new EcJsonWebKey();
            ecJsonWebKey.Curve = "P-256";
            ecJsonWebKey.KeyType = "EC";

            ECParameters ecParameters = privateECKey.ExportParameters(includePrivateParameters: false);
            ecJsonWebKey.X = JwsConvert.ToBase64String(ecParameters.Q.X);
            ecJsonWebKey.Y = JwsConvert.ToBase64String(ecParameters.Q.Y);

            return ecJsonWebKey;
        }

        public byte[] SignData(byte[] data)
        {
            return privateECKey.SignData(data, HashAlgorithmName.SHA256);
        }

        public byte[] ToDer()
        {
            throw new NotImplementedException();
        }

        public string ToPem()
        {
            throw new NotImplementedException();
        }
    }
}
