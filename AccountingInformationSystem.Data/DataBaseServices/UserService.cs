using AccountingInformationSystem.Data.DataBaseServices.Settings;
using AccountingInformationSystem.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AccountingInformationSystem.Data.DataBaseServices
{
    public class UserService
    {
        private readonly IMongoCollection<User> _employeeCollection;

        public UserService(IOptions<AccountingInformationDataBaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _employeeCollection = mongoDatabase.GetCollection<User>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(long identificationNumber) =>
            await _employeeCollection.Find(x => x.IdentificationNumber == identificationNumber).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _employeeCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(long identificationNumber, User updatedUser) =>
            await _employeeCollection.ReplaceOneAsync(x => x.IdentificationNumber == identificationNumber, updatedUser);

        public async Task RemoveAsync(long identificationNumber) =>
            await _employeeCollection.DeleteOneAsync(x => x.IdentificationNumber == identificationNumber);
    }
}
