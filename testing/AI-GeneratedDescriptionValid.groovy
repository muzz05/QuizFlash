import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy

def APP_PATH     = 'A:/Coding/C#/Projects/QuizFlash/bin/Release/QuizFlash.exe'
def LOGIN_TITLE  = 'Login Window'
def STUDENT_DASH = 'Student Dashboard'
def EMAIL        = 'kaifnathani@gmail.com'
def PASSWORD     = '1234'
def FLASHCARD_TITLE = 'AI Test Flashcard'

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

def emailField        = el('emailBoxLogin')
def passwordField     = el('passwordBoxLogin')
def loginButton       = el('LoginButton')
def flashcardMenuBtn  = el('FlashcardsNavigatorButton')
def addFlashcardBtn   = el('AddManualFlashcardButton')
def titleField        = el('FlashCardTitleBox')
def saveFlashcardBtn  = el('SaveFlashcardBtn')
def aiButton          = el('GenerateAIButton')
def descriptionBox    = el('FlashCardDescriptionBox')
def logoutBtn         = el('LogoutButton')

KeywordUtil.logInfo('TEST CASE: AI Description Generation Started')

Windows.startApplication(APP_PATH)
Windows.delay(5)

stableSwitch(LOGIN_TITLE)

Windows.setText(emailField, EMAIL)
Windows.setText(passwordField, PASSWORD)
Windows.click(loginButton)

if (!stableSwitch(STUDENT_DASH)) {
	KeywordUtil.markFailedAndStop('Login failed')
}

Windows.click(flashcardMenuBtn)
Windows.delay(2)

Windows.click(addFlashcardBtn)
Windows.delay(3)

Windows.setText(titleField, FLASHCARD_TITLE)
Windows.delay(1)

KeywordUtil.logInfo('Clicking AI Generate Button')

Windows.click(aiButton)

boolean generated = false

for (int i = 0; i < 10; i++) {
	try {
		String text = Windows.getText(descriptionBox)

		if (text != null &&
			!text.trim().isEmpty() &&
			!text.contains("Enter description")) {

			generated = true
			break
		}
	} catch (Exception e) {}

	Windows.delay(1)
}

if (!generated) {
	KeywordUtil.markFailedAndStop('AI description not generated within 10 seconds')
}

KeywordUtil.logInfo('AI Description generated successfully')

stableSwitch(STUDENT_DASH)
Windows.click(logoutBtn)
Windows.delay(2)

Windows.closeApplication()

KeywordUtil.logInfo('TEST CASE PASSED')
