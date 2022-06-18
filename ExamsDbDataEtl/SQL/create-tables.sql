CREATE TABLE public.program
(
    id serial NOT NULL,
    name text NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE public.course
(
    id serial NOT NULL,
    name text NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE public.program_course
(
	program_id int REFERENCES public.program ON DELETE CASCADE,
	course_id int REFERENCES public.course ON DELETE CASCADE
);

CREATE TABLE public.student
(
	id serial NOT NULL,
	first_name text NOT NULL,
	last_name text NOT NULL,
	dob date NOT NULL,
	email text NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE public.instructor
(
	id serial NOT NULL,
	first_name text NOT NULL,
	last_name text NOT NULL,
	dob date NOT NULL,
	email text NOT NULL,
	date_joined date NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE public.question
(
	id serial NOT NULL,
	description text NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE public.answer
(
	id serial NOT NULL,
	description text NOT NULL,
	is_correct boolean NOT NULL,  
	question_id int REFERENCES public.question ON DELETE CASCADE,
	PRIMARY KEY (id)
);


CREATE TABLE public.program_student 
(
	program_id int REFERENCES public.program ON DELETE CASCADE,
	student_id int REFERENCES public.student ON DELETE CASCADE
);

CREATE TABLE public.exam
(
	id serial NOT NULL,
	name text NOT NULL,
	course_id int REFERENCES public.course,
	start_time timestamp NOT NULL,
	duration interval NOT NULL,
	PRIMARY KEY (id)
);

CREATE TABLE public.exam_question
(
	id serial NOT NULL,
	exam_id int REFERENCES public.exam ON DELETE CASCADE,
	question_id int REFERENCES public.question ON DELETE CASCADE,
	PRIMARY KEY (id)
);

CREATE TABLE public.exam_question_response
(
	id serial NOT NULL,
	exam_question_id int REFERENCES public.exam_question ON DELETE CASCADE,
	student_id int REFERENCES public.student ON DELETE CASCADE,
	answer_id int REFERENCES public.answer ON DELETE CASCADE,
	points int NOT NULL,
	PRIMARY KEY (id)
);