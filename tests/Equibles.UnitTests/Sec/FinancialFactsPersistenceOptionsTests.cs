using Equibles.Sec.FinancialFacts.HostedService.Configuration;

namespace Equibles.UnitTests.Sec;

public class FinancialFactsPersistenceOptionsTests
{
    // #4580: the FinancialFact upsert batch size is operator-tunable so small
    // hosts can bound peak memory. The default must stay at the historical
    // 1000, and a misconfigured non-positive value must not reach Chunk()
    // (which throws) or BatchPersister.
    [Fact]
    public void EffectiveInsertBatchSize_Default_Is1000()
    {
        new FinancialFactsPersistenceOptions().EffectiveInsertBatchSize.Should().Be(1000);
    }

    [Fact]
    public void EffectiveInsertBatchSize_Configured_UsesConfiguredValue()
    {
        new FinancialFactsPersistenceOptions { InsertBatchSize = 250 }
            .EffectiveInsertBatchSize.Should()
            .Be(250);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void EffectiveInsertBatchSize_NonPositive_FallsBackToDefault(int configured)
    {
        new FinancialFactsPersistenceOptions { InsertBatchSize = configured }
            .EffectiveInsertBatchSize.Should()
            .Be(FinancialFactsPersistenceOptions.DefaultInsertBatchSize);
    }
}
