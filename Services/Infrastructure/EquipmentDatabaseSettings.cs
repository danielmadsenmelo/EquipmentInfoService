namespace Services.Infrastructure
{
    public class EquipmentDatabaseSettings
    {
        public string ConnectionString { get; } = "mongodb://localhost:27017";

        public string DatabaseName { get; } = "EquipmentInfoService";

        public string CollectionName { get; } = "EquipmentInfo";
    }
}
