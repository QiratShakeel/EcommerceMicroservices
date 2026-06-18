import MainLayout from "../../layouts/MainLayout";

export const CheckoutPage = () => {
  return (
    <MainLayout>
      <div className="section">
        <div className="container">
          <div className="row">

            {/* LEFT SIDE */}
            <div className="col-md-7">

              {/* Billing */}
              <div className="billing-details">
                <div className="section-title">
                  <h3 className="title">Billing address</h3>
                </div>

                <div className="form-group">
                  <input className="input" type="text" placeholder="First Name" />
                </div>
                <div className="form-group">
                  <input className="input" type="text" placeholder="Last Name" />
                </div>
                <div className="form-group">
                  <input className="input" type="email" placeholder="Email" />
                </div>
                <div className="form-group">
                  <input className="input" type="text" placeholder="Address" />
                </div>
                <div className="form-group">
                  <input className="input" type="text" placeholder="City" />
                </div>
                <div className="form-group">
                  <input className="input" type="text" placeholder="Country" />
                </div>
                <div className="form-group">
                  <input className="input" type="text" placeholder="ZIP Code" />
                </div>
                <div className="form-group">
                  <input className="input" type="tel" placeholder="Telephone" />
                </div>

                {/* Create Account */}
                <div className="input-checkbox">
                  <input type="checkbox" id="create-account" />
                  <label htmlFor="create-account">
                    <span></span>
                    Create Account?
                  </label>
                  <div className="caption">
                    <input className="input" type="password" placeholder="Password" />
                  </div>
                </div>
              </div>

              {/* Shipping */}
              <div className="shiping-details">
                <div className="section-title">
                  <h3 className="title">Shipping address</h3>
                </div>

                <div className="input-checkbox">
                  <input type="checkbox" id="shiping-address" />
                  <label htmlFor="shiping-address">
                    <span></span>
                    Ship to a different address?
                  </label>

                  <div className="caption">
                    <div className="form-group">
                      <input className="input" type="text" placeholder="Address" />
                    </div>
                    <div className="form-group">
                      <input className="input" type="text" placeholder="City" />
                    </div>
                    <div className="form-group">
                      <input className="input" type="text" placeholder="Country" />
                    </div>
                  </div>
                </div>
              </div>

              {/* Notes */}
              <div className="order-notes">
                <textarea className="input" placeholder="Order Notes"></textarea>
              </div>

            </div>

            {/* RIGHT SIDE */}
            <div className="col-md-5">
              <div className="order-details">

                <div className="section-title text-center">
                  <h3 className="title">Your Order</h3>
                </div>

                <div className="order-summary">

                  <div className="order-col">
                    <div><strong>Product</strong></div>
                    <div><strong>Total</strong></div>
                  </div>

                  <div className="order-products">
                    <div className="order-col">
                      <div>1x Product Name</div>
                      <div>$980.00</div>
                    </div>
                    <div className="order-col">
                      <div>2x Product Name</div>
                      <div>$980.00</div>
                    </div>
                  </div>

                  <div className="order-col">
                    <div>Shipping</div>
                    <div><strong>FREE</strong></div>
                  </div>

                  <div className="order-col">
                    <div><strong>Total</strong></div>
                    <div><strong className="order-total">$2940.00</strong></div>
                  </div>

                </div>

                {/* Payment */}
                <div className="payment-method">

                  <div className="input-radio">
                    <input type="radio" name="payment" id="payment-1" />
                    <label htmlFor="payment-1">
                      <span></span>
                      Direct Bank Transfer
                    </label>
                  </div>

                  <div className="input-radio">
                    <input type="radio" name="payment" id="payment-2" />
                    <label htmlFor="payment-2">
                      <span></span>
                      Cash on Delivery
                    </label>
                  </div>

                </div>

                {/* Terms */}
                <div className="input-checkbox">
                  <input type="checkbox" id="terms" />
                  <label htmlFor="terms">
                    <span></span>
                    I agree to terms & conditions
                  </label>
                </div>

                <button className="primary-btn order-submit">
                  Place Order
                </button>

              </div>
            </div>

          </div>
        </div>
      </div>
    </MainLayout>
  );
};