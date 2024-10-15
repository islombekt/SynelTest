﻿namespace Employees.MVC.Models.DTOs
{
    public class Response<T>
    {
        public bool IsSuccess {  get; set; }
        public string Message { get; set; }=string.Empty;
        public T Data { get; set; }
    }
}
