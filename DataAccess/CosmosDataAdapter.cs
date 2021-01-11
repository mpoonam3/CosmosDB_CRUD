using CosmosDB_CRUD.DataAccess.Utility;
using CosmosDB_CRUD.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CosmosDB_CRUD.DataAccess
{
    public class CosmosDataAdapter : ICosmosDataAdapter
    {
        private readonly DocumentClient _client;
        private readonly string _accountUrl;
        private readonly string _primarykey;

        public CosmosDataAdapter(ICosmosConnection connection, IConfiguration config)
        {
           
            _accountUrl = config.GetValue<string>("Cosmos:AccountURL");
            _primarykey = config.GetValue<string>("Cosmos:AuthKey");
            _client = new DocumentClient(new Uri(_accountUrl), _primarykey);
        }

        public async Task<dynamic> GetStudentDetails(string dbName, string name)
        {
            try
            {

                var result = await _client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, name),
                    new FeedOptions { MaxItemCount = 10 });

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> AddNewStudent(string dbName, string name, StudentDetails details)
        {
            try
            {
                details.id = 3;
                await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, name), details);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<StudentDetails> UpsertStudentDetails(string dbName, string name, StudentDetails details)
        {
            ResourceResponse<Document> response = null;
            try
            {
                response = await _client.UpsertDocumentAsync(UriFactory.CreateDocumentUri(dbName, name, details.id.ToString()), details);
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

            return (dynamic)response.Resource;
        }

        public async Task<StudentDetails> DeleteStudentDetails(string dbName, string name, string id)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentUri(dbName, name, id);

                var result = await _client.DeleteDocumentAsync(collectionUri);

                return (dynamic)result.Resource;
            }
            catch (DocumentClientException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
             
    }
}
