﻿using ComPact.Models.V2;
using System;

namespace ComPact.Builders.V2
{
    public class ResponseBuilder
    {
        private readonly Response _response;

        internal ResponseBuilder()
        {
            _response = new Response();
        }

        public ResponseBuilder WithStatus(int status)
        {
            if (status < 100 || status > 599)
            {
                throw new ArgumentOutOfRangeException("Status should be between 100 and 599.");
            }

            _response.Status = status;
            return this;
        }

        public ResponseBuilder WithHeader(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _response.Headers.Add(key, value);
            return this;
        }

        /// <summary>
        /// Type Pact.ResponseBody...
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public ResponseBuilder WithBody(PactJsonContent responseBody)
        {
            _response.Body = responseBody.ToJToken();
            _response.MatchingRules = responseBody.CreateV2MatchingRules();
            return this;
        }

        internal Response Build()
        {
            return _response;
        }
    }
}
