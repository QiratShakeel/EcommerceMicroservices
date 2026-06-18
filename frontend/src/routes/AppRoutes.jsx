import { Routes, Route } from "react-router-dom";
import HomePage from "../pages/Home/HomePage";
import { ProductDetailPage } from "../pages/Product/ProductDetailPage";
import { CheckoutPage } from "../pages/Checkout/CheckoutPage";
import { CartPage } from "../pages/Cart/CartPage";
import { OrdersPage } from "../pages/Orders/OrdersPage";
import { LoginPage } from "../pages/Auth/LoginPage";
import { RegisterPage } from "../pages/Auth/RegisterPage";
import { ProductsPage } from "../pages/Product/ProductsPage";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/product/:id" element={<ProductDetailPage />} />
      <Route path="/checkout" element={<CheckoutPage />} />
      <Route path="/cart" element={<CartPage />} />
      <Route path="/orders" element={<OrdersPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/products" element={<ProductsPage />} />
    </Routes>
  );
};

export default AppRoutes;