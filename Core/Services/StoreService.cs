using Core.DTOs.Admin;
using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace Core.Services
{
    public class StoreService : IStoreService
    {
        private readonly MyContext _context;
        public StoreService(MyContext context)
        {
            _context = context;
        }
        #region General
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<SiteInfo> GetLastSiteInfo()
        {
            return await _context.SiteInfos.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByName(string userName)
        {
            List<User> users = await _context.Users
                .Include(x => x.UserRoles).Include(x => x.County).Include(x => x.County!.State).ToListAsync();
            User user = users.SingleOrDefault(x => x.UserName == userName)!;
            return user;
        }
        public async Task<List<State>> GetStatesAsync()
        {
            return await _context.States.Include(x => x.Counties).ToListAsync();
        }

        public async Task<List<County>> GetCountiesofStateAsync(int stId)
        {
            return await _context.Counties.Include(x => x.State).Where(w => w.StateId == stId).ToListAsync();
        }
        public async Task<State> GetStateAsync(int stId)
        {
            return await _context.States.Include(x => x.Counties).Include(x => x.StateFreights).SingleOrDefaultAsync(x => x.StateId == stId);
        }
        public async Task<StateFreight> GetStateFerWithWeight(int stId, float Weight)
        {
            State? state = await _context.States.Include(x => x.StateFreights).SingleOrDefaultAsync(x => x.StateId == stId);
            if (state != null)
            {
                List<StateFreight> stateFreights = await _context.StateFreights.Where(w => w.StateId == stId).ToListAsync();
                return stateFreights.OrderBy(x => x.Weight).LastOrDefault(x => x.Weight.GetValueOrDefault() >= Weight);
            }
            return null;
        }
        public void CreateCellphoneBank(CellphonesBank cellphonesBank)
        {
            _context.CellphonesBanks.Add(cellphonesBank);
        }
        public async Task<bool> ExistCellphoneinCellphoneBank(string cellphone)
        {
            return await _context.CellphonesBanks.AnyAsync(x => x.Cellphone == cellphone);
        }
        public (bool IsSuccess, string Content) GetNextPayToken(int Amount, string OrderId, string CustomerCellphone, string BackUrl, string Currency)
        {
            string url = "https://nextpay.org/nx/gateway/token";
            RestClient client = new(url);
            RestRequest request = new(url, Method.Post);
            _ = request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            _ = request.AddParameter("api_key", "47364478-b6ab-424a-b36f-c5b06c814120");
            _ = request.AddParameter("amount", Amount.ToString());
            _ = request.AddParameter("order_id", OrderId);
            _ = request.AddParameter("customer_phone", CustomerCellphone);
            _ = request.AddParameter("currency", Currency);
            _ = request.AddParameter("auto_verify", "yes");
            _ = request.AddParameter("callback_uri", BackUrl);
            RestResponse response = client.Execute(request);
            return (response.IsSuccessful!, response.Content!);
        }
        public async Task<List<Cart>> GetLoginUserCartsAsync(string userName)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(w => w.UserName == userName);
            List<Cart>? carts = null;
            if (user != null)
            {
                carts =await _context.Carts.Where(w => w.UserId == user.Id).ToListAsync();
                carts = carts.Where(w => w.IsActive && w.CheckOut).ToList();
            }
            return carts;
        }
        #endregion
        #region Product
        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.ProductGroup).Include(x => x.ProductItems).SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _context.Products.Include(x => x.ProductGroup).Include(x => x.ProductItems).ToListAsync();
        }
        public bool ExistProductById(int id)
        {
            return _context.Products.Any(x => x.Id == id);
        }
        public async Task<List<string>> GetProductBrandsAsync()
        {
            return await _context.Products.Where(w => w.IsActive && !string.IsNullOrEmpty(w.Brand)).Select(x => x.Brand!.Trim()).Distinct().ToListAsync();
        }
        public async Task<bool> ExistSpecialProduct()
        {
            List<Product> products = await _context.Products.ToListAsync();
            products = products.Where(w => w.IsActive && (w.IsSpecial || w.TagList.Any(a => a == "ویژه" || a == "جدید"))).ToList();
            return products.Any();
        }
        public async Task<bool> ExistBestSellProduct()
        {
            return await _context.Products.AnyAsync(w => w.IsActive && w.AsBestSelling);
        }
        public async Task<(int price, int netPrice, int discount, int percent,string comment)> GetNetPriceandDiscountOfProductAsync(int pId)
        {
            Product? product = await _context.Products.Include(x => x.ProductItems).Include(x => x.ProductGroup).SingleOrDefaultAsync(x => x.Id == pId);
            int Price = 0;
            int NetPrice = 0;
            int DisValue = 0;
            int DisPercent = 0;
            bool applyDis = false;
            string Comment = string.Empty;
            if (product != null)
            {
                if (product.ProductType == "pack")
                {
                    Price = product.Price.GetValueOrDefault();
                }
                else
                {
                    Price = product.ProductItems?.FirstOrDefault(x => x.ShowAsPrice)?.Price.GetValueOrDefault() ?? 0;
                }

            }
            if (product!.ProductItems!.Any())
            {
                ProductItem? productItem = product.ProductItems?.FirstOrDefault(x => x.ShowAsPrice);
                if (productItem != null)
                {
                    if (productItem.ValueDiscount != null)
                    {
                        DisValue = productItem.ValueDiscount.GetValueOrDefault();
                        DisPercent = (int)decimal.Divide(Price, DisValue);
                        applyDis = true;
                        Comment = "تخفیف مقداری جزء محصول";
                    }
                    if (productItem.PercentDiscount.HasValue)
                    {
                        if (productItem.PercentDiscount.Value != 0)
                        {
                            DisPercent = productItem.PercentDiscount.GetValueOrDefault();
                            applyDis = true;
                            Comment = "تخفیف درصدی جزء محصول";
                            DisValue = (int)(decimal.Divide(productItem.PercentDiscount.GetValueOrDefault(), 100) * Price);
                        }

                    }
                }

            }
            if (product!.ValueDiscount.HasValue && !applyDis)
            {
                if (product.ValueDiscount != 0)
                {
                    DisValue = product.ValueDiscount.GetValueOrDefault();
                    DisPercent = (int)decimal.Divide(Price, DisValue);
                    applyDis = true;
                    Comment = "تخفیف مقداری محصول";
                }

            }
            if (product?.PercentDiscount != null && !applyDis)
            {
                if (product.PercentDiscount.HasValue)
                {
                    if (product.PercentDiscount != 0)
                    {
                        DisPercent = product.PercentDiscount.GetValueOrDefault();
                        applyDis = true;
                        Comment = "تخفیف درصدی محصول";
                        DisValue = (int)(decimal.Divide(product.PercentDiscount.GetValueOrDefault(), 100) * Price);
                    }
                }


            }
            if (applyDis == false)
            {
                if (product != null)
                {

                    if (product.ProductGroup != null && !applyDis)
                    {
                        if (product.ProductGroup.DiscountValue.HasValue)
                        {
                            if (product.ProductGroup.DiscountValue != 0)
                            {
                                DisValue = product.ProductGroup.DiscountValue.GetValueOrDefault();
                                DisPercent = (int)decimal.Divide(Price, DisValue);
                                applyDis = true;
                                Comment = "تخفیف مقداری گروه محصول";
                            }
                        }
                        if (product.ProductGroup.DiscountPercent.HasValue)
                        {
                            if (product.ProductGroup.DiscountPercent != 0 && !applyDis)
                            {
                                DisValue = (int)(decimal.Divide(product.ProductGroup.DiscountPercent.GetValueOrDefault(), 100) * Price);
                                DisPercent = product.ProductGroup.DiscountPercent.GetValueOrDefault();
                                applyDis = true;
                                Comment = "تخفیف درصدی گروه محصول";
                            }
                        }

                    }
                }
            }

            NetPrice = Price-DisValue ;
            return (Price,NetPrice, DisValue, DisPercent,Comment);
        }

        public async Task<Product> GetProductByEnNameAsync(string enName)
        {
            Product? product = await _context.Products.Include(x => x.ProductGroup).Include(x => x.ProductItems)
                .SingleOrDefaultAsync(x => x.EnName == enName);
            return product;
        }

        public async Task<bool> ExistProductInWareHouseAsync(int Id)
        {
            bool exist = true;
            float? exCount = await _context.WareHouses.Where(w => w.ProductId == Id).SumAsync(x => x.Input.GetValueOrDefault() - x.Export.GetValueOrDefault());
            if (exCount.GetValueOrDefault() <= 0)
            {
                exist = false;
            }
            return exist;
        }
        public async Task<bool> AddProductViewCount(int productId)
        {
            bool Res = false;
            Product? product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.ViewCount = product.ViewCount.GetValueOrDefault() + 1;
                _context.Products.Update(product);
                Res = true;
            }
            return Res;
        }
        #endregion Product
        #region group
        public async Task<List<ProductGroup>> GetProductGroupsAsync()
        {
            return await _context.ProductGroups.Include(x => x.Products).ToListAsync();
        }
        public async Task<ProductGroup> GetProductGroupByIdAsync(int id)
        {
            return await _context.ProductGroups.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ProductGroup> GetProductGroupByEnTitleAsunc(string enTitle)
        {
            List<ProductGroup> productGroups = await _context.ProductGroups.Include(x => x.Products).ToListAsync();
            return productGroups.SingleOrDefault(x => x.EnTitle == enTitle);
        }
        public async Task<List<ProductItem>> GetGroupProductItemsAsync(int gId)
        {
            ProductGroup? productGroup = await _context.ProductGroups.SingleOrDefaultAsync(x => x.Id == gId);
            if (productGroup == null)
            {
                return null;
            }
            List<ProductItem> productItems = await _context.ProductItems.Include(x => x.Product).Include(x => x.Product!.ProductGroup)
                 .Where(w => w.Product!.ProductGroupId == gId).ToListAsync();
            return productItems;
        }
        #endregion
        #region WareHouse
        public void CreateWareHouse(WareHouse wareHouse)
        {
            _context.WareHouses.Add(wareHouse);
        }

        public void UpdateWareHouse(WareHouse wareHouse)
        {
            _context.WareHouses.Update(wareHouse);
        }

        public void DeleteWareHouse(WareHouse wareHouse)
        {
            _context.WareHouses.Remove(wareHouse);
        }

        public async Task<WareHouse> GetWareHouseByIdAsync(int id)
        {
            return await _context.WareHouses.Include(x => x.Product).Include(x => x.ProductItem).Include(x => x.ProductItem!.Product).Include(x => x.CartItem).Include(r => r.ProductItem!.Product!.ProductGroup)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<WareHouse>> GetWareHousesAsync()
        {
            return await _context.WareHouses.Include(x => x.Product).Include(x => x.ProductItem).Include(x => x.ProductItem!.Product).Include(x => x.CartItem).Include(x => x.CartItem!.Cart).Include(r => r.ProductItem!.Product!.ProductGroup)
                 .ToListAsync();
        }

        public bool ExistWareHouse(int id)
        {
            return _context.WareHouses.Any(x => x.Id == id);
        }
        /// <summary>
        /// change wareHouse To wareHouseVM
        /// </summary>
        /// <param name="wareHouses"></param>
        /// <returns></returns>
        public async Task<List<WareHouseVM>> ChangeWHToWHM(List<WareHouse> wareHouses)
        {
            List<WareHouseVM> wareHouseVMs = new();
            List<Product>? bulkproducts = await _context.Products.Where(w => w.ProductType == "bulk").ToListAsync();
            List<ProductItem>? productItems = await _context.ProductItems.Include(x => x.Product).Where(w => w.Product!.ProductType == "pack").ToListAsync();
            List<WareHouse>? ProductWarehouses = new();
            List<WareHouse>? ProductItemWarehouses = new();
            foreach (var item in bulkproducts)
            {
                float remb = 0;
                string unit = string.Empty;
                ProductWarehouses = wareHouses.Where(w => w.Product?.Id == item.Id || w.ProductItem?.ProductId == item.Id).ToList();
                foreach (var Pwh in ProductWarehouses)
                {
                    WareHouseVM wareHousevm = new()
                    {
                        RegDate = Pwh.RegDate,
                        Type = "p",
                        UnitofM = item.UnitofMeasure
                    };
                    if (Pwh.ProductId.HasValue)
                    {
                        wareHousevm.Product = Pwh.Product;
                    }
                    if (Pwh.ProductItemId.HasValue)
                    {
                        wareHousevm.ProductItem = Pwh.ProductItem;
                    }
                    if (Pwh.Input.HasValue)
                    {
                        wareHousevm.Input = Pwh.Input;
                    }
                    if (Pwh.Export.HasValue)
                    {
                        wareHousevm.Export = Pwh.Export;
                    }
                    if (Pwh.ProductItem != null)
                    {
                        if (Pwh.ProductItem.Weight.HasValue)
                        {
                            remb += (Pwh.Input.GetValueOrDefault() * Pwh.ProductItem.Weight.GetValueOrDefault()) - (Pwh.Export.GetValueOrDefault() * Pwh.ProductItem.Weight.GetValueOrDefault());
                        }
                        else
                        {
                            remb += Pwh.Input.GetValueOrDefault() - Pwh.Export.GetValueOrDefault();
                        }

                    }
                    else
                    {
                        remb += Pwh.Input.GetValueOrDefault() - Pwh.Export.GetValueOrDefault();
                    }
                    wareHousevm.Remain = remb;
                    wareHouseVMs.Add(wareHousevm);
                }
            }


            foreach (var Piwh in productItems)
            {
                float rem = 0;
                ProductItemWarehouses = wareHouses.Where(w => w.ProductItemId == Piwh.Id).ToList();
                foreach (var piwh in ProductItemWarehouses)
                {
                    WareHouseVM wareHousevm = new()
                    {
                        Type = "pitem",
                        RegDate = Piwh.RegDate,
                        UnitofM = piwh.ProductItem?.UnitofMeasure
                    };
                    if (piwh.ProductId.HasValue)
                    {
                        wareHousevm.Product = piwh.Product;
                    }
                    if (piwh.ProductItemId.HasValue)
                    {
                        wareHousevm.Product = piwh.ProductItem?.Product;
                        wareHousevm.ProductItem = piwh.ProductItem;
                    }
                    if (piwh.Input.HasValue)
                    {
                        wareHousevm.Input = piwh.Input;
                    }
                    if (piwh.Export.HasValue)
                    {
                        wareHousevm.Export = piwh.Export;
                    }
                    rem += piwh.Input.GetValueOrDefault() - piwh.Export.GetValueOrDefault();
                    wareHousevm.Remain = rem;
                    wareHouseVMs.Add(wareHousevm);
                }
            }
            return wareHouseVMs.ToList();
        }
        public async Task<(bool Exist, float Remain)> ExistShopIteminWareHouse(string itemType, int Id)
        {
            bool Ex = false; float Rem = 0;
            Product? product = null; ProductItem? productItem = null;
            if (Id == 3)
            {
                Rem = 0;
            }
            if (itemType == "pr")
            {
                product = await _context.Products.Include(x => x.ProductItems).Include(x => x.ProductGroup).Include(x => x.WareHouses).SingleOrDefaultAsync(x => x.Id == Id);
            }
            if (itemType == "pritem")
            {
                productItem = await _context.ProductItems.Include(x => x.Product).Include(x => x.WareHouses).SingleOrDefaultAsync(x => x.Id == Id);
            }
            if (product != null)
            {
                if (product.ProductType == "bulk")
                {
                    float Rem2 = 0;

                    List<WareHouse> wareHouses = await _context.WareHouses.Include(x => x.Product).Include(x => x.ProductItem).Include(x => x.ProductItem!.Product)
                        .Where(w => w.ProductId == Id).ToListAsync();
                    Rem = wareHouses.Sum(x => x.Input.GetValueOrDefault() - x.Export.GetValueOrDefault());
                    foreach (var pri in product.ProductItems!.ToList())
                    {
                        List<WareHouse> wareHouses1 = await _context.WareHouses.Include(x => x.Product).Include(x => x.ProductItem).Include(x => x.ProductItem!.Product)
                        .Where(w => w.ProductItem!.Id == pri.Id).ToListAsync();

                        if (pri.ValueBaseOnUoM.HasValue)
                        {
                            if (pri.ValueBaseOnUoM != 0)
                            {
                                Rem2 += wareHouses1.Sum(x => (x.Input.GetValueOrDefault() * x.ProductItem!.ValueBaseOnUoM.GetValueOrDefault()) - (x.Export.GetValueOrDefault() * x.ProductItem!.ValueBaseOnUoM.GetValueOrDefault()));
                            }
                            else
                            {
                                Rem2 += wareHouses1.Sum(x => x.Input.GetValueOrDefault() - x.Export.GetValueOrDefault());
                            }

                        }
                        else
                        {
                            Rem2 += wareHouses1.Sum(x => x.Input.GetValueOrDefault() - x.Export.GetValueOrDefault());
                        }

                    }

                    Rem += Rem2;
                }
            }
            if (productItem != null)
            {
                List<WareHouse> wareHouses = await _context.WareHouses.Include(x => x.Product).Include(x => x.ProductItem).Include(x => x.ProductItem!.Product)
                        .Where(w => w.ProductItem!.ProductId == Id).ToListAsync();
                Rem += wareHouses.Sum(x => x.Input.GetValueOrDefault() - x.Export.GetValueOrDefault());
            }
            if (Rem > 0)
            {
                Ex = true;
            }
            return (Ex, Rem);
        }
        public async Task<bool> UpdateWareHouseWithCart(Guid cartId)
        {
            Cart? cart = await _context.Carts.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == cartId);
            if (cart != null)
            {
                if (cart.CartItems.Any())
                {
                    foreach (var item in cart.CartItems.ToList())
                    {
                        ProductItem? productItem = await _context.ProductItems.Include(x => x.Product).SingleOrDefaultAsync(x => x.Id == item.ProductItemId);
                        if (productItem != null)
                        {
                            if (productItem.Product?.ProductType == "bulk")
                            {
                                WareHouse wareHouse_bulk = new()
                                {
                                    ProductId = productItem.ProductId,
                                    Export = item.Quantity,
                                    RegDate = DateTime.Now,
                                };
                                _context.WareHouses.Add(wareHouse_bulk);
                            }
                            else
                            {
                                WareHouse wareHouse = new()
                                {
                                    ProductItemId = item.ProductItemId,
                                    Export = item.Quantity,
                                    RegDate = DateTime.Now,

                                };
                                _context.WareHouses.Add(wareHouse);
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }
        public async Task<BestDeal> GetLastBestDealAsync()
        {
            List<BestDeal> bestDeals = await _context.BestDeals.Where(w => w.IsActive).OrderByDescending(x => x.RegDate).ToListAsync();

            BestDeal? bestDeal = bestDeals.FirstOrDefault();
            return bestDeal;
        }
        public async Task<List<ProductItem>> GetProductItemsHasDiscountTagAsync()
        {
            List<ProductItem> productItems = await _context.ProductItems.Include(x => x.Product).Include(x => x.Product!.ProductGroup).ToListAsync();
            productItems = productItems.Where(w => w.TagList.Any(z => z.Trim() == "تخفیف")).ToList();
            return productItems;
        }
        #endregion
        #region DiscountCoupen
        public void CreateDiscountCoupen(DiscountCoupen discountCoupen)
        {
            _context.DiscountCoupens.Add(discountCoupen);
        }

        public void UpdateDiscountCoupen(DiscountCoupen discountCoupen)
        {
            _context.DiscountCoupens.Update(discountCoupen);
        }

        public void DeleteDiscountCoupen(DiscountCoupen discountCoupen)
        {
            _context.DiscountCoupens.Remove(discountCoupen);
        }

        public async Task<DiscountCoupen> GetDiscountCoupenByIdAsync(int id)
        {
            return await _context.DiscountCoupens.FindAsync(id);
        }

        public async Task<List<DiscountCoupen>> GetDiscountCoupensAsync()
        {
            return await _context.DiscountCoupens.ToListAsync();
        }

        public bool ExistCoupen(int Id)
        {
            return _context.DiscountCoupens.Any(x => x.Id == Id);
        }
        public async Task<DiscountCoupen> GetDiscountCoupenByCodeAsync(string code)
        {
            return await _context.DiscountCoupens.SingleOrDefaultAsync(x => x.Code == code);
        }
        #endregion
        #region ProductItem
        public void CreateProductItem(ProductItem ProductItem)
        {
            _context.ProductItems.Add(ProductItem);
        }

        public void UpdateProductItem(ProductItem ProductItem)
        {
            _context.ProductItems.Update(ProductItem);
        }

        public void DeleteProductItem(ProductItem ProductItem)
        {
            _context.ProductItems.Remove(ProductItem);
        }

        public async Task<ProductItem> GetProductItemByIdAsync(int Id)
        {
            return await _context.ProductItems.Include(x => x.Product).Include(x => x.Product!.ProductGroup).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<ProductItem>> GetProductItemsAsync()
        {
            return await _context.ProductItems.Include(x => x.Product).Include(x => x.Product!.ProductGroup).ToListAsync();
        }

        public bool ExistProductItem(int Id)
        {
            return _context.ProductItems.Any(x => x.Id == Id);
        }

        public async Task<List<Product>> GetProductsByTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return null;
            }
            List<Product> products = await _context.Products.Where(w => w.IsActive).ToListAsync();
            products = products.Where(w => w.TagList.Any(z => z == tag)).ToList();
            return products.ToList();
        }

        public async Task<bool> ExistProductItemInWareHouseAsync(int Id)
        {
            bool exist = true;
            float? exCount = await _context.WareHouses.Where(w => w.ProductItemId == Id).SumAsync(x => x.Input - x.Export);
            if (exCount.GetValueOrDefault() <= 0)
            {
                exist = false;
            }
            return exist;
        }
        public async Task<(int price,int netPrice, int discount, int dispercent,string comment)> GetNetPriceandDiscountOfProductItemAsync(int Id)
        {
            ProductItem? productItem = await _context.ProductItems.Include(x => x.Product).Include(x => x.Product!.ProductGroup).SingleOrDefaultAsync(x => x.Id == Id);
            int Price = 0;
            int NetPrice = 0;
            int DisValue = 0;
            int DisPercent = 0;
            string Comment = string.Empty;
            bool applyDis = false;
            if (productItem != null)
            {
                Price = productItem.Price.GetValueOrDefault();
                if (productItem!.ValueDiscount.HasValue)
                {
                    if (productItem.ValueDiscount != 0)
                    {
                        DisValue = productItem.ValueDiscount.GetValueOrDefault();
                        DisPercent = (int)decimal.Divide(Price, DisValue);
                        applyDis = true;
                        Comment = "تخفیف مبلغی جزء محصول";
                    }
                }
                if (productItem?.PercentDiscount != null && !applyDis)
                {
                    if (productItem.PercentDiscount.HasValue)
                    {
                        if (productItem.PercentDiscount != 0)
                        {
                            DisPercent = productItem.PercentDiscount.GetValueOrDefault();
                            applyDis = true;
                            DisValue = (int)(decimal.Divide(productItem.PercentDiscount.GetValueOrDefault(), 100) * Price);
                            Comment = "تخفیف درصدی جزء محصول";
                        }
                    }

                }
            }

            if (applyDis == false)
            {
                if (productItem?.Product != null)
                {
                    if (productItem.Product.ValueDiscount.HasValue)
                    {
                        if (productItem.Product.ValueDiscount != 0)
                        {
                            DisValue = productItem!.Product.ValueDiscount.GetValueOrDefault();
                            DisPercent = (int)decimal.Divide(Price, DisValue);
                            applyDis = true;
                            Comment = "تخفیف مبلغی محصول";
                        }
                    }
                    if (productItem.Product.PercentDiscount != null && !applyDis)
                    {
                        if (productItem.Product.PercentDiscount.HasValue)
                        {
                            if (productItem.Product.PercentDiscount != 0)
                            {
                                DisValue = (int)(decimal.Divide(productItem!.Product.PercentDiscount.GetValueOrDefault(), 100) * Price);
                                DisPercent = (int)productItem.Product.PercentDiscount;
                                applyDis = true;
                                Comment = "تخفیف درصدی محصول";
                            }
                        }

                    }
                    if (productItem.Product.ProductGroup != null && !applyDis)
                    {
                        if (productItem.Product.ProductGroup.DiscountValue.HasValue)
                        {
                            if (productItem.Product.ProductGroup.DiscountValue != 0)
                            {
                                DisValue = productItem!.Product.ProductGroup.DiscountValue.GetValueOrDefault();
                                DisPercent = (int)decimal.Divide(Price, DisValue);
                                applyDis = true;
                                Comment = "تخفیف مبلغی گروه محصول";
                            }
                        }
                        if (productItem.Product.ProductGroup.DiscountPercent.HasValue)
                        {
                            if (productItem.Product.ProductGroup.DiscountPercent != 0 && !applyDis)
                            {
                                DisValue = (int)(decimal.Divide(productItem.Product.ProductGroup.DiscountPercent.GetValueOrDefault(), 100) * Price);
                                DisPercent = productItem.Product.ProductGroup.DiscountPercent.GetValueOrDefault();
                                applyDis=true;
                                Comment = "تخفیف درصدی گروه محصول ";
                                    
                            }
                        }

                    }
                }
            }

            NetPrice = Price-DisValue;
            return (Price, NetPrice, DisValue, DisPercent,Comment);
        }
        public async Task<List<Product>> GetSpecialProducts()
        {
            List<Product> products = await _context.Products.Include(x => x.ProductItems).ToListAsync();
            products = products.Where(w => w.IsSpecial || w.TagList.Any(a => a == "ویژه" || a == "جدید")).ToList();
            return products;
        }
        public async Task<List<Product>> GetBestSellingProducts()
        {
            return await _context.Products.Include(x => x.ProductItems).Where(w => w.IsActive && w.AsBestSelling).ToListAsync();
        }
        public async Task<List<ProductItem>> GetProductItemsHasDiscountAsync()
        {
            List<ProductItem> productItems = await _context.ProductItems.Include(x => x.Product).Include(x => x.Product!.ProductGroup).Where(w => w.IsActive).ToListAsync();
            productItems = productItems.Where(w => w.IsActive &&
            (w.ValueDiscount.GetValueOrDefault() != 0 || w.PercentDiscount.GetValueOrDefault() != 0 || w.Product!.ValueDiscount.GetValueOrDefault() != 0 || w.Product!.PercentDiscount.GetValueOrDefault() != 0 || w.Product!.ProductGroup!.DiscountValue.GetValueOrDefault() != 0 || w.Product!.ProductGroup!.DiscountPercent.GetValueOrDefault() != 0)).ToList();
            return productItems.ToList();
        }
        #endregion
        #region Cart
        public async Task<(Cart cart, string Result)> AddToCartOp(AddToCartVM addToCartVM)
        {
            Cart? cart = null;
            string state = "no";
            if (addToCartVM.CartId.HasValue)
            {
                cart = await _context.Carts.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == addToCartVM.CartId.Value);
                if (cart != null)
                {
                    if (addToCartVM.ProductItemId != null)
                    {
                        (int Price, int netPrice, int Discount, int DisPercent,string comment) = await GetNetPriceandDiscountOfProductItemAsync(addToCartVM.ProductItemId.Value);
                        if (cart.CartItems.Any(x => x.ProductItemId == addToCartVM.ProductItemId))
                        {
                            CartItem? cartItem = cart.CartItems.SingleOrDefault(x => x.ProductItemId == addToCartVM.ProductItemId);
                            if (cartItem != null)
                            {
                                cartItem.Quantity += addToCartVM.Quantity;
                                cartItem.Price = Price;
                                cartItem.Discount = Discount;
                                cartItem.NetPrice = netPrice;
                                cartItem.Comment = addToCartVM.Comment;                                
                                _context.Carts.Update(cart);
                                await _context.SaveChangesAsync();
                                state = "yes";
                            }
                            else
                            {
                                cart.CartItems.Add(new CartItem()
                                {
                                    Quantity = addToCartVM.Quantity,
                                    Price = Price,
                                    Discount = Discount,
                                    NetPrice = netPrice,
                                    Comment = addToCartVM.Comment,
                                });                                
                                _context.Carts.Update(cart);
                                await _context.SaveChangesAsync();
                                state = "yes";
                            }
                            
                            

                        }
                        else
                        {
                            cart.CartItems.Add(new()
                            {
                                ProductItemId = addToCartVM.ProductItemId,
                                Quantity = addToCartVM.Quantity,
                                Comment = addToCartVM.Comment,
                                Price = Price,
                                Discount = Discount,
                                NetPrice = netPrice
                            });
                            _context.Carts.Update(cart);
                            await _context.SaveChangesAsync();
                            state = "yes";
                        }
                    }
                    if (addToCartVM.ProductId != null)
                    {
                        (int Price, int netPrice, int Discount, int DisPercent,string comment) = await GetNetPriceandDiscountOfProductItemAsync(addToCartVM.ProductId.Value);
                        if (cart.CartItems.Any(x => x.ProductId == addToCartVM.ProductId))
                        {
                            CartItem? cartItem = cart.CartItems.SingleOrDefault(x => x.ProductId == addToCartVM.ProductId);
                            if (cartItem != null)
                            {
                                cartItem.Quantity += addToCartVM.Quantity;
                                cartItem.Price = Price;
                                cartItem.Discount = Discount;
                                cartItem.NetPrice = netPrice;
                                cartItem.Comment = addToCartVM.Comment;
                                _context.Carts.Update(cart);
                                await _context.SaveChangesAsync();
                                state = "yes";
                            }
                            else
                            {
                                cart.CartItems.Add(new()
                                {
                                    ProductId = addToCartVM.ProductId,
                                    Quantity = addToCartVM.Quantity,
                                    Comment = addToCartVM.Comment,
                                    Price = Price,
                                    Discount = Discount,
                                    NetPrice = netPrice
                                });
                                _context.Carts.Update(cart);
                                await _context.SaveChangesAsync();
                                state = "yes";
                            }
                            

                        }
                    }

                }
                else
                {
                    (int Price,int NetPrice, int Discount, int DisPercent,string comment) Res = (0,0,0,0,string.Empty);
                    SiteInfo? siteInfo = await _context.SiteInfos.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                    Cart newCart = new()
                    {
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Currency = siteInfo?.SiteCurrency ?? "ریال",

                    };
                    if (addToCartVM.ProductId != null)
                    {
                        Res = await GetNetPriceandDiscountOfProductItemAsync(addToCartVM.ProductId.Value);
                        CartItem cartItem = new()
                        {
                            ProductId = addToCartVM.ProductId!.Value,
                            Quantity = addToCartVM.Quantity,
                            Comment = addToCartVM.Comment,
                            Price = Res.Price, 
                            Discount = Res.Discount,
                            NetPrice = Res.NetPrice,
                        };
                        newCart.CartItems.Add(cartItem);
                        _context.Carts.Add(newCart);
                        await _context.SaveChangesAsync();
                        cart = newCart;
                        state = "yes";
                    }
                    else if (addToCartVM.ProductItemId != null)
                    {
                        Res = await GetNetPriceandDiscountOfProductItemAsync(addToCartVM.ProductItemId.Value);
                        CartItem cartItem = new()
                        {
                            ProductItemId = addToCartVM.ProductItemId.Value,
                            Quantity = addToCartVM.Quantity,
                            Comment = addToCartVM.Comment,
                            Price = Res.Price,
                            Discount = Res.Discount,
                            NetPrice = Res.NetPrice,
                        };
                        newCart.CartItems.Add(cartItem);
                        _context.Carts.Add(newCart);
                        await _context.SaveChangesAsync();
                        state = "yes";
                    }                   
                }
            }
            else
            {
                (int Price,int NetPrice, int Discount, int DisPercent,string Cpmment) Res = (0, 0, 0, 0,string.Empty);
                SiteInfo? siteInfo = await _context.SiteInfos.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                Cart newCart = new()
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Currency = siteInfo?.SiteCurrency ?? "ریال",
                    DedicatedCode = Prodocers.Generators.GenerateUniqueString(0, 0, 8, 0)
                };
                _context.Carts.Add(newCart);
                if (addToCartVM.ProductId != null)
                {
                    Res = await GetNetPriceandDiscountOfProductItemAsync(addToCartVM.ProductId.Value);
                    newCart.CartItems.Add(new CartItem()
                    {
                        ProductId = addToCartVM.ProductId!.Value,
                        Quantity = addToCartVM.Quantity,
                        Comment = addToCartVM.Comment,
                        Price = Res.Price,
                        Discount = Res.Discount,
                        NetPrice = Res.NetPrice,
                    });
                    
                    _ = _context.Carts.Add(newCart);
                    _context.SaveChanges();
                    cart = newCart;
                    state = "yes";
                }
                else if (addToCartVM.ProductItemId != null)
                {
                    Res = await GetNetPriceandDiscountOfProductItemAsync(addToCartVM.ProductItemId.Value);
                    newCart.CartItems.Add(new CartItem()
                    {
                        ProductItemId = addToCartVM.ProductItemId.Value,
                        Quantity = addToCartVM.Quantity,
                        Comment = addToCartVM.Comment,
                        Price = Res.Price,
                        Discount = Res.Discount,
                        NetPrice = Res.NetPrice,
                    });
                    
                    _ = _context.Carts.Add(newCart);
                    _context.SaveChanges();
                    cart = newCart;
                    state = "yes";
                }
            }
            return (cart!, state);
        }

        public async Task<Cart> GetCartByIdAsync(Guid id)
        {
            return await _context.Carts.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<Cart> GetCartByIdAsync(string id)
        {
            Guid gid = Guid.Parse(id);
            return await _context.Carts.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == gid && x.IsActive);
        }

        public async Task RemoveItemFromCart(string cartId, int itemId)
        {
            CartItem? cartItem = await _context.CartItems.Include(x => x.Cart).SingleOrDefaultAsync(x => x.CartId.ToString() == cartId && x.Id == itemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }

        }
        public async Task<List<CartItem>> GetCartItemsofCart(string cartId)
        {
            Guid gid = Guid.Parse(cartId);
            return await _context.CartItems.Include(x => x.ProductItem).Include(x => x.ProductItem!.Product).Where(w => w.CartId == gid).ToListAsync();
        }
        public async Task<string> UpdateCartItemQuantity(int cartitemId, int count)
        {
            string Result = "no";
            CartItem cartItem = await _context.CartItems.SingleOrDefaultAsync(x => x.Id == cartitemId) ?? new();
            if (cartItem != null)
            {
                if (cartItem.Quantity != count)
                {
                    cartItem.Quantity = count;
                    _context.CartItems.Update(cartItem);
                    Result = "yes";
                }
            }
            return Result;
        }
        public async Task<ProductItem> GetProductItemByEnName(string EnName)
        {
            return await _context.ProductItems.Include(x => x.Product).SingleOrDefaultAsync(x => x.EnName!.ToLower() == EnName.ToLower());
        }
        public async Task<bool> UpdateCartWithCheckoutAsync(CheckOutVM checkOutVM)
        {
            bool Response = false;
            if (!string.IsNullOrEmpty(checkOutVM.CartId))
            {
                List<Cart> carts = await _context.Carts.Include(x => x.CartItems).Include(x => x.CartItems).ToListAsync();
                Cart cart = carts.SingleOrDefault(x => x.Id.ToString() == checkOutVM.CartId)!;
                State? state = await _context.States.Include(x => x.StateFreights).SingleOrDefaultAsync(x => x.StateId == checkOutVM.StateId!.GetValueOrDefault());
                County? county = await _context.Counties.FindAsync(checkOutVM.CountyId.GetValueOrDefault());
                int cartItemsSum = 0;
                if (cart != null)
                {
                    User? user = await _context.Users.SingleOrDefaultAsync(w => w.Cellphone == checkOutVM.BuyerCellphone);
                    cart.UserId = user?.Id;

                    cartItemsSum = cart.CartItems.Sum(x => x.Quantity * x.NetPrice);
                    cart.BuyerName = checkOutVM.BuyerName;
                    cart.BuyerFamily = checkOutVM.BuyerFamily;
                    cart.BuyerCellphone = checkOutVM.BuyerCellphone;
                    cart.RecipientName = checkOutVM.RecepientName;
                    cart.RecipientFamily = checkOutVM.RecepientFamily;
                    cart.RecipientPhone = checkOutVM.RecepientCellphone;
                    cart.RecipientIsBuyer = checkOutVM.BuyerIsRecepient;
                    cart.StateName = state?.StateName;
                    cart.CountyName = county?.CountyName;
                    cart.Address = checkOutVM.Address;
                    cart.PostalCode = checkOutVM.PostalCode;
                    cart.Comment = checkOutVM.Comment;
                    cart.RecipientIsBuyer = checkOutVM.BuyerIsRecepient;
                    cart.PaymentofFareDuringDelivery = checkOutVM.PaymentofFareDuringDeliverySnap;
                    cart.ShippingBySnapp = checkOutVM.PaymentofFareDuringDeliverySnap;
                    cart.ShippingByTipax = checkOutVM.PaymentofFareDuringDeliveryTipax;
                    cart.ShippingByPishtazPost = checkOutVM.ShippingByPishtazPost;
                    float? Weight = cart.CartItems.Sum(x => x.Quantity * x.ProductItem?.Weight);
                    StateFreight? Fr = state?.StateFreights.OrderByDescending(x => x.Weight).FirstOrDefault(x => x.Weight <= Weight);

                    int CartTotal = cartItemsSum;
                    if (checkOutVM.ShippingByPishtazPost)
                    {
                        if (Fr != null)
                        {
                            cart.Freight = Fr?.Freight.GetValueOrDefault();
                            CartTotal += Fr!.Freight.GetValueOrDefault();
                        }
                    }
                    else
                    {
                        cart.PaymentofFareDuringDelivery = true;
                    }
                    cart.CartTotal = CartTotal;
                    Response = true;
                    _context.Carts.Update(cart);
                }
            }
            return Response;
        }
        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
        }
        public async Task<List<Cart>> GetCartsAsync()
        {
            return await _context.Carts.Include(x => x.CartItems).ToListAsync();
        }
        public async Task<List<string?>> GetCartsDedicatedNumbers()
        {
            List<Cart> carts = await _context.Carts.ToListAsync();
            carts = carts.Where(w => w.CheckOut && !string.IsNullOrEmpty(w.DedicatedCode)).ToList();
            return carts.Select(x => x.DedicatedCode).ToList();
        }
        #endregion Cart
        #region Order
        public async Task<bool> CreateOrderWithCheckoutAsync(CheckOutVM checkOutVM)
        {
            bool Response = false;
            int cartTotal = 0;
            string? StateName = string.Empty; string? CountyName = string.Empty;
            if (!string.IsNullOrEmpty(checkOutVM.CartId))
            {
                List<Cart> carts = await _context.Carts.Include(x => x.CartItems).Include(x => x.CartItems).ToListAsync();
                Cart cart = carts.SingleOrDefault(x => x.Id.ToString() == checkOutVM.CartId)!;
                if (cart != null)
                {
                    cartTotal = cart.CartTotal.GetValueOrDefault();
                    StateName = cart.StateName;
                    CountyName = cart.CountyName;
                }

            }
            List<string?> Dedicateds = await _context.Orders.Select(x => x.DedicatedNumber).ToListAsync();
            Order order = new()
            {
                BuyerName = checkOutVM.BuyerName,
                BuyerFamily = checkOutVM.BuyerFamily,
                BuyerCellphone = checkOutVM.BuyerCellphone,
                RecipientName = checkOutVM.RecepientName,
                RecipientFamily = checkOutVM.RecepientFamily,
                RecipientPhone = checkOutVM.RecepientCellphone,
                Address = checkOutVM.Address,
                PostalCode = checkOutVM.PostalCode,
                Comment = checkOutVM.Comment,
                OrderSum = cartTotal,
                CountyName = CountyName,
                StateName = StateName,
                IsFinished = true,
                CartId = Guid.Parse(checkOutVM.CartId!),
                RegDate = DateTime.Now,
                DiscountCode = "",
                DiscountValue = 100,
                DeliveryCost = 0,
                DeliveryCostDiscount = 0,
                DedicatedNumber = Prodocers.Generators.GenerateUniqueString(Dedicateds.ToList(), 0, 0, 8, 0)

            };

            return Response;
        }

        public async Task<bool> ExistOrderByDiscountCoupen(string Coupen)
        {
            return await _context.Orders.AllAsync(x => x.DiscountCode == Coupen);
        }

        public Task<bool> ValidateDiscountCoupenAsync(string Coupen)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCustomerWithCartInfo(CheckOutVM checkOutVM)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(x => x.Cellphone == checkOutVM.BuyerCellphone);
            if (user == null)
            {
                UserRole userRole = new()
                {
                    User = new()
                    {
                        Name = checkOutVM.BuyerName,
                        Family = checkOutVM.BuyerFamily,
                        Cellphone = checkOutVM.BuyerCellphone,
                        UserName = checkOutVM.BuyerCellphone,
                        Password = Prodocers.Generators.GenerateUniqueString(3, 0, 2, 2),
                        IsActive = true,
                        RegDate = DateTime.Now,
                    },
                    RoleId = 2
                };
                _context.UserRoles.Add(userRole);
                return true;

            }
            return false;
        }
        public async Task<(bool, string)> CreateOrderWithCartAsync(Guid CartId)
        {
            bool Res = false;
            List<string?> ExistDedicateds = await _context.Orders.Select(x => x.DedicatedNumber).ToListAsync();
            string DedNumber = Prodocers.Generators.GenerateUniqueString(ExistDedicateds.ToList(), 0, 0, 8, 0);
            Cart? cart = await _context.Carts.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == CartId);

            if (cart != null)
            {

                Order order = new()
                {
                    CartId = cart.Id,
                    BuyerName = cart.BuyerName,
                    BuyerFamily = cart.BuyerFamily,
                    BuyerCellphone = cart.BuyerCellphone,
                    RecipientName = cart.RecipientName,
                    RecipientFamily = cart.RecipientFamily,
                    RecipientPhone = cart.RecipientPhone,
                    IsFinished = true,
                    DedicatedNumber = DedNumber,
                    RegDate = DateTime.Now,
                    OrderSum = cart.CartTotal,
                    Address = cart.Address,
                    StateName = cart.StateName,
                    CountyName = cart.CountyName,
                    PostalCode = cart.PostalCode,
                    DiscountCode = cart.DiscountCoupen,
                    Currency = cart.Currency,
                    Comment = cart.Comment,
                    DeliveryCost = cart.Freight,
                    UserId = cart.UserId,

                };
                if (cart.ShippingByPishtazPost)
                {
                    order.DeliveryType = "ارسال با پست پیشتاز";
                }
                if (cart.ShippingBySnapp)
                {
                    order.DeliveryType = "ارسال با اسنپ";
                }
                if (cart.ShippingByTipax)
                {
                    order.DeliveryType = "ارسال با تیپاکس";
                }
                foreach (var item in cart.CartItems)
                {
                    ProductItem? productItem = await _context.ProductItems.SingleOrDefaultAsync(x => x.Id == item.ProductItemId.GetValueOrDefault());
                    order.OrderDetails.Add(new OrderDetail()
                    {
                        ProductId = item.ProductItemId.GetValueOrDefault(),
                        ProductCount = item.Quantity,
                        OrderDetailPrice = item.Price,
                        OrderDetailNetPrice = item.NetPrice,
                        OrderDetailDiscountValue = item.Discount,
                        Comment = item.Comment,
                        ProductName = productItem?.Name,

                    });
                }
                _context.Orders.Add(order);
            }
            return (Res, DedNumber);
        }


        #endregion Order
        #region ContactMessage
        public async Task<List<CustomerContact>> GetCustomerContacts()
        {
            return await _context.CustomerContacts.ToListAsync();
        }
        #endregion ContactMessage
    }
}
