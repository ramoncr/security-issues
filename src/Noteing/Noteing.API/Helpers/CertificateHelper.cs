using System.Security.Cryptography.X509Certificates;

namespace Noteing.API.Helpers
{
    internal static class CertificateHelper
    {
        internal static X509Certificate2 LoadSigningCertificate()
        {
#pragma warning disable SYSLIB0026

            var cert = new X509Certificate2("./Certs/rsaCert.pfx", "1234");

            return cert;
#pragma warning restore SYSLIB0026
        }
    }
}
