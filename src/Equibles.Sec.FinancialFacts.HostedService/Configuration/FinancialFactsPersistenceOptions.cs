namespace Equibles.Sec.FinancialFacts.HostedService.Configuration;

/// <summary>
/// Persistence tuning shared by every <c>FinancialFact</c> writer (the Company
/// Facts importer and the dimensional-fact extractor).
/// </summary>
public class FinancialFactsPersistenceOptions
{
    public const int DefaultInsertBatchSize = 1000;

    /// <summary>
    /// Rows per upsert statement. The upsert materialises one DbParameter per
    /// column per row before executing, so peak memory scales with this value;
    /// lower it on small hosts (e.g. <c>FinancialFactsPersistence__InsertBatchSize=250</c>)
    /// to trade round-trips for a smaller footprint (#4580). Non-positive
    /// values fall back to <see cref="DefaultInsertBatchSize"/>.
    /// </summary>
    public int InsertBatchSize { get; set; } = DefaultInsertBatchSize;

    internal int EffectiveInsertBatchSize =>
        InsertBatchSize > 0 ? InsertBatchSize : DefaultInsertBatchSize;
}
