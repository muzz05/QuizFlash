
CREATE TABLE [User] (
    id INT Identity(1,1) PRIMARY KEY,
    email NVARCHAR(255) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    departmentId INT NOT NULL,
    name NVARCHAR(255) NOT NULL,
    isTeacher BIT NOT NULL
);

CREATE TABLE Teachers (
    id INT Identity(1,1) PRIMARY KEY,
    userId INT NOT NULL,
    teacherCode NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Students (
    id INT Identity(1,1) PRIMARY KEY,
    userId INT NOT NULL,
    studentCode NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Quiz (
    id INT Identity(1,1) PRIMARY KEY,
    totalQuestions INT NOT NULL,
    totalMarks INT NOT NULL,
    marksPerQuestion INT NOT NULL,
    teacherId INT NOT NULL,
    classroomId INT NOT NULL
);

CREATE TABLE QuestionAnswers (
    id INT Identity(1,1) PRIMARY KEY,
    quizId INT NOT NULL,
    question NVARCHAR(MAX) NOT NULL,
    optionA NVARCHAR(MAX) NOT NULL,
    optionB NVARCHAR(MAX) NOT NULL,
    optionC NVARCHAR(MAX) NOT NULL,
    optionD NVARCHAR(MAX) NOT NULL,
    correct CHAR(1) NOT NULL
);

CREATE TABLE StudentResponse (
    id INT Identity(1,1) PRIMARY KEY,
    quizId INT NOT NULL,
    questionId INT NOT NULL,
    studentId INT NOT NULL,
    isCorrect BIT NOT NULL,
    checkedAnswer BIT NOT NULL
);

CREATE TABLE Classroom (
    id INT Identity(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    teacherId INT NOT NULL,
    studentCount INT NOT NULL,
    classCode NVARCHAR(255) NOT NULL
);

CREATE TABLE ClassroomStudents (
    id INT Identity(1,1) PRIMARY KEY,
    classroomId INT NOT NULL,
    studentId INT NOT NULL
);

CREATE TABLE Flashcards (
    id INT Identity(1,1) PRIMARY KEY,
    studentId INT NOT NULL,
    title NVARCHAR(255) NOT NULL,
    data NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Result (
    id INT Identity(1,1) PRIMARY KEY,
    quizId INT NOT NULL,
    studentId INT NOT NULL,
    marksObtained INT NOT NULL
);

CREATE TABLE LoggedDevices (
    id INT Identity(1,1) PRIMARY KEY,
    userId INT NOT NULL,
    MacAddress NVARCHAR(255) NOT NULL,
    lastLogin INT DEFAULT 0
);

CREATE TABLE Department (
    id INT Identity(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL
);

-- ADDING DUMMY DATA TO THE TABLES

-- Dummy data for User table
INSERT INTO [User] (email, password, departmentId, name, isTeacher) VALUES 
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
INSERT INTO Quiz (totalQuestions, totalMarks, marksPerQuestion, teacherId, classroomId) VALUES 
(10, 50, 5, 1, 1),
(15, 60, 4, 2, 2),
(20, 100, 5, 3, 3);

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
(4, 'Flashcards for Mechanical Engineering', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'),
(5, 'Flashcards for Electrical Engineering', 'Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'),
(6, 'Flashcards for Chemical Engineering', 'Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.');

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

