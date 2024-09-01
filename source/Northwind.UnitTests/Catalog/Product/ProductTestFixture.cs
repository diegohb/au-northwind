namespace Northwind.UnitTests.Catalog.Product;

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Core.Persistence;
using Domain.Catalog.Category;
using Domain.Catalog.Product;
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
    Assert.That(_sut.CategoryID, Is.EqualTo(expectedCategoryId));
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductCategorizedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();
    Assert.That(actualMessages, Is.Not.Empty);
    Assert.That(actualMessage, Is.Not.Null);
    Assert.That(actualMessage.CategoryID, Is.EqualTo(expectedCategoryId));
  }

  [Test]
  [Category("Product:Category")]
  public async Task CategorizeThenRecategorizeShouldThrowDifferentEvents()
  {
    var expectedCategoryId = CategoryId.NewCategoryId(202);
    _sut.Categorize(expectedCategoryId);
    Assert.That(_sut.CategoryID, Is.EqualTo(expectedCategoryId));
    await _productRepo.SaveAsync(_sut);

    var expectedChangedCategoryId = CategoryId.NewCategoryId(203);
    _sut.Categorize(expectedChangedCategoryId);
    Assert.That(_sut.CategoryID, Is.EqualTo(expectedChangedCategoryId));
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.ToImmutableHashSet();
    var hasCategorizedEvent = actualMessages.OfType<ProductCategorizedEvent>().Any();
    var hasRecategorizedEvent = actualMessages.OfType<ProductRecategorizedEvent>().Any();
    Assert.That(actualMessages, Is.Not.Empty);
    Assert.That(hasCategorizedEvent, Is.True);
    Assert.That(hasRecategorizedEvent, Is.True);

    //just out of caution, verify it will reload with latest categoryid applied in materialized state.
    var reloadedEntity = await _productRepo.GetByIdAsync(_sut.Id);
    Assert.That(reloadedEntity, Is.Not.Null);
    Assert.That(reloadedEntity?.CategoryID, Is.EqualTo(expectedChangedCategoryId));
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
    Assert.That(actualUpdatedSku, Is.EqualTo(expectedNewGuid));
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductSkuChangedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();

    Assert.That(actualMessages, Is.Not.Empty);
    Assert.That(actualMessage, Is.Not.Null);
    Assert.That(actualMessage.OldSku, Is.EqualTo(expectedOldGuid));
    Assert.That(actualMessage.NewSku, Is.EqualTo(expectedNewGuid));
  }

  [Test]
  [Category("Product:Description")]
  public async Task DescribeProductShouldChangeDescription()
  {
    var originalDesc = "An initial description goes here.";
    _sut.DescribeProduct(originalDesc);
    await _productRepo.SaveAsync(_sut);

    var actualDesc = _sut.Description;
    Assert.That(actualDesc, Is.EqualTo(originalDesc));

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductDescribedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();
    Assert.That(actualMessages, Is.Not.Empty);
    Assert.That(actualMessage.OldDescription, Is.Null);
    Assert.That(actualMessage.NewDescription, Is.EqualTo(originalDesc));
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

    Assert.That(_sut.ListedInCatalog, Is.True);
    Assert.That(_sut.ListingExpiration, Is.EqualTo(expectedExpiration));
    Assert.That(productListedEvent, Is.Not.Null);
    Assert.That(productListedEvent!.ListingExpiresAt, Is.EqualTo(expectedExpiration));
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task ListProductWithoutExpriationShouldUpdateListingStatus()
  {
    _sut.List();
    await _productRepo.SaveAsync(_sut);

    var productListedEvent = _productNotificationMediator.Messages.OfType<ProductListedEvent>()
      .SingleOrDefault(msg => msg.AggregateId.Equals(_sut.Id));

    Assert.That(_sut.ListedInCatalog, Is.True);
    Assert.That(productListedEvent, Is.Not.Null);
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
    Assert.That(_sut.CategoryID, Is.EqualTo(expectedCategoryId));
    await _productRepo.SaveAsync(_sut);

    var expectedChangedCategoryId = CategoryId.NewCategoryId(categoryId);

    Assert.Throws<InvalidOperationException>(() => { _sut.Categorize(expectedChangedCategoryId); }, "Category ID provided is not different.");

    var actualMessages = _productNotificationMediator.Messages.ToImmutableHashSet();
    var hasCategorizedEvent = actualMessages.OfType<ProductCategorizedEvent>().Any();
    var hasRecategorizedEvent = actualMessages.OfType<ProductRecategorizedEvent>().Any();
    Assert.That(actualMessages, Is.Not.Empty);
    Assert.That(hasCategorizedEvent, Is.True);
    Assert.That(hasRecategorizedEvent, Is.False);
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task RehydratedExpiredProductShouldNotBeListed()
  {
    //arrange
    await ListProductWithExpriationShouldUpdateListingStatusAndExpiration();
    await Task.Delay(1000);

    var reloadedProduct = await _productRepo.GetByIdAsync(_sut.Id);
    Assert.That(reloadedProduct, Is.Not.Null);
    Assert.That(reloadedProduct!.ListedInCatalog, Is.False);
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
    Assert.That(actualNewName, Is.EqualTo(expectedNewName));
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductRenamedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();

    Assert.That(actualMessages, Is.Not.Empty);
    Assert.That(actualMessage, Is.Not.Null);
    Assert.That(actualMessage.OldName, Is.EqualTo(expectedOldName));
    Assert.That(actualMessage.NewName, Is.EqualTo(expectedNewName));
  }

  [Test]
  public async Task SaveAndLoadAggregateShouldReturnAddedAggregate()
  {
    var newProductId = ProductId.NewProductId(102);
    var expectedProductGuid = Guid.NewGuid();
    var sut = new CatalogProduct(newProductId, expectedProductGuid);
    await _productRepo.SaveAsync(sut);
    var result = await _productRepo.GetByIdAsync(newProductId);
    Assert.That(result, Is.Not.Null);
    Assert.That(result!.Sku, Is.EqualTo(expectedProductGuid));
    var messages = _productNotificationMediator.Messages.OfType<CatalogProductCreatedEvent>().ToImmutableHashSet();
    Assert.That(messages, Is.Not.Empty);
    Assert.That(messages.Single(msg => msg.AggregateId.Equals(newProductId)).Sku, Is.EqualTo(expectedProductGuid));
  }

  [Test]
  [Category("Product:CatalogListing")]
  public async Task UnlistProductShouldChangeListedInCatalog()
  {
    _sut.List();
    Assert.That(_sut.ListedInCatalog, Is.True);
    _sut.Unlist();
    Assert.That(_sut.ListedInCatalog, Is.False);
    await _productRepo.SaveAsync(_sut);

    var unlistedProductEvent = _productNotificationMediator.Messages.OfType<ProductUnlistedEvent>()
      .SingleOrDefault(msg => msg.AggregateId.Equals(_sut.Id));
    Assert.That(unlistedProductEvent, Is.Not.Null);
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
    Assert.That(_sut.Price, Is.EqualTo(expectedInitialPrice));
    var expectedPriceDelta1 = 7.55m;
    _sut.DecreaseListPrice(expectedPriceDelta1, "adjustment 1");
    Assert.That(_sut.Price, Is.EqualTo(expectedInitialPrice - expectedPriceDelta1));
    var expectedPriceDelta2 = 10m;
    _sut.IncreaseListPrice(expectedPriceDelta2, "adjustment 2");

    var expectedFinalPrice = expectedInitialPrice - expectedPriceDelta1 + expectedPriceDelta2;
    Assert.That(_sut.Price, Is.EqualTo(expectedFinalPrice));

    await _productRepo.SaveAsync(_sut);

    var priceChangedEvents = _productNotificationMediator.Messages.OfType<PriceAdjustedEvent>()
      .Where(msg => msg.AggregateId.Equals(_sut.Id))
      .ToImmutableArray();
    Assert.That(priceChangedEvents.Length, Is.EqualTo(3));
    Assert.That(priceChangedEvents[0].Amount, Is.EqualTo(expectedInitialPrice));
    Assert.That(priceChangedEvents[0].AdjustmentType, Is.EqualTo(PriceAdjustmentTypeEnum.Increase));
    Assert.That(priceChangedEvents[1].Amount, Is.EqualTo(-expectedPriceDelta1));
    Assert.That(priceChangedEvents[1].AdjustmentType, Is.EqualTo(PriceAdjustmentTypeEnum.Decrease));
    Assert.That(priceChangedEvents[2].Amount, Is.EqualTo(expectedPriceDelta2));
    Assert.That(priceChangedEvents[2].AdjustmentType, Is.EqualTo(PriceAdjustmentTypeEnum.Increase));
  }
}