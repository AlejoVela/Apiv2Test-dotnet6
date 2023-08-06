namespace EmployeesAPI2.Application.Settings
{
    public class MongoSettings
    {
        public string SectionName { get; } = "MongoSettings";
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public MongoCollectionSettigns Collections { get; set; }


    }

    public class MongoCollectionSettigns
    {
        public string Employees { get; set; }
        public string Users { get; set; }
    }
}
