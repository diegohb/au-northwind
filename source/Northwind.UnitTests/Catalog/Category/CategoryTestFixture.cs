namespace Northwind.UnitTests.Catalog.Category;

using System;
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

  [Test]
  public async Task RenameCategoryEmptyNameShouldThrowError()
  {
    const string expectedOldName = "Fruits";
    Assert.Throws<NullReferenceException>
      (() => { _sut.ChangeName(string.Empty); });
    await _categoryRepo.SaveAsync(_sut);

    var renamedEvent = _categoryDomainMediator.Messages.OfType<CatalogCategoryRenamedEvent>().SingleOrDefault();
    Assert.IsNull(renamedEvent);
    Assert.AreEqual(expectedOldName, _sut.DisplayName);
  }

  [Test]
  public void RenameCategorySameNameShouldThrowError()
  {
    const string expectedOldName = "Fruits";
    Assert.Throws<InvalidOperationException>
      (() => { _sut.ChangeName(expectedOldName); }, "The new name is the same.");

    Assert.AreEqual(expectedOldName, _sut.DisplayName);
  }

  [Test]
  public async Task RenameCategoryShouldUpdateName()
  {
    const string expectedNewName = "Tools";
    const string expectedOldName = "Fruits";
    _sut.ChangeName(expectedNewName);
    await _categoryRepo.SaveAsync(_sut);

    var renamedEvent = _categoryDomainMediator.Messages.OfType<CatalogCategoryRenamedEvent>().SingleOrDefault();
    Assert.IsNotNull(renamedEvent);
    Assert.AreEqual(expectedOldName, renamedEvent?.OldName);
    Assert.AreEqual(expectedNewName, renamedEvent?.NewName);
  }
}