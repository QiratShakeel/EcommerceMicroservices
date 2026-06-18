import MainLayout from "../../layouts/MainLayout";

export const OrdersPage = () => {
  return (
    <MainLayout>
      <section className="section" style={{margin: "0 auto"}}>
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-10 col-lg-8">
              <div className="product" style={{ borderRadius: "10px" }}>

                <div style={{ padding: "20px" }}>
                  <h4>
                    Thanks for your Order,{" "}
                    <span style={{ color: "#D10024" }}>Anna</span>!
                  </h4>
                </div>

                <div style={{ padding: "20px" }}>
                  <div className="clearfix" style={{ marginBottom: "20px" }}>
                    <h4 style={{ float: "left", color: "#D10024" }}>
                      Receipt
                    </h4>
                    <p style={{ float: "right", color: "#8D99AE" }}>
                      Receipt Voucher : 1KAU9-84UIL
                    </p>
                  </div>

                  {/* PRODUCT 1 */}
                  <div className="product-widget">
                    <div className="product-img" style={{position: "relative"}}>
                      <img
                        src="../../../public/img/product01.png"
                        alt=""
                      />
                    </div>

                    <div className="product-body">
                      <p className="product-category">Samsung Galaxy</p>
                      <p>White | 64GB | Qty: 1</p>
                      <h4 className="product-price">$499</h4>
                    </div>
                  </div>

                  <hr />

                  {/* TRACK */}
                  <p style={{ color: "#8D99AE" }}>Track Order</p>
                  <div
                    style={{
                      height: "6px",
                      backgroundColor: "#E4E7ED",
                      borderRadius: "10px",
                    }}
                  >
                    <div
                      style={{
                        width: "65%",
                        height: "100%",
                        backgroundColor: "#D10024",
                        borderRadius: "10px",
                      }}
                    ></div>
                    <div style={{ display: "flex", justifyContent: "space-between", fontSize: "12px", color: "#8D99AE", marginTop: "5px" }}>
                      <span>Out for delivery</span>
                      <span>Delivered</span>
                    </div>
                  </div>

                  <br />

                  {/* PRODUCT 2 */}
                  <div className="product-widget">
                    <div className="product-img">
                      <img
                        src="https://mdbcdn.b-cdn.net/img/Photos/Horizontal/E-commerce/Products/1.webp"
                        alt=""
                      />
                    </div>

                    <div className="product-body">
                      <p className="product-category">iPad</p>
                      <p>Pink | 32GB | Qty: 1</p>
                      <h4 className="product-price">$399</h4>
                    </div>
                  </div>

                  <hr />

                  <p style={{ color: "#8D99AE" }}>Track Order</p>
                  <div
                    style={{
                      height: "6px",
                      backgroundColor: "#E4E7ED",
                      borderRadius: "10px",
                    }}
                  >
                    <div
                      style={{
                        width: "20%",
                        height: "100%",
                        backgroundColor: "#D10024",
                        borderRadius: "10px",
                      }}
                    ></div>
                  </div>

                  <hr />

                  {/* SUMMARY */}
                  <div className="clearfix">
                    <p style={{ float: "left" }}>Order Details</p>
                    <p style={{ float: "right", color: "#8D99AE" }}>
                      Total: $898
                    </p>
                  </div>

                  <div className="clearfix">
                    <p style={{ float: "left", color: "#8D99AE" }}>
                      Invoice Number : 788152
                    </p>
                    <p style={{ float: "right", color: "#8D99AE" }}>
                      Discount: $19
                    </p>
                  </div>

                  <div className="clearfix">
                    <p style={{ float: "left", color: "#8D99AE" }}>
                      Invoice Date : 22 Dec
                    </p>
                    <p style={{ float: "right", color: "#8D99AE" }}>
                      GST: 123
                    </p>
                  </div>

                  <div className="clearfix">
                    <p style={{ float: "left", color: "#8D99AE" }}>
                      Receipt Voucher : 18KU-62IIK
                    </p>
                    <p style={{ float: "right", color: "#8D99AE" }}>
                      Delivery: Free
                    </p>
                  </div>
                </div>

                {/* FOOTER */}
                <div
                  style={{
                    backgroundColor: "#D10024",
                    color: "#FFF",
                    padding: "20px",
                    textAlign: "right",
                    borderBottomLeftRadius: "10px",
                    borderBottomRightRadius: "10px",
                  }}
                >
                  <h3>Total Paid: $1040</h3>
                </div>

              </div>
            </div>
          </div>
        </div>
      </section>
    </MainLayout>
  );
};