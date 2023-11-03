using Core.DTOs.Admin;
using Core.DTOs.General;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;

namespace Core.Services.Interfaces
{
    public interface IStoreService
    {
        #region General
        void SaveChanges();
        Task SaveChangesAsync();
        Task<User> GetUserByName(string userName);
        Task<SiteInfo> GetLastSiteInfo();
        Task<List<State>> GetStatesAsync();
        Task<List<County>> GetCountiesofStateAsync(int stId);
        Task<State> GetStateAsync(int stId);
        Task<StateFreight> GetStateFerWithWeight(int stId, float Weight);
        void CreateCellphoneBank(CellphonesBank cellphonesBank);
        Task<bool> ExistCellphoneinCellphoneBank(string cellphone);
        Task<bool> CreateCustomerWithCartInfo(CheckOutVM checkOutVM);
        (bool IsSuccess, string Content) GetNextPayToken(int Amount, string OrderId, string CustomerCellphone, string BackUrl, string Currency);
        Task<List<Cart>> GetLoginUserCartsAsync(string userName);
        #endregion
        #region Product
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        bool ExistProductById(int id);
        Task<List<string>> GetProductBrandsAsync();
        Task<bool> ExistSpecialProduct();
        Task<bool> ExistBestSellProduct();
        Task<(int price,int netPrice, int discount, int percent,string comment)> GetNetPriceandDiscountOfProductAsync(int pId);
        Task<Product> GetProductByEnNameAsync(string enName);
        Task<bool> ExistProductInWareHouseAsync(int Id);
        Task<bool> AddProductViewCount(int productId);
        #endregion Product
        #region Group
        Task<List<ProductGroup>> GetProductGroupsAsync();
        Task<ProductGroup> GetProductGroupByIdAsync(int id);
        Task<ProductGroup> GetProductGroupByEnTitleAsunc(string enTitle);
        Task<List<ProductItem>> GetGroupProductItemsAsync(int gId);
        #endregion
        #region WareHouse
        void CreateWareHouse(WareHouse wareHouse);
        void UpdateWareHouse(WareHouse wareHouse);
        void DeleteWareHouse(WareHouse wareHouse);
        Task<WareHouse> GetWareHouseByIdAsync(int id);
        Task<List<WareHouse>> GetWareHousesAsync();
        bool ExistWareHouse(int id);
        Task<(bool Exist, float Remain)> ExistShopIteminWareHouse(string itemType, int Id);
        /// <summary>
        /// change wareHouse To wareHouseVM
        /// </summary>
        /// <param name="wareHouses"></param>
        /// <returns></returns>
        Task<List<WareHouseVM>> ChangeWHToWHM(List<WareHouse> wareHouses);
        Task<bool> UpdateWareHouseWithCart(Guid cartId);
        #endregion
        #region DiscountCoupen
        void CreateDiscountCoupen(DiscountCoupen discountCoupen);
        void UpdateDiscountCoupen(DiscountCoupen discountCoupen);
        void DeleteDiscountCoupen(DiscountCoupen discountCoupen);
        Task<DiscountCoupen> GetDiscountCoupenByIdAsync(int id);
        Task<List<DiscountCoupen>> GetDiscountCoupensAsync();
        bool ExistCoupen(int Id);
        Task<DiscountCoupen> GetDiscountCoupenByCodeAsync(string code);
        #endregion
        #region ProductItem
        void CreateProductItem(ProductItem ProductItem);
        void UpdateProductItem(ProductItem ProductItem);
        void DeleteProductItem(ProductItem ProductItem);
        Task<ProductItem> GetProductItemByIdAsync(int Id);
        Task<List<ProductItem>> GetProductItemsAsync();
        bool ExistProductItem(int Id);
        Task<List<Product>> GetProductsByTag(string tag);
        Task<bool> ExistProductItemInWareHouseAsync(int Id);        
        Task<(int price,int netPrice,int discount,int dispercent,string comment)> GetNetPriceandDiscountOfProductItemAsync(int Id);
        /// <summary>
        /// محصولات ویژه و جدید
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetSpecialProducts();
        Task<List<Product>> GetBestSellingProducts();
        Task<ProductItem> GetProductItemByEnName(string EnName);
        Task<BestDeal> GetLastBestDealAsync();
        /// <summary>
        /// کالاهای دارای برچسب تخفیف
        /// </summary>
        /// <returns></returns>
        Task<List<ProductItem>> GetProductItemsHasDiscountTagAsync();
        Task<List<ProductItem>> GetProductItemsHasDiscountAsync();
        #endregion
        #region Cart
        Task<(Cart cart, string Result)> AddToCartOp(AddToCartVM addToCartVM);
        Task<Cart> GetCartByIdAsync(Guid id);
        Task<Cart> GetCartByIdAsync(string id);
        Task RemoveItemFromCart(string cartId, int itemId);
        Task<List<CartItem>> GetCartItemsofCart(string cartId);
        Task<string> UpdateCartItemQuantity(int cartitemId, int count);
        Task<bool> UpdateCartWithCheckoutAsync(CheckOutVM checkOutVM);
        void UpdateCart(Cart cart);
        Task<List<Cart>> GetCartsAsync();
        Task<List<string?>> GetCartsDedicatedNumbers();
        #endregion Cart
        #region Order
        Task<bool> CreateOrderWithCheckoutAsync(CheckOutVM checkOutVM);
        Task<(bool,string)> CreateOrderWithCartAsync(Guid CartId);
        Task<bool> ValidateDiscountCoupenAsync(string Coupen);
        #endregion Order
        #region ContactMessage
        Task<List<CustomerContact>> GetCustomerContacts();
        #endregion ContactMessage
    }
}
