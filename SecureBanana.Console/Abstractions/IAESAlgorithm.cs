namespace SecureBanana.Console.Abstractions
{
	public interface IAESAlgorithm : System.IDisposable
	{
		int BlockSize { set; get; }
		int FeedbackSize { set; get; }
		byte[] IV { set; get; }
		byte[] Key { set; get; }
		int KeySize { set; get; }
		System.Security.Cryptography.KeySizes[] LegalBlockSizes { get; }
		System.Security.Cryptography.KeySizes[] LegalKeySizes { get; }
		System.Security.Cryptography.CipherMode Mode { set; get; }
		System.Security.Cryptography.PaddingMode Padding { set; get; }

		void Clear();
		System.Security.Cryptography.ICryptoTransform CreateDecryptor(byte[] key, byte[] iv);
		System.Security.Cryptography.ICryptoTransform CreateDecryptor();
		System.Security.Cryptography.ICryptoTransform CreateEncryptor(byte[] key, byte[] iv);
		System.Security.Cryptography.ICryptoTransform CreateEncryptor();
		void GenerateIV();
		void GenerateKey();
		bool ValidKeySize(int bitLength);
	}
}