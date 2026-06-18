import { API } from "./api";

export const getProducts = async () => {
  const res = await API.get("/catalog/api/products");
  return res.data;
};

export const getProductById = async (id) => {
  const res = await API.get(`/catalog/api/products/${id}`);
  return res.data;
};