namespace ScreeningsService.Enums
{
    public enum ScreeningScheduleStatusEnum
    {
        Draft,       // Solo visible desde el CMS, editable
        Published,   // Visible al público, pero aún sin entradas habilitadas
        Cancelled,    // No se debe mostrar
    }
}
