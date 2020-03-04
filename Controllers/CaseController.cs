﻿using Microsoft.AspNetCore.Mvc;
using MongoDAL;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace QueryApp.Controllers
{
    [Route("api/[controller]")]
    public class CaseController : Controller
    {
        MongoDBRepository mongoRepository = new MongoDBRepository("mongodb+srv://projector:projector@cluster0-gr1bz.mongodb.net/test?retryWrites=true&w=majority");

        public CaseController()
        {
            mongoRepository.Connect();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            IAsyncCursor<BsonDocument> cursor = mongoRepository.GetCollection("Case").FindSync<BsonDocument>(FilterDefinition<BsonDocument>.Empty);
            List<string> returnVal = new List<string>();

            while (cursor.MoveNext())
            {
                IEnumerable<BsonDocument> batch = cursor.Current;
                foreach (BsonDocument document in batch)
                {
                    returnVal.Add(document.ToString());
                }
            }

            return returnVal;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Id", id.ToString());
            var entityCollection = mongoRepository.GetCollection("Case");

            var searchResult = entityCollection.Find(filter).First();

            return searchResult.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
