export const Footer = () => {
  return (
    <>
    {/* // <!-- NEWSLETTER --> */}
		<div id="newsletter" className="section">
			{/* <!-- container --> */}
			<div className="container">
				{/* <!-- row --> */}
				<div className="row">
					<div className="col-md-12">
						<div className="newsletter">
							<p>Sign Up for the <strong>NEWSLETTER</strong></p>
							<form>
								<input className="input" type="email" placeholder="Enter Your Email"/>
								<button className="newsletter-btn"><i className="fa fa-envelope"></i> Subscribe</button>
							</form>
							<ul className="newsletter-follow">
								<li>
									<a href="#"><i className="fa fa-facebook"></i></a>
								</li>
								<li>
									<a href="#"><i className="fa fa-twitter"></i></a>
								</li>
								<li>
									<a href="#"><i className="fa fa-instagram"></i></a>
								</li>
								<li>
									<a href="#"><i className="fa fa-pinterest"></i></a>
								</li>
							</ul>
						</div>
					</div>
				</div>
				{/* <!-- /row --> */}
			</div>
			{/* <!-- /container --> */}
		</div>
		  {/*  /Newsletter  */}

    <footer id="footer">

      {/* Top Footer */}
      <div className="section">
        <div className="container">
          <div className="row">

            {/* About */}
            <div className="col-md-3 col-xs-6">
              <div className="footer">
                <h3 className="footer-title">About Us</h3>
                <p>Lorem ipsum dolor sit amet...</p>
                <ul className="footer-links">
                  <li>1734 Stonecoal Road</li>
                  <li>+021-95-51-84</li>
                  <li>email@email.com</li>
                </ul>
              </div>
            </div>

            {/* Categories */}
            <div className="col-md-3 col-xs-6">
              <div className="footer">
                <h3 className="footer-title">Categories</h3>
                <ul className="footer-links">
                  <li>Hot deals</li>
                  <li>Laptops</li>
                  <li>Smartphones</li>
                </ul>
              </div>
            </div>

            {/* Info */}
            <div className="col-md-3 col-xs-6">
              <div className="footer">
                <h3 className="footer-title">Information</h3>
                <ul className="footer-links">
                  <li>About Us</li>
                  <li>Contact Us</li>
                  <li>Privacy Policy</li>
                </ul>
              </div>
            </div>

            {/* Service */}
            <div className="col-md-3 col-xs-6">
              <div className="footer">
                <h3 className="footer-title">Service</h3>
                <ul className="footer-links">
                  <li>My Account</li>
                  <li>View Cart</li>
                  <li>Wishlist</li>
                </ul>
              </div>
            </div>

          </div>
        </div>
      </div>

      {/* Bottom Footer */}
      <div id="bottom-footer" className="section">
        <div className="container text-center">
          <span>© 2026 All rights reserved</span>
        </div>
      </div>

    </footer>
    </>
  );
};