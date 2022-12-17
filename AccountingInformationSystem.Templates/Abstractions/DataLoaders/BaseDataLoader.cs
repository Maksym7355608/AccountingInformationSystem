namespace AccountingInformationSystem.Templates.Abstractions.DataLoaders
{
    public abstract class BaseDataLoader
    {
        public IEnumerable<long> Ids { get; set; }
    }
}
