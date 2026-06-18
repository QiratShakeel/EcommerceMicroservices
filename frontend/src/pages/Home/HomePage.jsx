import { useEffect, useState, useRef  } from "react";
import { API } from "../../services/api";
import MainLayout from "../../layouts/MainLayout";
import { ProductList } from "../../components/product/ProductList";


const HomePage = () => {
//   const [products, setProducts] = useState([]);
//   useEffect(() => {
//     API.get("/catalog/api/products")
//       .then((res) => setProducts(res.data))
//       .catch((err) => console.log(err));
//   }, []);
// 	const sliderRef = useRef(null);

//   	useEffect(() => {
//     	const $el = window.$(sliderRef.current);

//     if ($el && !$el.hasClass("slick-initialized")) {
//       $el.slick({
//         slidesToShow: 4,
//         slidesToScroll: 1,
// 		autoplay: true,
//         infinite: true,
//       });
//     }

//     return () => {
//       if ($el && $el.hasClass("slick-initialized")) {
//         $el.slick("unslick"); // cleanup (VERY IMPORTANT)
//       }
//     };
//   }, []);
  return (
    <MainLayout>
		{/* <!-- SECTION --> */}
		<div className="section">
			{/* <!-- container --> */}
			<div className="container">
				{/* <!-- row --> */}
				<div className="row">
					{/* <!-- shop --> */}
					<div className="col-md-4 col-xs-6">
						<div className="shop">
							<div className="shop-img">
								<img src="/img/shop01.png" alt=""/>
							</div>
							<div className="shop-body">
								<h3>Laptop<br/>Collection</h3>
								<a href="#" className="cta-btn">Shop now <i className="fa fa-arrow-circle-right"></i></a>
							</div>
						</div>
					</div>
					{/* <!-- /shop --> */}

					{/* <!-- shop --> */}
					<div className="col-md-4 col-xs-6">
						<div className="shop">
							<div className="shop-img">
								<img src="/img/shop03.png" alt=""/>
							</div>
							<div className="shop-body">
								<h3>Accessories<br/>Collection</h3>
								<a href="#" className="cta-btn">Shop now <i className="fa fa-arrow-circle-right"></i></a>
							</div>
						</div>
					</div>
					{/* <!-- /shop --> */}

					{/* <!-- shop --> */}
					<div className="col-md-4 col-xs-6">
						<div className="shop">
							<div className="shop-img">
								<img src="/img/shop02.png" alt=""/>
							</div>
							<div className="shop-body">
								<h3>Cameras<br/>Collection</h3>
								<a href="#" className="cta-btn">Shop now <i className="fa fa-arrow-circle-right"></i></a>
							</div>
						</div>
					</div>
					{/* <!-- /shop --> */}
				</div>
				{/* <!-- /row --> */}
			</div>
			{/* <!-- /container --> */}
		</div>
		{/* <!-- /SECTION --> */}

		{/* <!-- SECTION --> */}
		<div className="section">
			{/* <!-- container --> */}
			<div className="container">
				{/* <!-- row --> */}
				<div className="row">

					{/* <!-- section title --> */}
					<div className="col-md-12">
						<div className="section-title">
							<h3 className="title">New Products</h3>
							<div className="section-nav">
								<ul className="section-tab-nav tab-nav">
									<li className="active"><a data-toggle="tab" href="#tab1">Laptops</a></li>
									<li><a data-toggle="tab" href="#tab1">Smartphones</a></li>
									<li><a data-toggle="tab" href="#tab1">Cameras</a></li>
									<li><a data-toggle="tab" href="#tab1">Accessories</a></li>
								</ul>
							</div>
						</div>
					</div>
					{/* <!-- /section title --> */}

					{/* <!-- Products tab & slick --> */}
					<div className="col-md-12">
						<div className="row">
							<div className="products-tabs">
								{/* <!-- tab --> */}
								<div id="tab1" className="tab-pane active">
									<div className="products-slick" data-nav="#slick-nav-1"  >
										{/* <!-- product --> */}
										<ProductList/>
										{/* <!-- /product --> */}
									</div>
									<div id="slick-nav-1" className="products-slick-nav"></div>
								</div>
								{/* <!-- /tab --> */}
							</div>
						</div>
					</div>
					{/* <!-- Products tab & slick --> */}
				</div>
				{/* <!-- /row --> */}
			</div>
			{/* <!-- /container --> */}
		</div>
		{/* <!-- /SECTION --> */}

		{/* <!-- HOT DEAL SECTION --> */}
		<div id="hot-deal" className="section">
			{/* <!-- container --> */}
			<div className="container">
				{/* <!-- row --> */}
				<div className="row">
					<div className="col-md-12">
						<div className="hot-deal">
							<ul className="hot-deal-countdown">
								<li>
									<div>
										<h3>02</h3>
										<span>Days</span>
									</div>
								</li>
								<li>
									<div>
										<h3>10</h3>
										<span>Hours</span>
									</div>
								</li>
								<li>
									<div>
										<h3>34</h3>
										<span>Mins</span>
									</div>
								</li>
								<li>
									<div>
										<h3>60</h3>
										<span>Secs</span>
									</div>
								</li>
							</ul>
							<h2 className="text-uppercase">hot deal this week</h2>
							<p>New Collection Up to 50% OFF</p>
							<a className="primary-btn cta-btn" href="#">Shop now</a>
						</div>
					</div>
				</div>
				{/* <!-- /row --> */}
			</div>
			{/* <!-- /container --> */}
		</div>
		{/* <!-- /HOT DEAL SECTION --> */}

		{/* <!-- SECTION --> */}
		<div className="section">
			{/* <!-- container --> */}
			<div className="container">
				{/* <!-- row --> */}
				<div className="row">
					<div className="col-md-4 col-xs-6">
						<div className="section-title">
							<h4 className="title">Top selling</h4>
							<div className="section-nav">
								<div id="slick-nav-3" className="products-slick-nav"></div>
							</div>
						</div>

						<div className="products-widget-slick" data-nav="#slick-nav-3">
							<div>
								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product07.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product08.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product09.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- product widget --> */}
							</div>

							<div>
								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product01.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product02.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product03.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- product widget --> */}
							</div>
						</div>
					</div>

					<div className="col-md-4 col-xs-6">
						<div className="section-title">
							<h4 className="title">Top selling</h4>
							<div className="section-nav">
								<div id="slick-nav-4" className="products-slick-nav"></div>
							</div>
						</div>

						<div className="products-widget-slick" data-nav="#slick-nav-4">
							<div>
								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product04.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product05.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product06.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- product widget --> */}
							</div>

							<div>
								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product07.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product08.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product09.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- product widget --> */}
							</div>
						</div>
					</div>

					<div className="clearfix visible-sm visible-xs"></div>

					<div className="col-md-4 col-xs-6">
						<div className="section-title">
							<h4 className="title">Top selling</h4>
							<div className="section-nav">
								<div id="slick-nav-5" className="products-slick-nav"></div>
							</div>
						</div>

						<div className="products-widget-slick" data-nav="#slick-nav-5">
							<div>
								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product01.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product02.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product03.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- product widget --> */}
							</div>

							<div>
								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product04.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product05.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- /product widget --> */}

								{/* <!-- product widget --> */}
								<div className="product-widget">
									<div className="product-img">
										<img src="/img/product06.png" alt=""/>
									</div>
									<div className="product-body">
										<p className="product-category">Category</p>
										<h3 className="product-name"><a href="#">product name goes here</a></h3>
										<h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
									</div>
								</div>
								{/* <!-- product widget --> */}
							</div>
						</div>
					</div>

				</div>
				{/* <!-- /row --> */}
			</div>
			{/* <!-- /container --> */}
		</div>
		{/* <!-- /SECTION --> */}

    </MainLayout>
  );
};

export default HomePage;