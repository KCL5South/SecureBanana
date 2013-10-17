Feature: Output Help when it is requested and in some cases when it is not.
	SecureBanana need to be able to output usage directions when the user requests them
	and when the user passes incorrect arguments to SecureBanana.

Scenario: Only the -Help argument
	When the following arguments are sent to SecureBanana:
	"""
-Help
	""" 
	And SecureBanana is ran
	Then stdout should start with:
	"""
Secure Banana
Squadron 5 South
----------------
Usage: SecureBanana [Options]

	"""

Scenario: Only the -help argument (Case Insensitive)
	When the following arguments are sent to SecureBanana:
	"""
-help
	""" 
	And SecureBanana is ran
	Then stdout should start with:
	"""
Secure Banana
Squadron 5 South
----------------
Usage: SecureBanana [Options]

	"""

Scenario: Only the -? argument
	When the following arguments are sent to SecureBanana:
	"""
-?
	""" 
	And SecureBanana is ran
	Then stdout should start with:
	"""
Secure Banana
Squadron 5 South
----------------
Usage: SecureBanana [Options]

	"""

Scenario: When an unknown argument is passed
	When the following arguments are sent to SecureBanana:
	"""
asdfasdfoawieveniiiavh  asdovod e enenkkk -aaaaaaa
	""" 
	And SecureBanana is ran
	Then stdout should start with:
	"""
Secure Banana
Squadron 5 South
----------------
Usage: SecureBanana [Options]

	"""

Scenario: When a known argument is passed along with -Help 
	When the following arguments are sent to SecureBanana:
	"""
-password=TestPassword -help
	""" 
	And SecureBanana is ran
	Then stdout should start with:
	"""
Secure Banana
Squadron 5 South
----------------
Usage: SecureBanana [Options]

	"""