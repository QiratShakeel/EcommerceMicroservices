import { useEffect, useRef } from "react";
import MainLayout from "../../layouts/MainLayout"

export const ProductsPage = () => {
    const sliderRef = useRef(null);
    const minRef = useRef(null);
    const maxRef = useRef(null);

    useEffect(() => {
        if (!sliderRef.current) return;

        const slider = sliderRef.current;

        noUiSlider.create(slider, {
            start: [1, 999],
            connect: true,
            step: 1,
            range: {
                min: 1,
                max: 999,
            },
        });

        slider.noUiSlider.on("update", (values, handle) => {
            if (handle === 0) {
                minRef.current.value = Math.round(values[0]);
            } else {
                maxRef.current.value = Math.round(values[1]);
            }
        });

        const updateSlider = (isMin, value) => {
            if (isMin) {
                slider.noUiSlider.set([value, null]);
            } else {
                slider.noUiSlider.set([null, value]);
            }
        };

        minRef.current.addEventListener("change", function () {
            updateSlider(true, this.value);
        });

        maxRef.current.addEventListener("change", function () {
            updateSlider(false, this.value);
        });
        return () => {
            if (slider.noUiSlider) {
                slider.noUiSlider.destroy();
            }
        };

    }, []);
    return (
        <MainLayout>
            {/* <!-- SECTION --> */}
            <div className="section">
                {/* <!-- container --> */}
                <div className="container">
                    {/* <!-- row --> */}
                    <div className="row">
                        {/* <!-- ASIDE --> */}
                        <div id="aside" className="col-md-3">
                            {/* <!-- aside Widget --> */}
                            <div className="aside">
                                <h3 className="aside-title">Categories</h3>
                                <div className="checkbox-filter">

                                    <div className="input-checkbox">
                                        <input type="checkbox" id="category-1" />
                                        <label htmlFor="category-1">
                                            <span></span>
                                            Laptops
                                            <small>(120)</small>
                                        </label>
                                    </div>

                                    <div className="input-checkbox">
                                        <input type="checkbox" id="category-2" />
                                        <label htmlFor="category-2">
                                            <span></span>
                                            Smartphones
                                            <small>(740)</small>
                                        </label>
                                    </div>

                                    <div className="input-checkbox">
                                        <input type="checkbox" id="category-3" />
                                        <label htmlFor="category-3">
                                            <span></span>
                                            Cameras
                                            <small>(1450)</small>
                                        </label>
                                    </div>

                                    <div className="input-checkbox">
                                        <input type="checkbox" id="category-4" />
                                        <label htmlFor="category-4">
                                            <span></span>
                                            Accessories
                                            <small>(578)</small>
                                        </label>
                                    </div>

                                    <div className="input-checkbox">
                                        <input type="checkbox" id="category-5" />
                                        <label htmlFor="category-5">
                                            <span></span>
                                            Laptops
                                            <small>(120)</small>
                                        </label>
                                    </div>

                                    <div className="input-checkbox">
                                        <input type="checkbox" id="category-6" />
                                        <label htmlFor="category-6">
                                            <span></span>
                                            Smartphones
                                            <small>(740)</small>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            {/* <!-- /aside Widget --> */}

                            {/* <!-- aside Widget --> */}
                            <div className="aside">
                                <h3 className="aside-title">Price</h3>
                                <div className="price-filter">
                                    <div id="price-slider" ref={sliderRef}></div>
                                    <div className="input-number price-min">
                                        <input id="price-min" ref={minRef} type="number" />
                                        <span className="qty-up">+</span>
                                        <span className="qty-down">-</span>
                                    </div>
                                    <span>-</span>
                                    <div className="input-number price-max">
                                        <input id="price-max" ref={maxRef} type="number" />
                                        <span className="qty-up">+</span>
                                        <span className="qty-down">-</span>
                                    </div>
                                </div>
                            </div>
                            {/* <!-- /aside Widget --> */}

                            {/* <!-- aside Widget --> */}
                            <div className="aside">
                                <h3 className="aside-title">Brand</h3>
                                <div className="checkbox-filter">
                                    <div className="input-checkbox">
                                        <input type="checkbox" id="brand-1" />
                                        <label htmlFor="brand-1">
                                            <span></span>
                                            SAMSUNG
                                            <small>(578)</small>
                                        </label>
                                    </div>
                                    <div className="input-checkbox">
                                        <input type="checkbox" id="brand-2" />
                                        <label htmlFor="brand-2">
                                            <span></span>
                                            LG
                                            <small>(125)</small>
                                        </label>
                                    </div>
                                    <div className="input-checkbox">
                                        <input type="checkbox" id="brand-3" />
                                        <label htmlFor="brand-3">
                                            <span></span>
                                            SONY
                                            <small>(755)</small>
                                        </label>
                                    </div>
                                    <div className="input-checkbox">
                                        <input type="checkbox" id="brand-4" />
                                        <label htmlFor="brand-4">
                                            <span></span>
                                            SAMSUNG
                                            <small>(578)</small>
                                        </label>
                                    </div>
                                    <div className="input-checkbox">
                                        <input type="checkbox" id="brand-5" />
                                        <label htmlFor="brand-5">
                                            <span></span>
                                            LG
                                            <small>(125)</small>
                                        </label>
                                    </div>
                                    <div className="input-checkbox">
                                        <input type="checkbox" id="brand-6" />
                                        <label htmlFor="brand-6">
                                            <span></span>
                                            SONY
                                            <small>(755)</small>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            {/* <!-- /aside Widget --> */}

                            {/* <!-- aside Widget --> */}
                            <div className="aside">
                                <h3 className="aside-title">Top selling</h3>
                                <div className="product-widget">
                                    <div className="product-img">
                                        <img src="/img/product01.png" alt="" />
                                    </div>
                                    <div className="product-body">
                                        <p className="product-category">Category</p>
                                        <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                        <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                    </div>
                                </div>

                                <div className="product-widget">
                                    <div className="product-img">
                                        <img src="/img/product02.png" alt="" />
                                    </div>
                                    <div className="product-body">
                                        <p className="product-category">Category</p>
                                        <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                        <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                    </div>
                                </div>

                                <div className="product-widget">
                                    <div className="product-img">
                                        <img src="/img/product03.png" alt="" />
                                    </div>
                                    <div className="product-body">
                                        <p className="product-category">Category</p>
                                        <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                        <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                    </div>
                                </div>
                            </div>
                            {/* <!-- /aside Widget --> */}
                        </div>
                        {/* <!-- /ASIDE --> */}

                        {/* <!-- STORE --> */}
                        <div id="store" className="col-md-9">
                            {/* <!-- store top filter --> */}
                            <div className="store-filter clearfix">
                                <div className="store-sort">
                                    <label>
                                        Sort By:
                                        <select className="input-select">
                                            <option value="0">Popular</option>
                                            <option value="1">Position</option>
                                        </select>
                                    </label>

                                    <label>
                                        Show:
                                        <select className="input-select">
                                            <option value="0">20</option>
                                            <option value="1">50</option>
                                        </select>
                                    </label>
                                </div>
                                <ul className="store-grid">
                                    <li className="active"><i className="fa fa-th"></i></li>
                                    <li><a href="#"><i className="fa fa-th-list"></i></a></li>
                                </ul>
                            </div>
                            {/* <!-- /store top filter --> */}

                            {/* <!-- store products --> */}
                            <div className="row">
                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product01.png" alt="" />
                                            <div className="product-label">
                                                <span className="sale">-30%</span>
                                                <span className="new">NEW</span>
                                            </div>
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product02.png" alt="" />
                                            <div className="product-label">
                                                <span className="new">NEW</span>
                                            </div>
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star-o"></i>
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                <div className="clearfix visible-sm visible-xs"></div>

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product03.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                <div className="clearfix visible-lg visible-md"></div>

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product04.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                <div className="clearfix visible-sm visible-xs"></div>

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product05.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product06.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star-o"></i>
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                <div className="clearfix visible-lg visible-md visible-sm visible-xs"></div>

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product07.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                                <i className="fa fa-star"></i>
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product08.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}

                                <div className="clearfix visible-sm visible-xs"></div>

                                {/* <!-- product --> */}
                                <div className="col-md-4 col-xs-6">
                                    <div className="product">
                                        <div className="product-img">
                                            <img src="/img/product09.png" alt="" />
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">Category</p>
                                            <h3 className="product-name"><a href="#">product name goes here</a></h3>
                                            <h4 className="product-price">$980.00 <del className="product-old-price">$990.00</del></h4>
                                            <div className="product-rating">
                                            </div>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist"><i className="fa fa-heart-o"></i><span className="tooltipp">add to wishlist</span></button>
                                                <button className="add-to-compare"><i className="fa fa-exchange"></i><span className="tooltipp">add to compare</span></button>
                                                <button className="quick-view"><i className="fa fa-eye"></i><span className="tooltipp">quick view</span></button>
                                            </div>
                                        </div>
                                        <div className="add-to-cart">
                                            <button className="add-to-cart-btn"><i className="fa fa-shopping-cart"></i> add to cart</button>
                                        </div>
                                    </div>
                                </div>
                                {/* <!-- /product --> */}
                            </div>
                            {/* <!-- /store products --> */}

                            {/* <!-- store bottom filter --> */}
                            <div className="store-filter clearfix">
                                <span className="store-qty">Showing 20-100 products</span>
                                <ul className="store-pagination">
                                    <li className="active">1</li>
                                    <li><a href="#">2</a></li>
                                    <li><a href="#">3</a></li>
                                    <li><a href="#">4</a></li>
                                    <li><a href="#"><i className="fa fa-angle-right"></i></a></li>
                                </ul>
                            </div>
                            {/* <!-- /store bottom filter --> */}
                        </div>
                        {/* <!-- /STORE --> */}
                    </div>
                    {/* <!-- /row --> */}
                </div>
                {/* <!-- /container --> */}
            </div>
            {/* <!-- /SECTION --> */}
        </MainLayout>
    )
}