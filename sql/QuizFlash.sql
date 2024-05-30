CREATE TABLE User (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    isTeacher BOOLEAN NOT NULL
);

CREATE TABLE Teachers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NOT NULL,
    course VARCHAR(255) NOT NULL,
    teacherCode VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Students (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NOT NULL,
    studentCode VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Quiz (
    id INT AUTO_INCREMENT PRIMARY KEY,
    teacherId INT NOT NULL,
    classroomId INT NOT NULL
);

CREATE TABLE QuestionAnswers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quizId INT NOT NULL,
    question TEXT NOT NULL,
    optionA TEXT NOT NULL,
    optionB TEXT NOT NULL,
    optionC TEXT NOT NULL,
    optionD TEXT NOT NULL,
    correct CHAR(1) NOT NULL
);

CREATE TABLE Classroom (
    id INT AUTO_INCREMENT PRIMARY KEY,
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
    data TEXT NOT NULL
);

CREATE TABLE Result (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quizId INT NOT NULL,
    studentId INT NOT NULL,
    marks INT NOT NULL
);
