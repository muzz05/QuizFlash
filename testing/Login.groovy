import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy

// ── CONFIG ──────────────────────────────────────────────────
def APP_PATH        = 'C:/Users/Haider Shahid/AppData/Local/QuizFlash/QuizFlash.exe' 
def APP_TITLE       = 'QuizFlash'      
def LOGIN_TITLE     = 'Login Window'        
def VALID_EMAIL     = 'asmakhan@gmail.com'
def VALID_PASSWORD  = '1234'
def WRONG_PASSWORD  = 'WrongPass999'
def INVALID_EMAIL   = 'notanemail'

def TEACHER_DASHBOARD_TITLE = 'Teacher Dashboard'
def STUDENT_DASHBOARD_TITLE = 'Student Dashboard'
def ERROR_DIALOG_TITLE      = 'Message'

// ── LAUNCH ──────────────────────────────────────────────────
Windows.startApplicationWithTitle(APP_PATH, APP_TITLE)
Windows.delay(2)

// ── SWITCH TO LOGIN WINDOW ──────────────────────────────────
Windows.switchToWindowTitle(LOGIN_TITLE)
Windows.delay(1)

// ── HELPER ──────────────────────────────────────────────────
def el(String automationId) {
	def obj = new WindowsTestObject(automationId)
	obj.setLocator(automationId)
	obj.setLocatorStrategy(LocatorStrategy.ACCESSIBILITY_ID)
	return obj
}

def findByName(String name) {
	def obj = new WindowsTestObject(name)
	obj.setLocator(name)
	obj.setLocatorStrategy(LocatorStrategy.NAME)
	return obj
}

def fillAndSubmit(String email, String password) {
	Windows.clearText(el('emailBoxLogin'))
	Windows.setText(el('emailBoxLogin'), email)
	Windows.clearText(el('passwordBoxLogin'))
	Windows.setText(el('passwordBoxLogin'), password)
	Windows.click(el('LoginButton'))
	Windows.delay(1)
}

// ── HELPER: VERIFY WINDOW EXISTS ─────────────────────────────
def verifyWindowExists(String title, int timeout = 5) {
    try {
        Windows.switchToWindowTitle(title)
        return true
    } catch (Exception e) {
        return false
    }
}

// ── TC-01: Valid login (Teacher OR Student) ──────────────────
KeywordUtil.logInfo('TC-01: Valid login')

fillAndSubmit(VALID_EMAIL, VALID_PASSWORD)

boolean isTeacher = verifyWindowExists(TEACHER_DASHBOARD_TITLE, 5)
boolean isStudent = verifyWindowExists(STUDENT_DASHBOARD_TITLE, 5)

assert isTeacher || isStudent : "No dashboard window opened"

KeywordUtil.logInfo('TC-01 PASSED')

// ── RETURN TO LOGIN (if needed) ──────────────────────────────

Windows.click(findByName('Logout'))
Windows.delay(2)
Windows.switchToWindowTitle(LOGIN_TITLE)

// ── TC-02: Empty email ───────────────────────────────────────
KeywordUtil.logInfo('TC-02: Empty email')

fillAndSubmit('', VALID_PASSWORD)

assert verifyWindowExists(ERROR_DIALOG_TITLE, 3)

Windows.switchToWindowTitle(ERROR_DIALOG_TITLE)
Windows.click(findByName('Ok')) 

KeywordUtil.logInfo('TC-02 PASSED')

// ── TC-03: Empty password ────────────────────────────────────
KeywordUtil.logInfo('TC-03: Empty password')

fillAndSubmit(VALID_EMAIL, '')

assert verifyWindowExists(ERROR_DIALOG_TITLE, 3)

Windows.switchToWindowTitle(ERROR_DIALOG_TITLE)
Windows.click(findByName('Ok')) 

KeywordUtil.logInfo('TC-03 PASSED')

// ── TC-04: Both empty ────────────────────────────────────────
KeywordUtil.logInfo('TC-04: Both empty')

fillAndSubmit('', '')

assert verifyWindowExists(ERROR_DIALOG_TITLE, 3)

Windows.switchToWindowTitle(ERROR_DIALOG_TITLE)
Windows.click(findByName('Ok')) 

KeywordUtil.logInfo('TC-04 PASSED')

// ── TC-05: Invalid email ─────────────────────────────────────
KeywordUtil.logInfo('TC-05: Invalid email')

fillAndSubmit(INVALID_EMAIL, VALID_PASSWORD)

assert verifyWindowExists(ERROR_DIALOG_TITLE, 3)

Windows.switchToWindowTitle(ERROR_DIALOG_TITLE)
Windows.click(findByName('Ok')) 

KeywordUtil.logInfo('TC-05 PASSED')

// ── TC-06: Wrong password ────────────────────────────────────
KeywordUtil.logInfo('TC-06: Wrong password')

fillAndSubmit(VALID_EMAIL, WRONG_PASSWORD)

assert verifyWindowExists(ERROR_DIALOG_TITLE, 5)

Windows.switchToWindowTitle(ERROR_DIALOG_TITLE)
Windows.click(findByName('Ok')) 

KeywordUtil.logInfo('TC-06 PASSED')