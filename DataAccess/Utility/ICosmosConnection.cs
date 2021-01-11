using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace CosmosDB_CRUD.DataAccess.Utility
{   
    public interface ICosmosConnection
    {
        Task<DocumentClient> InitializeAsync(string collectionId);
    }
}
