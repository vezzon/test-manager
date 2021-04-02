using System;
using System.Net;
using System.Net.Http;

namespace Testro.TestingManagement.WebApi.Exceptions
{
    public class NotFoundException : HttpRequestException
    {
        public NotFoundException() : base("Not found.", null, HttpStatusCode.NotFound)
        {
            
        }
    }
}