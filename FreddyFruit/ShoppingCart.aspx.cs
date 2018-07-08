using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreddyFruit.Models;
using FreddyFruit.Logic;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;
using System.Web.ModelBinding;

namespace FreddyFruit
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        /// <summary>
        /// Reloads the page with cart data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Update cart items on reload
            UpdateCartItems();

            //Check for an empty cart
            if (GetShoppingCartItems().Count == 0)
            {
                LabelTotalText.Text = "";
                lblTotal.Text = "";
                lblSavings.Text = "";           
                lblTotalWithDiscount.Text = "";

                ShoppingCartTitle.InnerText = "Shopping Cart is Empty";

                UpdateBtn.Visible = false;
                CheckoutImageBtn.Visible = false;
            }
        }

        /// <summary>
        /// Returns all the items in the cart
        /// </summary>
        /// <returns></returns>
        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions actions = new ShoppingCartActions();
            return actions.GetCartItems();
        }

        /// <summary>
        /// Updates the cart according to item quantities.
        /// Updated to take into account product specials.
        /// </summary>
        /// <returns></returns>
        public List<CartItem> UpdateCartItems()
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                String cartId = usersShoppingCart.GetCartId();

                ShoppingCartActions.ShoppingCartUpdates[] cartUpdates =
                    new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count];

                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");

                    cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                }

                usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();

                //Check for more than 10 bananas
                foreach (var shoppingCartItem in GetShoppingCartItems())
                {
                    if (shoppingCartItem.Product.ProductName.Equals("Bananas", StringComparison.Ordinal) &&
                        shoppingCartItem.Quantity > 10)
                    {
                        lblWarning.ForeColor = Color.Red;
                        lblWarning.Text = "Warning! You can only order a maximum of 10 Bananas. Net Total Updated.";                       
                    }
                }

                lblTotal.Text = String.Format("{0:c}", usersShoppingCart.GetTotal());

                //TODO: Timing issue may be present with these two lines
                //TODO: The data seems to correct itself on the 2nd button click.

                lblSavings.Text = String.Format("{0:c}", usersShoppingCart.GetSavings());
                lblTotalWithDiscount.Text = String.Format("{0:c}", usersShoppingCart.GetTotalWithDiscount());

                return usersShoppingCart.GetCartItems();
            }
        }

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible)
                {
                    // Extract values from the cell.
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
        }

        protected void CheckoutBtn_Click(object sender, ImageClickEventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                //Session["payment_amt"] = usersShoppingCart.GetTotal();
                Session["payment_amt"] = usersShoppingCart.GetTotalWithDiscount();
            }

            //Response.Redirect("Checkout/CheckoutStart.aspx");

            //Skip Paypal verificatin
            Response.Redirect("Checkout/CheckoutReview.aspx");
        }
    }
}