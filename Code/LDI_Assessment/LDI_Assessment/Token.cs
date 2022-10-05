using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace LDI_Assessment
{
    public record Token
    {
        string _text;
        TokenKind _kind;
       
        public Token(String text, TokenKind kind)                       //Token constructors
        {
            _text = text;
            _kind = kind;
        }
        public Token()
        {
            _text = "";
            _kind = TokenKind.Null;
        }

        public dynamic ReturnType()                                     //Return the token vlaue type
        {
            if (_text == "True" && _kind == TokenKind.Bool)
            {
                return true;
            }
            else if (_text == "False" && _kind == TokenKind.Bool)
            {
                return false;
            }
            else if (_kind == TokenKind.Integer)
            {
                return Int32.Parse(_text);
            }
            else if (_kind == TokenKind.Float)
            {
                return float.Parse(_text);
            }
            else if(_kind == TokenKind.String)
            {
                return _text;
            }
            throw new Exception("The " + this._text + " token is invalid");
        }

        public String Text()                                                    //Getters for Token text value and Token Kind
        {
            return _text;
        }

        public TokenKind Kind()
        {
            return _kind;
        }
    }
}
