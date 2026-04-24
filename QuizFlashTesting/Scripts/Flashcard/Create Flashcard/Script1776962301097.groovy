import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy

def APP_PATH        = 'A:/Coding/C#/Projects/QuizFlash/bin/Release/QuizFlash.exe'

def LOGIN_TITLE     = 'Login Window'
def STUDENT_DASH    = 'Student Dashboard'

def EMAIL           = 'kaifnathani@gmail.com'
def PASSWORD        = '1234'

def FLASHCARD_TITLE = 'WPF Automation'
def FLASHCARD_DESC  = 'Testing a WPF UI requires proper Accessibility IDs.'

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
    if (!waitForWindow(title, 10)) {
        return false
    }
    Windows.delay(3)
    Windows.switchToWindowTitle(title)
    return true
}

// ── OBJECTS ─────────────────────────────────────────────────
def emailField       = el('emailBoxLogin')
def passwordField    = el('passwordBoxLogin')
def loginButton      = el('LoginButton')

def flashcardMenuBtn = el('FlashcardsNavigatorButton')
def addFlashcardBtn  = el('AddManualFlashcardButton')
def titleField       = el('FlashCardTitleBox')
def descriptionField = el('FlashCardDescriptionBox')
def saveFlashcardBtn = el('SaveFlashcardBtn')

def logoutBtn        = el('LogoutButton')

KeywordUtil.logInfo('TEST CASE: Create Flashcard Started')

Windows.startApplication(APP_PATH)
Windows.delay(5)

KeywordUtil.logInfo('Login Flow')

stableSwitch(LOGIN_TITLE)

Windows.waitForElementPresent(emailField, 10)
Windows.waitForElementPresent(passwordField, 10)

Windows.setText(emailField, EMAIL)
Windows.setText(passwordField, PASSWORD)
Windows.click(loginButton)

if (!stableSwitch(STUDENT_DASH)) {
    KeywordUtil.markFailedAndStop('Login failed → Student dashboard not loaded')
}

KeywordUtil.logInfo('Flashcard Flow')

Windows.click(flashcardMenuBtn)
Windows.delay(2)

Windows.click(addFlashcardBtn)
Windows.delay(5)

Windows.setText(titleField, FLASHCARD_TITLE)
Windows.setText(descriptionField, FLASHCARD_DESC)
Windows.delay(1)

Windows.click(saveFlashcardBtn)
Windows.delay(2)

KeywordUtil.logInfo('Logging out')

stableSwitch(STUDENT_DASH)
Windows.click(logoutBtn)
Windows.delay(2)

Windows.closeApplication()

KeywordUtil.logInfo('TEST CASE PASSED')