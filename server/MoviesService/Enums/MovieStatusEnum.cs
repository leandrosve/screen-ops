namespace MoviesService.Enums
{
    public enum MovieStatusEnum
    {
        Draft,       // Solo visible desde el CMS, editable
        Published,   // Visible al público, pero aún sin entradas habilitadas
        Hidden,    // No se debe mostrar
    }
}
