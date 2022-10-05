The syntax of my language:

All lines should end with a ';' sign.

To assign the value to a variable use this expression: 
[identifier] '=' [value];

Strings creation:
"[string text]" 

The keywords available in my language:
And - make and boolean operation. 
Or - make or operation		 
== - is equal
!= - is not equal
! - make negation
>, >= - is greater/greater or equal
<, <= - is lesser/lesser or equal
True/False - boolean values

Examples: ((2 < 3) And False), (!True Or (2 <= 2)), "Hello world" != "hello world"

Print [value, identifier or expression] - Print to a console
Read("[Potential string text]") - Read the user input from a console

If statement:
If([Boolean expression)]{
CODE HERE
};	<-If has to be ended with ';' character.
Language does not provide the "Else" functionality.

While loop:
While([Boolean expression]){
CODE HERE
};	<-The loop has to be ended with ';' character.

To make a comment use the "**" at the beginning of the comment
Example:
**This is a comment in my language