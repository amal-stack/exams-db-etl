# Student Exam Database Data ETL

## Tables

| Table 	                 | Purpose                                                                         |
|----------------------------|---------------------------------------------------------------------------------|
| `program`                  | Represents an academic program offered at a university/educational institution. |
| `course`                   | Represents one of the courses under the program.                                |
| `program_course`           | Bridge table that defines the relationship between programs and courses.        |
| `student`                  | A student who enrolls in one or more programs.                                  |   
| `program_student`          | Bridge table that represents a registration of a student to a program.          |
| `instructor`               | An instructor who manages exams.                                                |
| `question`                 | A multiple-choice question that can be part of one or more exams.               |
| `answer`                   | One of the answers of a question.                                               |
| `exam`	                 | A scheduled exam.                                                               |
| `exam_question`	         | A question part of the exam.                                                    |
| `exam_question_response`   | A student response to a question part of the exam.                              |

## Source Datasets

1. [College Majors | Kaggle](https://www.kaggle.com/datasets/tunguz/college-majors)
2. [SciQ Dataset — Allen Institute for AI](https://allenai.org/data/sciq)
