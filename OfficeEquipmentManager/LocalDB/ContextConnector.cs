namespace OfficeEquipmentManager.LocalDB
{
    class ContextConnector
    {
        public static ModelContext db { get; set; } = new LocalDB.ModelContext();
    }
}
