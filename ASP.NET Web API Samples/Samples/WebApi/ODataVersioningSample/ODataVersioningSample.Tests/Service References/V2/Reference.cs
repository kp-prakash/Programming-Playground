//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Original file name:
// Generation date: 2013/2/15 15:42:34
namespace ODataVersioningSample.Tests.V2
{

    /// <summary>
    /// There are no comments for Container in the schema.
    /// </summary>
    public partial class Container : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// Initialize a new Container object.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Container(global::System.Uri serviceRoot) :
            base(serviceRoot, global::System.Data.Services.Common.DataServiceProtocolVersion.V3)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            if (typeName.StartsWith("ODataVersioningSample.V2.ViewModels", global::System.StringComparison.Ordinal))
            {
                return this.GetType().Assembly.GetType(string.Concat("ODataVersioningSample.Tests.V2", typeName.Substring(35)), false);
            }
            return null;
        }
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("ODataVersioningSample.Tests.V2", global::System.StringComparison.Ordinal))
            {
                return string.Concat("ODataVersioningSample.V2.ViewModels.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<Product> Products
        {
            get
            {
                if ((this._Products == null))
                {
                    this._Products = base.CreateQuery<Product>("Products");
                }
                return this._Products;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<Product> _Products;
        /// <summary>
        /// There are no comments for ProductFamilies in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<ProductFamily> ProductFamilies
        {
            get
            {
                if ((this._ProductFamilies == null))
                {
                    this._ProductFamilies = base.CreateQuery<ProductFamily>("ProductFamilies");
                }
                return this._ProductFamilies;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<ProductFamily> _ProductFamilies;
        /// <summary>
        /// There are no comments for Suppliers in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<Supplier> Suppliers
        {
            get
            {
                if ((this._Suppliers == null))
                {
                    this._Suppliers = base.CreateQuery<Supplier>("Suppliers");
                }
                return this._Suppliers;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<Supplier> _Suppliers;
        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToProducts(Product product)
        {
            base.AddObject("Products", product);
        }
        /// <summary>
        /// There are no comments for ProductFamilies in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToProductFamilies(ProductFamily productFamily)
        {
            base.AddObject("ProductFamilies", productFamily);
        }
        /// <summary>
        /// There are no comments for Suppliers in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToSuppliers(Supplier supplier)
        {
            base.AddObject("Suppliers", supplier);
        }
    }
    /// <summary>
    /// There are no comments for ODataVersioningSample.V2.ViewModels.Product in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Products")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("ID")]
    public partial class Product : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="ID">Initial value of ID.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Product CreateProduct(long ID)
        {
            Product product = new Product();
            product.ID = ID;
            return product;
        }
        /// <summary>
        /// There are no comments for Property ID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this._ID = value;
                this.OnIDChanged();
                this.OnPropertyChanged("ID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _ID;
        partial void OnIDChanging(long value);
        partial void OnIDChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property ReleaseDate in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> ReleaseDate
        {
            get
            {
                return this._ReleaseDate;
            }
            set
            {
                this.OnReleaseDateChanging(value);
                this._ReleaseDate = value;
                this.OnReleaseDateChanged();
                this.OnPropertyChanged("ReleaseDate");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _ReleaseDate;
        partial void OnReleaseDateChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnReleaseDateChanged();
        /// <summary>
        /// There are no comments for Property SupportedUntil in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> SupportedUntil
        {
            get
            {
                return this._SupportedUntil;
            }
            set
            {
                this.OnSupportedUntilChanging(value);
                this._SupportedUntil = value;
                this.OnSupportedUntilChanged();
                this.OnPropertyChanged("SupportedUntil");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _SupportedUntil;
        partial void OnSupportedUntilChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnSupportedUntilChanged();
        /// <summary>
        /// There are no comments for Family in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public ProductFamily Family
        {
            get
            {
                return this._Family;
            }
            set
            {
                this._Family = value;
                this.OnPropertyChanged("Family");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private ProductFamily _Family;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for ODataVersioningSample.V2.ViewModels.ProductFamily in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("ProductFamilies")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("ID")]
    public partial class ProductFamily : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new ProductFamily object.
        /// </summary>
        /// <param name="ID">Initial value of ID.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static ProductFamily CreateProductFamily(int ID)
        {
            ProductFamily productFamily = new ProductFamily();
            productFamily.ID = ID;
            return productFamily;
        }
        /// <summary>
        /// There are no comments for Property ID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this._ID = value;
                this.OnIDChanged();
                this.OnPropertyChanged("ID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _ID;
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Description in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Description;
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        /// <summary>
        /// There are no comments for Supplier in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Supplier Supplier
        {
            get
            {
                return this._Supplier;
            }
            set
            {
                this._Supplier = value;
                this.OnPropertyChanged("Supplier");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private Supplier _Supplier;
        /// <summary>
        /// There are no comments for Products in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<Product> Products
        {
            get
            {
                return this._Products;
            }
            set
            {
                this._Products = value;
                this.OnPropertyChanged("Products");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<Product> _Products = new global::System.Data.Services.Client.DataServiceCollection<Product>(null, global::System.Data.Services.Client.TrackingMode.None);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for ComplexType ODataVersioningSample.V2.ViewModels.Address in the schema.
    /// </summary>
    public partial class Address : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property Street in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Street
        {
            get
            {
                return this._Street;
            }
            set
            {
                this.OnStreetChanging(value);
                this._Street = value;
                this.OnStreetChanged();
                this.OnPropertyChanged("Street");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Street;
        partial void OnStreetChanging(string value);
        partial void OnStreetChanged();
        /// <summary>
        /// There are no comments for Property City in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this.OnCityChanging(value);
                this._City = value;
                this.OnCityChanged();
                this.OnPropertyChanged("City");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _City;
        partial void OnCityChanging(string value);
        partial void OnCityChanged();
        /// <summary>
        /// There are no comments for Property State in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this.OnStateChanging(value);
                this._State = value;
                this.OnStateChanged();
                this.OnPropertyChanged("State");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _State;
        partial void OnStateChanging(string value);
        partial void OnStateChanged();
        /// <summary>
        /// There are no comments for Property ZipCode in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ZipCode
        {
            get
            {
                return this._ZipCode;
            }
            set
            {
                this.OnZipCodeChanging(value);
                this._ZipCode = value;
                this.OnZipCodeChanged();
                this.OnPropertyChanged("ZipCode");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _ZipCode;
        partial void OnZipCodeChanging(string value);
        partial void OnZipCodeChanged();
        /// <summary>
        /// There are no comments for Property Country in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                this.OnCountryChanging(value);
                this._Country = value;
                this.OnCountryChanged();
                this.OnPropertyChanged("Country");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Country;
        partial void OnCountryChanging(string value);
        partial void OnCountryChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for ODataVersioningSample.V2.ViewModels.Supplier in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Suppliers")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("ID")]
    public partial class Supplier : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new Supplier object.
        /// </summary>
        /// <param name="ID">Initial value of ID.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Supplier CreateSupplier(int ID)
        {
            Supplier supplier = new Supplier();
            supplier.ID = ID;
            return supplier;
        }
        /// <summary>
        /// There are no comments for Property ID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this._ID = value;
                this.OnIDChanged();
                this.OnPropertyChanged("ID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _ID;
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Address in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Address Address
        {
            get
            {
                if (((this._Address == null)
                            && (this._AddressInitialized != true)))
                {
                    this._Address = new Address();
                    this._AddressInitialized = true;
                }
                return this._Address;
            }
            set
            {
                this.OnAddressChanging(value);
                this._Address = value;
                this._AddressInitialized = true;
                this.OnAddressChanged();
                this.OnPropertyChanged("Address");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private Address _Address;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _AddressInitialized;
        partial void OnAddressChanging(Address value);
        partial void OnAddressChanged();
        /// <summary>
        /// There are no comments for ProductFamilies in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<ProductFamily> ProductFamilies
        {
            get
            {
                return this._ProductFamilies;
            }
            set
            {
                this._ProductFamilies = value;
                this.OnPropertyChanged("ProductFamilies");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<ProductFamily> _ProductFamilies = new global::System.Data.Services.Client.DataServiceCollection<ProductFamily>(null, global::System.Data.Services.Client.TrackingMode.None);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
