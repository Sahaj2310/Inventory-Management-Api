namespace InventoryManagementAPI.Configurations
{
    public class LowStockSettings
    {
        public int DefaultThreshold { get; set; } = 10;
        public bool EnableNotifications { get; set; } = true;
        public int NotificationThreshold { get; set; } = 5;
    }
} 