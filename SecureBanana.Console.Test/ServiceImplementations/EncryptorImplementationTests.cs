using NUnit.Framework;
using System.IO;
using System.Linq;
using SecureBanana.Console.Abstractions;
using Moq;

namespace SecureBanana.Console.ServiceImplementations
{
	[TestFixture]
	public class EncryptorImplementationTests
	{
		#region Test Dependencies

		public class DummyEncryptor : System.Security.Cryptography.ICryptoTransform
		{
			public System.Func<byte, byte> ByteTransform { get; private set; }
			public int BlockSize { get; private set; }

			public DummyEncryptor(System.Func<byte, byte> byteTransform, int blockSize)
			{
				ByteTransform = byteTransform;
				BlockSize = blockSize;
			}

			#region ICryptoTransform Members

			public bool CanReuseTransform { get { return true; } }

			public bool CanTransformMultipleBlocks { get { return true; } }

			public int InputBlockSize { get { return BlockSize; } }

			public int OutputBlockSize { get { return BlockSize; } }

			public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
			{
				int i = 0;
				for (i = 0; i < inputCount; i++)
				{
					outputBuffer[i + outputOffset] = ByteTransform(inputBuffer[i + inputOffset]);
				}

				return i;
			}

			public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
			{
				byte[] result = new byte[inputCount];
				TransformBlock(inputBuffer, inputOffset, inputCount, result, 0);
				return result;
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{ }

			#endregion
		}

		public class DummyStream : MemoryStream
		{
			public DummyStream() : base() { }
			public DummyStream(byte[] bytes) : base(bytes) { }

			public override void Close() { }

			protected override void Dispose(bool disposing) { }

			public void BaseClose()
			{
				base.Close();
			}

			public void BaseDispose(bool disposing)
			{
				base.Dispose(disposing);
			}
		}

		#endregion

		[Test]
		public void Constructor_NullAlgorithm()
		{
			try
			{
				new EncryptorImplementation(null, Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Assert.Fail("An ArgumentNulLException was expected.");
			}
			catch (System.ArgumentNullException) { }
		}

		[Test]
		public void Constructor_NullHashAlgorithm()
		{
			try
			{
				new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), null, Mock.Of<IFile>());
				Assert.Fail("An ArgumentNulLException was expected.");
			}
			catch (System.ArgumentNullException) { }
		}

