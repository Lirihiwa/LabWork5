namespace LabWork5
{
    class Parenthesis :  Token
    {
        public bool isOpen;
        public char bracket;

        public Parenthesis(char parenthesis)
        {
            if (parenthesis == '(') {
                isOpen = true;
                return;
            }
            isOpen = false;
            bracket = parenthesis;
        }
    }
}

