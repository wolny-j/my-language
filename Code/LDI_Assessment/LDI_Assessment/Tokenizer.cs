using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDI_Assessment
{
    public class Tokenizer
    {
        private string text;
        private int cursor = 0;
        private int line;
        private int totalLines;
        bool isIf = false;
        bool isWhile = false;
        public Tokenizer(String _text, int _line, int _totalLines)                          //Constructor for tokenizer
        {
            text = _text;
            line = _line + 1;
            totalLines = _totalLines;
        }
        public int GetLine()                                                                //Getters and setters
        {
            return line;
        }
        public void SetLineNumber(int x)
        {
            line = x;
        }
        public void SetLine(string x)
        {
            text = x;
        }
        public string GetInput()
        {
            return text;
        }
        public void CleanCursor()                                                           //Set the cursor value to 0
        {
            cursor = 0;
        }
        public Token Get()                                                                  //Parse the token
        {
            if(text.Length == 0)                                                            //Skip the empty lines
            {
                if (line == totalLines)                                                     //If it is the last line return ENDFILE token
                {
                    return new Token("EndFile", TokenKind.EndFile);
                }
                else
                {
                    line++;
                    return new Token("End", TokenKind.End);
                }
            }
            while (cursor < text.Length)
            {
                int stringHead;
                int numberHead;
                int lenght;
                int counter = 0;
                switch (text[cursor])                                                      //Check all possible cases for tokens
                {
                    case '\t':
                        cursor++;
                        break;
                    case '\n':
                        break;
                    case '\f':
                        break;
                    case '\r':
                        break;
                    case ' ':
                        cursor++;
                        break;
                    case '_':
                        cursor++;
                        break;
                    case 'T':                                                               //TRUE
                        cursor++;
                        if(text.Substring(cursor, 3) == "rue")
                        {
                            cursor += 3;
                            return new Token("True", TokenKind.Bool);
                        }
                        break;
                    case 'F':                                                               //FALSE
                        cursor++;
                        if (text.Substring(cursor, 4) == "alse")
                        {
                            cursor += 4;
                            return new Token("False", TokenKind.Bool);
                        }
                        break;
                    case 'P':                                                               //PRINT
                        cursor++;
                        if (text.Substring(cursor, 4) == "rint")
                        {
                            cursor += 4;
                            return new Token("Print", TokenKind.Print);
                        }
                        break;
                    case 'R':                                                               //READ
                        cursor++;
                        if (text.Substring(cursor, 3) == "ead")
                        {
                            cursor += 3;
                            return new Token("Read", TokenKind.Read);
                        }
                        break;
                    case 'I':                                                               //IF
                        cursor++;
                        if(text[cursor] == 'f')
                        {
                            cursor++;
                            isIf = true;
                            return new Token("If", TokenKind.If);
                        }
                        break;
                    case 'E':                                                              //ELSE
                        cursor++;
                        if (text.Substring(cursor, 3) == "lse")
                        {
                            cursor += 3;
                            return new Token("Else", TokenKind.Else);
                        }
                        break;
                    case 'W':                                                               //WHILE
                        cursor++;
                        if (text.Substring(cursor, 4) == "hile")
                        {
                            cursor += 4;
                            isWhile = true;
                            return new Token("While", TokenKind.While);
                        }
                        break;
                    case '+':                                                               //ADD
                        cursor++;
                        return new Token(text[cursor-1].ToString(), TokenKind.Add);
                    case '-':                                                               //SUBSTRACT or MINUS
                        cursor++;
                        if (char.IsDigit(text[cursor]))
                        {
                            bool isFloat = false;
                            counter = 0;
                            for (numberHead = cursor; cursor < text.Length; cursor++)   //Loop through the whole number
                            {
                                if (text[cursor] == ';' || text[cursor] == '+' || text[cursor] == '*' || text[cursor] == '/' || text[cursor] == '-' || text[cursor] == '(' || text[cursor] == ')' || text[cursor] == ' ' || text[cursor] == '>' || text[cursor] == '<' || text[cursor] == '=')
                                {                                           
                                    if (isFloat)                              //Return float or int type
                                    {
                                        return new Token(text.Substring(numberHead-1, counter+1), TokenKind.Float);
                                    }
                                    else
                                    {
                                        return new Token(text.Substring(numberHead-1, counter+1), TokenKind.Integer);
                                    }
                                }
                                else if (text[cursor] == '.')               //Check if the number is a float type
                                {
                                    isFloat = true;
                                }
                                counter++;
                            }
                            lenght = text.Length - numberHead;
                            if (isFloat)                                   //Return float or int type
                            {
                                return new Token(text.Substring(numberHead, lenght), TokenKind.Float);
                            }
                            else
                            {
                                return new Token(text.Substring(numberHead, lenght), TokenKind.Integer);
                            }
                        }
                        return new Token(text[cursor - 1].ToString(), TokenKind.Substract);
                    case '*':                                                                            //MULTIPLY
                        cursor++;
                        if(text[cursor] == '*')
                        {
                            return new Token(";", TokenKind.End);
                        }
                        return new Token(text[cursor - 1].ToString(), TokenKind.Multiply);
                    case '/':                                                                            //DIVIDE
                        cursor++;
                        return new Token(text[cursor - 1].ToString(), TokenKind.Divide);
                    case ';':                                                                            //ENDLINE or ENDFILE
                        cursor++;
                        if (line == totalLines)                                                          //Check if this is the end of the file
                        {
                            return new Token(text[cursor - 1].ToString(), TokenKind.EndFile);
                        }
                        else
                        {
                            line++;
                            
                            return new Token(text[cursor - 1].ToString(), TokenKind.End);
                        } 
                    case '=':                                                                           //EQUAL or ASSIGN
                        cursor++;
                        if (text[cursor++] == '=')
                        {
                            return new Token("==", TokenKind.Equal);
                        }
                        else
                        {
                            return new Token("=", TokenKind.Assignment);
                        }
                    case '!':                                                                           //NEGATION
                        cursor++;
                        if (text[cursor] == '=')
                        {
                            cursor++;
                            return new Token("!=", TokenKind.NotEqual);
                        }
                        else
                        {
                            return new Token("!", TokenKind.Negation);
                        }
                    case '>':                                                                           //GREATER or GREATEREQUAL
                        cursor++;
                        if (text[cursor++] == '=')
                        {
                            return new Token(">=", TokenKind.GreaterEqual);
                        }
                        else
                        {
                            return new Token(">", TokenKind.Greater);
                        }
                    case '<':                                                                           //LESSER OR LESSEREQUAL
                        cursor++;
                        if (text[cursor + 1] == '=')
                        {
                            return new Token("<=", TokenKind.LesserEqual);
                        }
                        else
                        {
                            return new Token("<", TokenKind.Lesser);
                        }
                    case '(':                                                                           //PARENTHISISES
                        cursor++;
                        return new Token(text[cursor - 1].ToString(), TokenKind.LeftParenthesis);
                    case ')':
                        cursor++;
                        return new Token(text[cursor - 1].ToString(), TokenKind.RightParenthesis);
                    case '[':
                        cursor++;
                        
                        return new Token(text[cursor - 1].ToString(), TokenKind.SquareBracketLeft);
                    case ']':
                        cursor++;
                        return new Token(text[cursor - 1].ToString(), TokenKind.SquareBracketRight);
                    case '"':                                                                           //STRING                      
                        cursor++;
                        counter = 0;
                        for (stringHead = cursor; cursor < text.Length; cursor++)
                        {
                            counter++;
                            if (text[cursor] == '"')
                            {
                                cursor++;
                                return new Token(text.Substring(stringHead, counter-1), TokenKind.String);
                            }
                        }
                        break;
                    case '{':                                                                           //CURLYBRACKET
                        cursor++;
                        if(isIf)                                                                        //If there is a IF token enable treat it as a end of the line
                        {
                            line++;
                            isIf = false;
                            return new Token("End", TokenKind.End);
                        }
                        else if (isWhile)                                                                     //If there is a WHILE token enable treat it as a end of the line
                        {
                            line++;
                            isWhile = false;
                            return new Token("End", TokenKind.End);
                        }
                        return new Token("{", TokenKind.CurlyBracketLeft);
                    case '}':
                        cursor++;
                        return new Token("}", TokenKind.CurlyBracketRight);
                    default:    
                        if(char.IsDigit(text[cursor]))                                                  //NUMBER 
                        {
                            bool isFloat = false;
                            counter = 0;
                            for (numberHead = cursor; cursor < text.Length; cursor++)
                            {
                                if (text[cursor] == ';' || text[cursor] == '+' || text[cursor] == '*' || text[cursor] == '/' || text[cursor] == '-' || text[cursor] == '(' || text[cursor] == ')' || text[cursor] == ' ' || text[cursor] == '>' || text[cursor] == '<' || text[cursor] == '=')
                                {
                                    if (isFloat)                                                       //Return float or int type
                                    {
                                        return new Token(text.Substring(numberHead, counter), TokenKind.Float);
                                    }
                                    else
                                    {
                                        return new Token(text.Substring(numberHead, counter), TokenKind.Integer);
                                    }
                                }
                                else if(text[cursor] == '.')
                                {
                                    isFloat = true;
                                }
                                counter++;
                            }
                            lenght = text.Length - numberHead;
                            if (isFloat)                                                                //Return float or int type
                            {
                                return new Token(text.Substring(numberHead, lenght), TokenKind.Float);
                            }
                            else
                            {
                                return new Token(text.Substring(numberHead, lenght), TokenKind.Integer);
                            }
                        }
                        if (!char.IsLetter(text[cursor]))
                        {
                            throw new Exception("Invalid character: " + text[cursor] + ".");
                        }
                        counter = 0;
                        if(text[cursor] == 'A')                                              //AND
                        {
                            cursor++;
                            if(text.Substring(cursor, 2) == "nd")
                            {
                                cursor += 2;
                                return new Token("AND", TokenKind.And);
                            }
                        }
                        if (text[cursor] == 'O')                                            //OR
                        {
                            cursor++;
                            if (text[cursor] == 'r')
                            {
                                cursor++;
                                return new Token("OR", TokenKind.Or);
                            }
                        }
                        stringHead = cursor++;                                              //IDENTIFIER
                        while (true)
                        {
                            if (text[cursor] == ';' || text[cursor] == '+' || text[cursor] == '*' || text[cursor] == '/' || text[cursor] == '-' || text[cursor] == '(' || text[cursor] == ')' || text[cursor] == ' ' || text[cursor] == '>' || text[cursor] == '<' || text[cursor] == '=')
                            {
                                break;
                            }
                            counter++;
                            cursor++;
                            
                        }
                        if ((text[cursor] == ' ' && text[cursor + 1] == '=' && text[cursor + 2] != '=') || text[cursor] == '=')
                        {                                                                       //If there is a "=" (assign) token create assign identifier that is treated diffrently
                            return new Token(text.Substring(stringHead, counter + 1), TokenKind.IdentidierAssign);
                        }
                        else
                        {                                                                   //Create read only identifier
                            return new Token(text.Substring(stringHead, counter + 1), TokenKind.Identifier);
                        }
                }
            }                                                                        
            throw new Exception("No ';' at the end of the line.");                          //Throw error when no ; detected at the end of the line       
        }
        
    }
}
