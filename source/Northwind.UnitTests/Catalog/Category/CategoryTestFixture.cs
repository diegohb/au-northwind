namespace Northwind.UnitTests.Catalog.Category;

using System.Linq;
using System.Threading.Tasks;
using Core.Persistence;
using Domain.Catalog;
using NUnit.Framework;

[TestFixture]
public class CategoryTestFixture
{
  private readonly InMemCategoryEventStore _eventStore = new();
  private readonly FakeCategoryDomainMediator _categoryDomainMediator = new();
  private EventSourcingRepository<CatalogCategory, CategoryId> _categoryRepo = null!;
  private CatalogCategory _sut = null!;

  [SetUp]
  public async Task Setup()
  {
    _eventStore.ResetForTesting();
    _categoryDomainMediator.ResetForTesting();
    _categoryRepo = new EventSourcingRepository<CatalogCategory, CategoryId>(_eventStore, _categoryDomainMediator);
    var newCategoryId = CategoryId.NewCategoryId(501);
    const string expectedName = "Fruits";
    _sut = new CatalogCategory(newCategoryId, expectedName);
    await _categoryRepo.SaveAsync(_sut);
  }

  [Test]
  public async Task InstantiatedCategoryReloadsFromEvents()
  {
    var newCategoryId = CategoryId.NewCategoryId(510);
    var expectedName = "Tools";
    _sut = new CatalogCategory(newCategoryId, expectedName);
    await _categoryRepo.SaveAsync(_sut);

    var creationEvent = _categoryDomainMediator.Messages.OfType<CatalogCategoryCreatedEvent>()
      .SingleOrDefault(category => category.AggregateId.Equals(newCategoryId));
    Assert.IsNotNull(creationEvent);
    Assert.AreEqual(expectedName, creationEvent?.Name);
  }
}