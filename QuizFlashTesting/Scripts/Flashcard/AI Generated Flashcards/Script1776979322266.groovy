import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import com.kms.katalon.core.util.KeywordUtil
import com.kms.katalon.core.testobject.WindowsTestObject
import com.kms.katalon.core.enums.windows.LocatorStrategy

import java.awt.Robot
import java.awt.event.KeyEvent
import java.awt.datatransfer.StringSelection
import java.awt.Toolkit

def APP_PATH    = 'A:/Coding/C#/Projects/QuizFlash/bin/Release/QuizFlash.exe'
def LOGIN_TITLE = 'Login Window'
def DASH_TITLE  = 'Student Dashboard'

def EMAIL    = 'kaifnathani@gmail.com'
def PASSWORD = '1234'

def FILE_NAME = "TCP & UDP.pdf"

def el(String id) {
    def obj = new WindowsTestObject(id)
    obj.setLocator(id)
    obj.setLocatorStrategy(LocatorStrategy.ACCESSIBILITY_ID)
    return obj
}

def emailField    = el('emailBoxLogin')
def passwordField = el('passwordBoxLogin')
def loginButton   = el('LoginButton')

def flashcardBtn  = el('FlashcardsNavigatorButton')
def uploadBtn     = el('UploadPdfButton')
def logoutBtn         = el('LogoutButton')

Robot robot = new Robot()

KeywordUtil.logInfo("START TEST: AI FLASHCARD FILE UPLOAD")

Windows.startApplication(APP_PATH)
Windows.delay(5)

Windows.switchToWindowTitle(LOGIN_TITLE)

Windows.setText(emailField, EMAIL)
Windows.setText(passwordField, PASSWORD)
Windows.click(loginButton)

Windows.delay(5)

Windows.switchToWindowTitle(DASH_TITLE)

Windows.click(flashcardBtn)
Windows.delay(2)

Windows.click(uploadBtn)
Windows.delay(3)

StringSelection selection = new StringSelection(FILE_NAME)
Toolkit.getDefaultToolkit().getSystemClipboard().setContents(selection, null)

Windows.delay(1)

robot.keyPress(KeyEvent.VK_CONTROL)
robot.keyPress(KeyEvent.VK_V)
robot.keyRelease(KeyEvent.VK_V)
robot.keyRelease(KeyEvent.VK_CONTROL)

Windows.delay(1)

robot.keyPress(KeyEvent.VK_ENTER)
robot.keyRelease(KeyEvent.VK_ENTER)

Windows.delay(5)

KeywordUtil.logInfo("File selected successfully: " + FILE_NAME)

KeywordUtil.logInfo("Waiting for AI flashcard generation...")

Windows.delay(40)
Windows.click(logoutBtn)
Windows.closeApplication()

KeywordUtil.logInfo("TEST COMPLETED SUCCESSFULLY")