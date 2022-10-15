using AccountingInformationSystem.Data.DataBaseServices.Settings;
using AccountingInformationSystem.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AccountingInformationSystem.Data.DataBaseServices
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(IOptions<AccountingInformationDataBaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Employee>> GetAsync() =>
        await _employeeCollection.Find(_ => true).ToListAsync();

        public async Task<Employee?> GetAsync(long identificationNumber) =>
            await _employeeCollection.Find(x => x.IdentificationNumber == identificationNumber).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee newEmployee) =>
            await _employeeCollection.InsertOneAsync(newEmployee);

        public async Task UpdateAsync(long identificationNumber, Employee updatedEmployee) =>
            await _employeeCollection.ReplaceOneAsync(x => x.IdentificationNumber == identificationNumber, updatedEmployee);

        public async Task RemoveAsync(long identificationNumber) =>
            await _employeeCollection.DeleteOneAsync(x => x.IdentificationNumber == identificationNumber);
    }
}
