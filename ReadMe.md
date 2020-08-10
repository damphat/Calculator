# Calculator, handy recursive descent parser in C#

[![Test Status](https://github.com/damphat/Calculator/workflows/test/badge.svg)](https://github.com/damphat/Calculator/actions?query=test)


BNF Grammar used in the project:

    exp -> exp "+" term
         | exp "-" term
         | term

    term -> term "*" factor
         | term "/" factor
         | factor

    factor -> "+" factor
         | "-" factor
         | "(" exp ")"
         | number

    number -> [0-9]+ ([.][0-9]+)? ([eE](+|-)?[0-9]+)


![alt](screenshot.jpg)
