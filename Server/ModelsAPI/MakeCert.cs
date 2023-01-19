namespace RISKAPI
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    public class MakeCert
    {
        public static void CreateCert()
        {
            var ecdsa = ECDsa.Create(); // generate asymmetric key pair
            var req = new CertificateRequest("cn=foobar", ecdsa, HashAlgorithmName.SHA256);
            var cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));

            // Create PFX (PKCS #12) with private key
            File.WriteAllBytes("C:\\Users\\Charif\\Documents\\Projet\\Projet_IUT\\Semestre 3\\S3.01A - saé\\Sujet\\SAE-S3-B2-RISK\\JurassicRisk\\Server\\RISKAPI\\RiskApiCert.pfx", cert.Export(X509ContentType.Pfx, "/Riskapi123"));

            // Create Base 64 encoded CER (public key only)
            File.WriteAllText("C:\\Users\\Charif\\Documents\\Projet\\Projet_IUT\\Semestre 3\\S3.01A - saé\\Sujet\\SAE-S3-B2-RISK\\JurassicRisk\\Server\\RISKAPI\\RiskApiCert.cer",
                "-----BEGIN CERTIFICATE-----\r\n"
                + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                + "\r\n-----END CERTIFICATE-----");
        }

        public bool ValidateCertificate(X509Certificate2 clientCert)
        {
            var cert = new X509Certificate2("RiskApiCert.pfx", "/Riskapi123", X509KeyStorageFlags.Exportable);
            if (clientCert.Thumbprint == cert.Thumbprint)
            {
                return true;
            }
            return false;
        }
    }
}
