namespace Northwind.UnitTests;

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Core.Persistence;
using Domain.Catalog;
using NUnit.Framework;

[TestFixture]
public class ProductTestFixture
{
  private readonly InMemProductEventStore _eventStore = new();
  private readonly FakeProductDomainMediator _productNotificationMediator = new();
  private EventSourcingRepository<CatalogProduct, ProductId> _productRepo = null!;
  private CatalogProduct _sut = null!;

  [SetUp]
  public async Task Setup()
  {
    _eventStore.ResetForTesting();
    _productNotificationMediator.ResetForTesting();
    _productRepo = new EventSourcingRepository<CatalogProduct, ProductId>(_eventStore, _productNotificationMediator);
    var newProductId = ProductId.NewProductId(101);
    var expectedProductGuid = Guid.NewGuid();
    _sut = new CatalogProduct(newProductId, expectedProductGuid);
    await _productRepo.SaveAsync(_sut);
  }

  [Test]
  [Category("Product:Category")]
  public async Task CategorizeShouldAlterProductCategory()
  {
    var expectedCategoryId = CategoryId.NewCategoryId(202);
    _sut.Categorize(expectedCategoryId);
    Assert.AreEqual(expectedCategoryId, _sut.CategoryID);
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductCategorizedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();
    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsNotNull(actualMessage);
    Assert.AreEqual(expectedCategoryId, actualMessage.CategoryID);
  }

  [Test]
  [Category("Product:Category")]
  public async Task CategorizeThenRecategorizeShouldThrowDifferentEvents()
  {
    var expectedCategoryId = CategoryId.NewCategoryId(202);
    _sut.Categorize(expectedCategoryId);
    Assert.AreEqual(expectedCategoryId, _sut.CategoryID);
    await _productRepo.SaveAsync(_sut);

    var expectedChangedCategoryId = CategoryId.NewCategoryId(203);
    _sut.Categorize(expectedChangedCategoryId);
    Assert.AreEqual(expectedChangedCategoryId, _sut.CategoryID);
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.ToImmutableHashSet();
    var hasCategorizedEvent = actualMessages.OfType<ProductCategorizedEvent>().Any();
    var hasRecategorizedEvent = actualMessages.OfType<ProductRecategorizedEvent>().Any();
    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsTrue(hasCategorizedEvent);
    Assert.IsTrue(hasRecategorizedEvent);

    //just out of caution, verify it will reload with latest categoryid applied in materialized state.
    var reloadedEntity = await _productRepo.GetByIdAsync(_sut.Id);
    Assert.IsNotNull(reloadedEntity);
    Assert.AreEqual(expectedChangedCategoryId, reloadedEntity?.CategoryID);
  }

  [Test]
  [Category("Product:Sku")]
  public void ChangeSkuShouldThrowWhenSameSku()
  {
    var expectedOldGuid = _sut.Sku;
    var expectedNewGuid = expectedOldGuid;

    Assert.Throws<InvalidOperationException>(() => _sut.ChangeSku(expectedNewGuid));
  }

  [Test]
  [Category("Product:Sku")]
  public async Task ChangeSkuShouldUpdateProductSkuWhenNew()
  {
    var expectedOldGuid = _sut.Sku;
    var expectedNewGuid = Guid.NewGuid();

    _sut.ChangeSku(expectedNewGuid);
    var actualUpdatedSku = _sut.Sku;
    Assert.AreEqual(expectedNewGuid, actualUpdatedSku);
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductSkuChangedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();

    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsNotNull(actualMessage);
    Assert.AreEqual(expectedOldGuid, actualMessage.OldSku);
    Assert.AreEqual(expectedNewGuid, actualMessage.NewSku);
  }

