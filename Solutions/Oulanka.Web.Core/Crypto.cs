using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Oulanka.Web.Core
{
    public class Crypto
    {
        public enum CryptoProvider : int
        {
            /// <summary>
            /// Des
            /// </summary>
            Des = 0,
            /// <summary>
            /// RC2
            /// </summary>
            Rc2 = 1,
            /// <summary>
            /// TripleDes
            /// </summary>
            TripleDes = 2,
            /// <summary>
            /// Rijndael
            /// </summary>
            Rijndael = 3
        }

        static readonly int _encryptionMethod = 3; // overwrite in web.config
        static readonly string _encryptionKey = "r71W+2LtpTP62MKRIJWCXhWdm39RG8+/";// overwrite in web.config
        static readonly string _encryptionIv = "8RJ4OY/Yj1P79vXS";// overwrite in web.config
        string _returnValue;


        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        private static byte[] Key => Encoding.UTF8.GetBytes(_encryptionKey);

        /// <summary>
        /// Gets the IV.
        /// </summary>
        /// <value>
        /// The IV.
        /// </value>
        private static byte[] IV => Encoding.UTF8.GetBytes(_encryptionIv);

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncryptString(string value)
        {
            if (value == null) return new ArgumentNullException(nameof(value)).ToString();


            var ms = new MemoryStream();
            switch (_encryptionMethod)
            {
                case (int)CryptoProvider.Des:
                    var providerDes = new DESCryptoServiceProvider();
                    var cs = new CryptoStream(ms, providerDes.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                    var sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                case (int)CryptoProvider.Rc2:

                    var providerRc2 = new RC2CryptoServiceProvider();
                    cs = new CryptoStream(ms, providerRc2.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                case (int)CryptoProvider.TripleDes:

                    var provider3Des = new TripleDESCryptoServiceProvider();
                    cs = new CryptoStream(ms, provider3Des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                case (int)CryptoProvider.Rijndael:

                    var providerRij = new RijndaelManaged();
                    cs = new CryptoStream(ms, providerRij.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;
            }


            ms.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string DecryptString(string value)
        {
            if (value == null) return new ArgumentNullException(nameof(value)).ToString();


            var returnValue = "";
            var buffer = Convert.FromBase64String(value);
            var ms = new MemoryStream(buffer);

            switch (_encryptionMethod)
            {
                case (int)CryptoProvider.Des:
                    {
                        var providerDes = new DESCryptoServiceProvider();
                        var cs = new CryptoStream(ms, providerDes.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        returnValue = sr.ReadToEnd();

                        break;

                    }
                case (int)CryptoProvider.Rc2:
                    {
                        var providerRc2 = new RC2CryptoServiceProvider();
                        var cs = new CryptoStream(ms, providerRc2.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        returnValue = sr.ReadToEnd();

                        break;
                    }
                case (int)CryptoProvider.TripleDes:
                    {
                        var providerDes = new TripleDESCryptoServiceProvider();
                        var cs = new CryptoStream(ms, providerDes.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        returnValue = sr.ReadToEnd();

                        break;

                    }
                case (int)CryptoProvider.Rijndael:
                    {
                        var providerRij = new RijndaelManaged();
                        var cs = new CryptoStream(ms, providerRij.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        returnValue = sr.ReadToEnd();

                        break;

                    }
            }
            return returnValue;
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="value">The password.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns></returns>
        public static string EncryptString(string value, CryptoProvider provider, string key, string iv)
        {
            if (value == null) return new ArgumentNullException(nameof(value)).ToString();


            var ms = new MemoryStream();
            switch (provider)
            {
                case CryptoProvider.Des:

                    var providerDes = new DESCryptoServiceProvider();
                    var cs = new CryptoStream(ms, providerDes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Write);
                    var sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                case CryptoProvider.Rc2:
                    var providerRc2 = new RC2CryptoServiceProvider();
                    cs =
                        new CryptoStream(ms,
                            providerRc2.CreateEncryptor(Encoding.UTF8.GetBytes(key),
                                Encoding.UTF8.GetBytes(iv)),
                            CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                case CryptoProvider.TripleDes:

                    var provider3Des = new TripleDESCryptoServiceProvider();
                    cs =
                        new CryptoStream(ms,
                            provider3Des.CreateEncryptor(Encoding.UTF8.GetBytes(key),
                                Encoding.UTF8.GetBytes(iv)),
                            CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                case CryptoProvider.Rijndael:

                    var providerRij = new RijndaelManaged();
                    cs =
                        new CryptoStream(ms,
                            providerRij.CreateEncryptor(Encoding.UTF8.GetBytes(key),
                                Encoding.UTF8.GetBytes(iv)),
                            CryptoStreamMode.Write);
                    sw = new StreamWriter(cs);
                    sw.Write(value);
                    sw.Flush();
                    cs.FlushFinalBlock();

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
            }

            ms.Flush();

            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="value">The password.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns></returns>
        public string DecryptString(string value, CryptoProvider provider, string key, string iv)
        {
            if (value == null) return new ArgumentNullException(nameof(value)).ToString();


            _returnValue = "";
            var buffer = Convert.FromBase64String(value);
            var ms = new MemoryStream(buffer);
            switch (provider)
            {
                case CryptoProvider.Des:
                    {
                        var providerDes = new DESCryptoServiceProvider();
                        var cs = new CryptoStream(ms, providerDes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        _returnValue = sr.ReadToEnd();
                        break;
                    }
                case CryptoProvider.Rc2:
                    {
                        var providerRc2 = new RC2CryptoServiceProvider();
                        var cs = new CryptoStream(ms, providerRc2.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        _returnValue = sr.ReadToEnd();
                        break;
                    }
                case CryptoProvider.TripleDes:
                    {
                        var providerDes = new TripleDESCryptoServiceProvider();
                        var cs = new CryptoStream(ms, providerDes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        _returnValue = sr.ReadToEnd();
                        break;
                    }
                case CryptoProvider.Rijndael:
                    {
                        var providerRij = new RijndaelManaged();
                        var cs = new CryptoStream(ms, providerRij.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Read);
                        var sr = new StreamReader(cs);
                        _returnValue = sr.ReadToEnd();
                        break;
                    }
            }
            return _returnValue;


        }
    }
}