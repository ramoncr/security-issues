// See https://aka.ms/new-console-template for more information
using CertificateManager;
using CertificateManager.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");


var sp = new ServiceCollection()
       .AddCertificateManager()
       .BuildServiceProvider();

var cc = sp.GetService<CreateCertificates>();

var rsaCert = CreateRsaCertificate(cc, "localhost", 10);
var ecdsaCert = CreateECDsaCertificate(cc, "localhost", 10);

string password = "1234";
var iec = sp.GetService<ImportExportCertificate>();

var rsaCertPfxBytes =
    iec.ExportSelfSignedCertificatePfx(password, rsaCert);
File.WriteAllBytes("rsaCert.pfx", rsaCertPfxBytes);

var ecdsaCertPfxBytes =
    iec.ExportSelfSignedCertificatePfx(password, ecdsaCert);
File.WriteAllBytes("ecdsaCert.pfx", ecdsaCertPfxBytes);


static X509Certificate2 CreateRsaCertificate(CreateCertificates cc,
    string dnsName, int validityPeriodInYears)
{
    var basicConstraints = new BasicConstraints
    {
        CertificateAuthority = false,
        HasPathLengthConstraint = false,
        PathLengthConstraint = 0,
        Critical = false
    };

    var subjectAlternativeName = new SubjectAlternativeName
    {
        DnsName = new List<string> { dnsName }
    };

    var x509KeyUsageFlags = X509KeyUsageFlags.DigitalSignature;

    // only if certification authentication is used
    var enhancedKeyUsages = new OidCollection
    {
        new Oid("1.3.6.1.5.5.7.3.1"),  // TLS Server auth
        new Oid("1.3.6.1.5.5.7.3.2"),  // TLS Client auth
    };

    var certificate = cc.NewRsaSelfSignedCertificate(
        new DistinguishedName { CommonName = dnsName },
        basicConstraints,
        new ValidityPeriod
        {
            ValidFrom = DateTimeOffset.UtcNow,
            ValidTo = DateTimeOffset.UtcNow.AddYears(validityPeriodInYears)
        },
        subjectAlternativeName,
        enhancedKeyUsages,
        x509KeyUsageFlags,
        new RsaConfiguration { KeySize = 2048 }
    );

    return certificate;
}

static X509Certificate2 CreateECDsaCertificate(CreateCertificates cc,
    string dnsName, int validityPeriodInYears)
{
    var basicConstraints = new BasicConstraints
    {
        CertificateAuthority = false,
        HasPathLengthConstraint = false,
        PathLengthConstraint = 0,
        Critical = false
    };

    var san = new SubjectAlternativeName
    {
        DnsName = new List<string> { dnsName }
    };

    var x509KeyUsageFlags = X509KeyUsageFlags.DigitalSignature;

    // only if certification authentication is used
    var enhancedKeyUsages = new OidCollection {
        new Oid("1.3.6.1.5.5.7.3.1"),  // TLS Server auth
        new Oid("1.3.6.1.5.5.7.3.2"),  // TLS Client auth
    };

    var certificate = cc.NewECDsaSelfSignedCertificate(
        new DistinguishedName { CommonName = dnsName },
        basicConstraints,
        new ValidityPeriod
        {
            ValidFrom = DateTimeOffset.UtcNow,
            ValidTo = DateTimeOffset.UtcNow.AddYears(validityPeriodInYears)
        },
        san,
        enhancedKeyUsages,
        x509KeyUsageFlags,
        new ECDsaConfiguration());

    return certificate;
}