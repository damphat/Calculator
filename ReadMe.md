Learn to implement a handy recursive parser in C#

BNF
    exp -> exp "+" term
         | exp "-" term
         | term

    term -> term "*" factor
         | term "/" factor
         | factor

    factor -> "+" factor
         | "-" factor
         | number

    number -> [0-9]+ (. [0-9]+)?