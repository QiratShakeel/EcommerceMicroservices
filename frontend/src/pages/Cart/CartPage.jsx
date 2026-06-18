import MainLayout from "../../layouts/MainLayout";

export const CartPage = () => {
  return (
    <MainLayout>
      <div className="section">
        <div className="container">
          <div className="row">

            <div className="col-md-12">
              <div className="order-details">
                <div className="section-title text-center">
                  <h3 className="title">Your Cart</h3>
                </div>

                <div className="order-summary">

                  {/* Header Row */}
                  <div className="order-col">
                    <div><strong>Product</strong></div>

                    <div><strong>Total</strong></div>
                  </div>

                  {/* Product 1 */}
                  <div className="order-col">
                    <div style={{display: "flex", alignItems: "center"}}>
                      
                      <div className="product-widget" style={{flex: 1}}>
                        <div className="product-img">
                          <img src="../../../public/img/product03.png" alt="" />
                        </div>
                        <div className="product-body">
                          <h3 className="product-name">Product Name</h3>
                          <h4 className="product-price">$4.87</h4>
                        </div>
                      </div>

                      {/* Quantity */}
                      <div style={{width: "120px", marginLeft: "15px"}}>
                        <div className="input-number">
                          <input type="number" defaultValue="3" />
                          <span className="qty-up">+</span>
                          <span className="qty-down">-</span>
                        </div>
                      </div>

                    </div>
                    {/* Per Price */}
                    <div>$4.61</div>

                    {/* Total */}
                    <div>$14.61</div>
                  </div>

                  {/* Product 2 */}
                  <div className="order-col">
                    <div style={{display: "flex", alignItems: "center"}}>
                      
                      <div className="product-widget" style={{flex: 1}}>
                        <div className="product-img">
                          <img src="../..//img/product01.png" alt="" />
                        </div>
                        <div className="product-body">
                          <h3 className="product-name">Product Name</h3>
                          <h4 className="product-price">$4.99</h4>
                        </div>
                      </div>

                      {/* Quantity */}
                      <div style={{width: "120px", marginLeft: "15px"}}>
                        <div className="input-number">
                          <input type="number" defaultValue="2" />
                          <span className="qty-up">+</span>
                          <span className="qty-down">-</span>
                        </div>
                      </div>

                    </div>
                    {/* Total */}
                    <div>$5.08</div>

                    {/* Total */}
                    <div>$9.98</div>
                  </div>

                  <hr />

                  {/* Summary */}
                  <div className="order-col">
                    <div>Subtotal</div>
                    <div>$24.59</div>
                  </div>

                  <div className="order-col">
                    <div>Shipping</div>
                    <div>$6.94</div>
                  </div>

                  <div className="order-col">
                    <div><strong>Total</strong></div>
                    <div><strong className="order-total">$31.53</strong></div>
                  </div>

                </div>

                {/* Buttons */}
                <div style={{ marginTop: "20px", textAlign: "right" }}>
                  <button className="primary-btn" style={{ marginRight: "10px" }}>
                    Continue Shopping
                  </button>
                  <button className="primary-btn">
                    Checkout
                  </button>
                </div>

              </div>
            </div>

          </div>
        </div>
      </div>
    </MainLayout>
  );
};