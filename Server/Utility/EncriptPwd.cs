using System.Security.Cryptography;
using System.Text;

namespace GreenCoinHealth.Server.Utilities
{
    public class EncriptPwd
    {
        private const string EncriptationKey = "Password";

        public string EncryptPassword(string password)
        {
            byte[] clearByte = Encoding.Unicode.GetBytes(password);

            using (Aes Eencryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncriptationKey, new byte[]
                {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });

                Eencryptor.Key = pdb.GetBytes(32);
                Eencryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, Eencryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearByte, 0, clearByte.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }
    }
}
