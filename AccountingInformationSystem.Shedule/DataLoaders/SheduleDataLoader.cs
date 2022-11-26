using AccountingInformationSystem.Shedules.DataModels;
using AccountingInformationSystem.Templates.Abstractions.DataLoaders;

namespace AccountingInformationSystem.Shedules.DataLoaders
{
    public class SheduleDataLoader : BaseDataLoader
    {
        public List<WorkSheduleDataModel> WorkShedule { get; set; }
        public List<SheduleDataModel> Shedules { get; set; }
    }
}
