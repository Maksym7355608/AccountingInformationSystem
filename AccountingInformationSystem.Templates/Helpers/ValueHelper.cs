namespace AccountingInformationSystem.Templates.Helpers
{
    public static class ValueHelper
    {
        public const decimal TAX = 0.18m;
        public const decimal MTAX = 0.015m;
        public const int NormalDayHours = 8;
        public const decimal MinimalSalary = 3470;
        public const decimal TaxSocialBenefit = 1240.50m;

        /// <summary>
        /// Відділи, int - ід віддділу
        /// </summary>
        public static (int id, string name)[] Departaments
        {
            get
            {
                return new[] {
                     (1, "Departament of finances"),
                     (2, "Departament of electronics"),
                     (3, "Departament of welding"),
                     (4, "Assembly departament")
                 };
            }
        }

        /// <summary>
        /// Юніти, 1) int - ід Юніта, 2) int - ід віддділу
        /// </summary>
        public static (int id, int departamentId, string name)[] Units
        {
            get
            {
                return new[] {
                    (1, 1, "Accountant unit"), (2, 1, "Purchaces unit"),
                    (3, 2, "Engineering unit"),
                    (4, 3, "Welding unit"),
                    (5, 4, "Collection unit"), (6, 4, "Packaging unit"), (7, 4, "Installation")
                };
            }
        }

        /// <summary>
        /// Посади, int - ід Юніта
        /// </summary>
        public static (int unitId, string name)[] Positions
        {
            get
            {
                return new[] {
                     (1, "Accountant"), (1, "Chief accountant"),
                     (2, "Manager"),
                     (3, "Engineer of electronics"), (3, "Chief engineer of electronics"),
                     (4, "Welder"),
                     (5, "Collector of boilers"), 
                     (6, "Packer"),
                     (7, "Equipment installer")
                 };
            }
        }
    }
}
