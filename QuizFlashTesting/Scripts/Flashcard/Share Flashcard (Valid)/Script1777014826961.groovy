import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy

def APP_PATH     = 'A:/Coding/C#/Projects/QuizFlash/bin/Release/QuizFlash.exe'
def LOGIN_TITLE  = 'Login Window'
def STUDENT_DASH = 'Student Dashboard'
def SHARE_POPUP  = 'Share Popup'
def SUCCESS_POPUP = 'Message'
def EMAIL        = 'kaifnathani@gmail.com'
def PASSWORD     = '1234'
def SHARE_CODE   = 'JNP9UN'

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

def successHeading    = elByName('Share Success')
def successMessage    = elByName('Flashcard has been shared successfully')

KeywordUtil.logInfo('TEST CASE: Share Flashcard Started')
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

KeywordUtil.logInfo('Entering student share code')
Windows.waitForElementPresent(shareCodeField, 5)
Windows.clearText(shareCodeField)
Windows.setText(shareCodeField, SHARE_CODE)
Windows.delay(1)

KeywordUtil.logInfo('Clicking Share button inside popup')
Windows.waitForElementPresent(shareBtn, 5)
Windows.click(shareBtn)
Windows.delay(2)

KeywordUtil.logInfo('Waiting for success popup to appear')
if (!waitForWindow(SUCCESS_POPUP, 10)) {
    KeywordUtil.markFailedAndStop('Success popup did not appear — share may have failed')
}
KeywordUtil.logInfo('Success popup detected')

KeywordUtil.logInfo('Verifying success popup content')
boolean headingCorrect = Windows.verifyElementPresent(successHeading, 3, 
    com.kms.katalon.core.model.FailureHandling.OPTIONAL)
boolean messageCorrect = Windows.verifyElementPresent(successMessage, 3, 
    com.kms.katalon.core.model.FailureHandling.OPTIONAL)

if (!headingCorrect || !messageCorrect) {
    // Content did not match — this might be an error popup instead of success
    KeywordUtil.logInfo("Heading matched: ${headingCorrect}")
    KeywordUtil.logInfo("Message matched: ${messageCorrect}")
    Windows.click(okButton)
    KeywordUtil.markFailedAndStop('Popup appeared but content did not match success message — share failed')
}

KeywordUtil.logInfo('Success popup content verified correctly')

KeywordUtil.logInfo('Closing success popup')
Windows.waitForElementPresent(okButton, 5)
Windows.click(okButton)
Windows.delay(1)

KeywordUtil.logInfo('Returning to Student Dashboard')
if (!stableSwitch(STUDENT_DASH)) {
    KeywordUtil.markFailedAndStop('Could not return to Student Dashboard after sharing')
}

KeywordUtil.logInfo('Logging out')
Windows.click(logoutBtn)
Windows.delay(2)

Windows.closeApplication()
KeywordUtil.logInfo('TEST CASE PASSED: Share Flashcard Verified Successfully')