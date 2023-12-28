namespace LabWork5
{
    class Operation : Token
    {
        public char operation;
        public int priority;

        public Operation(char operation) 
        {  
            this.operation = operation;

            if (this.operation == '+') { priority = 1; }
            else if (this.operation == '-') { priority = 1; }
            else if (this.operation == '*') { priority = 2; }
            else if (this.operation == '/') { priority = 2; }
        }
    }
}
