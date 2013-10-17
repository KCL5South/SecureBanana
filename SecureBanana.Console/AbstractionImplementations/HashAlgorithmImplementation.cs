namespace SecureBanana.Console.AbstractionImplementations
{
	internal class HashAlgorithmImplementation : SecureBanana.Console.Abstractions.IHashAlgorithm
	{
		private System.Security.Cryptography.SHA512Managed _algorithm = null;

		public HashAlgorithmImplementation()
		{
			_algorithm = new System.Security.Cryptography.SHA512Managed();
		}

		#region IHashAlgorithm Members

		public bool CanReuseTransform
		{
			get { return _algorithm.CanReuseTransform; }
		}
		public bool CanTransformMultipleBlocks
		{
			get { return _algorithm.CanTransformMultipleBlocks; }
		}
		public byte[] Hash
		{
			get { return _algorithm.Hash; }
		}
		public int HashSize
		{
			get { return _algorithm.HashSize; }
		}
		public int InputBlockSize
		{
			get { return _algorithm.InputBlockSize; }
		}
		public int OutputBlockSize
		{
			get { return _algorithm.OutputBlockSize; }
		}

		public void Clear()
		{
			_algorithm.Clear();
		}
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			return _algorithm.ComputeHash(buffer, offset, count);
		}
		public byte[] ComputeHash(byte[] buffer)
		{
			return _algorithm.ComputeHash(buffer);
		}
		public byte[] ComputeHash(System.IO.Stream inputStream)
		{
			return _algorithm.ComputeHash(inputStream);
		}
		public void Initialize()
		{
			_algorithm.Initialize();
		}
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return _algorithm.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			return _algorithm.TransformFinalBlock(inputBuffer, inputOffset, inputCount);
		}

		#endregion

		#region IDisposable Members

		public void Dispose() { _algorithm.Dispose(); }

		#endregion
	}
}