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
    correct TINYINT(4) NOT NULL
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
    courseCode VARCHAR(255),
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
    deviceName VARCHAR(255) NOT NULL,
    deviceType TINYINT(1) NOT NULL,
    MacAddress VARCHAR (255) NOT NULL,
    lastLogin INT DEFAULT 0
);

CREATE TABLE Department (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE ClassroomStream (
    id INT PRIMARY KEY AUTO_INCREMENT,
    classroomId INT NOT NULL,
    userId INT NOT NULL,
    message VARCHAR(255),
    createTime BIGINT,
    isTeacher TINYINT(1)
);

-- ADDING DUMMY DATA TO THE TABLES

-- Dummy data for User table
INSERT INTO
    Users (
        email,
        password,
        departmentId,
        name,
        isTeacher
    )
VALUES
    ('dr.smith@example.com', 'password1', 1, 'Dr. John Smith', 1),
    ('prof.jones@example.com', 'password2', 2, 'Prof. Emily Jones', 1),
    ('dr.brown@example.com', 'password3', 3, 'Dr. Michael Brown', 1),
    ('prof.miller@example.com', 'password4', 4, 'Prof. Sarah Miller', 1),
    ('dr.wilson@example.com', 'password5', 5, 'Dr. David Wilson', 1),
    ('prof.moore@example.com', 'password6', 6, 'Prof. Jessica Moore', 1),
    ('dr.taylor@example.com', 'password7', 7, 'Dr. Chris Taylor', 1),
    ('prof.anderson@example.com', 'password8', 8, 'Prof. Amanda Anderson', 1),
    ('dr.thomas@example.com', 'password9', 9, 'Dr. Joshua Thomas', 1),
    ('prof.jackson@example.com', 'password10', 10, 'Prof. Laura Jackson', 1),
    ('dr.white@example.com', 'password11', 1, 'Dr. Kevin White', 1),
    ('prof.harris@example.com', 'password12', 2, 'Prof. Megan Harris', 1),
    ('dr.martin@example.com', 'password13', 3, 'Dr. Robert Martin', 1),
    ('prof.lee@example.com', 'password14', 4, 'Prof. Patricia Lee', 1),
    ('dr.thompson@example.com', 'password15', 5, 'Dr. Daniel Thompson', 1),
    ('prof.young@example.com', 'password16', 6, 'Prof. Rebecca Young', 1),
    ('dr.clark@example.com', 'password17', 7, 'Dr. Anthony Clark', 1),
    ('prof.allen@example.com', 'password18', 8, 'Prof. Kimberly Allen', 1),
    ('dr.scott@example.com', 'password19', 9, 'Dr. Jason Scott', 1),
    ('prof.green@example.com', 'password20', 10, 'Prof. Jennifer Green', 1),
    
    ('alice.johnson@example.com', 'password21', 1, 'Alice Johnson', 0),
    ('bob.williams@example.com', 'password22', 2, 'Bob Williams', 0),
    ('charlie.jones@example.com', 'password23', 3, 'Charlie Jones', 0),
    ('diana.brown@example.com', 'password24', 4, 'Diana Brown', 0),
    ('edward.miller@example.com', 'password25', 5, 'Edward Miller', 0),
    ('fiona.davis@example.com', 'password26', 6, 'Fiona Davis', 0),
    ('george.wilson@example.com', 'password27', 7, 'George Wilson', 0),
    ('hannah.moore@example.com', 'password28', 8, 'Hannah Moore', 0),
    ('ian.taylor@example.com', 'password29', 9, 'Ian Taylor', 0),
    ('julia.anderson@example.com', 'password30', 10, 'Julia Anderson', 0),
    ('kevin.thomas@example.com', 'password31', 1, 'Kevin Thomas', 0),
    ('laura.jackson@example.com', 'password32', 2, 'Laura Jackson', 0),
    ('michael.white@example.com', 'password33', 3, 'Michael White', 0),
    ('natalie.harris@example.com', 'password34', 4, 'Natalie Harris', 0),
    ('oliver.martin@example.com', 'password35', 5, 'Oliver Martin', 0),
    ('paula.lee@example.com', 'password36', 6, 'Paula Lee', 0),
    ('quentin.thompson@example.com', 'password37', 7, 'Quentin Thompson', 0),
    ('rachel.young@example.com', 'password38', 8, 'Rachel Young', 0),
    ('steven.clark@example.com', 'password39', 9, 'Steven Clark', 0),
    ('tina.allen@example.com', 'password40', 10, 'Tina Allen', 0),
    ('uma.scott@example.com', 'password41', 1, 'Uma Scott', 0),
    ('victor.green@example.com', 'password42', 2, 'Victor Green', 0);



-- Dummy data for Teachers table-- Dummy data for Teachers table
INSERT INTO
    Teachers (userId, teacherCode)
VALUES
    (1, 'TCODE001'),
    (2, 'TCODE002'),
    (3, 'TCODE003'),
    (4, 'TCODE004'),
    (5, 'TCODE005'),
    (6, 'TCODE006'),
    (7, 'TCODE007'),
    (8, 'TCODE008'),
    (9, 'TCODE009'),
    (10, 'TCODE010'),
    (11, 'TCODE011'),
    (12, 'TCODE012'),
    (13, 'TCODE013'),
    (14, 'TCODE014'),
    (15, 'TCODE015'),
    (16, 'TCODE016'),
    (17, 'TCODE017'),
    (18, 'TCODE018'),
    (19, 'TCODE019'),
    (20, 'TCODE020');

-- Dummy data for Students table
INSERT INTO
    Students (userId, studentCode)
VALUES
    (21, 'SCODE001'),
    (22, 'SCODE002'),
    (23, 'SCODE003'),
    (24, 'SCODE004'),
    (25, 'SCODE005'),
    (26, 'SCODE006'),
    (27, 'SCODE007'),
    (28, 'SCODE008'),
    (29, 'SCODE009'),
    (30, 'SCODE010'),
    (31, 'SCODE011'),
    (32, 'SCODE012'),
    (33, 'SCODE013'),
    (34, 'SCODE014'),
    (35, 'SCODE015'),
    (36, 'SCODE016'),
    (37, 'SCODE017'),
    (38, 'SCODE018'),
    (39, 'SCODE019'),
    (40, 'SCODE020'),
    (41, 'SCODE021'),
    (42, 'SCODE022');


-- Dummy data for Quiz table
INSERT INTO
    Quiz (
        name,
        totalQuestions,
        totalMarks,
        marksPerQuestion,
        teacherId,
        classroomId,
        createTime,
        dueDate
    )
VALUES (
        'Math Quiz 1',
        10,
        50,
        5,
        1,
        1,
        1718496000,
        1726099200
    ),
    (
        'Science Quiz 1',
        15,
        60,
        4,
        2,
        2,
        1718496000,
        1726099200
    ),
    (
        'History Quiz 1',
        20,
        100,
        5,
        3,
        3,
        1718496000,
        1726099200
    );

-- Dummy data for QuestionAnswers table with engineering-related questions
INSERT INTO
    QuestionAnswers (
        quizId,
        question,
        optionA,
        optionB,
        optionC,
        optionD,
        correct
    )
VALUES (
        1,
        'What is the main programming language used in software engineering?',
        'A) Java',
        'B) Python',
        'C) C++',
        'D) Ruby',
        1
    ),
    (
        1,
        'What is the SI unit of electrical resistance?',
        'A) Watt',
        'B) Ohm',
        'C) Volt',
        'D) Ampere',
        1
    ),
    (
        2,
        'Which of the following is a primary material used in civil engineering for construction?',
        'A) Aluminum',
        'B) Steel',
        'C) Copper',
        'D) Plastic',
        1
    ),
    (
        2,
        'What is the principle behind the operation of a steam turbine?',
        'A) Boyle''s Law',
        'B) Newton''s Third Law',
        'C) Carnot Cycle',
        'D) Pascal''s Law',
        2
    );

-- Dummy data for StudentResponse table
INSERT INTO
    StudentResponse (
        quizId,
        questionId,
        studentId,
        isCorrect,
        checkedAnswer
    )
VALUES (1, 1, 4, 1, 1),
    (1, 2, 4, 0, 1),
    (2, 3, 5, 1, 1),
    (2, 4, 5, 0, 1);

-- Dummy data for Classroom table
INSERT INTO
    Classroom (
        name,
        teacherId,
        studentCount,
        courseCode,
        classCode
    )
VALUES 
    ('Software Engineering', 1, 30, 'SE-119', 'CLASSA001'),
    ('Applied Physics', 1, 28, 'AP-120', 'CLASSA002'),
    ('Discrete Structures', 1, 32, 'DS-121', 'CLASSA003'),
    ('Database Systems', 1, 26, 'DB-122', 'CLASSA004'),
    ('Operating Systems', 1, 29, 'OS-123', 'CLASSA005'),
    ('Data Structures', 1, 31, 'DS-124', 'CLASSA006'),
    ('Computer Networks', 1, 27, 'CN-125', 'CLASSA007'),
    ('Machine Learning', 1, 33, 'ML-126', 'CLASSA008'),
    ('Artificial Intelligence', 1, 30, 'AI-127', 'CLASSA009'),
    ('Web Development', 1, 28, 'WD-128', 'CLASSA010'),
    
    ('Linear Algebra', 2, 25, 'LA-234', 'CLASSB001'),
    ('Calculus I', 2, 23, 'C1-235', 'CLASSB002'),
    ('Calculus II', 2, 27, 'C2-236', 'CLASSB003'),
    ('Differential Equations', 2, 24, 'DE-237', 'CLASSB004'),
    ('Probability and Statistics', 2, 26, 'PS-238', 'CLASSB005'),
    ('Numerical Methods', 2, 28, 'NM-239', 'CLASSB006'),
    ('Real Analysis', 2, 25, 'RA-240', 'CLASSB007'),
    ('Complex Analysis', 2, 22, 'CA-241', 'CLASSB008'),
    ('Abstract Algebra', 2, 29, 'AA-242', 'CLASSB009'),
    ('Topology', 2, 21, 'TP-243', 'CLASSB010'),
    
    ('Modern Physics', 3, 20, 'MP-345', 'CLASSC001'),
    ('Classical Mechanics', 3, 18, 'CM-346', 'CLASSC002'),
    ('Quantum Mechanics', 3, 22, 'QM-347', 'CLASSC003'),
    ('Thermodynamics', 3, 19, 'TD-348', 'CLASSC004'),
    ('Electromagnetism', 3, 21, 'EM-349', 'CLASSC005'),
    ('Optics', 3, 23, 'OP-350', 'CLASSC006'),
    ('Nuclear Physics', 3, 20, 'NP-351', 'CLASSC007'),
    ('Astrophysics', 3, 17, 'AP-352', 'CLASSC008'),
    ('Particle Physics', 3, 24, 'PP-353', 'CLASSC009'),
    ('Condensed Matter Physics', 3, 19, 'CMP-354', 'CLASSC010');

-- Dummy data for ClassroomStudents table
-- Insert students into classrooms
INSERT INTO
    ClassroomStudents (classroomId, studentId)
VALUES 
    -- Classroom 1: Software Engineering
    (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8), (1, 9), (1, 10), (1, 11), (1, 12), (1, 13), (1, 14), (1, 15), (1, 16), (1, 17), (1, 18), (1, 19), (1, 20), (1, 21), (1, 22),

    -- Classroom 2: Applied Physics
    (2, 1), (2, 2), (2, 3), (2, 4), (2, 5), (2, 6), (2, 7), (2, 8), (2, 9), (2, 10), (2, 11), (2, 12), (2, 13), (2, 14), (2, 15), (2, 16), (2, 17), (2, 18), (2, 19), (2, 20), (2, 21), (2, 22),

    -- Classroom 3: Discrete Structures
    (3, 1), (3, 2), (3, 3), (3, 4), (3, 5), (3, 6), (3, 7), (3, 8), (3, 9), (3, 10), (3, 11), (3, 12), (3, 13), (3, 14), (3, 15), (3, 16), (3, 17), (3, 18), (3, 19), (3, 20), (3, 21), (3, 22),

    -- Classroom 4: Database Systems
    (4, 1), (4, 2), (4, 3), (4, 4), (4, 5), (4, 6), (4, 7), (4, 8), (4, 9), (4, 10), (4, 11), (4, 12), (4, 13), (4, 14), (4, 15), (4, 16), (4, 17), (4, 18), (4, 19), (4, 20), (4, 21), (4, 22),

    -- Classroom 5: Operating Systems
    (5, 1), (5, 2), (5, 3), (5, 4), (5, 5), (5, 6), (5, 7), (5, 8), (5, 9), (5, 10), (5, 11), (5, 12), (5, 13), (5, 14), (5, 15), (5, 16), (5, 17), (5, 18), (5, 19), (5, 20), (5, 21), (5, 22),

    -- Classroom 6: Data Structures
    (6, 1), (6, 2), (6, 3), (6, 4), (6, 5), (6, 6), (6, 7), (6, 8), (6, 9), (6, 10), (6, 11), (6, 12), (6, 13), (6, 14), (6, 15), (6, 16), (6, 17), (6, 18), (6, 19), (6, 20), (6, 21), (6, 22),

    -- Classroom 7: Computer Networks
    (7, 1), (7, 2), (7, 3), (7, 4), (7, 5), (7, 6), (7, 7), (7, 8), (7, 9), (7, 10), (7, 11), (7, 12), (7, 13), (7, 14), (7, 15), (7, 16), (7, 17), (7, 18), (7, 19), (7, 20), (7, 21), (7, 22),

    -- Classroom 8: Machine Learning
    (8, 1), (8, 2), (8, 3), (8, 4), (8, 5), (8, 6), (8, 7), (8, 8), (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (8, 15), (8, 16), (8, 17), (8, 18), (8, 19), (8, 20), (8, 21), (8, 22),

    -- Classroom 9: Artificial Intelligence
    (9, 1), (9, 2), (9, 3), (9, 4), (9, 5), (9, 6), (9, 7), (9, 8), (9, 9), (9, 10), (9, 11), (9, 12), (9, 13), (9, 14), (9, 15), (9, 16), (9, 17), (9, 18), (9, 19), (9, 20), (9, 21), (9, 22),

    -- Classroom 10: Web Development
    (10, 1), (10, 2), (10, 3), (10, 4), (10, 5), (10, 6), (10, 7), (10, 8), (10, 9), (10, 10), (10, 11), (10, 12), (10, 13), (10, 14), (10, 15), (10, 16), (10, 17), (10, 18), (10, 19), (10, 20), (10, 21), (10, 22),

    -- Classroom 11: Linear Algebra
    (11, 1), (11, 2), (11, 3), (11, 4), (11, 5), (11, 6), (11, 7), (11, 8), (11, 9), (11, 10), (11, 11), (11, 12), (11, 13), (11, 14), (11, 15), (11, 16), (11, 17), (11, 18), (11, 19), (11, 20), (11, 21), (11, 22);


-- Dummy data for Flashcards table with engineering-related topics
INSERT INTO Flashcards (studentId, title, data)
VALUES
    (1, 'Mechanical Engineering Basics', 'Key concepts include thermodynamics, fluid mechanics, and material science.'),
    (1, 'Thermodynamics Laws', 'First Law: Energy cannot be created or destroyed. Second Law: Entropy always increases.'),
    (1, 'Fluid Mechanics', 'Study of fluid properties and behavior under various conditions.'),
    (1, 'Material Science', 'Properties of materials, including metals, polymers, and composites.'),
    (1, 'Mechanics of Materials', 'Analysis of stress, strain, and deformation in solid materials.'),
    
    (2, 'Electrical Circuit Analysis', 'Ohm''s Law: V=IR, Kirchhoff''s laws for current and voltage.'),
    (2, 'Power Systems', 'Understanding of AC/DC systems, transformers, and power distribution.'),
    (2, 'Digital Electronics', 'Logic gates, flip-flops, and digital circuits design.'),
    (2, 'Analog Electronics', 'Operational amplifiers, filters, and analog circuit design.'),
    (2, 'Electromagnetic Fields', 'Maxwell''s equations, wave propagation, and antenna theory.'),
    
    (3, 'Chemical Reaction Engineering', 'Study of reactor design, rate laws, and reaction mechanisms.'),
    (3, 'Process Dynamics', 'Control systems, Laplace transforms, and stability analysis.'),
    (3, 'Chemical Thermodynamics', 'Gibbs free energy, phase equilibria, and chemical potential.'),
    (3, 'Biochemical Engineering', 'Application of chemical engineering principles to biological systems.'),
    (3, 'Polymer Engineering', 'Synthesis, properties, and applications of polymer materials.'),
    
    (4, 'Civil Engineering Materials', 'Properties of concrete, steel, and composite materials used in construction.'),
    (4, 'Structural Analysis', 'Methods for analyzing stress, strain, and load distribution in structures.'),
    (4, 'Geotechnical Engineering', 'Soil mechanics, foundation design, and earth retaining structures.'),
    (4, 'Transportation Engineering', 'Traffic flow theory, transportation planning, and highway design.'),
    (4, 'Environmental Engineering', 'Pollution control, waste management, and sustainable design practices.'),
    
    (5, 'Software Engineering Principles', 'Software development life cycle, agile methodologies, and design patterns.'),
    (5, 'Database Management Systems', 'SQL, normalization, and transaction management.'),
    (5, 'Computer Networks', 'OSI model, TCP/IP protocol, and network security principles.'),
    (5, 'Operating Systems', 'Processes, memory management, and file systems.'),
    (5, 'Data Structures and Algorithms', 'Stacks, queues, trees, and algorithm complexity.'),
    
    (6, 'Environmental Engineering', 'Pollution control, waste management, and sustainable design practices.'),
    (6, 'Water Resources Engineering', 'Hydrology, fluid dynamics, and water treatment processes.'),
    (6, 'Air Quality Management', 'Techniques and regulations for controlling air pollution.'),
    (6, 'Sustainable Design', 'Principles of designing buildings and systems with minimal environmental impact.'),
    (6, 'Renewable Energy Systems', 'Solar, wind, and biomass energy technologies and their applications.'),
    
    (7, 'Mechanical Engineering Design', 'CAD tools, design optimization, and product lifecycle management.'),
    (7, 'Heat Transfer', 'Conduction, convection, and radiation principles in engineering applications.'),
    (7, 'Manufacturing Processes', 'Machining, casting, and additive manufacturing techniques.'),
    (7, 'Dynamics of Machinery', 'Analysis of motion, forces, and energy in mechanical systems.'),
    (7, 'Vibration Analysis', 'Study of mechanical vibrations and methods for controlling them.'),
    
    (8, 'Electrical Machines', 'Function and design of transformers, motors, and generators.'),
    (8, 'Digital Signal Processing', 'Sampling, Fourier transforms, and signal filtering techniques.'),
    (8, 'Control Systems', 'PID controllers, feedback loops, and stability analysis in engineering systems.'),
    (8, 'Microelectronics', 'Design and fabrication of integrated circuits and semiconductor devices.'),
    (8, 'Power Electronics', 'Converters, inverters, and power management circuits.'),
    
    (9, 'Chemical Thermodynamics', 'Gibbs free energy, phase equilibria, and chemical potential.'),
    (9, 'Biochemical Engineering', 'Application of chemical engineering principles to biological systems.'),
    (9, 'Process Control', 'Instrumentation, control strategies, and automation in chemical processes.'),
    (9, 'Separation Processes', 'Distillation, filtration, and membrane processes in chemical engineering.'),
    (9, 'Catalysis', 'Mechanisms and applications of catalysts in chemical reactions.'),
    
    (10, 'Transportation Engineering', 'Traffic flow theory, transportation planning, and highway design.'),
    (10, 'Geotechnical Engineering', 'Soil mechanics, foundation design, and earth retaining structures.'),
    (10, 'Hydraulics', 'Flow of water in pipes, channels, and hydraulic structures.'),
    (10, 'Construction Management', 'Project planning, scheduling, and cost estimation.'),
    (10, 'Urban Planning', 'Design and development of urban spaces and infrastructure.'),
    
    (11, 'Computer Networks', 'OSI model, TCP/IP protocol, and network security principles.'),
    (11, 'Operating Systems', 'Processes, memory management, and file systems.'),
    (11, 'Software Testing', 'Techniques and tools for testing software and ensuring quality.'),
    (11, 'Artificial Intelligence', 'Machine learning, neural networks, and AI applications.'),
    (11, 'Cybersecurity', 'Protecting systems and data from cyber threats and attacks.'),
    
    (12, 'Renewable Energy Engineering', 'Solar, wind, and biomass energy technologies and their applications.'),
    (12, 'Energy Systems Analysis', 'Energy conversion, storage, and efficiency evaluation methods.'),
    (12, 'Sustainable Energy Policy', 'Regulations and policies for promoting renewable energy use.'),
    (12, 'Energy Auditing', 'Techniques for assessing energy use and identifying efficiency improvements.'),
    (12, 'Smart Grids', 'Advanced grid technologies for efficient energy distribution.'),
    
    (13, 'Control Systems Engineering', 'PID controllers, feedback loops, and stability analysis in engineering systems.'),
    (13, 'Robotics', 'Kinematics, dynamics, and control of robotic systems.'),
    (13, 'Automation', 'Design and implementation of automated systems in manufacturing.'),
    (13, 'Mechatronics', 'Integration of mechanical, electronic, and computer systems.'),
    (13, 'Sensors and Actuators', 'Types and applications of sensors and actuators in engineering systems.'),
    
    (14, 'Biomedical Engineering', 'Medical imaging, biomaterials, and tissue engineering.'),
    (14, 'Nanotechnology', 'Nanomaterials, fabrication techniques, and applications in various fields.'),
    (14, 'Biomechanics', 'Study of the mechanics of biological systems and human movement.'),
    (14, 'Medical Device Design', 'Principles and standards for designing medical devices.'),
    (14, 'Bioinformatics', 'Application of computer science and statistics to biological data.'),
    
    (15, 'Aerospace Engineering', 'Aerodynamics, propulsion systems, and spacecraft design.'),
    (15, 'Flight Mechanics', 'Aircraft performance, stability, and control.'),
    (15, 'Aerospace Materials', 'Properties and applications of materials used in aerospace engineering.'),
    (15, 'Orbital Mechanics', 'Study of the motion of spacecraft and other objects in space.'),
    (15, 'Aircraft Structures', 'Design and analysis of aircraft structural components.'),
    
    (16, 'Materials Science', 'Crystal structures, material properties, and failure mechanisms.'),
    (16, 'Metallurgy', 'Extraction, refining, and properties of metals and alloys.'),
    (16, 'Composite Materials', 'Design and application of composite materials in engineering.'),
    (16, 'Ceramics', 'Properties and applications of ceramic materials.'),
    (16, 'Material Characterization', 'Techniques for analyzing material properties and behavior.'),
    
    (17, 'Telecommunications Engineering', 'Wireless communication, signal modulation, and network infrastructure.'),
    (17, 'Embedded Systems', 'Microcontrollers, interfacing, and real-time operating systems.'),
    (17, 'RF Engineering', 'Design and analysis of radio frequency circuits and systems.'),
    (17, 'Optical Communications', 'Fiber optics, lasers, and optical network design.'),
    (17, 'Telecommunication Networks', 'Design and management of telecommunication networks.'),
    
    (18, 'Marine Engineering', 'Ship design, marine propulsion, and offshore structures.'),
    (18, 'Hydrodynamics', 'Study of fluid flow behavior in marine environments.'),
    (18, 'Marine Propulsion Systems', 'Design and operation of propulsion systems for ships and submarines.'),
    (18, 'Naval Architecture', 'Principles of designing and constructing ships and other marine vessels.'),
    (18, 'Offshore Engineering', 'Design and analysis of structures for offshore oil and gas extraction.'),
    
    (19, 'Nuclear Reactor Physics', 'Study of the physical principles governing nuclear reactors.'),
    (19, 'Nuclear Materials', 'Properties and behavior of materials used in nuclear reactors.'),
    (19, 'Nuclear Power Plant Design', 'Design principles and safety considerations for nuclear power plants.'),
    (19, 'Systems Engineering', 'System design, integration, and project management.'),
    (19, 'Risk Management', 'Identifying, assessing, and mitigating risks in engineering projects.'),
    
    (20, 'Project Management', 'Planning, executing, and closing engineering projects.'),
    (20, 'Human Factors Engineering', 'Designing systems with consideration of human capabilities and limitations.'),
    (20, 'Quality Assurance', 'Ensuring product quality through systematic processes and standards.'),
    (20, 'Automotive Engineering', 'Vehicle dynamics, engine design, and automotive electronics.'),
    (20, 'Hybrid and Electric Vehicles', 'Design and operation of electric and hybrid vehicle systems.'),
    
    (21, 'Vehicle Safety Systems', 'Design and analysis of safety features in vehicles.'),
    (21, 'Automotive Manufacturing', 'Techniques and processes used in the manufacture of automobiles.'),
    (21, 'Automotive Control Systems', 'Design and implementation of control systems in vehicles.'),
    (21, 'Industrial Engineering', 'Optimization of complex processes, systems, and organizations.'),
    (21, 'Supply Chain Management', 'Logistics, inventory control, and production planning.'),
    
    (22, 'Operations Research', 'Mathematical modeling and analysis for decision making.'),
    (22, 'Human Factors and Ergonomics', 'Designing systems and processes for optimal human use.'),
    (22, 'Lean Manufacturing', 'Principles and practices for minimizing waste in manufacturing.');    


-- Dummy data for Result table
INSERT INTO
    Result (
        quizId,
        studentId,
        marksObtained
    )
VALUES (1, 4, 40),
    (1, 5, 45),
    (2, 6, 50),
    (2, 7, 55);

-- Dummy data for Department table
INSERT INTO
    Department (name)
VALUES ('Software Engineering'),
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
-- Insert data into the ClassroomStream table with the updated schema
INSERT INTO
    ClassroomStream (
        classroomId,
        userId,
        message,
        createTime,
        isTeacher
    )
VALUES 
    -- Classroom 1
    (1, 1, 'Welcome to Classroom 1! We are excited to start this journey together and look forward to a productive semester.', 1718496000, 1),
    (1, 2, 'Assignment 1 posted. Please review the instructions carefully and submit your work by the due date.', 1718582400, 1),
    (1, 3, 'Reminder: Quiz on Monday. Make sure to study the materials covered in the last three lectures.', 1718668800, 1),
    (1, 4, 'Class rescheduled to 10 AM this Wednesday due to a special event. Apologies for any inconvenience.', 1718755200, 1),
    (1, 5, 'Project submission deadline extended to next Friday. Use this extra time to refine your work.', 1718841600, 1),
    (1, 21, 'Great job on the recent assignment, everyone!', 1719100800, 0),
    (1, 22, 'Can someone explain the last lecture topic?', 1719187200, 0),
    (1, 23, 'Looking forward to the lab session tomorrow.', 1719273600, 0),
    (1, 24, 'Has anyone finished the project? Need some help.', 1719360000, 0),
    (1, 25, 'Mid-term exams are tough, but we can do it!', 1719446400, 0),
    (1, 26, 'What time is the field trip starting?', 1719532800, 0),
    (1, 27, 'Study group meeting after class today.', 1719619200, 0),
    (1, 28, 'Can we have a review session before the quiz?', 1719705600, 0),
    (1, 29, 'I found some useful resources online. Check the portal.', 1719792000, 0),
    (1, 30, 'Who is ready for the next project phase?', 1719878400, 0),
    (1, 31, 'Don’t forget to bring your lab coats tomorrow.', 1719964800, 0),
    (1, 32, 'I missed the last class. Can someone share notes?', 1720051200, 0),
    (1, 33, 'The new assignment seems challenging. Any tips?', 1720137600, 0),
    (1, 34, 'Reminder: Submit your projects by Friday.', 1720224000, 0),
    (1, 35, 'Extra credit opportunity posted on the portal.', 1720310400, 0),
    (1, 36, 'I am looking for a study partner for the upcoming quiz.', 1720396800, 0),
    (1, 37, 'Group 3, please meet me after class for project discussion.', 1720483200, 0),
    (1, 38, 'What chapters are covered in the next quiz?', 1720569600, 0),
    (1, 6, 'New course material uploaded on the portal. Please review the new content before our next class.', 1720656000, 1),
    (1, 7, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1720742400, 1),
    (1, 8, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1720828800, 1),
    (1, 9, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1720915200, 1),
    (1, 10, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1721001600, 1),

    -- Classroom 2
    (2, 1, 'Welcome to Classroom 2! This semester we will cover various interesting topics. Stay engaged and active.', 1718496000, 1),
    (2, 2, 'Discussion on Chapter 3 scheduled for tomorrow. Read the chapter and come prepared with questions.', 1718582400, 1),
    (2, 3, 'Lab session details have been updated. Please check the new schedule and make necessary arrangements.', 1718668800, 1),
    (2, 4, 'Group project guidelines are now available. Form your teams and start brainstorming on your project ideas.', 1718755200, 1),
    (2, 5, 'Extra class on Friday to cover additional material before the midterms. Attendance is highly recommended.', 1718841600, 1),
    (2, 21, 'I found some useful resources online. Check the portal.', 1719100800, 0),
    (2, 22, 'Great job on the recent assignment, everyone!', 1719187200, 0),
    (2, 23, 'Can someone explain the last lecture topic?', 1719273600, 0),
    (2, 24, 'Looking forward to the lab session tomorrow.', 1719360000, 0),
    (2, 25, 'Has anyone finished the project? Need some help.', 1719446400, 0),
    (2, 26, 'Mid-term exams are tough, but we can do it!', 1719532800, 0),
    (2, 27, 'What time is the field trip starting?', 1719619200, 0),
    (2, 28, 'Study group meeting after class today.', 1719705600, 0),
    (2, 29, 'Can we have a review session before the quiz?', 1719792000, 0),
    (2, 30, 'Who is ready for the next project phase?', 1719878400, 0),
    (2, 31, 'Don’t forget to bring your lab coats tomorrow.', 1719964800, 0),
    (2, 32, 'I missed the last class. Can someone share notes?', 1720051200, 0),
    (2, 33, 'The new assignment seems challenging. Any tips?', 1720137600, 0),
    (2, 34, 'Reminder: Submit your projects by Friday.', 1720224000, 0),
    (2, 35, 'Extra credit opportunity posted on the portal.', 1720310400, 0),
    (2, 36, 'I am looking for a study partner for the upcoming quiz.', 1720396800, 0),
    (2, 37, 'Group 3, please meet me after class for project discussion.', 1720483200, 0),
    (2, 38, 'What chapters are covered in the next quiz?', 1720569600, 0),
    (2, 6, 'New course material uploaded on the portal. Please review the new content before our next class.', 1720656000, 1),
    (2, 7, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1720742400, 1),
    (2, 8, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1720828800, 1),
    (2, 9, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1720915200, 1),
    (2, 10, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1721001600, 1),

    -- Classroom 3
    (3, 1, 'Welcome to Classroom 3! Let\'s make this a great learning experience by actively participating in discussions.', 1718496000, 1),
    (3, 2, 'Mid-term exam schedule has been posted. Please review the dates and prepare accordingly.', 1718582400, 1),
    (3, 3, 'Homework for the weekend: Complete the exercises at the end of Chapter 4. This will help reinforce the concepts.', 1718668800, 1),
    (3, 4, 'Field trip announcement: We will be visiting the science museum next week. Details will be shared soon.', 1718755200, 1),
    (3, 5, 'Online resources shared: Check the portal for additional reading materials and resources to aid your studies.', 1718841600, 1),
    (3, 21, 'I found some useful resources online. Check the portal.', 1719100800, 0),
    (3, 22, 'Great job on the recent assignment, everyone!', 1719187200, 0),
    (3, 23, 'Can someone explain the last lecture topic?', 1719273600, 0),
    (3, 24, 'Looking forward to the lab session tomorrow.', 1719360000, 0),
    (3, 25, 'Has anyone finished the project? Need some help.', 1719446400, 0),
    (3, 26, 'Mid-term exams are tough, but we can do it!', 1719532800, 0),
    (3, 27, 'What time is the field trip starting?', 1719619200, 0),
    (3, 28, 'Study group meeting after class today.', 1719705600, 0),
    (3, 29, 'Can we have a review session before the quiz?', 1719792000, 0),
    (3, 30, 'Who is ready for the next project phase?', 1719878400, 0),
    (3, 31, 'Don’t forget to bring your lab coats tomorrow.', 1719964800, 0),
    (3, 32, 'I missed the last class. Can someone share notes?', 1720051200, 0),
    (3, 33, 'The new assignment seems challenging. Any tips?', 1720137600, 0),
    (3, 34, 'Reminder: Submit your projects by Friday.', 1720224000, 0),
    (3, 35, 'Extra credit opportunity posted on the portal.', 1720310400, 0),
    (3, 36, 'I am looking for a study partner for the upcoming quiz.', 1720396800, 0),
    (3, 37, 'Group 3, please meet me after class for project discussion.', 1720483200, 0),
    (3, 38, 'What chapters are covered in the next quiz?', 1720569600, 0),
    (3, 6, 'New course material uploaded on the portal. Please review the new content before our next class.', 1720656000, 1),
    (3, 7, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1720742400, 1),
    (3, 8, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1720828800, 1),
    (3, 9, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1720915200, 1),
    (3, 10, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1721001600, 1),

    -- Classroom 4
    (4, 1, 'Welcome to Classroom 4! Looking forward to a productive semester.', 1718496000, 1),
    (4, 2, 'Assignment 2 is available now. Review the instructions and submit by the deadline.', 1718582400, 1),
    (4, 3, 'Don’t forget about the quiz on Friday. Study well!', 1718668800, 1),
    (4, 4, 'Next class will be at 11 AM due to the faculty meeting.', 1718755200, 1),
    (4, 5, 'New project guidelines have been posted. Check the portal for details.', 1718841600, 1),
    (4, 21, 'Can anyone help me with the homework questions?', 1719100800, 0),
    (4, 22, 'Does anyone have the notes from the last lecture?', 1719187200, 0),
    (4, 23, 'Group study session at the library this afternoon.', 1719273600, 0),
    (4, 24, 'Are there any extra credit opportunities available?', 1719360000, 0),
    (4, 25, 'Looking forward to the lab experiment next week.', 1719446400, 0),
    (4, 26, 'I’m having trouble understanding the last topic. Any tips?', 1719532800, 0),
    (4, 27, 'Who else is joining the field trip on Saturday?', 1719619200, 0),
    (4, 28, 'Study group meeting after class today.', 1719705600, 0),
    (4, 29, 'Can we have a review session before the quiz?', 1719792000, 0),
    (4, 30, 'I found some useful resources online. Check the portal.', 1719878400, 0),
    (4, 31, 'Don’t forget to bring your lab coats tomorrow.', 1719964800, 0),
    (4, 32, 'I missed the last class. Can someone share notes?', 1720051200, 0),
    (4, 33, 'The new assignment seems challenging. Any tips?', 1720137600, 0),
    (4, 34, 'Reminder: Submit your projects by Friday.', 1720224000, 0),
    (4, 35, 'Extra credit opportunity posted on the portal.', 1720310400, 0),
    (4, 36, 'I am looking for a study partner for the upcoming quiz.', 1720396800, 0),
    (4, 37, 'Group 3, please meet me after class for project discussion.', 1720483200, 0),
    (4, 38, 'What chapters are covered in the next quiz?', 1720569600, 0),
    (4, 6, 'New course material uploaded on the portal. Please review the new content before our next class.', 1720656000, 1),
    (4, 7, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1720742400, 1),
    (4, 8, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1720828800, 1),
    (4, 9, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1720915200, 1),
    (4, 10, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1721001600, 1),

    -- Add similar entries for classrooms 5 to 30
    (5, 1, 'Welcome to Classroom 5! Looking forward to a productive semester.', 1718496000, 1),
    (5, 2, 'Assignment 3 is available now. Review the instructions and submit by the deadline.', 1718582400, 1),
    (5, 3, 'Don’t forget about the quiz on Monday. Study well!', 1718668800, 1),
    (5, 4, 'Next class will be at 11 AM due to the faculty meeting.', 1718755200, 1),
    (5, 5, 'New project guidelines have been posted. Check the portal for details.', 1718841600, 1),
    (5, 21, 'Can anyone help me with the homework questions?', 1719100800, 0),
    (5, 22, 'Does anyone have the notes from the last lecture?', 1719187200, 0),
    (5, 23, 'Group study session at the library this afternoon.', 1719273600, 0),
    (5, 24, 'Are there any extra credit opportunities available?', 1719360000, 0),
    (5, 25, 'Looking forward to the lab experiment next week.', 1719446400, 0),
    (5, 26, 'I’m having trouble understanding the last topic. Any tips?', 1719532800, 0),
    (5, 27, 'Who else is joining the field trip on Saturday?', 1719619200, 0),
    (5, 28, 'Study group meeting after class today.', 1719705600, 0),
    (5, 29, 'Can we have a review session before the quiz?', 1719792000, 0),
    (5, 30, 'I found some useful resources online. Check the portal.', 1719878400, 0),
    (5, 31, 'Don’t forget to bring your lab coats tomorrow.', 1719964800, 0),
    (5, 32, 'I missed the last class. Can someone share notes?', 1720051200, 0),
    (5, 33, 'The new assignment seems challenging. Any tips?', 1720137600, 0),
    (5, 34, 'Reminder: Submit your projects by Friday.', 1720224000, 0),
    (5, 35, 'Extra credit opportunity posted on the portal.', 1720310400, 0),
    (5, 36, 'I am looking for a study partner for the upcoming quiz.', 1720396800, 0),
    (5, 37, 'Group 3, please meet me after class for project discussion.', 1720483200, 0),
    (5, 38, 'What chapters are covered in the next quiz?', 1720569600, 0),
    (5, 6, 'New course material uploaded on the portal. Please review the new content before our next class.', 1720656000, 1),
    (5, 7, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1720742400, 1),
    (5, 8, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1720828800, 1),
    (5, 9, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1720915200, 1),
    (5, 10, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1721001600, 1),
    
    -- Add similar entries for classrooms 6 to 30, following the same pattern as above
    -- ...

    -- Classroom 30
    (30, 1, 'Welcome to Classroom 30! Let\'s make this a great learning experience.', 1718496000, 1),
    (30, 2, 'Assignment 30 is available now. Review the instructions and submit by the deadline.', 1718582400, 1),
    (30, 3, 'Don’t forget about the quiz on Friday. Study well!', 1718668800, 1),
    (30, 4, 'Next class will be at 11 AM due to the faculty meeting.', 1718755200, 1),
    (30, 5, 'New project guidelines have been posted. Check the portal for details.', 1718841600, 1),
    (30, 21, 'Can anyone help me with the homework questions?', 1719100800, 0),
    (30, 22, 'Does anyone have the notes from the last lecture?', 1719187200, 0),
    (30, 23, 'Group study session at the library this afternoon.', 1719273600, 0),
    (30, 24, 'Are there any extra credit opportunities available?', 1719360000, 0),
    (30, 25, 'Looking forward to the lab experiment next week.', 1719446400, 0),
    (30, 26, 'I’m having trouble understanding the last topic. Any tips?', 1719532800, 0),
    (30, 27, 'Who else is joining the field trip on Saturday?', 1719619200, 0),
    (30, 28, 'Study group meeting after class today.', 1719705600, 0),
    (30, 29, 'Can we have a review session before the quiz?', 1719792000, 0),
    (30, 30, 'I found some useful resources online. Check the portal.', 1719878400, 0),
    (30, 31, 'Don’t forget to bring your lab coats tomorrow.', 1719964800, 0),
    (30, 32, 'I missed the last class. Can someone share notes?', 1720051200, 0),
    (30, 33, 'The new assignment seems challenging. Any tips?', 1720137600, 0),
    (30, 34, 'Reminder: Submit your projects by Friday.', 1720224000, 0),
    (30, 35, 'Extra credit opportunity posted on the portal.', 1720310400, 0),
    (30, 36, 'I am looking for a study partner for the upcoming quiz.', 1720396800, 0),
    (30, 37, 'Group 3, please meet me after class for project discussion.', 1720483200, 0),
    (30, 38, 'What chapters are covered in the next quiz?', 1720569600, 0),
    (30, 6, 'New course material uploaded on the portal. Please review the new content before our next class.', 1720656000, 1),
    (30, 7, 'Guest lecture tomorrow by Dr. Smith on the latest trends in biotechnology. Don\'t miss this opportunity.', 1720742400, 1),
    (30, 8, 'Next week topics include advanced algorithms. Make sure to read the pre-class materials available online.', 1720828800, 1),
    (30, 9, 'Feedback on assignments has been posted. Check the portal for detailed comments and grades.', 1720915200, 1),
    (30, 10, 'Weekly quiz reminder: The quiz will cover Chapters 2 and 3. Ensure you understand the key concepts.', 1721001600, 1);