		[Test]
		public void Constructor_NullFileSystem()
		{
			try
			{
				new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), null);
				Assert.Fail("An ArgumentNulLException was expected.");
			}
			catch (System.ArgumentNullException) { }
		}

		[Test]
		public void Constructor_AlgorithmSet()
		{
			var algorithm = Mock.Of<IAESAlgorithm>();
			var target = new EncryptorImplementation(algorithm, Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());

			Assert.AreEqual(algorithm, target.Algorithm);
		}

		[Test]
		public void Constructor_HashAlgorithmSet()
		{
			var hashAlgorithm = Mock.Of<IHashAlgorithm>();
			var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), hashAlgorithm, Mock.Of<IFile>());

			Assert.AreEqual(hashAlgorithm, target.HashAlgorithm);
		}

		[Test]
		public void Constructor_FileSystemSet()
		{
			var fileSystem = Mock.Of<IFile>();
			var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), fileSystem);

			Assert.AreEqual(fileSystem, target.FileSystem);
		}

		[Test]
		public void EncryptTest_NullInput()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Encrypt(null, Mock.Of<System.IO.Stream>(), "TestPassword");
				Assert.Fail("An ArgumentNullException was expected.");
			}
			catch(System.ArgumentNullException) { }
		}

		[Test]
		public void EncryptTest_NullOutput()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Encrypt(Mock.Of<System.IO.Stream>(), null, "TestPassword");
				Assert.Fail("An ArgumentNullException was expected.");
			}
			catch(System.ArgumentNullException) { }
		}

		[Test]
		public void EncryptTest_NullPassword()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Encrypt(Mock.Of<System.IO.Stream>(), Mock.Of<System.IO.Stream>(), null);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void EncryptTest_EmptyPassword()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Encrypt(Mock.Of<System.IO.Stream>(), Mock.Of<System.IO.Stream>(), string.Empty);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void EncryptTest()
		{
			byte[] bytesToEncrypt = new byte[] { 1, 2, 3, 4, 5, 6 };
			byte[] expectedEncryptedBytes = new byte[] { 2, 4, 6, 8, 10, 12 };
			byte[] hashedPasswordBytes = new byte[] {
														1, 2, 3, 4, 5, 6, 7, 8,
														1, 2, 3, 4, 5, 6, 7, 7,
														1, 2, 3, 4, 5, 6, 7, 6,
														1, 2, 3, 4, 5, 6, 7, 5,
														1, 2, 3, 4, 5, 6, 7, 4,
														1, 2, 3, 4, 5, 6, 7, 3,
														1, 2, 3, 4, 5, 6, 7, 2,
														1, 2, 3, 4, 5, 6, 7, 1,	
													};
			string password = "TestRandomPassword";
			System.Func<byte, byte> transformMethod = (a) =>
			{
				if(a >= 127)
					return System.Byte.MaxValue;
				return (byte)(a * 2);
			};

			MemoryStream streamToEncrypt = new MemoryStream(bytesToEncrypt);
			MemoryStream outputStream = new MemoryStream();

			Mock<IHashAlgorithm> hashMock = new Mock<IHashAlgorithm>();
			Mock<IAESAlgorithm> aesMock = new Mock<IAESAlgorithm>();
			DummyEncryptor transform = new DummyEncryptor(transformMethod, 16);

			hashMock.Setup(a => a.ComputeHash(It.IsAny<byte[]>())).Returns(hashedPasswordBytes);
			aesMock.Setup(a => a.CreateEncryptor(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(transform);

			EncryptorImplementation encryptor = new EncryptorImplementation(aesMock.Object, hashMock.Object, Mock.Of<IFile>());

			encryptor.Encrypt(streamToEncrypt, outputStream, password);

			outputStream.Position = 0;
			CollectionAssert.AreEqual(expectedEncryptedBytes, outputStream.ToArray());
		}	

		[Test]
		public void DecryptTest_NullOutput()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Decrypt(Mock.Of<System.IO.Stream>(), null, "TestPassword");
				Assert.Fail("An ArgumentNullException was expected.");
			}
			catch(System.ArgumentNullException) { }
		}

		[Test]
		public void DecryptTest_NullPassword()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Decrypt(Mock.Of<System.IO.Stream>(), Mock.Of<System.IO.Stream>(), null);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void DecryptTest_EmptyPassword()
		{
			try
			{
				var target = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				target.Decrypt(Mock.Of<System.IO.Stream>(), Mock.Of<System.IO.Stream>(), string.Empty);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void DecryptTest()
		{
			byte[] bytesToDecrypt = new byte[] { 1, 2, 3, 4, 5, 6 };
			byte[] expectedDecryptedBytes = new byte[] { 2, 4, 6, 8, 10, 12 };
			byte[] hashedPasswordBytes = new byte[] {
														1, 2, 3, 4, 5, 6, 7, 8,
														1, 2, 3, 4, 5, 6, 7, 7,
														1, 2, 3, 4, 5, 6, 7, 6,
														1, 2, 3, 4, 5, 6, 7, 5,
														1, 2, 3, 4, 5, 6, 7, 4,
														1, 2, 3, 4, 5, 6, 7, 3,
														1, 2, 3, 4, 5, 6, 7, 2,
														1, 2, 3, 4, 5, 6, 7, 1,	
													};
			string password = "TestRandomPassword";
			System.Func<byte, byte> transformMethod = (a) =>
			{
				if(a >= 127)
					return System.Byte.MaxValue;
				return (byte)(a * 2);
			};

			MemoryStream streamToDecrypt = new MemoryStream(bytesToDecrypt);
			MemoryStream outputStream = new MemoryStream();

			Mock<IHashAlgorithm> hashMock = new Mock<IHashAlgorithm>();
			Mock<IAESAlgorithm> aesMock = new Mock<IAESAlgorithm>();
			DummyEncryptor transform = new DummyEncryptor(transformMethod, 16);

			hashMock.Setup(a => a.ComputeHash(It.IsAny<byte[]>())).Returns(hashedPasswordBytes);
			aesMock.Setup(a => a.CreateDecryptor(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(transform);

			EncryptorImplementation Encryptor = new EncryptorImplementation(aesMock.Object, hashMock.Object, Mock.Of<IFile>());

			Encryptor.Decrypt(streamToDecrypt, outputStream, password);

			outputStream.Position = 0;
			CollectionAssert.AreEqual(expectedDecryptedBytes, outputStream.ToArray());
		}	

		[Test]
		public void Encrypt_File_NullInput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt(null, "TestOutput", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File_EmptyInput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt(string.Empty, "TestOutput", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File_WhiteSpaceInput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("\t", "TestOutput", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}
		
		[Test]
		public void Encrypt_File_NullOutput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("TestInput", null, "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File_EmptyOutput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("TestInput", string.Empty, "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File_WhiteSpaceOutput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("TestInput", "\t", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}
		
		[Test]
		public void Encrypt_File_NullPassword()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("TestInput", "TestOutput", null);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File_EmptyPassword()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("TestInput", "TestOutput", string.Empty);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File_WhiteSpacePassword()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Encrypt("TestInput", "TestOutput", "\t");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Encrypt_File()
		{
			byte[] bytesToEncrypt = new byte[] { 1, 2, 3, 4, 5, 6 };
			byte[] expectedEncryptedBytes = new byte[] { 2, 4, 6, 8, 10, 12 };
			byte[] hashedPasswordBytes = new byte[] {
														1, 2, 3, 4, 5, 6, 7, 8,
														1, 2, 3, 4, 5, 6, 7, 7,
														1, 2, 3, 4, 5, 6, 7, 6,
														1, 2, 3, 4, 5, 6, 7, 5,
														1, 2, 3, 4, 5, 6, 7, 4,
														1, 2, 3, 4, 5, 6, 7, 3,
														1, 2, 3, 4, 5, 6, 7, 2,
														1, 2, 3, 4, 5, 6, 7, 1,	
													};
			string inputFilePath = "TestInputFilePath";
			string outputFilePath = "TestOutputFilePath";
			string password = "TestRandomPassword";
			System.Func<byte, byte> transformMethod = (a) =>
			{
				if(a >= 127)
					return System.Byte.MaxValue;
				return (byte)(a * 2);
			};

			DummyStream streamToEncrypt = new DummyStream(bytesToEncrypt);
			DummyStream outputStream = new DummyStream();

			Mock<IHashAlgorithm> hashMock = new Mock<IHashAlgorithm>();
			Mock<IAESAlgorithm> aesMock = new Mock<IAESAlgorithm>();
			Mock<IFile> fileMock = new Mock<IFile>();
			DummyEncryptor transform = new DummyEncryptor(transformMethod, 16);

			hashMock.Setup(a => a.ComputeHash(It.IsAny<byte[]>())).Returns(hashedPasswordBytes);
			aesMock.Setup(a => a.CreateEncryptor(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(transform);
			fileMock.Setup(a => a.Exists(inputFilePath)).Returns(true);
			fileMock.Setup(a => a.Exists(outputFilePath)).Returns(false);
			fileMock.Setup(a => a.OpenRead(inputFilePath)).Returns(streamToEncrypt);
			fileMock.Setup(a => a.OpenWrite(outputFilePath)).Returns(outputStream);

			EncryptorImplementation encryptor = new EncryptorImplementation(aesMock.Object, hashMock.Object, fileMock.Object);

			encryptor.Encrypt(inputFilePath, outputFilePath, password);

			outputStream.Position = 0;
			CollectionAssert.AreEqual(expectedEncryptedBytes, outputStream.ToArray());
		}

		[Test]
		public void Decrypt_File_NullInput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt(null, "TestOutput", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File_EmptyInput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt(string.Empty, "TestOutput", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File_WhiteSpaceInput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("\t", "TestOutput", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}
		
		[Test]
		public void Decrypt_File_NullOutput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("TestInput", null, "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File_EmptyOutput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("TestInput", string.Empty, "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File_WhiteSpaceOutput()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("TestInput", "\t", "TestPassword");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}
		
		[Test]
		public void Decrypt_File_NullPassword()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("TestInput", "TestOutput", null);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File_EmptyPassword()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("TestInput", "TestOutput", string.Empty);
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File_WhiteSpacePassword()
		{
			try
			{
				EncryptorImplementation Encryptor = new EncryptorImplementation(Mock.Of<IAESAlgorithm>(), Mock.Of<IHashAlgorithm>(), Mock.Of<IFile>());
				Encryptor.Decrypt("TestInput", "TestOutput", "\t");
				Assert.Fail("An ArgumentException was expected.");
			}
			catch(System.ArgumentException) { }
		}

		[Test]
		public void Decrypt_File()
		{
			byte[] bytesToDecrypt = new byte[] { 1, 2, 3, 4, 5, 6 };
			byte[] expectedDecryptedBytes = new byte[] { 2, 4, 6, 8, 10, 12 };
			byte[] hashedPasswordBytes = new byte[] {
														1, 2, 3, 4, 5, 6, 7, 8,
														1, 2, 3, 4, 5, 6, 7, 7,
														1, 2, 3, 4, 5, 6, 7, 6,
														1, 2, 3, 4, 5, 6, 7, 5,
														1, 2, 3, 4, 5, 6, 7, 4,
														1, 2, 3, 4, 5, 6, 7, 3,
														1, 2, 3, 4, 5, 6, 7, 2,
														1, 2, 3, 4, 5, 6, 7, 1,	
													};
			string inputFilePath = "TestInputFilePath";
			string outputFilePath = "TestOutputFilePath";
			string password = "TestRandomPassword";
			System.Func<byte, byte> transformMethod = (a) =>
			{
				if(a >= 127)
					return System.Byte.MaxValue;
				return (byte)(a * 2);
			};

			DummyStream streamToDecrypt = new DummyStream(bytesToDecrypt);
			DummyStream outputStream = new DummyStream();

			Mock<IHashAlgorithm> hashMock = new Mock<IHashAlgorithm>();
			Mock<IAESAlgorithm> aesMock = new Mock<IAESAlgorithm>();
			Mock<IFile> fileMock = new Mock<IFile>();
			DummyEncryptor transform = new DummyEncryptor(transformMethod, 16);

			hashMock.Setup(a => a.ComputeHash(It.IsAny<byte[]>())).Returns(hashedPasswordBytes);
			aesMock.Setup(a => a.CreateDecryptor(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(transform);
			fileMock.Setup(a => a.Exists(inputFilePath)).Returns(true);
			fileMock.Setup(a => a.Exists(outputFilePath)).Returns(false);
			fileMock.Setup(a => a.OpenRead(inputFilePath)).Returns(streamToDecrypt);
			fileMock.Setup(a => a.OpenWrite(outputFilePath)).Returns(outputStream);

			EncryptorImplementation Encryptor = new EncryptorImplementation(aesMock.Object, hashMock.Object, fileMock.Object);

			Encryptor.Decrypt(inputFilePath, outputFilePath, password);

			outputStream.Position = 0;
			CollectionAssert.AreEqual(expectedDecryptedBytes, outputStream.ToArray());
		}
	}
}