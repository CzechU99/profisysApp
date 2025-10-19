namespace profisysApp.Config
{
    public class AppSettings
    {
        public string PATH_TO_DOCUMENTS_CSV { get; set; } = "Docs/Documents.csv";
        public string PATH_TO_DOCUMENT_ITEMS_CSV { get; set; } = "Docs/DocumentItems.csv";
        public string CLIENT_URL_ADDRESS { get; set; } = "http://localhost:5173";
    }
}