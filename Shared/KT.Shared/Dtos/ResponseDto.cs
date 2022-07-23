﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KT.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccess { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data,int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccess = true
            };

        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccess = true
            };

        }

        public static ResponseDto<T> Fail(List<string> errors,int statusCode)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                IsSuccess = false,
                Errors = errors
            };

        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                IsSuccess = false,
                Errors = new List<string>() { error}
            };

        }

    }
}
