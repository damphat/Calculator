Canculator, handy recursive descent parser in C#
=====

[![Build Status](https://travis-ci.org/damphat/Calculator.svg?branch=master)](https://travis-ci.org/damphat/Calculator)


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

    number -> [0-9]+ (. [0-9]+)?


![alt](screenshot.jpg)