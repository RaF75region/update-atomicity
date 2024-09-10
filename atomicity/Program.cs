using atomicity.Tests;


Test.TestMemoryUsage("Lock-based approach", Test.TestLockApproach);
Test.TestMemoryUsage("ReaderWriterLockSlim approach", Test.TestReaderWriterLockSlimApproach);
Test.TestMemoryUsage("Immutable approach", Test.TestImmutableApproach);
Test.TestMemoryUsage("ConcurrentDictionary approach", Test.TestConcurrentDictionaryApproach);
