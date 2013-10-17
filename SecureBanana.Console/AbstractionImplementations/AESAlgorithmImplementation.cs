namespace SecureBanana.Console.AbstractionImplementations
{
	internal class AESAlgorithmImplementation : SecureBanana.Console.Abstractions.IAESAlgorithm
	{
		private System.Security.Cryptography.AesManaged _algorithm = null;

		public AESAlgorithmImplementation()
		{
			_algorithm = new System.Security.Cryptography.AesManaged();
		}

		#region IAESAlgorithm Members

		public int BlockSize
		{
			get
			{
				return _algorithm.BlockSize;
			}
			set
			{
				_algorithm.BlockSize = value;
			}
		}
		public int FeedbackSize
		{
			get
			{
				return _algorithm.FeedbackSize;
			}
			set
			{
				_algorithm.FeedbackSize = value;
			}
		}
		public byte[] IV
		{
			get
			{
				return _algorithm.IV;
			}
			set
			{
				_algorithm.IV = value;
			}
		}
		public byte[] Key
		{
			get
			{
				return _algorithm.Key;
			}
			set
			{
				_algorithm.Key = value;
			}
		}
		public int KeySize
		{
			get
			{
				return _algorithm.KeySize;
			}
			set
			{
				_algorithm.KeySize = value;
			}
		}
		public System.Security.Cryptography.KeySizes[] LegalBlockSizes
		{
			get
			{
				return _algorithm.LegalBlockSizes;
			}
		}
		public System.Security.Cryptography.KeySizes[] LegalKeySizes
		{
			get
			{
				return _algorithm.LegalKeySizes;
			}
		}
		public System.Security.Cryptography.CipherMode Mode
		{
			get
			{
				return _algorithm.Mode;
			}
			set
			{
				_algorithm.Mode = value;
			}
		}
		public System.Security.Cryptography.PaddingMode Padding
		{
			get
			{
				return _algorithm.Padding;
			}
			set
			{
				_algorithm.Padding = value;
			}
		}

		public void Clear()
		{
			_algorithm.Clear();
		}
		public System.Security.Cryptography.ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			return _algorithm.CreateDecryptor(key, iv);
		}
		public System.Security.Cryptography.ICryptoTransform CreateDecryptor()
		{
			return _algorithm.CreateDecryptor();
		}
		public System.Security.Cryptography.ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			return _algorithm.CreateEncryptor(key, iv);
		}
		public System.Security.Cryptography.ICryptoTransform CreateEncryptor()
		{
			return _algorithm.CreateEncryptor();
		}
		public void GenerateIV()
		{
			_algorithm.GenerateIV();
		}
		public void GenerateKey()
		{
			_algorithm.GenerateKey();
		}
		public bool ValidKeySize(int bitLength)
		{
			return _algorithm.ValidKeySize(bitLength);
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			_algorithm.Dispose();
		}

		#endregion
	}
}