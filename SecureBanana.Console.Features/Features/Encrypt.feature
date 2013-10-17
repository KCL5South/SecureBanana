Feature: Encrypting and Decrypting Files
	As a user of SecureBanana
	I need to be able to encrypt files 
	So that I am able to secure data or information

Scenario: Encrypt and decrypt a file
	When I have a file called ".\FileToEncrypt.txt"
	And ".\FileToEncrypt.txt" has content of:
	"""
Some random Text that I need to encrypt.
	"""
	And I pass the following arguments to SecureBanana:
	"""
-encrypt=.\FileToEncrypt.txt -output=.\EncryptedFile.txt -password=GarbleFlagarblePassword
	"""
	And SecureBanana is ran for encryption
	And I pass the following arguments to SecureBanana:
	
	"""
-decrypt=.\EncryptedFile.txt -output=.\DecryptedFile.txt -password=GarbleFlagarblePassword
	"""
	And SecureBanana is ran for encryption
	Then the content of ".\EncryptedFile.txt" should not be:
	"""
Some random Text that I need to encrypt.
	"""
	And the content of ".\DecryptedFile.txt" should be:
	"""
Some random Text that I need to encrypt.
	"""