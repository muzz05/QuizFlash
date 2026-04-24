import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy
import com.kms.katalon.core.model.FailureHandling

def APP_PATH      = 'A:/Coding/C#/Projects/QuizFlash/bin/Release/QuizFlash.exe'
def LOGIN_TITLE   = 'Login Window'
def STUDENT_DASH  = 'Student Dashboard'
def SHARE_POPUP   = 'Share Popup'
def ERROR_POPUP   = 'Message'
def EMAIL         = 'kaifnathani@gmail.com'
def PASSWORD      = '1234'
def INVALID_CODE  = 'INVALID999'

def el(String id) {
	def obj = new WindowsTestObject(id)
	obj.setLocator(id)
	obj.setLocatorStrategy(LocatorStrategy.ACCESSIBILITY_ID)
	return obj
}

def elByName(String name) {
	def obj = new WindowsTestObject(name)
	obj.setLocator(name)
	obj.setLocatorStrategy(LocatorStrategy.NAME)
	return obj
}

def waitForWindow(String title, int retries = 10) {
	for (int i = 0; i < retries; i++) {
		try {
			Windows.switchToWindowTitle(title)
			Windows.delay(1)
			return true
		} catch (Exception e) {
			Windows.delay(1)
		}
	}
	return false
}

def stableSwitch(String title) {
	if (!waitForWindow(title, 10)) return false
	Windows.delay(3)
	Windows.switchToWindowTitle(title)
	return true
}

def emailField        = el('emailBoxLogin')
def passwordField     = el('passwordBoxLogin')
def loginButton       = el('LoginButton')
def flashcardMenuBtn  = el('FlashcardsNavigatorButton')
def shareFlashcardBtn = el('ShareFlashcardBtn')
def shareCodeField    = el('studentcodeflashcard')
def shareBtn          = el('ShareButton')
def okButton          = el('OkButton')
def logoutBtn         = el('LogoutButton')

def errorHeading      = elByName('Incorrect Code')
def errorMessage      = elByName('Please enter the correct student code')

KeywordUtil.logInfo('TEST CASE: Share Flashcard Invalid Code Started')
Windows.startApplication(APP_PATH)
Windows.delay(5)

KeywordUtil.logInfo('Logging in')
stableSwitch(LOGIN_TITLE)
Windows.waitForElementPresent(emailField, 10)
Windows.waitForElementPresent(passwordField, 10)
Windows.setText(emailField, EMAIL)
Windows.setText(passwordField, PASSWORD)
Windows.click(loginButton)

if (!stableSwitch(STUDENT_DASH)) {
	KeywordUtil.markFailedAndStop('Login failed → Student Dashboard not loaded')
}

KeywordUtil.logInfo('Navigating to Flashcards')
Windows.click(flashcardMenuBtn)
Windows.delay(2)

KeywordUtil.logInfo('Clicking Share button on Flashcard')
Windows.click(shareFlashcardBtn)
Windows.delay(2)

KeywordUtil.logInfo('Waiting for Share popup to appear')
if (!waitForWindow(SHARE_POPUP, 10)) {
	KeywordUtil.markFailedAndStop('Share popup did not appear')
}
KeywordUtil.logInfo('Share popup detected')

KeywordUtil.logInfo("Entering invalid student code: ${INVALID_CODE}")
Windows.waitForElementPresent(shareCodeField, 5)
Windows.clearText(shareCodeField)
Windows.setText(shareCodeField, INVALID_CODE)
Windows.delay(1)

KeywordUtil.logInfo('Clicking Share button inside popup')
Windows.waitForElementPresent(shareBtn, 5)
Windows.click(shareBtn)
Windows.delay(2)

KeywordUtil.logInfo('Waiting for error popup to appear')
if (!waitForWindow(ERROR_POPUP, 10)) {
	KeywordUtil.markFailedAndStop('Error popup did not appear — invalid code was not handled')
}
KeywordUtil.logInfo('Error popup detected')

KeywordUtil.logInfo('Verifying error popup content')
boolean headingCorrect = Windows.verifyElementPresent(errorHeading, 3, FailureHandling.OPTIONAL)
boolean messageCorrect = Windows.verifyElementPresent(errorMessage, 3, FailureHandling.OPTIONAL)

KeywordUtil.logInfo("Heading matched 'Incorrect Code': ${headingCorrect}")
KeywordUtil.logInfo("Message matched 'Please enter the correct student code': ${messageCorrect}")

if (!headingCorrect || !messageCorrect) {
	Windows.click(okButton)
	KeywordUtil.markFailedAndStop('Popup appeared but content did not match expected error message — validation may be broken')
}

KeywordUtil.logInfo('Error popup content verified correctly — invalid code was properly rejected')

KeywordUtil.logInfo('Closing error popup')
Windows.waitForElementPresent(okButton, 5)
Windows.click(okButton)
Windows.delay(1)

KeywordUtil.logInfo('Closing Share popup')
if (waitForWindow(SHARE_POPUP, 5)) {
	def cancelBtn = el('CancelButton')
	Windows.waitForElementPresent(cancelBtn, 5)
	Windows.click(cancelBtn)
	Windows.delay(1)
}

KeywordUtil.logInfo('Returning to Student Dashboard')
if (!stableSwitch(STUDENT_DASH)) {
	KeywordUtil.markFailedAndStop('Could not return to Student Dashboard')
}

KeywordUtil.logInfo('Logging out')
Windows.click(logoutBtn)
Windows.delay(2)

Windows.closeApplication()
KeywordUtil.logInfo('TEST CASE PASSED: Invalid Share Code Correctly Rejected')
