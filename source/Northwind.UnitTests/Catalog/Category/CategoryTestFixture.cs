namespace Northwind.UnitTests.Catalog.Category;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Persistence;
using Domain.Catalog.Category;
using Domain.Catalog.Product;
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
  public void AddExistingProductToCategoryThrowsError()
  {
    var expectedProductId = ProductId.NewProductId(101);
    _sut.AddProduct(expectedProductId);
    Assert.Throws<InvalidOperationException>(() => { _sut.AddProduct(expectedProductId); }, "Product is already categorized.");
    Assert.AreEqual(_sut.Products.Count, 1);
  }

  [Test]
  public async Task AddProductsToCategoryIncreasesTheCount()
  {
    var productIds = Enumerable.Range(105, 10).Select(ProductId.NewProductId);
    _sut.AddProduct(productIds.ToArray());
    await _categoryRepo.SaveAsync(_sut);

    var addedEvents = _categoryDomainMediator.Messages.OfType<CategoryProductAddedEvent>();
    Assert.IsNotNull(addedEvents);
    foreach (var addedEvent in addedEvents)
    {
      Assert.Contains(addedEvent.NewProductID, productIds.ToArray());
    }

    Assert.AreEqual(_sut.Products.Count, 10);
  }

  [Test]
  public void AddProductsToCategoryWithAlreadyExistingShouldThrowError()
  {
    var productIds = new List<ProductId>(Enumerable.Range(105, 10).Select(ProductId.NewProductId));
    productIds.Add(ProductId.NewProductId(110));

    try
    {
      _sut.AddProduct(productIds.ToArray());
    }
    catch (InvalidOperationException exAtLeastOneExists)
      when (exAtLeastOneExists.Message.Equals("At least one of the products is already categorized."))
    {
      Assert.Pass();
    }

    Assert.AreEqual(_sut.Products.Count, 0);
  }

  [Test]
  public async Task AddProductToCategoryIncreasesTheCount()
  {
    var expectedProductId = ProductId.NewProductId(101);
    _sut.AddProduct(expectedProductId);
    await _categoryRepo.SaveAsync(_sut);

    var addedEvent = _categoryDomainMediator.Messages.OfType<CategoryProductAddedEvent>().SingleOrDefault();
    Assert.IsNotNull(addedEvent);
    Assert.AreEqual(expectedProductId, addedEvent?.NewProductID);
    Assert.AreEqual(_sut.Products.Count, 1);
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