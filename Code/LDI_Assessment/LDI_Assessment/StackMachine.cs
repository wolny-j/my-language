using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDI_Assessment
{
    internal class StackMachine
    {
        Stack<Token> stack = new Stack<Token>();                                                    //Stack for the all opearations rsults
        IDictionary<Token, Token> globalVariables = new Dictionary<Token, Token>();                 //Data structure used to sotre all variables created by the user                                                                      

        bool ifResult = false;
        bool whileResult = true;

        public void Push(Token value)                                                               //Push tokend to the stack
        {
            stack.Push(value);
        }
        public void SetIfResult(bool x)                                                             //Setters and getters
        {
            ifResult = x;
        }
        public bool GetIfResult()
        {
            return ifResult;
        }
        public void SetWhileResult(bool x)
        {
            whileResult = x;
        }
        public bool GetWhileResult()
        {
            return whileResult;
        }
        public void Print()                                                                         //Execute PRINT token
        {
            bool done = false;
            var value = stack.Pop();
            if (value.Kind() == TokenKind.Identifier)                                           //Find the right value for identifier
            {
                foreach (Token variable in globalVariables.Keys)                                    
                {
                    if (variable.Text() == value.Text())
                    {
                        Console.WriteLine(globalVariables[variable]);
                        done = true;
                        break;
                    }
                }
                if (!done)
                {
                    throw new Exception("Unassigned variable used.");
                }
            }
            else
            {
                Console.WriteLine(value.Text());
            }
        }
        public Token Read()                                                                  //Execute READ token and get user input
        {
            Print();
            string value = Console.ReadLine();
            if (value == "")
            {
                return new Token("", TokenKind.String);
            }
            if (value[0] == '\'')                                                           //Special prefix for number instead of string type
            {
                if(value[1] == 'n')
                {
                    value = value.Substring(2, value.Length - 2);
                    return new Token(value, TokenKind.Integer);
                }
                else
                {
                    throw new Exception("Invalid string prefix occured");
                }
            }
            else
            {
                return new Token(value, TokenKind.String);
            }
        }
        public Token Negate()                                                               //Execute the NEGATION token
        {
            var value = stack.Pop();
            if (value.Kind() == TokenKind.Bool)
            {
                if (value.Text() == "True")
                {
                    return new Token("False", TokenKind.Bool);
                }
                else
                {
                    return new Token("True", TokenKind.Bool);
                }
            }
            else
            {
                throw new Exception("The variable must be a boolean type.");
            }
        }

        public Token Add()                                                                  //Execute ADD token
        {
            Token type = stack.Peek();
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            var result = value1 + value2;
            if (type.Kind() == TokenKind.String)
            {
               return new Token(result, TokenKind.String);
            }
            else
            {
                if (result.GetType() == typeof(System.Int32))
                {
                    return new Token(result.ToString(), TokenKind.Integer);
                }
                else if (result.GetType() == typeof(float))
                {
                    return new Token(result.ToString(), TokenKind.Float);
                }
                else
                {
                    return new Token(result.ToString(), TokenKind.Integer);
                }
            }
        }
        public Token Substract()                                                            //Execute the SUBSTRACT token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            var result = value1 - value2;

            if (result.GetType() == typeof(System.Int32))
            {
                return new Token(result.ToString(), TokenKind.Integer);
            }
            else if (result.GetType() == typeof(float))
            {
                return new Token(result.ToString(), TokenKind.Float);
            }
            else
            {
                return new Token(result.ToString(), TokenKind.Integer);
            }
        }
        public Token Multiply()                                                             //Execute the MULTIPLY token
        {
            Token type = stack.Peek();
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            var result = value1 * value2;

            if (result.GetType() == typeof(System.Int32))
            {
                return new Token(result.ToString(), TokenKind.Integer);
            }
            else if (result.GetType() == typeof(float))
            {
                return new Token(result.ToString(), TokenKind.Float);
            }
            else
            {
                return new Token(result.ToString(), TokenKind.Integer);
            }
        }
        public Token Divide()                                                               //Execute the DIVIDE token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            var result = value1 / value2;

            if (result.GetType() == typeof(System.Int32))
            {
                return new Token(result.ToString(), TokenKind.Integer);
            }
            else if (result.GetType() == typeof(float))
            {
                return new Token(result.ToString(), TokenKind.Float);
            }
            else
            {
                return new Token(result.ToString(), TokenKind.Integer);
            }
        }

        public Token CompareGreater()                                                   //Execute the GREATER token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            if (value1 > value2)
            {
                return new Token("True", TokenKind.Bool);
            }
            else
            {
                return new Token("False", TokenKind.Bool);
            }

        }
        public Token CompareLesser()                                                    //Execute the LESSER token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            if (value1 < value2)
            {
                return new Token("True", TokenKind.Bool);
            }
            else
            {
                return new Token("False", TokenKind.Bool);
            }

        }
        public Token CompareLesserEqual()                                              //Execute the LESSEREQUAL token                 
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            if (value1 <= value2)
            {
                return new Token("True", TokenKind.Bool);
            }
            else
            {
                return new Token("False", TokenKind.Bool);
            }

        }
        public Token CompareGreaterEqual()                                             //Execute the GREATEREQUAL token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            if (value1 >= value2)
            {
                return new Token("True", TokenKind.Bool);
            }
            else
            {
                return new Token("False", TokenKind.Bool);
            }

        }

        public Token CompareEqual()                                                     //Execute the EQUAL token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            if (value1 == value2)
            {
                return new Token("True", TokenKind.Bool);
            }
            else
            {
                return new Token("False", TokenKind.Bool);
            }
        }
        public Token CompareNotEqual()                                                  //Execute the NOTEQUAL token
        {
            var value2 = stack.Pop().ReturnType();
            var value1 = stack.Pop().ReturnType();

            if (value1 != value2)
            {
                return new Token("True", TokenKind.Bool);
            }
            else
            {
                return new Token("False", TokenKind.Bool);
            }
        }
        public Token MakeAnd()                                                          //Execute AND token
        {
            var value2 = stack.Pop();
            var value1 = stack.Pop();

            if (value1.Kind() == TokenKind.Bool && value2.Kind() == TokenKind.Bool)
            {
                if(value1.Text() == "False" || value2.Text() == "False")
                {
                    return new Token("False", TokenKind.Bool);
                }
                else
                {
                    return new Token("True", TokenKind.Bool);
                }
            }
            else
            {
                throw new Exception("At least one of the variables is not a boolean type.");
            }
        }
        public Token MakeOr()                                                            //Execute the OR token
        {
            var value2 = stack.Pop();
            var value1 = stack.Pop();

            if (value1.Kind() == TokenKind.Bool && value2.Kind() == TokenKind.Bool)
            {
                if (value1.Text() == "False" && value2.Text() == "False")
                {
                    if(value1.Text() == "True" && value2.Text() == "True")
                    {
                        return new Token("True", TokenKind.Bool);
                    }
                    return new Token("False", TokenKind.Bool);
                }
                else
                {
                    return new Token("True", TokenKind.Bool);
                }
            }
            else
            {
                throw new Exception("At least one of the variables is not a boolean type.");
            }
        }
        public void AssignValue()                                                        //Execute the ASSAIGN token
        {
            bool done = false;
            var identifier = stack.Pop();
            var value = stack.Pop();
            foreach (Token variable in globalVariables.Keys)
            {

                if (variable.Text() == identifier.Text())
                {
                    globalVariables[variable] = value;
                    done = true;
                }

            }
            if (!done)
            {
                globalVariables.Add(identifier, value);
            }
        }
        public void MakeIf()                                                            //Execute the IF token
        {
            var value = stack.Pop().Text();
            if(value == "False")
            {
                ifResult = true;
            }
            else if(value == "True")
            {    
            }
            else
            {
                throw new Exception("Error only bool statements are allowed");
            }
        }
        public void MakeWhile()                                                         //Execute the WHILE token
        {
            var value = stack.Pop().Text();
            if (value == "False")
            {
                whileResult = false;
            }
            else if (value == "True")
            {
                whileResult = true;
            }
            else
            {
                throw new Exception("Error only bool statements are allowed");
            }
        }

        public void Clean()                                                             //Clean the stack                                             
        {
            stack.Clear();
        }

        
        public void Run(List<Token> input)                                             //Go through all of the tokens given
        {
            foreach (Token token in input)
            {
                switch(token.Kind())
                {
                    case TokenKind.Integer:
                        Push(token);
                        break;
                    case TokenKind.String:
                        Push(token);
                        break;
                    case TokenKind.Float:
                        Push(token);
                        break;
                    case TokenKind.Add:
                        Push(Add());
                        break;
                    case TokenKind.Substract:
                        Push(Substract());
                        break;
                    case TokenKind.Multiply:
                        Push(Multiply());
                        break;
                    case TokenKind.Divide:
                        Push(Divide());
                        break;
                    case TokenKind.Bool:
                        Push(token);
                        break;
                    case TokenKind.Greater:
                        Push(CompareGreater());
                        break;
                    case TokenKind.Lesser:
                        Push(CompareLesser());
                        break;
                    case TokenKind.GreaterEqual:
                        Push(CompareGreaterEqual());
                        break;
                    case TokenKind.LesserEqual:
                        Push(CompareLesserEqual());
                        break;
                    case TokenKind.Equal:
                        Push(CompareEqual());
                        break;
                    case TokenKind.NotEqual:
                        Push(CompareNotEqual());
                        break;
                    case TokenKind.And:
                        Push(MakeAnd());
                        break;
                    case TokenKind.Or:
                        Push(MakeOr());
                        break;
                    case TokenKind.Identifier:
                        bool done = false;
                        foreach (Token variable in globalVariables.Keys)                        //Go through all indentifiers and fint the right key
                        {
                            if(variable.Text() == token.Text())
                            {
                                Push(globalVariables[variable]);
                                done = true;
                            }
                        }
                        if (!done)
                        {
                            Push(token);
                        }
                        break;
                    case TokenKind.IdentidierAssign:
                        Push(token);
                        break;
                    case TokenKind.Assignment:
                        AssignValue();
                        break;
                    case TokenKind.Print:
                        Print();
                        break;
                    case TokenKind.Negation:
                        Push(Negate());
                        break;
                    case TokenKind.If:
                        MakeIf();
                        break;
                    case TokenKind.While:
                        MakeWhile();
                        break;
                    case TokenKind.Read:
                        Push(Read());
                        break;
                    default:
                        throw new Exception("Invalid token occured");

                }
                  
                
            }
        }
    }
}
