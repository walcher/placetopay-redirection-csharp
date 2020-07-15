using Newtonsoft.Json.Linq;
using PlacetoPay.Redirection.Contracts;
using PlacetoPay.Redirection.Extensions;
using System;

namespace PlacetoPay.Redirection.Entities
{
    /// <summary>
    /// Class <c>Item</c>
    /// </summary>
    public class Item : Entity
    {
        protected string sku;
        protected string name;
        protected string category;
        protected int qty;
        protected double price;
        protected double tax;

        /// <summary>
        /// Item constructor.
        /// </summary>
        /// <param name="data">JObject</param>
        public Item(JObject data)
        {
            this.Load<Item>(data, new JArray { "sku", "name", "category", "qty", "price", "tax" });
        }

        /// <summary>
        /// Item constructor.
        /// </summary>
        /// <param name="data">string</param>
        public Item(string data)
        {
            JObject json = JObject.Parse(data);

            this.Load<Item>(json, new JArray { "sku", "name", "category", "qty", "price", "tax" });
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
            throw new NotImplementedException();
        }
    }
}