  [Test]
  [Category("Product:Description")]
  public async Task DescribeProductShouldChangeDescription()
  {
    var originalDesc = "An initial description goes here.";
    _sut.DescribeProduct(originalDesc);
    await _productRepo.SaveAsync(_sut);

    var actualDesc = _sut.Description;
    Assert.AreEqual(originalDesc, actualDesc);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductDescribedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();
    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsNull(actualMessage.OldDescription);
    Assert.AreEqual(originalDesc, actualMessage.NewDescription);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task ListProductWithExpriationShouldUpdateListingStatusAndExpiration()
  {
    var expectedExpiration = DateTime.UtcNow.AddSeconds(1);
    _sut.List(expectedExpiration);
    await _productRepo.SaveAsync(_sut);

    var productListedEvent = _productNotificationMediator.Messages.OfType<ProductListedEvent>()
      .SingleOrDefault(msg => msg.AggregateId.Equals(_sut.Id));

    Assert.IsTrue(_sut.ListedInCatalog);
    Assert.AreEqual(expectedExpiration, _sut.ListingExpiration);
    Assert.IsNotNull(productListedEvent);
    Assert.AreEqual(expectedExpiration, productListedEvent!.ListingExpiresAt);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task ListProductWithoutExpriationShouldUpdateListingStatus()
  {
    _sut.List();
    await _productRepo.SaveAsync(_sut);

    var productListedEvent = _productNotificationMediator.Messages.OfType<ProductListedEvent>()
      .SingleOrDefault(msg => msg.AggregateId.Equals(_sut.Id));

    Assert.IsTrue(_sut.ListedInCatalog);
    Assert.IsNotNull(productListedEvent);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public void ListProductWithPastExpirationShouldThrow()
  {
    Assert.Throws<InvalidOperationException>
    (() =>
      _sut.List(DateTime.UtcNow.AddDays(-1))
    );
  }

  [Test]
  [Category("Product:Category")]
  public async Task RecategorizeShouldThrowErrorWhenNewCategoryIsNotDifferent()
  {
    const int categoryId = 202;
    var expectedCategoryId = CategoryId.NewCategoryId(categoryId);
    _sut.Categorize(expectedCategoryId);
    Assert.AreEqual(expectedCategoryId, _sut.CategoryID);
    await _productRepo.SaveAsync(_sut);

    var expectedChangedCategoryId = CategoryId.NewCategoryId(categoryId);

    Assert.Throws<InvalidOperationException>(() => { _sut.Categorize(expectedChangedCategoryId); }, "Category ID provided is not different.");

    var actualMessages = _productNotificationMediator.Messages.ToImmutableHashSet();
    var hasCategorizedEvent = actualMessages.OfType<ProductCategorizedEvent>().Any();
    var hasRecategorizedEvent = actualMessages.OfType<ProductRecategorizedEvent>().Any();
    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsTrue(hasCategorizedEvent);
    Assert.IsFalse(hasRecategorizedEvent);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task RehydratedExpiredProductShouldNotBeListed()
  {
    //arrange
    await ListProductWithExpriationShouldUpdateListingStatusAndExpiration();
    await Task.Delay(1000);

    var reloadedProduct = await _productRepo.GetByIdAsync(_sut.Id);
    Assert.IsNotNull(reloadedProduct);
    Assert.IsFalse(reloadedProduct!.ListedInCatalog);
  }

  [Test]
  [Category("Product:Naming")]
  public void RenameShouldThrowWhenNull()
  {
    Assert.Throws<ArgumentNullException>(() => _sut.Rename(string.Empty));
  }

  [Test]
  [Category("Product:Naming")]
  public async Task RenameShouldThrowWhenSameName()
  {
    var productName = "Banana";
    _sut.Rename(productName);
    await _productRepo.SaveAsync(_sut);

    Assert.Throws<InvalidOperationException>(() => _sut.Rename(productName));
  }

  [Test]
  [Category("Product:Naming")]
  public async Task RenameShouldUpdateWhenNew()
  {
    var expectedOldName = _sut.Name;
    var expectedNewName = "Bananas";

    _sut.Rename(expectedNewName);
    var actualNewName = _sut.Name;
    Assert.AreEqual(expectedNewName, actualNewName);
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductRenamedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();

    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsNotNull(actualMessage);
    Assert.AreEqual(expectedOldName, actualMessage.OldName);
    Assert.AreEqual(expectedNewName, actualMessage.NewName);
  }

  [Test]
  public async Task SaveAndLoadAggregateShouldReturnAddedAggregate()
  {
    var newProductId = ProductId.NewProductId(102);
    var expectedProductGuid = Guid.NewGuid();
    var sut = new CatalogProduct(newProductId, expectedProductGuid);
    await _productRepo.SaveAsync(sut);
    var result = await _productRepo.GetByIdAsync(newProductId);
    Assert.IsNotNull(result);
    Assert.AreEqual(expectedProductGuid, result!.Sku);
    var messages = _productNotificationMediator.Messages.OfType<CatalogProductCreatedEvent>().ToImmutableHashSet();
    CollectionAssert.IsNotEmpty(messages);
    Assert.AreEqual(expectedProductGuid, messages.Single(msg => msg.AggregateId.Equals(newProductId)).Sku);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task UnlistProductShouldChangeListedInCatalog()
  {
    _sut.List();
    Assert.IsTrue(_sut.ListedInCatalog);
    _sut.Unlist();
    Assert.IsFalse(_sut.ListedInCatalog);
    await _productRepo.SaveAsync(_sut);

    var unlistedProductEvent = _productNotificationMediator.Messages.OfType<ProductUnlistedEvent>()
      .SingleOrDefault(msg => msg.AggregateId.Equals(_sut.Id));
    Assert.IsNotNull(unlistedProductEvent);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public void UnlistProductWhenUnlistedShouldThrow()
  {
    Assert.Throws<InvalidOperationException>(() => _sut.Unlist());
  }

  [Test]
  [Category("Product:Pricing")]
  public void UpdateListingPrice_DisallowNegativeValuesForAmount()
  {
    Assert.Throws<InvalidOperationException>
      (() => { _sut.IncreaseListPrice(-10, string.Empty); }, "Change amount must not be negative.");

    Assert.Throws<InvalidOperationException>
      (() => { _sut.DecreaseListPrice(-10, string.Empty); }, "Change amount must not be negative.");
  }

  [Test]
  [Category("Product:Pricing")]
  public async Task UpdateListingPrice_ShouldIncreaseAndDecrease()
  {
    var expectedInitialPrice = 20.55m;
    _sut.IncreaseListPrice(expectedInitialPrice, "initial price");
    Assert.AreEqual(expectedInitialPrice, _sut.Price);
    var expectedPriceDelta1 = 7.55m;
    _sut.DecreaseListPrice(expectedPriceDelta1, "adjustment 1");
    Assert.AreEqual(expectedInitialPrice - expectedPriceDelta1, _sut.Price);
    var expectedPriceDelta2 = 10m;
    _sut.IncreaseListPrice(expectedPriceDelta2, "adjustment 2");

    var expectedFinalPrice = expectedInitialPrice - expectedPriceDelta1 + expectedPriceDelta2;
    Assert.AreEqual(expectedFinalPrice, _sut.Price);

    await _productRepo.SaveAsync(_sut);

    var priceChangedEvents = _productNotificationMediator.Messages.OfType<PriceAdjustedEvent>()
      .Where(msg => msg.AggregateId.Equals(_sut.Id))
      .ToImmutableArray();
    Assert.AreEqual(3, priceChangedEvents.Length);
    Assert.AreEqual(expectedInitialPrice, priceChangedEvents[0].Amount);
    Assert.AreEqual(PriceAdjustmentTypeEnum.Increase, priceChangedEvents[0].AdjustmentType);
    Assert.AreEqual(-expectedPriceDelta1, priceChangedEvents[1].Amount);
    Assert.AreEqual(PriceAdjustmentTypeEnum.Decrease, priceChangedEvents[1].AdjustmentType);
    Assert.AreEqual(expectedPriceDelta2, priceChangedEvents[2].Amount);
    Assert.AreEqual(PriceAdjustmentTypeEnum.Increase, priceChangedEvents[2].AdjustmentType);
  }
}