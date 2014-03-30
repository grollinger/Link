﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tavis;
using Xunit;

namespace LinkTests
{
    public class LinkParameterTests
    {
        [Fact]
        public void Add_parameters_to_uri_without_query_string()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer"),
                AddNonTemplatedParametersToQueryString = true
            };

            
            var request = link.BuildRequestMessage(new Dictionary<string, object> {{"id", 99}});

            
            Assert.Equal("http://example/customer?id=99", request.RequestUri.OriginalString);
        }


        [Fact]
        public void Add_parameters_to_uri_with_query_string()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer?view=true"),
                AddNonTemplatedParametersToQueryString = true
            };

            

            var request = link.BuildRequestMessage(new Dictionary<string,object> {{"id", 99}});
            Assert.Equal("http://example/customer?view=true&id=99", request.RequestUri.OriginalString);
        }


        // TODO Not sure how to resolve this.  How do I know not to create a query string param with the id?
        // I could regex into the path, but that's just ugly.
        [Fact]
        public void Add_parameters_to_uri_with_query_string_ignoring_path_parameter()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer/{id}?view=true"),
                AddNonTemplatedParametersToQueryString = true
            };

            

            var request = link.BuildRequestMessage(new Dictionary<string, object>
            {
                {"id", 99},
                {"context", "detail"}
            });
            Assert.Equal("http://example/customer/99?view=true&context=detail", request.RequestUri.OriginalString);
        }

        [Fact]
        public void Update_existing_parameters_in_query_string_automatically()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer?view=true"),
                AddNonTemplatedParametersToQueryString = true
            };

            var request = link.BuildRequestMessage(new Dictionary<string, object> {{"view", false}});
            
            Assert.Equal("http://example/customer?view=False", request.RequestUri.OriginalString);
        }


        [Fact]
        public void Update_existing_parameters_in_query_string()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer?view=true"),
                AddNonTemplatedParametersToQueryString =  true
            };

            
            var request = link.BuildRequestMessage(new Dictionary<string,object> {{"view", false}});
            Assert.Equal("http://example/customer?view=False", request.RequestUri.OriginalString);
        }

        [Fact]
        public void Add_multiple_parameters_to_uri()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer"),
                AddNonTemplatedParametersToQueryString = true
            };

            

            var request = link.BuildRequestMessage(new Dictionary<string, object>
            {
                {"id", 99},
                {"view", false}
            });
            Assert.Equal("http://example/customer?id=99&view=False", request.RequestUri.OriginalString);
        }


        [Fact]
        public void Add_no_parameters_to_uri()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer"),
                AddNonTemplatedParametersToQueryString = true
            };

            var request = link.BuildRequestMessage();
            Assert.Equal("http://example/customer", request.RequestUri.OriginalString);
        }

        [Fact]
        public void Change_an_existing_parameter()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer?view=False"),
                AddNonTemplatedParametersToQueryString = true
            };


            var request = link.BuildRequestMessage(new Dictionary<string, object> {{"view", true}});
            Assert.Equal("http://example/customer?view=True", request.RequestUri.OriginalString);
        }


        [Fact]
        public void Change_an_existing_parameter_within_multiple()
        {
            var link = new Link()
            {
                Target = new Uri("http://example/customer?view=False&foo=bar"),
                AddNonTemplatedParametersToQueryString = true
            };
            var parameters = link.GetQueryStringParameters();
            parameters["view"] = true;

            var request = link.BuildRequestMessage(parameters);
            Assert.Equal("http://example/customer?view=True&foo=bar", request.RequestUri.OriginalString);
        }
    }
}
