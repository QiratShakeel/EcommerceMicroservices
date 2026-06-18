import React from 'react';
import MainLayout from "../../layouts/MainLayout";

export const LoginPage = () => {
  return (
    <MainLayout>
      <section className="section" style={{ backgroundColor: "#FBFBFC", minHeight: "100vh" }}>
        <div className="container">
          <div className="row">
            <div className="col-md-10 col-md-offset-1">

              {/* PANEL */}
              <div className="product" style={{ padding: "30px", borderRadius: "30px" }}>
                <div className="row">

                  {/* LEFT IMAGE */}
                  <div className="col-md-6 hidden-sm hidden-xs text-center">
                    <img
                      src="/img/login.webp"
                      alt="login"
                      style={{ maxWidth: "100%", marginTop: "40px" }}
                    />
                  </div>

                  {/* RIGHT FORM */}
                  <div className="col-md-6 col-sm-12">

                    <h2 style={{ textAlign: "center", marginBottom: "30px" }}>
                      Login
                    </h2>

                    <form>

                      {/* SOCIAL */}
                      <div style={{ textAlign: "center", marginBottom: "20px" }}>
                        <p style={{ color: "#8D99AE" }}>Sign in with:</p>

                        <button type="button" className="btn btn-primary" style={{ marginRight: "5px", borderRadius: "50%" }}>
                          <i className="fa fa-facebook"></i>
                        </button>

                        <button type="button" className="btn btn-info" style={{ margin: "0 5px", borderRadius: "50%" }}>
                          <i className="fa fa-twitter"></i>
                        </button>

                        <button type="button" className="btn btn-danger" style={{ marginLeft: "5px", borderRadius: "50%" }}>
                          <i className="fa fa-google"></i>
                        </button>
                      </div>

                      <div className="divider">
                        <p><strong>Or</strong></p>
                      </div>

                      {/* EMAIL */}
                      <div style={{ marginBottom: "15px" }}>
                        <label>Email Address</label>
                        <input
                          type="email"
                          className="input"
                          placeholder="Enter your email"
                        />
                      </div>

                      {/* PASSWORD */}
                      <div style={{ marginBottom: "15px" }}>
                        <label>Password</label>
                        <input
                          type="password"
                          className="input"
                          placeholder="Enter password"
                        />
                      </div>

                      {/* REMEMBER */}
                      <div className="clearfix" style={{ marginBottom: "20px" }}>
                        <div style={{ float: "left" }}>
                          <label>
                            <input type="checkbox" /> Remember me
                          </label>
                        </div>

                        <div style={{ float: "right" }}>
                          <a href="#!" style={{ color: "#8D99AE" }}>
                            Forgot password?
                          </a>
                        </div>
                      </div>

                      {/* BUTTON */}
                      <button className="primary-btn" style={{ width: "100%" }}>
                        Login
                      </button>

                      <p style={{ textAlign: "center", marginTop: "20px" }}>
                        Don’t have an account?{" "}
                        <a href="#!" style={{ color: "#D10024", fontWeight: "700" }}>
                          Register
                        </a>
                      </p>

                    </form>

                  </div>
                </div>
              </div>
              {/* PANEL END */}

            </div>
          </div>
        </div>
      </section>
    </MainLayout>
  );
};