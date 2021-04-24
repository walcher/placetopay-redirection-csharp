using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using PlacetoPay.Redirection.Helpers;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Item</c>
    /// </summary>
    public class Item : Entity
    {
        protected const string CATEGORY = "category";
        protected const string NAME = "name";
        protected const string PRICE = "price";
        protected const string QTY = "qty";
        protected const string SKU = "sku";
        protected const string TAX = "tax";

        protected string sku;
        protected string name;
        protected string category;
        protected int qty;
        protected double price;
        protected double tax;

        /// <summary>
        /// Item constructor.
        /// </summary>
        public Item() { }

        /// <summary>
        /// Item constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Item(string data) : this(JsonFormatter.ParseJObject(data)) { }

        /// <summary>
        /// Item constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Item(JObject data)
        {
            this.Load<Item>(data, new JArray { SKU, NAME, CATEGORY, QTY, PRICE, TAX });
        }

        /// <summary>
        /// Item constructor.
        /// </summary>
        /// <param name="sku">string</param>
        /// <param name="name">string</param>
        /// <param name="category">string</param>
        /// <param name="qty">int</param>
        /// <param name="price">double</param>
        /// <param name="tax">double</param>
        public Item(
            string sku,
            string name,
            string category,
            int qty,
            double price,
            double tax
            )
        {
            this.sku = sku;
            this.name = name;
            this.category = category;
            this.qty = qty;
            this.price = price;
            this.tax = tax;
        }

        /// <summary>
        /// Sku property.
        /// </summary>
        public string Sku
        {
            get { return sku; }
            set { sku = value; }
        }

        /// <summary>
        /// Name property.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Category property.
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// Qty property.
        /// </summary>
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        /// <summary>
        /// Price property.
        /// </summary>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// Tax property.
        /// </summary>
        public double Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        /// <summary>
        /// Json Object sent back from API.
        /// </summary>
        /// <returns>JsonObject</returns>
        public override JObject ToJsonObject()
        {
            return JObjectFilter(new JObject {
                { SKU, Sku },
                { NAME, Name },
                { CATEGORY, Category },
                { QTY, Qty },
                { PRICE, Price },
                { TAX, Tax },
            });
        }
    }
}
