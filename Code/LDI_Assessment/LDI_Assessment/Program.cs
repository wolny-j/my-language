using System;
using System.Runtime.CompilerServices;
namespace LDI_Assessment
{
    public class Program
    {
        static void Main()
        {
            int line = 0;
            int ifEndLine = line;
            int whileStartLine = line;
            int whileEndLine = line;
            try
            {
                string path = Directory.GetCurrentDirectory();                      //Get the directory with files
                
                Console.WriteLine("Enter the file name: ");
                string file = Console.ReadLine();
                Console.WriteLine("Opening " + file);
                Console.WriteLine("----------------------------------------------");
                string[] code = System.IO.File.ReadAllLines(path + @"\" + file);   //Read file and add lines to the list
                PostfixAlghoritm postfixAlghoritm = new();
                StackMachine stackMachine = new();                                                                         

                bool endFile = false;
                bool ifDone = false;
                bool whileDone = false;
                bool jumpToWhile = false;

                while (true)                                                        //Run the program till the end of file break
                {
                    Tokenizer tokenizer = new(code[line], line, code.Length);       //Create a tokenizer and add a new line of code to it

                    if(jumpToWhile && stackMachine.GetWhileResult() == true)        //Perform the jumps if needed
                    {
                        WhileLoopJump();
                    }

                    if (stackMachine.GetWhileResult() == false)
                    {
                        EndWhileLoop();
                    }

                    line++;

                    if (stackMachine.GetIfResult() == true)
                    {
                        MakeIfJump();
                    }

                    for (Token token = tokenizer.Get(); token.Kind() != TokenKind.End; token = tokenizer.Get()) //Tokenize the line 
                    {
                        if (token.Kind() == TokenKind.EndFile)                                                  //Check if the token is the last in the file
                        {
                            endFile = true;
                            break;
                        }

                        postfixAlghoritm.ShuntingYard(token);                                                   //Use ShuntingYard on the token

                        if (token.Kind() == TokenKind.If && ifDone == false)                                    //Look for end of the IF and WHILE statements if needed
                        {
                            EvaluateIf();
                        }
                        if(token.Kind() == TokenKind.While && whileDone == false)
                        {
                            EvaluateWhile();
                        }
                        
                    }
                    
                    //postfixAlghoritm.Result();                                                                  
                    List<Token> result = postfixAlghoritm.ResultList();                                         
                    stackMachine.Run(result);                                                                   //Return the postfix notation and add run stack machine to execute tokens
                    
                    if (stackMachine.GetWhileResult() == true && line == whileEndLine)                          //If the line is equal to the end of the loop change bool to make jump in next tokenization
                    {
                        jumpToWhile = true;
                    }

                    stackMachine.Clean();                                                                       //Clean all objects and prepare for next line
                    postfixAlghoritm.Clean();
                    result.Clear();
                    ifDone = false;

                    if (endFile == true)                                                                        //Break the loop if end of file token detected
                    {
                        break;
                    }


                    //ADDITIONAL FUNCTIONS

                    void MakeIfJump()                                                                           //Jump to the if endline when the expression is false 
                    {
                        tokenizer.SetLine(code[ifEndLine]);
                        line = ifEndLine + 1;
                        ifEndLine = 0;
                        stackMachine.SetIfResult(false);
                        tokenizer.SetLineNumber(line);
                    }

                    void EndWhileLoop()                                                                         //End the wile loop when the expression is false
                    {
                        tokenizer.SetLine(code[whileEndLine]);
                        line = whileEndLine;
                        jumpToWhile = false;
                        tokenizer.SetLineNumber(line + 1);
                        stackMachine.SetWhileResult(false);
                    }

                    void WhileLoopJump()                                                                        //Jump to the while endline when the expression is false    
                    {
                        tokenizer.SetLine(code[whileStartLine - 1]);
                        line = whileStartLine - 1;
                        jumpToWhile = false;
                        tokenizer.SetLineNumber(line);
                    }

                    void EvaluateIf()                                                                           //When the IF token is occured go through the code and find the end of the statement
                    {
                        ifEndLine = line;
                        int leftCounter = 1;
                        int rightCounter = 0;
                        for (Token skip = tokenizer.Get(); leftCounter != rightCounter; skip = tokenizer.Get())
                        {
                            if(skip.Kind() == TokenKind.CurlyBracketLeft)
                            {
                                leftCounter++;
                            }
                            else if (skip.Kind() == TokenKind.CurlyBracketRight)
                            {
                                rightCounter++;
                            }
                            else if (skip.Kind() == TokenKind.End)
                            {
                                tokenizer.SetLine(code[ifEndLine]);
                                ifEndLine++;
                                tokenizer.CleanCursor();

                            }

                        }
                        tokenizer.CleanCursor();
                        postfixAlghoritm.Clean();
                        ifDone = true;
                        tokenizer.SetLine(code[line - 1]);
                    }

                    void EvaluateWhile()                                                                         //When the WHILE token is occured go through the code and find the end of expression
                    {
                        int leftCounter = 1;
                        int rightCounter = 0;
                        whileStartLine = line;
                        whileEndLine = line;
                        for (Token skip = tokenizer.Get(); leftCounter != rightCounter; skip = tokenizer.Get())
                        {
                            if (skip.Kind() == TokenKind.While || skip.Kind() == TokenKind.If)
                            {
                                leftCounter++;
                            }
                            else if (skip.Kind() == TokenKind.CurlyBracketRight)
                            {
                                rightCounter++;
                            }
                            else if (skip.Kind() == TokenKind.End)
                            {
                                tokenizer.SetLine(code[whileEndLine]);
                                whileEndLine++;
                                tokenizer.CleanCursor();
                                //line = whileEndLine;
                            }
                        }
                        tokenizer.CleanCursor();
                        postfixAlghoritm.Clean();
                        whileDone = true;
                        tokenizer.SetLine(code[line - 1]);
                    }
                }
                
            }
            
            catch (Exception ex)                                        //Return error with line given if occured
            {
                Console.WriteLine("An error occured! --> Line " + line);
                Console.WriteLine(ex);
            }
            
        }
        
        

    }

    
}