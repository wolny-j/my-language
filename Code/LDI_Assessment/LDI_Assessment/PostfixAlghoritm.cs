using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDI_Assessment
{
    internal class PostfixAlghoritm
    {
        List<Token> outputList = new List<Token>();                                         //Output list for tokens
        Stack<Token> operatorStack = new Stack<Token>();                                    //List for the operators which will go next to the output stack
        
        bool isAssigned = false;                                                            //Bools to assign special tokens always at the end of the output list
        bool isIdentifierAssigned = false;
        bool isPrint = false;

        Token temp = new();                                                                 //Temp token for a value to assign to the identifier

        public string Result()                                                              //Returns values form operator stack, assign special tokens and returns result of the algorythm
        {
            if (operatorStack.Count != 0)
            {
                for (int i = 0; i <= operatorStack.Count; i++)
                {
                    outputList.Add(operatorStack.Pop());
                }
            }

            if (isIdentifierAssigned == true)
            {
                outputList.Add(temp);
                isIdentifierAssigned = false;
            }
            if (isAssigned == true)
            {
                outputList.Add(new Token("=", TokenKind.Assignment));
                isAssigned = false;
            }
            if (isPrint == true)
            {
                outputList.Add(new Token("Print", TokenKind.Print));
                isPrint = false;
            }

            var builder = new StringBuilder();
            foreach (var token in outputList)
            {
                builder.Append(token.Text()).Append(" ");
            }
            
            return builder.ToString();                                                      //Returns result in a string form 
        }
        public List<Token> ResultList()                                                     //Returns values form operator stack, assign special tokens and returns result of the algorythm
        {
            
            if (operatorStack.Count != 0)
            {
                for (int i = 0; i <= operatorStack.Count; i++)
                {
                    outputList.Add(operatorStack.Pop());
                }
            }
            if (isIdentifierAssigned == true)
            {
                outputList.Add(temp);
                isIdentifierAssigned = false;
            }
            if (isAssigned == true)
            {
                outputList.Add(new Token("=", TokenKind.Assignment));
                isAssigned = false;
            }
            if (isPrint == true)
            {
                outputList.Add(new Token("Print", TokenKind.Print));
                isPrint = false;
            }
            return outputList;
        }

        public void Clean()                                                                 //Clean the operator stack and output list
        {
            operatorStack.Clear();
            outputList.Clear();
        }
        public void ShuntingYard(Token token)                                               //Do Shunting Yard algorithm on a token
        {
            switch (token.Kind())
            {
                case TokenKind.Integer:
                    outputList.Add(token);
                    break;
                case TokenKind.String:
                    outputList.Add(token);
                    break;
                case TokenKind.Float:
                    outputList.Add(token);
                    break;
                case TokenKind.Bool:
                    outputList.Add(token);
                    break;
                case TokenKind.Add:
                    if (operatorStack.Count > 0)
                    {
                        if (operatorStack.Peek().Kind() != TokenKind.LeftParenthesis)       //Check the parenthesis for ADD token
                        {
                            for (int i = 0; i <= operatorStack.Count; i++)
                            {
                                outputList.Add(operatorStack.Pop());
                            }
                        }
                    }
                    operatorStack.Push(token);

                    break;
                case TokenKind.Substract:

                    if (operatorStack.Count > 0)
                    {
                        if (operatorStack.Peek().Kind() != TokenKind.LeftParenthesis)       //Check the parenthesis for SUBSTRACT token
                        {
                            for (int i = 0; i <= operatorStack.Count; i++)
                            {
                                outputList.Add(operatorStack.Pop());
                            }
                        }
                    }
                    operatorStack.Push(token);
                    break;
                case TokenKind.Multiply:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Divide:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Lesser:
                    operatorStack.Push(token);
                    break;
                case TokenKind.LesserEqual:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Greater:
                    operatorStack.Push(token);
                    break;
                case TokenKind.GreaterEqual:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Equal:
                    operatorStack.Push(token);
                    break;
                case TokenKind.NotEqual:
                    operatorStack.Push(token);
                    break;
                case TokenKind.RightParenthesis:
                    if (operatorStack.Count > 0)
                    {
                        while (operatorStack.Peek().Kind() != TokenKind.LeftParenthesis)            //Look for left parenthesis and add everything between them to a stack
                        {
                            outputList.Add(operatorStack.Pop());
                        }
                    }
                    operatorStack.Pop();
                    break;
                case TokenKind.LeftParenthesis:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Or:
                    operatorStack.Push(token);
                    break;
                case TokenKind.And:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Assignment:
                    isAssigned = true;                                                              //If true add the ASSIGNMENT token at the end of the outputlist
                    break;
                case TokenKind.Identifier:
                    outputList.Add(token);
                    break;
                case TokenKind.IdentidierAssign:
                    isIdentifierAssigned = true;                                                    //If true add the ASSIGNIDENTIFIER token at the end of the outputlist
                    temp = token;
                    break;
                case TokenKind.Print:
                    isPrint = true;                                                                 //If true add the PRINT token at the end of the outputlist
                    break;
                case TokenKind.Negation:
                    operatorStack.Push(token);
                    break;
                case TokenKind.If:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Else:
                    outputList.Add(token);
                    break;
                case TokenKind.While:
                    operatorStack.Push(token);
                    break;
                case TokenKind.Read:
                    operatorStack.Push(token);
                    break;
            }
        }
    }
    
}
