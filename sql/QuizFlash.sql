
CREATE TABLE Users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    departmentId INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    isTeacher BOOLEAN NOT NULL
);

CREATE TABLE Teachers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NOT NULL,
    teacherCode VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Students (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NOT NULL,
    studentCode VARCHAR(255) UNIQUE NOT NULL
);

    CREATE TABLE Quiz (
        id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255),
        totalQuestions INT NOT NULL,
        totalMarks INT NOT NULL,
        marksPerQuestion INT NOT NULL,
        teacherId INT NOT NULL,
        classroomId INT NOT NULL,
        createTime BIGINT,
        dueDate BIGINT
    );

CREATE TABLE QuestionAnswers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quizId INT NOT NULL,
    question VARCHAR(255) NOT NULL,
    optionA VARCHAR(255) NOT NULL,
    optionB VARCHAR(255) NOT NULL,
    optionC VARCHAR(255) NOT NULL,
    optionD VARCHAR(255) NOT NULL,
    correct CHAR(1) NOT NULL
);

CREATE TABLE StudentResponse (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quizId INT NOT NULL,
    questionId INT NOT NULL,
    studentId INT NOT NULL,
    isCorrect BOOLEAN NOT NULL,
    checkedAnswer BOOLEAN NOT NULL
);

CREATE TABLE Classroom (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    teacherId INT NOT NULL,
    studentCount INT NOT NULL,
    classCode VARCHAR(255) NOT NULL
);

CREATE TABLE ClassroomStudents (
    id INT AUTO_INCREMENT PRIMARY KEY,
    classroomId INT NOT NULL,
    studentId INT NOT NULL
);

CREATE TABLE Flashcards (
    id INT AUTO_INCREMENT PRIMARY KEY,
    studentId INT NOT NULL,
    title VARCHAR(255) NOT NULL,
    data VARCHAR(255) NOT NULL
);

CREATE TABLE Result (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quizId INT NOT NULL,
    studentId INT NOT NULL,
    marksObtained INT NOT NULL
);

CREATE TABLE LoggedDevices (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NOT NULL,
    MacAddress NVARCHAR(255) NOT NULL,
    lastLogin INT DEFAULT 0
);

