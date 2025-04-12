namespace ScreeningsService.Enums
{
    public enum ScreeningStatusEnum
    {
        Draft,       // Solo visible desde el CMS, editable
        Published,   // Visible al público, pero aún sin entradas habilitadas
        Active,      // Visible y con entradas disponibles
        Cancelled,    // No se debe mostrar
    }
}
