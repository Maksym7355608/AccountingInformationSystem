using AccountingInformationSystem.Templates.Abstractions.CreateObject;
using AccountingInformationSystem.Templates.Abstractions.DataLoaders;

namespace AccountingInformationSystem.Templates.Abstractions.Interfaces
{
    public interface IDataLoader<TDataLoader> where TDataLoader : BaseDataLoader
    {
        protected TDataLoader LoadCasheData(BaseCreateObject createObject);
    }
}
