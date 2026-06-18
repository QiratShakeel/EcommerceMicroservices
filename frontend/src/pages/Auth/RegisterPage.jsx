// import React, { useState } from "react";

import MainLayout from "../../layouts/MainLayout";

export const RegisterPage = () => {
    //   const [form, setForm] = useState({
    //     name: "",
    //     email: "",
    //     password: "",
    //   });

    //   const handleChange = (e) => {
    //     setForm({
    //       ...form,
    //       [e.target.name]: e.target.value,
    //     });
    //   };

    //   const handleSubmit = (e) => {
    //     e.preventDefault();
    //     console.log("Register Data:", form);
    //     // TODO: API call to Identity Service
    //   };

    return (
    <MainLayout>
      <section className="section" style={{ backgroundColor: "#FBFBFC", minHeight: "100vh" }}>
        <div className="container">
          <div className="row">
            <div className="col-lg-10 col-lg-offset-1">
              <div className="product" style={{ padding: "30px", borderRadius: "30px" }}>
                <div className="row">
                  
                  {/* Left Side: Form */}
                  <div className="col-md-6 col-sm-12">
                    <h2 style={{ textAlign: "center", marginBottom: "30px" }}>
                      Sign Up
                    </h2>
                    
                    <form className="form-horizontal">
                      
                      {/* Name Field */}
                      <div className="input-group form-group-custom">
                        <span className="input-group-addon"><i className="fa fa-user fa-fw" style={{height: "10px", color:"#6b6666"}}></i></span>
                        <input type="text" className="input" placeholder="Your Name" />
                      </div>

                      {/* Email Field */}
                      <div className="input-group form-group-custom">
                        <span className="input-group-addon"><i className="fa fa-envelope fa-fw" style={{height: "10px", color:"#6b6666"}}></i></span>
                        <input type="email" className="input" placeholder="Your Email" />
                      </div>

                      {/* Password Field */}
                      <div className="input-group form-group-custom">
                        <span className="input-group-addon"><i className="fa fa-lock fa-fw" style={{height: "10px", color:"#6b6666"}}></i></span>
                        <input type="password" className="input" placeholder="Password" />
                      </div>

                      {/* Repeat Password */}
                      <div className="input-group form-group-custom">
                        <span className="input-group-addon"><i className="fa fa-key fa-fw" style={{height: "10px", color:"#6b6666"}}></i></span>
                        <input type="password" className="input" placeholder="Repeat your password" />
                      </div>

                      {/* Checkbox */}
                      <div className="checkbox text-center" style={{ marginBottom: '30px' }}>
                        <label>
                          <input type="checkbox" /> I agree all statements in <a href="#!">Terms of service</a>
                        </label>
                      </div>

                      {/* BUTTON */}
                      <button className="primary-btn" style={{ width: "100%" }}>
                        Register
                      </button>

                    </form>
                  </div>

                  {/* Right Side: Image */}
                  <div className="col-md-6 hidden-xs hidden-sm text-center">
                    <img 
                      src="../../../public/img/register.webp" 
                      className="img-responsive" 
                      alt="Sample" 
                      style={{ marginTop: '40px' }}
                    />
                  </div>

                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </MainLayout>
    );
};
