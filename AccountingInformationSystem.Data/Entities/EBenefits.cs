using System.ComponentModel;

namespace AccountingInformationSystem.Data.Entities
{
    /// <summary>
    /// Пільги
    /// </summary>
    public enum EBenefits : byte
    {
        /// <summary>
        /// Немає пільги (0%)
        /// </summary>
        [Description("None")]
        None = 0, // 0%
        /// <summary>
        /// Пільга по дітям (100% ПСП * кількість дітей)
        /// </summary>
        [Description("Benefit 100% for children")]
        Kids = 1,
        /// <summary>
        /// Пільга по дітям, якщо тільки один опікун (150% ПСП * кількість дітей)
        /// </summary>
        [Description("Benefit 150% for children and widow")]
        Widow = 2,
        /// <summary>
        /// Пільга по дітям, якщо дитина з інвалідністю (150% ПСП * кількість дітей)
        /// </summary>
        [Description("Benefit 150% for children with disability")]
        KidsWithDisability = 3,
        /// <summary>
        /// Пільга особі, яка постраждала внаслідок аварії на ЧАЕС (1 та 2 категорії) 150%
        /// </summary>
        [Description("Benefit 150% for injured person in Chernobyl")]
        Chernobyl = 4,
        /// <summary>
        /// Пільга особі, яка є учнем, студентом, аспірантом, ординатором, ад'юнктом 150%
        /// </summary>
        [Description("Benefit 150% for students, aspirants, ordinators, adjunks")]
        Student = 5,
        /// <summary>
        /// Пільга особі з інвалідністю I групи, у тому числі з дитинства 150%
        /// </summary>
        [Description("Benefit 150% for invalid with I group")]
        FirstDisability = 6,
        /// <summary>
        /// Пільга особі з інвалідністю II групи, у тому числі з дитинства 150%
        /// </summary>
        [Description("Benefit 150% for invalid with II group")]
        SecondDisability = 7,
        /// <summary>
        /// Пільга особі, якій призначена довічна стипендія у звязку з переслідуваннями 
        /// за правозахисну діяльність, включаючи журналістів 150%
        /// </summary>
        [Description("Benefit 150% for person who have lifetime scholarship")]
        AssignedLifetimeScholarship = 8,
        /// <summary>
        /// Пільга учаснику бойових дій на території інших країн у період після Другої світової війни 150%
        /// </summary>
        [Description("Benefit 150% for person who was in wars after Second World War")]
        MilitaryAfterWWII = 9,
        /// <summary>
        /// Пільга Героєм України, Радянського Союзу, Соціалістичної Праці або повним 
        /// кавалером ордена Слави чи ордена Трудової Слави 200%
        /// </summary>
        [Description("Benefit 200% for person who have Hero titul")]
        HeroTitul = 10, //200%
        /// <summary>
        /// Пільга особі, яка нагороджена чотирма і більше медалями "За відвагу" 200%
        /// </summary>
        [Description("Benefit 200% for person who have 4 and more medal for Courge")]
        FourMedalForCourage = 11,
        /// <summary>
        /// Пільга учасником бойових дій під час Другої світової війни 200%
        /// </summary>
        [Description("Benefit 200% for person who was in Second World War")]
        MilitaryInWWII = 12,
        /// <summary>
        /// Пільга колишнім в'язням концтаборів, гетто та місць примусового утримання під час Другої світової війни 200%
        /// </summary>
        [Description("Benefit 200% for person who was prisoners of concentration camps")]
        PrisonersOfConcentrationCamps = 13,
        /// <summary>
        /// Пільга особі, яка визнана репресованою чи реабілітованою 200%
        /// </summary>
        [Description("Benefit 200% for person recognized as repressed or rehabilitated")]
        PersonRecognizedAsRepressedOrRehabilitated = 14,
    }
}