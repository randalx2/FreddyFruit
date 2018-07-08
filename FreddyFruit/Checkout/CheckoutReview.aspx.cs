using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreddyFruit.Models;

namespace FreddyFruit.Checkout
{
    public partial class CheckoutReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string retMsg = "";
                string token = "";
                string PayerID = "";

                //TODO: Replace the default value of true with a validation control signal
                if (true)
                {
                    Session["payerId"] = PayerID;

                    var myOrder = new Order
                    {
                        OrderDate = DateTime.Now,
                        Username = User.Identity.Name,
                        Total = Convert.ToDecimal(Session["payment_amt"]),
                        FirstName = "TestBuyer",
                        LastName = "Tester",
                        Address = "Test Street",
                        City = "Test City",
                        State = "Test State",
                        PostalCode = "4339",
                        Country = "South Africa",
                        Email = "tester@gmail.com"
                    };

                    // Get DB context.
                    ProductContext _db = new ProductContext();

                    // Add order to DB.
                    _db.Orders.Add(myOrder);
                    _db.SaveChanges();

                    // Get the shopping cart items and process them.
                    using (FreddyFruit.Logic.ShoppingCartActions usersShoppingCart = new FreddyFruit.Logic.ShoppingCartActions())
                    {
                        List<CartItem> myOrderList = usersShoppingCart.GetCartItems();

                        // Add OrderDetail information to the DB for each product purchased.
                        for (int i = 0; i < myOrderList.Count; i++)
                        {
                            // Create a new OrderDetail object.
                            var myOrderDetail = new OrderDetail();
                            myOrderDetail.OrderId = myOrder.OrderId;
                            myOrderDetail.Username = User.Identity.Name;
                            myOrderDetail.ProductId = myOrderList[i].ProductId;
                            myOrderDetail.Quantity = myOrderList[i].Quantity;
                            myOrderDetail.UnitPrice = myOrderList[i].Product.UnitPrice;

                            // Add OrderDetail to DB.
                            _db.OrderDetails.Add(myOrderDetail);
                            _db.SaveChanges();
                        }

                        // Set OrderId.
                        Session["currentOrderId"] = myOrder.OrderId;

                        // Display Order information.
                        List<Order> orderList = new List<Order>();
                        orderList.Add(myOrder);
                        ShipInfo.DataSource = orderList;
                        ShipInfo.DataBind();

                        // Display OrderDetails.
                        OrderItemList.DataSource = myOrderList;
                        OrderItemList.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("CheckoutError.aspx?" + retMsg);
                }                               
            }
        }

        protected void CheckoutConfirm_Click(object sender, EventArgs e)
        {
            Session["userCheckoutCompleted"] = "true";
            Response.Redirect("~/Checkout/CheckoutComplete.aspx");
        }
    }
}