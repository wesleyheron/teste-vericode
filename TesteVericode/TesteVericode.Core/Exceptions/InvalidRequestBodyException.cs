﻿namespace TesteVericode.Core.Exceptions
{
    public class InvalidRequestBodyException : Exception
    {
        public string[] Errors { get; set; }
    }
}
