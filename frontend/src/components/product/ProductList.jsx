import { useProducts } from "../../hooks/useProducts";
import SliderImport from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import ProductCard from "./ProductCard";
const dummyProducts = [
  { id: 1, name: "Laptop", price: 1000 },
  { id: 2, name: "Phone", price: 500 },
];

const settings = {
  dots: true,          // bottom dots
  arrows: true,        // next/prev arrows
  infinite: true,
  speed: 500,
  slidesToShow: 4,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 2000,
  pauseOnHover: true,
  swipeToSlide: true,
  centerMode: false,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
      },
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 2,
      },
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
      },
    },
  ],
};

export const ProductList = ()=>{
    const { data, isLoading, error } = useProducts();
    const Slider = SliderImport.default || SliderImport;
    if (isLoading) return <p>Loading...</p>;
    if (error) return <p>Error...</p>;
    if(!data) return <p>Null</p>;
    console.log('ProductCard type:', typeof ProductCard, ProductCard);
    // console.log('SlickSlider', typeof SlickSlider, SlickSlider);
    console.log('Slider', typeof Slider, Slider);
    return(
        <Slider {...settings}>                 
            {data.map((p) => (
                    <ProductCard key={p.id} product={p}/>
            ))}
        </Slider>
    );
};