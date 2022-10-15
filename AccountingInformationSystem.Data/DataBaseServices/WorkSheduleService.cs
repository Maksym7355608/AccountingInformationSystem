using AccountingInformationSystem.Data.DataBaseServices.Settings;
using AccountingInformationSystem.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AccountingInformationSystem.Data.DataBaseServices
{
    public class WorkSheduleService
    {
        private readonly IMongoCollection<WorkShedule> _employeeCollection;

        public WorkSheduleService(IOptions<AccountingInformationDataBaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _employeeCollection = mongoDatabase.GetCollection<WorkShedule>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<WorkShedule>> GetAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

        public async Task<WorkShedule?> GetAsync(long identificationNumber) =>
            await _employeeCollection.Find(x => x.IdentificationNumber == identificationNumber).FirstOrDefaultAsync();

        public async Task CreateAsync(WorkShedule newWorkShedule) =>
            await _employeeCollection.InsertOneAsync(newWorkShedule);

        public async Task UpdateAsync(long identificationNumber, WorkShedule updatedWorkShedule) =>
            await _employeeCollection.ReplaceOneAsync(x => x.IdentificationNumber == identificationNumber, updatedWorkShedule);

        public async Task RemoveAsync(long identificationNumber) =>
            await _employeeCollection.DeleteOneAsync(x => x.IdentificationNumber == identificationNumber);
    }
}
