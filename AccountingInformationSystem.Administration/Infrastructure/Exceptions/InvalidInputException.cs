﻿namespace AccountingInformationSystem.Administration.Infrastructure.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        { }
    }
}
