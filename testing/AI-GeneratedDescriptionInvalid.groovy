import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy

def APP_PATH     = 'A:/Coding/C#/Projects/QuizFlash/bin/Release/QuizFlash.exe'
def LOGIN_TITLE  = 'Login Window'
def STUDENT_DASH = 'Student Dashboard'
def POPUP_TITLE  = 'Message'
def EMAIL        = 'kaifnathani@gmail.com'
def PASSWORD     = '1234'

def el(String id) {
    def obj = new WindowsTestObject(id)
    obj.setLocator(id)
    obj.setLocatorStrategy(LocatorStrategy.ACCESSIBILITY_ID)
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

def emailField       = el('emailBoxLogin')
def passwordField    = el('passwordBoxLogin')
def loginButton      = el('LoginButton')
def flashcardMenuBtn = el('FlashcardsNavigatorButton')
def addFlashcardBtn  = el('AddManualFlashcardButton')
def titleField       = el('FlashCardTitleBox')
def generateAIButton = el('GenerateAIButton')
def okButton         = el('OkButton')
def logoutBtn        = el('LogoutButton')

KeywordUtil.logInfo('TEST CASE: AI Empty Title Popup Started')
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

KeywordUtil.logInfo('Adding new Flashcard')
Windows.click(addFlashcardBtn)
Windows.delay(2)

KeywordUtil.logInfo('Leaving title empty and clicking Generate AI Description')
Windows.clearText(titleField)
Windows.delay(1)
Windows.click(generateAIButton)
Windows.delay(2)

KeywordUtil.logInfo('Waiting for error popup to appear')
if (!waitForWindow(POPUP_TITLE, 10)) {
    KeywordUtil.markFailedAndStop('Error popup did not appear after clicking Generate AI with empty title')
}
KeywordUtil.logInfo('Popup detected successfully')

KeywordUtil.logInfo('Clicking OK button on popup')
Windows.waitForElementPresent(okButton, 5)
Windows.click(okButton)
Windows.delay(1)

KeywordUtil.logInfo('Returning to Student Dashboard')
if (!stableSwitch(STUDENT_DASH)) {
    KeywordUtil.markFailedAndStop('Could not return to Student Dashboard after closing popup')
}

KeywordUtil.logInfo('Logging out')
Windows.click(logoutBtn)
Windows.delay(2)

Windows.closeApplication()
KeywordUtil.logInfo('TEST CASE PASSED: AI Empty Title Popup Verified Successfully')