CREATE TABLE Department (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE ClassroomStream (
    id INT PRIMARY KEY AUTO_INCREMENT,
    classroomId INT NOT NULL,
    teacherId INT NOT NULL,
    message VARCHAR(255),
    createTime BIGINT
);

-- ADDING DUMMY DATA TO THE TABLES

-- Dummy data for User table
INSERT INTO Users (email, password, departmentId, name, isTeacher) VALUES 
('teacher1@example.com', 'password1', 1, 'Teacher One', 1),
('teacher2@example.com', 'password2', 2, 'Teacher Two', 1),
('teacher3@example.com', 'password3', 3, 'Teacher Three', 1),
('student1@example.com', 'password1', 4, 'Student One', 0),
('student2@example.com', 'password2', 5, 'Student Two', 0),
('student3@example.com', 'password3', 6, 'Student Three', 0),
('student4@example.com', 'password4', 7, 'Student Four', 0),
('student5@example.com', 'password5', 8, 'Student Five', 0),
('student6@example.com', 'password6', 9, 'Student Six', 0),
('student7@example.com', 'password7', 10, 'Student Seven', 0);

-- Dummy data for Teachers table
INSERT INTO Teachers (userId, teacherCode) VALUES 
(1, 'TCODE001'),
(2, 'TCODE002'),
(3, 'TCODE003');

-- Dummy data for Students table with Engineering departments
INSERT INTO Students (userId, studentCode) VALUES 
(4, 'SCODE001'),
(5, 'SCODE002'),
(6, 'SCODE003'),
(7, 'SCODE004'),
(8, 'SCODE005'),
(9, 'SCODE006'),
(10, 'SCODE007');

-- Dummy data for Quiz table
INSERT INTO Quiz (name, totalQuestions, totalMarks, marksPerQuestion, teacherId, classroomId, createTime, dueDate) VALUES 
('Math Quiz 1', 10, 50, 5, 1, 1, 1718496000, 1726099200),
('Science Quiz 1', 15, 60, 4, 2, 2, 1718496000, 1726099200),
('History Quiz 1', 20, 100, 5, 3, 3, 1718496000, 1726099200);


-- Dummy data for QuestionAnswers table with engineering-related questions
INSERT INTO QuestionAnswers (quizId, question, optionA, optionB, optionC, optionD, correct) VALUES 
(1, 'What is the main programming language used in software engineering?', 'A) Java', 'B) Python', 'C) C++', 'D) Ruby', 'B'),
(1, 'What is the SI unit of electrical resistance?', 'A) Watt', 'B) Ohm', 'C) Volt', 'D) Ampere', 'B'),
(2, 'Which of the following is a primary material used in civil engineering for construction?', 'A) Aluminum', 'B) Steel', 'C) Copper', 'D) Plastic', 'B'),
(2, 'What is the principle behind the operation of a steam turbine?', 'A) Boyle''s Law', 'B) Newton''s Third Law', 'C) Carnot Cycle', 'D) Pascal''s Law', 'C');

-- Dummy data for StudentResponse table
INSERT INTO StudentResponse (quizId, questionId, studentId, isCorrect, checkedAnswer) VALUES 
(1, 1, 4, 1, 1),
(1, 2, 4, 0, 1),
(2, 3, 5, 1, 1),
(2, 4, 5, 0, 1);

-- Dummy data for Classroom table
INSERT INTO Classroom (name, teacherId, studentCount, classCode) VALUES 
('Class A', 1, 30, 'CLASSA001'),
('Class B', 2, 25, 'CLASSB002'),
('Class C', 3, 20, 'CLASSC003');

-- Dummy data for ClassroomStudents table
INSERT INTO ClassroomStudents (classroomId, studentId) VALUES 
(1, 4),
(1, 5),
(1, 6),
(2, 7),
(2, 8),
(2, 9),
(3, 10);

-- Dummy data for Flashcards table with engineering-related topics
INSERT INTO Flashcards (studentId, title, data) VALUES 
(1, 'Flashcards for Mechanical Engineering', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'),
(1, 'Flashcards for Electrical Engineering', 'Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'),
(1, 'Flashcards for Chemical Engineering', 'Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.');

-- Dummy data for Result table
INSERT INTO Result (quizId, studentId, marksObtained) VALUES 
(1, 4, 40),
(1, 5, 45),
(2, 6, 50),
(2, 7, 55);

-- Dummy data for Department table
INSERT INTO Department (name) VALUES 
('Software Engineering'),
('Electrical Engineering'),
('Mechanical Engineering'),
('Civil Engineering'),
('Chemical Engineering'),
('Biomedical Engineering'),
('Aerospace Engineering'),
('Environmental Engineering'),
('Industrial Engineering'),
('Materials Engineering');

-- Dummy data for ClassroomStream table
INSERT INTO ClassroomStream (classroomId, teacherId, message, createTime) VALUES
(1, 1, 'Welcome to Classroom 1! We are excited to start this journey together and look forward to a productive semester.', 1718496000),
(1, 1, 'Assignment 1 posted. Please review the instructions carefully and submit your work by the due date.', 1718582400),
(1, 1, 'Reminder: Quiz on Monday. Make sure to study the materials covered in the last three lectures.', 1718668800),
(1, 1, 'Class rescheduled to 10 AM this Wednesday due to a special event. Apologies for any inconvenience.', 1718755200),
(1, 1, 'Project submission deadline extended to next Friday. Use this extra time to refine your work.', 1718841600),
(2, 2, 'Welcome to Classroom 2! This semester we will cover various interesting topics. Stay engaged and active.', 1718496000),
(2, 2, 'Discussion on Chapter 3 scheduled for tomorrow. Read the chapter and come prepared with questions.', 1718582400),
(2, 2, 'Lab session details have been updated. Please check the new schedule and make necessary arrangements.', 1718668800),
(2, 2, 'Group project guidelines are now available. Form your teams and start brainstorming on your project ideas.', 1718755200),
(2, 2, 'Extra class on Friday to cover additional material before the midterms. Attendance is highly recommended.', 1718841600),
(3, 3, 'Welcome to Classroom 3! Let\'s make this a great learning experience by actively participating in discussions.', 1718496000),
(3, 3, 'Mid-term exam schedule has been posted. Please review the dates and prepare accordingly.', 1718582400),
(3, 3, 'Homework for the weekend: Complete the exercises at the end of Chapter 4. This will help reinforce the concepts.', 1718668800),
(3, 3, 'Field trip announcement: We will be visiting the science museum next week. Details will be shared soon.', 1718755200),
(3, 3, 'Online resources shared: Check the portal for additional reading materials and resources to aid your studies.', 1718841600),
(1, 1, 'New course material uploaded on the portal. Please review the new content before our next class.', 1718928000),
(2, 2, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1718928000),
(3, 3, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1718928000),
(1, 1, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1719014400),
(2, 2, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1719014400);

